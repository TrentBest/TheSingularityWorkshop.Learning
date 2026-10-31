# First contact: the torch, the turn, the cavern

**Purpose:** give a visitor a short, wordless first impression of the Workshop, then let them choose what to explore.

## The short journey

1. Pages opens in near-total darkness with one distant torch.
2. The passage begins moving on its own; there is no instruction panel over the scene.
3. A brief turn redirects the view.
4. The passage opens into the cavern, where five routes appear: start from a problem, operate FSM_API, test a boundary, explore the technology atlas, or trace this visit through FSM_UserIO.
5. A sound icon and a skip icon remain available during the arrival. The skip control and Escape key open the cavern immediately.
6. The visitor can replay the short arrival from the learning page.

The cavern geometry is generated at Pages build time by a small C# program referencing the real [TheSingularityWorkshop.Visualize](https://github.com/TrentBest/TheSingularityWorkshop.Visualize) project. It uses Visualize's CPU-side static SVG backend. CSS animates the browser presentation; this is not a live GPU renderer or physical cavern simulation. The low-poly torch flame is presentation geometry.

## FSM_UserIO at the front door

[FSM_UserIO](https://github.com/TrentBest/FSM_UserIO) defines a platform-neutral semantic boundary. Its current core artifact is `SemanticIntent(Name, ProtocolId?)`; it does not own keyboard, pointer, Blazor, GUI, authorization, or execution APIs.

The landing page acts as a browser surface adapter:

1. A visitor action such as skipping, enabling sound, choosing a route, or playing the acoustic cue is mapped to an application-owned semantic name.
2. The static page stores a short same-tab trail as JSON matching the `SemanticIntent` shape. Device details do not cross the semantic boundary.
3. The Blazor WebAssembly lab reads that trail and deserializes it into the real `SemanticIntent` type from FSM_UserIO.
4. In the live FSM_API controls, the browser action is converted into a `SemanticIntent`. Host policy checks the selected agent's state before applying the operation; for example, a Disabled agent rejects further signals.

This keeps the roles separate: the browser adapts a surface event, FSM_UserIO carries meaning, and the host owns policy and execution. The trace demonstrates the current small API rather than implying that UserIO is already a device-input framework.

## The acoustic side vault

The cavern includes an optional instrument based on the Physics repository's [first-order room-reflection experiment](https://github.com/TrentBest/TheSingularityWorkshop.Physics/blob/development/docs/experiments/first-order-room-reflection.md).

That controlled experiment uses a 10 m × 10 m × 10 m room, one source, one listener, and a single reflective wall. It records:

| Path | Distance | Arrival | Estimated level |
|---|---:|---:|---:|
| Direct | 6.3246 m | 18.4389 ms | 63.9794 dB SPL |
| One reflection | 10.1980 m | 29.7319 ms | 53.8091 dB SPL |

During a Pages build, the workflow checks out the Physics development source, runs its C# room-reflection record generator, verifies the generated JSON matches the experiment's committed run record, and records the exact Physics commit used. The page also embeds the experiment's SVG path diagram from the same scenario. The **Hear the path pulse** control schedules two short browser-generated cues from the computed arrivals and relative levels. Source, listener, and reflection positions provide simple left/right placement.

### Evidence boundary

- **Physics model output:** source/listener and reflection-point geometry, path lengths, delays, and per-path level estimates in the generated run record.
- **Browser output:** short pulses scheduled and positioned using the Physics values. The browser produces the cue; Physics produces the path metadata.
- **Not modeled by this preview:** cavern geometry, a full room response, waveform propagation, phase/interference, frequency-dependent absorption, scattering, diffraction, multiple-reflection reverberation, or binaural perception.
- **Arrival ambience:** the optional browser-generated wind/torch texture is an authored atmospheric cue, not a Physics result or output from an Experiences sound engine.
- **Sound capability boundary:** [Experiences/Sound](https://github.com/TrentBest/TheSingularityWorkshop.Experiences/blob/development/experiences/Sound/README.md) is currently a design contract; it does not claim a complete SFX engine or executable sound MicroBundle.

## Why this still belongs at the front door

The first seconds demonstrate composition, depth, light, movement, and a place to explore. The acoustic side vault makes one bounded Physics result tangible. The FSM_UserIO trace makes the visitor's route itself part of the lesson. The remaining paths lead to runnable systems and problem-focused plans.

The arrival stays brief and skippable, with the useful choices immediately available once the cavern opens.
