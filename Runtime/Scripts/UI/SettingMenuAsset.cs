namespace FinnSchuuring.Utilities {
    using Sirenix.OdinInspector;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class SettingMenuAsset<T> : MenuAsset {
        [field: SerializeField] public List<SettingOption<T>> Options { get; private set; } = new();

        public override void InitializeWidget(MenuWidget widget) {
            var settingWidget = widget as SettingMenuWidget;
            settingWidget.OnSelectedIndexChanged.Subscribe(HandleSelectedIndexChanged);
            settingWidget.SelectedIndex = 0;
        }

        private void HandleSelectedIndexChanged(SettingMenuWidget settingWidget) {
            Debug.Log(settingWidget.SelectedIndex);
        }
    }

    [Serializable]
    public class SettingOption<T> {
        [field: SerializeField, HorizontalGroup("Option", Width = 0.5f), HideLabel] public string Name { get; private set; } = "Option";
        [field: SerializeField, HorizontalGroup("Option", Width = 0.5f), HideLabel] public T Value { get; private set; } = default;
    }
}