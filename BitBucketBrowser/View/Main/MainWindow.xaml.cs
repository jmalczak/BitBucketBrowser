namespace BitBucketBrowser.View.Main
{
    using System;

    using BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces;
    using BitBucketBrowser.View.Main.Interfaces;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IMainWindowView
    {
        private readonly IMainWindowViewModel mainWindowViewModel;

        private readonly IQueryViewModel queryViewModel;

        private readonly Func<IAddEditQueryWindowView> addEditQueryWindowViewFactory;

        public MainWindow(IMainWindowViewModel mainWindowViewModel, IQueryViewModel queryViewModel, Func<IAddEditQueryWindowView> addEditQueryWindowViewFactory)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.queryViewModel = queryViewModel;
            this.addEditQueryWindowViewFactory = addEditQueryWindowViewFactory;

            InitializeComponent();

            this.InitializeViewModels();
        }

        public event Action LogOutAndShowLoginWindow;

        public void OpenMain()
        {
            this.queryViewModel.GetRepositories();
            this.SetAsMainWindow();
            this.Show();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.ApplicationClose();
        }

        private void InitializeViewModels()
        {
            this.DataContext = this.mainWindowViewModel;

            this.mainWindowViewModel.ViewClose += this.Close;
            this.mainWindowViewModel.ViewHide += this.Hide;
            this.mainWindowViewModel.ViewShow += this.Show;
            this.mainWindowViewModel.ViewOpenMain += this.OpenMain;
            this.mainWindowViewModel.LogOutAndShowLoginWindow += this.LogOutAndShowLoginWindow;

            this.queryViewModel.DisplayIssuesCommand = this.mainWindowViewModel.DisplayIssuesCommand;
            this.queryViewModel.AddQuery += (q, c) =>
                {
                    var addQueryWindow = this.addEditQueryWindowViewFactory();
                    addQueryWindow.ShowAddDialog(q, c);
                };
            this.queryViewModel.EditQuery += (q, c) =>
            {
                var addQueryWindow = this.addEditQueryWindowViewFactory();
                addQueryWindow.ShowEditDialog(q, c);
            };

            this.QueryControl.DataContext = this.queryViewModel;
        }
    }
}