using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.App.ViewModels
{
    public interface IMainViewModel
    {
        void Navigate(BaseViewModel vm);
    }
}
