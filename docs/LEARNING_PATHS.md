<!-- The Singularity Workshop moniker -->
<p align="center"><a href="https://github.com/TrentBest"><img src="https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg" alt="The Singularity Workshop — animated moniker" width="720" /></a></p>

# Learning Paths

Choose a path by the thing you want your software to do. Each route gives you an order for learning and points to the project that owns the detailed guide or contract.

These routes are a learner's map, not a compatibility matrix. A project appearing beside another project does not by itself mean they are integrated or interchangeable.

## Control behavior with state machines

**Goal:** model behavior as states, transitions, and actions that respond to application context.

1. Learn the [FSM_API overview and LightSwitch quickstart](https://github.com/TrentBest/FSM_API).
2. Identify the roles of an FSM definition, an instance, its state context, and the application's update loop.
3. Try the [Unity integration](https://github.com/TrentBest/FSM_API_Unity) if your host is a Unity project.
4. Explore [FSM_UserIO](https://github.com/TrentBest/FSM_UserIO) when your learning goal involves the FSM input/output layer.

**Keep in mind:** FSM_API provides state-machine behavior. Your application still owns its domain data, main loop, rendering, and product behavior.

## Compose capabilities into a runtime

**Goal:** request one capability and have its declared prerequisites composed with it.

1. Begin with [Learn FSM_COS by Following One Request](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/docs/LEARNING_GUIDE.md).
2. Run or inspect the [minimal consumer](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/tree/master/samples/FSM_COS.MinimalConsumer).
3. Learn the consumer boundary in [Consuming MicroBundles](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/docs/CONSUMING_MICROBUNDLES.md).
4. Read [MicroBundleDomain](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleDomain) for the capability contract that FSM_COS consumes.
5. Explore [MicroBundleRepository](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleRepository) and [MicroBundleIngestor](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleIngestor) when your task involves artifact storage or ingestion.
6. Read [FSM_Serialization](https://github.com/TrentBest/TheSingularityWorkshop.FSM_Serialization) if your task needs to represent FSM ecosystem data.

**Try this:** change the requested root in the minimal consumer as described in the FSM_COS learning guide. Observe how the requested root changes the composition result.

**Boundary:** a manifest describes what to compose; it is not itself a download, security verification, or application launch. The catalog and host are supplied by your application.

## Describe and send a REST capability

**Goal:** keep a reusable operation description separate from the values and response of one request.

1. Start with the [FSM_REST repository's capability recipe](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST).
2. Find the operation descriptor example and identify the method, path, parameters, and response metadata it describes.
3. Follow the separate binding step that supplies runtime parameter values.
4. Follow the request through an `IRestTransport` implementation.
5. Read [REST MicroBundles](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST/blob/master/docs/MICROBUNDLE.md) to learn how a REST description can participate in composition.

**Boundary:** an operation description does not fetch the remote response. A transport sends a request; downstream application code decides what the response means.

## Build an AI-capable application path

**Goal:** distinguish semantic structure, protocol handling, and the application’s actual model or service connection.

1. Read [ProtocolAi](https://github.com/TrentBest/TheSingularityWorkshop.ProtocolAi) to understand its address/identity layer.
2. Continue to [GrammarAi](https://github.com/TrentBest/TheSingularityWorkshop.GrammarAi) for structure and connections among application-owned meanings.
3. Explore [HeadlessAi](https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi) for a direct HTTP input-to-response path in a .NET application.
4. Browse [CommandAi](https://github.com/TrentBest/TheSingularityWorkshop.CommandAi) and [CiAi](https://github.com/TrentBest/TheSingularityWorkshop.CiAi) for related work, then use each project's current README to establish its actual status and usage.

**Important distinction:** ProtocolAi's README describes it as infrastructure for AI applications, not an AI model or inference engine. Check each project's own documentation before assuming that a package calls a model, owns network transport, or provides a production-ready integration.

## Author and present an Experience

**Goal:** understand the authored Experience as a product of its own, then learn which host or presentation surface fits your intended user.

1. Start with [Experiences](https://github.com/TrentBest/TheSingularityWorkshop.Experiences), which separates Experience artifacts from the hosts that present them.
2. Read the [AnyApp overview](https://github.com/TrentBest/AnyApp) to understand the desktop-host perspective.
3. Compare [WebApp](https://github.com/TrentBest/TheSingularityWorkshop.WebApp), [WebPage](https://github.com/TrentBest/WebPage), [GUI](https://github.com/TrentBest/TheSingularityWorkshop.GUI), and [MyVR](https://github.com/TrentBest/TheSingularityWorkshop.MyVR) for the presentation or host concerns relevant to your target.
4. Follow each project's documented startup, sample, and publication path before assuming that two hosts share an implemented integration.

**Boundary:** an Experience is not automatically a browser page, desktop window, or VR scene. The Experiences repository describes the authored artifact; the host/presentation projects own how it is manifested.

## Model, render, and interact

**Goal:** learn the difference between meaning, physical behavior, visual output, and user input.

- Start with [Ontology](https://github.com/TrentBest/TheSingularityWorkshop.Ontology) for contracts around defining and relating semantic information.
- Explore [Physics](https://github.com/TrentBest/TheSingularityWorkshop.Physics) for physical modeling.
- Explore [Renderer](https://github.com/TrentBest/TheSingularityWorkshop.Renderer) and [Visualize](https://github.com/TrentBest/TheSingularityWorkshop.Visualize) for rendering and image-producing work.
- Explore [Gestures](https://github.com/TrentBest/TheSingularityWorkshop.Gestures) for gesture input and [FSM_UserIO](https://github.com/TrentBest/FSM_UserIO) for user I/O.
- Use [StoryBuilder](https://github.com/TrentBest/StoryBuilder) to explore the separate work of authoring an Experience/Tool design record and its narrative material.

**Try this:** choose one visible or physical behavior, then write down what is authored data, what is computed, what the user supplies, and what the renderer displays. Use the relevant project documentation to check each boundary.

## Work with profiles and creator systems

**Goal:** explore identity-specific views and the supporting systems around creators and their work.

- Start with [Profiles](https://github.com/TrentBest/TheSingularityWorkshop.Profiles) to see how the same entity can have different representations for different observers.
- Browse [Economy](https://github.com/TrentBest/TheSingularityWorkshop.Economy), [CreatorNetwork](https://github.com/TrentBest/TheSingularityWorkshop.CreatorNetwork), [Foundry](https://github.com/TrentBest/TheSingularityWorkshop.Foundry), and [DataWarehouse](https://github.com/TrentBest/TheSingularityWorkshop.DataWarehouse) for adjacent projects. Use their current source documentation to establish their specific supported tasks.

## Learn the connected repos without guessing

For any path:

1. Start with the task or scenario, not the repository name.
2. Prefer the repository's runnable sample or quickstart where one exists.
3. Record the project and branch/version you followed.
4. Distinguish what the example actually demonstrates from what remains the host's responsibility.
5. Follow the linked deeper docs when you need exact contracts, edge cases, deployment, or security details.

If a path describes an intended combination rather than a verified integration, it says so. The project source and its documentation are authoritative for current behavior.
