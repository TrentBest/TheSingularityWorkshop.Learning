window.learningUserIo = {
    readTrace: function () {
        try {
            return sessionStorage.getItem("tsw.fsm-userio.trace.v1") || "[]";
        } catch {
            return "[]";
        }
    },
    recordIntent: function (name) {
        try {
            const stored = JSON.parse(sessionStorage.getItem("tsw.fsm-userio.trace.v1") || "[]");
            const intents = Array.isArray(stored) ? stored : [];
            intents.push({ Name: String(name), ProtocolId: null });
            sessionStorage.setItem("tsw.fsm-userio.trace.v1", JSON.stringify(intents.slice(-50)));
        } catch {
            // A blocked session store must not prevent first-person campus movement.
        }
    }
};
