using System.Diagnostics;
using System.Numerics;
using Microsoft.JSInterop;
using TheSingularityWorkshop.Visualize;

namespace TheSingularityWorkshop.Learning.FsmApiShowcase;

/// <summary>
/// Browser-hosted Visualize backend that submits VisualizerFrame resources to WebGPU.
/// The browser owns the WebGPU device; this adapter owns frame/resource translation.
/// </summary>
public sealed class CampusWebGpuBackend : IVisualizerBackend
{
    private readonly IJSRuntime _js;
    private readonly Dictionary<ulong, ulong> _geometryRevisions = [];
    private readonly Dictionary<ulong, ulong> _instanceRevisions = [];
    private long _frameNumber;
    private bool _disposed;
    private bool _initialized;

    public CampusWebGpuBackend(IJSRuntime js)
    {
        _js = js;
        Capabilities = new("Visualize Campus WebGPU", "WebGPU", supportsCompute: false,
            supportsInstancing: false, supportsIndirectDrawing: false,
            supportsPresentationSurface: false);
    }

    public VisualizerCapabilities Capabilities { get; private set; }

    public async Task InitializeAsync(string canvasId, CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        await _js.InvokeVoidAsync("learningCampusGpu.initializeCanvas", cancellationToken, canvasId);
        _initialized = true;
        Capabilities = new("Visualize Campus WebGPU", "WebGPU", supportsCompute: false,
            supportsInstancing: false, supportsIndirectDrawing: false,
            supportsPresentationSurface: true);
    }

