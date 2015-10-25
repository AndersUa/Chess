using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UIFoundation
{
    public class BindableBase : INotifyPropertyChanged
    {
        protected void SetProperty<T>(ref T container, T value, [CallerMemberName]string memberName = null)
        {
            container = value;
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }

        protected void OnPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
