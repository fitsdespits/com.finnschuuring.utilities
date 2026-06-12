namespace FinnSchuuring.Utilities {
    using Cysharp.Threading.Tasks;

    public interface ISceneLoadable {
        public int LoadPriority { get; }

        public UniTask LoadAsync();

        public UniTask UnloadAsync();
    }
}
