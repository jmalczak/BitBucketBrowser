namespace BitBucketBrowser.Common.Dto
{
    using System;
    using System.Collections.Generic;

    public class Configuration
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public List<Query> UserQueries { get; set; } 
    }
}
