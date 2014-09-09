namespace BitBucketBrowser.View.Main
{
    using System;
    using System.Windows.Input;

    using BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces;
    using BitBucketBrowser.Common.Dto.BitBucket;
    using BitBucketBrowser.View.Main.Interfaces;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IMainWindowView
    {
        private readonly IMainWindowViewModel mainWindowViewModel;

        private readonly IQueryViewModel queryViewModel;

        private readonly Func<IAddEditQueryWindowView> addEditQueryWindowViewFactory;

        private readonly Func<IShowIssueWindow> showIssueWindowViewFactory;

        public MainWindow(
            IMainWindowViewModel mainWindowViewModel,
            IQueryViewModel queryViewModel,
            Func<IAddEditQueryWindowView> addEditQueryWindowViewFactory,
            Func<IShowIssueWindow> showIssueWindowViewFactory)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            this.queryViewModel = queryViewModel;
            this.addEditQueryWindowViewFactory = addEditQueryWindowViewFactory;
            this.showIssueWindowViewFactory = showIssueWindowViewFactory;

            InitializeComponent();

            this.InitializeViewModels();
        }

        public event Action LogOutAndShowLoginWindow;

        private IMainWindowViewModel MainWindowViewModel
        {
            get
            {
                return this.DataContext as IMainWindowViewModel;
            }

            set
            {
                this.DataContext = value;
            }
        }

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
            this.InitializeMainViewModel();
            this.InitializeQueryControlViewModel();
        }

        private void InitializeQueryControlViewModel()
        {
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

        private void InitializeMainViewModel()
        {
            this.MainWindowViewModel = this.mainWindowViewModel;

            this.mainWindowViewModel.ViewClose += this.Close;
            this.mainWindowViewModel.ViewHide += this.Hide;
            this.mainWindowViewModel.ViewShow += this.Show;
            this.mainWindowViewModel.ViewOpenMain += this.OpenMain;
            this.mainWindowViewModel.LogOutAndShowLoginWindow += this.LogOutAndShowLoginWindowHandler;
        }

        private void LogOutAndShowLoginWindowHandler()
        {
            if (this.LogOutAndShowLoginWindow != null)
            {
                this.LogOutAndShowLoginWindow();
            }
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var showIssueWindow = this.showIssueWindowViewFactory();
            showIssueWindow.ShowIssueDialog(this, IssuesGrid.SelectedItem as Issue);
        }
    }
}