using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Helpers
{
    public struct Point
    {
        int x;
        int y;

        public int X { get { return x; } }
        public int Y { get { return y; } }

        public static Point operator * (Point point, int multiplier)
        {
            return new Point(point.X * multiplier, point.Y * multiplier);
        }

        public static Point operator / (Point point, int multiplier)
        {
            return new Point(point.X / multiplier, point.Y / multiplier);
        }

        public static Point operator + (Point point1, Point point2)
        {
            return new Point(point1.X + point2.X, point1.Y + point2.Y);
        }

        public static Point operator - (Point point1, Point point2)
        {
            return new Point(point1.X - point2.X, point1.Y - point2.Y);
        }

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
