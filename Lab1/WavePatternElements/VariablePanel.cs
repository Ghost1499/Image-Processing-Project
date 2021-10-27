using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1.WavePatternElements
{
    public class VariablePanel : Panel
    {
        Label NameLabel { get; }
        TextBox TextBox { get; }
        TrackBar TrackBar { get; }
        public event Action<object,float> ValueChanged;

        public VariablePanel(string name,float value,float max)
        {
            NameLabel = createNameLabel(name);
            TextBox = createTextBox(value);
            TrackBar = createTrackbar(max,value);
            Height = TextBox.Height*2;

            Controls.AddRange(new Control[]
            {
                NameLabel,TextBox,TrackBar
            });
        }
        Label createNameLabel(string name)
        {
            Label label = new Label();
            label.Text = name ?? "";
            label.Dock = DockStyle.Right;
            //label.BorderStyle = BorderStyle.FixedSingle;
            label.AutoSize = true;
            return label;
        }
        TextBox createTextBox(float value)
        {
            TextBox textBox = new TextBox();
            textBox.Dock = DockStyle.Right;
            textBox.Text = floatToInt(value).ToString();
            textBox.ReadOnly = true;
            return textBox;
        }
        TrackBar createTrackbar(float max, float value)
        {
            TrackBar trackBar = new TrackBar();
            trackBar.TickStyle = TickStyle.None;
            trackBar.Dock = DockStyle.Right;
            trackBar.Width = Width*2/3;
            //trackBar.MinimumSize = trackBar.Size;
            trackBar.Maximum = floatToInt(max);
            trackBar.Minimum = -floatToInt(max);
            trackBar.Value = floatToInt(value);
            trackBar.Scroll += TrackBar_Scroll;
            return trackBar;
        }

        private void TrackBar_Scroll(object sender, EventArgs e)
        {
            Type expectedType = TrackBar.GetType();
            if (sender?.GetType() == expectedType)
            {
                TrackBar trackBar = sender as TrackBar;
                changeValue(this, trackBar.Value);
            }
        }
        void changeValue(object sender,int value)
        {
            TextBox.Text= value.ToString();
            ValueChanged?.Invoke(sender,intToFloat(value));

        }
        static int floatToInt(float value)
        {
            return (int)value;
        }
        static float intToFloat(int value)
        {
            return (float)value;
        }
    }
}
