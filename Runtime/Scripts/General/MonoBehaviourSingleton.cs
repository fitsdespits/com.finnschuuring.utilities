namespace FinnSchuuring.Utilities {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class MonoBehaviourSingleton : MonoBehaviour {

    }

    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T> {
        private static readonly Dictionary<Type, MonoBehaviourSingleton> singletonInstances = new();

        public static T Instance {
            get {
                singletonInstances.TryGetValue(typeof(T), out MonoBehaviourSingleton singletonInstance);
                T instance = singletonInstance as T;
                if (instance == null) {
                    singletonInstances.Remove(typeof(T));
                    instance = FindFirstObjectByType<T>();
                    instance.name = typeof(T).Name;
                    singletonInstances.Add(typeof(T), singletonInstance);
                }
                return instance;
            }
        }
    }
}