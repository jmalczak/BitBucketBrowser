namespace BitBucketBrowser.Common.Service
{
    using BitBucketBrowser.Common.Service.Interfaces;

    public class AppConfigService : IAppConfigService
    {
        public string GetDatabaseConnectionString() 
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["LocalDb"].ConnectionString;
        }    
    }
}