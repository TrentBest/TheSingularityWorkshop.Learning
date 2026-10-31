using System.Globalization;
using System.Numerics;
using System.Text.Json;
using System.Text.RegularExpressions;
using TheSingularityWorkshop.Visualize;

if (args.Length != 6)
    throw new ArgumentException("Expected city SVG, campus SVG, city JSON, campus JSON, city catalog source, and campus catalog source paths.");

var citySvg = Path.GetFullPath(args[0]);
var campusSvg = Path.GetFullPath(args[1]);
var cityJson = Path.GetFullPath(args[2]);
var campusJson = Path.GetFullPath(args[3]);
var citySource = File.ReadAllText(args[4]);
var campusSource = File.ReadAllText(args[5]);
Directory.CreateDirectory(Path.GetDirectoryName(citySvg)!);
Directory.CreateDirectory(Path.GetDirectoryName(campusSvg)!);
Directory.CreateDirectory(Path.GetDirectoryName(cityJson)!);
Directory.CreateDirectory(Path.GetDirectoryName(campusJson)!);

var cityStructures = ParseStructures(citySource, isCampus: false);
var campusBuildings = ParseStructures(campusSource, isCampus: true);
if (!cityStructures.Any(item => item.Id == "city-university"))
    throw new InvalidDataException("The WebPage city catalog does not contain the University Quarter.");
if (!campusBuildings.Any(item => item.Id == "fsm-bench") ||
    !campusBuildings.Any(item => item.Id == "acoustic-studio-lab"))
    throw new InvalidDataException("The WebPage campus catalog is missing its expected Workshop learning buildings.");

await RenderMap(citySvg, cityStructures, isCampus: false);
await RenderMap(campusSvg, campusBuildings, isCampus: true);
File.WriteAllText(cityJson, JsonSerializer.Serialize(cityStructures));
File.WriteAllText(campusJson, JsonSerializer.Serialize(campusBuildings));
Console.WriteLine($"Visualize CPU/SVG city view: {cityStructures.Count} WebPage catalog places -> {citySvg}");
Console.WriteLine($"Visualize CPU/SVG campus view: {campusBuildings.Count} WebPage catalog buildings -> {campusSvg}");

static List<Place> ParseStructures(string source, bool isCampus)
{
    var linePattern = new Regex(
        @"new\(\s*""(?<id>[^""]+)""\s*,\s*""(?<name>[^""]+)""\s*,\s*new SpatialBounds\((?<bounds>[^)]+)\)(?<tail>[^\r\n]*)",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);
    var results = new List<Place>();
    foreach (Match match in linePattern.Matches(source))
    {
        var bounds = match.Groups["bounds"].Value.Split(',')
            .Select(value => double.Parse(value.Trim(), CultureInfo.InvariantCulture))
            .ToArray();
        if (bounds.Length != 4)
            continue;

        var tail = match.Groups["tail"].Value;
        var kind = Regex.Match(tail, @"SpatialCityStructureKind\.(?<kind>[A-Za-z]+)");
        var accentMatch = Regex.Match(tail, @"Accent:\s*""(?<color>#[0-9a-fA-F]{6})""");
        var fillMatch = Regex.Match(tail, @"Fill:\s*""(?<color>#[0-9a-fA-F]{6})""");
        var kindName = kind.Success ? kind.Groups["kind"].Value : "Campus";
        var accent = accentMatch.Success ? accentMatch.Groups["color"].Value : CityAccent(kindName);
        var fill = fillMatch.Success ? fillMatch.Groups["color"].Value : CityFill(kindName);
        var columnsMatch = Regex.Match(tail, @"Columns:\s*(?<value>\d+)");
        var rowsMatch = Regex.Match(tail, @"Rows:\s*(?<value>\d+)");
        var future = Regex.Match(tail, @"SpatialCityStructureKind\.[A-Za-z]+,\s*""(?<future>[^""]+)""");
        results.Add(new Place(
            match.Groups["id"].Value,
            match.Groups["name"].Value,
            bounds[0], bounds[1], bounds[2], bounds[3],
            accent, fill, kindName,
            columnsMatch.Success ? int.Parse(columnsMatch.Groups["value"].Value, CultureInfo.InvariantCulture) : Math.Max(2, (int)Math.Ceiling(bounds[2] / 5)),
            rowsMatch.Success ? int.Parse(rowsMatch.Groups["value"].Value, CultureInfo.InvariantCulture) : Math.Max(2, (int)Math.Ceiling(bounds[3] / 5)),
            future.Success ? future.Groups["future"].Value : ""));
    }

    return results;
}

