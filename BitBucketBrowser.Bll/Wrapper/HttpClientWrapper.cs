namespace BitBucketBrowser.Bll.Wrapper
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    using BitBucketBrowser.Bll.Wrapper.Interfaces;
    using BitBucketBrowser.Common.Service.Interfaces;

    [Annotations.UsedImplicitly]
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly IUserService userService;

        private readonly string baseAddress;

        public HttpClientWrapper(IUserService userService, string baseAddress)
        {
            this.userService = userService;
            this.baseAddress = baseAddress;
        }

        public async Task<dynamic> GetAsync(string method)
        {
            var client = this.GetClient();
            var response = await client.GetAsync(method);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<dynamic>();
        }

        public async Task<dynamic> GetAsync(string method, string login, string password)
        {
            var client = this.GetClient(login, password);
            var response = await client.GetAsync(method);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<dynamic>();
        }

        private HttpClient GetClient(string login, string password)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", login, password))));
            client.BaseAddress = new Uri(this.baseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private HttpClient GetClient()
        {
            return this.GetClient(this.GetUserLogin(), this.GetUserPassword());
        }

        private string GetUserLogin()
        {
            if (!this.userService.IsAuthenticated())
            {
                throw new Exception("User is not authenticated");
            }

            return this.userService.GetCurrentUser().Login;
        }

        private string GetUserPassword()
        {
            if (!this.userService.IsAuthenticated())
            {
                throw new Exception("User is not authenticated");
            }

            return this.userService.GetCurrentUser().Password;
        }        
    }
}
