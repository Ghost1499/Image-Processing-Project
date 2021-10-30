using Lab1.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class MainForm : Form
    {
        public Bitmap source;
        public double[,] kernel;
        private WavePatternForm WavePatternForm { get; set; }
        //public ImageProcessingMethods methods;
        public MainForm()
        {
            InitializeComponent();
            saveFileDialog1.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            saveFileDialog1.Filter = "Image files (*.jpg)|*.jpg|Bitmap files (*.bmp)|*.bmp|All Files (*.*)|*.*";
            openFileDialog1.Filter = "Image files (*.jpg;*.jpeg)|*.jpg;*jpeg|Bitmap files (*.bmp)|*.bmp|All Files (*.*)|*.*";
            kernel = ImageProcessingMethods.sobelFilterdx;

        }


        private void wavePatternButton_Click(object sender, EventArgs e)
        {
            if (WavePatternForm is null||WavePatternForm.IsDisposed)
            {
                WavePatternForm = new WavePatternForm();
                WavePatternForm.Show();
            }
            else
            {
                WavePatternForm.Focus();
            }
            //resultPictureBox.Image = ImageProcessingMethods.WavePattern(mainSplitContainer.Panel2.Width,mainSplitContainer.Panel2.Height);
        }

        private void convolutionButton_Click(object sender, EventArgs e)
        {
            double[,] kernel = this.kernel;
            Bitmap source = this.source;
            Bitmap result = ImageProcessingMethods.Convolution(source, kernel);
            resultPictureBox.Image = result;
        }

        private void correlationButton_Click(object sender, EventArgs e)
        {
            Bitmap source = this.source;
            Bitmap sourceGray = MathUtils.IsGray(source) ? source : MathUtils.Image2Gray(source);
            Bitmap target = openImage();
            Bitmap targetGray = MathUtils.IsGray(target) ? source : MathUtils.Image2Gray(target);

            Bitmap result = ImageProcessingMethods.Correlation(source, target);
            sourcePictureBox.Image = sourceGray;
            resultPictureBox.Image = result;
        }

        private void mainSplitContainer_Scroll(object sender, ScrollEventArgs e)
        {
            Panel slave;
            Panel master = (Panel)sender;
            if (master.Equals(mainSplitContainer.Panel1))
                slave = mainSplitContainer.Panel2;
            else
                slave = mainSplitContainer.Panel1;
            if (!slave.HasChildren)
                return;
            ScrollProperties scroll;
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                scroll = slave.HorizontalScroll;
            else
                scroll = slave.VerticalScroll;
            if (e.NewValue <= scroll.Maximum && e.NewValue >= scroll.Minimum)
            {
                scroll.Value = e.NewValue;
            }
        }
        private void openSourceButton_Click(object sender, EventArgs e)
        {
            Bitmap source = openImage();
            if (source == null)
            {
                return;
            }
            this.source = source;
            sourcePictureBox.Image = this.source;
        }

        private void saveSourceButton_Click(object sender, EventArgs e)
        {
            saveImage(source);
        }

        private void saveResultButton_Click(object sender, EventArgs e)
        {
            saveImage(resultPictureBox.Image as Bitmap);

        }
        private void saveImage(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                MessageBox.Show($"Файл для сохранения не существует");
            }
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            bitmap.Save(filename);
            MessageBox.Show($"Файл {filename} сохранен");
        }

        private Bitmap openImage()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return null;
            string filename = openFileDialog1.FileName;
            Bitmap bitmap = new Bitmap(filename);
            return bitmap;
            //MessageBox.Show("Файл открыт");
        }

        private Bitmap Locus()
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

        private void LocusRGBbutton_Click(object sender, EventArgs e)
        {
            resultPictureBox.Image = Locus();
        }

        private void mainSplitContainer_Panel2_Paint(object sender, PaintEventArgs e)
        {
            wavePatternButton_Click(this, null);
        }
    }


}
