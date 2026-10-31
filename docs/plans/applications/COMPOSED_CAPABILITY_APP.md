# Build an application from capabilities

> **Mission:** ask for one useful capability and assemble the prerequisites it declares, while keeping application execution with the host.

**Route:** define the contract → request a root → resolve its closure → hand the result back to the host.

This map highlights the host handoff after capability composition.

**Progress map:** Capability contract → Request root → Resolve closure → Host owns execution

```mermaid
flowchart LR
 A["Capability contract"] --> B
 B["Request root"] --> C
 C["Resolve closure"] --> D
 D["Host owns execution"]
 classDef trail fill:#14283b,stroke:#49677d,color:#eef6ff,stroke-width:2px
 classDef evidence fill:#123e43,stroke:#66e0be,color:#f3fffc,stroke-width:3px
 class A,B,C trail
 class D evidence
```



## Your route

1. **Learn the contract.** Read [MicroBundleDomain](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleDomain) to understand the neutral capability boundary.
2. **Follow one request.** Work through [Learn FSM_COS by Following One Request](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/docs/LEARNING_GUIDE.md).
3. **Run the minimal consumer.** Inspect the [FSM_COS minimal consumer](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/tree/master/samples/FSM_COS.MinimalConsumer) to see the host-facing workflow.
4. **Trace artifact handling only when needed.** Explore [MicroBundleRepository](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleRepository) for durable artifact concerns and [MicroBundleIngestor](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleIngestor) when ingestion is part of your task.
5. **Follow the handoff.** Identify the returned RuntimeAssembly and the host code that decides what to run or present.

## Checkpoints

- Which capability did the host request?
- Which dependency edges came from bundle metadata?
- Where does the host resume ownership after composition?

## Project map

[MicroBundleDomain](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleDomain) · [FSM_COS](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS) · [MicroBundleRepository](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleRepository) · [MicroBundleIngestor](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleIngestor) · [Experiences](https://github.com/TrentBest/TheSingularityWorkshop.Experiences)

**Good fit:** systems that need a deterministic composition boundary around reusable capability descriptions.  
**Boundary:** FSM_COS resolves and validates a closure and returns a RuntimeAssembly. The host supplies the catalog and owns security policy, application execution, lifecycle, and presentation. A manifest alone is not a download or launch mechanism.

**Next:** [Learn runtime composition](../technologies/RUNTIME_COMPOSITION.md) · [browse all application plans](../INDEX.md#choose-by-what-youre-building)
