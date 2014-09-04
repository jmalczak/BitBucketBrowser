namespace BitBucketBrowser.Bll.Logic.Interfaces
{
    using BitBucketBrowser.Common.Dto;

    public interface ILoginService
    {
        bool CredentialsSaved();

        Credentials GetSavedCredentials();

        void ClearSavedCredentials();

        void SaveCredentials(Credentials credentials);
    }
}