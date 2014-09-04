namespace BitBucketBrowser.View.Main.Interfaces
{
    using BitBucketBrowser.View.Controls.Interfaces;

    public interface IView : IControlView
    {        
        void Show();

        bool? ShowDialog();

        void Hide();
        
        void SetAsMainWindow();
        
        void Close();

        void ApplicationClose();
    }
}