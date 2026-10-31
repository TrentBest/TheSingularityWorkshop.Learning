# Turn simulation results into a visual explanation

> **Mission:** make a computed result inspectable, so a person can see how the outcome emerged from the run.

**Route:** run a model → capture evidence → map evidence to a scene → render a view.

The rendered view is the end of the evidence path; it does not replace the captured run.

**Progress map:** Run model → Capture evidence → Map to scene → Render view

```mermaid
flowchart LR
 A["Run model"] --> B
 B["Capture evidence"] --> C
 C["Map to scene"] --> D
 D["Render view"]
 classDef trail fill:#14283b,stroke:#49677d,color:#eef6ff,stroke-width:2px
 classDef evidence fill:#123e43,stroke:#66e0be,color:#f3fffc,stroke-width:3px
 class A,B,C trail
 class D evidence
```



## Your route

1. **Choose a concrete question.** Start from an output you want to explain, such as a trajectory, a changing value, or a spatial distribution.
2. **Inspect the data owner.** Read the current [Physics project](https://github.com/TrentBest/TheSingularityWorkshop.Physics) and decide which run fields answer the question.
3. **Preserve evidence.** Follow [Run Record to Trajectory Picture](../TRACE_TO_TRAJECTORY.md) to map observed run data into a visual scene.
4. **Choose a renderer for the job.** Explore [GUI](https://github.com/TrentBest/TheSingularityWorkshop.GUI) and its SVG adapter; compare the experimental [Renderer](https://github.com/TrentBest/TheSingularityWorkshop.Renderer) and [Visualize](https://github.com/TrentBest/TheSingularityWorkshop.Visualize) projects using their own scope notes.
5. **Inspect the picture’s provenance.** Label whether it comes from a captured run, an authored illustration, or a conceptual model.

## Checkpoints

- Can a reader trace a plotted point back to the source record?
- Are units, coordinate transforms, and missing values visible?
- Does the visual preserve distinctions the data actually supports?

## Project map

[Physics](https://github.com/TrentBest/TheSingularityWorkshop.Physics) · [GUI](https://github.com/TrentBest/TheSingularityWorkshop.GUI) · [Renderer](https://github.com/TrentBest/TheSingularityWorkshop.Renderer) · [Visualize](https://github.com/TrentBest/TheSingularityWorkshop.Visualize) · [DataWarehouse](https://github.com/TrentBest/TheSingularityWorkshop.DataWarehouse)

**Good fit:** simulation, telemetry, or scientific output where visual interpretation is part of the workflow.  
**Plan boundary:** a renderer presents data; it does not produce the simulation evidence. Check each project’s maturity and supported inputs before choosing an integration.

**Next:** [Learn visual systems](../technologies/VISUAL_SYSTEMS.md) · [browse all application plans](../INDEX.md#choose-by-what-youre-building)
