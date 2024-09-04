using NeaLibrary.Data;
using NeaLibrary.DataStructures;
using NeaLibrary.NeuralNetwork.FFNN;
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
    public partial class FFNNCreateControl : UserControl
    {

        List<HiddenLayerDimensionSpecifier> HiddenLayers = new List<HiddenLayerDimensionSpecifier>();
        public FFNNCreateControl()
        {
            InitializeComponent();
        }
        public event EventHandler FinishedCreating;
        private void label5_Click(object sender, EventArgs e)
        {

        }
        private void ReRenderHiddenLayersTable()
        {
            HiddenLayerP.RowStyles.Clear();
            HiddenLayerP.RowCount = HiddenLayers.Count;
            int row = 0;
            foreach (HiddenLayerDimensionSpecifier layer in HiddenLayers)
            {
                HiddenLayerP.Controls.Add(layer, 0, row);
                row++;
            }
        }
        private void RemoveHiddenLayer(object sender, EventArgs e)
        {
            HiddenLayers.Remove((HiddenLayerDimensionSpecifier)sender);
            ReRenderHiddenLayersTable();
        }

        private void AddHiddenLayer_B_Click(object sender, EventArgs e)
        {
            HiddenLayerP.RowStyles.Clear();
            HiddenLayerP.Visible = true;
            HiddenLayerDimensionSpecifier c = new HiddenLayerDimensionSpecifier();
            c.Show();
            c.RemoveHiddenLayer += new EventHandler(RemoveHiddenLayer);
            HiddenLayers.Add(c);
            HiddenLayerP.RowCount += 1;
            HiddenLayerP.Controls.Add(c);

        }

        private void Create_B_Click(object sender, EventArgs e)
        {
            List<int> topology = new List<int>
            {
                (int)InputDimension_NUpDown.Value
            };
            foreach (HiddenLayerDimensionSpecifier h in HiddenLayers)
            {
                topology.Add((int)h.numericUpDown1.Value);
            }
            topology.Add((int)OutputDimension_NUpDown.Value);
            Vector tp = new Vector(topology.Count);
            for (int i = 0; i < tp.dimension; i++)
            {
                tp[i] = topology[i];
            }
            SaveFFNNDialogue.DefaultExt = ".ffnn";

            DialogResult result = SaveFFNNDialogue.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(SaveFFNNDialogue.FileName))
            {
                FFNN_Client client = new FFNN_Client(tp, (double)LearningRate_NUpDown.Value, (int)Clients_NUpDown.Value);
                client.Save(SaveFFNNDialogue.FileName);
                FinishedCreating(this, new EventArgs());
            }


        }

        private void HiddenLayerP_Paint(object sender, PaintEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            FinishedCreating(this, EventArgs.Empty);
        }
    }
}
