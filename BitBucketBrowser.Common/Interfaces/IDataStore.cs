namespace BitBucketBrowser.Common.Interfaces
{
    using Raven.Client;

    public interface IDataStore
    {
        IDocumentSession OpenSession();
    }
}