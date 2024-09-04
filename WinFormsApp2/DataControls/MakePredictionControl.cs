using NeaLibrary.Data;
using NeaLibrary.Data.Other;
using NeaLibrary.DataStructures;
using NeaLibrary.NeuralNetwork;
using NeaLibrary.NeuralNetwork.FFNN;
using NeaLibrary.NeuralNetwork.NEAT;
using NeaLibrary.Tools;

namespace WinFormsApp2.DataControls
{
    public partial class MakePredictionControl : UserControl
    {
        public event EventHandler Exit;
        public event EventHandler<Vector> VectorViewRequested;
        string neuralnetworktype = "";
        INeuralNetwork? model;
        NNSpecification? NNSpec;
        string info_template =
"""
Neural Network Type: {0}
Count: {1}
Last trained: {2}

Mapping
{3}
""";
        public MakePredictionControl()
        {
            InitializeComponent();
        }

        private void MakePredictionB_Click(object sender, EventArgs e)
        {
            if (NNSpec == null || model == null) return;
            //for (int i =0; i<NNSpec.InputDimension; i++)
            //{
            //    InputTablePanel;
            //}
            Vector result = new Vector(NNSpec.OutputDimension);
            Vector input = new Vector(NNSpec.InputDimension);
            foreach (ArgumentEditorControl control in InputTablePanel.Controls)
            {
                if (control != null)
                {
                    int n = Convert.ToInt32(control.Name);
                    input[n] = Convert.ToDouble(control.GetValue());

                }
            }
            Vector output = model.BestPrediction(input);

            if (output.dimension == 1)
            {
                double pred = output[0];
                //pred should be in [-1,1] but to ensure this putthrough ReLu2
                pred = Tools.ReLu2(pred);
                PredictionLabel.Text = pred.ToString();
                TrackBar.Value = (int)(pred * 10);
            }
            else
            {
                VectorViewRequested(this, output);
            }
        }

        private void LoadInputArgumentEditorControls()
        {
            if (model != null)
            {
                if (neuralnetworktype == "FFNN")
                {
                    NNSpec = model.GetNNSpecification();
                }
                else
                {
                    NNSpec = model.GetNNSpecification();
                }
                InfoLabel.Text = String.Format(info_template, neuralnetworktype, "", ((NNSpec.LastTrainedOn != null) ? NNSpec.LastTrainedOn : "uknown"), ((NNSpec.InputMapCacheDescription != null) ? NNSpec.InputMapCacheDescription.ToString() : ""));
                InputTablePanel.Controls.Clear();
                InputTablePanel.RowStyles.Clear();
                InputTablePanel.ColumnStyles.Clear();
                for (int i = 0; i < NNSpec.InputDimension; i++)
                {
                    var ArgumentControl = new ArgumentEditorControl();
                    SourceParameterInfo sp = new SourceParameterInfo();
                    sp.Name = $"input n.{i.ToString()}";
                    ArgumentControl.setArgumentLabelText(sp.Name);
                    sp.IsNumeric = true;
                    sp.BoundsAndStep = new double[] { -1e6, +1e6, 1e-3 };

                    ArgumentControl.SetInfo(sp);
                    ArgumentControl.Name = i.ToString();
                    InputTablePanel.Controls.Add(ArgumentControl, 0, i);
                }
            }
        }

        private void ChangeB_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenSourceDialogue = new OpenFileDialog();
            OpenSourceDialogue.Multiselect = false;
            OpenSourceDialogue.Filter = "Neural Network | *.neat;*.ffnn";
            DialogResult result = OpenSourceDialogue.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(OpenSourceDialogue.FileName))
            {
                string c = OpenSourceDialogue.FileName.Split('.').Last();
                int count = -1;
                switch (c)
                {
                    case "neat":
                        neuralnetworktype = "NEAT";
                        model = NEAT_Handler.Load(OpenSourceDialogue.FileName);//new NEAT_Handler(Neat.Load(OpenSourceDialogue.FileName));
                        count = ((NEAT_Handler)model).NumberOfCreatures();
                        break;
                    case "ffnn":
                        neuralnetworktype = "FFNN";
                        model = FFNN_Client.Load(OpenSourceDialogue.FileName);
                        count = ((FFNN_Client)model).GetClients().Count;
                        break;
                }
                SourceTB.Text = OpenSourceDialogue.FileName;
                LoadInputArgumentEditorControls();
            }
        }

        private void PredictionLabel_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void InputTablePanel_Paint(object sender, PaintEventArgs e)
        {
        }

        private void ExitB_Click(object sender, EventArgs e)
        {
            Exit(this, EventArgs.Empty);
        }

        private void InfoLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
