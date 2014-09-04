namespace BitBucketBrowser.Bll.Presentation.ViewModel
{
    using System.Windows.Input;

    public class QueryTreeViewModelCommands
    {
        public ICommand DisplayIssuesCommand { get; set; }

        public ICommand AddNewQueryCommand { get; set; }

        public ICommand EditQueryCommand { get; set; }

        public ICommand DeleteQueryCommand { get; set; }
    }
}