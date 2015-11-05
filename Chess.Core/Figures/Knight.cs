using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Figures
{
    public class Knight : BaseFigure
    {
        internal Knight(IGameInternal game, Point point, FigureColor color)
        {
            base.Init(point, color, game);
        }

        public override FigureType Type => FigureType.Knight;

        public override Point[] OnGetPossibleMoves()
        {
            List<Point> res = new List<Point>();
            res.Add(Point.Create(base.Point.X + 2, base.Point.Y + 1));
            res.Add(Point.Create(base.Point.X + 2, base.Point.Y - 1));
            res.Add(Point.Create(base.Point.X - 2, base.Point.Y + 1));
            res.Add(Point.Create(base.Point.X - 2, base.Point.Y - 1));

            res.Add(Point.Create(base.Point.X + 1, base.Point.Y + 2));
            res.Add(Point.Create(base.Point.X - 1, base.Point.Y + 2));
            res.Add(Point.Create(base.Point.X + 1, base.Point.Y - 2));
            res.Add(Point.Create(base.Point.X - 1, base.Point.Y - 2));

            return res.ValidateRange(8)
                      .Where((p) => base.Board[p.X, p.Y] == null || base.Board[p.X, p.Y].Color != base.Color)
                      .ToArray();
        }
    }
}
