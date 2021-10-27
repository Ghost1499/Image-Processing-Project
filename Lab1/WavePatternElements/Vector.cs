using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.WavePatternElements
{
    public struct Vector
    {
        public static Vector Empty=new Vector(PointF.Empty,0);
        public PointF Point { get; }
        public float Scale { get; }
        public PointF DisplayPoint { get; }

        public Vector(PointF point,float scale)
        {
            bool d=PointF.Empty == point;
            Point = point;
            Scale = scale;
            DisplayPoint = point.Multiply(scale);
        }
        public PointF DisplayToValue(PointF display)
        {
            return display.Multiply(1/Scale);
        }
        public PointF ValueToDisplay(PointF value)
        {
            return value.Multiply(Scale);
        }
        public static bool operator ==(Vector vector,Vector other)
        {
            return vector.Point == other.Point && vector.Scale == other.Scale;
        }
        public static bool operator !=(Vector vector, Vector other)
        {
            return !(vector == other);
        }
    }
}
