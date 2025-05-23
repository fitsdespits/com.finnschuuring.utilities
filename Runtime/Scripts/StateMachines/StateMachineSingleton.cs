﻿namespace FinnSchuuring.Utilities {
    using UnityEngine;

    public abstract class StateMachineSingleton<T> : StateMachine where T : StateMachineSingleton<T> {
        public static T Instance { get {
                if (_instance == null) {
                    _instance = FindFirstObjectByType<T>();
                    if (_instance == null) {
                        Debug.LogError($"No instance of {typeof(T).Name} could be found in the scene.");
                    }
                }
                return _instance;
            }
        }

        private static T _instance;
    }
}