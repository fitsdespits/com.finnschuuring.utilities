namespace FinnSchuuring.Utilities {
    using UnityEngine;

    public abstract class MenuAsset : ScriptableObjectAsset {
        public override bool IsInstantiatable => false;

        [field: SerializeField] public string Name { get; set; } = "Option";
        [field: SerializeField] public GameObject OverridePrefab { get; private set; } = null;

        public abstract void InitializeWidget(MenuWidget widget);
    }
}