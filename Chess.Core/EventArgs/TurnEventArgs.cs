using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.EventArgs
{
    public class TurnEventArgs : System.EventArgs
    {
        public FigureColor Color { get; internal set; }
        public bool IsCheck { get; internal set; }
    }
}
