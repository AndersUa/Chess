using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core
{
    internal class Game : IGame, IGameInternal
    {
        private IFigure[,] board = new IFigure[8, 8];

        public IFigure[,] Board => this.board;

        public event Action<IFigure, Move> Move;
        public event Action<FigureColor> Turn;

        public IFigure[] GetFigures()
        {
            return Enumerable.Range(0, 8)
                    .SelectMany(i => Enumerable.Range(0, 8).Where(j => this.Board[i, j] != null).Select(j => this.Board[i, j]))
                    .ToArray();
        }

        public Move[] GetMoves()
        {
            throw new NotImplementedException();
        }

        public bool Load(string file)
        {
            throw new NotImplementedException();
        }

        public void MakeStep(IFigure figure, Point from, Point to)
        {
            this.Board[from.X, from.Y] = null;
            this.Board[to.X, to.Y] = figure;
            this.Move?.Invoke(figure, new Move(from, to));
            this.Turn?.Invoke(figure.Color == FigureColor.Black ? FigureColor.White : FigureColor.Black);
        }

        public void Start()
        {
            this.Turn?.Invoke(FigureColor.Black);
        }

        public Game()
        {
            for (int i = 0; i < 8; i++)
            {
                this.board[i, 1] = new Figures.Pawn(this, new Point(i, 1), FigureColor.Black);
                this.board[i, 6] = new Figures.Pawn(this, new Point(i, 6), FigureColor.White);
            }
        }
    }
}
