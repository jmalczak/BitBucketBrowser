namespace BitBucketBrowser.Bll.Presentation.Common
{
    using BitBucketBrowser.Bll.Annotations;
    using BitBucketBrowser.Bll.Wrapper.Interfaces;

    [UsedImplicitly]
    public class Delegates
    {
        public delegate IHttpClientWrapper HttpClientWrapperFactory(string baseAddress);
    }
}