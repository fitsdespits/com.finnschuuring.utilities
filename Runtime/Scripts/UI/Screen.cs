namespace FinnSchuuring.Utilities {
    using System.Threading.Tasks;
    using UnityEngine;

    public abstract class Screen<T> : Widget, ISceneLoadable where T : Screen<T> {
        public static T Instance {
            get {
                if (_instance == null) {
                    _instance = FindFirstObjectByType<T>(FindObjectsInactive.Include);
                    if (_instance == null) {
                        Debug.LogError($"No instance of {typeof(T).Name} could be found in the scene.");
                    }
                }
                return _instance;
            }
        }

        private static T _instance = null;

        [SerializeField] private bool enableOnLoad = true;

        public async Task LoadAsync() {
            await OnLoadAsync();
            if (enableOnLoad) {
                await TryEnableAsync();
            } else {
                await TryDisableAsync();
            }
        }

        public async Task UnloadAsync() {
            await OnUnloadAsync();
            await TryDisableAsync();
        }

        public virtual async Task OnLoadAsync() {
            await Task.CompletedTask;
        }

        public virtual async Task OnUnloadAsync() {
            await Task.CompletedTask;
        }
    }
}