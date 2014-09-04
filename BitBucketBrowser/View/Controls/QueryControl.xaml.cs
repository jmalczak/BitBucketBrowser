namespace BitBucketBrowser.View.Controls
{
    using System.Windows;

    using BitBucketBrowser.Bll.Presentation.ViewModel;
    using BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces;
    using BitBucketBrowser.View.Controls.Interfaces;

    public partial class QueryControl : IQueryControlView
    {
        public QueryControl()
        {
            InitializeComponent();
        }

        public IQueryViewModel ViewModel
        {
            get
            {
                return this.DataContext as IQueryViewModel;
            }

            set
            {
                this.DataContext = value;
            }
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var value = e.NewValue as QueryTreeViewModel;
            this.ViewModel.DisplayIssuesCommand.Execute(value);
        }
    }
}
