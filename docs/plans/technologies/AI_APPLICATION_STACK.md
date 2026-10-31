# Technology path: AI-facing application layers

**Question:** how can an application preserve its own meaning across structured composition and an AI-capable service boundary?

## Learning progression

`Application vocabulary → grammar/relationships → configured service path → command/orchestration boundaries`

Each step names a boundary to verify in its owning project.

**Progress map:** Vocabulary → Structure → Service path → Command boundary

```mermaid
flowchart LR
 A["Vocabulary"] --> B
 B["Structure"] --> C
 C["Service path"] --> D
 D["Command boundary"]
 classDef trail fill:#14283b,stroke:#49677d,color:#eef6ff,stroke-width:2px
 classDef evidence fill:#123e43,stroke:#66e0be,color:#f3fffc,stroke-width:3px
 class A,B,C trail
 class D evidence
```



1. Inspect [ProtocolAi](https://github.com/TrentBest/TheSingularityWorkshop.ProtocolAi) for application-owned identity and deterministic address concepts.
2. Continue to [GrammarAi](https://github.com/TrentBest/TheSingularityWorkshop.GrammarAi) for structures connecting those identities.
3. Study [HeadlessAi](https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi) for its documented input-to-response endpoint workflow.
4. Browse [CommandAi](https://github.com/TrentBest/TheSingularityWorkshop.CommandAi) and [CiAi](https://github.com/TrentBest/TheSingularityWorkshop.CiAi) as separate project areas; establish current usage from each source.
5. Draw the actual boundary between application meaning, request, transport, external service, and deterministic action.

### Check your understanding

For each step, name the owning package and the evidence that it performed that job.

### Choose a deeper route

- [AI-enabled application plan](../applications/AI_ENABLED_APPLICATIONS.md)
- [ProtocolAi](https://github.com/TrentBest/TheSingularityWorkshop.ProtocolAi)
- [HeadlessAi](https://github.com/TrentBest/TheSingularityWorkshop.HeadlessAi)

**Scope:** the stack is a learning map across separate repositories. It does not imply a bundled model, inference engine, provider, or production service.
