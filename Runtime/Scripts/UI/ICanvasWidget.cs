namespace FinnSchuuring.Utilities {
    using System.Threading.Tasks;

    public interface ICanvasWidget {
        public virtual Task EnableAsync() {
            return Task.CompletedTask;
        }

        public virtual Task DisableAsync() {
            return Task.CompletedTask;
        }
    }
}