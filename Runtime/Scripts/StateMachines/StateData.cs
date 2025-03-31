namespace FinnSchuuring.Utilities {
    using System;

    public class StateData {
        private readonly object[] values;

        public StateData(params object[] values) {
            this.values = values;
        }

        public bool TryGetTypeOfIndex(int index, out Type type) {
            type = null;
            if (values == null || values.Length == 0) return false;
            if (!values.IsIndexValid(index)) return false;
            type = values[index].GetType();
            return true;
        }

        public bool TryGetAtIndex<T>(int index, out T value) {
            value = default;
            if (values == null || values.Length == 0) return false;
            if (!values.IsIndexValid(index)) return false;
            if (values[index] is T castValue) {
                value = castValue;
                return true;
            }
            return false;
        }
    }
}