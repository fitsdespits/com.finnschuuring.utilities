namespace FinnSchuuring.Utilities {
    public interface ISmartCursorObject {
        public SmartCursorSettings SmartCursorSettings { get; }

        public void OnSmartCursorDown();

        public void OnSmartCursorUp();

        public void OnSmartCursorEnter();

        public void OnSmartCursorExit();
    }
}
