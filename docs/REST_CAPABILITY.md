
<!-- The Singularity Workshop moniker -->
<p align="center"><a href="https://github.com/TrentBest"><img src="https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg" alt="The Singularity Workshop — animated moniker" width="720" /></a></p>

# Describe an Operation, Then Send a Request

**Problem:** an application needs to ask a remote service for a product catalog page, while keeping the reusable description of that operation separate from the values and response of one specific call.

## Follow the request

```mermaid
flowchart LR
    Need["Need: list products"] --> Describe["Describe operation<br/>GET /products"]
    Describe --> Bind["Bind this call<br/>page = 1"]
    Bind --> Build["Build request<br/>example.test/products?page=1"]
    Build --> Send["Send via IRestTransport"]
    Send --> Result["Response data"]
    Result --> App["Your application interprets it"]
    classDef stage fill:#14283b,stroke:#49677d,color:#eef6ff,stroke-width:2px
    classDef highlight fill:#123e43,stroke:#66e0be,color:#f3fffc,stroke-width:4px
    class Need,Describe,Bind,Build,Send stage
    class Result,App highlight
```

**Diagram status:** authored, source-checked workflow based on the [FSM_REST README examples](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST). It describes the code path; it is not a captured HTTP trace. The example hostname is reserved for documentation and is not a live service.

## 1. Describe the reusable capability

The operation description records reusable information: HTTP method, path, operation name, parameters, and response metadata.

```csharp
var operation = new RestOperationDescriptor(
    "GET",
    "/products",
    "listProducts",
    "List products",
    "Returns the current product catalog.",
    [
        new RestParameterDescriptor(
            "page", "query", false, "integer", null, "Page number.")
    ],
    null,
    [
        new RestResponseDescriptor(
            "200", "Product collection.",
            ["application/json"], "array", null)
    ]);

var api = new RestApiDescriptor(
    "Store Catalog",
    "1.0",
    [operation]);
```

At this point, no HTTP request has been sent. The descriptor is a reusable recipe for a capability. It does not contain the current catalog data.

## 2. Bind values for one call

A binding supplies the runtime value for this invocation:

```csharp
var binding = new RestOperationBinding(
    operation,
    new Dictionary<string, string?> { ["page"] = "1" });

var request = RestRequestFactory.Create(
    binding,
    new Uri("https://example.test"));
```

The binding does not change the operation description. It provides the `page=1` value for this request and constructs a request relative to the supplied base URI.

## 3. Send it through a transport

An `IRestTransport` implementation handles communication:

```csharp
using var httpClient = new HttpClient();
IRestTransport transport = new HttpClientRestTransport(httpClient);

RestResponse response = await transport.SendAsync(request);
```

A real application supplies a base address for a service it is authorized and configured to use. The documentation URI above is illustrative; it is not a live API.

## 4. Decide what the response means

After transport returns, application code checks status and interprets the body. FSM_REST separates these concerns: the operation describes what can be requested, the binding describes one call, the transport communicates, and the application decides how to use returned data.

## Change one thing

Change the bound page number from `1` to `2`. Before sending, predict which part of the request should change. The operation description should remain the same; the bound query value and resulting URL should differ.

For the exact project contract and additional examples, read the [FSM_REST README](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST) and [REST capability model](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST/blob/master/docs/REFLECTION.md). For composition with MicroBundles, see [REST MicroBundles](https://github.com/TrentBest/TheSingularityWorkshop.FSM_REST/blob/master/docs/MICROBUNDLE.md).

**Evidence:** source-checked against FSM_REST `master` on 2026-10-30; this learning-center author did not execute the code. The example values are adapted from the repository's documented snippets.

<!-- THE SINGULARITY WORKSHOP FOOTER -->
<p align="center"><img src="https://raw.githubusercontent.com/TrentBest/FSM_API/master/Documentation/Branding/TheSingularityWorkshop.png" alt="The Singularity Workshop" width="200" /></p>
