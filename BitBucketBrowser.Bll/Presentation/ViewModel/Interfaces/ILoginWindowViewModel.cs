namespace BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces
{
    using System;

    using BitBucketBrowser.Common.Dto;

    public interface ILoginWindowViewModel : IViewModel
    {
        Credentials Credentials { get; }

        void ClearLoginDetails();

        event Action OpenMainWindow;

        event Action ViewShow;

        event Action ViewClearPassword;

        event Action ApplicationClose;
    }
}