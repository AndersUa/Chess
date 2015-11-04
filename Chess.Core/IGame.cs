using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core
{
    public interface IGame
    {
        IFigure[] GetFigures();
        Move[] GetMoves();
        bool Load(string file);
        void Start();


        event Action<IFigure, IFigure, Move> Move;
        event Action<FigureColor> Turn;
    }
}
