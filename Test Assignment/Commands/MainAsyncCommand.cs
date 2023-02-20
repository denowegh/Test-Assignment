using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Assignment.Commands
{
    public class MainAsyncCommand : AsyncCommandBase
    {
        private readonly Func<Task> _command;
        private readonly Func<bool> _canExecute;
        public MainAsyncCommand(Func<Task> command, Func<bool> canExecute = null)
        {
            if(canExecute == null)
            {
                _canExecute = () => true;
            }
            else
            {
                _canExecute = canExecute;
            }
            _command = command;
        }

        public override bool CanExecute(object parameter)
        {
           
             return _canExecute();
            
        }

        public override Task ExecuteAsync(object parameter = null)
        {
            return _command();
        }
    }
}
