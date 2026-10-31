# Build an application that integrates with a service

> **Mission:** call a remote operation while keeping the reusable contract, this request, transport, and response handling distinct.

**Route:** choose an operation → bind this call’s values → send through a transport → interpret the response.

**Progress map:** Operation contract → request values → transport → application decision

```mermaid
flowchart LR
 A["Operation contract"] --> B["Bind request values"] --> C["Send through transport"] --> D["Interpret response"]
 classDef trail fill:#14283b,stroke:#49677d,color:#eef6ff,stroke-width:2px
 classDef evidence fill:#123e43,stroke:#66e0be,color:#f3fffc,stroke-width:3px
 class A,B,C trail
 class D evidence
```

The response interpretation is an application decision; the transport does not make it for you.

## Your route

1. Start with this center’s [REST operation walkthrough](../REST_CAPABILITY.md).
2. Open [FSM_REST](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST) and identify how an operation is described.
3. Follow the separate binding step that supplies values for one request.
4. Trace the request through an `IRestTransport` implementation and note what it returns.
5. Use [FSM_Serialization](https://github.com/TrentBest/TheSingularityWorkshop.FSM_Serialization) when your application crosses a documented serialization boundary.
6. Record who owns retries, authentication, error policy, and the meaning of each response in your actual host.

## Checkpoints

- Which fields describe every call to this operation?
- Which values are specific to this request?
- Which code performs transport, and which code makes the application decision?

## Project map

[TheSingularityWorkshop.FSM_REST](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST) · [FSM_Serialization](https://github.com/TrentBest/TheSingularityWorkshop.FSM_Serialization) · [FSM_COS](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS)

**Good fit:** applications that call APIs and need explicit ownership of request construction and response handling.  
**Plan boundary:** a reusable operation descriptor does not itself send a request or decide the response’s meaning. Confirm the current project docs for supported transports and runtime wiring.

**Next:** [Learn service and serialization technologies](../technologies/SERVICE_AND_SERIALIZATION.md) · [browse all application plans](../INDEX.md#choose-by-what-youre-building)
