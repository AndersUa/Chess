using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Figures
{
    public class Pawn : BaseFigure
    {
        bool isFistMove = true;

        internal Pawn(IGameInternal game, Point point, FigureColor color)
        {
            base.Init(point, color, game);
        }

        public override FigureType Type => FigureType.Pawn;

        public override Point[] OnGetPossibleMoves()
        {
            var direction = base.Color == FigureColor.Black ? 1 : -1;
            List<Point> moves = new List<Point>(2);
            var n = isFistMove ? 3 : 2;
            for (int i = 1; i < n; i++)
            {
                if (base.Board[this.Point.X, this.Point.Y + i * direction] != null)
                {
                    break;
                }
                moves.Add(new Point(this.Point.X, this.Point.Y + i * direction));
            }

            var attack = GetAttacPoints(this.Point, this.Color)
                            .Where(p => base.Board[p.X, p.Y]== null? false: base.Board[p.X, p.Y].Color != this.Color);

            return moves.Concat(attack).ToArray();
                
        }

        public static Point[] GetAttacPoints(Point p, FigureColor c)
        {
            var direction = c == FigureColor.Black ? 1 : -1;
            return new[] { new Point(p.X - 1, p.Y + 1 * direction), new Point(p.X + 1, p.Y + 1 * direction) }
                            .ValidateRange(8).ToArray();
        }

        public override void Move(Point p)
        {
            base.Move(p);
            this.isFistMove = false;
        }
    }
}
