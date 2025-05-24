using TMPro;
using UnityEngine;

namespace FinnSchuuring.Utilities {
    public class SettingMenuWidget : MenuWidget {
        public int SelectedIndex { get; set; } = 0;
        public SortedEvent<SettingMenuWidget> OnSelectedIndexChanged { get; private set; } = new();

        [Header("SettingMenuWidget")]
        [SerializeField] private PointerHandler leftPointerHandler = null;
        [SerializeField] private PointerHandler rightPointerHandler = null;
        [SerializeField] private TMP_Text selectedOptionRenderer = null;

        public override void OnInitializeAsChild() {
            leftPointerHandler.OnCursorDown.Subscribe(HandleCursorDownLeft);
            rightPointerHandler.OnCursorDown.Subscribe(HandleCursorDownRight);
        }

        public void SetSelectedOptionText(string name) {
            selectedOptionRenderer.text = name;
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