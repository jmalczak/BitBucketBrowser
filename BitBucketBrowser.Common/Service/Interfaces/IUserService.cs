namespace BitBucketBrowser.Common.Service.Interfaces
{
    using BitBucketBrowser.Common.Dto.BitBucket;

    public interface IUserService
    {
        void LogInUser(User user);

        void LogOutUser();

        User GetCurrentUser();

        bool IsAuthenticated();
    }
}