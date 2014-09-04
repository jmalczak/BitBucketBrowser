namespace BitBucketBrowser
{
    using System.Windows;

    using Autofac;

    using BitBucketBrowser.Infrastructure.Autofac;
    using BitBucketBrowser.View.Main.Interfaces;

    public partial class App
    {
        private readonly IContainer container;

        public App()
        {
            this.container = AutofacConfigurator.ConfigureContainer();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var loginWindow = this.container.Resolve<ILoginWindowView>();
            loginWindow.ShowMainOrLoginView();
        }
    }
}
