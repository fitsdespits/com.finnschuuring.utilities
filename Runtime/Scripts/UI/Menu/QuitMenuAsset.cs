namespace FinnSchuuring.Utilities {
    using UnityEngine;

    [CreateAssetMenu(menuName = "Menu/Quit")]
    public class QuitMenuAsset : MenuAsset {
        public override void InitializeWidget(MenuWidget widget) {
            widget.OnCursorDown.Subscribe(HandleCursorDown);
        }

        private void HandleCursorDown(MenuWidget widget) {
            Application.Quit();
        }
    }
}