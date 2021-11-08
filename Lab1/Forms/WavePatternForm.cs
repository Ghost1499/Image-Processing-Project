using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab1.WavePatternElements;

namespace Lab1
{
    public partial class WavePatternForm : Form
    {
        WavePattern WavePattern { get; set; }
        WavePatternDrawer WavePatternDrawer { get; set; }
        VectorsControlPanel VectorsControlPanel { get; set; }

        public WavePatternForm()
        {
            InitializeComponent();
            init();
        }
        private void init()
        {
            typeof(Panel).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,null, imagePanel, new object[] { true });
            WavePatternDrawer = new WavePatternDrawer(WavePattern.SoftBrightness);
        }
        private void initGui()
        {
            VectorsControlPanel = new VectorsControlPanel(WavePattern.Vectors, WavePattern.maxDisplay,WavePattern.displayScale);
            VectorsControlPanel.Dock = DockStyle.Fill;
            VectorsControlPanel.VectorChanged += VectorsControlPanel_VectorChanged;
            VectorsControlPanel.VectorsChanged += VectorsControlPanel_VectorsChanged;
            VectorsControlPanel.VectorDeleted += VectorsControlPanel_VectorDeleted;
            VectorsControlPanel.VectorCreated += VectorsControlPanel_VectorCreated;
            splitContainer1.Panel1.Controls.Add(VectorsControlPanel);
        }
        public void DrawAll()
        {
            WavePatternDrawer.DrawWavePattern(WavePattern.Vectors);
            DrawAxisGui();
        }
        public void DrawAxisGui()
        {
            WavePatternDrawer.UpdateAxisGui(WavePattern.Vectors);
            //this.BackgroundImage = WavePatternDrawer.CurrentBitmap;
            imagePanel.BackgroundImage = WavePatternDrawer.CurrentBitmap;
        }
        private void VectorsControlPanel_VectorsChanged(object sender, Dictionary<int, Vector> vectors)
        { 
            if (sender?.GetType() == VectorsControlPanel.GetType())
            {
                WavePattern.ChangeVectors(vectors);
                DrawAxisGui();
            }
        }

        private void VectorsControlPanel_VectorCreated(object sender, Vector vector)
        {
            if (sender?.GetType() == VectorsControlPanel.GetType())
            {
                WavePattern.AddVector(vector);
                DrawAxisGui();
            }
        }

        private void VectorsControlPanel_VectorDeleted(object sender, int index)
        {
            if (sender?.GetType() == VectorsControlPanel.GetType())
            {
                WavePattern.RemoveVector(index);
                DrawAxisGui();
            }
        }

        private void VectorsControlPanel_VectorChanged(object sender, int index, Vector vector)
        {
            if (sender?.GetType() == VectorsControlPanel.GetType())
            {
                WavePattern.ChangeVector(index, vector);
                DrawAxisGui();
            }
        }

        private void drawWavePatternButton_Click(object sender, EventArgs e)
        {
            DrawAll();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                WavePatternDrawer.UpdateAxisGui(WavePattern.Vectors);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void imagePanel_Paint(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;
            if (WavePattern is null)
            {
                int maxDisplay = panel.Width > panel.Height ? panel.Height / 2 : panel.Width / 2;
                WavePattern = new WavePattern(maxDisplay);
                initGui();
            }
            if (!WavePatternDrawer.SizeSet)
                WavePatternDrawer.SetSize(panel.Size);
            if (WavePatternDrawer.CurrentBitmap is null)
            {
                DrawAll();
                panel.Paint -= imagePanel_Paint;
            }
        }
    }
}
