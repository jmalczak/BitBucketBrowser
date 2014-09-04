namespace BitBucketBrowser.Bll.Presentation.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using BitBucketBrowser.Common.Dto;

    public class QueryTreeViewModel : NotifyPropertyChangedBase
    {
        private readonly ObservableCollection<QueryTreeViewModel> children = new ObservableCollection<QueryTreeViewModel>();

        private readonly Query query;
        
        private readonly QueryTreeViewModelCommands queryTreeViewModelCommands;

        private bool selected;

        public QueryTreeViewModel(Query query, QueryTreeViewModelCommands queryTreeViewModelCommands)
        {            
            this.query = query;
            this.queryTreeViewModelCommands = queryTreeViewModelCommands;

            if (query.Children != null)
            {
                this.children = new ObservableCollection<QueryTreeViewModel>(query.Children.Select(q => new QueryTreeViewModel(q, queryTreeViewModelCommands)).ToList());
            }
        }

        public Guid Id
        {
            get
            {
                return this.query.Id;
            }
        }

        public ObservableCollection<QueryTreeViewModel> Children
        {
            get
            {
                return this.children;
            }
        }

        public QueryTreeViewModelCommands Commands 
        {
            get
            {
                return this.queryTreeViewModelCommands;
            }
        }

        public bool IsUserQuery
        {
            get
            {
                return this.query.IsUserQuery;
            }
        }

        public bool IsTopLevelUserQuery
        {
            get
            {
                return this.query.IsTopLevelUserQuery;
            }
        }

        public string Name
        {
            get
            {
                return this.query.Name;
            }
        }

        public bool Selected
        {
            get
            {
                return this.selected;
            }

            set
            {
                this.selected = value;
                this.OnPropertyChanged();
            }
        }

        public string RepositorySlug
        {
            get
            {
                return this.query.RepositorySlug;
            }
        }

        public Query Query
        {
            get
            {
                return this.query;
            }
        }
    }
}
