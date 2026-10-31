# Frontier challenges: push the Workshop and measure the seams

This is a set of experiments to run against real Workshop packages. The threshold examples below are starting hypotheses, not measured results. Record the package versions, host, inputs, run count, and raw trace before drawing a conclusion.

## How to run an experiment

1. Pick a user problem and the smallest package path that solves it.
2. State the expected behavior and define what counts as failure before running.
3. Record a baseline on one supported host.
4. Change one stress dimension at a time.
5. Capture the earliest divergence, recovery behavior, and resource cost.
6. Repeat the same input and environment; publish measurements separately from interpretation.
7. Follow the first failure to its owning boundary. Do not hide a package limitation by moving the responsibility without documenting it.

Use the [interactive frontier planner](https://trentbest.github.io/TheSingularityWorkshop.Learning/frontier.html) to choose a scenario, then use the [technology atlas](TECHNOLOGY_ATLAS.md) to follow the package trail.

## Expedition board

| Challenge | Package trail | Stress dimension | Failure signal to measure |
|---|---|---|---|
| A thousand agents, then ten thousand | FSM_API → FSM_COS → Physics → Visualize | Instance count, tick rate, observer/detail budget | Missed updates, unbounded tick time/memory, or a visible scene that no longer matches the recorded state |
| Dependency graph with a missing leaf, diamond, then cycle | MicroBundleDomain → FSM_COS → MicroBundleRepository → MicroBundleIngestor | Missing, duplicate, incompatible, cyclic, or delayed dependency | Ambiguous resolution, partial runtime, wrong artifact selected, or recovery that cannot explain the cause |
| AI output that is malformed, oversized, contradictory, or slow | ProtocolAi → GrammarAi → HeadlessAi → CommandAi/CiAi | Syntax, size, latency, retry storm, semantic mismatch | Invalid structure reaches execution, unbounded work, lost diagnostic, or unsafe fallback |
| Same experience on browser, desktop, Unity, and VR | Experiences → WebPage/WebApp/AnyApp → FSM_API_Unity/MyVR | Lifecycle, input modality, screen/device constraints | Behavior diverges across manifestations or host-specific code leaks into authored experience |
| Physics replay under changed timestep and input order | FSM_API → Physics → DataWarehouse → Visualize | Timestep, ordering, precision, replay size | Replay diverges without an explained model/input difference |
| The right data shown to the wrong observer | Ontology → Profiles → FSM_UserIO → GUI | Audience policy, absent/malformed profile, identity collision | Over-disclosure, wrong identity binding, or output that cannot explain the policy decision |
| Service goes offline mid-operation | FSM_REST → MicroBundleDomain → FSM_COS | Timeout, partial response, retry, backpressure | Duplicate side effects, stuck capability, lost error context, or no bounded recovery |
| Corrupt bytes and old schema meet a new reader | FSM_Serialization → FSM_COS | Truncation, invalid lengths, version skew, partial stream | Crash, silent data mutation, unbounded allocation, or undocumented incompatibility |
| Render budget collapses as detail rises | GUI → Visualize → Renderer | Scene size, observer distance, event horizon/detail, update frequency | Frame/latency budget exceeded or important information disappears without a traceable reduction policy |
| Gestures become noisy and ambiguous | Gestures → FSM_UserIO → FSM_API | Jitter, missed input, simultaneous signals | Flicker between states, duplicate action, or ambiguity with no observable resolution rule |

## Progressive stress levels

1. **Normal use:** one supported example, ordinary inputs.
2. **Boundary values:** zero, maximum, empty, very large, stale, and out-of-order inputs where the package contract allows them.
3. **Disruption:** missing dependency, endpoint timeout, process restart, lost frame, truncated stream, or unavailable device.
4. **Scale:** increase instances, scene detail, data volume, concurrency, and duration independently.
5. **Recovery:** repeat after interruption; inspect idempotency, cleanup, and whether the explanation remains useful.
6. **Host transfer:** run the same scenario in a second host and compare behavior, not screenshots alone.

## What “failure” means here

A failure can be:

- **Correctness:** the result differs from the contract or reproducible expected trace.
- **Containment:** malformed, unauthorized, or unavailable inputs cross a boundary that should stop them.
- **Recovery:** the system cannot return to a known state or explain why it could not.
- **Scale:** resource use or latency exceeds the budget chosen before the run.
- **Portability:** a package path requires an undeclared host assumption or changes behavior across supported hosts.
- **Observability:** the user cannot locate the first bad handoff from the available trace.
- **Security:** a credential, private value, or action authority appears in the wrong boundary.

Choose budgets per use case. Do not present an arbitrary universal number as an official Workshop limit.

## Reporting template

- **Problem and user impact:**
- **Package path and exact versions:**
- **Host, OS, runtime, and configuration:**
- **Expected behavior / contract:**
- **Stress dimension and values:**
- **Predeclared failure condition:**
- **Run count and raw measurements:**
- **First divergence and owner boundary:**
- **Recovery / cleanup result:**
- **What the evidence supports:**
- **What remains unknown:**
- **Repository links and reproduction steps:**

## Evidence discipline

No benchmark result is implied by this plan. The Workshop already has benchmark repositories for FSM_API, FSM_COS, Renderer, and DataWarehouse; use their existing setup where it applies and keep new results reproducible. A static browser lab can demonstrate browser-compatible packages and boundary behavior. Challenges requiring private credentials, persistent storage, a server, Unity, or VR must name that host requirement explicitly.
