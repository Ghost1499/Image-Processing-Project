using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1.WavePatternElements
{
    public class VectorPanel : Panel
    {
        Label NumberLabel{ get; }
        VariablePanel XVariablePanel { get; set; }
        VariablePanel YVariablePanel { get; set; }
        Button DeleteButton { get; set; }
        public int Number { get; private set; }
        public Vector Vector { get; private set; }
        public float Max { get; }
        public event Action<VectorPanel, Vector> VectorChanged;
        public event Action<VectorPanel> VectorDeleted;

        public void UpdateNumber(int number)
        {
            Number = number;
            NumberLabel.Text = Number.ToString();
        }

        public VectorPanel(int number,Vector vector,float max)
        {
            Number = number;
            Vector = vector;
            Max = max;
            NumberLabel = new Label();
            XVariablePanel = new VariablePanel("X",vector.DisplayPoint.X, Max);
            YVariablePanel = new VariablePanel("Y",vector.DisplayPoint.Y, Max);
            DeleteButton = new Button();

            initGui();
            //XTrackBar.Minimum = minx;
            //XTrackBar.Minimum = minx;


            Controls.AddRange(new Control[] {
            YVariablePanel,
            XVariablePanel,
            NumberLabel,
            DeleteButton
            });
        }
        void initGui()
        {
            //AutoSizeMode = AutoSizeMode.GrowOnly;
            //AutoSize = true;
            BorderStyle = BorderStyle.FixedSingle;


            NumberLabel.Dock = DockStyle.Left;
            NumberLabel.Text = Number.ToString();
            NumberLabel.Font = new Font(NumberLabel.Font.FontFamily, 16, FontStyle.Bold);
            NumberLabel.AutoSize = true;

            XVariablePanel.Dock = DockStyle.Top;
            YVariablePanel.Dock = DockStyle.Top;

            DeleteButton.Text = "x";
            DeleteButton.Font= new Font(DeleteButton.Font.FontFamily, 14, FontStyle.Bold);
            DeleteButton.AutoSize = true;
            DeleteButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            DeleteButton.Dock = DockStyle.Right;
            DeleteButton.Click += DeleteButton_Click;

            XVariablePanel.ValueChanged += VariablePanel_ValueChanged;
            YVariablePanel.ValueChanged += VariablePanel_ValueChanged;

            Height = NumberLabel.Height*4;

        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            VectorDeleted?.Invoke(this);
        }
        private void Delete()
        {
            VectorDeleted?.Invoke(this);
        }
        private void VariablePanel_ValueChanged(object sender,float value)
        {
            Type expType = typeof(VariablePanel);
            if(sender!=null && sender.GetType() == expType)
            {
                VariablePanel variablePanel = sender as VariablePanel;
                bool ind = false;
                PointF newPoint = PointF.Empty;
                if (variablePanel.Equals(XVariablePanel))
                {
                    newPoint = new PointF(value, Vector.DisplayPoint.Y);
                    ind = true;
                }
                else if (variablePanel.Equals(YVariablePanel))
                {
                    newPoint = new PointF(Vector.DisplayPoint.X,value);
                    ind = true;
                }
                if (ind)
                {
                    Vector vector = new Vector(Vector.DisplayToValue(newPoint), Vector.Scale);
                    VectorChanged?.Invoke(this, vector);
                    Vector = vector;
                }

            }
        }
    }
}
