namespace FinnSchuuring.Utilities {
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class StateMachine : State {
        protected State CurrentState { get; private set; } = null;
        public SortedEvent<State> OnStateChanged { get; private set; } = new();

        protected readonly List<State> states = new();
        protected readonly List<State> queuedStates = new();

        public void GoToState<T>() where T : State {
            GoToState(GetOrCreateState<T>());
        }

        public void GoToState<T>(params object[] data) where T : State {
            GoToState(GetOrCreateState<T>(), new StateData(data));
        }

        private void GoToState(State state, StateData data = null) {
            if (CurrentState != null) {
                CurrentState.Deactivate();
            }
            CurrentState = state;
            if (state != null) {
                CurrentState.SetData(this, data);
                CurrentState.Activate();
            }
            OnStateChanged.Invoke(CurrentState);
        }

        public void GoToNoState() {
            GoToState(null);
        }

        public void QueueState<T>(StateQueueMode queueMode) where T : State {
            QueueState(GetOrCreateState<T>(), queueMode);
        }

        public void QueueState<T>(StateQueueMode queueMode, params object[] data) where T : State {
            QueueState(GetOrCreateState<T>(), queueMode, new StateData(data));
        }

        private void QueueState(State state, StateQueueMode queueMode = StateQueueMode.Last, StateData data = null) {
            state.SetData(this, data);
            switch (queueMode) {
                case StateQueueMode.First:
                    queuedStates.Insert(0, state);
                    break;
                case StateQueueMode.Last:
                    queuedStates.Add(state);
                    break;
            }
        }

        public void AdvanceQueue() {
            if (queuedStates == null || queuedStates.Count == 0) {
                GoToNoState();
                return;
            }
            State nextQueuedState = queuedStates.First();
            queuedStates.Remove(nextQueuedState);
            GoToState(nextQueuedState);
        }

        public void ClearQueue() {
            foreach (var queuedState in queuedStates) {
                queuedState.ResetData();
            }
            queuedStates.Clear();
        }

        private State GetOrCreateState<T>() where T : State {
            State state = states.Find(x => x.GetType() == typeof(T));
            if (state == null) {
                state = gameObject.FindInDirectChildren<T>();
                if (state == null) {
                    GameObject obj = new();
                    obj.name = typeof(T).Name;
                    obj.transform.SetParent(transform, false);
                    state = obj.AddComponent(typeof(T)) as State;
                    states.Add(state);
                }
            }
            return state;
        }
    }
}