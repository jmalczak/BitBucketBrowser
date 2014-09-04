namespace BitBucketBrowser.View.Main
{
    using System;
    using System.Windows.Input;

    using BitBucketBrowser.Bll.Presentation.ViewModel.Interfaces;
    using BitBucketBrowser.Common.Dto;
    using BitBucketBrowser.View.Main.Interfaces;

    /// <summary>
    /// Interaction logic for AddEditQueryWindow.xaml
    /// </summary>
    public partial class AddEditQueryWindow : IAddEditQueryWindowView
    {
        private readonly Func<Query, Action, IAddEditQueryViewModel> viewModelFactory;

        public AddEditQueryWindow(Func<Query, Action, IAddEditQueryViewModel> viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
            InitializeComponent();
        }        

        public void ShowAddDialog(Query queryToAdd, Action successCallback)
        {
            this.InitializeViewModels(this.viewModelFactory(queryToAdd, successCallback));
            this.ShowDialog();            
        }

        public void ShowEditDialog(Query queryToEdit, Action successCallback)
        {
            this.InitializeViewModels(this.viewModelFactory(queryToEdit, successCallback));
            this.ShowDialog();
        }

        private void InitializeViewModels(IAddEditQueryViewModel viewModel)
        {
            this.DataContext = viewModel;
            viewModel.ViewClose += this.Close;
        }
    }
}
