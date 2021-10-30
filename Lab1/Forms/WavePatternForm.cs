using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab1.WavePatternElements;

namespace Lab1
{
    public partial class WavePatternForm : Form
    {
        Bitmap Bitmap { get; set; }
        Bitmap WaveBitmap { get; set; }
        WavePattern WavePattern { get; set; }
        VectorsControlPanel VectorsControlPanel { get; set; }

        public WavePatternForm()
        {
            InitializeComponent();
            int maxDisplay = imagePanel.Width > imagePanel.Height ? imagePanel.Height / 2 : imagePanel.Width / 2;
            maxDisplay = 400;
            WavePattern = new WavePattern(maxDisplay);
            initGui();
        }
        private void initGui()
        {
            VectorsControlPanel = new VectorsControlPanel(WavePattern.Vectors, WavePattern.maxDisplay);
            VectorsControlPanel.Dock = DockStyle.Fill;
            VectorsControlPanel.VectorChanged += VectorsControlPanel_VectorChanged;
            VectorsControlPanel.VectorDeleted += VectorsControlPanel_VectorDeleted; ;
            splitContainer1.Panel1.Controls.Add(VectorsControlPanel);
        }

        private void VectorsControlPanel_VectorDeleted(object sender, int index)
        {
            if (sender?.GetType() == VectorsControlPanel.GetType())
            {
                WavePattern.RemoveVector(index);
                DrawVectors();
            }
        }

        private void VectorsControlPanel_VectorChanged(object sender, int index, Vector vector)
        {
            if (sender?.GetType() == VectorsControlPanel.GetType())
            {
                WavePattern.ChangeVector(index, vector);
                DrawVectors();
            }
        }
        public void DrawAll()
        {
            drawWavePattern();
            DrawVectors();

        }
        private void drawWavePattern()
        {
            if(WaveBitmap is null)
                WaveBitmap = new Bitmap(imagePanel.Width, imagePanel.Height);
            WavePattern.DrawPattern(WaveBitmap);
            //mainPictureBox.Image = WaveBitmap;
            //WaveBitmap = new Bitmap(Bitmap);
        }

        private void DrawVectors()
        {
            Bitmap oldBitmap = Bitmap;
            Bitmap = (Bitmap)WaveBitmap.Clone();
            oldBitmap?.Dispose();
            WavePattern.DrawVectors(Bitmap);

            mainPictureBox.Image = Bitmap;
        }


        private void drawWavePatternButton_Click(object sender, EventArgs e)
        {
            DrawAll();
        }
        
        private void mainPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (Bitmap is null)
                DrawAll();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                DrawAll();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
