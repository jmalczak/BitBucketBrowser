namespace BitBucketBrowser.View
{
    using System.Windows;

    using BitBucketBrowser.View.Main.Interfaces;

    public abstract class ViewBase : Window, IView
    {
        public void SetAsMainWindow()
        {
            Application.Current.MainWindow = this;
        }

        public void ApplicationClose()
        {
            Application.Current.Shutdown();
        }
    }
}