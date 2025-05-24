namespace FinnSchuuring.Utilities {
    using Sirenix.OdinInspector;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class SettingMenuAsset<T> : MenuAsset {
        [field: Header("SettingMenuAsset")]
        [field: SerializeField] public int DefaultIndex { get; private set; } = 0;
        [field: SerializeField] public List<SettingOption<T>> Options { get; private set; } = new();

        public override void InitializeWidget(MenuWidget widget) {
            var settingWidget = widget as SettingMenuWidget;
            var selectedSettingOption = GetSelectedOption();
            settingWidget.SelectedIndex = Options.IndexOf(selectedSettingOption);
            settingWidget.SetSelectedOptionText(selectedSettingOption.Name);
            settingWidget.OnSelectedIndexChanged.Subscribe(HandleSelectedIndexChanged);
        }

        private void HandleSelectedIndexChanged(SettingMenuWidget settingWidget) {
            if (settingWidget.SelectedIndex > Options.Count - 1) {
                settingWidget.SelectedIndex = 0;
            }
            if (settingWidget.SelectedIndex < 0) {
                settingWidget.SelectedIndex = Options.Count - 1;
            }
            var selectedSettingOption = Options[settingWidget.SelectedIndex];
            OnSelectedOptionChanged(selectedSettingOption);
            settingWidget.SetSelectedOptionText(selectedSettingOption.Name);
        }

        public abstract SettingOption<T> GetSelectedOption();

        public abstract void OnSelectedOptionChanged(SettingOption<T> settingOption);
    }

    [Serializable]
    public class SettingOption<T> {
        [field: SerializeField, HorizontalGroup("Option", Width = 0.5f), HideLabel] public string Name { get; private set; } = "Option";
        [field: SerializeField, HorizontalGroup("Option", Width = 0.5f), HideLabel] public T Value { get; private set; } = default;
    }
}