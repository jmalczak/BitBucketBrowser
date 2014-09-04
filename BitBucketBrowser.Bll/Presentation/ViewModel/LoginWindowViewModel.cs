namespace BitBucketBrowser.Bll.Presentation.ViewModel
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using BitBucketBrowser.Bll.Annotations;
    using BitBucketBrowser.Bll.BitBucket.Interfaces;
    using BitBucketBrowser.Bll.Logic.Interfaces;
    using BitBucketBrowser.Bll.Presentation.Common;
    using BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces;
    using BitBucketBrowser.Common.Dto;
    using BitBucketBrowser.Common.Dto.BitBucket;
    using BitBucketBrowser.Common.Extensions;
    using BitBucketBrowser.Common.Service.Interfaces;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class LoginWindowViewModel : NotifyPropertyChangedBase, ILoginWindowViewModel
    {
        private readonly IUserService userService;

        private readonly IBitBucketClient bitBucketClient;

        private readonly ILoginService login;

        private Credentials credentials;

        private Message errorMessage; 

        public LoginWindowViewModel(
            IUserService userService,
            IBitBucketClient bitBucketClient,
            ILoginService login)
        {
            this.bitBucketClient = bitBucketClient;
            this.login = login;
            this.userService = userService;

            this.ClearCurrentUserCredentials();
            this.ClearErrorMessage();
        }

        public event Action OpenMainWindow;

        public event Action ViewShow;

        public event Action ViewClearPassword;

        public event Action ApplicationClose;

        public Credentials Credentials
        {
            get
            {
                return this.credentials;
            }

            set
            {
                this.credentials = value;
                this.OnPropertyChanged();
            }
        }

        public Message ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }

            set
            {
                this.errorMessage = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand LogInCommand
        {
            get
            {
                return new DelegateCommandOfT<LoginWindowViewModel>(viewModel => this.LogInUser());
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return new DelegateCommandOfT<LoginWindowViewModel>(
                    viewModel =>
                        {
                            if (this.ApplicationClose != null)
                            {
                                this.ApplicationClose();
                            }
                        });
            }
        }

        public void LogInUser()
        {
            this.ClearErrorMessage();

            this.bitBucketClient.AuthenticateUser(this.Credentials.Login, this.Credentials.Password)
                                .ContinueWithUi(this.HandleLoginResult);
        }

        public void ShowMainOrLoginView()
        {
            if (this.login.CredentialsSaved())
            {
                this.Credentials = this.login.GetSavedCredentials();
                this.LogInUser();
            }
            else
            {
                if (this.ViewShow != null)
                {
                    this.ViewShow();
                }
            }
        }

        public void ClearLoginDetails()
        {
            this.Credentials = new Credentials();

            if (this.ViewClearPassword != null)
            {
                this.ViewClearPassword();
            }            
        }

        private void HandleLoginResult(Task<User> r)
        {
            this.userService.LogInUser(r.Result);

            if (this.userService.IsAuthenticated())
            {
                if (this.Credentials.Remember)
                {
                    this.login.SaveCredentials(this.Credentials);
                }

                this.OpenMainView();
            }
            else
            {
                this.SetErrorMessage("Incorrect credentials");
            }
        }

        private void OpenMainView()
        {
            if (this.OpenMainWindow != null)
            {
                this.OpenMainWindow();
            }                      
        }

        private void ClearCurrentUserCredentials()
        {
            this.Credentials = new Credentials();
        }

        private void ClearErrorMessage()
        {
            this.ErrorMessage = new Message();
        }

        private void SetErrorMessage(string message)
        {
            this.ErrorMessage = new Message { MessageText = message };
        }
    }
}
