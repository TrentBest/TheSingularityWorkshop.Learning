<!-- The Singularity Workshop moniker -->
<p align="center"><a href="https://github.com/TrentBest"><img src="https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg" alt="The Singularity Workshop — animated moniker" width="720" /></a></p>

# Technology path: GUI, rendering, and visual output

**Question:** how do I turn meaning or computed data into something a person can inspect?

## Learning progression

`Input data → semantic scene → presentation adapter → rendered evidence`

Follow an observable value from its source to the final presentation.

**Progress map:** Input data → Semantic scene → Adapter → Rendered evidence

```mermaid
flowchart LR
 A["Input data"] --> B
 B["Semantic scene"] --> C
 C["Adapter"] --> D
 D["Rendered evidence"]
 classDef trail fill:#14283b,stroke:#49677d,color:#eef6ff,stroke-width:2px
 classDef evidence fill:#123e43,stroke:#66e0be,color:#f3fffc,stroke-width:3px
 class A,B,C trail
 class D evidence
```



1. Explore [GUI](https://github.com/TrentBest/TheSingularityWorkshop.GUI) as the Workshop GUI project; distinguish the scene model from its output adapters.
2. Follow the [GUI.SVG adapter guide](https://github.com/TrentBest/TheSingularityWorkshop.GUI/blob/development/docs/SVG_ADAPTER.md) and the [animated sample scene](https://github.com/TrentBest/TheSingularityWorkshop.GUI/blob/development/samples/FoxSheepdogPlane/Program.cs).
3. Compare the [Renderer](https://github.com/TrentBest/TheSingularityWorkshop.Renderer) project, which describes an independent rendering stack, and [Visualize](https://github.com/TrentBest/TheSingularityWorkshop.Visualize), which describes image-producing scene capability.
4. Bring real evidence: follow [run record to trajectory](../../TRACE_TO_TRAJECTORY.md), and label captured traces separately from authored motion.

### Check your understanding

Can you point from a rendered mark back to its semantic node or source data?

### Choose a deeper route

- [Simulation-to-visual application plan](../applications/SIMULATION_TO_VISUAL.md)
- [GUI.SVG sample recipe](https://github.com/TrentBest/TheSingularityWorkshop.GUI/blob/development/samples/demonstrations/recipes/07-fsm-driven-oscillator.md)
- [Visual Lab](../../VISUAL_LAB.md)

**Scope:** visual output is a manifestation of data or scene semantics; a renderer does not automatically own behavior or a simulation clock.

<!-- THE SINGULARITY WORKSHOP FOOTER -->
<p align="center"><img src="https://raw.githubusercontent.com/TrentBest/FSM_API/master/Documentation/Branding/TheSingularityWorkshop.png" alt="The Singularity Workshop" width="200" /></p>
