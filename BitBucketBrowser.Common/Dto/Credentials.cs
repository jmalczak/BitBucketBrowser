namespace BitBucketBrowser.Common.Dto
{
    using BitBucketBrowser.Common.Properties;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Credentials : NotifyPropertyChangedBase
    {
        private string login;

        private string password;

        private bool remember;

        public string Login
        {
            get
            {
                return this.login;
            }

            set
            {
                this.login = value;
                this.OnPropertyChanged();
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                this.password = value;
                this.OnPropertyChanged();
            }
        }

        public bool Remember
        {
            get
            {
                return this.remember;
            }

            set
            {
                this.remember = value;
                this.OnPropertyChanged();
            }
        }
    }
}