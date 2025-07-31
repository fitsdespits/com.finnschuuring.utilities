namespace FinnSchuuring.Utilities {
    using System.Threading.Tasks;

    public interface ISceneLoadable {
        public int LoadPriority { get; }

        public Task LoadAsync();

        public Task UnloadAsync();
    }
}
