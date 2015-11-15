using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core.EventArgs
{
    public class MoveEventArgs : System.EventArgs
    {
        public IEnumerable<IFigure> AffectedFigures { get; internal set; }
        public IFigure FallenFigure { get; internal set; }
        public Move Move { get; internal set; }
        public bool IsChack { get; internal set; }
    }
}
