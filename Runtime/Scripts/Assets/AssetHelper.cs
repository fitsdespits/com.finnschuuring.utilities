namespace FinnSchuuring.Utilities {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public static class AssetHelper {
        private readonly static List<IAsset> projectAssets = new();
        private readonly static List<IAsset> runtimeAssets = new();

        public static void Initialize() {
            runtimeAssets.Clear();
            CacheProjectAssets();
        }

        private static void CacheProjectAssets() {
            projectAssets.Clear();
            IAsset[] scriptableObjectAssets = Resources.LoadAll<ScriptableObjectAsset>("");
            IAsset[] monoBehaviourAssets = Resources.LoadAll<MonoBehaviourAsset>("");
            foreach (var asset in scriptableObjectAssets) {
                projectAssets.Add(asset);
            }
            foreach (var asset in monoBehaviourAssets) {
                projectAssets.Add(asset);
            }
        }

        public static List<T> GetAllOfTypeInProject<T>() where T : IAsset {
            List<T> assets = new();
            foreach (var projectAsset in projectAssets) {
                if (projectAsset is T asset) {
                    assets.Add(asset);
                }
            }
            return assets;
        }

        public static List<T> GetAllOfTypeInRuntime<T>() where T : IAsset {
            List<T> assets = new();
            foreach (var runtimeAsset in runtimeAssets) {
                if (runtimeAsset is T asset) {
                    assets.Add(asset);
                }
            }
            return assets;
        }

        public static bool TryFindInProject(Guid Guid, out IAsset asset) {
            asset = null;
            foreach (var projectAsset in projectAssets) {
                if (projectAsset.Guid == Guid) {
                    asset = projectAsset;
                    return true;
                }
            }
            return false;
        }

        public static bool TryFindInRuntime(Guid Guid, out IAsset asset) {
            asset = null;
            foreach (var runtimeAsset in runtimeAssets) {
                if (runtimeAsset.Guid == Guid) {
                    asset = runtimeAsset;
                    return true;
                }
            }
            return false;
        }

        public static T CloneScriptableObjectAsset<T>(T originalAsset) where T : ScriptableObjectAsset {
            if (!originalAsset.IsCloneable) {
                Debug.LogError($"Cannot clone {originalAsset.name}, as cloning of type {typeof(T).Name} has not been enabled.");
                return null;
            }
            T cloneAsset = UnityEngine.Object.Instantiate(originalAsset);
            runtimeAssets.Add(cloneAsset);
            return cloneAsset;
        }
    }
}