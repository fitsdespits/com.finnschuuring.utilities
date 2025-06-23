namespace FinnSchuuring.Utilities {
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "App/Libraries/SmartCursorLibrary", fileName = "SmartCursorLibrary")]
    public class SmartCursorLibrary : Library<SmartCursorLibrary> {
        [field: Header("SmartCursorLibrary")]
        [field: SerializeField] public float RaycastUpdateStepDuration { get; private set; } = 0.05f;
        [field: SerializeField] public LayerMask RaycastLayerMask { get; private set; } = new();
        [field: SerializeField] public List<SmartCursorLayerAsset> Layers { get; private set; } = new();
    }
}
