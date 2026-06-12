namespace FinnSchuuring.Utilities {
    public class SceneTransitionPass {
        public SceneTransitionEvent Event { get; private set; } = SceneTransitionEvent.BeforeTransitioning;
        public SceneTransitionTaskDelegate UniTask { get; private set; } = null;

        public SceneTransitionPass(SceneTransitionEvent sceneEvent, SceneTransitionTaskDelegate uniTask) {
            Event = sceneEvent;
            UniTask = uniTask;
        }
    }
}
