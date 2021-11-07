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

        public static double[,,] Image2Kernel(Bitmap bitmap)
        {
            int width = bitmap.Width, height = bitmap.Height;
            int square = width * height;
            int colorChanelsCount = 3;
            double[,,] kernel = new double[height, width, colorChanelsCount];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    byte[] color = bitmap.GetPixel(x, y).ToByteArray();
                    for (int chanel = 0; chanel < colorChanelsCount; chanel++)
                    {
                        kernel[y, x, chanel] = (double)color[chanel] / byte.MaxValue / square;
                    }
                }
            return kernel;
        }

        public static bool CheckKernelNormalization(double[,] kernel)
        {
            double sum = 0;
            double targetSum = 1;
            int width = kernel.GetLength(1), height = kernel.GetLength(0);
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    sum += kernel[y, x];
                }
            return sum < targetSum + double.Epsilon;
        }

        public static bool CheckKernelNormalization(double[,,] kernel, out bool[] chanelsResults)
        {
            int chanelsCount = kernel.GetLength(2);
            chanelsResults = new bool[chanelsCount];
            int width = kernel.GetLength(1), height = kernel.GetLength(0);
            bool result = true;
            for (int chanel = 0; chanel < chanelsCount; chanel++)
            {
                double[,] kernel2d = new double[height, width];
                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++)
                    {
                        kernel2d[y, x] = kernel[y, x, chanel];
                    }
                bool chanelRes = CheckKernelNormalization(kernel2d);
                chanelsResults[chanel] = chanelRes;
                if (!chanelRes)
                {
                    result = chanelRes;
                }
            }
            return result;
        }

 

        public static Bitmap Image2Gray(Bitmap bitmap)
        {
            Bitmap grayImage = new Bitmap(bitmap);
            using (ImageWrapper wrapper = new ImageWrapper(grayImage))
            {
                foreach (Point point in wrapper)
                {
                    Color color = wrapper[point];
                    double newColor = 0.2126 * color.R + 0.7152 * color.G + 0.0722 * color.B;
                    int newColorInt = Convert.ToInt32(newColor);
                    if (newColorInt > 255)
                        newColorInt = 255;
                    else if (newColorInt < 0)
                        newColorInt = 0;
                    wrapper[point] = Color.FromArgb(newColorInt,newColorInt,newColorInt);
                }
            }
            return grayImage;
        }

        public static bool IsGray(Bitmap bitmap)
        {
            using (ImageWrapper wrapper = new ImageWrapper(bitmap, true, System.Drawing.Imaging.ImageLockMode.ReadOnly))
            {
                foreach (Point point in wrapper)
                {
                    Color color = wrapper[point];
                    if (!(color.R==color.G && color.G==color.B))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // For gray images only
        public static int BitmapColorsSumGray(Bitmap bitmap)
        {
            int result = 0;
            using (ImageWrapper wrapper=new ImageWrapper(bitmap,false,System.Drawing.Imaging.ImageLockMode.ReadOnly))
            {
                result = BitmapColorsSumGray(wrapper);
            }
            return result;
        }

        public static int BitmapColorsSumGray(ImageWrapper imageWrapper)
        {
            int result = 0;
            foreach (Point point in imageWrapper)
            {
                result += imageWrapper[point].R;
            }
            return result;
        }

        public static void DrawAxis(Graphics graphics,float width,float height)
        {
            Pen pen = new Pen(Color.Blue, 1.5f);
            PointF xStartPoint=new PointF(0,height/2),yStartPoint=new PointF(width/2,0),  xEndPoint = new PointF(width - 1, height / 2), yEndPoint = new PointF(width / 2, height - 1);
            DrawArrow(graphics, pen, xStartPoint, xEndPoint, 20);
            DrawArrow(graphics, pen, yStartPoint, yEndPoint, 20);

        }
        public static void DrawVector(Graphics graphics, Pen pen, PointF start, PointF end, float arrowLength,int number)
        {
            DrawArrow(graphics, pen, start,end,arrowLength);
            string drawString = number.ToString();
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.DarkViolet);
            graphics.DrawString(drawString, drawFont, drawBrush, end);
            drawFont.Dispose();
            drawBrush.Dispose();
        }

        public static void DrawArrow(Graphics graphics,Pen pen, PointF start,PointF end,float arrowLength)
        {
            float d = (float)(arrowLength / Math.Sqrt(2));
            graphics.DrawLine(pen, start,end);

            PointD v = (PointD)end-start;
            v *= arrowLength / v.GetLength();
            PointD vPos = v;
            vPos.Rotate(Math.PI * 3 / 4);
            vPos += end;
            PointD vNeg = v;
            vNeg.Rotate(-Math.PI*3 / 4);
            vNeg += end;

            graphics.DrawLine(pen, (PointF)vPos, end);
            graphics.DrawLine(pen, (PointF)vNeg, end);
        }

        public static void DrawLineFromPoint(Graphics graphics, Pen pen, PointF start, PointF vector)
        {
            graphics.DrawLine(pen, start, new PointF(start.X + vector.X , start.Y + vector.Y ));
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
