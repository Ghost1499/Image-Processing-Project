using System;
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

            int r = color1.R + color2.R, g = color1.G + color2.G, b = color1.B + color2.B;
            int[] chanels = { r, g, b };
            for (int i=0;i<chanels.Length;i++)
            {
                int elem = chanels[i];
                if(elem<byte.MinValue)
                {
                    chanels[i] = byte.MinValue;
                }
                else if (elem>byte.MaxValue) {
                    chanels[i] = byte.MaxValue;
                }
            }
            return Color.FromArgb(chanels[0],chanels[1],chanels[2]);
        }
        public static byte[] ToByteArray(this Color color)
        {
            return new byte[3] { color.R, color.G, color.B };
        }
    }

    public static class RectangleFExtensions
    {
        public static PointF GetCenter(this RectangleF rectangleF)
        {
            return new PointF(rectangleF.X + rectangleF.Width / 2, rectangleF.Y + rectangleF.Height / 2);
        }
    }
    public static class RectangleExtensions
    {
        public static Point GetCenter(this Rectangle rectangle)
        {
            return new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
        }
    }



}
