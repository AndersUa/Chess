using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Figures
{
    public class Pawn : BaseFigure
    {
        internal Pawn(IGameInternal game, Point point, FigureColor color)
        {
            this.g = game;
            base.Init(point, color);
        }

        private IGameInternal g;

        public override FigureType Type => FigureType.Pawn;

        public override bool CanMove(Point p)
        {
            return this.GetPossibleMoves().Any(m => m == p);
        }

        public override Point[] GetPossibleMoves()
        {
            var direction = base.Color == FigureColor.Black ? 1 : -1;
            List<Point> moves = new List<Point>(2);
            for (int i = 1; i < 3; i++)
            {
                if (this.g.Board[this.Point.X, this.Point.Y + i * direction] != null)
                {
                    break;
                }
                moves.Add(new Point(this.Point.X, this.Point.Y + i * direction));
            }

            var attack = new[] { new Point(base.Point.X - 1, base.Point.Y + 1 * direction), new Point(base.Point.X + 1, base.Point.Y + 1 * direction) }
                            .ValidateRange(8)
                            .Where(p => this.g.Board[p.X, p.Y]== null? false: this.g.Board[p.X, p.Y].Color != this.Color);

            return moves.Concat(attack).ToArray();
                
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
