using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UIFoundation
{
    public class Command : ICommand
    {
        Action<object> action = null;
        Func<object, bool> canExecute = null;
        public Command(Action<object> action, Func<object, bool> canExecute = null)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            this.action = action;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            if (this.canExecute != null)
            {
                return this.canExecute(parameter);
            }
            else
            {
                return true;
            }
        }

        public void Execute(object parameter)
        {
            this.action(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
