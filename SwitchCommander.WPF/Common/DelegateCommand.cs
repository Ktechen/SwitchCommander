using System;
using System.Windows.Input;

namespace SwitchCommander.WPF.Common;

public class DelegateCommand : ICommand
{
    private readonly Func<bool> _canExecute;
    private readonly Action _execute;

    public DelegateCommand(Action execute, Func<bool> canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute();
    }

    public void Execute(object? parameter)
    {
        _execute();
    }
}