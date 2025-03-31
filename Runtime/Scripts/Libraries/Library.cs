namespace FinnSchuuring.Utilities {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class Library<T> : LibraryBase where T : Library<T> {
        private protected readonly static Dictionary<Type, T> libraryInstances = new();
        public static T Instance {
            get {
                libraryInstances.TryGetValue(typeof(T), out T libraryInstance);
                if (libraryInstance == null) {
                    libraryInstance = Resources.Load($"Libraries/{typeof(T).Name}") as T;
                    libraryInstances.Add(typeof(T), libraryInstance);
                }
                return libraryInstance;
            }
        }
    }
}