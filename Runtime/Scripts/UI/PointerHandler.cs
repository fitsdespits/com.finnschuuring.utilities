namespace FinnSchuuring.Utilities {
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class PointerHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler {
        public SortedEvent OnCursorDown { get; private set; } = new();
        public SortedEvent OnCursorUp { get; private set; } = new();
        public SortedEvent OnCursorEnter { get; private set; } = new();
        public SortedEvent OnCursorExit { get; private set; } = new();

        public void OnPointerDown(PointerEventData eventData) {
            OnCursorDown.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData) {
            OnCursorUp.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            OnCursorEnter.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData) {
            OnCursorExit.Invoke();
        }

        private void OnDestroy() {
            OnCursorDown.UnsubscribeAll();
            OnCursorUp.UnsubscribeAll();
            OnCursorEnter.UnsubscribeAll();
            OnCursorExit.UnsubscribeAll();
        }
    }
}