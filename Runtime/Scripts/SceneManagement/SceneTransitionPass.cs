namespace FinnSchuuring.Utilities {
    public class SceneTransitionPass {
        public SceneTransitionEvent Event { get; private set; } = SceneTransitionEvent.BeforeTransitioning;
        public SceneTransitionTaskDelegate Task { get; private set; } = null;

        public SceneTransitionPass(SceneTransitionEvent sceneEvent, SceneTransitionTaskDelegate task) {
            Event = sceneEvent;
            Task = task;
        }
    }
}