using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Figures
{
    public abstract class BaseFigure : IFigure
    {
        public FigureColor Color { get; internal set; }

        public Point Point { get; internal set; }

        internal IGameInternal Game { get; private set; }

        protected IFigure[,] Board => this.Game.Board;

        internal void Init(Point point, FigureColor color, IGameInternal game)
        {
            this.Point = point;
            this.Color = color;
            this.Game = game;
        }

        public abstract FigureType Type { get; }
        

        public virtual bool CanMove(Point p)
        {
            return this.GetPossibleMoves().Any(m => m == p);
        }

        public abstract Point[] OnGetPossibleMoves();

        public Point[] GetPossibleMoves(bool checkForCheck = true)
        {
            if (checkForCheck)
                return this.Game.FilterPossibleMoveForCheck(this.OnGetPossibleMoves(), this).ToArray();
            else
                return this.OnGetPossibleMoves();
        }

        public virtual void Move(Point p)
        {
            if (!this.CanMove(p))
                throw new InvalidOperationException("wrong move");
            var tmp = this.Point;
            this.Point = p;
            this.Game.MakeStep(this, tmp, p);
        }

        protected IEnumerable<Point> CreatePath(Point start, Func<Point, Point> nextStepFunc, IFigure[,] board)
        {
            var p = nextStepFunc(start);
            while (p.IsInRange(8))
            {
                if (board[p.X, p.Y] != null)
                {
                    if (board[p.X, p.Y].Color != this.Color)
                        yield return p;
                    break;
                }
                yield return p;
                p = nextStepFunc(p);
            }
        }
    }
}