static async Task RenderMap(string output, IReadOnlyList<Place> places, bool isCampus)
{
    var vertices = new List<VisualizerVertex>();
    var indices = new List<uint>();

    void Triangle(double ax, double ay, double bx, double by, double cx, double cy, string color)
    {
        var rgba = LinearColor(color);
        foreach (var point in new[] { (X: ax, Y: ay), (X: bx, Y: by), (X: cx, Y: cy) })
        {
            indices.Add((uint)vertices.Count);
            vertices.Add(new VisualizerVertex(
                new Vector3((float)(point.X / 50.0 - 1.0), (float)(1.0 - point.Y / 50.0), 0),
                Vector3.UnitZ,
                Vector2.Zero,
                rgba));
        }
    }

    void Rect(double x, double y, double width, double height, string color)
    {
        if (width <= 0 || height <= 0) return;
        Triangle(x, y, x + width, y, x + width, y + height, color);
        Triangle(x, y, x + width, y + height, x, y + height, color);
    }

    void Border(double x, double y, double width, double height, string color, double thickness)
    {
        Rect(x, y, width, thickness, color);
        Rect(x, y + height - thickness, width, thickness, color);
        Rect(x, y, thickness, height, color);
        Rect(x + width - thickness, y, thickness, height, color);
    }

    // Tile grid and main avenues make the world read as a playable block-built map.
    Rect(0, 0, 100, 100, isCampus ? "#183a33" : "#152d38");
    for (var p = 5; p < 100; p += 10)
    {
        Rect(p, 0, .22, 100, isCampus ? "#21413a" : "#1a3540");
        Rect(0, p, 100, .22, isCampus ? "#21413a" : "#1a3540");
    }

    if (isCampus)
    {
        Rect(0, 18, 100, 2.2, "#4b4a3e");
        Rect(0, 51, 100, 2.2, "#4b4a3e");
        Rect(22, 0, 2.2, 100, "#4b4a3e");
        Rect(62, 0, 2.2, 100, "#4b4a3e");
        Rect(39, 32, 24, 24, "#586052");
        Rect(43, 36, 16, 16, "#68705a");
        Border(39, 32, 24, 24, "#8cbf87", .55);
        for (var i = 0; i < 18; i++)
        {
            var tx = 5 + (i * 23 % 90);
            var ty = 3 + (i * 37 % 92);
            var nearBuilding = places.Any(p => tx >= p.X - 2 && tx <= p.X + p.Width + 2 && ty >= p.Y - 2 && ty <= p.Y + p.Height + 2);
            if (!nearBuilding) Tree(tx, ty);
        }
    }
    else
    {
        // Waterfront and transit bands give the city view context for the University Quarter.
        Rect(0, 0, 100, 5, "#234c59");
        Rect(0, 95, 100, 5, "#234c59");
        Rect(0, 48.7, 100, 2.6, "#484943");
        Rect(49, 0, 2.2, 100, "#484943");
        Rect(4, 4, 17, 16, "#24483c");
        Rect(80, 4, 16, 14, "#24483c");
        Rect(3, 82, 23, 11, "#24483c");
    }

    foreach (var place in places.OrderBy(item => item.Y).ThenBy(item => item.X))
    {
        Rect(place.X + .8, place.Y + 1.1, place.Width, place.Height, "#0a141a");
        Rect(place.X, place.Y, place.Width, place.Height, place.Fill);
        var tileWidth = Math.Max(1.4, place.Width / Math.Clamp(place.Columns, 2, 12));
        var tileHeight = Math.Max(1.4, place.Height / Math.Clamp(place.Rows, 2, 12));
        for (var row = 0; row < Math.Clamp(place.Rows, 2, 12); row++)
        for (var column = 0; column < Math.Clamp(place.Columns, 2, 12); column++)
        {
            var tileX = place.X + .8 + column * tileWidth;
            var tileY = place.Y + .8 + row * tileHeight;
            var tileW = Math.Min(tileWidth - .8, place.X + place.Width - tileX - .5);
            var tileH = Math.Min(tileHeight - .8, place.Y + place.Height - tileY - .5);
            if (tileW > 0 && tileH > 0 && (row + column) % 3 != 0)
                Rect(tileX, tileY, tileW, tileH, (row + column) % 2 == 0 ? place.Fill : place.Accent);
        }
        Border(place.X, place.Y, place.Width, place.Height, place.Accent, .55);
        var doorWidth = Math.Min(2.4, Math.Max(.8, place.Width * .12));
        Rect(place.X + (place.Width - doorWidth) / 2, place.Y + place.Height - 1.5, doorWidth, 1.5, "#d8b56c");
        if (place.Id == "city-university")
        {
            Border(place.X - 1.2, place.Y - 1.2, place.Width + 2.4, place.Height + 2.4, "#ffd27d", .7);
            Rect(place.X + place.Width * .34, place.Y + place.Height * .4, place.Width * .32, place.Height * .22, "#66e0be");
        }
    }

    if (isCampus)
    {
        for (var i = 0; i < 9; i++)
        {
            var x = 31 + (i % 3) * 3.2;
            var y = 40 + (i / 3) * 3.1;
            StudentBlock(x, y, i % 3 == 0 ? "#ffd27d" : i % 3 == 1 ? "#71c9ff" : "#66e0be");
        }
    }

    var catalog = new VisualizerResourceCatalog();
    var geometryId = new VisualizerResourceId(1);
    var materialId = new VisualizerResourceId(2);
    catalog.SetGeometry(geometryId, new VisualizerGeometry(1, vertices, indices));
    catalog.SetMaterial(materialId, new VisualizerMaterial(1, Vector4.One));
    var frame = new VisualizerFrame(
        new VisualizerView(Vector3.Zero, Matrix4x4.Identity, Matrix4x4.Identity),
        [new VisualizerDrawCommand(geometryId, materialId, Matrix4x4.Identity, indices.Count, 0, 0, VisualizerRepresentation.Near)]);
    await using var visualizer = new Visualizer(
        new SvgVisualizerBackend(svg => File.WriteAllText(output, svg), width: 1200, height: 1200, background: "#0b1720"),
        catalog);
    await visualizer.RenderAsync(frame);

    void Tree(double x, double y)
    {
        Rect(x + .55, y + .55, 1.4, 1.4, "#523c2d");
        Rect(x, y, 2.5, 2.5, "#4e9862");
        Rect(x + .35, y + .35, 1.8, 1.8, "#69bc72");
    }

    void StudentBlock(double x, double y, string color)
    {
        Rect(x, y + .7, .9, 1.4, color);
        Rect(x + .15, y, .6, .7, "#ead3ad");
    }
}

