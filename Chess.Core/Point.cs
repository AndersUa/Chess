using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Core
{
    public struct Point
    {
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public static Point Create(int x, int y)
        {
            return new Point(x, y);
        }

        public override bool Equals(object obj)
        {
            var p = obj as Point?;
            return p.HasValue ? this.X == p.Value.X && this.Y == p.Value.Y : false;
        }

        public static bool operator ==(Point p1, Point p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Point p1, Point p2)
        {
            return !p1.Equals(p2);
        }
    }

    public static class PointExt
    {
        /// <summary>
        /// Gets points that X>=0 and Y>=0 and X<range and Y<range
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static IEnumerable<Point> ValidateRange(this IEnumerable<Point> points, int maxBoundary)
        {
            return points.Where(p => p.IsInRange(maxBoundary));
        }

        public static bool IsInRange(this Point p, int maxBoundary)
        {
            return p.X >= 0 && p.Y > 0 && p.X < maxBoundary && p.Y < maxBoundary;
        }
    }

}
