namespace BitBucketBrowser.View.Controls.Interfaces
{
    using System.Windows.Threading;

    public interface IControlView
    {
        object DataContext { set; }

        Dispatcher Dispatcher { get; }
    }
}