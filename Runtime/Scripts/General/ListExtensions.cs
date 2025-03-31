namespace FinnSchuuring.Utilities {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public static class ListExtensions {
        public static T First<T>(this List<T> list) {
            list.Validate();
            return list[0];
        }

        public static T Last<T>(this List<T> list) {
            list.Validate();
            return list[^1];
        }

        public static T Looped<T>(this List<T> list, int index) {
            list.Validate();
            int loopingIndex = (index % list.Count + list.Count) % list.Count;
            return list[loopingIndex];
        }

        public static bool IsIndexValid<T>(this List<T> list, int index) {
            list.Validate();
            return index >= 0 && index <= list.Count - 1;
        }

        public static void Merge<T>(this List<T> list, List<T> other) {
            list.Validate();
            other.Validate();
            list.AddRange(other);
        }

        public static void Merge<T>(this List<T> list, T[] other) {
            list.Validate();
            other.Validate(true);
            list.AddRange(other);
        }

        public static List<T2> Convert<T, T2>(this List<T> list) where T : T2 {
            list.Validate();
            return list.Cast<T2>().ToList();
        }

        public static void Validate<T>(this List<T> list) {
            if (list == null) {
                throw new ArgumentException($"List<{typeof(T).Name}> cannot be null.");
            }
        }
    }
}