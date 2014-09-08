namespace BitBucketBrowser.View.Main.Interfaces
{
    public interface IShowIssueWindow : IView
    {
        void ShowIssueDialog(string issueId, string repoSlug);
    }
}