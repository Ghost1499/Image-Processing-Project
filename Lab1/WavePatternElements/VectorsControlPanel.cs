using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1.WavePatternElements
{
    public class VectorsControlPanel : Panel
    {
        public int Max { get; }
        public float DisplayScale { get; }
        List<VectorPanel> VectorsPanels { get; set; }
        Button AddVectorButton { get; }
        VariablePanel XVariablePanel { get; set; }
        VariablePanel YVariablePanel { get; set; }
        VariablePanel LengthVariablePanel { get; set; }
        VariablePanel AngleVariablePanel { get; set; }

        Dictionary<VariablePanel, Action<VectorPanel, int>> updateActions;

        public event Action<object, Vector> VectorCreated;
        public event Action<object, int, Vector> VectorChanged;
        public event Action<object, Dictionary<int,Vector>> VectorsChanged;
        public event Action<object, int> VectorDeleted;

        public VectorsControlPanel(IEnumerable<Vector> vectors, int max, float displayScale)
        {
            Max = max;
            DisplayScale = displayScale;
            VectorsPanels = new List<VectorPanel>();
            initGui();
            int index = 0;
            foreach (var vector in vectors)
            {
                addVectorPanel(index, vector, Max);
                index++;
            }
            AddVectorButton = new Button();
            AddVectorButton.Text = "Добавить вектор";
            AddVectorButton.AutoSize = true;
            AddVectorButton.Dock = DockStyle.Top;
            AddVectorButton.Click += AddVectorButton_Click;

            XVariablePanel = createVariablePanel("X", 0, -max, max);
            YVariablePanel = createVariablePanel("Y", 0, -max, max);
            LengthVariablePanel = createVariablePanel("Length", 0, -max, max);
            AngleVariablePanel = createVariablePanel("Phi", 0, -180, 180);



            updateActions = new Dictionary<VariablePanel, Action<VectorPanel, int>>(4)
            {
                { XVariablePanel, (VectorPanel v, int value) => v.SetX(v.Vector.DisplayX + value) },
                { YVariablePanel, (VectorPanel v, int value) => v.SetY(v.Vector.DisplayY + value) },
                { LengthVariablePanel, (VectorPanel v, int value) => v.SetLength(v.Vector.DisplayLength + value) },
                { AngleVariablePanel, (VectorPanel v, int value) => v.SetAngleDegrees(v.Vector.AngleDegrees + value) }
            };

            addControlsFirst();

            foreach (var variablePanel in updateActions.Keys)
            {
                addControlFirst(variablePanel);

            }
        }
        private VariablePanel createVariablePanel(string name, int value, int min, int max)
        {
            VariablePanel variablePanel = new VariablePanel(name, value, min, max);
            variablePanel.Dock = DockStyle.Top;
            variablePanel.Height = Convert.ToInt32(variablePanel.Height / 1.5);
            variablePanel.ValueChanged += VariablePanel_ValueChanged;
            return variablePanel;
        }

        private void VariablePanel_ValueChanged(object sender, int value,int oldValue)
        {
            if (sender?.GetType() == typeof(VariablePanel))
            {
                VariablePanel variablePanel = sender as VariablePanel;
                Action<VectorPanel, int> action;
                bool res=updateActions.TryGetValue(variablePanel, out action);
                if (res)
                {
                    changeVectors(value-oldValue, action);
                }
                else
                {
                    throw new Exception($"Ключ {variablePanel.Name} не найдена в словаре операций");
                }
            }
        }

        private void AddVectorButton_Click(object sender, EventArgs e)
        {
            Vector vector = new Vector();
            vector.Scale = DisplayScale;
            VectorCreated?.Invoke(this, vector);
            addVector(vector);
        }

        void initGui()
        {
            BorderStyle = BorderStyle.FixedSingle;
            AutoScroll = true;
        }
        void addVector(Vector vector)
        {
            int number = VectorsPanels.Count;
            addVectorPanel(number, vector, Max);
        }
        void addVectorPanel(int number, Vector vector, int max)
        {
            VectorPanel vectorPanel = new VectorPanel(number, vector, max);
            vectorPanel.Dock = DockStyle.Top;
            vectorPanel.VectorChanged += VectorPanel_VectorChaged;
            vectorPanel.VectorDeleted += VectorPanel_VectorDeleted;
            addControlFirst(vectorPanel);
            addControlsFirst();
            VectorsPanels.Add(vectorPanel);
        }

        private void VectorPanel_VectorDeleted(VectorPanel sender)
        {
            if (sender != null)
            {
                int index = VectorsPanels.IndexOf(sender);
                VectorDeleted?.Invoke(this, index);
                VectorsPanels.Remove(sender);
                Controls.Remove(sender);
            }
        }

        private void VectorPanel_VectorChaged(VectorPanel sender, Vector vector)
        {
            if (sender != null && vector != Vector.Empty)
            {
                int index = VectorsPanels.IndexOf(sender);
                VectorChanged?.Invoke(this, index, vector);
            }
        }

        private void changeVectors(int value,Action<VectorPanel,int> action)
        {
            Dictionary<int, Vector> dict = new Dictionary<int, Vector>(VectorsPanels.Count);
            for(int i=0;i<VectorsPanels.Count;i++)
            {
                var v = VectorsPanels[i];
                action(v, value);
                dict.Add(i, v.Vector);
            }

            VectorsChanged?.Invoke(this, dict);
        }

        private void addControlFirst(Control control)
        {
            if (control != null && Controls != null)
            {
                if (!Controls.Contains(control))
                {
                    Controls.Add(control);
                }
                Controls.SetChildIndex(control, 0);
            }
        }
        private void addControlsFirst()
        {
            addControlFirst(AddVectorButton);
            if (updateActions != null)
            {
                foreach (var vpanel in updateActions.Keys)
                {
                    addControlFirst(vpanel);
                }
            } 
        }
    }
}
