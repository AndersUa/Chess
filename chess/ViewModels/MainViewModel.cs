using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIFoundation;

namespace chess.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public IEnumerable<Tuple<int, int>> items;
        public IEnumerable<Tuple<int, int>> Items
        {
            get { return this.items; }
            private set { this.SetProperty(ref this.items, value); }
        }


        public ICommand Command { get; private set; }
        private Random r = new Random();

        private string state;

        public string State
        {
            get { return state; }
            set { this.SetProperty(ref this.state, value); }
        }
        

        public MainViewModel()
        {
            this.State = "State1";
            this.Command = new Command((o) => this.State = this.State == "State1" ? "State2" : "State1");
            this.Items = new List<Tuple<int, int>>() { new Tuple<int, int>(1, 1), new Tuple<int, int>(2, 2), new Tuple<int, int>(3, 3) };
        }
    }
}
