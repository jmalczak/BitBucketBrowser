namespace BitBucketBrowser.Common.Service
{
    using BitBucketBrowser.Common.Dto.BitBucket;
    using BitBucketBrowser.Common.Properties;
    using BitBucketBrowser.Common.Service.Interfaces;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class UserService : IUserService
    {
        private User loggedInUser;

        public UserService()
        {
            this.loggedInUser = new User();
        }

        public void LogInUser(User user)
        {
            this.loggedInUser = user;
        }

        public void LogOutUser()
        {
            this.loggedInUser = new User();
        }

        public User GetCurrentUser()
        {
            return this.loggedInUser;
        }

        public bool IsAuthenticated()
        {
            return this.loggedInUser.IsAuthenticated;
        }
    }
}
