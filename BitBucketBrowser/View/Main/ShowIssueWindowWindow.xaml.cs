namespace BitBucketBrowser.View.Main
{
    using System;

    using BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces;
    using BitBucketBrowser.View.Main.Interfaces;

    /// <summary>
    /// Interaction logic for ShowIssueWindowWindow.xaml
    /// </summary>
    public partial class ShowIssueWindowWindow : IShowIssueWindow
    {
        private readonly Func<string, string, IShowIssueWindowViewModel> viewModelFactory;

        public ShowIssueWindowWindow(Func<string, string, IShowIssueWindowViewModel> viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
            InitializeComponent();
        }

        public void ShowIssueDialog(string issueId, string repoSlug)
        {
            this.DataContext = this.viewModelFactory(issueId, repoSlug);
        }
    }
}
