using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LogbookApp.ViewModel
{
    public class DelegateCommand<T> : ICommand
    {
        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        readonly Func<T, bool> _canExecute;
        readonly Action<T> _executeAction;
        bool _canExecuteCache;

        public DelegateCommand(Action<T> executeAction,
                               Func<T, bool> canExecute)
        {
            this._executeAction = executeAction;
            this._canExecute = canExecute;
        }

        #region ICommand Members

        /// <summary>
        /// Defines the method that determines whether the command
        /// can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command.
        /// If the command does not require data to be passed,
        /// this object can be set to null.
        /// </param>
        /// <returns>
        /// true if this command can be executed; otherwise, false.
        /// </returns>
        public bool CanExecuteEx(T parameter)
        {
            bool tempCanExecute = _canExecute(parameter);

            if (_canExecuteCache != tempCanExecute)
            {
                _canExecuteCache = tempCanExecute;
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, new EventArgs());
                }
            }

            return _canExecuteCache;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the command.
        /// If the command does not require data to be passed,
        /// this object can be set to null.
        /// </param>
        public void ExecuteEx(T parameter)
        {
            _executeAction(parameter);
        }

        #endregion ICommand Members

        public bool CanExecute(object parameter)
        {
            return CanExecuteEx((T)parameter);
        }

        public void Execute(object parameter)
        {
            ExecuteEx((T)parameter);
        }
    }
}
