using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core
{
    public interface IFigure
    {
        bool CanMove(Point p);
        void Move(Point p);
        Point[] GetPossibleMoves();
        FigureType Type { get; }
        FigureColor Color { get; }
        Point Point { get; }
    }
}
