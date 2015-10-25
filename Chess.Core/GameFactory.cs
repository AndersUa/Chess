using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core
{
    public class GameFactory
    {
        public IGame CreateGame()
        {
            return new Game();
        }
    }
}
