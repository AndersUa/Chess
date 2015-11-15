using Chess.App.Models;
using Chess.Core;
using Chess.Core.EventArgs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIFoundation;

namespace Chess.App.ViewModels
{
    public enum States
    {
        State1,
        State2
    }

    public class ChessPiece : BaseViewModel
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

    public class GameViewModel : BaseViewModel
    {
        public GameViewModel(IMainViewModel mainVm)
            : this()
        {
            this.mainVm = mainVm;
        }

        public ICollection<FigureModel> chessPieces;
        public ICollection<FigureModel> ChessPieces
        {
            get { return this.chessPieces; }
            set { this.SetProperty(ref this.chessPieces, value); }
        }

        public IEnumerable<Tuple<int, int>> possibleMoves;
        public IEnumerable<Tuple<int, int>> PossibleMoves
        {
            get { return this.possibleMoves; }
            set { this.SetProperty(ref this.possibleMoves, value); }
        }

        private FigureModel activePiece = null;

        public ICommand MoveStep1Command { get; private set; }
        public ICommand MoveStep2Command { get; private set; }
        public ICommand DisableStep2Command { get; private set; }

        private States state = States.State1;
        private IGame game;

        private ICollection<FigureModel> fallenFigures = new ObservableCollection<FigureModel>();
        private IMainViewModel mainVm;

        public ICollection<FigureModel> FallenFigures => this.fallenFigures;

        public IEnumerable BlackFallenFigures => this.fallenFigures.ObservableWhere(f => f.Color == FigureColor.Black);
        public IEnumerable WhiteFallenFigures => this.fallenFigures.ObservableWhere(f => f.Color == FigureColor.White);

        public States State
        {
            get { return state; }
            set { this.SetProperty(ref this.state, value); }
        }


        public GameViewModel()
        {
            this.State = States.State1;
            this.MoveStep1Command = new Command(MoveStep1);
            this.MoveStep2Command = new Command(MoveStep2);
            this.DisableStep2Command = new Command((o) => this.State = States.State1);

            this.game = new GameFactory().CreateGame();
            this.game.Turn += Game_Turn;
            this.game.Move += Game_Move;
            this.game.Chackmate += Game_Chackmate;

            this.ChessPieces = new ObservableCollection<FigureModel>(game.GetFigures().Select(f => new FigureModel(f)).ToArray());

            this.game.Start();
        }

        private void Game_Chackmate(object sender, ChackmateEventArgs args)
        {
            System.Windows.MessageBox.Show($"{args.WinnerColor.ToString()} is win!");
            this.game.Turn -= Game_Turn;
            this.game.Move -= Game_Move;
            this.game.Chackmate -= Game_Chackmate;
            this.game = new GameFactory().CreateGame();
            this.game.Turn += Game_Turn;
            this.game.Move += Game_Move;
            this.game.Chackmate += Game_Chackmate;

            this.ChessPieces = new ObservableCollection<FigureModel>(game.GetFigures().Select(f => new FigureModel(f)).ToArray());

            this.game.Start();
        }

        private void Game_Move(object sender, MoveEventArgs args)
        {
            if (args.FallenFigure != null)
            {
                this.ChessPieces.Remove(this.ChessPieces.First(p => p.Origin == args.FallenFigure));
                this.fallenFigures.Add(new FigureModel(args.FallenFigure));
            }
            foreach (var figure in args.AffectedFigures)
            {
                this.ChessPieces.First(p => p.Origin == figure).Update();
            }
        }

        private void Game_Turn(object sender, TurnEventArgs args)
        {
            foreach (var figure in chessPieces)
            {
                figure.IsActive = figure.Color == args.Color;
            }
        }

        private void MoveStep1(object p)
        {
            var val = p as FigureModel;
            if (val != null)
            {
                var moves = val.Origin.GetPossibleMoves().Select(t => Tuple.Create(t.X, t.Y));
                if (moves.Count() > 0)
                {
                    this.PossibleMoves = moves;
                    this.activePiece = val;
                    this.State = States.State2;
                }
            }

        }

        private void MoveStep2(object p)
        {
            var val = p as Tuple<int, int>;
            if (val != null && this.activePiece != null)
            {
                this.activePiece.Origin.Move(new Point(val.Item1, val.Item2));
                this.State = States.State1;
            }
        }
    }
}
