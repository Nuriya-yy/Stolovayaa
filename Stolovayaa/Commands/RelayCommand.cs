using System;
using System.Windows.Input;

namespace Stolovayaa.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;      // 1) Что делать при нажатии?
        private readonly Predicate<object> _canExecute; // 2) Можно ли нажать?

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) // 3) WPF вызывает: "Кнопка активна?"
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)   // 4) WPF вызывает: "Кнопку нажали!"
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;    // 5) Кнопка подписалась → метод в общий список
            remove => CommandManager.RequerySuggested -= value;   // 6) Кнопка отписалась → метод удалить
        }

        public void RaiseCanExecuteChanged() // 7) Ты вызываешь: "Обнови все кнопки!"
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}