# Technology path: runtime composition and MicroBundles

**Question:** how does a host request a capability and receive its dependency closure?

## Learning progression

`Capability contract → host catalog + request → resolve/validate closure → RuntimeAssembly → host-owned execution`

1. Read the [MicroBundleDomain contract](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleDomain).
2. Follow the [FSM_COS learning guide](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/docs/LEARNING_GUIDE.md) and its [minimal consumer](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/tree/master/samples/FSM_COS.MinimalConsumer).
3. Learn the host boundary through [Consuming MicroBundles](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/docs/CONSUMING_MICROBUNDLES.md).
4. Branch into [MicroBundleRepository](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleRepository) for artifact persistence or [MicroBundleIngestor](https://github.com/TrentBest/TheSingularityWorkshop.MicroBundleIngestor) for ingestion workflows.
5. Compare [Experiences](https://github.com/TrentBest/TheSingularityWorkshop.Experiences) when the authored artifact is something a host presents.

### Check your understanding

Can you identify which component supplies the catalog, which resolves dependencies, and which owns running the application?

### Choose a deeper route

- [Composed-capability application plan](../applications/COMPOSED_CAPABILITY_APP.md)
- [FSM_COS documentation standard](https://github.com/TrentBest/TheSingularityWorkshop.FSM_COS/blob/master/DOCUMENTATION_STANDARD.md)

**Scope:** FSM_COS composes and returns a runtime assembly. The host owns catalog policy, application execution, lifecycle, security, and presentation.
