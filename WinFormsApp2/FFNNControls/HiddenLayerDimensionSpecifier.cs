using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2.FFNNControls
{
    public partial class HiddenLayerDimensionSpecifier : UserControl
    {
        public event EventHandler RemoveHiddenLayer;
        public HiddenLayerDimensionSpecifier()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RemoveHiddenLayer(this,e);
            
            this.Hide();
            this.Dispose();


        }
    }
}
