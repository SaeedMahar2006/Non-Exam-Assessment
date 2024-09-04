using NeaLibrary.Data;
using NeaLibrary.DataStructures;

using NeaLibrary.NeuralNetwork.NEAT;
using System.ComponentModel;
using System.Xml.Linq;
using WinFormsApp2.MainControls;

namespace WindowsFormsApp1
{
    public partial class NEATControl : UserControl
    {
        private NEAT_Handler neat_handler;
        private IDataSet dataset;
        private string datasource_string = "null";
        public event EventHandler Close;
        public event EventHandler<(NEAT_Handler, string name)> ClientChange;
        public event EventHandler<GraphDisplay> GraphDisplayRequested;

        bool loading_parameters = true;

        private BackgroundWorker neatWorker = new BackgroundWorker();
        public NEATControl()
        {
            InitializeComponent();
            BindLabels();

        }


        private void PlotPoint_NeatChart(double x, double y)
        {
            Action action = () => { NEAT_Chart.Series["Performance"].Points.AddXY(x, y); Update(); };
            this.Invoke(action);
        }
        private void BindLabels()
        {
            DataSourceLocation.DataBindings.Add("Text", DataSourceTextBox_Binding, "Text");
            NeatSourceTextBox.DataBindings.Add("Text", NeatSourceTextBox_Binding, "Text");
        }
        //private async Task PlotNeat()
        //{
        //    while (true)
        //    {
        //        if (neat_handler != null)
        //        {
        //            if (neat_handler.client.PAUSED == false)
        //            {
        //                PlotPoint_NeatChart(neat_handler.client.generations, neat_handler.client.best_acc);
        //            }
        //        }
        //    }
        //}

        class SimpleTextBinding : INotifyPropertyChanged        //from so
        {
            private string text = "null";
            public string Text
            {
                get { return text; }
                set
                {
                    text = value;
                    InvokePropertyChanged(new PropertyChangedEventArgs("Text"));
                }
            }



            public event PropertyChangedEventHandler? PropertyChanged;

            public void InvokePropertyChanged(PropertyChangedEventArgs e)
            {
                PropertyChangedEventHandler handler = PropertyChanged!;
                if (handler != null) handler(this, e);
            }
        }
        public void ReleaseClient()
        {
            neat_handler.Pause();
            try { neat_handler.NextGeneration -= OnNextGeneration; }
            catch
            {
                //shouldnt happen
            }
            Action action = () => { NEAT_Chart.Series["Performance"].Points.Clear(); Update(); };
            this.Invoke(action);
        }
        public void SetName(string s)
        {
            Invoke(() => this.Name = s);
        }
        public void Neat_Programatic_Assign(string path)
        {
            if (neat_handler != null)
            {
                ReleaseClient();
            }

            neat_handler = (NEAT_Handler)NEAT_Handler.Load(path);//new NEAT_Handler(Neat.Load(path)!);
            fetch_parameters();
            NeatSourceTextBox.Text = path;
            //ClientsCountLabel.Text = client.clients.Count().ToString();

            neat_handler.NextGeneration += OnNextGeneration;
            SetName(path.Split('\\').Last());
            OnClientChange(neat_handler, path.Split('\\').Last());
        }

