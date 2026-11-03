<!-- The Singularity Workshop moniker -->
<p align="center"><a href="https://github.com/TrentBest"><img src="https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg" alt="The Singularity Workshop — animated moniker" width="720" /></a></p>

# Technology path: data, identity, and simulation

**Question:** how do I name a domain, capture a run, and preserve enough structure to explain its output?

## Learning progression

`Domain concepts → identity/relationships → model or run → captured record → query/visualization`

Keep the labels for authored concepts, captured observations, and rendered output distinct.

**Progress map:** Domain concepts → Identity + relations → Model / run → Captured record → Query / view

```mermaid
flowchart LR
 A["Domain concepts"] --> B
 B["Identity + relations"] --> C
 C["Model / run"] --> D
 D["Captured record"] --> E
 E["Query / view"]
 classDef trail fill:#14283b,stroke:#49677d,color:#eef6ff,stroke-width:2px
 classDef evidence fill:#123e43,stroke:#66e0be,color:#f3fffc,stroke-width:3px
 class A,B,C,D trail
 class E evidence
```



1. Start with the problem vocabulary. Use [Ontology](https://github.com/TrentBest/TheSingularityWorkshop.Ontology) to inspect contracts for defining, versioning, indexing, addressing, and relating ontological information.
2. Explore [DataWarehouse](https://github.com/TrentBest/TheSingularityWorkshop.DataWarehouse) for the project's current data infrastructure.
3. Read [Physics](https://github.com/TrentBest/TheSingularityWorkshop.Physics) for its own current model and run record contracts.
4. Follow the Learning Center’s [run-record-to-trajectory walkthrough](../../TRACE_TO_TRAJECTORY.md), which traces captured values into GUI.SVG output.
5. Compare [Profiles](https://github.com/TrentBest/TheSingularityWorkshop.Profiles) and [Economy](https://github.com/TrentBest/TheSingularityWorkshop.Economy) when those domain concepts fit your question.

### Check your understanding

Can you label each value as authored input, live state, captured observation, derived metric, or rendered output?

### Choose a deeper route

- [Identity-aware application plan](../applications/IDENTITY_AND_DATA.md)
- [Simulation-to-visual application plan](../applications/SIMULATION_TO_VISUAL.md)
- [Visual Systems](VISUAL_SYSTEMS.md)

**Scope:** separate projects may use separate schemas and runtime assumptions. Read each owning repository before treating the concepts as a shared data model.

<!-- THE SINGULARITY WORKSHOP FOOTER -->
<p align="center"><img src="https://raw.githubusercontent.com/TrentBest/FSM_API/master/Documentation/Branding/TheSingularityWorkshop.png" alt="The Singularity Workshop" width="200" /></p>
