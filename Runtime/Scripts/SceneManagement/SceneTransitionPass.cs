namespace FinnSchuuring.Utilities {
    public class SceneTransitionPass {
        public SceneTransitionEvent Event { get; private set; } = SceneTransitionEvent.BeforeTransitioning;
        public SceneTaskDelegate Task { get; private set; } = null;

        public SceneTransitionPass(SceneTransitionEvent sceneEvent, SceneTaskDelegate task) {
            Event = sceneEvent;
            Task = task;
        }
    }
}