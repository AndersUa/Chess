using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Figures
{
    public class Queen : BaseFigure
    {
        internal Queen(IGameInternal game, Point point, FigureColor color)
        {
            base.Init(point, color, game);
        }

        public override FigureType Type => FigureType.Queen;

        public override Point[] OnGetPossibleMoves()
        {
            var res = Enumerable.Empty<Point>();
            res = res.Concat(base.CreatePath(base.Point, (p) => Point.Create(p.X, p.Y + 1), base.Board));
            res = res.Concat(base.CreatePath(base.Point, (p) => Point.Create(p.X, p.Y - 1), base.Board));
            res = res.Concat(base.CreatePath(base.Point, (p) => Point.Create(p.X + 1, p.Y), base.Board));
            res = res.Concat(base.CreatePath(base.Point, (p) => Point.Create(p.X - 1, p.Y), base.Board));

            res = res.Concat(base.CreatePath(base.Point, (p) => Point.Create(p.X + 1, p.Y + 1), base.Board));
            res = res.Concat(base.CreatePath(base.Point, (p) => Point.Create(p.X + 1, p.Y - 1), base.Board));
            res = res.Concat(base.CreatePath(base.Point, (p) => Point.Create(p.X - 1, p.Y + 1), base.Board));
            res = res.Concat(base.CreatePath(base.Point, (p) => Point.Create(p.X - 1, p.Y - 1), base.Board));
            return res.ToArray();
        }
    }
}
