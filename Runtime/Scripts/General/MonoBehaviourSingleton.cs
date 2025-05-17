namespace FinnSchuuring.Utilities {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class MonoBehaviourSingleton : MonoBehaviour {

    }

    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T> {
        public static T Instance {
            get {
                _instances.TryGetValue(typeof(T), out MonoBehaviourSingleton singletonInstance);
                T instance = singletonInstance as T;
                if (instance == null) {
                    _instances.Remove(typeof(T));
                    instance = FindFirstObjectByType<T>();
                    instance.name = typeof(T).Name;
                    _instances.Add(typeof(T), singletonInstance);
                }
                return instance;
            }
        }

        private static readonly Dictionary<Type, MonoBehaviourSingleton> _instances = new();
    }
}