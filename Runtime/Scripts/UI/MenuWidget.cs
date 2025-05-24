namespace FinnSchuuring.Utilities {
    using DG.Tweening;
    using System.Threading.Tasks;
    using TMPro;
    using UnityEngine;

    public class MenuWidget : Screen<MenuWidget> {
        [field: SerializeField] public WidgetAlignmentMode AlignmentMode { get; private set; } = WidgetAlignmentMode.DirectionalDown;
        [field: SerializeField] public float Spacing { get; private set; } = 100f;

        public MenuAsset Asset { get; private set; } = null;
        public MenuWidget Parent { get; private set; } = null;
        public SortedEvent<MenuWidget> OnCursorDown { get; private set; } = new();

        [SerializeField] private ParentMenuAsset parentAsset = null;
        [SerializeField] private TMP_Text textRenderer = null;
        [SerializeField] private CanvasGroup canvasGroup = null;

        public override async Task OnLoadAsync() {
            parentAsset.InitializeWidget(this);
            DisableAllExceptChildContainer();
            await Task.CompletedTask;
        }

        public override async Task OnEnableAsync() {
            gameObject.SetActive(true);
            await Task.Delay(100);
            await canvasGroup.DOFade(1.0f, 0.1f).AsyncWaitForCompletion();
        }

        public override async Task OnDisableAsync() {
            await canvasGroup.DOFade(0.0f, 0.1f).AsyncWaitForCompletion();
            gameObject.SetActive(false);
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
            textRenderer.text = Asset.Name;
        }

        public override void OnPointerDown() {
            OnCursorDown.Invoke(this);
        }

        private void OnDestroy() {
            OnCursorDown.UnsubscribeAll();
        }

        public virtual void OnInitializeAsChild() {

        }
    }
}