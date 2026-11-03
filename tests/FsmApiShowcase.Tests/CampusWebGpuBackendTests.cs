using System.Numerics;
using System.Text.Json;
using Microsoft.JSInterop;
using TheSingularityWorkshop.Learning.FsmApiShowcase;
using TheSingularityWorkshop.Visualize;
using Xunit;

namespace TheSingularityWorkshop.Learning.Tests;

public sealed class CampusWebGpuBackendTests
{
    private static readonly VisualizerResourceId GeometryId = new(1);
    private static readonly VisualizerResourceId MaterialId = new(2);
    private static readonly VisualizerResourceId InstanceId = new(3);

    [Fact]
    public async Task RenderAsync_PacksTransformAndTintIntoTwentyFloatInstanceRecords()
    {
        var js = new RecordingJsRuntime();
        var backend = await CreateBackend(js);
        var resources = CreateResources();
        resources.SetInstances(InstanceId, new VisualizerInstanceBuffer(7,
        [
            new VisualizerInstance(Matrix4x4.CreateTranslation(1, 2, 3), new Vector4(.1f, .2f, .3f, .4f)),
            new VisualizerInstance(Matrix4x4.Identity, Vector4.One)
        ]));

        await backend.RenderAsync(CreateFrame(InstanceId, 2), resources);

        using var payload = ReadLastPayload(js);
        var packed = payload.RootElement.GetProperty("instances")[0].GetProperty("data");
        Assert.Equal(40, packed.GetArrayLength());
        Assert.Equal(new float[]
        {
            1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 1, 2, 3, 1, .1f, .2f, .3f, .4f
        }, packed.EnumerateArray().Take(20).Select(x => x.GetSingle()).ToArray());
        var draw = payload.RootElement.GetProperty("draws")[0];
        Assert.Equal(3UL, draw.GetProperty("instanceResource").GetUInt64());
        Assert.Equal(2, draw.GetProperty("instanceCount").GetInt32());
        Assert.Equal(0, draw.GetProperty("firstInstance").GetInt32());
        Assert.False(backend.Capabilities.SupportsInstancing);
    }

    [Fact]
    public async Task RenderAsync_OnlyUploadsChangedInstanceRevisions()
    {
        var js = new RecordingJsRuntime();
        var backend = await CreateBackend(js);
        var resources = CreateResources();
        resources.SetInstances(InstanceId, new VisualizerInstanceBuffer(1,
            [new VisualizerInstance(Matrix4x4.Identity, Vector4.One)]));
        var frame = CreateFrame(InstanceId, 1);

        await backend.RenderAsync(frame, resources);
        using (var first = ReadPayload(js, 1))
        {
            Assert.Equal(1, first.RootElement.GetProperty("instances").GetArrayLength());
            Assert.Equal(1UL, first.RootElement.GetProperty("instances")[0].GetProperty("revision").GetUInt64());
        }

        await backend.RenderAsync(frame, resources);
        using (var unchanged = ReadLastPayload(js))
            Assert.Equal(0, unchanged.RootElement.GetProperty("instances").GetArrayLength());

        resources.SetInstances(InstanceId, new VisualizerInstanceBuffer(2,
            [new VisualizerInstance(Matrix4x4.CreateTranslation(4, 0, 0), Vector4.One)]));
        await backend.RenderAsync(frame, resources);
        using var changed = ReadLastPayload(js);
        Assert.Equal(1, changed.RootElement.GetProperty("instances").GetArrayLength());
        Assert.Equal(2UL, changed.RootElement.GetProperty("instances")[0].GetProperty("revision").GetUInt64());
    }

    [Fact]
    public async Task RenderAsync_RejectsMissingInstanceResource()
    {
        var backend = await CreateBackend(new RecordingJsRuntime());
        var resources = CreateResources();

        var error = await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await backend.RenderAsync(CreateFrame(InstanceId, 1), resources));

