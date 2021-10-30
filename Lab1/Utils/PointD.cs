using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Utils
{
    public struct PointD
    {
        public static readonly PointD Empty = new PointD();
        public double X { get; set; }
        public double Y { get; set; }


        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }
        //public void ToPoint(this PointF point)
        //{
        //    return new Point(Convert.ToInt32(point.X), Convert.ToInt32(point.Y));
        //}
        public static PointD operator +(PointD point, PointD other)
        {
            return new PointD(point.X + other.X, point.Y + other.Y);
        }

        public static PointD operator -(PointD point, PointD other)
        {
            return new PointD(point.X - other.X, point.Y - other.Y);
        }

        public static PointD operator *(PointD point, double value)
        {
            return new PointD(point.X * value, point.Y * value);
        }

        public static PointD operator /(PointD point, double value)
        {
            return new PointD(point.X / value, point.Y / value);
        }
        public static bool operator ==(PointD point, PointD other)
        {
            return point.X == other.X && point.Y == other.Y;
        }

        public static bool operator !=(PointD point, PointD other)
        {
            return point.X != other.X || point.Y != other.Y;
        }
        public static explicit operator Point(PointD point)
        {
            return new Point(Convert.ToInt32(point.X), Convert.ToInt32(point.Y));
        }
        public static implicit operator PointD(Point point)
        {
            return new PointD(point.X, point.Y);
        }
        public void Flip()
        {
            double x = X;
            X = Y;
            Y = x;
        }

        public void Rotate(double angle)
        {
            double x = X, y = Y;
            double cos = Math.Cos(angle), sin = Math.Sin(angle);
            X = x * cos - y * sin;
            Y = x * sin + y * cos;
        }
        public double GetLength()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public double GetAngle()
        {
            return Math.Atan2(Y, X);
        }
        public int GetAngleDegrees()
        {
            return Convert.ToInt32(Math.Atan2(Y, X) / 2 / Math.PI * 359);
        }
    }
}
