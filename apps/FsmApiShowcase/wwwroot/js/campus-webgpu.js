window.learningCampusGpu = (() => {
    let device = null;
    let context = null;
    let canvas = null;
    let pipeline = null;
    let uniformBuffer = null;
    let bindGroup = null;
    let depthTexture = null;
    let depthSize = "";
    let format = null;
    let frameNumber = 0;
    let observer = null;
    const geometryCache = new Map();

    const shader = `
struct FrameUniforms {
    mvp: mat4x4<f32>,
    materialColor: vec4<f32>,
};
@group(0) @binding(0) var<uniform> frame: FrameUniforms;

struct VertexInput {
    @location(0) position: vec3<f32>,
    @location(1) normal: vec3<f32>,
    @location(2) uv: vec2<f32>,
    @location(3) color: vec4<f32>,
};
struct VertexOutput {
    @builtin(position) clipPosition: vec4<f32>,
    @location(0) color: vec4<f32>,
    @location(1) light: f32,
};
@vertex fn vertexMain(input: VertexInput) -> VertexOutput {
    var output: VertexOutput;
    output.clipPosition = frame.mvp * vec4<f32>(input.position, 1.0);
    let normal = normalize(input.normal);
    let sun = normalize(vec3<f32>(-0.38, 0.82, -0.43));
    output.light = 0.28 + 0.72 * max(dot(normal, sun), 0.0);
    output.color = input.color * frame.materialColor;
    return output;
}
@fragment fn fragmentMain(input: VertexOutput) -> @location(0) vec4<f32> {
    return vec4<f32>(input.color.rgb * input.light, input.color.a);
}`;

    async function initialize(target) {
        if (!navigator.gpu) throw new Error("WebGPU is unavailable in this browser. Use a current WebGPU-enabled browser over HTTPS.");
        canvas = target;
        const adapter = await navigator.gpu.requestAdapter({ powerPreference: "high-performance" });
        if (!adapter) throw new Error("WebGPU could not find a compatible graphics adapter.");
        device = await adapter.requestDevice();
        format = navigator.gpu.getPreferredCanvasFormat();
        context = canvas.getContext("webgpu");
        if (!context) throw new Error("The browser could not create a WebGPU canvas context.");
        context.configure({ device, format, alphaMode: "opaque" });

        const module = device.createShaderModule({ label: "Visualize campus shader", code: shader });
        pipeline = device.createRenderPipeline({
            label: "Visualize campus indexed scene",
            layout: "auto",
            vertex: {
                module, entryPoint: "vertexMain",
                buffers: [{
                    arrayStride: 48,
                    attributes: [
                        { shaderLocation: 0, offset: 0, format: "float32x3" },
                        { shaderLocation: 1, offset: 12, format: "float32x3" },
                        { shaderLocation: 2, offset: 24, format: "float32x2" },
                        { shaderLocation: 3, offset: 32, format: "float32x4" }
                    ]
                }]
            },
            fragment: { module, entryPoint: "fragmentMain", targets: [{ format }] },
            primitive: { topology: "triangle-list", cullMode: "none" },
            depthStencil: { format: "depth24plus", depthWriteEnabled: true, depthCompare: "less" }
        });
        uniformBuffer = device.createBuffer({
            label: "Visualize per-draw uniforms",
            size: 256 * 64,
            usage: GPUBufferUsage.UNIFORM | GPUBufferUsage.COPY_DST
        });
        bindGroup = device.createBindGroup({
            layout: pipeline.getBindGroupLayout(0),
            entries: [{ binding: 0, resource: { buffer: uniformBuffer, offset: 0, size: 80 } }]
        });
        observer = new ResizeObserver(resizeCanvas);
        observer.observe(canvas);
        resizeCanvas();
        device.lost.then(info => {
            if (device) console.error("Visualize WebGPU device lost:", info.message || info.reason);
        });
    }

    function resizeCanvas() {
        if (!canvas || !device) return;
        const ratio = Math.min(window.devicePixelRatio || 1, 2);
        const width = Math.max(1, Math.round(canvas.clientWidth * ratio));
        const height = Math.max(1, Math.round(canvas.clientHeight * ratio));
        if (canvas.width !== width || canvas.height !== height) {
            canvas.width = width;
            canvas.height = height;
        }
        const key = width + "x" + height;
        if (key !== depthSize) {
            depthTexture?.destroy();
            depthTexture = device.createTexture({
                label: "Visualize campus depth",
                size: [width, height],
                format: "depth24plus",
                usage: GPUTextureUsage.RENDER_ATTACHMENT
            });
            depthSize = key;
        }
    }

    function uploadGeometry(item) {
        const key = String(item.id);
        const cached = geometryCache.get(key);
        if (cached?.revision === item.revision) return;
        cached?.vertexBuffer.destroy();
        cached?.indexBuffer.destroy();

        const vertexData = new Float32Array(item.vertices);
        const indexData = new Uint32Array(item.indices);
        const vertexBuffer = device.createBuffer({
            label: "Visualize geometry " + key + " vertices",
            size: Math.max(4, vertexData.byteLength),
            usage: GPUBufferUsage.VERTEX | GPUBufferUsage.COPY_DST
        });
        const indexBuffer = device.createBuffer({
            label: "Visualize geometry " + key + " indices",
            size: Math.max(4, indexData.byteLength),
            usage: GPUBufferUsage.INDEX | GPUBufferUsage.COPY_DST
        });
        if (vertexData.length) device.queue.writeBuffer(vertexBuffer, 0, vertexData);
        if (indexData.length) device.queue.writeBuffer(indexBuffer, 0, indexData);
        geometryCache.set(key, {
            revision: item.revision,
            vertexBuffer,
            indexBuffer
        });
    }

    async function initializeCanvas(canvasId) {
        if (device) return;
        const target = document.getElementById(canvasId);
        if (!target) throw new Error("The campus GPU canvas is missing.");
        try {
            await initialize(target);
        } catch (error) {
            dispose();
            throw error;
        }
    }

    async function renderFrame(canvasId, payload) {
        if (!device) {
            const target = document.getElementById(canvasId);
            if (!target) throw new Error("The campus GPU canvas is missing.");
            await initialize(target);
        }
        for (const geometry of payload.geometries) uploadGeometry(geometry);
        resizeCanvas();

        const encoder = device.createCommandEncoder({ label: "Visualize campus frame" });
        const pass = encoder.beginRenderPass({
            colorAttachments: [{
                view: context.getCurrentTexture().createView(),
                clearValue: { r: 0.035, g: 0.055, b: 0.065, a: 1 },
                loadOp: "clear",
                storeOp: "store"
            }],
            depthStencilAttachment: {
                view: depthTexture.createView(),
                depthClearValue: 1,
                depthLoadOp: "clear",
                depthStoreOp: "store"
            }
        });
        pass.setPipeline(pipeline);
        let drawIndex = 0;
        for (const draw of payload.draws) {
            const geometry = geometryCache.get(String(draw.geometry));
            if (!geometry) throw new Error("Visualize frame references a geometry resource that was not uploaded.");
            const uniform = new Float32Array(20);
            uniform.set(draw.mvp, 0);
            uniform.set(draw.color, 16);
            device.queue.writeBuffer(uniformBuffer, drawIndex * 256, uniform);
            pass.setBindGroup(0, bindGroup, [drawIndex * 256]);
            pass.setVertexBuffer(0, geometry.vertexBuffer);
            pass.setIndexBuffer(geometry.indexBuffer, "uint32");
            pass.drawIndexed(draw.indexCount, 1, draw.firstIndex, draw.baseVertex, 0);
            drawIndex++;
        }
        pass.end();
        device.queue.submit([encoder.finish()]);
        frameNumber++;
    }

    function getAspect(canvasId) {
        const target = document.getElementById(canvasId);
        return target && target.clientHeight > 0 ? target.clientWidth / target.clientHeight : 16 / 9;
    }

    function dispose() {
        observer?.disconnect();
        observer = null;
        for (const item of geometryCache.values()) {
            item.vertexBuffer.destroy();
            item.indexBuffer.destroy();
        }
        geometryCache.clear();
        depthTexture?.destroy();
        uniformBuffer?.destroy();
        device?.destroy();
        device = context = canvas = pipeline = uniformBuffer = bindGroup = depthTexture = null;
        depthSize = "";
        frameNumber = 0;
    }

    return { initializeCanvas, renderFrame, getAspect, dispose };
})();
