using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WpfApp1.ViewModels
{
    public class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<T> execute;
        private Predicate<T> canExecute;

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            execute((T)parameter);
        }

        public RelayCommand(Action<T> action)
        {
            execute = action;
        }

        public RelayCommand(Action<T> action, Predicate<T> predicate)
        {
            execute = action;
            canExecute = predicate;
        }
    }
}
