<!-- The Singularity Workshop moniker -->
<p align="center">
  <a href="https://github.com/TrentBest">
    <img src="https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/master/docs/assets/moniker.svg" alt="The Singularity Workshop — animated moniker" width="720" />
  </a>
</p>

# ✳️ 00 — Learn The Singularity Workshop

A practical guide to using The Singularity Workshop technology. Find a task that matches what you want to build, follow a learning path, and open the linked project when you need its full documentation, examples, contracts, or source.

This guide follows the [FSM_COS documentation standard](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/DOCUMENTATION_STANDARD.md): explain the human problem, teach through examples, state boundaries and evidence, and link to the authoritative technical reference.

## 🟣 01 — Why this guide exists

The Workshop is spread across projects because different parts solve different problems. A learner should be able to start from an outcome—such as composing capabilities, handling input, or building an experience—and discover the relevant projects in one place.

This repository is a **learning map and set of task-oriented guides**. It does not copy project READMEs or replace their detailed documentation. Each guide should teach how to use one or more capabilities together; linked project repositories remain the source for implementation details and deeper reading.

## 🟦 02 — Choose a learning path

### Compose capabilities and runtime behavior

Learn how to define a reusable capability, declare what it needs, request a composition, and hand the result to an application host.

- [FSM_API](https://github.com/TrentBest/FSM_API) — finite-state-machine and state/context foundations.
- [MicroBundleDomain](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleDomain) — contracts for independently authored capabilities.
- [FSM_COS](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS) — resolve dependencies and assemble a requested runtime.
- [FSM_Serialization](https://github.com/TrentBest/TheSingularityWorkshop.FSM_Serialization) — represent and serialize FSM ecosystem data.
- [FSM_REST](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST) — REST capability and transport for the FSM ecosystem.
- [MicroBundleRepository](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleRepository) — work with a repository of MicroBundles.
- [MicroBundleIngestor](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleIngestor) — ingest MicroBundle artifacts.

### Build AI-driven capabilities

Explore projects for communicating with AI services, working with structured language, and using AI in different kinds of hosts.

- [ProtocolAi](https://github.com/TrentBest/TheSingularityWorkshop.ProtocolAi)
- [GrammarAi](https://github.com/TrentBest/TheSingularityWorkshop.GrammarAi)
- [HeadlessAi](https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi)
- [CommandAi](https://github.com/TrentBest/TheSingularityWorkshop.CommandAi)
- [CiAi](https://github.com/TrentBest/TheSingularityWorkshop.CiAi) and its [CogA](https://github.com/TrentBest/TheSingularityWorkshop.CiAi.CogA), [CogB](https://github.com/TrentBest/TheSingularityWorkshop.CiAi.CogB), [CogC](https://github.com/TrentBest/TheSingularityWorkshop.CiAi.CogC), [CogD](https://github.com/TrentBest/TheSingularityWorkshop.CiAi.CogD), and [CogE](https://github.com/TrentBest/TheSingularityWorkshop.CiAi.CogE) projects.

### Build and deliver user experiences

Follow the path from an authored experience to an application or host that presents it.

- [Experiences](https://github.com/TrentBest/TheSingularityWorkshop.Experiences)
- [WebApp](https://github.com/TrentBest/TheSingularityWorkshop.WebApp)
- [GUI](https://github.com/TrentBest/TheSingularityWorkshop.GUI)
- [AnyApp](https://github.com/TrentBest/AnyApp)
- [WebPage](https://github.com/TrentBest/WebPage)
- [MyVR](https://github.com/TrentBest/TheSingularityWorkshop.MyVR)

### Model, render, and interact with a world

Find projects concerned with representing meaning, creating visual output, modeling physical behavior, or accepting user gestures.

- [Ontology](https://github.com/TrentBest/TheSingularityWorkshop.Ontology)
- [Renderer](https://github.com/TrentBest/TheSingularityWorkshop.Renderer)
- [Visualize](https://github.com/TrentBest/TheSingularityWorkshop.Visualize)
- [Physics](https://github.com/TrentBest/TheSingularityWorkshop.Physics)
- [Gestures](https://github.com/TrentBest/TheSingularityWorkshop.Gestures)
- [FSM_UserIO](https://github.com/TrentBest/FSM_UserIO) — user I/O for FSM-based applications.
- [FSM_API_Unity](https://github.com/TrentBest/FSM_API_Unity) — Unity integration for FSM_API.

### Create, organize, and support projects

Explore tools for authoring, storing, and supporting Workshop work.

- [StoryBuilder](https://github.com/TrentBest/StoryBuilder) — design records and narrative composition for Experiences and Tools.
- [Foundry](https://github.com/TrentBest/TheSingularityWorkshop.Foundry)
- [DataWarehouse](https://github.com/TrentBest/TheSingularityWorkshop.DataWarehouse)
- [Profiles](https://github.com/TrentBest/TheSingularityWorkshop.Profiles)
- [Economy](https://github.com/TrentBest/TheSingularityWorkshop.Economy)
- [CreatorNetwork](https://github.com/TrentBest/TheSingularityWorkshop.CreatorNetwork)

## 🩵 03 — How to use this guide

Start with a goal, not a repository name:

1. Choose the path above closest to what you want to accomplish.
2. Follow its learning guide or runnable tutorial as those materials are added.
3. Use the linked project's quick start to install or run the capability.
4. Return to that project's documentation for exact APIs, configuration, limitations, and implementation details.
5. Treat a claim as verified only when the guide names the project revision and says how it was checked.

The category map is a learner-facing orientation. A project may support more than one workflow; its placement here is a starting point, not an exclusive boundary.

## 🟢 04 — See it in a minute

The shortest complete path into the Workshop today is FSM_COS's minimal consumer: [run the sample](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/samples/FSM_COS.MinimalConsumer/Program.md) to see a host request a capability and FSM_COS assemble its prerequisite first. Then continue with [Consuming MicroBundles](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/docs/CONSUMING_MICROBUNDLES.md) to adapt the pattern.

## 🟪 05 — What to learn next

Task guides in this repository will be added by workflow: each should explain the goal, required pieces, steps, expected result, common alternatives or failures, and what to read next. See the [FSM_COS documentation index](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/DOCUMENTATION_INDEX.md) when you need a deeper reference for FSM_COS.

---

## 🔗 Project links

- [The Singularity Workshop repositories](https://github.com/TrentBest?tab=repositories)
- [FSM_COS documentation standard](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/DOCUMENTATION_STANDARD.md)
