namespace FinnSchuuring.Utilities {
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public abstract class Widget : MonoBehaviourAsset, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler {
        [field: SerializeField] public RectTransform ChildContainer { get; private set; } = null;
        public bool? IsEnabled { get; private set; } = null;
        public bool IsChangingEnabledState { get; private set; } = false;
        public List<Widget> ChildWidgets { get; private set; } = new();
        public GameObject Prototype { get; private set; } = null;

        public void OnPointerDown(PointerEventData eventData) {
            OnPointerDown();
        }

        public void OnPointerUp(PointerEventData eventData) {
            OnPointerUp();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            OnPointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData) {
            OnPointerExit();
        }

        public async Task<bool> TryEnableAsync() {
            if (IsChangingEnabledState) {
                return false;
            }
            if (IsEnabled.HasValue && IsEnabled.Value) {
                return false;
            }
            IsChangingEnabledState = true;
            await OnEnableAsync();
            IsEnabled = true;
            IsChangingEnabledState = false;
            return true;
        }

        public async Task<bool> TryDisableAsync() {
            if (IsChangingEnabledState) {
                return false;
            }
            if (IsEnabled.HasValue && !IsEnabled.Value) {
                return false;
            }
            IsChangingEnabledState = true;
            await OnDisableAsync();
            IsEnabled = false;
            IsChangingEnabledState = false;
            return true;
        }

        public void CreatePrototype(GameObject gameObject) {
            Prototype = Instantiate(gameObject, transform);
            Prototype.name = gameObject.name;
            Prototype.SetActive(false);
        }

        public Widget AddChildWidget(GameObject gameObject) {
            Widget widget = Instantiate(gameObject, GetChildContainer()).GetComponent<Widget>();
            widget.gameObject.SetActive(true);
            ChildWidgets.Add(widget);
            return widget;
        }

        public void RemoveChildWidget(Widget widget) {
            ChildWidgets.Remove(widget);
            Destroy(widget.gameObject);
        }

        public List<T> GetChildWidgetsAsType<T>() where T : Widget {
            List<T> childWidgets = new();
            foreach (var childWidget in ChildWidgets) {
                childWidgets.Add(childWidget as T);
            }
            return childWidgets;
        }

        public void DisableAllExceptChildContainer() {
            foreach (Transform childTransform in transform) {
                if (childTransform != ChildContainer) {
                    childTransform.gameObject.SetActive(false);
                }
            }
        }

        public void AlignChildWidgetsSingleAxis(WidgetAlignmentMode mode, float spacing) {
            switch (mode) {
                case WidgetAlignmentMode.DirectionalUp:
                    AlignChildWidgetsSingleAxis(Vector2.up, spacing, false);
                    break;
                case WidgetAlignmentMode.DirectionalDown:
                    AlignChildWidgetsSingleAxis(Vector2.down, spacing, false);
                    break;
                case WidgetAlignmentMode.DirectionalLeft:
                    AlignChildWidgetsSingleAxis(Vector2.left, spacing, false);
                    break;
                case WidgetAlignmentMode.DirectionalRight:
                    AlignChildWidgetsSingleAxis(Vector2.right, spacing, false);
                    break;
                case WidgetAlignmentMode.CenteredHorizontal:
                    AlignChildWidgetsSingleAxis(Vector2.right, spacing, true);
                    break;
                case WidgetAlignmentMode.CenteredVertical:
                    AlignChildWidgetsSingleAxis(Vector2.up, spacing, true);
                    break;
            }
        }

        public void AlignChildWidgetsDoubleAxis(WidgetAlignmentMode xMode, WidgetAlignmentMode yMode, WidgetOrderMode orderMode, int widgetsPerRow, float xSpacing, float ySpacing) {
            int count = ChildWidgets.Count;
            int rows = Mathf.CeilToInt(count / (float)widgetsPerRow);

            float totalWidth = (widgetsPerRow - 1) * xSpacing;
            float totalHeight = (rows - 1) * ySpacing;
            float xStart = xMode == WidgetAlignmentMode.CenteredHorizontal ? -totalWidth / 2f : 0f;
            float yStart = yMode == WidgetAlignmentMode.CenteredVertical ? -totalHeight / 2f : 0f;

            for (int i = 0; i < count; i++) {
                int col = i % widgetsPerRow;
                int row = i / widgetsPerRow;

                if (orderMode == WidgetOrderMode.Descending) {
                    row = rows - 1 - row;
                }

                float x = xStart + col * xSpacing;
                float y = yStart + row * ySpacing;

                if (xMode == WidgetAlignmentMode.DirectionalLeft) x = -col * xSpacing;
                if (xMode == WidgetAlignmentMode.DirectionalRight) x = col * xSpacing;
                if (yMode == WidgetAlignmentMode.DirectionalDown) y = -row * ySpacing;
                if (yMode == WidgetAlignmentMode.DirectionalUp) y = row * ySpacing;

                ChildWidgets[i].transform.localPosition = new(x, y, 0f);
            }
        }

        private void AlignChildWidgetsSingleAxis(Vector2 direction, float spacing, bool centered) {
            int count = ChildWidgets.Count;
            float totalLength = (count - 1) * spacing;
            Vector2 startOffset = centered ? -0.5f * totalLength * direction : Vector2.zero;

            for (int i = 0; i < count; i++) {
                Vector2 localPos = startOffset + i * spacing * direction;
                ChildWidgets[i].transform.localPosition = new(localPos.x, localPos.y, 0f);
            }
        }

        private RectTransform GetChildContainer() {
            if (ChildContainer == null) {
                GameObject childContainerObject = new() {
                    name = "ContainerChild"
                };
                childContainerObject.transform.SetParent(transform, false);
                ChildContainer = childContainerObject.AddComponent(typeof(RectTransform)) as RectTransform;
            }
            return ChildContainer;
        }

        public virtual async Task OnEnableAsync() {
            gameObject.SetActive(true);
            await Task.CompletedTask;
        }

        public virtual async Task OnDisableAsync() {
            gameObject.SetActive(false);
            await Task.CompletedTask;
        }

        public virtual void OnPointerDown() {

        }

        public virtual void OnPointerUp() {

        }

        public virtual void OnPointerEnter() {

        }

        public virtual void OnPointerExit() {

        }
    }
}