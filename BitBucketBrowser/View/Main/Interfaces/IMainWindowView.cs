namespace BitBucketBrowser.View.Main.Interfaces
{
    using System;

    public interface IMainWindowView : IView
    {
        event Action LogOutAndShowLoginWindow;

        void OpenMain();
    }
}