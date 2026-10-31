using System.Globalization;
using TheSingularityWorkshop.FSM_API;
using TheSingularityWorkshop.Workshop.Gui;
using TheSingularityWorkshop.Workshop.Gui.Svg;

namespace TheSingularityWorkshop.LearningExplorer;

public sealed class OscillatorRuntime : IDisposable
{
    private const string Group = "LearningCenter.Oscillator";
    private const string Machine = "LearningCenter.BoundedOscillator";
    private const double Minimum = 0;
    private const double Maximum = 10;
    private const int VisibleTicks = 12;
    private readonly OscillatorContext _context;
    private readonly FSMHandle _handle;
    private int _tick;

    public OscillatorRuntime(double step)
    {
        _context = new OscillatorContext("live-learning-oscillator", Minimum, Maximum, step);

        FSM_API.Create.CreateFiniteStateMachine(Machine, processRate: -1, processingGroup: Group)
            .State("Forward", null, state => Advance((OscillatorContext)state, +1), null)
            .State("Reverse", null, state => Advance((OscillatorContext)state, -1), null)
            .WithInitialState("Forward")
            .Transition("Forward", "Reverse", state =>
                ((OscillatorContext)state).Value >= ((OscillatorContext)state).Maximum)
            .Transition("Reverse", "Forward", state =>
                ((OscillatorContext)state).Value <= ((OscillatorContext)state).Minimum)
            .BuildDefinition();

        _handle = FSM_API.Create.CreateInstance(Machine, _context, Group);
        Samples.Add(new TickSnapshot(_tick, _context.Value, _handle.CurrentState));
    }

    public double Step => _context.StepMagnitude;
    public double Value => _context.Value;
    public string State => _handle.CurrentState;
    public int TickNumber => _tick;
    public List<TickSnapshot> Samples { get; } = [];

    public void Tick()
    {
        FSM_API.Interaction.Update(Group);
        _tick++;
        Samples.Add(new TickSnapshot(_tick, _context.Value, _handle.CurrentState));
    }

    public RenderFragment BuildRuntimeSummary()
    {
        var summary = GuiBuilder.Create("Panel", "fsm-live-summary")
            .Text("Current FSM_API state")
            .Property("class", "runtime-summary")
            .Child("Text", "fsm-tick", node => node.Text($"Tick {_tick}"))
            .Child("Text", "fsm-value", node => node.Text($"Value {_context.Value:0.###} / {_context.Maximum:0.###}"))
            .Child("Text", "fsm-state", node => node.Text($"State {_handle.CurrentState}"))
            .Child("Text", "fsm-step", node => node.Text($"Step {_context.StepMagnitude:0.###}"))
            .Build();

        return BlazorGuiRenderer.Render(summary);
    }