        void fetch_parameters()
        {

            if (neat_handler != null)
            {
                loading_parameters = true;

                CreaturesNUpDown.Value = neat_handler.client!.max_clients;
                KillRateNUpDown.Value = (decimal)neat_handler.client.kill_rate;
                SpecieDistanceNUpDown.Value = (decimal)neat_handler.client.specie_distance;
                C1NUpDown.Value = (decimal)neat_handler.client.c1;
                C2NUpDown.Value = (decimal)neat_handler.client.c2;
                C3NUpDown.Value = (decimal)neat_handler.client.c3;

                MNodeNUpDown.Value = (decimal)neat_handler.client.probability_mutate_node;
                MConnNUpDown.Value = (decimal)neat_handler.client.probability_mutate_link;
                MToggleNUpDown.Value = (decimal)neat_handler.client.probability_mutate_link_toggle;
                MWeightShiftNUpDown.Value = (decimal)neat_handler.client.probability_mutate_weight_shift;
                WeightShiftStrengthNUpDown.Value = (decimal)neat_handler.client.weight_shift_strength;

                loading_parameters = false;
            }
        }
        void OnClientChange(NEAT_Handler n, string name)
        {
            EventHandler<(NEAT_Handler, string)> handler = ClientChange;
            if (handler != null)
            {
                //fetch_parameters();
                handler(this, (n, name));
            }
        }
        void OnGraphDisplayRequested(GraphDisplay g)
        {
            EventHandler<GraphDisplay> handler = GraphDisplayRequested;
            if (handler != null)
            {
                Graph? temp = (neat_handler.client.GetBest().Phenotype != null) ? neat_handler.client.GetBest().Phenotype : neat_handler.client.GetBest().ToGraph();
                if (temp == null) throw new Exception("No phonetype for the best creature");
                g.SetGraph(temp);
                handler(this, g);
            }
        }

        private void SelectDataSourceB_Click(object sender, EventArgs e)
        {
            OpenDatafileDialogue.Multiselect = false;
            OpenDatafileDialogue.Filter = "Supported Encodings | *.ads;*.ds;*dbds";
            DialogResult result = OpenDatafileDialogue.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(OpenDatafileDialogue.FileName))
            {
                //.ads   ArrayDataSet
                //.ds DataSet
                //.dbds  DB_DataSet
                string c = OpenDatafileDialogue.FileName.Split('.').Last();
                switch (c)
                {
                    case "ads":
                        dataset = ArrayDataSet.Load(OpenDatafileDialogue.FileName);
                        break;
                    case "ds":
                        dataset = NeaLibrary.DataStructures.DataSet.Load(OpenDatafileDialogue.FileName);
                        break;
                    case "dbds":
                        dataset = DB_DataSet.Load(OpenDatafileDialogue.FileName);
                        break;
                }
                datasource_string = OpenDatafileDialogue.FileName;
                DataSourceTextBox_Binding.Text = datasource_string;
            }
        }

