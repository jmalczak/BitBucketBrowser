namespace BitBucketBrowser.Bll.BitBucket
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    using BitBucketBrowser.Bll.Annotations;
    using BitBucketBrowser.Bll.BitBucket.Interfaces;
    using BitBucketBrowser.Bll.Presentation.Common;
    using BitBucketBrowser.Bll.Presentation.ViewModel;
    using BitBucketBrowser.Bll.Wrapper.Interfaces;
    using BitBucketBrowser.Common.Dto.BitBucket;
    using BitBucketBrowser.Common.Service.Interfaces;

    [UsedImplicitly]
    public class BitBucketClient : IBitBucketClient
    {
        private const int QueryLimit = 50;

        private readonly IUserService userService;

        private readonly IHttpClientWrapper httpClientWrapper;

        public BitBucketClient(Delegates.HttpClientWrapperFactory httpClientWrapperFactory, IUserService userService)
        {
            this.userService = userService;
            this.httpClientWrapper = httpClientWrapperFactory("https://bitbucket.org/");
        }

        public async Task<User> AuthenticateUser(string login, string password)
        {
            try
            {
                dynamic result =
                    await this.httpClientWrapper.GetAsync("api/1.0/user/", login, password).ConfigureAwait(false);

                if (result.user.username != null)
                {
                    return new User
                               {
                                   UserName = result.user.username,
                                   Login = login,
                                   Password = password,
                                   IsAuthenticated = true
                               };
                }

                return new User();
            }
            catch (HttpRequestException)
            {
                return new User();
            }
        }

        public async Task<List<Issue>> GetAllIssues(string repositorySlug)
        {
            try
            {
                var issues = new List<Issue>();

                dynamic result = await this.httpClientWrapper.GetAsync(string.Format("api/1.0/repositories/{0}/{1}/issues/?limit={2}&status=!closed", this.userService.GetCurrentUser().UserName, repositorySlug, QueryLimit));

                foreach (dynamic issue in result.issues)
                {
                    issues.Add(this.ParseIssue(issue));
                }

                return issues;
            }
            catch (HttpRequestException)
            {
                return new List<Issue>();
            }
        }

        public async Task<List<Repository>> GetRepositories()
        {
            try
            {
                var repositories = new List<Repository>();

                dynamic result = await this.httpClientWrapper.GetAsync("api/1.0/user/repositories/");

                foreach (dynamic repository in result)
                {
                    repositories.Add(new Repository { Name = repository.name, Slug = repository.slug });
                }

                return repositories;
            }
            catch (HttpRequestException)
            {
                return new List<Repository>();
            }
        }

        public async Task<List<Issue>> GetMyIssues(string repositorySlug)
        {
            try
            {
                var issues = new List<Issue>();

                dynamic result = await this.httpClientWrapper.GetAsync(string.Format("api/1.0/repositories/{0}/{1}/issues/?limit={2}&status=!closed&responsible={0}", this.userService.GetCurrentUser().UserName, repositorySlug, QueryLimit));

                foreach (dynamic issue in result.issues)
                {
                    issues.Add(this.ParseIssue(issue));
                }

                return issues;
            }
            catch (HttpRequestException)
            {
                return new List<Issue>();
            }
        }

        public async Task<List<Issue>> GetIssues(QueryTreeViewModel query)
        {
            try
            {
                var issues = new List<Issue>();

                dynamic result =
                    await
                    this.httpClientWrapper.GetAsync(
                        string.Format(
                            "api/1.0/repositories/{0}/{1}/issues/?limit={3}&{2}",
                            this.userService.GetCurrentUser().UserName,
                            query.RepositorySlug,
                            query.Query.Value,
                            QueryLimit));

                foreach (dynamic issue in result.issues)
                {
                    issues.Add(this.ParseIssue(issue));
                }

                return issues;
            }
            catch (HttpRequestException)
            {
                return new List<Issue>();
            }
        }

        private Issue ParseIssue(dynamic issue)
        {
            return new Issue { Id = issue.local_id, Title = issue.title, CreatedAtUtc = issue.utc_created_on, LastUpdatedAtUtc = issue.utc_last_updated, Status = issue.status, Content = issue.content };
        }
    }
}