    public async ValueTask<VisualizerFrameResult> RenderAsync(
        VisualizerFrame frame,
        IVisualizerResourceProvider resources,
        CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        if (!_initialized)
            throw new InvalidOperationException("Initialize the WebGPU canvas before submitting Visualizer frames.");
        ArgumentNullException.ThrowIfNull(frame);
        ArgumentNullException.ThrowIfNull(resources);
        var timer = Stopwatch.StartNew();

        var geometrySnapshots = new Dictionary<VisualizerResourceId, VisualizerGeometry>();
        var instanceSnapshots = new Dictionary<VisualizerResourceId, VisualizerInstanceBuffer>();
        var materialSnapshots = new Dictionary<VisualizerResourceId, VisualizerMaterial>();

        foreach (var command in frame.Commands)
        {
            if (!geometrySnapshots.ContainsKey(command.Geometry))
            {
                if (!resources.TryGetGeometry(command.Geometry, out var geometry) || geometry is null)
                    throw new InvalidOperationException($"Missing geometry resource {command.Geometry.Value}.");
                geometrySnapshots.Add(command.Geometry, geometry);
            }

            if (!materialSnapshots.ContainsKey(command.Material))
            {
                if (!resources.TryGetMaterial(command.Material, out var material) || material is null)
                    throw new InvalidOperationException($"Missing material resource {command.Material.Value}.");
                materialSnapshots.Add(command.Material, material);
            }

            if (command.Instances.IsValid && !instanceSnapshots.ContainsKey(command.Instances))
            {
                if (!resources.TryGetInstances(command.Instances, out var snapshot) || snapshot is null)
                    throw new InvalidOperationException($"Missing instance data resource {command.Instances.Value}.");
                instanceSnapshots.Add(command.Instances, snapshot);
            }

            var geometrySnapshot = geometrySnapshots[command.Geometry];
            if (command.FirstIndex > geometrySnapshot.Indices.Count ||
                command.IndexCount > geometrySnapshot.Indices.Count - command.FirstIndex)
            {
                throw new InvalidOperationException(
                    $"Draw command index range [{command.FirstIndex}, " +
                    $"{(long)command.FirstIndex + command.IndexCount}) exceeds geometry resource " +
                    $"{command.Geometry.Value} index count {geometrySnapshot.Indices.Count}.");
            }
            for (var indexOffset = command.FirstIndex; indexOffset < command.FirstIndex + command.IndexCount; indexOffset++)
            {
                var effectiveVertex = (long)geometrySnapshot.Indices[indexOffset] + command.BaseVertex;
                if (effectiveVertex < 0 || effectiveVertex >= geometrySnapshot.Vertices.Count)
                    throw new InvalidOperationException(
                        $"Draw command index at offset {indexOffset} resolves to vertex {effectiveVertex}, " +
                        $"outside geometry resource {command.Geometry.Value} vertex count {geometrySnapshot.Vertices.Count}.");
            }

            if (command.Instances.IsValid)
            {
                var snapshot = instanceSnapshots[command.Instances];
                if (command.FirstInstance > snapshot.Instances.Count ||
                    command.InstanceCount > snapshot.Instances.Count - command.FirstInstance)
                {
                    throw new InvalidOperationException(
                        $"Draw command instance range [{command.FirstInstance}, " +
                        $"{(long)command.FirstInstance + command.InstanceCount}) exceeds instance resource " +
                        $"{command.Instances.Value} count {snapshot.Instances.Count}.");
                }
            }
        }

        var geometryIds = geometrySnapshots.Keys.ToArray();
        var geometries = new List<object>(geometryIds.Length);
        foreach (var id in geometryIds)
        {
            var geometry = geometrySnapshots[id];
            if (_geometryRevisions.TryGetValue(id.Value, out var uploadedRevision) && uploadedRevision == geometry.Revision)
                continue;

            var vertices = new float[geometry.Vertices.Count * 12];
            for (var i = 0; i < geometry.Vertices.Count; i++)
            {
                var vertex = geometry.Vertices[i];
                var offset = i * 12;
                vertices[offset] = vertex.Position.X;
                vertices[offset + 1] = vertex.Position.Y;
                vertices[offset + 2] = vertex.Position.Z;
                vertices[offset + 3] = vertex.Normal.X;
                vertices[offset + 4] = vertex.Normal.Y;
                vertices[offset + 5] = vertex.Normal.Z;
                vertices[offset + 6] = vertex.TextureCoordinate.X;
                vertices[offset + 7] = vertex.TextureCoordinate.Y;
                vertices[offset + 8] = vertex.Color.X;
                vertices[offset + 9] = vertex.Color.Y;
                vertices[offset + 10] = vertex.Color.Z;
                vertices[offset + 11] = vertex.Color.W;
            }

            geometries.Add(new
            {
                id = id.Value,
                revision = geometry.Revision,
                vertices,
                indices = geometry.Indices
            });
        }

        var instanceIds = instanceSnapshots.Keys.ToArray();
        var instances = new List<object>(instanceIds.Length);
        foreach (var id in instanceIds)
        {
            var snapshot = instanceSnapshots[id];
            if (_instanceRevisions.TryGetValue(id.Value, out var uploadedRevision) && uploadedRevision == snapshot.Revision)
                continue;

            var data = new float[snapshot.Instances.Count * 20];
            for (var i = 0; i < snapshot.Instances.Count; i++)
            {
                var instance = snapshot.Instances[i];
                Array.Copy(MatrixValues(instance.WorldTransform), 0, data, i * 20, 16);
                data[i * 20 + 16] = instance.Color.X;
                data[i * 20 + 17] = instance.Color.Y;
                data[i * 20 + 18] = instance.Color.Z;
                data[i * 20 + 19] = instance.Color.W;
            }
            instances.Add(new { id = id.Value, revision = snapshot.Revision, data });
        }

        var draws = new List<object>(frame.Commands.Count);
        foreach (var command in frame.Commands)
        {
            var material = materialSnapshots[command.Material];
            draws.Add(new
            {
                geometry = command.Geometry.Value,
                instanceResource = command.Instances.IsValid ? command.Instances.Value : 0UL,
                instanceCount = command.InstanceCount,
                firstInstance = command.FirstInstance,
                indexCount = command.IndexCount,
                firstIndex = command.FirstIndex,
                baseVertex = command.BaseVertex,
                mvp = MatrixValues(command.WorldTransform * frame.View.View * frame.View.Projection),
                color = new[] { material.BaseColor.X, material.BaseColor.Y, material.BaseColor.Z, material.BaseColor.W }
            });
        }

        var payload = new { geometries, instances, draws };
        await _js.InvokeVoidAsync("learningCampusGpu.renderFrame", cancellationToken, "campus-gpu-canvas", payload);
        foreach (var pair in geometrySnapshots)
            _geometryRevisions[pair.Key.Value] = pair.Value.Revision;
        foreach (var pair in instanceSnapshots)
            _instanceRevisions[pair.Key.Value] = pair.Value.Revision;
        timer.Stop();
        return new VisualizerFrameResult(
            Interlocked.Increment(ref _frameNumber),
            frame.Commands.Count,
            timer.Elapsed,
            gpuExecutionTime: null,
            wasPresented: true);
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;
        _disposed = true;
        try
        {
            await _js.InvokeVoidAsync("learningCampusGpu.dispose");
        }
        catch (JSDisconnectedException) { }
        catch (InvalidOperationException) { }
    }

    private static float[] MatrixValues(Matrix4x4 matrix) =>
    [
        matrix.M11, matrix.M12, matrix.M13, matrix.M14,
        matrix.M21, matrix.M22, matrix.M23, matrix.M24,
        matrix.M31, matrix.M32, matrix.M33, matrix.M34,
        matrix.M41, matrix.M42, matrix.M43, matrix.M44
    ];
}
