using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public event Action<object,int,int> ValueChanged;
        int value;
        int? oldValue=null;

        public VariablePanel(string name,int value,int min, int max)
        {
            
            NameLabel = createNameLabel(name);
            TextBox = createTextBox(value);
            TrackBar = createTrackbar(min,max,value);
            SetValue(value);

            Height = TextBox.Height*2;

            Controls.AddRange(new Control[]
            {
                NameLabel,TextBox,TrackBar
            });
        }
        public void SetValue(int value)
        {
            this.value = value;
            if (TrackBar != null) {
                if (value > TrackBar.Maximum)
                {
                    TrackBar.Maximum = value;
                }
                else if (value < TrackBar.Minimum)
                {
                    TrackBar.Minimum = value;
                }
                TrackBar.Value = value; }
            if (TextBox != null ) 
                TextBox.Text = value.ToString();
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
            textBox.Text = value.ToString();
            textBox.ReadOnly = true;
            return textBox;
        }
        TrackBar createTrackbar(int min,int max, float value)
        {
            TrackBar trackBar = new TrackBar();
            trackBar.TickStyle = TickStyle.None;
            trackBar.Dock = DockStyle.Right;
            trackBar.Width = Width*2/3;
            //trackBar.MinimumSize = trackBar.Size;
            trackBar.Maximum = max;
            trackBar.Minimum = min;
            trackBar.Value = floatToInt(value);
            trackBar.Scroll += TrackBar_Scroll;
            return trackBar;
        }

        private void TrackBar_Scroll(object sender, EventArgs e)
        {
            //Debug.WriteLine("TrackBar_Scroll");
            Type expectedType = TrackBar.GetType();
            if (sender?.GetType() == expectedType)
            {
                TrackBar trackBar = sender as TrackBar;
                changeValue(this, trackBar.Value);
            }
        }
        void changeValue(object sender,int value)
        {
            if (oldValue is null)
                oldValue = value;
            ValueChanged?.Invoke(sender,value,(int)oldValue);
            SetValue(value);
            oldValue = value;
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
