<!-- The Singularity Workshop moniker -->
<p align="center"><a href="https://github.com/TrentBest"><img src="https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg" alt="The Singularity Workshop — animated moniker" width="720" /></a></p>

# Build an identity-aware data application

> **Mission:** represent things, their identity, and their relationships clearly enough for software to work with them.

**Route:** define what a thing is → give it identity → connect relationships → store and query evidence.

This map moves from domain meaning toward explicit identity and usable evidence.

**Progress map:** Domain concepts → Assign identity → Connect relations → Store + query

```mermaid
flowchart LR
 A["Domain concepts"] --> B
 B["Assign identity"] --> C
 C["Connect relations"] --> D
 D["Store + query"]
 classDef trail fill:#14283b,stroke:#49677d,color:#eef6ff,stroke-width:2px
 classDef evidence fill:#123e43,stroke:#66e0be,color:#f3fffc,stroke-width:3px
 class A,B,C trail
 class D evidence
```



## Your route

1. **Name the domain.** Write down the entities and questions your application needs to answer before choosing a storage shape.
2. **Explore semantic structure.** Read [Ontology](https://github.com/TrentBest/TheSingularityWorkshop.Ontology) for its contracts around defining, versioning, indexing, addressing, and relating ontological information.
3. **Inspect data infrastructure.** Explore [DataWarehouse](https://github.com/TrentBest/TheSingularityWorkshop.DataWarehouse) and its current repository guidance before relying on a storage or query capability.
4. **Follow domain-oriented packages.** Look at [Profiles](https://github.com/TrentBest/TheSingularityWorkshop.Profiles) and [Economy](https://github.com/TrentBest/TheSingularityWorkshop.Economy) when identity or economic concepts match your application.
5. **Keep representation explicit.** Use [FSM_Serialization](https://github.com/TrentBest/TheSingularityWorkshop.FSM_Serialization) when your task crosses the documented FSM ecosystem serialization boundary.

## Checkpoints

- Which IDs are stable, and who owns them?
- Are relationships explicit or inferred?
- Which data is canonical, derived, or captured from a run?

## Project map

[Ontology](https://github.com/TrentBest/TheSingularityWorkshop.Ontology) · [DataWarehouse](https://github.com/TrentBest/TheSingularityWorkshop.DataWarehouse) · [Profiles](https://github.com/TrentBest/TheSingularityWorkshop.Profiles) · [Economy](https://github.com/TrentBest/TheSingularityWorkshop.Economy) · [FSM_Serialization](https://github.com/TrentBest/TheSingularityWorkshop.FSM_Serialization)

**Good fit:** domain applications whose data needs stable identity and meaningful relationships.  
**Plan boundary:** the presence of these projects does not imply one integrated schema or shared persistence runtime; each repository defines its own contract.

**Next:** [Learn data, identity, and simulation technologies](../technologies/DATA_IDENTITY_SIMULATION.md) · [browse all application plans](../INDEX.md#choose-by-what-youre-building)

<!-- THE SINGULARITY WORKSHOP FOOTER -->
<p align="center"><img src="https://raw.githubusercontent.com/TrentBest/FSM_API/master/Documentation/Branding/TheSingularityWorkshop.png" alt="The Singularity Workshop" width="200" /></p>
