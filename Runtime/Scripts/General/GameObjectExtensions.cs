namespace FinnSchuuring.Utilities {
    using UnityEngine;

    public static class GameObjectExtensions {
        public static T FindInDirectChildren<T>(this GameObject gameObject) where T : Component {
            foreach (Transform childTransform in gameObject.transform) {
                if (childTransform.TryGetComponent(out T component)) {
                    return component;
                }
            }
            return null;
        }
    }
}