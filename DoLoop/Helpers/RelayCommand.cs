﻿using System;
using System.Windows.Input;

namespace DoLoop.Helpers
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _exec;
        private readonly Func<object, bool> _can;
        public RelayCommand(Action<object> exec, Func<object, bool> can = null)
        {
            _exec = exec;
            _can = can;
        }
        public bool CanExecute(object parameter) => _can == null || _can(parameter);
        public void Execute(object parameter) => _exec(parameter);
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
    }
}
