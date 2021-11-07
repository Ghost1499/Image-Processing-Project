using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Utils
{
    public static class ImageProcessingUtils
    {
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
                    wrapper[point] = Color.FromArgb(newColorInt, newColorInt, newColorInt);
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
                    if (!(color.R == color.G && color.G == color.B))
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
            using (ImageWrapper wrapper = new ImageWrapper(bitmap, false, System.Drawing.Imaging.ImageLockMode.ReadOnly))
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
    }
}
