<!-- The Singularity Workshop moniker -->
<p align="center"><a href="https://github.com/TrentBest"><img src="https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg" alt="The Singularity Workshop — animated moniker" width="720" /></a></p>

# Shared Vocabulary

These definitions help a learner move between Workshop projects. When a term belongs to a specific project contract, the linked project documentation is authoritative.

| Term | Plain-language meaning | Boundary to remember |
|---|---|---|
| **FSM (finite state machine)** | A model of behavior as named states and allowed transitions, evaluated against application context. | The machine models behavior; the host still owns its application loop, data, and presentation. See [FSM_API](https://github.com/TrentBest/FSM_API). |
| **State context** | The application-owned data a state machine reads or changes as it operates. | It is the bridge to the app's data, not a separate application or GUI. |
| **MicroBundle** | A domain-defined unit of capability or behavior that can identify itself, declare needs, accept configuration, enter a runtime, and participate in composition. | A MicroBundle is not automatically the same thing as a project, assembly, NuGet package, or deployment unit. See [MicroBundleDomain](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleDomain). |
| **MicroBundle descriptor** | Descriptive identity and declared relationships for a MicroBundle. | The descriptor does not by itself fetch, verify, or execute an artifact. |
| **RuntimeManifest** | A host's request describing the runtime identity and root MicroBundles/versions it wants composed. | It is a request, not an artifact downloader or a bundle catalog. See [Runtime Manifest](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/docs/RUNTIME_MANIFEST.md). |
| **Catalog** | The host-supplied resolution boundary that answers which MicroBundle implementation is available for a requested identity. | It can be in-memory or backed by a store; FSM_COS does not require a particular repository. |
| **Dependency closure** | The set of requested roots plus the prerequisites reachable through their declared dependencies. | The requested root can bring in prerequisites; a manifest need not list every transitive dependency. |
| **FSM_COS** | The composition kernel that resolves and validates requested MicroBundle dependencies, loads prerequisites before dependents, runs bounded arbitration, and returns a RuntimeAssembly. | It does not become the host application or guarantee arbitrary bundles are semantically compatible. |
| **RuntimeAssembly** | The result FSM_COS returns after a successful composition. | It is not itself a running application, visible experience, or proof that host execution succeeded. |
| **Host** | The application or system that supplies composition inputs and decides what to do with the result. | The host owns execution, lifecycle, UI, and presentation responsibilities. |
| **Experience** | An authored unit designed to be manifested through one or more host surfaces. | It is not itself a web page, desktop window, or VR scene. See [Experiences](https://github.com/TrentBest/TheSingularityWorkshop.Experiences). |
| **Capability recipe** | A reusable description of what a REST operation can do: method, path, parameters, rules, and response metadata. | It is distinct from one bound request and the changing response data. See [FSM_REST](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST). |
| **Transport** | The mechanism that sends a request across a communication boundary. | Transport moves data; downstream application code interprets the response. |
| **Artifact** | A packaged or persisted form used to store or move a capability/Experience. | Artifact existence and repository availability do not imply trust, compatibility, or successful runtime composition. |

## A useful mental check

When learning any new Workshop project, ask:

1. **What do I author or request?**
2. **Which project owns the contract?**
3. **Which project resolves, transforms, or transports it?**
4. **Who owns the result after handoff?**
5. **What did the sample actually prove, and what did it leave to the host?**

For longer project-specific definitions, use the authoritative links in the table.

<!-- THE SINGULARITY WORKSHOP FOOTER -->
<p align="center"><img src="https://raw.githubusercontent.com/TrentBest/FSM_API/master/Documentation/Branding/TheSingularityWorkshop.png" alt="The Singularity Workshop" width="200" /></p>
