<!-- The Singularity Workshop moniker -->
<p align="center"><a href="https://github.com/TrentBest"><img src="https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg" alt="The Singularity Workshop — animated moniker" width="720" /></a></p>

# Learning Paths: Solve a Problem, Then Learn the Projects

Choose a path by the problem you need to solve. Each path moves from a goal to a model, through a runnable example where one exists, and toward a visible result and checks. Project links are there for the deeper explanation and exact contract, not as a reading checklist.

The recommended first lesson is [From Problem to Proof](PROBLEM_TO_PROOF.md): keep a changing value within bounds, reverse at the endpoints, record its behavior, and visualize the computed result. To browse more worked visual examples, see the [Visual Lab](VISUAL_LAB.md). When a result is wrong, follow [Debug a Result by Finding the First Divergence](DEBUG_A_RESULT.md) to locate the first mismatching handoff.

## Choose a targeted plan

Use the [plan catalog](plans/INDEX.md) to start from either of two questions:

- **What are you building?** Choose an interactive system, browser/desktop experience, AI-enabled application, composed-capability application, simulation visualization, or identity-aware data application.
- **Which technology are you learning?** Choose state and interaction, visual systems, runtime composition, service and serialization, AI application stack, data/identity/simulation, or Experiences and hosts.

Each card opens its own plan page with a practical progression and checkpoints. This page remains the cross-cutting set of problem-solving walkthroughs; the catalog helps you choose a route into them and the owning repositories.

## Control behavior with state machines

**Problem:** behavior has distinct modes and must change predictably as conditions change.

