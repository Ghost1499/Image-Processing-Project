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
        WavePattern WavePattern { get; set; }
        VectorsControlPanel VectorsControlPanel { get; set; }

        public WavePatternForm()
        {
            InitializeComponent();
            WavePattern = new WavePattern();
            initGui();

            //CreateVectorPanels();
        }
        private void initGui()
        {
            VectorsControlPanel = new VectorsControlPanel(WavePattern.Vectors, WavePattern.MaxCDisplay);
            VectorsControlPanel.VectorChanged += VectorsControlPanel_VectorChanged;
            VectorsControlPanel.VectorDeleted += VectorsControlPanel_VectorDeleted; ;
            mainSplitContainer.Panel1.Controls.Add(VectorsControlPanel);
            //controlVectorsPanel.Controls.Add()
        }

        private void VectorsControlPanel_VectorDeleted(object sender, int index)
        {
            if (sender?.GetType() == VectorsControlPanel.GetType())
            {
                WavePattern.RemoveVector(index);
                Bitmap = new Bitmap(Bitmap.Width, Bitmap.Height);
                DrawVectors(Bitmap);
            }
        }

        private void VectorsControlPanel_VectorChanged(object sender, int index, Vector vector)
        {
            if (sender?.GetType() == VectorsControlPanel.GetType())
            {
                WavePattern.ChangeVector(index, vector);
                Bitmap = new Bitmap(Bitmap.Width,Bitmap.Height);
                DrawVectors(Bitmap);
            }
        }

        public void DrawWavePattern(Bitmap bitmap)
        {
            WavePattern.DrawPattern(bitmap);
            mainPictureBox.Image = bitmap;
        }

        public void DrawVectors(Bitmap bitmap)
        {
            WavePattern.DrawVectors(bitmap);
            mainPictureBox.Image = bitmap;
        }

        private void imagePanel_Paint(object sender, PaintEventArgs e)
        {
            Bitmap = new Bitmap(imagePanel.Width, imagePanel.Height);
            DrawWavePattern(Bitmap);
            DrawVectors(Bitmap);
        }

        private void drawWavePatternButton_Click(object sender, EventArgs e)
        {
            DrawWavePattern(Bitmap);
            DrawVectors(Bitmap);
        }

        private void WavePatternForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                drawWavePatternButton.PerformClick();
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                drawWavePatternButton.PerformClick();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
