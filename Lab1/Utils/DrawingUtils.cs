using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.Utils
{
    public static class DrawingUtils
    {
        public static void DrawAxis(Graphics graphics,RectangleF rectangle,Pen pen=null,float arrowLength=20)
        {
            if(pen is null)
            {
                pen = new Pen(Color.Blue, 1.5f);
            }
            PointF xStartPoint = new PointF(rectangle.Left,rectangle.Y+rectangle.Height / 2), yStartPoint = new PointF(rectangle.X +rectangle.Width / 2, rectangle.Top), xEndPoint = new PointF(rectangle.X+rectangle.Width-1,rectangle.Y +rectangle.Height/2), yEndPoint = new PointF(rectangle.X +rectangle.Width/2, rectangle.Y+rectangle.Height-1);
            DrawArrow(graphics, pen, xStartPoint, xEndPoint, arrowLength);
            DrawArrow(graphics, pen, yStartPoint, yEndPoint, arrowLength);

        }
        public static void DrawVector(Graphics graphics, Pen pen, PointF start, PointF end, float arrowLength, int number)
        {
            DrawArrow(graphics, pen, start, end, arrowLength);
            string drawString = number.ToString();
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 16);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.DarkViolet);
            graphics.DrawString(drawString, drawFont, drawBrush, end);
            drawFont.Dispose();
            drawBrush.Dispose();
        }

        public static void DrawArrow(Graphics graphics, Pen pen, PointF start, PointF end, float arrowLength)
        {
            float d = (float)(arrowLength / Math.Sqrt(2));
            graphics.DrawLine(pen, start, end);

            PointD v = (PointD)end - start;
            v *= arrowLength / v.GetLength();
            PointD vPos = v;
            vPos.Rotate(Math.PI * 3 / 4);
            vPos += end;
            PointD vNeg = v;
            vNeg.Rotate(-Math.PI * 3 / 4);
            vNeg += end;

            graphics.DrawLine(pen, (PointF)vPos, end);
            graphics.DrawLine(pen, (PointF)vNeg, end);
        }

        public static void DrawLineFromPoint(Graphics graphics, Pen pen, PointF start, PointF vector)
        {
            graphics.DrawLine(pen, start, new PointF(start.X + vector.X, start.Y + vector.Y));
        }
    }
}
