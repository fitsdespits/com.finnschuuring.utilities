namespace FinnSchuuring.Utilities {
    using System;

    public class ObservableVariable<T> {
        public T Value {
            get => _value;
            set {
                if (!Equals(_value, value)) {
                    var oldValue = _value;
                    _value = value;
                    OnValueChanged.Invoke(_value, oldValue);
                }
            }
        }
        public SortedEvent<T, T> OnValueChanged { get; private set; } = new();

        private T _value = default;

        public void Observe(Action<T, T> action) {
            OnValueChanged.Subscribe(action);
        }

        public void Unobserve(Action<T, T> action) {
            OnValueChanged.Unsubscribe(action);
        }
    }
}