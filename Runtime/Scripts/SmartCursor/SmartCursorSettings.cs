namespace FinnSchuuring.Utilities {
    using Sirenix.OdinInspector;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class SmartCursorSettings {
        [field: SerializeField, ValueDropdown(nameof(GetLayerOptions))] public SmartCursorLayerAsset Layer { get; private set; } = null;
        [field: SerializeField] public int OrderInLayer { get; private set; } = 0;
        [field: SerializeField] public bool DoConsumeCursorDown { get; private set; } = true;

        private IEnumerable<ValueDropdownItem<SmartCursorLayerAsset>> GetLayerOptions() {
            var result = new List<ValueDropdownItem<SmartCursorLayerAsset>>();
            if (SmartCursorLibrary.Instance == null || SmartCursorLibrary.Instance.Layers.Count == 0) {
                result.Add(new ValueDropdownItem<SmartCursorLayerAsset>("None", null));
                return result;
            }
            foreach (var layer in SmartCursorLibrary.Instance.Layers) {
                string label = layer != null ? layer.Name : "None";
                result.Add(new ValueDropdownItem<SmartCursorLayerAsset>(label, layer));
            }
            return result;
        }
    }
}
