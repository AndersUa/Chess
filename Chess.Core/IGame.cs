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


        event Action<IFigure, IFigure, Move, bool> Move;
        event Action<FigureColor, bool> Turn;
        event Action<FigureColor> Chackmate;
    }
}
