namespace BitBucketBrowser.Bll.Logic
{
    using System.Collections.Generic;
    using System.Linq;

    using BitBucketBrowser.Bll.Logic.Interfaces;
    using BitBucketBrowser.Common.Dto;
    using BitBucketBrowser.Common.Interfaces;

    public class ConfigurationService : IConfigurationService
    {
        private readonly IDataStore dataStore;

        public ConfigurationService(IDataStore dataStore)
        {
            this.dataStore = dataStore;
        }

        public Configuration GetConfiguration()
        {
            using (var session = this.dataStore.OpenSession())
            {
                if (!this.HasSavedConfiguration())
                {
                    this.SaveConfiguration(new Configuration { UserQueries = new List<Query>() });
                }

                return session.Query<Configuration>().First();
            }
        }

        public bool HasSavedConfiguration()
        {
            using (var session = this.dataStore.OpenSession())
            {
                return session.Query<Configuration>().Any();
            }
        }

        public void SaveConfiguration(Configuration configuration)
        {
            using (var session = this.dataStore.OpenSession())
            {
                session.Store(configuration);
                session.SaveChanges();
            }
        }
    }
}