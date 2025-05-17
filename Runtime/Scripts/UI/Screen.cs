namespace FinnSchuuring.Utilities {
    using System;
    using System.Threading.Tasks;
    using UnityEngine;

    public abstract class Screen<T> : MonoBehaviourSingleton<T>, ISceneLoadable where T : Screen<T> {
        [field: SerializeField] public RectTransform WidgetContainer { get; private set; }
        public abstract bool EnableOnAwake { get; }
        public bool IsEnabled => WidgetContainer != null && WidgetContainer.gameObject.activeSelf;

        public async Task LoadAsync() {
            await InitializeAsync();
        }

        public async Task UnloadAsync() {
            await Task.CompletedTask;
        }

        private async Task InitializeAsync() {
            OnInitialize();
            if (EnableOnAwake) {
                await EnableAsync();
            } else {
                await DisableAsync();
            }
        }

        public async Task<bool> TryEnableAsync(Action onDone = null) {
            if (IsEnabled) {
                return false;
            }
            await EnableAsync(onDone);
            return true;
        }

        public async Task<bool> TryDisableAsync(Action onDone = null) {
            if (!IsEnabled) {
                return false;
            }
            await DisableAsync(onDone);
            return true;
        }

        private async Task EnableAsync(Action onDone = null) {
            WidgetContainer.gameObject.SetActive(true);
            await OnEnableAsync();
            onDone?.Invoke();
        }

        private async Task DisableAsync(Action onDone = null) {
            await OnDisableAsync();
            WidgetContainer.gameObject.SetActive(false);
            onDone?.Invoke();
        }

        private void OnDestroy() {
            OnTerminate();
        }

        public virtual void OnInitialize() {

        }

        public virtual void OnTerminate() {

        }

        public virtual Task OnEnableAsync() {
            return Task.CompletedTask;
        }

        public virtual Task OnDisableAsync() {
            return Task.CompletedTask;
        }
    }
}