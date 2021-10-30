using Lab1.Utils;
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
        public static Vector Empty = new Vector(PointD.Empty, 0);
        private PointD _valuePoint;

        public PointD ValuePoint { get => _valuePoint; set => _valuePoint = value; }
        public double ValueX { get => ValuePoint.X; set => _valuePoint.X = value; }
        public double ValueY { get => ValuePoint.Y; set => _valuePoint.Y = value; }
        public float Scale { get; set; }
        public Point DisplayPoint { get => ValueToDisplay(ValuePoint); set => ValuePoint = DisplayToValue(value); }
        public int DisplayX { get => DisplayPoint.X; set => ValueX=DisplayToValue(value); }
        public int DisplayY { get => DisplayPoint.Y; set => ValueY = DisplayToValue(value); }
        public double ValueLength
        {
            get => ValuePoint.GetLength();
            set
            {
                if (value>=0)
                {
                    double angle = Angle;
                    _valuePoint.X = value * Math.Cos(angle);
                    _valuePoint.Y = value * Math.Sin(angle);
                }
            }
        }
        public double Angle { get => ValuePoint.GetAngle();
            set
            {
                double length = ValueLength;
                _valuePoint.X = length * Math.Cos(value);
                _valuePoint.Y = length * Math.Sin(value);
            }
        }
        public int AngleDegrees { get => ValuePoint.GetAngleDegrees(); set => Angle = MathUtils.DegreesToRadians(value); }
        public int DisplayLength { get => Convert.ToInt32(((PointD)DisplayPoint).GetLength()); set => ValueLength = DisplayToValue(value); }

        public Vector(PointD point, float scale)
        {
            _valuePoint = point;
            Scale = scale;
        }
        public Vector(Vector vector)
        {
            _valuePoint = vector.ValuePoint;
            Scale = vector.Scale;
        }
        private static Point initDisplayPoint(PointD point, float scale)
        {
           return (Point)(point * scale);
        }
        public void Rotate(double angle)
        {
            Angle += angle;
        }
        public void Rotate(int angleDegrees)
        {
            Angle += MathUtils.DegreesToRadians(angleDegrees);
        }
        public PointD DisplayToValue(Point display)
        {
            return (PointD)display/ Scale;
        }
        public double DisplayToValue(int display)
        {
            return display/Scale;
        }
        public Point ValueToDisplay(PointD value)
        {
            return (Point)(value*Scale);
        }
        public static bool operator ==(Vector vector, Vector other)
        {
            return vector.ValuePoint == other.ValuePoint && vector.Scale == other.Scale;
        }
        public static bool operator !=(Vector vector, Vector other)
        {
            return !(vector == other);
        }
    }
}
