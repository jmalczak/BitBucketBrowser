namespace BitBucketBrowser.View.Main.Interfaces
{
    using BitBucketBrowser.Common.Dto.BitBucket;

    public interface IShowIssueWindow : IView
    {
        void ShowIssueDialog(ViewBase owner, Issue issue);
    }
}