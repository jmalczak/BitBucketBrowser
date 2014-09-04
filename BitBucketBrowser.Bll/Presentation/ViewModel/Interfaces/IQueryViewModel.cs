namespace BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using BitBucketBrowser.Common.Dto;
    using BitBucketBrowser.Common.Dto.BitBucket;

    public interface IQueryViewModel : IConvrolViewModel
    {
        event Action<Query, Action> EditQuery;

        event Action<Query, Action> AddQuery;

        string CurrentRepositorySlug { get; set; }

        List<Repository> Repositories { get; set; }

        ObservableCollection<QueryTreeViewModel> Queries { get; set; }

        ICommand DisplayIssuesCommand { get; set; }

        bool IsQueryListVisible { get; set; }

        void GetRepositories();
    }
}