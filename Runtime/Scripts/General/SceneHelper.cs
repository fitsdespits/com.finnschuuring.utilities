namespace FinnSchuuring.Utilities {
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public static class SceneHelper {
        public static async Task TransitionAsync(Scene? sceneFrom, Scene sceneTo, ObservableVariable<float> progress = null) {
            List<Task> tasks = new() {
                LoadSceneAsync(sceneTo, progress, 0.25f),
                UnloadSceneLoadables(sceneFrom, progress, 0.25f),
                UnloadSceneAsync(sceneFrom, progress, 0.25f),
                SetActiveSceneAsync(sceneTo),
                LoadSceneLoadables(sceneTo, progress, 0.25f)
            };
            foreach (var task in tasks) {
                await task;
            }
        }

        public static async Task LoadSceneAsync(Scene scene, ObservableVariable<float> progress, float progressIncrement) {
            await SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
            progress.Value += progressIncrement;
            await Task.Yield();
        }

        public static async Task UnloadSceneAsync(Scene? scene, ObservableVariable<float> progress, float progressIncrement) {
            if (scene != null) {
                await SceneManager.UnloadSceneAsync((Scene)scene);
            }
            progress.Value += progressIncrement;
            await Task.Yield();
        }

        public static async Task SetActiveSceneAsync(Scene scene) {
            SceneManager.SetActiveScene(scene);
            await Task.Yield();
        }

        public static async Task LoadSceneLoadables(Scene scene, ObservableVariable<float> progress, float progressIncrement) {
            var sceneLoadables = GetSceneLoadables(scene);
            float progressIncrementPerSceneLoadable = (float)(progressIncrement / sceneLoadables.Count);
            for (int i = 0; i < sceneLoadables.Count; i++) {
                await sceneLoadables[i].LoadAsync();
                progress.Value += progressIncrementPerSceneLoadable;
                await Task.Yield();
            }
        }

        public static async Task UnloadSceneLoadables(Scene? scene, ObservableVariable<float> progress, float progressIncrement) {
            if (scene != null) {
                var sceneLoadables = GetSceneLoadables((Scene)scene);
                float progressIncrementPerSceneLoadable = (float)(progressIncrement / sceneLoadables.Count);
                for (int i = 0; i < sceneLoadables.Count; i++) {
                    await sceneLoadables[i].UnloadAsync();
                    progress.Value += progressIncrementPerSceneLoadable;
                    await Task.Yield();
                }
            } else {
                progress.Value += progressIncrement;
                await Task.Yield();
            }
        }

        private static List<ISceneLoadable> GetSceneLoadables(Scene scene) {
            List<ISceneLoadable> sceneLoadables = new();
            var monoBehaviours = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
            foreach (var monoBehaviour in monoBehaviours) {
                if (monoBehaviour.gameObject.scene != scene) {
                    continue;
                }
                if (monoBehaviour is ISceneLoadable sceneLoadable) {
                    sceneLoadables.Add(sceneLoadable);
                }
            }
            return sceneLoadables;
        }
    }
}