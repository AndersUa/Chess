using Chess.Core.Figures;
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

        public event Action<IFigure, IFigure, Move, bool> Move;
        public event Action<FigureColor, bool> Turn;
        public event Action<FigureColor> Chackmate;

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
            IFigure fallen = null;
            this.Board[from.X, from.Y] = null;
            if (this.Board[to.X, to.Y] != null)
            {
                fallen = this.Board[to.X, to.Y];
            }
            this.Board[to.X, to.Y] = figure;
            var isCheck = this.IsCheck(figure.Color == FigureColor.Black ? FigureColor.White : FigureColor.Black);
            this.Move?.Invoke(figure, fallen, new Move(from, to), isCheck);
            if (isCheck && this.IsMate(figure.Color == FigureColor.Black ? FigureColor.White : FigureColor.Black))
                this.Chackmate?.Invoke(figure.Color);
            else
                this.Turn?.Invoke(figure.Color == FigureColor.Black ? FigureColor.White : FigureColor.Black, isCheck);
        }

        public void Start()
        {
            this.Turn?.Invoke(FigureColor.White, false);
        }

        public IEnumerable<Point> FilterPossibleMoveForCheck(IEnumerable<Point> points, BaseFigure piece)
        {
            return points.Where(p =>
            {
                var tmp1 = piece.Point;
                var tmp2 = this.Board[p.X,p.Y];
                this.Board[p.X, p.Y] = piece;
                this.Board[piece.Point.X, piece.Point.Y] = null;
                piece.Point = p;
                var res = this.IsCheck(piece.Color);
                piece.Point = tmp1;
                this.Board[tmp1.X, tmp1.Y] = piece;
                if (tmp2 != null)
                    this.Board[tmp2.Point.X, tmp2.Point.Y] = tmp2;
                else
                    this.Board[p.X, p.Y] = null;
                return !res;
            });
        }

        public bool IsCheck(FigureColor c)
        {
            var king = Enumerable.Range(0, 8).SelectMany(x => Enumerable.Range(0, 8).Select(y => this.Board[x, y]).Where(f => f != null)).First(f => f.Type == FigureType.King && f.Color == c);
            var possible = Enumerable.Range(0, 8).SelectMany(x => Enumerable.Range(0, 8).Select(y => this.Board[x, y]).Where(f => f != null && f.Color != c))
                                                           .SelectMany(f => f.Type != FigureType.Pawn ? f.GetPossibleMoves(false) : Figures.Pawn.GetAttacPoints(f.Point, f.Color)).ToArray();
            return possible.Any(p => p == king.Point);
        }

        public bool IsMate(FigureColor color)
        {
            var possible = Enumerable.Range(0, 8).SelectMany(x => Enumerable.Range(0, 8).Select(y => this.Board[x, y]).Where(f => f != null && f.Color == color))
                                               .SelectMany(f => f.GetPossibleMoves()).ToArray();
            return possible.Count() == 0;
        }

        public Game()
        {
            //black setup
            //pawns
            for (int i = 0; i < 8; i++)
            {
                this.board[i, 1] = new Figures.Pawn(this, new Point(i, 1), FigureColor.Black);
            }
            //rooks
            this.board[0, 0] = new Figures.Rook(this, new Point(0, 0), FigureColor.Black);
            this.board[7, 0] = new Figures.Rook(this, new Point(7, 0), FigureColor.Black);
            //bishops
            this.board[2, 0] = new Figures.Bishop(this, new Point(2, 0), FigureColor.Black);
            this.board[5, 0] = new Figures.Bishop(this, new Point(5, 0), FigureColor.Black);
            //queen
            this.board[3, 0] = new Figures.Queen(this, new Point(3, 0), FigureColor.Black);
            //knights
            this.board[1, 0] = new Figures.Knight(this, new Point(1, 0), FigureColor.Black);
            this.board[6, 0] = new Figures.Knight(this, new Point(6, 0), FigureColor.Black);
            //king
            this.board[4, 0] = new Figures.King(this, new Point(4, 0), FigureColor.Black);

            //white setup
            //pawns
            for (int i = 0; i < 8; i++)
            {
                this.board[i, 6] = new Figures.Pawn(this, new Point(i, 6), FigureColor.White);
            }
            //rooks
            this.board[0, 7] = new Figures.Rook(this, new Point(0, 7), FigureColor.White);
            this.board[7, 7] = new Figures.Rook(this, new Point(7, 7), FigureColor.White);
            //bishops
            this.board[2, 7] = new Figures.Bishop(this, new Point(2, 7), FigureColor.White);
            this.board[5, 7] = new Figures.Bishop(this, new Point(5, 7), FigureColor.White);
            //queen
            this.board[3, 7] = new Figures.Queen(this, new Point(3, 7), FigureColor.White);
            //knights
            this.board[1, 7] = new Figures.Knight(this, new Point(1, 7), FigureColor.White);
            this.board[6, 7] = new Figures.Knight(this, new Point(6, 7), FigureColor.White);
            //king
            this.board[4, 7] = new Figures.King(this, new Point(4, 7), FigureColor.White);

        }
    }
}
