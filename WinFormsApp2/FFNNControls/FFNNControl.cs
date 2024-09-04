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
using WinFormsApp2.MainControls;

namespace WinFormsApp2.FFNNControls
{
    public partial class FFNNControl : UserControl
    {
        SimpleTextBinding FFNNSource_TextBinding = new SimpleTextBinding();
        SimpleTextBinding DataSourceTextBox_Binding = new SimpleTextBinding();
        IDataSet? dataset;
        FFNN_Client? client;

        CancellationTokenSource cancelTask = new CancellationTokenSource();
        CancellationToken cancelTaskToken;

        public event EventHandler<FFNN_Client> ClientReleased;
        public event EventHandler<GraphDisplay> GraphDisplayRequested;
        public event EventHandler Close;
        public FFNNControl()
        {
            InitializeComponent();
            BindLabels();
            //Thread tr = new Thread(() => { PlotFFNN(); });
            //tr.Start();
            cancelTaskToken = cancelTask.Token;
        }

        void OnGraphDisplayRequested(GraphDisplay g)
        {
            EventHandler<GraphDisplay> handler = GraphDisplayRequested;
            if (handler != null && client != null)
            {
                g.SetGraph(client.ToGraph());
                handler(this, g);
            }
        }

        private void BindLabels()
        {
            DataSourceLocation.DataBindings.Add("Text", DataSourceTextBox_Binding, "Text");
            FFNNSourceTextBox.DataBindings.Add("Text", FFNNSource_TextBinding, "Text");
        }
        private void SetName(string name)
        {
            this.Name = name;
        }
        class SimpleTextBinding : INotifyPropertyChanged        //Binding code from Stack Overflow
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

        protected void ReleaseClient()
        {
            EventHandler<FFNN_Client> handler = ClientReleased;
            if (handler != null)
            {
                handler(this, this.client!);  //shouldnt be null when called
                client = null;
            }
            FFNNChart.Series["Error"].Points.Clear();

        }

        private void SelectDataSourceB_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog OpenDatafileDialogue = new OpenFileDialog())
            {
                OpenDatafileDialogue.Multiselect = false;
                OpenDatafileDialogue.Filter = "Dataset Files | *.ads;*.ds;*dbds";
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
                    DataSourceTextBox_Binding.Text = OpenDatafileDialogue.FileName;
                }
            }
        }

        private void SelectFFNNB_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog OpenDatafileDialogue = new OpenFileDialog())
            {
                OpenDatafileDialogue.Multiselect = false;
                OpenDatafileDialogue.Filter = "FFNN Files | *.ffnn";
                DialogResult result = OpenDatafileDialogue.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(OpenDatafileDialogue.FileName))
                {
                    if (client != null)
                    {
                        ReleaseClient();
                    }

                    client = FFNN_Client.Load(OpenDatafileDialogue.FileName);

                    LearningRateNUpDown.Value = (decimal)client.GetLearningRate();

                    FFNNSource_TextBinding.Text = OpenDatafileDialogue.FileName;
                    ClientsCountLabel.Text = client.GetClients().Count().ToString();

                    client.NextGeneration += (sender, e) => PlotPoint_FFNNChart(e.Item1, e.Item2);
                    client.Unpause();

                    SetName(OpenDatafileDialogue.SafeFileName);
                }
            }
        }
        public void FFNN_Programatic_Assign(string path)
        {
            if (client != null)
            {
                ReleaseClient();
            }

            client = FFNN_Client.Load(path);

            LearningRateNUpDown.Value = (decimal)client.GetLearningRate();

            FFNNSource_TextBinding.Text = path;
            ClientsCountLabel.Text = client.GetClients().Count().ToString();

            client.NextGeneration += (sender, e) => PlotPoint_FFNNChart(e.Item1, e.Item2);
            client.Unpause();
            SetName(path.Split('\\').Last());
        }

        private void  TrainB_Click(object sender, EventArgs e)
        {
            if (dataset != null && client != null)
            {
                try
                {
                    Task.Run(() =>
                    {
                        bool relevant = true;
                        ClientReleased += (sender, e) => relevant = false;
                        while (relevant)
                        {
                            while (!client.GetPause())
                            {
                                if (!relevant) break;
                                client.Train(dataset: dataset);
                            }
                        }
                        MessageBox.Show("Successfully terminated");
                        Clear_FFNNChart();
                    }, cancelTaskToken
                    ); //we DONT want to wait
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void PauseB_Click(object sender, EventArgs e)
        {
            if (client != null)
            {
                bool state = client.GetPause();
                if (state)
                {
                    //Paused
                    client.TogglePause();
                    PauseB.Text = "Pause";
                }
                else
                {
                    //Unpaused
                    client.TogglePause();
                    PauseB.Text = "Unpause";
                }
            }
        }

        private void SaveB_Click(object sender, EventArgs e)
        {
            if (dataset != null && client != null)
            {
                using (SaveFileDialog SaveLocationDialogue = new SaveFileDialog())
                {
                    SaveLocationDialogue.AddExtension = true;
                    SaveLocationDialogue.DefaultExt = ".ffnn";
                    //SaveLocationDialogue.InitialDirectory=
                    DialogResult result = SaveLocationDialogue.ShowDialog();
                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(SaveLocationDialogue.FileName))
                    {

                        string SaveLocation = SaveLocationDialogue.FileName;
                        client.Save(SaveLocation);
                    }
                }
            }
        }
        private void PlotPoint_FFNNChart(double x, double y)
        {
            //FFNNChart.ChartAreas["ChartArea1"].AxisY.Maximum = 5;
            //FFNNChart.ChartAreas["ChartArea1"].AxisY.Minimum = 0;
            //FFNNChart.ChartAreas["ChartArea1"].AxisX.Maximum = 10000;
            //FFNNChart.ChartAreas["ChartArea1"].AxisX.Minimum = 0;
            //FFNNChart.ChartAreas["ChartArea1"].AxisX.IsLogarithmic = true;
            //FFNNChart.ChartAreas["ChartArea1"].AxisY.Maximum = 1;

            Action action = () => { FFNNChart.Series["Error"].Points.AddXY(x, y); Update(); };
            this.Invoke(action);
        }

        private void Clear_FFNNChart()
        {
            Action action = () => { FFNNChart.Series["Error"].Points.Clear(); Update(); };
            this.Invoke(action);
        }

        private void LearningRateNUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (client != null)
            {

                if (Decimal.ToDouble(LearningRateNUpDown.Value) != client.GetLearningRate())
                {
                    client.SetLearningRate(Decimal.ToDouble(LearningRateNUpDown.Value));
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            if (client != null) { client.Pause(); ReleaseClient(); }
            Close(this, e);
        }

        private void ViewGraphB_Click(object sender, EventArgs e)
        {
            GraphDisplay graphDisplay = new GraphDisplay();
            OnGraphDisplayRequested(graphDisplay);
        }

        //private async Task PlotFFNN()
        //{
        //    while (true)
        //    {
        //        if (client != null)
        //        {
        //            if (dataset != null)
        //            {
        //                //if (FFNNChart.Series["Accuracy"].Points.Count != 0)
        //                //{

        //                //FFNNChart.ChartAreas["Chart1"].AxisX.Interval = 1000;
        //                PlotPoint_FFNNChart(client.generations + 1, client.lowest_error);
        //                //}
        //            }
        //        }
        //    }
        //}

    }
}