1. Explore the [FSM_API LightSwitch quickstart](https://github.com/TrentBest/FSM_API).
2. Identify the FSM definition, runtime instance, state context, transition conditions, and host update loop.
3. For an example that includes an observable verified trace and SVG output, follow the [FSM-driven bounded oscillator](https://github.com/TrentBest/TheSingularityWorkshop.GUI/blob/development/samples/demonstrations/recipes/07-fsm-driven-oscillator.md).
4. Use [FSM_API_Unity](https://github.com/TrentBest/FSM_API_Unity) when your host is Unity, or explore [FSM_UserIO](https://github.com/TrentBest/FSM_UserIO) for the FSM input/output layer.

**Try this:** change the oscillator step size. Predict the endpoint trace before running it.

## Compose capabilities into a runtime

**Problem:** the host wants one capability, but that capability depends on other pieces that should be resolved and ordered consistently.

1. Follow [Learn FSM_COS by Following One Request](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/docs/LEARNING_GUIDE.md).
2. Run or inspect the [minimal consumer](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/tree/master/samples/FSM_COS.MinimalConsumer).
3. Learn the host boundary in [Consuming MicroBundles](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/docs/CONSUMING_MICROBUNDLES.md).
4. Read [MicroBundleDomain](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleDomain) for the capability contract that FSM_COS consumes.
5. Explore [MicroBundleRepository](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleRepository) and [MicroBundleIngestor](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleIngestor) when your task involves bundle storage or ingestion.
6. Read [FSM_Serialization](https://github.com/TrentBest/TheSingularityWorkshop.FSM_Serialization) if you need to represent FSM ecosystem data.

**Try this:** change the requested root as described by the FSM_COS guide. Record which bundles appear in the result and why.

**Boundary:** a manifest describes what to compose; it is not itself a download, security verification, or application launch. The host supplies the catalog and owns execution and presentation.

## Describe and send a REST capability

**Problem:** describe a reusable remote operation once, bind values for one call, and send a request without treating the current response as part of the reusable description.

1. Follow this center's problem-led [REST operation walkthrough](REST_CAPABILITY.md), then open the [FSM_REST capability recipe](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST).
2. Identify the method, path, parameters, and response metadata in its operation descriptor example.
3. Follow the separate binding step that supplies runtime parameter values.
4. Follow the request through an `IRestTransport` implementation.
5. Read [REST MicroBundles](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST/blob/master/docs/MICROBUNDLE.md) for the composition view.

**Try this:** change the page value in the sample binding and identify which values describe the operation versus this one request.

**Boundary:** an operation description does not fetch a response. A transport sends; application code decides what the response means.

## Build an AI-capable application path

**Problem:** keep application meaning and structure distinct from the model or network service that eventually uses them.

1. Read [ProtocolAi](https://github.com/TrentBest/TheSingularityWorkshop.ProtocolAi) for application-owned meaning and deterministic addresses.
2. Continue to [GrammarAi](https://github.com/TrentBest/TheSingularityWorkshop.GrammarAi) for structures that connect identities.
3. Explore [HeadlessAi](https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi) for a headless input-to-response path to a configured AI-capable HTTP endpoint.
4. Browse [CommandAi](https://github.com/TrentBest/TheSingularityWorkshop.CommandAi) and [CiAi](https://github.com/TrentBest/TheSingularityWorkshop.CiAi); use their current project docs to establish exact scope.

**Try this:** draw the boundary between your application's own input/meaning and the request/response handled by a service.

**Boundary:** ProtocolAi does not contain a model or inference engine. Check each project's current documentation before assuming it owns inference, transport, or a production integration.

## Author and present an Experience

**Problem:** keep the thing being authored distinct from the host and platform used to present it.

1. Start with [Experiences](https://github.com/TrentBest/TheSingularityWorkshop.Experiences), which separates Experience artifacts from host applications.
2. Read the [AnyApp overview](https://github.com/TrentBest/AnyApp) for the desktop-host perspective.
3. Compare [WebApp](https://github.com/TrentBest/TheSingularityWorkshop.WebApp), [WebPage](https://github.com/TrentBest/WebPage), [GUI](https://github.com/TrentBest/TheSingularityWorkshop.GUI), and [MyVR](https://github.com/TrentBest/TheSingularityWorkshop.MyVR) for relevant host and presentation concerns.
4. Follow each project's current startup, sample, and publication path before relying on an integration.

**Try this:** describe the Experience separately from the display surface and host-specific behavior.

**Boundary:** an Experience is not automatically a web page, desktop window, or VR scene.

## Model, render, and interact

**Problem:** a system has meaning, changing behavior, output, and input; these belong to related but distinct parts.

- Start with [Ontology](https://github.com/TrentBest/TheSingularityWorkshop.Ontology) for defining and relating semantic information.
- Explore [Physics](https://github.com/TrentBest/TheSingularityWorkshop.Physics) for physical modeling.
- To follow actual Physics data into a picture, use [From Run Record to Trajectory Picture](TRACE_TO_TRAJECTORY.md): the run record belongs to Physics, while GUI.Core and GUI.SVG make and check the visual representation.
- Explore [GUI.SVG](https://github.com/TrentBest/TheSingularityWorkshop.GUI/tree/development/src/SVG) and its [demonstration gallery](https://github.com/TrentBest/TheSingularityWorkshop.GUI/tree/development/samples/demonstrations) for more vector-output lessons.
- Explore [Visualize](https://github.com/TrentBest/TheSingularityWorkshop.Visualize) for scene/image-producing work. Visualize and GUI.SVG have different roles; see their own repository docs before treating one as a substitute for the other.
- Explore [Gestures](https://github.com/TrentBest/TheSingularityWorkshop.Gestures) for gesture input and [FSM_UserIO](https://github.com/TrentBest/FSM_UserIO) for user I/O.
- Use [StoryBuilder](https://github.com/TrentBest/StoryBuilder) to explore the distinct task of designing an Experience or Tool and composing narrative material.

**Try this:** choose one visible or physical behavior. Write down what is authored, what is computed, what the user supplies, what the renderer shows, and what evidence would prove the image came from the computed result.

## Work with profiles and creator systems

**Problem:** understand how identity-specific views and supporting creator systems relate to the work someone can build.

- Start with [Profiles](https://github.com/TrentBest/TheSingularityWorkshop.Profiles) for observer-specific representations of an entity.
- Browse [Economy](https://github.com/TrentBest/TheSingularityWorkshop.Economy), [CreatorNetwork](https://github.com/TrentBest/TheSingularityWorkshop.CreatorNetwork), [Foundry](https://github.com/TrentBest/TheSingularityWorkshop.Foundry), and [DataWarehouse](https://github.com/TrentBest/TheSingularityWorkshop.DataWarehouse). Use their current documentation to establish each project's exact supported tasks.

## Debug a result by finding the first divergence

**Problem:** the final image, value, or response is wrong, and several projects participate in producing it.

1. Record the smallest repeatable input and expected result.
2. Compare each handoff from inputs through data/state, representation, and output.
3. Fix the first mismatch in the project that owns that boundary.
4. Recheck that handoff and one downstream result.

Follow [the troubleshooting walkthrough](DEBUG_A_RESULT.md) for an oscillator and recorded trajectory, including the evidence each example does and does not establish.

## Follow the same learning rhythm

For any path:

1. Describe the problem and what a successful outcome looks like.
2. Identify the data, contracts, and ownership boundaries involved.
3. Run a small example or label it conceptual if no runnable example exists.
4. Show the result in a way the learner can inspect.
5. Check ordinary and edge cases against evidence.
6. Change one input, predict what changes, then try it.

A picture can make a result easier to understand, but it does not prove which code produced it. Keep source, trace, generated artifact, and runtime observation distinct.
