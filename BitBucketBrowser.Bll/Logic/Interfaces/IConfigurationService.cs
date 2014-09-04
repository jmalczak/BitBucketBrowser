namespace BitBucketBrowser.Bll.Logic.Interfaces
{
    using BitBucketBrowser.Common.Dto;

    public interface IConfigurationService
    {
        Configuration GetConfiguration();

        bool HasSavedConfiguration();

        void SaveConfiguration(Configuration configuration);
    }
}