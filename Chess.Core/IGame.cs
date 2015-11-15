using Chess.Core.EventArgs;
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
        void Start();

        
        event EventHandler<MoveEventArgs> Move;
        event EventHandler<TurnEventArgs> Turn;
        event EventHandler<ChackmateEventArgs> Chackmate;
    }
}
