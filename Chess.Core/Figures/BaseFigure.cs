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

        protected void Init(Point point, FigureColor color)
        {
            this.Point = point;
            this.Color = color;
        }

        public abstract FigureType Type { get; }

        public abstract bool CanMove(Point p);

        public abstract Point[] GetPossibleMoves();

        public abstract void Move(Point p);

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
