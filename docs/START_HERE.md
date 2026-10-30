<!-- The Singularity Workshop moniker -->
<p align="center"><a href="https://github.com/TrentBest"><img src="https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg" alt="The Singularity Workshop — animated moniker" width="720" /></a></p>

# Start Here: Choose a Thing to Build

This learning center is organized around what you want to accomplish. It helps you choose a path, try a first example, and find the right project for the next step.

You do not need to understand every repository before you begin. The project links lead to deeper explanations and exact technical contracts; this guide teaches how the pieces fit into practical work.

## Pick your starting point

- **I want behavior to change predictably as inputs or conditions change.** Start with [FSM_API](https://github.com/TrentBest/FSM_API), then explore [FSM_UserIO](https://github.com/TrentBest/FSM_UserIO) or [FSM_API_Unity](https://github.com/TrentBest/FSM_API_Unity) when your application needs those integrations.
- **I want to assemble reusable capabilities into a runtime.** Start with [Build your first composition](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/docs/LEARNING_GUIDE.md), then continue with [Consuming MicroBundles](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/docs/CONSUMING_MICROBUNDLES.md).
- **I want an application to describe and send a REST request.** Start with [FSM_REST](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST); follow its capability recipe from description to bound request and transport.
- **I want to understand how Experiences can be authored and presented.** Begin with [Experiences](https://github.com/TrentBest/TheSingularityWorkshop.Experiences), then compare host projects such as [WebApp](https://github.com/TrentBest/TheSingularityWorkshop.WebApp), [AnyApp](https://github.com/TrentBest/AnyApp), and [MyVR](https://github.com/TrentBest/TheSingularityWorkshop.MyVR).
- **I want to work with visual, physical, or gesture-based interaction.** Browse the [world and interaction path](LEARNING_PATHS.md#model-render-and-interact).
- **I am not sure what I need yet.** Read the [functional map](FUNCTIONAL_MAP.md), which groups projects by the jobs they help a learner accomplish.

## See a working composition

A useful first demonstration is the FSM_COS minimal consumer. It requests a report MicroBundle; that report depends on a data-source MicroBundle. The host provides both through a small in-memory catalog, and the composition result lists the prerequisite first.

1. Install the **.NET 8 SDK** and Git.
2. Follow the source repository's [step-by-step learning guide](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/docs/LEARNING_GUIDE.md).
3. If you prefer to start from the terminal, use the exact commands in its [README](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS#-04-see-it-work).
4. Compare what the example proves with the [composition boundary](FUNCTIONAL_MAP.md#the-composition-loop).

**Evidence:** Source-checked against FSM_COS `master` on 2026-10-30; this learning page has not independently run the sample. The sample documents the expected final line as `Assembly order: 2 -> 1`. That demonstrates dependency ordering in this example; it does not prove that a real application, GUI, or remote repository has been started.

## A simple mental model

A host says what capability it wants. The composition kernel determines which declared prerequisites are needed and returns an assembled result. The host decides how to use that result.

```text
Capability contracts and implementations
                  │
                  ▼
    Host supplies a catalog + request
                  │
                  ▼
               FSM_COS
                  │
                  ▼
          RuntimeAssembly
                  │
                  ▼
       Host runs/presents the app
```

This is a teaching model for the composition path, source-checked against the [FSM_COS learning guide](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/docs/LEARNING_GUIDE.md). The catalog and application host are supplied by the consumer. FSM_COS composes; it does not itself become the application.

## Continue learning

- [Learning paths by goal](LEARNING_PATHS.md)
- [Functional map of the Workshop](FUNCTIONAL_MAP.md)
- [Shared vocabulary](GLOSSARY.md)
- [How this learning center is written and maintained](EDITORIAL_STANDARD.md)
- [FSM_COS deeper documentation index](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/DOCUMENTATION_INDEX.md)
