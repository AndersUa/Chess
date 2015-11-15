using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIFoundation;

namespace Chess.App.ViewModels
{
    public class MainViewModel : BindableBase, IMainViewModel
    {
        private BaseViewModel currnt;

        public BaseViewModel Current
        {
            get { return this.currnt; }
            set { this.SetProperty(ref this.currnt, value); }
        }

        public MainViewModel()
        {
            this.currnt = new StartupViewModel(this);
        }

        public void Navigate(BaseViewModel vm)
        {
            if (vm != null)
            {
                this.Current = vm;
            }
        }
    }
}
