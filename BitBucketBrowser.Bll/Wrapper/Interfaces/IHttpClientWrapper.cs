namespace BitBucketBrowser.Bll.Wrapper.Interfaces
{
    using System.Threading.Tasks;

    public interface IHttpClientWrapper
    {
        Task<object> GetAsync(string method);

        Task<object> GetAsync(string method, string login, string password);
    }
}