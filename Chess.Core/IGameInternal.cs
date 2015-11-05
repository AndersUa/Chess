using Chess.Core.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core
{
    internal interface IGameInternal
    {
        IFigure[,] Board { get; }
        void MakeStep(IFigure figure, Point from, Point t);
        IEnumerable<Point> FilterPossibleMoveForCheck(IEnumerable<Point> point, BaseFigure piece);
        bool IsCheck(FigureColor c);

    }
}
