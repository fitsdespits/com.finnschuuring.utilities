namespace FinnSchuuring.Utilities {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public static class AssetHelper {
        private readonly static List<IAsset> _scriptableObjectAssets = new();
        private readonly static List<IAsset> _monoBehaviourAssets = new();

        public static void CacheScriptableObjectAssets() {
            _scriptableObjectAssets.Clear();
            IAsset[] scriptableObjectAssets = Resources.LoadAll<ScriptableObjectAsset>("");
            foreach (var scriptableObjectAsset in scriptableObjectAssets) {
                _scriptableObjectAssets.Add(scriptableObjectAsset);
            }
        }

        public static void CacheMonoBehaviourAssets() {
            _monoBehaviourAssets.Clear();
            IAsset[] monoBehaviourAssets = Resources.LoadAll<MonoBehaviourAsset>("");
            foreach (var monoBehaviourAsset in monoBehaviourAssets) {
                _monoBehaviourAssets.Add(monoBehaviourAsset);
            }
        }

        public static List<T> GetAllOfType<T>() where T : IAsset {
            List<T> assets = new();
            foreach (var scriptableObjectAsset in _scriptableObjectAssets) {
                if (scriptableObjectAsset is T asset) {
                    assets.Add(asset);
                }
            }
            foreach (var monoBehaviourAsset in _monoBehaviourAssets) {
                if (monoBehaviourAsset is T asset) {
                    assets.Add(asset);
                }
            }
            return assets;
        }

        public static bool TryFind(Guid Guid, out IAsset asset) {
            asset = null;
            foreach (var scriptableObjectAsset in _scriptableObjectAssets) {
                if (scriptableObjectAsset.Guid == Guid) {
                    asset = scriptableObjectAsset;
                    return true;
                }
            }
            foreach (var monoBehaviourAsset in _monoBehaviourAssets) {
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
            _scriptableObjectAssets.Add(instantiatedAsset);
            return instantiatedAsset;
        }

        private static bool TryInstantiateMonoBehaviourAsset<T>(T asset, out T instantiatedAsset) where T : MonoBehaviourAsset {
            instantiatedAsset = UnityEngine.Object.Instantiate(asset);
            _monoBehaviourAssets.Add(instantiatedAsset);
            return true;
        }
    }
}