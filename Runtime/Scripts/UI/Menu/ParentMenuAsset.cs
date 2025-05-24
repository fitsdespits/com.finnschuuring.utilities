namespace FinnSchuuring.Utilities {
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Menu/Parent")]
    public class ParentMenuAsset : MenuAsset {
        [field: Header("ParentMenuAsset")]
        [field: SerializeField] public List<MenuAsset> Children { get; set; } = new();

        public override void InitializeWidget(MenuWidget widget) {
            widget.CreatePrototype(widget.gameObject);
            foreach (var child in Children) {
                GameObject gameObject;
                if (child.OverridePrefab != null) {
                    gameObject = child.OverridePrefab;
                } else {
                    gameObject = widget.Prototype;
                }
                var childWidget = widget.AddChildWidget(gameObject) as MenuWidget;
                childWidget.InitializeAsChild(child, widget, widget.transform.parent as RectTransform);
            }
            widget.AlignChildWidgetsSingleAxis(widget.AlignmentMode, widget.Spacing);
            widget.OnCursorDown.Subscribe(HandleCursorDown);
        }

        private void HandleCursorDown(MenuWidget widget) {
            if (widget.IsChangingEnabledState) {
                return;
            }
            if (widget.Parent == null) {
                return;
            }
            foreach (var childWidget in widget.Parent.ChildWidgets) {
                _ = childWidget.TryDisableAsync();
            }
            foreach (var childWidget in widget.ChildWidgets) {
                _ = childWidget.TryEnableAsync();
            }
        }
    }
}