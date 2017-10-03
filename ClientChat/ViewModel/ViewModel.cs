using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace ClientChat.ViewModel
{
    public class VMInterface : INotifyPropertyChanged, ICommand
    {
        public VMInterface()
        {

        }

        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public VMInterface(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            _execute = Execute;
            _canExecute = CanExecute;
        }

        public bool CanExecute(object Parameter)
        {
            return _canExecute == null || _canExecute(Parameter);
        }

        public void Execute(object Parameter)
        {
            _execute(Parameter);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
