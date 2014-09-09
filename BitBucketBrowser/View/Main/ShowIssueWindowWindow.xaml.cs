namespace BitBucketBrowser.View.Main
{
    using System;

    using BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces;
    using BitBucketBrowser.Common.Dto.BitBucket;
    using BitBucketBrowser.View.Main.Interfaces;

    /// <summary>
    /// Interaction logic for ShowIssueWindowWindow.xaml
    /// </summary>
    public partial class ShowIssueWindowWindow : IShowIssueWindow
    {
        private readonly Func<Issue, IShowIssueWindowViewModel> viewModelFactory;

        public ShowIssueWindowWindow(Func<Issue, IShowIssueWindowViewModel> viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
            InitializeComponent();
        }


        public void ShowIssueDialog(ViewBase owner, Issue issue)
        {
            this.Owner = owner;
            this.DataContext = this.viewModelFactory(issue);
            this.ShowDialog();
        }
    }
}
