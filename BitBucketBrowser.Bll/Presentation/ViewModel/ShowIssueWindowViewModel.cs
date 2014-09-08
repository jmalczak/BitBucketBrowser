namespace BitBucketBrowser.Bll.Presentation.ViewModel
{
    using BitBucketBrowser.Bll.BitBucket.Interfaces;
    using BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces;
    using BitBucketBrowser.Common.Dto;

    public class ShowIssueWindowViewModel : NotifyPropertyChangedBase, IShowIssueWindowViewModel
    {
        private readonly string issueId;

        private readonly string repoSlug;

        private readonly IBitBucketClient bitBucketClient;

        private string issueTitle;

        private string issueContent;

        public ShowIssueWindowViewModel(string issueId, string repoSlug, IBitBucketClient bitBucketClient)
        {
            this.issueId = issueId;
            this.repoSlug = repoSlug;
            this.bitBucketClient = bitBucketClient;
        }

        public string IssueTitle
        {
            get
            {
                return this.issueTitle;
            }

            set
            {
                this.issueTitle = value;
                this.OnPropertyChanged();
            }
        }

        public string IssueContent
        {
            get
            {
                return this.issueContent;
            }

            set
            {
                this.issueContent = value;
                this.OnPropertyChanged();
            }
        }
    }
}