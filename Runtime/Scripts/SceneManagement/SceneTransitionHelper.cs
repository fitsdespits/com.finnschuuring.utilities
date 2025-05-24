namespace FinnSchuuring.Utilities {
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public delegate Task SceneTransitionTaskDelegate(int? fromSceneBuildIndex, int toSceneBuildIndex, ObservableVariable<float> progress, float progressIncrement);

    public static class SceneTransitionHelper {
        private static readonly List<SceneTransitionPass> _defaultPasses = new() {
            new(SceneTransitionEvent.AfterUnloadSceneLoadables, UnloadSceneLoadablesAsync),
            new(SceneTransitionEvent.AfterLoadScene, LoadSceneAsync),
            new(SceneTransitionEvent.AfterSetActiveScene, SetActiveSceneAsync),
            new(SceneTransitionEvent.AfterUnloadScene, UnloadSceneAsync),
            new(SceneTransitionEvent.AfterLoadSceneLoadables, LoadSceneLoadablesAsync),
        };

        public static async Task TransitionAsync(int? fromSceneBuildIndex, int toSceneBuildIndex, List<SceneTransitionPass> customPasses = null, ObservableVariable<float> progress = null) {
            var passes = MergePasses(_defaultPasses, customPasses);
            float progressIncrement = 1f / passes.Count;
            foreach (var pass in passes) {
                await pass.Task(fromSceneBuildIndex, toSceneBuildIndex, progress, progressIncrement);
            }
            if (progress.Value < 1.0f) {
                progress.Value = 1.0f;
            }
        }

        private static async Task LoadSceneAsync(int? fromSceneBuildIndex, int toSceneBuildIndex, ObservableVariable<float> progress, float progressIncrement) {
            await SceneManager.LoadSceneAsync(toSceneBuildIndex, LoadSceneMode.Additive);
            progress.Value += progressIncrement;
        }

        private static async Task UnloadSceneAsync(int? fromSceneBuildIndex, int toSceneBuildIndex, ObservableVariable<float> progress, float progressIncrement) {
            if (fromSceneBuildIndex != null) {
                await SceneManager.UnloadSceneAsync(fromSceneBuildIndex.Value);
            }
            progress.Value += progressIncrement;
        }

        private static async Task SetActiveSceneAsync(int? fromSceneBuildIndex, int toSceneBuildIndex, ObservableVariable<float> progress, float progressIncrement) {
            Scene scene = SceneManager.GetSceneByBuildIndex(toSceneBuildIndex);
            SceneManager.SetActiveScene(scene);
            progress.Value += progressIncrement;
            await Task.Yield();
        }

        private static async Task LoadSceneLoadablesAsync(int? fromSceneBuildIndex, int toSceneBuildIndex, ObservableVariable<float> progress, float progressIncrement) {
            var sceneLoadables = GetSceneLoadables(toSceneBuildIndex);
            if (sceneLoadables.Count <= 0) {
                progress.Value += progressIncrement;
                return;
            }
            float progressIncrementPerSceneLoadable = (float)(progressIncrement / sceneLoadables.Count);
            for (int i = 0; i < sceneLoadables.Count; i++) {
                await sceneLoadables[i].LoadAsync();
                progress.Value += progressIncrementPerSceneLoadable;
            }
        }

        private static async Task UnloadSceneLoadablesAsync(int? fromSceneBuildIndex, int toSceneBuildIndex, ObservableVariable<float> progress, float progressIncrement) {
            if (fromSceneBuildIndex != null) {
                var sceneLoadables = GetSceneLoadables(fromSceneBuildIndex.Value);
                if (sceneLoadables.Count <= 0) {
                    progress.Value += progressIncrement;
                    return;
                }
                float progressIncrementPerSceneLoadable = (float)(progressIncrement / sceneLoadables.Count);
                for (int i = 0; i < sceneLoadables.Count; i++) {
                    await sceneLoadables[i].UnloadAsync();
                    progress.Value += progressIncrementPerSceneLoadable;
                }
            } else {
                progress.Value += progressIncrement;
            }
        }

        private static List<SceneTransitionPass> MergePasses(List<SceneTransitionPass> defaultPasses, List<SceneTransitionPass> customPasses) {
            if (customPasses == null) {
                return new(defaultPasses);
            }
            var mergedPasses = new List<SceneTransitionPass>(defaultPasses);
            foreach (var customPass in customPasses) {
                if (customPass.Event == SceneTransitionEvent.BeforeTransitioning) {
                    mergedPasses.Insert(0, customPass);
                    continue;
                }
                if (customPass.Event == SceneTransitionEvent.AfterTransitioning) {
                    mergedPasses.Add(customPass);
                    continue;
                }
                int index = mergedPasses.FindIndex(p => p.Event == customPass.Event);
                if (index >= 0) {
                    mergedPasses.Insert(index + 1, customPass);
                }
            }
            return mergedPasses;
        }

        private static List<ISceneLoadable> GetSceneLoadables(int sceneBuildIndex) {
            Scene scene = SceneManager.GetSceneByBuildIndex(sceneBuildIndex);
            List<ISceneLoadable> sceneLoadables = new();
            var monoBehaviours = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
            foreach (var monoBehaviour in monoBehaviours) {
                if (monoBehaviour.gameObject.scene == scene) {
                    if (monoBehaviour is ISceneLoadable sceneLoadable) {
                        sceneLoadables.Add(sceneLoadable);
                    }
                }
            }
            return sceneLoadables;
        }
    }
}