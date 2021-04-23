using System;
using System.Windows.Input;

namespace DbWpfApp.Infrastructure.Commands.Base
{
    internal abstract class Command : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public abstract bool CanExecute(object parameter); //абстракция для того что бы метод наследник который будет вызывать, будет сам реаилзовывать возвращаемый тип


        public abstract void Execute(object parameter);

    }
}
