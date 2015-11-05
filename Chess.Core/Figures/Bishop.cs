using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Figures
{
    public class Bishop : BaseFigure
    {
        internal Bishop(IGameInternal game, Point point, FigureColor color)
        {
            base.Init(point, color, game);
        }

        public override FigureType Type => FigureType.Bishop;

        public override Point[] OnGetPossibleMoves()
        {
            var res = Enumerable.Empty<Point>();
            res = res.Concat(base.CreatePath(base.Point, (p) => Point.Create(p.X + 1, p.Y + 1), base.Board));
            res = res.Concat(base.CreatePath(base.Point, (p) => Point.Create(p.X + 1, p.Y - 1), base.Board));
            res = res.Concat(base.CreatePath(base.Point, (p) => Point.Create(p.X - 1, p.Y + 1), base.Board));
            res = res.Concat(base.CreatePath(base.Point, (p) => Point.Create(p.X - 1, p.Y - 1), base.Board));
            return res.ToArray();
        }
    }
}
