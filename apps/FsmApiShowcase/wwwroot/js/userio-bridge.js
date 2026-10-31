window.learningUserIo = {
    readTrace: function () {
        try {
            return sessionStorage.getItem("tsw.fsm-userio.trace.v1") || "[]";
        } catch {
            return "[]";
        }
    }
};
