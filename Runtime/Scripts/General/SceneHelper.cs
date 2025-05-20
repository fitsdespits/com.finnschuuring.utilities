namespace FinnSchuuring.Utilities {
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public static class SceneHelper {
        public static async Task TransitionAsync(int? sceneFromBuildIndex, int sceneToBuildIndex, ObservableVariable<float> progress = null) {
            await LoadSceneAsync(sceneToBuildIndex, progress, 0.25f);
            await UnloadSceneLoadables(sceneFromBuildIndex, progress, 0.25f);
            await UnloadSceneAsync(sceneFromBuildIndex, progress, 0.25f);
            await SetActiveSceneAsync(sceneToBuildIndex);
            await LoadSceneLoadables(sceneToBuildIndex, progress, 0.25f);
        }

        public static async Task LoadSceneAsync(int sceneBuildIndex, ObservableVariable<float> progress, float progressIncrement) {
            await SceneManager.LoadSceneAsync(sceneBuildIndex, LoadSceneMode.Additive);
            progress.Value += progressIncrement;
            await Task.Yield();
        }

        public static async Task UnloadSceneAsync(int? sceneBuildIndex, ObservableVariable<float> progress, float progressIncrement) {
            if (sceneBuildIndex != null) {
                await SceneManager.UnloadSceneAsync(sceneBuildIndex.Value);
            }
            progress.Value += progressIncrement;
            await Task.Yield();
        }

        public static async Task SetActiveSceneAsync(int sceneBuildIndex) {
            Scene scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);
            SceneManager.SetActiveScene(scene);
            await Task.Yield();
        }

        public static async Task LoadSceneLoadables(int sceneBuildIndex, ObservableVariable<float> progress, float progressIncrement) {
            var sceneLoadables = GetSceneLoadables(sceneBuildIndex);
            if (sceneLoadables.Count <= 0) {
                progress.Value += progressIncrement;
                await Task.Yield();
                return;
            }
            float progressIncrementPerSceneLoadable = (float)(progressIncrement / sceneLoadables.Count);
            for (int i = 0; i < sceneLoadables.Count; i++) {
                await sceneLoadables[i].LoadAsync();
                progress.Value += progressIncrementPerSceneLoadable;
                await Task.Yield();
            }
        }

        public static async Task UnloadSceneLoadables(int? sceneBuildIndex, ObservableVariable<float> progress, float progressIncrement) {
            if (sceneBuildIndex != null) {
                var sceneLoadables = GetSceneLoadables(sceneBuildIndex.Value);
                if (sceneLoadables.Count <= 0) {
                    progress.Value += progressIncrement;
                    await Task.Yield();
                    return;
                }
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

        private static List<ISceneLoadable> GetSceneLoadables(int sceneBuildIndex) {
            Scene scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);
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