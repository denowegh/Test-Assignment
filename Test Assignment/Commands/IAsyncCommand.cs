using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Test_Assignment.Commands
{
    public interface IAsyncCommand:ICommand
    {
        Task ExecuteAsync(object parameter = null);
    }
}
