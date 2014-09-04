namespace BitBucketBrowser.Bll.Logic
{
    using BitBucketBrowser.Bll.Annotations;
    using BitBucketBrowser.Bll.Logic.Interfaces;
    using BitBucketBrowser.Common.Dto;
    using BitBucketBrowser.Common.Interfaces;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class LoginService : ILoginService
    {
        private readonly IDataStore dataStore;

        private readonly IConfigurationService configurationService;

        public LoginService(IDataStore dataStore, IConfigurationService configurationService)
        {
            this.dataStore = dataStore;
            this.configurationService = configurationService;
        }

        public bool CredentialsSaved()
        {
            var configuratition = this.configurationService.GetConfiguration();
            return configuratition.Login != null && configuratition.Password != null;
        }

        public Credentials GetSavedCredentials()
        {
            var configuration = this.configurationService.GetConfiguration();

            if (configuration.Login != null && configuration.Password != null)
            {
                return new Credentials
                           {
                               Login = configuration.Login,
                               Password = configuration.Password
                           };
            }

            return new Credentials();
        }

        public void ClearSavedCredentials()
        {
            var configuration = this.configurationService.GetConfiguration();
            configuration.Login = null;
            configuration.Password = null;
            this.configurationService.SaveConfiguration(configuration);
        }

        public void SaveCredentials(Credentials credentials)
        {
            var configuration = this.configurationService.GetConfiguration();
            configuration.Login = credentials.Login;
            configuration.Password = credentials.Password;
            this.configurationService.SaveConfiguration(configuration);
        }
    }
}
