using System.Numerics;
using TheSingularityWorkshop.Visualize;

var output = Path.GetFullPath(args.Length > 0 ? args[0] : "arrival-scene.svg");
Directory.CreateDirectory(Path.GetDirectoryName(output)!);

var vertices = new List<VisualizerVertex>();
var indices = new List<uint>();

void Triangle((float X, float Y) a, (float X, float Y) b, (float X, float Y) c, string color)
{
    var rgba = LinearColor(color);
    foreach (var point in new[] { a, b, c })
    {
        indices.Add((uint)vertices.Count);
        vertices.Add(new VisualizerVertex(
            new Vector3(point.X, point.Y, 0),
            Vector3.UnitZ,
            Vector2.Zero,
            rgba));
    }
}

void Quad((float X, float Y) a, (float X, float Y) b, (float X, float Y) c, (float X, float Y) d, string color)
{
    Triangle(a, b, c, color);
    Triangle(a, c, d, color);
}

void Polygon((float X, float Y)[] points, string color)
{
    for (var i = 1; i < points.Length - 1; i++)
        Triangle(points[0], points[i], points[i + 1], color);
}

// The first triangles form a narrow, nearly black passage. The later triangles
// form the cavern that the browser reveals after its camera turn.
Polygon([(-1, 1), (1, 1), (0.29f, 0.36f), (-0.29f, 0.36f)], "#090e13");
Polygon([(-1, 1), (-0.29f, 0.36f), (-0.38f, -0.24f), (-1, -1)], "#10171c");
Polygon([(1, 1), (0.29f, 0.36f), (0.38f, -0.24f), (1, -1)], "#0e151a");
Polygon([(-1, -1), (-0.38f, -0.24f), (0.38f, -0.24f), (1, -1)], "#101316");
Quad((-0.91f, 0.73f), (-0.29f, 0.36f), (-0.36f, -0.21f), (-0.83f, -0.7f), "#1b2428");
Quad((0.91f, 0.73f), (0.29f, 0.36f), (0.36f, -0.21f), (0.83f, -0.7f), "#1a2327");
Quad((-0.79f, -0.76f), (-0.36f, -0.21f), (0.36f, -0.21f), (0.79f, -0.76f), "#252626");

// Torch-lit opening and distant chamber, composed as Visualize geometry.
Polygon([(-0.52f, 0.18f), (-0.18f, 0.42f), (0.25f, 0.4f), (0.55f, 0.08f), (0.59f, -0.4f), (-0.55f, -0.4f)], "#11191e");
Polygon([(-0.47f, 0.05f), (-0.12f, 0.25f), (0.25f, 0.23f), (0.48f, -0.02f), (0.52f, -0.35f), (-0.5f, -0.35f)], "#090e12");
Polygon([(-1, 0.22f), (-0.51f, 0.05f), (-0.5f, -0.36f), (-1, -0.82f)], "#080d12");
Polygon([(1, 0.22f), (0.51f, 0.05f), (0.5f, -0.36f), (1, -0.82f)], "#080d12");
Quad((-0.64f, -0.58f), (-0.5f, -0.34f), (0.5f, -0.34f), (0.77f, -0.7f), "#344149");
Quad((-0.18f, -0.34f), (0.18f, -0.34f), (0.28f, -0.72f), (-0.27f, -0.72f), "#151e22");

// Torch bracket, shaft, and a small authored flame silhouette. The flame is a
// presentation shape; the Physics branch does not currently expose combustion.
Quad((-0.045f, -0.13f), (0.045f, -0.13f), (0.055f, 0.13f), (-0.055f, 0.13f), "#704820");
Quad((-0.09f, 0.1f), (0.09f, 0.1f), (0.12f, 0.14f), (-0.12f, 0.14f), "#9b6d35");
Polygon([(0, 0.15f), (-0.08f, 0.25f), (-0.04f, 0.38f), (0.005f, 0.31f), (0.07f, 0.42f), (0.1f, 0.25f), (0.04f, 0.15f)], "#ff9d38");
Polygon([(0.015f, 0.16f), (-0.025f, 0.25f), (0.02f, 0.31f), (0.055f, 0.22f), (0.035f, 0.16f)], "#ffe3a0");

// Edge glints guide the eye down the passage and out across the cavern.
Quad((-0.88f, 0.72f), (-0.84f, 0.72f), (-0.36f, -0.21f), (-0.4f, -0.21f), "#303b3e");
Quad((0.88f, 0.72f), (0.84f, 0.72f), (0.36f, -0.21f), (0.4f, -0.21f), "#303b3e");
Quad((-0.74f, -0.65f), (-0.7f, -0.65f), (-0.35f, -0.24f), (-0.39f, -0.24f), "#4a4940");
Quad((0.74f, -0.65f), (0.7f, -0.65f), (0.35f, -0.24f), (0.39f, -0.24f), "#4a4940");

var catalog = new VisualizerResourceCatalog();
var geometryId = new VisualizerResourceId(1);
var materialId = new VisualizerResourceId(2);
catalog.SetGeometry(geometryId, new VisualizerGeometry(1, vertices, indices));
catalog.SetMaterial(materialId, new VisualizerMaterial(1, Vector4.One));

var frame = new VisualizerFrame(
    new VisualizerView(Vector3.Zero, Matrix4x4.Identity, Matrix4x4.Identity),
    [new VisualizerDrawCommand(
        geometryId,
        materialId,
        Matrix4x4.Identity,
        indices.Count,
        0,
        0,
        VisualizerRepresentation.Near)]);

await using var visualizer = new Visualizer(
    new SvgVisualizerBackend(svg => File.WriteAllText(output, svg), width: 1440, height: 900, background: "#030609"),
    catalog);
var result = await visualizer.RenderAsync(frame);
Console.WriteLine($"Visualize CPU/SVG artifact: {result.SubmittedDrawCount} draw command(s) -> {output}");

static Vector4 LinearColor(string hex)
{
    static float Channel(string value)
    {
        var srgb = Convert.ToInt32(value, 16) / 255f;
        return srgb <= 0.04045f ? srgb / 12.92f : MathF.Pow((srgb + 0.055f) / 1.055f, 2.4f);
    }

    return new Vector4(Channel(hex[1..3]), Channel(hex[3..5]), Channel(hex[5..7]), 1);
}
