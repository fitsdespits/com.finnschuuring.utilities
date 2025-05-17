namespace FinnSchuuring.Utilities {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class AssetLibrary<TLibrary, TAsset> : Library<TLibrary>, IAssetLibrary where TLibrary : Library<TLibrary> where TAsset : IAsset {
        private static List<TAsset> _assets = new();

        public void Initialize() {
            _assets.Clear();
            _assets = AssetHelper.GetAllOfType<TAsset>();
            OnInitialize();
        }

        public bool TryFind(Guid guid, out TAsset asset) {
            asset = _assets.Find(x => x.Guid.Equals(guid));
            if (asset == null) {
                Debug.LogError($"Could not find {guid} in {name}.");
                return false;
            }
            return true;
        }

        public List<TAsset> GetAll() {
            return _assets;
        }

        public virtual void OnInitialize() {

        }
    }
}