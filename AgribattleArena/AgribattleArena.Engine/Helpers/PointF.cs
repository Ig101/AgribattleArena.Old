using System;
using System.Collections.Generic;
using System.Text;

namespace AgribattleArena.Engine.Helpers
{
    public struct PointF
    {
        float x;
        float y;

        public float X { get { return x; } }
        public float Y { get { return y; } }

        public static PointF operator * (PointF point, float multiplier)
        {
            return new PointF(point.X * multiplier, point.Y * multiplier);
        }

        public static PointF operator / (PointF point, float multiplier)
        {
            return new PointF(point.X / multiplier, point.Y / multiplier);
        }

        public static PointF operator + (PointF point1, PointF point2)
        {
            return new PointF(point1.X + point2.X, point1.Y + point2.Y);
        }

        public static PointF operator - (PointF point1, PointF point2)
        {
            return new PointF(point1.X - point2.X, point1.Y - point2.Y);
        }

        public PointF (float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