        Assert.Contains("Missing instance data resource 3", error.Message);
    }

    [Fact]
    public async Task RenderAsync_RejectsInstanceRangeOutsideResource()
    {
        var backend = await CreateBackend(new RecordingJsRuntime());
        var resources = CreateResources();
        resources.SetInstances(InstanceId, new VisualizerInstanceBuffer(1,
            [new VisualizerInstance(Matrix4x4.Identity, Vector4.One)]));

        var error = await Assert.ThrowsAsync<InvalidOperationException>(
            async () => await backend.RenderAsync(CreateFrame(InstanceId, 2), resources));

        Assert.Contains("exceeds instance resource 3 count 1", error.Message);
    }

    [Fact]
    public async Task RenderAsync_UsesIdentityFallbackForNonInstancedDraw()
    {
        var js = new RecordingJsRuntime();
        var backend = await CreateBackend(js);

        await backend.RenderAsync(CreateFrame(default, 1), CreateResources());

        using var payload = ReadLastPayload(js);
        var draw = payload.RootElement.GetProperty("draws")[0];
        Assert.Equal(0UL, draw.GetProperty("instanceResource").GetUInt64());
        Assert.Equal(1, draw.GetProperty("instanceCount").GetInt32());
        Assert.Equal(0, draw.GetProperty("firstInstance").GetInt32());
        Assert.Equal(0, payload.RootElement.GetProperty("instances").GetArrayLength());
    }

    private static async Task<CampusWebGpuBackend> CreateBackend(RecordingJsRuntime js)
    {
        var backend = new CampusWebGpuBackend(js);
        await backend.InitializeAsync("test-canvas");
        return backend;
    }

    private static VisualizerResourceCatalog CreateResources()
    {
        var resources = new VisualizerResourceCatalog();
        resources.SetGeometry(GeometryId, new VisualizerGeometry(1,
        [
            new VisualizerVertex(Vector3.Zero, Vector3.UnitZ, Vector2.Zero, Vector4.One),
            new VisualizerVertex(Vector3.UnitX, Vector3.UnitZ, Vector2.UnitX, Vector4.One),
            new VisualizerVertex(Vector3.UnitY, Vector3.UnitZ, Vector2.UnitY, Vector4.One)
        ], [0, 1, 2]));
        resources.SetMaterial(MaterialId, new VisualizerMaterial(1, Vector4.One));
        return resources;
    }

    private static VisualizerFrame CreateFrame(VisualizerResourceId instances, int instanceCount)
    {
        var command = new VisualizerDrawCommand(
            GeometryId, MaterialId, Matrix4x4.Identity, 3, 0, 0,
            VisualizerRepresentation.Near, instances, instanceCount, 0);
        return new VisualizerFrame(
            new VisualizerView(Vector3.Zero, Matrix4x4.Identity, Matrix4x4.Identity),
            [command]);
    }

    private static JsonDocument ReadLastPayload(RecordingJsRuntime js) =>
        ReadPayload(js, js.Calls.Count - 1);

    private static JsonDocument ReadPayload(RecordingJsRuntime js, int callIndex)
    {
        var call = js.Calls.Where(x => x.Identifier == "learningCampusGpu.renderFrame").ElementAt(callIndex);
        return JsonDocument.Parse(JsonSerializer.Serialize(call.Arguments![1]));
    }

    private sealed class RecordingJsRuntime : IJSRuntime
    {
        public List<JsCall> Calls { get; } = [];

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
        {
            Calls.Add(new JsCall(identifier, args));
            return ValueTask.FromResult(default(TValue)!);
        }

        public ValueTask<TValue> InvokeAsync<TValue>(
            string identifier, CancellationToken cancellationToken, object?[]? args)
        {
            Calls.Add(new JsCall(identifier, args));
            return ValueTask.FromResult(default(TValue)!);
        }
    }

    private sealed record JsCall(string Identifier, object?[]? Arguments);
}
