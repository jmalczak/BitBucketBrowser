namespace BitBucketBrowser.Common.Dto.BitBucket
{
    using System;

    public class Issue
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public string CreatedAt
        {
            get
            {
                return this.CreatedAtUtc.ToLocalTime().ToShortDateString();
            }
        }

        public DateTime LastUpdatedAtUtc { get; set; }

        public string LastUpdatedAt
        {
            get
            {
                return this.LastUpdatedAtUtc.ToLocalTime().ToShortDateString();
            }
        }

        public string Status { get; set; }

        public string Content { get; set; }
    }
}
