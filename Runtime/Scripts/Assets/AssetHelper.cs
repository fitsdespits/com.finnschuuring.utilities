namespace FinnSchuuring.Utilities {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public static class AssetHelper {
        private readonly static List<IAsset> scriptableObjectAssets = new();
        private readonly static List<IAsset> monoBehaviourAssets = new();

        public static void CacheScriptableObjectAssets() {
            AssetHelper.scriptableObjectAssets.Clear();
            IAsset[] scriptableObjectAssets = Resources.LoadAll<ScriptableObjectAsset>("");
            foreach (var scriptableObjectAsset in scriptableObjectAssets) {
                AssetHelper.scriptableObjectAssets.Add(scriptableObjectAsset);
            }
        }

        public static void CacheMonoBehaviourAssets() {
            AssetHelper.monoBehaviourAssets.Clear();
            IAsset[] monoBehaviourAssets = Resources.LoadAll<MonoBehaviourAsset>("");
            foreach (var monoBehaviourAsset in monoBehaviourAssets) {
                AssetHelper.monoBehaviourAssets.Add(monoBehaviourAsset);
            }
        }

        public static List<T> GetAllOfType<T>() where T : IAsset {
            List<T> assets = new();
            foreach (var scriptableObjectAsset in scriptableObjectAssets) {
                if (scriptableObjectAsset is T asset) {
                    assets.Add(asset);
                }
            }
            foreach (var monoBehaviourAsset in monoBehaviourAssets) {
                if (monoBehaviourAsset is T asset) {
                    assets.Add(asset);
                }
            }
            return assets;
        }

        public static bool TryFind(Guid Guid, out IAsset asset) {
            asset = null;
            foreach (var scriptableObjectAsset in scriptableObjectAssets) {
                if (scriptableObjectAsset.Guid == Guid) {
                    asset = scriptableObjectAsset;
                    return true;
                }
            }
            foreach (var monoBehaviourAsset in monoBehaviourAssets) {
                if (monoBehaviourAsset.Guid == Guid) {
                    asset = monoBehaviourAsset;
                    return true;
                }
            }
            return false;
        }

        public static bool TryInstantiate<T>(T asset, out T instantiatedAsset) where T : class, IAsset {
            instantiatedAsset = default;
            if (asset is ScriptableObjectAsset scriptableObjectAsset) {
                if (TryInstantiateScriptableObjectAsset(scriptableObjectAsset, out var result)) {
                    instantiatedAsset = result as T;
                    return true;
                }
                return false;
            }
            if (asset is MonoBehaviourAsset monoBehaviourAsset) {
                if (TryInstantiateMonoBehaviourAsset(monoBehaviourAsset, out var result)) {
                    instantiatedAsset = result as T;
                    return true;
                }
                return false;
            }
            return false;
        }

        private static bool TryInstantiateScriptableObjectAsset<T>(T asset, out T instantiatedAsset) where T : ScriptableObjectAsset {
            instantiatedAsset = null;
            if (!asset.IsInstantiatable) {
                Debug.LogError($"Cannot instantiate {asset.name}. Instantiating of type {typeof(T).Name} has not been enabled.", asset);
                return false;
            }
            instantiatedAsset = UnityEngine.Object.Instantiate(asset);
            scriptableObjectAssets.Add(instantiatedAsset);
            return instantiatedAsset;
        }

        private static bool TryInstantiateMonoBehaviourAsset<T>(T asset, out T instantiatedAsset) where T : MonoBehaviourAsset {
            instantiatedAsset = UnityEngine.Object.Instantiate(asset);
            monoBehaviourAssets.Add(instantiatedAsset);
            return true;
        }
    }
}