    public string RenderTraceSvg()
    {
        var startIndex = Math.Max(0, Samples.Count - VisibleTicks - 1);
        var visible = Samples.Skip(startIndex).ToArray();
        var firstTick = visible[0].Tick;
        var tickSpan = Math.Max(1, visible[^1].Tick - firstTick);

        static string F(double value) => value.ToString("0.###", CultureInfo.InvariantCulture);
        static double Y(double value) => 47 - (value / Maximum * 34);
        double X(int tick) => 10 + ((tick - firstTick) / (double)tickSpan * 80);

        var points = string.Join(" ", visible.Select(sample =>
            $"{F(X(sample.Tick))},{F(Y(sample.Value))}"));

        var scene = GuiBuilder.Create("svg-scene", "live-fsm-trace")
            .Text("Live FSM_API value trace")
            .Property("viewBox", "0 0 100 58")
            .Property("description", $"Trace generated from {_tick} browser-triggered FSM_API updates. Current value {_context.Value:0.###}; state {_handle.CurrentState}.")
            .Property("aria-label", $"FSM_API live trace, value {_context.Value:0.###}, state {_handle.CurrentState}");

        foreach (var gridValue in new[] { 0d, 5d, 10d })
        {
            var y = F(Y(gridValue));
            scene.Child("svg-line", $"grid-value-{gridValue:0}", line => line
                .Property("x1", "10").Property("y1", y)
                .Property("x2", "90").Property("y2", y)
                .Property("stroke", gridValue == 0 ? "#66e0be" : "#294357")
                .Property("stroke-width", gridValue == 0 ? "0.35" : "0.2")
                .Property("stroke-dasharray", gridValue == 0 ? "none" : "1 1"));
            scene.Child("svg-text", $"label-value-{gridValue:0}", label => label
                .Text(F(gridValue)).Property("x", "3").Property("y", F(Y(gridValue) + 1))
                .Property("font-size", "2.2").Property("fill", "#a9bfd0"));
        }

        scene.Child("svg-line", "vertical-axis", line => line
            .Property("x1", "10").Property("y1", "12")
            .Property("x2", "10").Property("y2", "47")
            .Property("stroke", "#7390a8").Property("stroke-width", "0.3"));
        scene.Child("svg-line", "horizontal-axis", line => line
            .Property("x1", "10").Property("y1", "47")
            .Property("x2", "90").Property("y2", "47")
            .Property("stroke", "#7390a8").Property("stroke-width", "0.3"));
        scene.Child("svg-text", "chart-title", label => label
            .Text("Value per real FSM_API update")
            .Property("x", "12").Property("y", "8")
            .Property("font-size", "2.8").Property("fill", "#eef6ff"));

        if (visible.Length > 1)
        {
            scene.Child("svg-polyline", "fsm-trace", curve => curve
                .Property("points", points)
                .Property("fill", "none")
                .Property("stroke", "#71c9ff")
                .Property("stroke-width", "0.7")
                .Property("stroke-linecap", "round")
                .Property("stroke-linejoin", "round"));
        }

        var currentX = F(X(_tick));
        var currentY = F(Y(_context.Value));
        var markerColor = _handle.CurrentState == "Forward" ? "#66e0be" : "#ffd27d";
        scene.Child("svg-circle", "live-state-marker", marker => marker
            .Property("cx", currentX).Property("cy", currentY)
            .Property("r", "1.5").Property("fill", markerColor));
        scene.Child("svg-text", "state-label", label => label
            .Text($"Tick {_tick} · {_context.Value:0.###} · {_handle.CurrentState}")
            .Property("x", "12").Property("y", "55")
            .Property("font-size", "2.8").Property("fill", markerColor));

        return new SvgGuiRenderer().Render(scene.Build(), width: 960, height: 540);
    }

    public void Dispose()
    {
        _context.IsValid = false;
        FSM_API.Interaction.DestroyInstance(_handle);
    }

    private static void Advance(OscillatorContext context, int direction)
    {
        var proposed = context.Value + direction * context.StepMagnitude;
        context.Value = Math.Clamp(proposed, context.Minimum, context.Maximum);
    }

    private sealed class OscillatorContext : IStateContext
    {
        public OscillatorContext(string name, double minimum, double maximum, double stepMagnitude)
        {
            if (!double.IsFinite(minimum) || !double.IsFinite(maximum) || minimum >= maximum)
                throw new ArgumentOutOfRangeException(nameof(minimum));
            if (!double.IsFinite(stepMagnitude) || stepMagnitude <= 0)
                throw new ArgumentOutOfRangeException(nameof(stepMagnitude));

            Name = name;
            Minimum = minimum;
            Maximum = maximum;
            StepMagnitude = stepMagnitude;
            Value = minimum;
        }

        public string Name { get; set; }
        public bool IsValid { get; set; } = true;
        public double Minimum { get; }
        public double Maximum { get; }
        public double StepMagnitude { get; }
        public double Value { get; set; }
    }
}

public sealed record TickSnapshot(int Tick, double Value, string State);
