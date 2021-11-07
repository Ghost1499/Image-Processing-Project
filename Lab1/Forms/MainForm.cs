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
            Bitmap sourceGray = ImageProcessingUtils.IsGray(source) ? source : ImageProcessingUtils.Image2Gray(source);
            Bitmap target = openImage();
            Bitmap targetGray = ImageProcessingUtils.IsGray(target) ? source : ImageProcessingUtils.Image2Gray(target);

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



        private void LocusRGBbutton_Click(object sender, EventArgs e)
        {
            resultPictureBox.Image = ImageProcessingMethods.Locus();
        }

        private void mainSplitContainer_Panel2_Paint(object sender, PaintEventArgs e)
        {
            //drawLFMWaveButton.PerformClick();

        }

        private void drawLFMWavebutton_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(mainSplitContainer.Panel2.Width, mainSplitContainer.Panel2.Height);
            ImageProcessingMethods.DrawLFMWave(bitmap);
            resultPictureBox.Image = bitmap;
        }
    }


}
