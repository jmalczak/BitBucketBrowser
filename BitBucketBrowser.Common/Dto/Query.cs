namespace BitBucketBrowser.Common.Dto
{
    using System;
    using System.Collections.Generic;

    public class Query
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public string RepositorySlug { get; set; }

        public bool IsUserQuery { get; set; }

        public bool IsTopLevelUserQuery { get; set; }

        public List<Query> Children { get; set; }
    }
}
