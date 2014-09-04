namespace BitBucketBrowser.View.Main.Interfaces
{
    using System;

    using BitBucketBrowser.Common.Dto;

    public interface IAddEditQueryWindowView : IView
    {
        void ShowAddDialog(Query queryToAdd, Action successCallback);

        void ShowEditDialog(Query queryToEdit, Action successCallback);
    }
}