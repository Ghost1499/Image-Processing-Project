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
        // чем больше с, тем меньше узоры
        float c;
        public const float maxC= 0.5f;
        public float MaxCDisplay { get => maxC*scale; }
        const float minC= 0.05f;
        const int minVectorPixelLength = 50;
        const float scale = 1 / minC * minVectorPixelLength;

        public List<Vector> Vectors { get;private set; } //= new PointF[] { new PointF(d, d), new PointF(d, -d), new PointF(0, c), new PointF(c, 0) };
        public WavePattern()
        {
            c = 0.25f;
            float d = Convert.ToSingle(Math.Sqrt(c * c / 2));
            Vectors = new List<Vector> {new Vector(new PointF(d, d),scale), new Vector(new PointF(d, -d), scale), new Vector(new PointF(0,c), scale), new Vector(new PointF(c, 0), scale)};

        }
        public void DrawPattern(Bitmap bitmap)
        {
            int width = bitmap.Width, height = bitmap.Height;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double cos = 1;
                    foreach (var vecotr in Vectors)
                    {
                        cos *= Math.Cos(vecotr.ValuePoint.X * x + vecotr.ValuePoint.Y * y);
                    }

                    int brightness = Convert.ToInt32(127 + 50 * cos);
                    Color color = Color.FromArgb(brightness, brightness, brightness);
                    bitmap.SetPixel(x, y, color);
                }
            }
        }

        public void DrawVectors(Bitmap bitmap)
        {
            int width = bitmap.Width, height = bitmap.Height;
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                Utils.DrawAxis(graphics, width, height);
                PointF axisCenter = new PointF(width / 2, height / 2);
                foreach (var vector in Vectors)
                {
                    Pen pen = new Pen(Color.OrangeRed, 3f);
                    Utils.DrawArrow(graphics, pen, axisCenter, vector.DisplayPoint.Sum(axisCenter), 20);
                }
            }
        }

        public void AddVector(Vector vector)
        {
            if (vector !=Vector.Empty)
            {
                Vectors.Add(vector);
            }
        }
        public void RemoveVector(int index)
        {
            if (index>=0 && index<Vectors.Count)
            {
                Vectors.RemoveAt(index);
            }
        }
        public void ChangeVector(int index,Vector vector)
        {
            if (index >= 0 && index < Vectors.Count &&vector!=Vector.Empty)
            {
                Vectors[index] = vector;
            }
        }
    }
}