static Vector4 LinearColor(string hex)
{
    static float Channel(string value)
    {
        var srgb = Convert.ToInt32(value, 16) / 255f;
        return srgb <= 0.04045f ? srgb / 12.92f : MathF.Pow((srgb + 0.055f) / 1.055f, 2.4f);
    }
    return new Vector4(Channel(hex[1..3]), Channel(hex[3..5]), Channel(hex[5..7]), 1);
}

static string CityAccent(string kind) => kind switch
{
    "Education" or "Science" or "Technology" => "#71c9ff",
    "Culture" => "#a883c9",
    "Park" => "#66e0be",
    "Transport" or "Port" => "#ffd27d",
    "Commerce" or "Hospitality" => "#ff9a67",
    "Civic" => "#ffd27d",
    "Residential" => "#72c09f",
    _ => "#8ca4b5"
};

static string CityFill(string kind) => kind switch
{
    "Park" => "#295b44",
    "Education" or "Science" or "Technology" => "#203b52",
    "Culture" => "#352b48",
    "Transport" or "Port" => "#3d3940",
    "Commerce" or "Hospitality" => "#49372e",
    "Civic" => "#4a4030",
    "Residential" => "#2b433d",
    _ => "#303c45"
};

sealed record Place(string Id, string Name, double X, double Y, double Width, double Height, string Accent, string Fill, string Kind, int Columns, int Rows, string FutureFunction);
