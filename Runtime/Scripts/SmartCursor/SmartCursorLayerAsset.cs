namespace FinnSchuuring.Utilities {
    using UnityEngine;

    [CreateAssetMenu(menuName = "SmartCursor/SmartCursorLayer")]
    public class SmartCursorLayerAsset : ScriptableObjectAsset {
        public override bool IsInstantiatable => false;

        [field: Header("SmartCursorLayerAsset")]
        [field: SerializeField] public string Name { get; private set; } = "Layer";
    }
}
