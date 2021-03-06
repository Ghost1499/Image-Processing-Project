using Lab1.Utils;
using Lab1.WavePatternElements;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class WavePattern
    {
        const double PI = Math.PI;
        double W { get; set; } // частота
        double T { get => 2 * Math.PI / W; set => W= 2 * Math.PI / value; } //период (в пикселях)
        public const double maxT = 32 * PI;
        public const double minT = 8*PI;
        public static readonly double maxW = getW(minT);
        public static readonly double minW = getW(maxT);
        public int maxDisplay = 600;
        public float displayScale;

        static double getT(double w)
        {
            return 2 * Math.PI / w;
        }
        static double getW(double t)
        {
            return 2 * Math.PI / t;
        }
        public List<Vector> Vectors { get; private set; } //= new PointF[] { new PointF(d, d), new PointF(d, -d), new PointF(0, c), new PointF(c, 0) };
        public WavePattern(int maxDisplay)
        {
            this.maxDisplay = maxDisplay;
            displayScale = Convert.ToSingle(maxDisplay / maxW);
            T = 20*PI;
            PointD point = new PointD(W, 0);
            Vector vector0 = new Vector(point, displayScale);
            Vector vector90 = new Vector(vector0);
            vector90.Rotate(90);
            Vector vector45 = new Vector(vector0);
            vector45.Rotate(45);
            Vector vector45n = new Vector(vector0);
            vector45n.Rotate(-45);
            Vectors = new List<Vector> { vector0, vector90, vector45, vector45n };
        }
        public static int SoftBrightness(double cos)
        {
            cos = cos > 1 ? 1 : cos;
            cos = cos < 0 ? 0 : cos;
            int brightness = Convert.ToInt32(127 + 50 * cos);
            return brightness;
        }
        public static int HardBrightness(double cos)
        {
            int value = Convert.ToInt32(127 + 50 * cos);
            value = value > byte.MaxValue ? byte.MaxValue : value;
            value = value < byte.MinValue ? byte.MinValue : value;
            int brightness = Convert.ToInt32(value);
            return brightness;
        }
        public void AddVector(Vector vector)
        {
            if (vector != Vector.Empty)
            {
                Vectors.Add(vector);
            }
        }
        public void RemoveVector(int index)
        {
            if (index >= 0 && index < Vectors.Count)
            {
                Vectors.RemoveAt(index);
            }
        }
        public void ChangeVector(int index, Vector vector)
        {
            if (index >= 0 && index < Vectors.Count && vector != Vector.Empty)
            {
                Vectors[index] = vector;
            }
        }

        public void ChangeVectors(Dictionary<int,Vector> vectors)
        {
            foreach (var keyValue in vectors)
            {
                ChangeVector(keyValue.Key, keyValue.Value);
            }
        }
    }
}
