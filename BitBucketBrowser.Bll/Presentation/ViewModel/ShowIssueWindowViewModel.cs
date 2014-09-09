namespace BitBucketBrowser.Bll.Presentation.ViewModel
{
    using BitBucketBrowser.Bll.Annotations;
    using BitBucketBrowser.Bll.BitBucket.Interfaces;
    using BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces;
    using BitBucketBrowser.Common.Dto;
    using BitBucketBrowser.Common.Dto.BitBucket;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class ShowIssueWindowViewModel : NotifyPropertyChangedBase, IShowIssueWindowViewModel
    {
        private readonly IBitBucketClient bitBucketClient;

        private readonly Issue issue;

        public ShowIssueWindowViewModel(IBitBucketClient bitBucketClient, Issue issue)
        {
            this.bitBucketClient = bitBucketClient;
            this.issue = issue;
        }

        public string IssueId
        {
            get
            {
                return this.issue.Id.ToString();
            }
        }

        public string IssueTitle
        {
            get
            {
                return this.issue.Title;
            }
        }

        public string IssueContent
        {
            get
            {
                return this.issue.Content;
            }
        }
    }
}