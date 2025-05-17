namespace FinnSchuuring.Utilities {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class Library : ScriptableObjectAsset {

    }

    public abstract class Library<T> : ScriptableObjectAsset where T : Library<T> {
        public static T Instance {
            get {
                _instances.TryGetValue(typeof(T), out T value);
                if (value == null) {
                    value = Resources.Load($"Libraries/{typeof(T).Name}") as T;
                    _instances.Add(typeof(T), value);
                }
                return value;
            }
        }

        private protected readonly static Dictionary<Type, T> _instances = new();

        public override bool IsInstantiatable => false;
    }
}