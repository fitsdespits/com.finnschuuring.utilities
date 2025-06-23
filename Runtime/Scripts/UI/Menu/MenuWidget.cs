namespace FinnSchuuring.Utilities {
    using System.Threading.Tasks;
    using TMPro;
    using UnityEngine;

    public class MenuWidget : Screen<MenuWidget> {
        [field: Header("MenuWidget")]
        [field: SerializeField] public WidgetAlignmentMode AlignmentMode { get; private set; } = WidgetAlignmentMode.DirectionalDown;
        [field: SerializeField] public float Spacing { get; private set; } = 100f;

        public MenuAsset Asset { get; private set; } = null;
        public MenuWidget Parent { get; private set; } = null;
        public SortedEvent<MenuWidget> OnCursorDown { get; private set; } = new();

        [SerializeField] private ParentMenuAsset parentAsset = null;
        [SerializeField] private TMP_Text nameRenderer = null;
        [SerializeField] private CanvasGroup canvasGroup = null;

        public override async Task OnLoadAsync() {
            parentAsset.InitializeWidget(this);
            DisableAllExceptChildContainer();
            await Task.CompletedTask;
        }

        public override async Task OnEnableAsync() {
            gameObject.SetActive(true);
            canvasGroup.alpha = 1f;
            await Task.CompletedTask;
        }

        public override async Task OnDisableAsync() {
            canvasGroup.alpha = 0f;
            gameObject.SetActive(false);
            await Task.CompletedTask;
        }

        public void InitializeAsChild(MenuAsset asset, MenuWidget parent, RectTransform container) {
            transform.SetParent(container, false);
            Parent = parent;
            Asset = asset;
            OnInitializeAsChild();
            Asset.InitializeWidget(this);
            if (Parent.Parent != null) {
                _ = TryDisableAsync();
            }
            nameRenderer.text = Asset.Name;
        }

        public override void OnSmartCursorDown() {
            OnCursorDown.Invoke(this);
        }

        private void OnDestroy() {
            OnCursorDown.UnsubscribeAll();
        }

        public virtual void OnInitializeAsChild() {

        }
    }
}