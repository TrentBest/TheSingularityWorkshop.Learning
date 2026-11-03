<!-- The Singularity Workshop moniker -->
<p align="center"><a href="https://github.com/TrentBest"><img src="https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg" alt="The Singularity Workshop — animated moniker" width="720" /></a></p>

# From Run Record to Trajectory Picture

**Problem:** how can someone inspect a computed trajectory and trust that the plotted markers came from the actual run?

This lesson follows one record from a Physics experiment into a vector picture. Physics owns the simulation and the recorded values. The GUI.SVG consumer reads those values, checks them, and draws them; it does not rerun the equations.

## See the result

![X/Z projection of nine recorded projectile states from the Physics experiment](https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Physics/development/docs/experiments/projectile-trajectory.svg)

*This embedded SVG is owned by Physics and rendered from its checked-in run record. It is not the GUI.SVG output. The companion GUI.SVG recipe consumes a pinned Physics record and verifies the rendered line and nine markers against it.*

## Follow the data

```mermaid
flowchart LR
    I["Initial position, velocity, acceleration"] --> P["Physics experiment"]
    P --> J["Versioned JSON run record"]
    J --> V["Check identity, units, frame, samples"]
    V --> G["Map recorded X/Z to GUI.Core scene"]
    G --> S["GUI.SVG renders the scene"]
    S --> C["Check every SVG marker against the record"]
    classDef owner fill:#14283b,stroke:#49677d,color:#eef6ff,stroke-width:2px
    classDef highlight fill:#123e43,stroke:#66e0be,color:#f3fffc,stroke-width:4px
    class I,P,J owner
    class V,G,S,C highlight
```

**Diagram status:** authored teaching model, source-checked against the Physics run-record guide and GUI.SVG consumer recipe; it is not a captured runtime trace.

The highlight marks the handoff where stored simulation facts become a visual artifact. It does not move the simulation into the renderer.

## What the record says

The example identifies the model as `kinematics.constant-acceleration-projectile`, records a world/Z-up coordinate frame, and stores nine states from time 0 through 2 seconds. With the documented default inputs, the final position is `(20, 0, 4.38)` metres.

The plotted X/Z projection shows the horizontal and vertical coordinates. Each point is one recorded state. The line between them is a visual guide; it is not another simulation or a claim about the exact path between samples.

## Try the workflow

The GUI.SVG recipe is [Projectile trajectory from a recorded Physics run](https://github.com/TrentBest/TheSingularityWorkshop.GUI/blob/development/samples/demonstrations/recipes/03-projectile-trajectory.md). It links to the source model, pinned run record, GUI consumer, and CI workflow.

To create a different run, start from the Physics experiment's [documented generator command](https://github.com/TrentBest/TheSingularityWorkshop.Physics/blob/development/docs/experiments/projectile-trajectory.md), changing one input such as timestep or initial velocity. Pass the resulting JSON record to the GUI.SVG consumer using the recipe's command. Before running, predict which markers should change and which record identity or coordinate metadata should remain consistent.

## What the checks protect

The GUI.SVG consumer validates record identity, schema, units, coordinate frame, finite values, sample-time ordering, and non-degenerate ranges. It then checks that the SVG polyline maps to the recorded samples and that all nine source states have corresponding markers.

The linked GUI workflow passed and its artifact was visually reviewed in [run 39453243758](https://github.com/TrentBest/TheSingularityWorkshop.GUI/actions/runs/39453243758). The Physics record used for that recipe is pinned to [commit `fc537e527a1590fbc4c18c39a4e2ad936b7e8ed3`](https://github.com/TrentBest/TheSingularityWorkshop.Physics/blob/fc537e527a1590fbc4c18c39a4e2ad936b7e8ed3/docs/experiments/projectile-trajectory.run.json).

## Keep the ownership clear

- **Physics** computes and records the states.
- **The run record** preserves model identity, units, frame, and sampled values.
- **GUI.Core** builds the semantic scene from the supplied data.
- **GUI.SVG** serializes that scene into vector output.
- **The consumer/checks** establish that the rendered markers correspond to the input record.

For the exact source, reproducible commands, and verification boundaries, use the [GUI.SVG recipe](https://github.com/TrentBest/TheSingularityWorkshop.GUI/blob/development/samples/demonstrations/recipes/03-projectile-trajectory.md) and the [Physics experiment guide](https://github.com/TrentBest/TheSingularityWorkshop.Physics/blob/development/docs/experiments/projectile-trajectory.md). For a state-machine problem that produces a snapshot directly from computed state, continue with [From Problem to Proof](PROBLEM_TO_PROOF.md).

<!-- THE SINGULARITY WORKSHOP FOOTER -->
<p align="center"><img src="https://raw.githubusercontent.com/TrentBest/FSM_API/master/Documentation/Branding/TheSingularityWorkshop.png" alt="The Singularity Workshop" width="200" /></p>
