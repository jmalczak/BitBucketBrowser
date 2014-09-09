namespace BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces
{
    public interface IShowIssueWindowViewModel
    {
        string IssueId { get; }

        string IssueTitle { get; }

        string IssueContent { get; }
    }
}