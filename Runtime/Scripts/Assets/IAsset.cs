namespace FinnSchuuring.Utilities {
    using UnityEngine;

    public interface IAsset : IGuidable {
        public abstract Object Object { get; }
    }
}