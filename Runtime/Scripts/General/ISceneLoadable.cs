namespace FinnSchuuring.Utilities {
    using System.Threading.Tasks;

    public interface ISceneLoadable {
        public Task LoadAsync();

        public Task UnloadAsync();
    }
}