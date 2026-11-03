using System.Diagnostics;
using System.Numerics;
using Microsoft.JSInterop;
using TheSingularityWorkshop.Visualize;
using TheSingularityWorkshop.Visualize.GPU;

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

        var resolved = VisualizerFrameResolver.Resolve(frame, resources);
        var geometryIds = resolved.Select(item => item.Command.Geometry).Distinct().ToArray();
        var geometries = new List<object>(geometryIds.Length);
        foreach (var id in geometryIds)
        {
            var geometry = resolved.First(item => item.Command.Geometry == id).Geometry;
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

        var instanceIds = resolved.Where(item => item.Instances is not null)
            .Select(item => item.Command.Instances).Distinct().ToArray();
        var instances = new List<object>(instanceIds.Length);
        foreach (var id in instanceIds)
        {
            var snapshot = resolved.First(item => item.Command.Instances == id).Instances!;
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

        var draws = new List<object>(resolved.Count);
        foreach (var item in resolved)
        {
            var command = item.Command;
            var material = item.Material;
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
        foreach (var item in resolved)
            _geometryRevisions[item.Command.Geometry.Value] = item.Geometry.Revision;
        foreach (var item in resolved.Where(item => item.Instances is not null))
            _instanceRevisions[item.Command.Instances.Value] = item.Instances!.Revision;
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
