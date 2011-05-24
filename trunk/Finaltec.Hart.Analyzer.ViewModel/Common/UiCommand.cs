using System;
using System.Windows.Input;

namespace Finaltec.Hart.Analyzer.ViewModel.Common
{
    /// <summary>
    /// UiCommand class.
    /// </summary>
    public class UiCommand : ICommand
    {
        private readonly Action<object> _objExecuteMethod;
        private readonly Predicate<object> _objCanExecuteMethod;

        /// <summary>
        /// Occurs on changes for the command.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_objCanExecuteMethod != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_objCanExecuteMethod != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// Invokes the can execute changed.
        /// </summary>
        public void InvokeCanExecuteChanged()
        {
            if (_objCanExecuteMethod != null)
                CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UiCommand"/> class.
        /// </summary>
        /// <param name="objExecuteMethod">The obj execute method.</param>
        public UiCommand(Action<object> objExecuteMethod)
            : this(objExecuteMethod, null)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="UiCommand"/> class.
        /// </summary>
        /// <param name="objExecuteMethod">The obj execute method.</param>
        /// <param name="objCanExecuteMethod">The obj can execute method.</param>
        public UiCommand(Action<object> objExecuteMethod, Predicate<object> objCanExecuteMethod)
        {
            if (objExecuteMethod == null)
                throw new ArgumentNullException("objExecuteMethod", "Comamnds can not be null. Please set a valid value.");

            _objExecuteMethod = objExecuteMethod;
            _objCanExecuteMethod = objCanExecuteMethod;
        }

        /// <summary>
        /// Check can execute for the command.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        /// <returns>
        /// true if the command can execute.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            if (_objCanExecuteMethod == null)
                return true;

            return _objCanExecuteMethod(parameter);
        }

        /// <summary>
        /// The method that will call on execute.
        /// </summary>
        /// <param name="parameter">Command parameter.</param>
        public void Execute(object parameter)
        {
            _objExecuteMethod(parameter);
        }
    }
}
