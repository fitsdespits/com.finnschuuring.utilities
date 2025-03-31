namespace FinnSchuuring.Utilities {
    using System;

    public static class ArrayExtensions {
        public static bool IsIndexValid<T>(this T[] array, int index) {
            array.Validate();
            return index >= 0 && index <= array.Length - 1;
        }

        public static void Validate<T>(this T[] array, bool canBeEmpty = false) {
            if (array == null) {
                throw new ArgumentException($"{typeof(T).Name}[] cannot be null.");
            } else
            if (array.Length == 0 && !canBeEmpty) {
                throw new ArgumentException($"{typeof(T).Name}[] cannot be empty.");
            }
        }
    }
}