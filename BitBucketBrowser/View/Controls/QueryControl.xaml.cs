namespace BitBucketBrowser.View.Controls
{
    using System.Windows;
    using System.Windows.Controls;

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

            if (value != null)
            {
                this.ViewModel.DisplayIssuesCommand.Execute(value);
            }
        }

        private void _myTree_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.OriginalSource as TreeViewItem;
            if (item != null)
            {
                //_currentSelected = item;
            }
        }
    }
}
