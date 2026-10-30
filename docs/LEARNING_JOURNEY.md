<!-- The Singularity Workshop moniker -->
<p align="center"><a href="https://github.com/TrentBest"><img src="https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg" alt="The Singularity Workshop — animated moniker" width="720" /></a></p>

# Learning Journey: From a Visible Problem to a Working Boundary

The course builds from concrete output toward more system boundaries. Each stop teaches one useful idea and gives you a source to inspect. You can follow the sequence or jump to the lesson that matches your current problem.

🧭 [Explore the visual learning center](https://trentbest.github.io/TheSingularityWorkshop.Learning/site/) to browse the progression and try its bounded-motion teaching model.

```mermaid
flowchart LR
    S1["01 · See a scene"] --> S2["02 · Model changing behavior"]
    S2 --> S3["03 · Compose prerequisites"]
    S3 --> S4["04 · Carry recorded data"]
    S4 --> S5["05 · Describe a service request"]
    S5 --> S6["06 · Choose a presentation host"]
    classDef stage fill:#14283b,stroke:#49677d,color:#eef6ff,stroke-width:2px
    classDef focus fill:#123e43,stroke:#66e0be,color:#f3fffc,stroke-width:4px
    class S1,S2,S3,S4,S6 stage
    class S5 focus
```

**Diagram status:** authored curriculum sequence, not an architectural or execution diagram. The highlighted service-request stage is a source-checked lesson; its sample code has not been executed by this learning-center author.

## 01 — See how an idea becomes a scene

**Question:** what does a grid, an actor, or an interaction look like when represented as a semantic scene?

Start with the animated [GUI.SVG fox-and-sheepdog plane](https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.GUI/development/docs/assets/fox-sheepdog-10x10.svg) and its [scene-building source](https://github.com/TrentBest/TheSingularityWorkshop.GUI/blob/development/samples/FoxSheepdogPlane/Program.cs).

**Notice:** GUI.SVG serializes the scene, while the routes and jumps in this image are authored SVG animation. This is a visual starting point, not a runtime trace from FSM_API.

Next: [Visual Lab](VISUAL_LAB.md).

## 02 — Give a behavior memory and rules

**Question:** how do you keep a changing value within limits and reverse it at both endpoints?

Follow [From Problem to Proof](PROBLEM_TO_PROOF.md). The bounded oscillator uses FSM_API for the state changes and GUI.SVG for a snapshot derived from the final state.

**Evidence:** the upstream recipe identifies its tested trace, endpoint checks, invalid-configuration checks, generated SVG checks, and visually reviewed CI artifact. The image is a snapshot; the renderer does not run the FSM.

Next: [FSM-driven oscillator source recipe](https://github.com/TrentBest/TheSingularityWorkshop.GUI/blob/development/samples/demonstrations/recipes/07-fsm-driven-oscillator.md).

## 03 — Ask for a capability and its prerequisites

**Question:** how can a host request one capability without hard-coding its full dependency order?

Follow the [FSM_COS learning guide](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/docs/LEARNING_GUIDE.md), then inspect the [minimal consumer sample](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/tree/master/samples/FSM_COS.MinimalConsumer).

**Notice:** the host supplies a manifest and catalog; FSM_COS resolves and composes the reachable dependencies, then returns a RuntimeAssembly. This is a distinct sample path, not an integration with the oscillator.

Next: [Consuming MicroBundles](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/docs/CONSUMING_MICROBUNDLES.md).

## 04 — Carry real computed data into a picture

**Question:** how do you know the trajectory markers came from the Physics run rather than being drawn by hand?

Follow [From Run Record to Trajectory Picture](TRACE_TO_TRAJECTORY.md). Physics owns the experiment and JSON record; the GUI.SVG consumer checks the record and verifies the rendered markers against its samples.

**Evidence:** the linked GUI workflow passed and visually reviewed its output in [run 39453243758](https://github.com/TrentBest/TheSingularityWorkshop.GUI/actions/runs/39453243758).

Next: [Physics-backed projectile recipe](https://github.com/TrentBest/TheSingularityWorkshop.GUI/blob/development/samples/demonstrations/recipes/03-projectile-trajectory.md).

## 05 — Describe a service request before sending it

**Question:** how do you separate a reusable operation description from one call's values and response?

Follow [Describe an Operation, Then Send a Request](REST_CAPABILITY.md). The lesson follows an operation descriptor through binding, request construction, transport, and application interpretation.

**Evidence:** the lesson is source-checked against FSM_REST documentation; its example has not been executed here. The documentation URI is illustrative, not a live service.

Next: [FSM_REST](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST) for the exact capability and transport contracts.

## 06 — Choose how an authored Experience is presented

**Question:** which project owns the authored Experience, and which project owns the host or presentation surface?

Start with [Experiences](https://github.com/TrentBest/TheSingularityWorkshop.Experiences), then compare the relevant [host and presentation projects in the Functional Map](FUNCTIONAL_MAP.md#author-and-present-experiences).

**Boundary:** this orientation does not claim that every host, renderer, and Experience is currently wired together. Use each project's implementation-status and startup guides before attempting a specific combination.

## What this course does and does not represent

The sequence is a teaching order, not one deployed application. The state-machine, composition, REST, and Physics-to-SVG stops are distinct examples with separate owners and evidence. A new integration should be treated as a new problem: identify the contracts, write down the data handoffs, and verify each boundary.
