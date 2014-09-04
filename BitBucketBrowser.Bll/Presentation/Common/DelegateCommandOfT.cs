namespace BitBucketBrowser.Bll.Presentation.Common
{
    using System;
    using System.Windows.Input;

    public class DelegateCommandOfT<T> : ICommand
    {
        private readonly Predicate<T> canExecute;

        private readonly Action<T> execute;

        public DelegateCommandOfT(Action<T> execute)
        {
            this.execute = execute;            
        }

        public DelegateCommandOfT(Action<T> execute, Predicate<T> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }

            return this.canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            this.execute((T)parameter);
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
