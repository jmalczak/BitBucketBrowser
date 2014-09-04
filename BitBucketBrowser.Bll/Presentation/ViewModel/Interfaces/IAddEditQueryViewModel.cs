namespace BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces
{
    using System;
    using System.Windows.Input;

    public interface IAddEditQueryViewModel
    {
        event Action ViewClose;

        string QueryName { get; set; }

        string QueryValue { get; set; }

        ICommand SaveQueryCommand { get; }

        ICommand CancelCommand { get; }
    }
}