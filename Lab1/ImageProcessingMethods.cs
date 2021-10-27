using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class ImageProcessingMethods
    {
        public const int ChanelsCount = 3;
        public static double[,] flfr1 ={
                { 1.0 / 9, 1.0 / 9, 1.0 / 9 },
                { 1.0 / 9, 1.0 / 9, 1.0 / 9 },
                { 1.0 / 9, 1.0 / 9, 1.0 / 9 }
            };
        public static double[,] flfr2 ={
                { 0.1,0.1,0.1 },
                {0.1,0.2,0.1 },
                {0.1,0.1,0.1 }
            };
        public static double[,] flfr24 ={
                {1.0/16,2.0/16,1.0/16 },
                {2.0/16,4.0/16,2.0/16 },
                {1.0/16,2.0/16,1.0/16 }
            };
        public static double[,] fhfr1n25 ={
                {1, -2, 1 },
                {-2,5,-2 },
                {1,-2,1}
            };
        public static double[,] fhfrn15 ={
                {0, -1, 0 },
                {-1,5,-1 },
                {0,-1,0}
            };
        public static double[,] fhfrn19 ={
                {-1,-1,-1 },
                {-1,9,-1 },
                {-1,-1,-1}
            };
        public static double[,] sobelFilterdx ={
                {-1,0,1 },
                { -2,0,2},
                {-1,0,1}
            };
        public static double[,] sobelFilterdy ={
                {-1,-2,-1 },
                { 0,0,0},
                {1,2,1}
            };
        public static double[,] df ={
                {1,4,1},
                { 4,-20,4},
                {1,4,1}
            };

        public static Bitmap Convolution(Bitmap source, double[,] kernel)
        {
            int width = source.Width, height = source.Height, kwidth = kernel.GetLength(1), kheight = kernel.GetLength(0);
            Bitmap result = new Bitmap(width, height);
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    double[] res = { 0, 0, 0 };
                    for (int xi = 0; xi < kwidth; xi++)
                        for (int eta = 0; eta < kheight; eta++)
                        {
                            int dx = kwidth / 2 - xi;
                            int dy = kheight / 2 - eta;
                            if (x + dx < 0 || y + dy < 0 || x + dx >= width || y + dy >= height)
                            {
                                continue;
                            }
                            Color color = source.GetPixel(x + dx, y + dy);
                            double[] temp = color.MultiplyArr(kernel[eta, xi]);
                            for (int i = 0; i < res.Length; i++)
                            {
                                res[i] += temp[i];
                            }
                        }
                    byte[] byteRes = new byte[3];
                    for (int i = 0; i < res.Length; i++)
                    {
                        if (res[i] > byte.MaxValue)
                        {
                            byteRes[i] = byte.MaxValue;
                        }
                        else if (res[i] < byte.MinValue)
                        {
                            byteRes[i] = byte.MinValue;
                        }
                        else
                        {
                            byteRes[i] = Convert.ToByte(res[i]);
                        }
                    }
                    Color resultColor = Color.FromArgb(byteRes[0], byteRes[1], byteRes[2]);
                    result.SetPixel(x, y, resultColor);
                }
            return result;
        }

        // For grayscale images only
        public static Bitmap Correlation(Bitmap source, Bitmap target)
        {
            int width = source.Width, height = source.Height, twidth = target.Width, theight = target.Height;
            Bitmap result = new Bitmap(source);
            using (ImageWrapper sourceWrapper = new ImageWrapper(result, true))
            using (ImageWrapper targetWrapper = new ImageWrapper(target, false, ImageLockMode.ReadOnly))
            {
                int colorsSum = Utils.BitmapColorsSumGray(targetWrapper);
                int colorsAvg = colorsSum / twidth / theight;
                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++)
                    {
                        //if (x > 300 || y > 300)
                        //{
                        //    sourceWrapper[x, y] = Color.MediumPurple;
                        //}
                        double res = 0;
                        for (int xi = 0; xi < twidth; xi++)
                            for (int eta = 0; eta < theight; eta++)
                            {
                                int dx = -twidth / 2 +xi;
                                int dy = -theight / 2 + eta;
                                if (x + dx < 0 || y + dy < 0 || x + dx >= width || y + dy >= height)
                                {
                                    continue;
                                }
                                byte scolor = sourceWrapper[x + dx, y + dy].R;
                                byte tcolor = targetWrapper[xi,eta].R;
                                res += (byte.MaxValue - Math.Abs(tcolor - scolor)) * tcolor;
                            }
                        // Делим на 255 и на количество пикселей в target для нормировки значения пикселя
                        //res /= byte.MaxValue;
                        //res /= theight*twidth;
                        res /= colorsSum;
                        if (res > byte.MaxValue)
                            res = byte.MaxValue;
                        else if (res < byte.MinValue)
                            res = byte.MinValue;
                        byte resColor = Convert.ToByte(res);
                        sourceWrapper[x, y] = Color.FromArgb(resColor, resColor, resColor);
                    }
            }
            return result;
            
        }

        //public static Bitmap WavePattern(Size size)
        //{
        //    return WavePattern(size.Width, size.Height);
        //}
        //public static Bitmap WavePattern(int width,int height)
        //{
        //    Bitmap source = new Bitmap(width,height);
            
        //}
    }
}
