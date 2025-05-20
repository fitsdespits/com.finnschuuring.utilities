namespace FinnSchuuring.Utilities {
    using System.Collections.Generic;
    using System.Linq;

    public static class ListExtensions {
        public static T First<T>(this List<T> list) {
            return list[0];
        }

        public static T Last<T>(this List<T> list) {
            return list[^1];
        }

        public static T Looped<T>(this List<T> list, int index) {
            int loopedIndex = (index % list.Count + list.Count) % list.Count;
            return list[loopedIndex];
        }

        public static bool IsIndexValid<T>(this List<T> list, int index) {
            return index >= 0 && index <= list.Count - 1;
        }

        public static void Merge<T>(this List<T> list, List<T> other) {
            list.AddRange(other);
        }

        public static void Merge<T>(this List<T> list, T[] other) {
            list.AddRange(other);
        }

        public static List<T2> Convert<T, T2>(this List<T> list) where T : T2 {
            return list.Cast<T2>().ToList();
        }
    }
}