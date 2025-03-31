namespace FinnSchuuring.Utilities {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SortedEventAsyncBase<T> {
        protected List<KeyValuePair<T, int>> actions = new();

        public void Subscribe(T action, int order = int.MaxValue) {
            actions.Add(new KeyValuePair<T, int>(action, order));
            actions = actions.OrderBy(key => key.Value).ToList();
        }

        public void Unsubscribe(T action) {
            actions.Remove(actions.Find(x => x.Key.Equals(action)));
        }

        public void UnsubscribeAll() {
            actions.Clear();
        }

        protected async Task InvokeInternalAsync(Func<T, Task<bool>> invokeAction) {
            int actionsCount = actions.Count;
            for (int i = 0; i < actionsCount; i++) {
                if (i > actions.Count - 1) break;
                var action = actions[i];
                bool canContinue = await invokeAction(action.Key);
                if (!canContinue) break;
                if (i <= actions.Count - 1 && !action.Equals(actions[i])) i--;
            }
        }
    }

    public class SortedEventAsync : SortedEventAsyncBase<Func<Task<bool>>> {
        public async Task InvokeAsync() {
            await InvokeInternalAsync(async action => await action());
        }
    }

    public class SortedEventAsync<T> : SortedEventAsyncBase<Func<T, Task<bool>>> {
        public async Task InvokeAsync(T t) {
            await InvokeInternalAsync(async action => await action(t));
        }
    }

    public class SortedEventAsync<T1, T2> : SortedEventAsyncBase<Func<T1, T2, Task<bool>>> {
        public async Task InvokeAsync(T1 t1, T2 t2) {
            await InvokeInternalAsync(async action => await action(t1, t2));
        }
    }

    public class SortedEventAsync<T1, T2, T3> : SortedEventAsyncBase<Func<T1, T2, T3, Task<bool>>> {
        public async Task InvokeAsync(T1 t1, T2 t2, T3 t3) {
            await InvokeInternalAsync(async action => await action(t1, t2, t3));
        }
    }

    public class SortedEventAsync<T1, T2, T3, T4> : SortedEventAsyncBase<Func<T1, T2, T3, T4, Task<bool>>> {
        public async Task InvokeAsync(T1 t1, T2 t2, T3 t3, T4 t4) {
            await InvokeInternalAsync(async action => await action(t1, t2, t3, t4));
        }
    }

    public class SortedEventAsync<T1, T2, T3, T4, T5> : SortedEventAsyncBase<Func<T1, T2, T3, T4, T5, Task<bool>>> {
        public async Task InvokeAsync(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) {
            await InvokeInternalAsync(async action => await action(t1, t2, t3, t4, t5));
        }
    }

    public class SortedEventAsync<T1, T2, T3, T4, T5, T6> : SortedEventAsyncBase<Func<T1, T2, T3, T4, T5, T6, Task<bool>>> {
        public async Task InvokeAsync(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6) {
            await InvokeInternalAsync(async action => await action(t1, t2, t3, t4, t5, t6));
        }
    }
}