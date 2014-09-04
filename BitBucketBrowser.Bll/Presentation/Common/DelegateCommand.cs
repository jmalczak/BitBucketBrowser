namespace BitBucketBrowser.Bll.Presentation.Common
{
    using System;
    using System.Windows.Input;

    public class DelegateCommand : ICommand
    {
        private readonly Action execute;

        public DelegateCommand(Action execute)
        {
            this.execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.execute();
        }

        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}