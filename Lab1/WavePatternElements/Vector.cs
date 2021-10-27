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
        public static Vector Empty = new Vector(PointF.Empty, 0);
        private PointF _valuePoint;

        public PointF ValuePoint { get => _valuePoint; private set => _valuePoint = value; }
        public float Scale { get; }
        public Point DisplayPoint { get;}
        public float ValueLength
        {
            get => ValuePoint.GetLength();
            set
            {
                if (value>0)
                {
                    _valuePoint.X = Convert.ToSingle(value * Math.Cos(ValueAngle));
                    _valuePoint.Y = Convert.ToSingle(value * Math.Sin(ValueAngle));

                }
            }
        }
        public float ValueAngle { get => ValuePoint.GetAngle();
            set
            {
                value %= Convert.ToSingle(2 * Math.PI);
                if (value != 0)
                {
                    _valuePoint.X = Convert.ToSingle(ValueLength * Math.Cos(value));
                    _valuePoint.Y = Convert.ToSingle(ValueLength * Math.Sin(value));
                }
            }
        }
        public float ValueAngleDegrees { get => ValuePoint.GetAngleDegrees(); }
        public float DisplayLength { get => DisplayPoint.ToPointF().GetLength(); }
        public float DisplayAngle { get => DisplayPoint.ToPointF().GetAngle(); }
        public float DisplayAngleDegrees { get => DisplayPoint.ToPointF().GetAngleDegrees(); }

        public Vector(PointF point, float scale)
        {
            _valuePoint = point;
            Scale = scale;
            DisplayPoint = point.Multiply(scale).ToPoint();
        }

        public void Rotate(float angle)
        {
            ValueAngle += angle;
        }
        public PointF DisplayToValue(Point display)
        {
            return ((PointF)display).Multiply(1 / Scale);
        }
        public float DisplayToValue(int display)
        {
            return display/Scale;
        }
        public Point ValueToDisplay(PointF value)
        {
            return value.Multiply(Scale).ToPoint();
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
