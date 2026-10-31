using System.Diagnostics;
using System.Numerics;
using Microsoft.JSInterop;

namespace TheSingularityWorkshop.Learning.FsmApiShowcase;

/// <summary>
/// Browser-hosted Visualize backend that submits VisualizerFrame resources to WebGPU.
/// The browser owns the WebGPU device; this adapter owns frame/resource translation.
/// </summary>
public sealed class CampusWebGpuBackend(IJSRuntime js) : IVisualizerBackend
{
    private readonly Dictionary<ulong, ulong> _geometryRevisions = [];
    private long _frameNumber;
    private bool _disposed;

    public VisualizerCapabilities Capabilities { get; } =
        new("Visualize Campus WebGPU", "WebGPU", supportsCompute: false,
            supportsInstancing: false, supportsIndirectDrawing: false,
            supportsPresentationSurface: true);

    public async ValueTask<VisualizerFrameResult> RenderAsync(
        VisualizerFrame frame,
        IVisualizerResourceProvider resources,
        CancellationToken cancellationToken = default)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        ArgumentNullException.ThrowIfNull(frame);
        ArgumentNullException.ThrowIfNull(resources);
        var timer = Stopwatch.StartNew();

        var geometryIds = frame.Commands.Select(command => command.Geometry).Distinct().ToArray();
        var geometries = new List<object>(geometryIds.Length);
        foreach (var id in geometryIds)
        {
            if (!resources.TryGetGeometry(id, out var geometry) || geometry is null)
                throw new InvalidOperationException($"Missing geometry resource {id.Value}.");
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

        var draws = new List<object>(frame.Commands.Count);
        foreach (var command in frame.Commands)
        {
            if (command.Instances.IsValid && command.InstanceCount != 1)
                throw new NotSupportedException("This first WebGPU backend proof does not yet submit Visualizer instance buffers.");
            if (!resources.TryGetMaterial(command.Material, out var material) || material is null)
                throw new InvalidOperationException($"Missing material resource {command.Material.Value}.");
            draws.Add(new
            {
                geometry = command.Geometry.Value,
                indexCount = command.IndexCount,
                firstIndex = command.FirstIndex,
                baseVertex = command.BaseVertex,
                mvp = MatrixValues(command.WorldTransform * frame.View.View * frame.View.Projection),
                color = new[] { material.BaseColor.X, material.BaseColor.Y, material.BaseColor.Z, material.BaseColor.W }
            });
        }

        var payload = new { geometries, draws };
        await js.InvokeVoidAsync("learningCampusGpu.renderFrame", cancellationToken, "campus-gpu-canvas", payload);
        foreach (var id in geometryIds)
        {
            if (resources.TryGetGeometry(id, out var geometry) && geometry is not null)
                _geometryRevisions[id.Value] = geometry.Revision;
        }
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
            await js.InvokeVoidAsync("learningCampusGpu.dispose");
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
