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
        List<VectorPanel> VectorPanels { get; set; }
        public int Max { get; }
        public event Action<object, int, Vector> VectorChanged;
        public event Action<object, int> VectorDeleted;

        public VectorsControlPanel(IEnumerable<Vector> vectors,int max)
        {
            Max = max;
            VectorPanels = new List<VectorPanel>();
            initGui();
            int index = 0;
            foreach (var vector in vectors)
            {
                addVectorPanel(index, vector,Max);
                index++;
            }

        }

        void initGui()
        {
            BorderStyle = BorderStyle.FixedSingle;
            AutoScroll = true;
        }
        void addVector(Vector vector)
        {
            int number=VectorPanels.Count;
            addVectorPanel(number, vector,Max);
        }
        void addVectorPanel(int number,Vector vector,int max)
        {
            VectorPanel vectorPanel = new VectorPanel(number,vector,max);
            vectorPanel.Dock = DockStyle.Top;
            vectorPanel.VectorChanged += VectorPanel_VectorChaged;
            vectorPanel.VectorDeleted += VectorPanel_VectorDeleted;
            addControlFirst(vectorPanel);
            VectorPanels.Add(vectorPanel);
        }

        private void VectorPanel_VectorDeleted(VectorPanel sender)
        {
            if (sender != null)
            {
                int index = VectorPanels.IndexOf(sender);
                VectorDeleted?.Invoke(this, index);
                VectorPanels.Remove(sender);
                Controls.Remove(sender);
            }
        }

        private void VectorPanel_VectorChaged(VectorPanel sender, Vector vector)
        {
            if (sender != null&&vector!=Vector.Empty)
            {
                int index=VectorPanels.IndexOf(sender);
                VectorChanged?.Invoke(this, index, vector);
            }
        }

        private void addControlFirst(Control control)
        {
            if (control != null && Controls!=null)
            {
                Controls.Add(control);
                Controls.SetChildIndex(control, 0);
            }
        }
    }
}