        private void NeatSourceTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SelectNeatB_Click(object sender, EventArgs e)
        {
            OpenDatafileDialogue.Multiselect = false;
            OpenDatafileDialogue.Filter = "Supported Encodings | *.neat";
            DialogResult result = OpenDatafileDialogue.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(OpenDatafileDialogue.FileName))
            {
                //.ads   ArrayDataSet
                //.ds DataSet
                //.dbds  DB_DataSet


                NEAT_Handler temp = (NEAT_Handler)NEAT_Handler.Load(OpenDatafileDialogue.FileName); //new NEAT_Handler(Neat.Load(OpenDatafileDialogue.FileName)!);

                ReleaseClient();

                neat_handler = temp;

                string s = OpenDatafileDialogue.FileName;
                NeatSourceTextBox_Binding.Text = s;

                neat_handler.NextGeneration += OnNextGeneration;
                fetch_parameters();

            }
        }

        void OnNextGeneration(object? sender, (int, double) e)
        {
            PlotPoint_NeatChart(e.Item1, e.Item2);
        }

        private void TrainB_Click(object sender, EventArgs e)
        {
            if (dataset != null && neat_handler != null)
            {
                neat_handler.Unpause();
                neat_handler.dataset = dataset;
                PauseB.Enabled = true;

                if (neatWorker.IsBusy)
                {
                    MessageBox.Show("Already training");
                    return;
                    //return;
                }

                //Task.Run(() => neat_handler.Train(0.9));

                neatWorker.DoWork += (seder, ev) =>
                {
                    Console.WriteLine();
                    //try
                    //{
                    //throw new NotImplementedException();
                    neat_handler.Train(dataset);   //fix the task thing
                    Console.WriteLine();
                    //}catch (Exception ex)
                    //{
                    //    throw ex;
                    //}
                };

                neatWorker.RunWorkerCompleted += (sdr, evnts) =>
                {
                    if (evnts.Error != null)
                    {
                        MessageBox.Show("Error encountered while training: " + evnts.Error.Message + evnts.Error.ToString());
                    }
                };

                neatWorker.RunWorkerAsync();
            }

        }
        private void PauseB_Click(object sender, EventArgs e)
        {

            if (dataset != null && neat_handler != null)
            {
                neat_handler.Pause();
                PauseB.Enabled = false;
            }
        }

        private void SaveB_Click_1(object sender, EventArgs e)
        {
            string SaveLocation;

            if (dataset != null && neat_handler != null)
            {
                using (SaveFileDialog SaveLocationDialogue = new SaveFileDialog())
                {
                    //SaveLocationDialogue.InitialDirectory=
                    SaveLocationDialogue.Filter = "NEAT (*.neat)|*.neat"; // credit to https://stackoverflow.com/questions/1213339/save-file-with-appropriate-extension-in-a-save-file-prompt
                    SaveLocationDialogue.DefaultExt = "neat";
                    SaveLocationDialogue.AddExtension = true;
                    DialogResult result = SaveLocationDialogue.ShowDialog();
                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(SaveLocationDialogue.FileName))
                    {

                        SaveLocation = SaveLocationDialogue.FileName;
                        neat_handler.Save(SaveLocation);
                    }
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {

            Close(this, e);
        }

        private void ViewAsGraphB_Click(object sender, EventArgs e)
        {
            if (neat_handler != null)
            {
                GraphDisplay d = new GraphDisplay();
                OnGraphDisplayRequested(d);
            }
        }



        private void CreaturesNUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (loading_parameters) return;
            neat_handler.ChangeParameter(NEAT_Handler.Action.creature_count, (double)CreaturesNUpDown.Value); //cast to int later so its fine
        }

        private void KillRateNUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (loading_parameters) return;
            neat_handler.ChangeParameter(NEAT_Handler.Action.kill_rate, (double)KillRateNUpDown.Value);
        }

        private void SpecieDistanceNUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (loading_parameters) return;
            neat_handler.ChangeParameter(NEAT_Handler.Action.specie_distance, (double)SpecieDistanceNUpDown.Value);
        }

        private void C1NUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (loading_parameters) return;
            neat_handler.ChangeParameter(NEAT_Handler.Action.c1, (double)C1NUpDown.Value);
        }

        private void C2NUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (loading_parameters) return;
            neat_handler.ChangeParameter(NEAT_Handler.Action.c2, (double)C2NUpDown.Value);
        }

        private void C3NUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (loading_parameters) return;
            neat_handler.ChangeParameter(NEAT_Handler.Action.c3, (double)C3NUpDown.Value);
        }

        private void MNodeNUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (loading_parameters) return;
            neat_handler.ChangeParameter(NEAT_Handler.Action.probability_mutate_node, (double)MNodeNUpDown.Value);
        }

        private void MConnNUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (loading_parameters) return;
            neat_handler.ChangeParameter(NEAT_Handler.Action.probability_mutate_link, (double)MConnNUpDown.Value);
        }

        private void MWeightShiftNUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (loading_parameters) return;
            neat_handler.ChangeParameter(NEAT_Handler.Action.probability_mutate_weight_shift, (double)MWeightShiftNUpDown.Value);
        }

        private void WeightShiftStrengthNUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (loading_parameters) return;
            neat_handler.ChangeParameter(NEAT_Handler.Action.weight_shift_strength, (double)WeightShiftStrengthNUpDown.Value);
        }

        private void MToggleNUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (loading_parameters) return;
            neat_handler.ChangeParameter(NEAT_Handler.Action.probability_mutate_link_toggle, (double)MToggleNUpDown.Value);
        }


    }
}
