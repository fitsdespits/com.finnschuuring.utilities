namespace FinnSchuuring.Utilities {
    using UnityEngine;

    public abstract class State : MonoBehaviour {
        protected StateMachine StateMachine { get; private set; }
        protected StateData Data { get; private set; }
        protected bool IsActive => StateMachine != null;

        public void SetData(StateMachine stateMachine, StateData data = null) {
            StateMachine = stateMachine;
            if (data != null) {
                Data = data;
            }
        }

        public void ResetData() {
            StateMachine = null;
            Data = null;
        }

        public void Activate() {
            OnActivate();
        }

        public void Deactivate() {
            OnDeactivate();
            ResetData();
        }

        private protected void Update() {
            if (IsActive) {
                OnUpdate();
            }
        }

        public virtual void OnActivate() {

        }

        public virtual void OnDeactivate() {

        }

        public virtual void OnUpdate() {

        }
    }
}