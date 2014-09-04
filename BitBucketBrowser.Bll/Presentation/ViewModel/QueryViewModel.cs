namespace BitBucketBrowser.Bll.Presentation.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
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

    using Task = System.Threading.Tasks.Task;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class QueryViewModel : NotifyPropertyChangedBase, IQueryViewModel
    {
        private readonly IBitBucketClient bitBucketClient;

        private readonly IQueryService queryService;

        private List<Repository> repositories;

        private string currentRepositorySlug;

        private bool isQueryListVisible;

        private ObservableCollection<QueryTreeViewModel> queries = new ObservableCollection<QueryTreeViewModel>();

        public QueryViewModel(IBitBucketClient bitBucketClient, IQueryService queryService)
        {
            this.bitBucketClient = bitBucketClient;
            this.queryService = queryService;
        }

        public event Action<Query, Action> EditQuery;

        public event Action<Query, Action> AddQuery;

        public bool IsQueryListVisible
        {
            get
            {
                return this.isQueryListVisible;
            }

            set
            {
                this.isQueryListVisible = value;
                this.OnPropertyChanged();
            }
        }

        public string CurrentRepositorySlug
        {
            get
            {
                return this.currentRepositorySlug;
            }

            set
            {
                this.currentRepositorySlug = value;
                this.IsQueryListVisible = value != null;
                this.PopulateQueriesForCurrentRepository();

                this.OnPropertyChanged();
            }
        }

        public List<Repository> Repositories
        {
            get
            {
                return this.repositories;
            }

            set
            {
                this.repositories = value;
                this.OnPropertyChanged();
            }
        }

        public ObservableCollection<QueryTreeViewModel> Queries
        {
            get
            {
                return this.queries;
            }

            set
            {
                this.queries = value;
                this.OnPropertyChanged();
            }
        }

        public ICommand DisplayIssuesCommand { get; set; }

        public Action RefreshQueriesForCurrentRepository
        {
            get
            {
                return this.PopulateQueriesForCurrentRepository;
            }
        }

        private ICommand AddNewQueryCommand
        {
            get
            {
                return new DelegateCommandOfT<QueryTreeViewModel>(
                    queryTreeViewModel =>
                    {
                        if (this.AddQuery != null)
                        {
                            this.AddQuery(new Query { Id = Guid.NewGuid(), RepositorySlug = this.CurrentRepositorySlug, IsUserQuery = true }, RefreshQueriesForCurrentRepository);
                        }
                    });
            }
        }

        private ICommand DeleteQueryCommand
        {
            get
            {
                return new DelegateCommandOfT<QueryTreeViewModel>(
                    queryTreeViewModel => Task.Factory.StartNew(
                        () => this.queryService.DeleteUserQuery(queryTreeViewModel.Id)).ContinueWithUi(result => this.PopulateQueriesForCurrentRepository()));
            }
        }

        private ICommand EditQueryCommand
        {
            get
            {
                return new DelegateCommandOfT<QueryTreeViewModel>(
                    queryTreeViewModel =>
                    {
                        if (this.EditQuery != null)
                        {
                            this.EditQuery(queryTreeViewModel.Query, RefreshQueriesForCurrentRepository);
                        }
                    });
            }
        }

        public void GetRepositories()
        {
            this.bitBucketClient.GetRepositories().ContinueWithUi(this.PopulateRepositoriesAndQueries);
        }

        private void PopulateRepositoriesAndQueries(Task<List<Repository>> repositoryResultTask)
        {
            var resultFromService = repositoryResultTask.Result.OrderBy(repo => repo.Name).ToList();
            this.Repositories = resultFromService;

            var repositoryToSelect = repositoryResultTask.Result.FirstOrDefault();

            if (repositoryToSelect != null)
            {
                this.CurrentRepositorySlug = repositoryToSelect.Slug;
            }
        }

        private void PopulateQueriesForCurrentRepository()
        {
            Task.Factory.StartNew(() => this.queryService.GetQueryiesByRepositorySlug(this.currentRepositorySlug)).ContinueWithUi(
                result =>
                {
                    var selectedQuery = this.Queries.SelectMany(q => q.Children).FirstOrDefault(q => q.Selected);
                    var queriesByRepositorySlug = result.Result;
                    var selectedQueries = queriesByRepositorySlug.Select(query =>
                            new QueryTreeViewModel(
                                query,
                                this.CreateCommands())).ToList();

                    selectedQueries.First().Children.First().Selected = true;
                    this.Queries = new ObservableCollection<QueryTreeViewModel>(selectedQueries);
                });
        }

        private QueryTreeViewModelCommands CreateCommands()
        {
            return new QueryTreeViewModelCommands
                       {
                           DisplayIssuesCommand = this.DisplayIssuesCommand,
                           AddNewQueryCommand = this.AddNewQueryCommand,
                           EditQueryCommand = this.EditQueryCommand,
                           DeleteQueryCommand = this.DeleteQueryCommand
                       };
        }
    }
}