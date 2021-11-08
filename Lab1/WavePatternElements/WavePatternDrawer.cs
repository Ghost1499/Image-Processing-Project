using Lab1.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.WavePatternElements
{
    public class WavePatternDrawer:IDisposable
    {
        private bool disposed = false;
        private bool sizeSet=false;

        public bool SizeSet { get => sizeSet; set => sizeSet = value; }
        public Bitmap WavePattern { get; private set; }
        public Bitmap CurrentBitmap { get; private set; }

        public Size BitmapSize { get;private set; }
        public int BitmapWidth { get => BitmapSize.Width; }
        public int BitmapHeight { get => BitmapSize.Height; }
        
        public Rectangle DrawingRectangle { get; private set; }
        public int DrawingRectangleWidth { get => DrawingRectangle.Width; }
        public int DrawingRectangleHeight { get => DrawingRectangle.Height; }

        private Graphics CurrentGraphics { get; set; }
        public Pen AxisPen { get; set; }
        public Pen VectorsPen { get; set; }
        public Point AxisCenter { get; set; }

        public WavePatternDrawer(Size bitmapSize)
        {
            SetSize(bitmapSize);
            init();
        }

        public WavePatternDrawer()
        {
            init();
        }
        private void init()
        {
            AxisPen = new Pen(Color.Blue, 1.5f);
            VectorsPen = new Pen(Color.OrangeRed, 3f);
        }
        private void updateBitmapSize(Bitmap bitmap)
        {
            if (bitmap != null)
            {
                Bitmap oldBitmap = bitmap;
                bitmap = new Bitmap(bitmap, BitmapSize);
                oldBitmap.Dispose();
            }
            //else
            //{
            //    WavePattern = new Bitmap(BitmapWidth, BitmapHeight);
            //}
        }
        private void updateCurrentBitmap()
        {
            CurrentBitmap?.Dispose();
            CurrentBitmap = (Bitmap)WavePattern.Clone();
        }
        private void updateGraphics()
        {
            CurrentGraphics?.Dispose();
            CurrentGraphics = Graphics.FromImage(CurrentBitmap);
        }
        private void checkSizeSet()
        {
            if (!sizeSet)
            {
                throw new Exception("Size not set");
            }
        }
        private void checkDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("Object has been disposed");
            }
        }
        public void SetSize(Size bitmapSize)
        {
            BitmapSize = bitmapSize;
            updateBitmapSize(WavePattern);
            updateBitmapSize(CurrentBitmap);
            SetDrawingRectangle(new Rectangle(Point.Empty, BitmapSize));
            sizeSet = true;
        }
        public void SetDrawingRectangle(Rectangle rectangle)
        {
            DrawingRectangle = rectangle;
            AxisCenter = DrawingRectangle.GetCenter();
        }

        ~WavePatternDrawer()
        {
            Dispose(false);
        }

        public void UpdateAxisGui(IEnumerable<Vector> vectors)
        {
            checkDisposed();
            checkSizeSet();
            updateCurrentBitmap();
            updateGraphics();

            DrawAxis();
            DrawVectors(vectors);
        }
        public void DrawWavePattern(IEnumerable<Vector> vectors)
        {
            checkDisposed();
            checkSizeSet();
            WavePattern?.Dispose();
            WavePattern = new Bitmap(BitmapWidth, BitmapHeight);

            Rectangle bitmapRectangle = new Rectangle(Point.Empty,BitmapSize);
            DrawingRectangle.Intersect(bitmapRectangle);
            int width = DrawingRectangleWidth, height = DrawingRectangleHeight;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    double cos = 0;
                    foreach (var vector in vectors)
                    {
                        cos += Math.Cos(vector.ValuePoint.X * x + vector.ValuePoint.Y * y);
                    }
                    cos = cos > 1 ? 1 : cos;
                    cos = cos < 0 ? 0 : cos;
                    int brightness = Convert.ToInt32(127 + 50 * cos);
                    Color color = Color.FromArgb(brightness, brightness, brightness);
                    WavePattern.SetPixel(x, y, color);
                }
            }

            CurrentBitmap?.Dispose();
            CurrentBitmap = (Bitmap)WavePattern.Clone();
        }
        
        public void DrawAxis()
        {
            checkDisposed();
            checkSizeSet();
            if (CurrentBitmap is null)
            {
                throw new Exception("Сurrent Bitmap is null");
            }
            if (CurrentGraphics is null)
            {
                CurrentGraphics = Graphics.FromImage(CurrentBitmap);
            }
            //Graphics graphics = CurrentGraphics;
            DrawingUtils.DrawAxis(CurrentGraphics, DrawingRectangle,AxisCenter,AxisPen);
        }

        public void DrawVectors(IEnumerable<Vector> vectors)
        {
            checkDisposed();
            checkSizeSet();
            if (CurrentBitmap is null)
            {
                CurrentBitmap = new Bitmap(BitmapWidth, BitmapHeight);
            }
            if (CurrentGraphics is null)
            {
                CurrentGraphics = Graphics.FromImage(CurrentBitmap);
            }
            //Graphics graphics = CurrentGraphics;
            int number = 0;
            foreach (var vector in vectors)
            {
                DrawingUtils.DrawVector(CurrentGraphics, VectorsPen, AxisCenter, (PointF)(((PointD)vector.DisplayPoint) + AxisCenter), 20, number);
                number++;
            }
        }

        // реализация интерфейса IDisposable.
        public void Dispose()
        {
            Dispose(true);
            // подавляем финализацию
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    WavePattern?.Dispose();
                    CurrentBitmap?.Dispose();
                    CurrentGraphics?.Dispose();
                    AxisPen?.Dispose();
                }
                // освобождаем неуправляемые объекты
                disposed = true;
            }
        }

    }
}
