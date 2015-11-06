using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.Figures
{
    public class King : BaseFigure
    {
        public override FigureType Type => FigureType.King;

        internal King(IGameInternal game, Point point, FigureColor color)
        {
            base.Init(point, color, game);
        }

        public override Point[] OnGetPossibleMoves()
        {
            var tmp = new[] { -1, 0, 1 };
            var moves = tmp.SelectMany(x => tmp.Select(y => Point.Create(x, y)))
               .Where(p => p != Point.Create(0, 0))
               .Select(p => this.Point + p)
               .ValidateRange(8)
               .Where(p => base.Board[p.X, p.Y] == null || base.Board[p.X, p.Y].Color != this.Color);
            return moves.ToArray();
        }
    }
}
