using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OpenUniversity.Commands
{
    class CommandHandler : ICommand
    {
        private Action _task;
        public event EventHandler CanExecuteChanged;
        public CommandHandler(Action task)
        {
            _task = task;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _task();
        }
    }
}
