namespace FinnSchuuring.Utilities {

    public static class ArrayExtensions {
        public static T First<T>(this T[] array) {
            return array[0];
        }

        public static T Last<T>(this T[] array) {
            return array[^1];
        }

        public static T Looped<T>(this T[] array, int index) {
            int loopedIndex = (index % array.Length + array.Length) % array.Length;
            return array[loopedIndex];
        }

        public static bool IsIndexValid<T>(this T[] array, int index) {
            return index >= 0 && index <= array.Length - 1;
        }
    }
}