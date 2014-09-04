namespace BitBucketBrowser.View.Main
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    using BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces;
    using BitBucketBrowser.View.Main.Interfaces;

    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindowView : ILoginWindowView
    {
        private readonly ILoginWindowViewModel loginWindowViewModel;

        private readonly IMainWindowView mainWindowView;

        public LoginWindowView(ILoginWindowViewModel loginWindowViewModel, IMainWindowView mainWindowView)
        {
            this.loginWindowViewModel = loginWindowViewModel;
            this.mainWindowView = mainWindowView;

            InitializeComponent();

            this.InitializeViewModels();
            this.UserNameText.Focus();
        }

        public void ShowMainOrLoginView()
        {
            this.loginWindowViewModel.ShowMainOrLoginView();
        }

        protected override void OnClosed(EventArgs e)
        {
            this.ApplicationClose();
        }

        private void InitializeViewModels()
        {
            this.DataContext = this.loginWindowViewModel;

            this.loginWindowViewModel.ApplicationClose += this.ApplicationClose;
            this.loginWindowViewModel.ViewClearPassword += this.ViewClearPassword;
            this.loginWindowViewModel.OpenMainWindow += this.OpenMainWindow;
            this.loginWindowViewModel.ViewShow += this.Show;
            this.mainWindowView.LogOutAndShowLoginWindow += this.LogOutAndShowLoginWindow;
        }

        private void LogOutAndShowLoginWindow()
        {
            this.loginWindowViewModel.ClearLoginDetails();
            this.loginWindowViewModel.ShowMainOrLoginView();
        }

        private void OpenMainWindow()
        {
            this.Hide();
            this.mainWindowView.OpenMain();
        }

        private void ViewClearPassword()
        {
            this.Password.Password = string.Empty;
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.loginWindowViewModel != null && sender is PasswordBox)
            {
                this.loginWindowViewModel.Credentials.Password = (sender as PasswordBox).Password;
            }
        }        
    }
}
