using Lab1.Utils;
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
        private Vector _vector;

        Label NumberLabel { get; }
        VariablePanel XVariablePanel { get; set; }
        VariablePanel YVariablePanel { get; set; }
        VariablePanel LengthVariablePanel { get; set; }
        VariablePanel AngleVariablePanel { get; set; }
        Button DeleteButton { get; set; }
        public int Number { get; private set; }
        public Vector Vector { get => _vector; private set => _vector = value; }
        public event Action<VectorPanel, Vector> VectorChanged;
        public event Action<VectorPanel> VectorDeleted;


        public void SetX(int value)
        {
            _vector.DisplayX = value;
            SetVector(_vector);
        }
        public void SetY(int value)
        {
            _vector.DisplayY = value;
            SetVector(_vector);
        }
        public void SetLength(int value)
        {
            _vector.DisplayLength = value;
            SetVector(_vector);
        }
        public void SetAngleDegrees(int value)
        {
            _vector.AngleDegrees = value;
            SetVector(_vector);
        }
        public void SetNumber(int number)
        {
            Number = number;
            NumberLabel.Text = Number.ToString();
        }
        public void SetVector(Vector vector)
        {
            if (vector!=Vector)
            {
                Vector = vector;
            }
            XVariablePanel.SetValue(vector.DisplayPoint.X);
            YVariablePanel.SetValue(vector.DisplayPoint.Y);
            LengthVariablePanel.SetValue(vector.DisplayLength);
            AngleVariablePanel.SetValue(vector.AngleDegrees);
        }

        public VectorPanel(int number, Vector vector, int max)
        {
            Number = number;
            Vector = vector;
            NumberLabel = new Label();
            XVariablePanel = createVariablePanel("X", vector.DisplayPoint.X, -max, max);
            YVariablePanel = createVariablePanel("Y", vector.DisplayPoint.Y, -max, max);
            LengthVariablePanel = createVariablePanel("Length", vector.DisplayLength, 0, max);
            AngleVariablePanel = createVariablePanel("Phi", vector.AngleDegrees, -180, 180);
            DeleteButton = new Button();

            initGui();
            //XTrackBar.Minimum = minx;
            //XTrackBar.Minimum = minx;


            Controls.AddRange(new Control[] {
                AngleVariablePanel,
                LengthVariablePanel,
            YVariablePanel,
            XVariablePanel,
            NumberLabel,
            DeleteButton
            });
        }
        void initGui()
        {
            //AutoSizeMode = AutoSizeMode.GrowOnly;
            AutoSize = true;
            BorderStyle = BorderStyle.FixedSingle;


            NumberLabel.Dock = DockStyle.Left;
            NumberLabel.Text = Number.ToString();
            NumberLabel.Font = new Font(NumberLabel.Font.FontFamily, 16, FontStyle.Bold);
            NumberLabel.AutoSize = true;

            DeleteButton.Text = "x";
            DeleteButton.Font = new Font(DeleteButton.Font.FontFamily, 14, FontStyle.Bold);
            DeleteButton.AutoSize = true;
            DeleteButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            DeleteButton.Dock = DockStyle.Right;
            DeleteButton.Click += DeleteButton_Click;

            Height = NumberLabel.Height * 4;

        }

        private VariablePanel createVariablePanel(string name, int value, int min, int max)
        {
            VariablePanel variablePanel = new VariablePanel(name, value, min, max);
            variablePanel.Dock = DockStyle.Top;
            variablePanel.Height = Convert.ToInt32(variablePanel.Height / 1.5);
            variablePanel.ValueChanged += VariablePanel_ValueChanged;
            return variablePanel;
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            Delete();
        }
        private void Delete()
        {
            VectorDeleted?.Invoke(this);
        }
        private void VariablePanel_ValueChanged(object sender, int value,int oldValue)
        {
            Type expType = typeof(VariablePanel);
            if (sender != null && sender.GetType() == expType)
            {
                VariablePanel variablePanel = sender as VariablePanel;
                bool ind = false;
                Vector vector = new Vector(Vector);
                if (variablePanel.Equals(XVariablePanel))
                {
                    vector.ValuePoint = new PointD(Vector.DisplayToValue(value), Vector.ValuePoint.Y);
                    ind = true;
                }
                else if (variablePanel.Equals(YVariablePanel))
                {
                    vector.ValuePoint = new PointD(Vector.ValuePoint.X, Vector.DisplayToValue(value));
                    ind = true;
                }
                else if (variablePanel.Equals(LengthVariablePanel))
                {
                    vector.ValueLength = Vector.DisplayToValue(value);
                    ind = true;
                }
                else if (variablePanel.Equals(AngleVariablePanel))
                {
                    vector.Angle = MathUtils.DegreesToRadians(value);
                    ind = true;
                }
                if (ind)
                {
                    VectorChanged?.Invoke(this, vector);
                    SetVector(vector);
                }

            }
        }
    }
}
