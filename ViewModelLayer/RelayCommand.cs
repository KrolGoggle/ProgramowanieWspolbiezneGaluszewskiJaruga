using System;
using System.Windows.Input;

namespace ViewModelLayer
{
    public class RelayCommand : ICommand
    {

        private readonly Action<object> m_execute;
        private readonly Func<object, bool> m_canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.m_execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.m_canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (m_canExecute != null)
            {
                return m_canExecute(parameter);
            }
            else return false;
        }

        public virtual void Execute(object parameter)
        {
            m_execute(parameter);
        }

        public event EventHandler CanExecuteChanged;
        internal void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}