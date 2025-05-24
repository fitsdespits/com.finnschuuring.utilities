using UnityEngine;

namespace FinnSchuuring.Utilities {
    public class SettingMenuWidget : MenuWidget {
        public SortedEvent<SettingMenuWidget> OnSelectedIndexChanged { get; private set; } = new();
        public int SelectedIndex { get; set; } = 0;

        [SerializeField] private PointerHandler leftPointerHandler = null;
        [SerializeField] private PointerHandler rightPointerHandler = null;

        public override void OnInitializeAsChild() {
            leftPointerHandler.OnCursorDown.Subscribe(HandleCursorDownLeft);
            rightPointerHandler.OnCursorDown.Subscribe(HandleCursorDownRight);
        }

        private void HandleCursorDownLeft() {
            SelectedIndex--;
            OnSelectedIndexChanged.Invoke(this);
        }

        private void HandleCursorDownRight() {
            SelectedIndex++;
            OnSelectedIndexChanged.Invoke(this);
        }
    }
}