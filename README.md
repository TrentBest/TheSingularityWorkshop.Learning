<!-- The Singularity Workshop moniker -->
<p align="center">
  <a href="https://github.com/TrentBest">
    <img src="https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg" alt="The Singularity Workshop — animated moniker" width="720" />
  </a>
</p>

# ✳️ 00 — Learn The Singularity Workshop

**A practical learning center for building with The Singularity Workshop.** Choose a goal, follow a guided path, try a real example, and use the source repositories when you need deeper technical detail.

This guide follows the [FSM_COS documentation standard](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/DOCUMENTATION_STANDARD.md): edify, not mystify; teach from a recognizable problem toward the implementation; explain boundaries; and show how to verify claims.

## 🟦 01 — What is this?

The Singularity Workshop is a family of software projects for modeling behavior, defining and composing capabilities, and building experiences for different hosts and forms of interaction.

You can enter through one project, but the projects have different jobs. This center explains those jobs through learner goals and workflows. Each project remains authoritative for its own installation, API, status, limitations, and implementation.

## 🟣 02 — Why a learning center?

A long list of repositories asks a newcomer to know the project names before knowing what to build. This guide starts with the learner's question instead:

- *How do I model behavior that changes over time?*
- *How do I assemble capabilities and their prerequisites?*
- *How can I describe and send a REST request?*
- *How do authored Experiences relate to the app that presents them?*
- *Where do meaning, rendering, physical behavior, and input fit?*

The [functional map](docs/FUNCTIONAL_MAP.md) groups projects around these kinds of tasks. The [learning paths](docs/LEARNING_PATHS.md) give a recommended order for exploring them.

## 🩵 03 — How to learn here

1. Pick the outcome closest to what you want to build in [Start Here](docs/START_HERE.md).
2. Follow a [learning path](docs/LEARNING_PATHS.md) and use its first concrete example.
3. Use the [functional map](docs/FUNCTIONAL_MAP.md) to see where each project fits.
4. Open the linked repository for its quickstart, runnable examples, exact contracts, and current constraints.
5. Check the [shared vocabulary](docs/GLOSSARY.md) whenever a term crosses project boundaries.

You do not need to read every project before trying the first example. You do need to follow each project's own setup and status guidance before relying on a package or integration.

## 🟢 04 — See a composition work

FSM_COS has a minimal consumer that requests a report capability while its catalog provides both the report and its data-source prerequisite.

With Git and the .NET 8 SDK installed, run:

```bash
git clone https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS.git
cd TheSingularityWorkshop.FSM_COS
dotnet run --project samples/FSM_COS.MinimalConsumer/FSM_COS.MinimalConsumer.csproj
```

Expected final line:

```text
Assembly order: 2 -> 1
```

That is a source-documented example of dependency ordering. It does not show a running application, graphical interface, remote artifact download, or production catalog. For the line-by-line lesson and a small exercise, follow [Learn FSM_COS by Following One Request](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/docs/LEARNING_GUIDE.md).

**Evidence:** Source-checked against FSM_COS `master` on 2026-10-30; this learning-center author did not independently run the sample. See the upstream guide for its verification notes.

## 🟪 05 — Learning center guides

- [Start Here](docs/START_HERE.md) — choose a goal and get oriented.
- [Learning Paths](docs/LEARNING_PATHS.md) — routes through related projects, ordered by what you want to do.
- [Functional Map](docs/FUNCTIONAL_MAP.md) — project families, their roles, and the core composition boundary.
- [Shared Vocabulary](docs/GLOSSARY.md) — plain-language definitions with boundaries and source links.
- [Editorial Standard](docs/EDITORIAL_STANDARD.md) — how lessons label evidence and stay accurate.

These pages are the learning layer, not duplicate API references. For exact project behavior, follow the links to its own documentation.

---

## 🔗 Project links

- [Workshop public repositories](https://github.com/TrentBest?tab=repositories)
- [FSM_COS documentation standard](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/DOCUMENTATION_STANDARD.md)
