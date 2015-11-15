using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIFoundation;

namespace Chess.App.ViewModels
{
    public class StartupViewModel : BaseViewModel
    {
        private IMainViewModel mainVm;

        public ICommand NewGameCommand => new Command(o=>this.mainVm.Navigate(new GameViewModel(this.mainVm)));

        public StartupViewModel(IMainViewModel mainVm)
        {
            this.mainVm = mainVm;
        }
    }
}
