namespace BitBucketBrowser.Bll.Presentation.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;

    using BitBucketBrowser.Bll.Annotations;
    using BitBucketBrowser.Bll.BitBucket.Interfaces;
    using BitBucketBrowser.Bll.Logic.Interfaces;
    using BitBucketBrowser.Bll.Presentation.Common;
    using BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces;
    using BitBucketBrowser.Common.Dto;
    using BitBucketBrowser.Common.Dto.BitBucket;
    using BitBucketBrowser.Common.Service.Interfaces;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class MainWindowViewModel : NotifyPropertyChangedBase, IMainWindowViewModel
    {
        private readonly ILoginService loginService;

        private readonly IBitBucketClient bitBucketClient;

        private readonly IUserService userService;

        private List<Issue> issues;

        private string status;

        public MainWindowViewModel(
            ILoginService loginService,
            IBitBucketClient bitBucketClient,
            IUserService userService)
        {
            this.loginService = loginService;
            this.bitBucketClient = bitBucketClient;
            this.userService = userService;
        }

        public event Action LogOutAndShowLoginWindow;

        public event Action ViewShow;

        public event Action ViewOpenMain;

        public event Action ViewHide;

        public event Action ViewClose;

        public List<Issue> Issues
        {
            get
            {
                return this.issues;
            }

            set
            {
                this.issues = value;
                this.OnPropertyChanged();
            }
        }

        public User CurrentUser
        {
            get
            {
                return this.userService.GetCurrentUser();
            }
        }

        public string Status
        {
            get
            {
                return this.status;
            }
            
            set
            {
                this.status = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return new DelegateCommandOfT<MainWindowViewModel>(
                    viewModel =>
                        {
                            if (this.ViewClose != null)
                            {
                                this.ViewClose();
                            }
                        });
            }
        }

        public ICommand LogOutCommand
        {
            get
            {
                return new DelegateCommandOfT<MainWindowViewModel>(
                    viewModel =>
                        {
                            if (this.ViewHide != null)
                            {
                                this.ViewHide();
                            }
                            
                            this.loginService.ClearSavedCredentials();

                            if (this.LogOutAndShowLoginWindow != null)
                            {
                                this.LogOutAndShowLoginWindow();
                            }
                        });
            }
        }

        public ICommand DisplayIssuesCommand
        {
            get
            {
                return new DelegateCommandOfT<QueryTreeViewModel>(async query =>
                {
                    this.Status = "Downloading items...";

                    if (query.Name == "All Items")
                    {
                        Issues = await this.bitBucketClient.GetAllIssues(query.RepositorySlug);
                    }
                    else if (query.Name == "My Items")
                    {
                        Issues = await this.bitBucketClient.GetMyIssues(query.RepositorySlug);
                    }
                    else
                    {
                        Issues = await this.bitBucketClient.GetIssues(query);
                    }

                    this.Status = string.Empty;
                });
            }
        }

        public void OpenMain()
        {
            if (this.ViewOpenMain != null)
            {
                this.ViewOpenMain();
            }
        }

        public void ShowMainOrLoginView()
        {
            if (this.ViewShow != null)
            {
                this.ViewShow();
            }
        }
    }
}
