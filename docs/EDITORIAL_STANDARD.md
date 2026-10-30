<!-- The Singularity Workshop moniker -->
<p align="center"><a href="https://github.com/TrentBest"><img src="https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg" alt="The Singularity Workshop — animated moniker" width="720" /></a></p>

# Learning Center Editorial Standard

This repository teaches people how to use Workshop technology. The [FSM_COS documentation standard](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/DOCUMENTATION_STANDARD.md) is the governing reference; this page applies its learner-facing principles to a multi-project guide.

## Build each guide around a learner's outcome

Every new lesson should make these things clear:

1. **Goal:** what the learner will be able to do.
2. **Starting point:** prerequisites, required project/package, and whether the reader needs to code.
3. **Reason:** the recognizable problem the workflow solves.
4. **Steps:** a small sequence with concrete actions and useful checkpoints.
5. **Result:** what should happen and how the learner can tell.
6. **Boundaries:** what the result proves, what it does not do, and which responsibility belongs to another project or the host.
7. **Next step:** a natural exercise or deeper reference.

Teach a concrete task before abstracting into theory. Use analogies only when you explain where they stop matching reality. Define specialized terms when they first matter and link to the project that owns their contract.

## Protect accuracy

- The linked project source and its current documentation are authoritative for implementation behavior.
- Distinguish current behavior from proposals, plans, and conceptual architecture.
- Label examples **Verified** only when they were actually built/run against a named project version or commit. Use **Source-checked** when the code or documentation was inspected but not run. Label an illustrative but non-runnable snippet **Conceptual**.
- Identify diagrams as source-derived, authored conceptual models, or captured runtime traces. Do not pass a static diagram off as an observed execution.
- Name prerequisites, trust assumptions, limitations, and host responsibilities when they affect a learner's decision.
- Keep one authoritative explanation per topic. Link to a project's exact API and contract docs rather than copying them here.
- Check repository links and branch paths when publishing or updating a lesson.

## Keep the course usable

- Put the next action near the top.
- Prefer a small runnable lesson over a long catalog of types.
- Give every code block a purpose; mark snippets partial when they are not complete programs.
- Make commands copyable and say which directory they run from.
- Give outputs, expected results, and a quick way to diagnose a mismatch when the source docs provide them.
- Use diagrams only when they clarify a workflow or boundary. Add descriptive alt text and explain what the diagram does and does not prove.
- Write for a curious newcomer. Never require the reader to decode the entire repository before reaching a useful first result.
- Review every lesson for broken links, stale commands, contradictory claims, and accidental duplication of authoritative project documentation.

## Evidence labels used here

- **Verified:** run successfully against the named version or commit.
- **Source-checked:** compared with the named source/documentation revision but not executed by this learning-center author.
- **Conceptual:** explanatory model or proposal, not a confirmed implementation or runnable sample.
