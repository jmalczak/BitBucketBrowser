namespace BitBucketBrowser.Bll.Logic.Interfaces
{
    using System;
    using System.Collections.Generic;

    using BitBucketBrowser.Common.Dto;

    public interface IQueryService
    {
        IEnumerable<Query> GetQueryiesByRepositorySlug(string slug);

        void AddUserQuery(Query query);

        void DeleteUserQuery(Guid id);

        void AddOrUpdateQuery(Query query);
    }
}