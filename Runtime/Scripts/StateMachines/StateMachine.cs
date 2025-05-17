namespace FinnSchuuring.Utilities {
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class StateMachine : State {
        protected State CurrentState { get; private set; } = null;
        public SortedEvent<State> OnStateChanged { get; private set; } = new();

        protected readonly List<State> _states = new();
        protected readonly List<State> _queuedStates = new();
        protected readonly List<StateData> _queuedStateData = new();

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
            switch (queueMode) {
                case StateQueueMode.First:
                    _queuedStates.Insert(0, state);
                    _queuedStateData.Insert(0, data);
                    break;
                case StateQueueMode.Last:
                    _queuedStates.Add(state);
                    _queuedStateData.Add(data);
                    break;
            }
        }

        public void AdvanceQueue() {
            if (_queuedStates == null || _queuedStates.Count == 0) {
                GoToNoState();
                return;
            }
            State nextQueuedState = _queuedStates.First();
            StateData nextQueuedStateData = _queuedStateData.First();
            _queuedStates.Remove(nextQueuedState);
            _queuedStateData.Remove(nextQueuedStateData);
            GoToState(nextQueuedState, nextQueuedStateData);
        }

        public void ClearQueue() {
            _queuedStates.Clear();
            _queuedStateData.Clear();
        }

        private State GetOrCreateState<T>() where T : State {
            State state = _states.Find(x => x.GetType() == typeof(T));
            if (state == null) {
                state = gameObject.FindInDirectChildren<T>();
                if (state == null) {
                    GameObject obj = new() {
                        name = typeof(T).Name
                    };
                    obj.transform.SetParent(transform, false);
                    state = obj.AddComponent(typeof(T)) as State;
                    _states.Add(state);
                }
            }
            return state;
        }
    }
}