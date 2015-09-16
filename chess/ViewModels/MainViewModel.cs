using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIFoundation;

namespace chess.ViewModels
{
    public enum States
    { 
        State1,
        State2
    }

    public class ChessPiece : BindableBase
    {
        public ChessPiece(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        private int x;

        public int X
        {
            get { return x; }
            set { this.SetProperty(ref this.x, value); }
        }

        private int y;

        public int Y
        {
            get { return y; }
            set { this.SetProperty(ref this.y, value); }
        }

        public string PiesType { get; set; }
        
    }

    public class MainViewModel : BindableBase
    {
        public IEnumerable<ChessPiece> chessPieces;
        public IEnumerable<ChessPiece> ChessPieces
        {
            get { return this.chessPieces; }
            set { this.SetProperty(ref this.chessPieces, value); }
        }

        public IEnumerable<Tuple<int,int>> possibleMoves;
        public IEnumerable<Tuple<int, int>> PossibleMoves
        {
            get { return this.possibleMoves; }
            set { this.SetProperty(ref this.possibleMoves, value); }
        }

        public ICommand MoveStep1Command { get; private set; }
        public ICommand MoveStep2Command { get; private set; }

        private States state = States.State1;

        public States State
        {
            get { return state; }
            set { this.SetProperty(ref this.state, value); }
        }
        

        public MainViewModel()
        {
            this.State = States.State1;
            this.MoveStep1Command = new Command(MoveStep1);
            this.MoveStep2Command = new Command(MoveStep2);
            this.ChessPieces = new List<ChessPiece>() { new ChessPiece(2, 2), new ChessPiece(2, 2), new ChessPiece(3, 3) };
        }

        private void MoveStep1(object p)
        {
            var val = p as ChessPiece;
            if (val != null)
            { 
                //possibleMoves = new []{-1,1}.GroupBy((i)=>i).Select()
            }
        }

        private void MoveStep2(object p)
        {

        }
    }
}
