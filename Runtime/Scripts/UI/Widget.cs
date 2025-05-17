namespace FinnSchuuring.Utilities {
    using System.Threading.Tasks;
    using UnityEngine.EventSystems;

    public abstract class Widget : MonoBehaviourAsset, IWidget, IPointerDownHandler, IPointerUpHandler {
        public void OnPointerDown(PointerEventData eventData) {
            OnClickDown();
        }

        public void OnPointerUp(PointerEventData eventData) {
            OnClickUp();
        }

        public virtual Task EnableAsync() {
            gameObject.SetActive(true);
            return Task.CompletedTask;
        }

        public virtual Task DisableAsync() {
            gameObject.SetActive(false);
            return Task.CompletedTask;
        }

        public virtual void OnClickDown() {

        }

        public virtual void OnClickUp() {

        }
    }
}