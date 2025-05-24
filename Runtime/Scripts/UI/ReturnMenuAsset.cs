namespace FinnSchuuring.Utilities {
    using UnityEngine;

    [CreateAssetMenu(menuName = "Menu/Return")]
    public class ReturnMenuAsset : MenuAsset {
        public override void InitializeWidget(MenuWidget widget) {
            widget.OnCursorDown.Subscribe(HandleCursorDown);
        }

        private void HandleCursorDown(MenuWidget widget) {
            if (widget.IsChangingEnabledState) {
                return;
            }
            foreach (var childWidget in widget.Parent.ChildWidgets) {
                _ = childWidget.TryDisableAsync();
            }
            foreach (var childWidget in widget.Parent.Parent.ChildWidgets) {
                _ = childWidget.TryEnableAsync();
            }
        }
    }
}