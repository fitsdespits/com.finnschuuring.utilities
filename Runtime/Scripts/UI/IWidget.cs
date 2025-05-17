namespace FinnSchuuring.Utilities {
    using System.Threading.Tasks;

    public interface IWidget {
        public virtual Task EnableAsync() {
            return Task.CompletedTask;
        }

        public virtual Task DisableAsync() {
            return Task.CompletedTask;
        }
    }
}