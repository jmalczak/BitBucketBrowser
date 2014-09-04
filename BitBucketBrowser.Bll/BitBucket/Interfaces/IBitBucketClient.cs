namespace BitBucketBrowser.Bll.BitBucket.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using BitBucketBrowser.Bll.Presentation.ViewModel;
    using BitBucketBrowser.Common.Dto.BitBucket;

    public interface IBitBucketClient
    {
        Task<User> AuthenticateUser(string login, string password);

        Task<List<Issue>> GetAllIssues(string repositorySlug);

        Task<List<Repository>> GetRepositories();

        Task<List<Issue>> GetMyIssues(string repositorySlug);

        Task<List<Issue>> GetIssues(QueryTreeViewModel query);
    }
}
