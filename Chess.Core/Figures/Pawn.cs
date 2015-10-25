using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Figures
{
    public class Pawn : IFigure
    {
        internal Pawn(IGameInternal game, Point point, FigureColor color)
        {
            this.g = game;
            this.point = point;
            this.color = color;
        }

        private FigureColor color;
        private IGameInternal g;
        private Point point;

        public FigureColor Color => this.color;

        public FigureType Type => FigureType.Pawn;

        public Point Point => this.point;


        public bool CanMove(Point p)
        {
            return this.GetPossibleMoves().Any(m => m == p);
        }

        public Point[] GetPossibleMoves()
        {
            var direction = this.color == FigureColor.Black ? 1 : -1;
            List<Point> moves = new List<Point>(2);
            for (int i = 1; i < 3; i++)
            {
                if (this.g.Board[this.Point.X, this.Point.Y + i * direction] != null)
                {
                    break;
                }
                moves.Add(new Point(this.Point.X, this.Point.Y + i * direction));
            }

            var attack = new[] { new Point(this.point.X - 1, this.point.Y + 1 * direction), new Point(this.point.X + 1, this.point.Y + 1 * direction) }
                            .ValidateRange(8)
                            .Where(p => this.g.Board[p.X, p.Y]== null? false: this.g.Board[p.X, p.Y].Color != this.Color);

            return moves.Concat(attack).ToArray();
                
        }

        public void Move(Point p)
        {
            if (!this.CanMove(p))
                throw new InvalidOperationException("wrong move");
            var tmp = this.point;
            this.point = p;
            this.g.MakeStep(this, tmp, p);
        }
    }
}
