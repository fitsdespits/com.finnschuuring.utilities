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

        private bool _isLoaded = false;

        public async Task LoadAsync() {
            await OnLoadAsync();
            if (enableOnLoad) {
                await TryEnableAsync();
            } else {
                await TryDisableAsync();
            }
            _isLoaded = true;
        }

        public async Task UnloadAsync() {
            if (!_isLoaded) {
                return;
            }
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