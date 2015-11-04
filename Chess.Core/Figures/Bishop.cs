using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Figures
{
    public class Bishop : BaseFigure
    {
        private IGameInternal g;

        internal Bishop(IGameInternal game, Point point, FigureColor color)
        {
            this.g = game;
            base.Init(point, color);
        }

        public override FigureType Type => FigureType.Bishop;

        public override bool CanMove(Point p)
        {
            return this.GetPossibleMoves().Any(m => m == p);
        }

        public override Point[] GetPossibleMoves()
        {
            var res = Enumerable.Empty<Point>();
            res = res.Concat(base.CreatePath(base.Point, (p) => Point.Create(p.X + 1, p.Y + 1), this.g.Board));
            res = res.Concat(base.CreatePath(base.Point, (p) => Point.Create(p.X + 1, p.Y - 1), this.g.Board));
            res = res.Concat(base.CreatePath(base.Point, (p) => Point.Create(p.X - 1, p.Y + 1), this.g.Board));
            res = res.Concat(base.CreatePath(base.Point, (p) => Point.Create(p.X - 1, p.Y - 1), this.g.Board));
            return res.ToArray();
        }

        public override void Move(Point p)
        {
            if (!this.CanMove(p))
                throw new InvalidOperationException("wrong move");
            var tmp = base.Point;
            base.Point = p;
            this.g.MakeStep(this, tmp, p);
        }
    }
}
