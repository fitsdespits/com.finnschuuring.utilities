namespace FinnSchuuring.Utilities {
    using Sirenix.OdinInspector;
    using System;
    using UnityEngine;

    [Serializable]
    public abstract class MonoBehaviourAsset : SerializedMonoBehaviour, IAsset {
        [field: SerializeField, HideLabel, PropertySpace(0, 15)] public Guid Guid { get; } = Guid.NewGuid();
        public UnityEngine.Object Object => this;
        public bool IsCloneable => true;
    }
}