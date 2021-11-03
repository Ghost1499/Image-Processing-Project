﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Utils
{
    public static class ColorExtensions
    {
        public static Color Sum(this Color color1, Color color2)
        {
            return Color.FromArgb(color1.R + color2.R, color1.G + color2.G, color1.B + color2.B);
        }
        public static byte[] ToByteArray(this Color color)
        {
            return new byte[3] { color.R, color.G, color.B };
        }
    }
    public static class PointExtensions
    {
        public static PointF ToPointF(this Point point)
        {
            return point;
        }


    }
    public static class PointFExtensions
    {
        public static Point ToPoint(this PointF point)
        {
            return new Point(Convert.ToInt32(point.X) , Convert.ToInt32(point.Y) );
        }
        public static PointF Sum(this PointF point, PointF other)
        {
            return new PointF(point.X + other.X, point.Y + other.Y);
        }
        public static PointF Sum(this PointF point, float x, float y)
        {
            return new PointF(point.X + x, point.Y + y);
        }

        public static PointF Substract(this PointF point, PointF other)
        {
            return new PointF(point.X - other.X, point.Y - other.Y);
        }
        public static PointF Substract(this PointF point, float x, float y)
        {
            return new PointF(point.X - x, point.Y - y);
        }
        
        public static PointF Multiply(this PointF point, float value)
        {
            return new PointF(point.X *value, point.Y *value);
        }
        public static PointF Flip(this PointF point)
        {
            return new PointF(point.Y, point.X);
        }

        public static PointF Rotate(this PointF point,double angle)
        {
            float x = point.X, y = point.Y;
            double cos = Math.Cos(angle), sin = Math.Sin(angle);
            return new PointF(Convert.ToSingle(x * cos-y*sin),Convert.ToSingle(x*sin+y*cos));

        }
        public static float GetLength(this PointF point)
        {
            return Convert.ToSingle(Math.Sqrt(point.X * point.X + point.Y * point.Y));
        }

        public static float GetAngle(this PointF point)
        {
            return Convert.ToSingle(Math.Atan2(point.Y, point.X));
        }
        public static int GetAngleDegrees(this PointF point)
        {
            return Convert.ToInt32(Math.Atan2(point.Y, point.X)/2/Math.PI*359);
        }
    }
}
