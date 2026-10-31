# Technology path: service calls and serialization

**Question:** how do I separate a reusable operation from one request and its response, and represent data at explicit boundaries?

## Learning progression

`Operation contract → per-call binding → transport → response interpretation → representation boundary`

1. Use [FSM_REST](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST) for the REST capability and transport substrate.
2. Follow the [REST operation walkthrough](../../REST_CAPABILITY.md): distinguish operation metadata, concrete values, transport behavior, and application interpretation.
3. Read the repository’s [REST MicroBundle guide](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST/blob/master/docs/MICROBUNDLE.md) to follow its composition view.
4. Explore [FSM_Serialization](https://github.com/TrentBest/TheSingularityWorkshop.FSM_Serialization) when data crosses an encoding or persistence boundary.
5. Check [DataWarehouse](https://github.com/TrentBest/TheSingularityWorkshop.DataWarehouse) separately for its documented data-infrastructure role.

### Check your understanding

Which object describes the operation? Which values belong to this request? Which component sends it? Who decides what the response means?

### Choose a deeper route

- [Describe a REST capability application plan](../applications/AI_ENABLED_APPLICATIONS.md)
- [Debug a result by finding the first divergence](../../DEBUG_A_RESULT.md)

**Scope:** a description is not a network call. Serialization is a representation boundary; it does not guarantee remote storage, transport, or semantic agreement.
