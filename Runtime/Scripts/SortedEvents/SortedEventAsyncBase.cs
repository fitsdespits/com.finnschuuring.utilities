namespace FinnSchuuring.Utilities {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cysharp.Threading.Tasks;

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

        protected async UniTask InvokeInternalAsync(Func<T, UniTask<bool>> invokeAction) {
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

    public class SortedEventAsync : SortedEventAsyncBase<Func<UniTask<bool>>> {
        public async UniTask InvokeAsync() {
            await InvokeInternalAsync(async action => await action());
        }
    }

    public class SortedEventAsync<T> : SortedEventAsyncBase<Func<T, UniTask<bool>>> {
        public async UniTask InvokeAsync(T t) {
            await InvokeInternalAsync(async action => await action(t));
        }
    }

    public class SortedEventAsync<T1, T2> : SortedEventAsyncBase<Func<T1, T2, UniTask<bool>>> {
        public async UniTask InvokeAsync(T1 t1, T2 t2) {
            await InvokeInternalAsync(async action => await action(t1, t2));
        }
    }

    public class SortedEventAsync<T1, T2, T3> : SortedEventAsyncBase<Func<T1, T2, T3, UniTask<bool>>> {
        public async UniTask InvokeAsync(T1 t1, T2 t2, T3 t3) {
            await InvokeInternalAsync(async action => await action(t1, t2, t3));
        }
    }

    public class SortedEventAsync<T1, T2, T3, T4> : SortedEventAsyncBase<Func<T1, T2, T3, T4, UniTask<bool>>> {
        public async UniTask InvokeAsync(T1 t1, T2 t2, T3 t3, T4 t4) {
            await InvokeInternalAsync(async action => await action(t1, t2, t3, t4));
        }
    }

    public class SortedEventAsync<T1, T2, T3, T4, T5> : SortedEventAsyncBase<Func<T1, T2, T3, T4, T5, UniTask<bool>>> {
        public async UniTask InvokeAsync(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5) {
            await InvokeInternalAsync(async action => await action(t1, t2, t3, t4, t5));
        }
    }

    public class SortedEventAsync<T1, T2, T3, T4, T5, T6> : SortedEventAsyncBase<Func<T1, T2, T3, T4, T5, T6, UniTask<bool>>> {
        public async UniTask InvokeAsync(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6) {
            await InvokeInternalAsync(async action => await action(t1, t2, t3, t4, t5, t6));
        }
    }

    public class SortedEventAsync<T1, T2, T3, T4, T5, T6, T7> : SortedEventAsyncBase<Func<T1, T2, T3, T4, T5, T6, T7, UniTask<bool>>> {
        public async UniTask InvokeAsync(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7) {
            await InvokeInternalAsync(async action => await action(t1, t2, t3, t4, t5, t6, t7));
        }
    }

    public class SortedEventAsync<T1, T2, T3, T4, T5, T6, T7, T8> : SortedEventAsyncBase<Func<T1, T2, T3, T4, T5, T6, T7, T8, UniTask<bool>>> {
        public async UniTask InvokeAsync(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8) {
            await InvokeInternalAsync(async action => await action(t1, t2, t3, t4, t5, t6, t7, t8));
        }
    }

    public class SortedEventAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9> : SortedEventAsyncBase<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, UniTask<bool>>> {
        public async UniTask InvokeAsync(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9) {
            await InvokeInternalAsync(async action => await action(t1, t2, t3, t4, t5, t6, t7, t8, t9));
        }
    }

    public class SortedEventAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : SortedEventAsyncBase<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, UniTask<bool>>> {
        public async UniTask InvokeAsync(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10) {
            await InvokeInternalAsync(async action => await action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10));
        }
    }

    public class SortedEventAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : SortedEventAsyncBase<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, UniTask<bool>>> {
        public async UniTask InvokeAsync(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11) {
            await InvokeInternalAsync(async action => await action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11));
        }
    }

    public class SortedEventAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : SortedEventAsyncBase<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, UniTask<bool>>> {
        public async UniTask InvokeAsync(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5, T6 t6, T7 t7, T8 t8, T9 t9, T10 t10, T11 t11, T12 t12) {
            await InvokeInternalAsync(async action => await action(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12));
        }
    }
}
