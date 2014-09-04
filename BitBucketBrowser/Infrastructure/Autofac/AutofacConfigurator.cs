namespace BitBucketBrowser.Infrastructure.Autofac
{
    using global::Autofac;

    using BitBucketBrowser.Bll.Annotations;
    using BitBucketBrowser.Bll.BitBucket;
    using BitBucketBrowser.Bll.BitBucket.Interfaces;
    using BitBucketBrowser.Bll.Logic;
    using BitBucketBrowser.Bll.Logic.Interfaces;
    using BitBucketBrowser.Bll.Presentation.ViewModel;
    using BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces;
    using BitBucketBrowser.Bll.Wrapper;
    using BitBucketBrowser.Bll.Wrapper.Interfaces;
    using BitBucketBrowser.Common.Interfaces;
    using BitBucketBrowser.Common.Service;
    using BitBucketBrowser.Common.Service.Interfaces;
    using BitBucketBrowser.Dal.Store;
    using BitBucketBrowser.View.Main;
    using BitBucketBrowser.View.Main.Interfaces;

    using LoginWindowView = BitBucketBrowser.View.Main.LoginWindowView;
    using MainWindow = BitBucketBrowser.View.Main.MainWindow;

    [UsedImplicitly]
    public class AutofacConfigurator
    {
        public static IContainer ConfigureContainer()
        {
            var container = new ContainerBuilder();

            ConfigureSingleInstances(container);
            ConfigureTransientInstances(container);

            return container.Build();
        }

        private static void ConfigureTransientInstances(ContainerBuilder container)
        {
            container.RegisterType<BitBucketClient>().As<IBitBucketClient>();
            container.RegisterType<QueryService>().As<IQueryService>();
            container.RegisterType<HttpClientWrapper>().As<IHttpClientWrapper>();            
            container.RegisterType<LoginService>().As<ILoginService>();
            container.RegisterType<ConfigurationService>().As<IConfigurationService>();
            container.RegisterType<AddEditQueryWindow>().As<IAddEditQueryWindowView>();
            container.RegisterType<AddEditQueryViewModel>().As<IAddEditQueryViewModel>();
        }

        private static void ConfigureSingleInstances(ContainerBuilder container)
        {
            container.RegisterType<MainWindow>().As<IMainWindowView>().SingleInstance();
            container.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>().SingleInstance();
            container.RegisterType<LoginWindowView>().As<ILoginWindowView>().SingleInstance();
            container.RegisterType<LoginWindowViewModel>().As<ILoginWindowViewModel>().SingleInstance();            
            container.RegisterType<QueryViewModel>().As<IQueryViewModel>().SingleInstance();
            container.RegisterType<UserService>().As<IUserService>().SingleInstance();
            container.RegisterType<AppConfigService>().As<IAppConfigService>().SingleInstance();
            container.RegisterType<DataStore>().As<IDataStore>().SingleInstance();
        }
    }
}