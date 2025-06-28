namespace FinnSchuuring.Utilities {
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SortedEventBase<T> {
        protected List<KeyValuePair<T, int>> _subscribers = new();

        public void Subscribe(T action, int order = int.MaxValue) {
            _subscribers.Add(new KeyValuePair<T, int>(action, order));
            _subscribers = _subscribers.OrderBy(key => key.Value).ToList();
        }

        public void Unsubscribe(T action) {
            _subscribers.Remove(_subscribers.Find(x => x.Key.Equals(action)));
        }

        public void UnsubscribeAll() {
            _subscribers.Clear();
        }

        protected void InvokeInternal(Action<T> invokeAction) {
            int actionsCount = _subscribers.Count;
            for (int i = 0; i < actionsCount; i++) {
                if (i > _subscribers.Count - 1) break;
                var subscriber = _subscribers[i];
                invokeAction(subscriber.Key);
                if (i <= _subscribers.Count - 1 && !subscriber.Equals(_subscribers[i])) i--;
            }
        }
    }

    public class SortedEvent : SortedEventBase<Action> {
        public void Invoke() {
            InvokeInternal(action => action());
        }
    }

    public class SortedEvent<T> : SortedEventBase<Action<T>> {
        public void Invoke(T t) {
            InvokeInternal(action => action(t));
        }
    }

    public class SortedEvent<T1, T2> : SortedEventBase<Action<T1, T2>> {
        public void Invoke(T1 t1, T2 t2) {
            InvokeInternal(action => action(t1, t2));
        }
    }

    public class SortedEvent<T1, T2, T3> : SortedEventBase<Action<T1, T2, T3>> {
        public void Invoke(T1 t1, T2 t2, T3 t3) {
            InvokeInternal(action => action(t1, t2, t3));
        }
    }

    public class SortedEvent<T1, T2, T3, T4> : SortedEventBase<Action<T1, T2, T3, T4>> {
        public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4) {
            InvokeInternal(action => action(t1, t2, t3, t4));
        }
    }

    public class SortedEvent<T1, T2, T3, T4, T5> : SortedEventBase<Action<T1, T2, T3, T4, T5>> {
        public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) {
            InvokeInternal(action => action(t1, t2, t3, t4, t5));
        }
    }

    public class SortedEvent<T1, T2, T3, T4, T5, T6> : SortedEventBase<Action<T1, T2, T3, T4, T5, T6>> {
        public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6) {
            InvokeInternal(action => action(t1, t2, t3, t4, t5, t6));
        }
    }

    public class SortedEvent<T1, T2, T3, T4, T5, T6, T7> : SortedEventBase<Action<T1, T2, T3, T4, T5, T6, T7>> {
        public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7) {
            InvokeInternal(action => action(t1, t2, t3, t4, t5, t6, t7));
        }
    }

    public class SortedEvent<T1, T2, T3, T4, T5, T6, T7, T8> : SortedEventBase<Action<T1, T2, T3, T4, T5, T6, T7, T8>> {
        public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8) {
            InvokeInternal(action => action(t1, t2, t3, t4, t5, t6, t7, t8));
        }
    }

    public class SortedEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9> : SortedEventBase<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9>> {
        public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9) {
            InvokeInternal(action => action(t1, t2, t3, t4, t5, t6, t7, t8, t9));
        }
    }

    public class SortedEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : SortedEventBase<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>> {
        public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10) {
            InvokeInternal(action => action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10));
        }
    }

    public class SortedEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : SortedEventBase<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>> {
        public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11) {
            InvokeInternal(action => action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11));
        }
    }

    public class SortedEvent<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : SortedEventBase<Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>> {
        public void Invoke(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12) {
            InvokeInternal(action => action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12));
        }
    }
}
