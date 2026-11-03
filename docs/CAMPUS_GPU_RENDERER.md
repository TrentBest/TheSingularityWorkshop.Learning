<!-- The Singularity Workshop moniker -->
<p align="center"><a href="https://github.com/TrentBest"><img src="https://raw.githubusercontent.com/TrentBest/TheSingularityWorkshop.Visualize/development/docs/assets/moniker.svg" alt="The Singularity Workshop — animated moniker" width="720" /></a></p>

# Campus WebGPU backend proof

## What runs

The first-person campus uses Visualize's public Visualizer, VisualizerFrame, geometry/material resources, and IVisualizerBackend contracts. CampusWebGpuBackend is the browser host adapter. It translates Visualize geometry and draw commands to a WebGPU canvas surface; the WGSL pipeline performs indexed drawing with depth testing and directional lighting.

The campus catalog is converted into a single indexed mesh. C# uploads each geometry revision once through the backend. Camera and projection uniforms change as the learner walks and looks around. The browser owns and disposes the WebGPU device and buffers; world meaning and campus/course state stay in the Blazor host.

There is no Three.js or external scene engine.

## Scope and evidence

This is the first executable GPU backend proof attached to the Visualize contract, not a production renderer selection or performance claim. Current capability report:

- WebGPU presentation surface: supported when the browser exposes navigator.gpu.
- Indexed triangle rendering and depth buffer: implemented.
- GPU compute, instancing, indirect drawing, timer queries: not implemented in this proof.
- Renderer Event Horizon scheduling and distant representation reuse: not yet connected.
- Device/driver runtime validation: dependent on the learner's browser and hardware; CI compilation does not prove a hardware run.

Pages are served over HTTPS. The host displays an initialization message if WebGPU is unavailable and does not silently substitute the CPU SVG backend. The separate city/campus orientation maps remain generated SVG artifacts.

## Data flow

```text
WebPage campus catalog
        ↓
Blazor builds Visualize geometry/material resources
        ↓
VisualizerFrame (camera, perspective, indexed draw)
        ↓
Visualizer.RenderAsync
        ↓
CampusWebGpuBackend : IVisualizerBackend
        ↓
WebGPU buffers + WGSL pipeline + depth texture
        ↓
campus canvas
```

Next proof points are a GPU-backed instance buffer, a compute-to-render pass, device-loss recovery, and browser/device capture. Add each capability to the reported Visualize backend snapshot only after it is implemented and verified.

<!-- THE SINGULARITY WORKSHOP FOOTER -->
<p align="center"><img src="https://raw.githubusercontent.com/TrentBest/FSM_API/master/Documentation/Branding/TheSingularityWorkshop.png" alt="The Singularity Workshop" width="200" /></p>
