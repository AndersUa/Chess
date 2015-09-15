using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfStuff
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected void SetProperty<T>(ref T container, T value, [CallerMemberName]string memberName = null)
        {
            container = value;
            var ev = this.PropertyChanged;
            if (ev != null)
            {
                ev(this, new PropertyChangedEventArgs(memberName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
