using UnityEngine;

namespace FinnSchuuring.Utilities {
    [CreateAssetMenu(menuName = "Game/Test")]
    public class TestScriptableObject : ScriptableObjectAsset {
        public override bool IsInstantiatable => false;
    }
}