# Workshop technology atlas

> This is a learning map, not a substitute for project documentation. Choose a problem, follow the package trail, then open the owning repository for implementation details.

## Start with the job

| What you need to make | Follow this package trail | Learning route |
|---|---|---|
| Behavior with explicit modes | [FSM_API](https://github.com/TrentBest/FSM_API) → [FSM_API_Unity](https://github.com/TrentBest/FSM_API_Unity) where Unity is the host | [State and interaction](plans/technologies/STATE_AND_INTERACTION.md) · [live behavior lab](https://trentbest.github.io/TheSingularityWorkshop.Learning/demo/) |
| Semantic input and capability requests | [FSM_UserIO](https://github.com/TrentBest/FSM_UserIO) → [Gestures](https://github.com/TrentBest/TheSingularityWorkshop.Gestures) → FSM_API | [State and interaction](plans/technologies/STATE_AND_INTERACTION.md) |
| Compose a runtime from capabilities | [MicroBundleDomain](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleDomain) → [FSM_COS](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS) → [MicroBundleRepository](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleRepository) / [MicroBundleIngestor](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleIngestor) → [Foundry](https://github.com/TrentBest/TheSingularityWorkshop.Foundry) | [Runtime composition](plans/technologies/RUNTIME_COMPOSITION.md) |
| Make a scene visible or render output | [GUI](https://github.com/TrentBest/TheSingularityWorkshop.GUI) → [Visualize](https://github.com/TrentBest/TheSingularityWorkshop.Visualize) → [Renderer](https://github.com/TrentBest/TheSingularityWorkshop.Renderer) | [Visual systems](plans/technologies/VISUAL_SYSTEMS.md) |
| Model physical motion or an economy | [Physics](https://github.com/TrentBest/TheSingularityWorkshop.Physics) · [Economy](https://github.com/TrentBest/TheSingularityWorkshop.Economy) | [Data, identity, and simulation](plans/technologies/DATA_IDENTITY_SIMULATION.md) |
| Keep identity, profile, and meaning separate | [Ontology](https://github.com/TrentBest/TheSingularityWorkshop.Ontology) · [Profiles](https://github.com/TrentBest/TheSingularityWorkshop.Profiles) · [DataWarehouse](https://github.com/TrentBest/TheSingularityWorkshop.DataWarehouse) | [Data, identity, and simulation](plans/technologies/DATA_IDENTITY_SIMULATION.md) |
| Give AI a precise interface | [ProtocolAi](https://github.com/TrentBest/TheSingularityWorkshop.ProtocolAi) → [GrammarAi](https://github.com/TrentBest/TheSingularityWorkshop.GrammarAi) → [HeadlessAi](https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi) / [CommandAi](https://github.com/TrentBest/TheSingularityWorkshop.CommandAi) → [CiAi](https://github.com/TrentBest/TheSingularityWorkshop.CiAi) | [AI application stack](plans/technologies/AI_APPLICATION_STACK.md) |
| Carry a capability over a service or byte boundary | [FSM_REST](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST) · [FSM_Serialization](https://github.com/TrentBest/TheSingularityWorkshop.FSM_Serialization) | [Service and serialization](plans/technologies/SERVICE_AND_SERIALIZATION.md) |
| Author an experience and choose where it runs | [Experiences](https://github.com/TrentBest/TheSingularityWorkshop.Experiences) → [WebApp](https://github.com/TrentBest/TheSingularityWorkshop.WebApp) / [WebPage](https://github.com/TrentBest/WebPage) / [AnyApp](https://github.com/TrentBest/AnyApp) / [MyVR](https://github.com/TrentBest/TheSingularityWorkshop.MyVR) | [Experiences and hosts](plans/technologies/EXPERIENCES_AND_HOSTS.md) |

## The package landscape

### Behavior and interaction
- [FSM_API](https://github.com/TrentBest/FSM_API) — explicit state definitions, contexts, handles, and processing.
- [FSM_API_Unity](https://github.com/TrentBest/FSM_API_Unity) — Unity integration for FSM_API.
- [FSM_UserIO](https://github.com/TrentBest/FSM_UserIO) — semantic interaction across forms, capabilities, observations, expression, and computation.
- [Gestures](https://github.com/TrentBest/TheSingularityWorkshop.Gestures) — gesture-oriented interaction.

### Composition and artifact lifecycle
- [FSM_COS](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS) — resolve dependencies and assemble a requested runtime.
- [MicroBundleDomain](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleDomain) — neutral capability contract.
- [MicroBundleRepository](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleRepository) — durable artifact boundary.
- [MicroBundleIngestor](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleIngestor) — ingestion path for MicroBundles.
- [Foundry](https://github.com/TrentBest/TheSingularityWorkshop.Foundry) — Workshop creation and composition surface.
- [Experiences](https://github.com/TrentBest/TheSingularityWorkshop.Experiences) — independently authored experiences and artifacts.

### Visual output and hosts
- [GUI](https://github.com/TrentBest/TheSingularityWorkshop.GUI) — describe an experience separately from the technology displaying it.
- [Visualize](https://github.com/TrentBest/TheSingularityWorkshop.Visualize) — image-producing capability within a larger scene.
- [Renderer](https://github.com/TrentBest/TheSingularityWorkshop.Renderer) — rendering model centered on computational detail and observation.
- [WebApp](https://github.com/TrentBest/TheSingularityWorkshop.WebApp) · [WebPage](https://github.com/TrentBest/WebPage) · [AnyApp](https://github.com/TrentBest/AnyApp) · [MyVR](https://github.com/TrentBest/TheSingularityWorkshop.MyVR) — different manifestation and host paths.

### Data, identity, and simulation
- [Ontology](https://github.com/TrentBest/TheSingularityWorkshop.Ontology) — define and relate meanings without forcing every capability to depend on one ontology.
- [Profiles](https://github.com/TrentBest/TheSingularityWorkshop.Profiles) — represent an entity differently to different observers while keeping identity separate from disclosure and presentation.
- [DataWarehouse](https://github.com/TrentBest/TheSingularityWorkshop.DataWarehouse) — data-oriented capability surface.
- [Physics](https://github.com/TrentBest/TheSingularityWorkshop.Physics) · [Economy](https://github.com/TrentBest/TheSingularityWorkshop.Economy) — model changing systems and their records.

### AI-facing application stack
- [ProtocolAi](https://github.com/TrentBest/TheSingularityWorkshop.ProtocolAi) — provider-neutral deterministic address space for application-owned meaning; its README says “for AI, not AI.”
- [GrammarAi](https://github.com/TrentBest/TheSingularityWorkshop.GrammarAi) — structural layer for self-defining, integer-backed protocols.
- [HeadlessAi](https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi) — direct HTTP input-to-response path for a configured AI-capable endpoint.
- [CommandAi](https://github.com/TrentBest/TheSingularityWorkshop.CommandAi) · [CiAi](https://github.com/TrentBest/TheSingularityWorkshop.CiAi) — command and composable cognition routes.

### Service and representation boundaries
- [FSM_REST](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST) — REST capability and transport substrate.
- [FSM_Serialization](https://github.com/TrentBest/TheSingularityWorkshop.FSM_Serialization) — byte-oriented representation contracts and stream adapters.

## Where the boundaries are real

These are constraints to explore, not claims that a package has failed:

1. **Browser versus host.** GitHub Pages can serve static browser demonstrations. It cannot itself provide a private credential store, persistent database, or arbitrary server process. When a challenge needs those, identify the host boundary and test a local/mock path before introducing a service.
2. **Contract versus authority.** A capability description, semantic request, or MicroBundle does not automatically grant permission to execute it. Test who authorizes it, what configuration it receives, and what happens when a dependency is missing.
3. **Structure versus intelligence.** ProtocolAi and GrammarAi can make meaning and structure explicit; they do not themselves guarantee that a model's answer is true, safe, or useful. Measure parse/validation failure separately from answer quality.
4. **Simulation versus reality.** A deterministic replay can show what the model did with its inputs. It cannot establish that the model captures every real-world force. Vary assumptions and publish the model boundary.
5. **Scene versus rendered image.** Compare the semantic scene, generated representation, and display. A visually plausible result does not prove the underlying data is correct.
6. **Package combination versus product.** A set of individually useful packages is not automatically a finished, secure, deployable application. Test the seams: versions, dependency resolution, configuration, observability, recovery, and host lifecycle.

## Evidence labels

Use these labels in every experiment report:

- **Repository fact:** stated in the owning repository or observed in source.
- **Runnable here:** executed in the published browser lab or another named host.
- **Measured:** includes reproducible inputs, environment, repetitions, and results.
- **Hypothesis:** a boundary to probe; no result is implied.
- **Blocked by host:** requires a runtime, credential, service, device, or persistence layer not present in static Pages.

## Next

- [Open the interactive frontier planner](https://trentbest.github.io/TheSingularityWorkshop.Learning/frontier.html)
- [Choose a challenge and define a measurable failure condition](FRONTIER_CHALLENGES.md)
- [Return to the Learning Center](../README.md)
