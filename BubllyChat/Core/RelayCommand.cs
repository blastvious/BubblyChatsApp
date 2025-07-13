using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BubllyChat.Core
{
    public class RelayCommand : ICommand
    {
        private Action<object> execute;

        private Func<object, bool> canExecute;
        private Action<string, string> executeRecoverPasswordCommand;
        private Action toggleMic;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action<string, string> executeRecoverPasswordCommand)
        {
            this.executeRecoverPasswordCommand = executeRecoverPasswordCommand;
        }

        public RelayCommand(Action toggleMic)
        {
            this.toggleMic = toggleMic;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        internal void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
