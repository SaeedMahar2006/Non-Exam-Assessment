using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeaLibrary.NeuralNetwork.NEAT;

namespace WindowsFormsApp1.NEATControls
{
    public partial class CreateNEATControl : UserControl
    {
        private string SaveLocation = "";
        public event EventHandler FinishedCreating;
        //public event EventHandler Close;
        public CreateNEATControl()
        {
            InitializeComponent();
        }

        private void ChangeSaveLocationButton_Click(object sender, EventArgs e)
        {

            DialogResult result = SaveLocationDialogue.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(SaveLocationDialogue.FileName))
            {

                SaveLocation = SaveLocationDialogue.FileName;

            }

        }

        private void ConfirmCreate_Click(object sender, EventArgs e)
        {
            SaveLocationDialogue.DefaultExt = ".neat";
            DialogResult result = SaveLocationDialogue.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(SaveLocationDialogue.FileName))
            {

                SaveLocation = SaveLocationDialogue.FileName;


                //Neat n = new Neat((int)InputSelector.Value, (int)OutputSelector.Value, (int)ClientsNumSpecifier.Value);

                NEAT_Handler n = new NEAT_Handler((int)InputSelector.Value, (int)OutputSelector.Value, (int)ClientsNumSpecifier.Value);

                n.ChangeParameter(NEAT_Handler.Action.kill_rate, (double)KillRateSpecifier.Value);
                n.ChangeParameter(NEAT_Handler.Action.specie_distance, (double)SpecieDistanceSpecifier.Value);
                n.ChangeParameter(NEAT_Handler.Action.probability_mutate_node, (double)NewNodeMutate.Value);
                n.ChangeParameter(NEAT_Handler.Action.probability_mutate_link, (double)NewConnectionMutate.Value);
                n.ChangeParameter(NEAT_Handler.Action.probability_mutate_link_toggle, (double)ConnectionToggleMutate.Value);
                n.ChangeParameter(NEAT_Handler.Action.probability_mutate_weight_random, (double)WeightRandomMutate.Value);
                n.ChangeParameter(NEAT_Handler.Action.probability_mutate_weight_shift, (double)WeightShiftMutate.Value);
                n.ChangeParameter(NEAT_Handler.Action.weight_shift_strength, (double)WeightShiftStrength.Value);

                n.Save(SaveLocation);

                FinishedCreating(this, new EventArgs());
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            FinishedCreating(this, EventArgs.Empty);
        }
    }
}
