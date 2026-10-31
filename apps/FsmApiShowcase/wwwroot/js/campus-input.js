window.learningCampusInput = (() => {
    const traceKey = "tsw.fsm-userio.trace.v1";
    let dotnet = null;
    let keys = new Set();
    let lookDelta = 0;
    let active = false;
    let lastFrame = 0;
    let touchX = null;
    let raf = 0;

    function keyName(event) {
        return event.key.toLowerCase();
    }
    function onKeyDown(event) {
        const key = keyName(event);
        if (["w", "a", "s", "d", "arrowup", "arrowleft", "arrowdown", "arrowright", " "].includes(key))
            event.preventDefault();
        if (!keys.has(key) && (key === "e" || key === "m" || key === "escape")) {
            if (dotnet) dotnet.invokeMethodAsync("OnCampusKey", key === "escape" ? "m" : key).catch(() => {});
        }
        keys.add(key);
    }
    function onKeyUp(event) {
        keys.delete(keyName(event));
    }
    function onMouseMove(event) {
        if (document.pointerLockElement === document.querySelector(".campus-world"))
            lookDelta += event.movementX;
    }
    function onPointerDown(event) { if (event.pointerType === "touch" && !event.target.closest("[data-walk]")) touchX = event.clientX; }
    function onPointerMove(event) {
        if (event.pointerType !== "touch" || touchX === null || event.target.closest("[data-walk]")) return;
        lookDelta += event.clientX - touchX;
        touchX = event.clientX;
    }
    function onPointerUp(event) { if (event.pointerType === "touch") touchX = null; }
    function frame(now) {
        raf = requestAnimationFrame(frame);
        if (!dotnet || !active || (lastFrame !== 0 && now - lastFrame < 16)) return;
        const deltaSeconds = lastFrame === 0 ? 1 / 60 : Math.min((now - lastFrame) / 1000, 0.05);
        lastFrame = now;
        const forward = (keys.has("w") || keys.has("arrowup") ? 1 : 0) - (keys.has("s") || keys.has("arrowdown") ? 1 : 0);
        const strafe = (keys.has("d") || keys.has("arrowright") ? 1 : 0) - (keys.has("a") || keys.has("arrowleft") ? 1 : 0);
        const look = lookDelta;
        lookDelta = 0;
        dotnet.invokeMethodAsync("OnCampusFrame", forward, strafe, look, deltaSeconds).catch(() => {});
    }
    function bindTouch() {
        document.querySelectorAll("[data-walk]").forEach(button => {
            const key = button.dataset.walk;
            button.addEventListener("pointerdown", event => { event.preventDefault(); keys.add(key); button.setPointerCapture(event.pointerId); });
            const release = () => keys.delete(key);
            button.addEventListener("pointerup", release);
            button.addEventListener("pointercancel", release);
            button.addEventListener("lostpointercapture", release);
        });
    }
    return {
        fetchCatalog: async () => {
            const response = await fetch("../data/campus-buildings.json", { cache: "no-store" });
            if (!response.ok) throw new Error("Campus place catalog unavailable (" + response.status + ").");
            return await response.text();
        },
        attach: reference => {
            dotnet = reference;
            active = true;
            window.addEventListener("keydown", onKeyDown, { passive: false });
            window.addEventListener("keyup", onKeyUp);
            window.addEventListener("mousemove", onMouseMove);
            window.addEventListener("pointerdown", onPointerDown, { passive: true });
            window.addEventListener("pointermove", onPointerMove, { passive: true });
            window.addEventListener("pointerup", onPointerUp, { passive: true });
            window.addEventListener("pointercancel", onPointerUp, { passive: true });
            bindTouch();
            cancelAnimationFrame(raf);
            raf = requestAnimationFrame(frame);
        },
        lockPointer: () => {
            const world = document.querySelector(".campus-world");
            if (world && world.requestPointerLock) world.requestPointerLock();
            active = true;
        },
        recordIntent: name => {
            try {
                const rows = JSON.parse(sessionStorage.getItem(traceKey) || "[]");
                const intents = Array.isArray(rows) ? rows : [];
                intents.push({ Name: String(name), ProtocolId: null });
                sessionStorage.setItem(traceKey, JSON.stringify(intents.slice(-50)));
            } catch { }
        },
        openLab: () => location.assign("./")
    };
})();
