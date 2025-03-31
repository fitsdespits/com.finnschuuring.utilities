namespace FinnSchuuring.Utilities {
    using Sirenix.OdinInspector;
    using System;
    using UnityEngine;

    [Serializable]
    public abstract class ScriptableObjectAsset : SerializedScriptableObject, IAsset {
        [field: SerializeField, HideLabel, PropertySpace(0, 15)] public Guid Guid { get; } = Guid.NewGuid();
        public UnityEngine.Object Object => this;
        public abstract bool IsCloneable { get; }
    }
}