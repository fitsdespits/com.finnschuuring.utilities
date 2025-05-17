namespace FinnSchuuring.Utilities {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class Library : ScriptableObjectAsset {

    }

    public abstract class Library<T> : ScriptableObjectAsset where T : Library<T> {
        public static T Instance {
            get {
                _instances.TryGetValue(typeof(T), out T libraryInstance);
                if (libraryInstance == null) {
                    libraryInstance = Resources.Load($"Libraries/{typeof(T).Name}") as T;
                    _instances.Add(typeof(T), libraryInstance);
                }
                return libraryInstance;
            }
        }

        private protected readonly static Dictionary<Type, T> _instances = new();

        public override bool IsInstantiatable => false;
    }
}