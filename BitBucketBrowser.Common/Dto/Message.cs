namespace BitBucketBrowser.Common.Dto
{
    public class Message : NotifyPropertyChangedBase
    {
        private string messageText;

        public string MessageText
        {
            get
            {
                return this.messageText;
            }

            set
            {
                this.messageText = value;
                this.OnPropertyChanged();
            }
        }
    }
}
