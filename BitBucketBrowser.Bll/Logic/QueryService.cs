namespace BitBucketBrowser.Bll.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using BitBucketBrowser.Bll.Annotations;
    using BitBucketBrowser.Bll.Logic.Interfaces;
    using BitBucketBrowser.Common.Dto;

    [UsedImplicitly]
    public class QueryService : IQueryService
    {
        private readonly IConfigurationService configurationService;

        public QueryService(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        public IEnumerable<Query> GetQueryiesByRepositorySlug(string slug)
        {
            var queriesList = new List<Query>
                                  {
                                      this.GetGlobalQueries(slug),
                                      this.GetTopLevelUserQueries(slug)
                                  };

            return queriesList;
        }

        public void AddUserQuery(Query query)
        {
            var configuration = this.configurationService.GetConfiguration();

            if (configuration.UserQueries == null)
            {
                configuration.UserQueries = new List<Query>();
            }

            configuration.UserQueries.Add(query);
            this.configurationService.SaveConfiguration(configuration);
        }

        public void DeleteUserQuery(Guid id)
        {
            var configuration = this.configurationService.GetConfiguration();

            if (configuration.UserQueries != null)
            {
                var queryToDelete = configuration.UserQueries.FirstOrDefault(q => q.Id == id);

                if (queryToDelete != null)
                {
                    configuration.UserQueries.Remove(queryToDelete);
                    this.configurationService.SaveConfiguration(configuration);
                }
            }
        }

        public void AddOrUpdateQuery(Query query)
        {
            var configuration = this.configurationService.GetConfiguration();

            if (configuration.UserQueries.Any(q => q.Id == query.Id))
            {
                this.UpdateUserQuery(query);
            }
            else
            {
                this.AddUserQuery(query);
            }
        }

        private void UpdateUserQuery(Query query)
        {
            var configuration = this.configurationService.GetConfiguration();
            var queryToUpdate = configuration.UserQueries.First(q => q.Id == query.Id);

            queryToUpdate.Name = query.Name;
            queryToUpdate.Value = query.Value;

            this.configurationService.SaveConfiguration(configuration);
        }

        private Query GetGlobalQueries(string slug)
        {
            return new Query
                       {
                           Name = "Global Queries",
                           Children = new List<Query> { new Query { Name = "All Items", RepositorySlug = slug, Id = Guid.NewGuid() }, new Query { Name = "My Items", RepositorySlug = slug, Id = Guid.NewGuid() } }
                       };
        }

        private List<Query> GetUserQueries(string slug)
        {
            var userQueries = new List<Query>();

            var configuration = this.configurationService.GetConfiguration();

            if (configuration.UserQueries != null)
            {
                userQueries = configuration.UserQueries.Where(q => q.RepositorySlug == slug).ToList();
            }

            return userQueries;
        }

        private Query GetTopLevelUserQueries(string slug)
        {
            return new Query { IsUserQuery = true, IsTopLevelUserQuery = true, Name = "User Queries", Children = this.GetUserQueries(slug) };
        }
    }
}