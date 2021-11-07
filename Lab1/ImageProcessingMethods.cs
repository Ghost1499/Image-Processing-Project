using Lab1.Utils;
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
        public const double PI = Math.PI;
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
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
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
                            double[] temp = MathUtils.MultiplyArray(color.ToByteArray().Select(elem => (double)elem).ToArray(), kernel[eta, xi]);
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
                int colorsSum = MathUtils.BitmapColorsSumGray(targetWrapper);
                int colorsAvg = colorsSum / twidth / theight;
                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++)
                    {
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
        public static Bitmap Locus()
        {
            Bitmap bmp = new Bitmap(256, 256);
            for (int g = 0; g < 256; g++)
                for (int r = 0; r < 256 - g; r++)
                {
                    int b = 255 - r - g;
                    double dr = r - 256.0 / 3, dg = g - 256.0 / 3;

                    int d = 100 - Convert.ToInt32(Math.Sqrt(dr * dr + dg * dg));
                    int r1 = r + d, g1 = g + d, b1 = b + d;
                    if (r1 < 0) r1 = 0; else if (r1 > 255) r1 = 255;
                    if (g1 < 0) g1 = 0; else if (g1 > 255) g1 = 255;
                    if (b1 < 0) b1 = 0; else if (b1 > 255) b1 = 255;


                    Color c1 = Color.FromArgb(r1, g1, b1);
                    bmp.SetPixel(r, g, c1);
                }
            return bmp;
        }

        public static void DrawLFMWave(Bitmap bitmap)
        {
            int width = bitmap.Width, height = bitmap.Height;
            double dT = 1; // период дискретизации для экрана - 1 пиксель (?)
            // du >= 2*umax
            // 2PI/dT>=2*umax (du=2PI/dT)
            // PI/dT>= C*xmax=C*(width-1)  (umax=C*xmax, xmax=width-1)
            // Cmax= PI/dT/(width-1)
            double du = 1.0/ dT;
            double u, v = 0, C = du/(width-1), D = 0;// D=0  по условию, что в правой части u=0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    u = C * x + D;
                    int brightness = Convert.ToInt32(127+127 * Math.Sin(u*x));
                    Color color = Color.FromArgb(brightness, brightness, brightness);
                    bitmap.SetPixel(x, y, color);
                }
            }
            //Graphics graphics = Graphics.FromImage(bitmap);
            //graphics.DrawLine(new Pen(Color.AliceBlue,3), width / 2, height / 2,0,height/2);
        }
    }
}
