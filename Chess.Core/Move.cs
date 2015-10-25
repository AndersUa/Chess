using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core
{
    public struct Move
    {
        Point from;
        Point to;

        public Point From => this.from;
        public Point To => this.to;

        public Move(Point from, Point to)
        {
            this.from = from;
            this.to = to;
        }
    }
}
