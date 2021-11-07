using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Utils
{
    public static class MathUtils
    {
        public static double[] MultiplyArrays(IList<double> array1, IList<double> array2)
        {
            if(array1 is null)
            {
                throw new ArgumentNullException(nameof(array1), "Массив равен null");
            }
            if (array2 is null)
            {
                throw new ArgumentNullException(nameof(array2), "Массив равен null");
            }
            int length = array1.Count;
            if (length!=array2.Count)
            {
                throw new ArgumentException("Размеры массивов не совпадают", nameof(array2));
            }
            double[] result = new double[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = array1[i] * array2[i];
            }
            return result;
        }
        public static double[] MultiplyArray(IList<double> array, double value)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array), "Массив равен null");
            }
            int length = array.Count;
            double[] result = new double[length];
            for (int i = 0; i < length; i++)
            {
                result[i] = array[i] * value;
            }
            return result;
        }
        public static byte ScaleColorsMultiply(byte color1, byte color2)
        {
            byte max = Byte.MaxValue;
            return Convert.ToByte((double)color1 * color2 / max);
        }
        public static double Multiply(byte c1, byte c2)
        {
            byte max = Byte.MaxValue;
            return (double)c1 * c2 / max;
        }
        public static int[] MultiplyColors(Color color1, Color color2)
        {
            int[] result = { color1.R * color2.R, color1.G * color2.G, color1.B * color2.B };
            return result;
        }
        public static double DegreesToRadians(int degrees)
        {
            double radians=degrees/360.0*2*Math.PI;
            return radians;
        }
        public static int RadiansToDegrees(double radians)
        {
            double doublePI = 2 * Math.PI;
            int degrees = Convert.ToInt32(radians/ doublePI * 360);
            return degrees;
        }
    }
}
