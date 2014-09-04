namespace BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces
{
    using System;
    using System.Windows.Input;

    public interface IMainWindowViewModel : IViewModel 
    {
        event Action LogOutAndShowLoginWindow;

        event Action ViewShow;

        event Action ViewHide;

        event Action ViewClose;

        event Action ViewOpenMain;

        ICommand DisplayIssuesCommand { get; }

        void OpenMain();
    }
}