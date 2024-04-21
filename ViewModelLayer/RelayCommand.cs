using System;
using System.Windows.Input;

namespace ViewModelLayer
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> m_Execute;
        private readonly Func<object, bool> m_CanExecute;

        public RelayCommand(Action<object> execute) : this(execute, null) { }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.m_execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.m_canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (this.m_CanExecute == null)
                return true;

            return this.m_CanExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.m_Execute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        internal void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

}