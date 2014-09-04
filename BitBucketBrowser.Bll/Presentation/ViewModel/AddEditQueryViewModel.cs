namespace BitBucketBrowser.Bll.Presentation.ViewModel
{
    using System;
    using System.Windows.Input;

    using BitBucketBrowser.Bll.Logic.Interfaces;
    using BitBucketBrowser.Bll.Presentation.Common;
    using BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces;
    using BitBucketBrowser.Common.Dto;
    using BitBucketBrowser.Common.Extensions;

    public class AddEditQueryViewModel : NotifyPropertyChangedBase, IAddEditQueryViewModel
    {
        private readonly IQueryService queryService;

        private readonly Action successCallback;

        private readonly Query query;

        public AddEditQueryViewModel(IQueryService queryService, Query query, Action successCallback)
        {
            this.queryService = queryService;
            this.successCallback = successCallback;
            this.query = query;
        }

        public event Action ViewClose;

        public string QueryName
        {
            get
            {
                return this.query.Name;
            }

            set
            {
                this.query.Name = value;
                this.OnPropertyChanged();
            }
        }

        public string QueryValue
        {
            get
            {
                return this.query.Value;
            }

            set
            {
                this.query.Value = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand SaveQueryCommand
        {
            get
            {
                return new DelegateCommand(this.SaveQuery);
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new DelegateCommand(this.Close);
            }
        }

        private void SaveQuery()
        {
            this.Close();

            System.Threading.Tasks.Task.Factory.StartNew(() => this.queryService.AddOrUpdateQuery(this.query))
                                               .ContinueWithUi(result => this.successCallback());
        }

        private void Close()
        {
            if (this.ViewClose != null)
            {
                this.ViewClose();
            }
        }
    }
}