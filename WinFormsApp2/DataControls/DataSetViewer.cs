using NeaLibrary.Data;
using NeaLibrary.DataStructures;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp2.FFNNControls;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinFormsApp2.DataControls
{
    public partial class DataSetViewer : UserControl
    {


        private delegate void SafeCallDelegate(string text);
        private delegate void CountReceivedDelegate(int b, int s, int h);
        public delegate void RequestTabForStockChartHandler(object sender, DB_DataSet e);
        public event EventHandler Exit; public event RequestTabForStockChartHandler RequestTabForStockChart;

        IDataSet dataset;
        List<(Vector, Vector)> TableInfo = new List<(Vector, Vector)>();
        object TableInfoLock = new object();
        object DataChartLock = new object();
        string DataSetType, InputCol, TableSourced, ValueCol, RelativeCol, Safety;
        string default_info =
            "{0} Data Set\n" +
            "Data Count: {1}\n" +
            "Input Dimension: {2}\n" +
            "Input Columns: {3}\n" +
            "Output Dimension: {4}\n" +
            "Tables Sourced: {5}\n" +
            "Value Column: {6}\n" +
            "Relative Columns: {7}\n" +
            "Safety: {8}\n"
            ;
        public void DisplayInfo()
        {
            string info = String.Format(default_info, DataSetType, dataset.Count(), dataset.First().Item1.dimension, InputCol, dataset.First().Item2.dimension, TableSourced, ValueCol, RelativeCol, Safety);
            InformationTB.Text = info;
        }

        public void PlotCountPoints(int b, int s, int h){
            lock (DataChartLock)
            {
                DataChart.Series["Count"].Points.AddXY("Buy", b);
                DataChart.Series["Count"].Points.AddXY("Sell", s);
                DataChart.Series["Count"].Points.AddXY("Hold", h);
            }
        }

        public void PlotInfo()
        {
            lock (DataChartLock)
            {
                DataChart.Series["Count"].Points.Clear();
            }
            BackgroundWorker plotCountWorker = new BackgroundWorker();
            plotCountWorker.DoWork += (sender, e) =>
            {
                Tuple<List<(Vector, Vector)>, IDataSet, Chart> tup = e.Argument as Tuple<List<(Vector, Vector)>, IDataSet, Chart>;

                List<(Vector, Vector)> _TableInfo = tup.Item1;
                IDataSet _dataset = tup.Item2;
                Chart _DataChart = tup.Item3;

                int buy = 0;
                int sell = 0;
                int hold = 0;
                int total = 0;
                int looped = 0;
                
                //Action action = () =>
                //{
                lock (TableInfoLock) {
                    _TableInfo.Clear();
                    foreach ((Vector, Vector) v in _dataset.Batch(_dataset.Count()))
                    {
                        _TableInfo.Add(v);

                        if (v.Item2[0] == 1)
                        {
                            buy++;
                            total++;
                        }
                        else if (v.Item2[0] == -1)
                        {
                            sell++;
                            total++;
                        }
                        else if (v.Item2[0] == 0)
                        {
                            hold++;
                            total++;
                        }

                        looped++;

                    }
                }
                //var safeInvoker = new CountReceivedDelegate(PlotCountPoints);
                //Invoke(safeInvoker, buy, sell, hold);
                PlotCountPoints(buy,sell,hold);


                e.Result = new Tuple<List<(Vector, Vector)>, IDataSet, Chart>(_TableInfo, _dataset, _DataChart);


                //_DataChart.Update();
                //};
                //this.Invoke(action);
            };

            plotCountWorker.RunWorkerCompleted += (sender, e) =>
            {
                ChartLoadingLabel.Hide();
                Tuple<List<(Vector, Vector)>, IDataSet, Chart> tup = e.Result as Tuple<List<(Vector, Vector)>, IDataSet, Chart>;
                lock (DataChartLock) {
                    DataChart = tup.Item3;
                }
                //dataset doesnt need to change
                lock (TableInfoLock) {
                    TableInfo = tup.Item1; //IT WORKS MashAllah !
                }
            };

            ChartLoadingLabel.Show();
            plotCountWorker.RunWorkerAsync(argument: new Tuple<List<(Vector, Vector)>, IDataSet, Chart>(TableInfo, dataset, DataChart));
            //fillInformationTAble();

        }
        //void plotInfoDb()
        //{
        //    //TableInfo.Clear();
        //    int buy = 0;
        //    int sell = 0;
        //    int hold = 0;
        //    int total = 0;
        //    int looped = 0;
        //    ((DB_DataSet)dataset).ToListComplete += (object? s, List<(Vector, Vector)> e) =>
        //    {
        //        TableInfo = e;
        //        Thread t = new Thread(() =>
        //        {
        //            foreach ((Vector, Vector) v in TableInfo)
        //            {

        //                if (v.Item2[0] == 1)
        //                {
        //                    buy++;
        //                    total++;
        //                }
        //                else if (v.Item2[0] == -1)
        //                {
        //                    sell++;
        //                    total++;
        //                }
        //                else if (v.Item2[0] == 0)
        //                {
        //                    hold++;
        //                    total++;
        //                }

        //                looped++;

        //            }


        //            DataChart.Series["Count"].Points.AddXY("Buy", buy);
        //            DataChart.Series["Count"].Points.AddXY("Sell", sell);
        //            DataChart.Series["Count"].Points.AddXY("Hold", hold);
        //            Update();
        //        }
        //        );
        //        t.Start();
        //    };
        //    ((DB_DataSet)dataset).ToList();
        //}
        void addRowToTable(Vector i, Vector o)
        {
            Invoke(() =>
            {
                Table.RowCount += 1;
                Label l2 = new Label();
                l2.BackColor = Color.Transparent;
                l2.AutoSize = true;
                l2.Text = "{" + String.Join(", ", i) + "}";
                Table.Controls.Add(l2);
                //Table.SetColumn(l2, 0);


                Label l = new Label();
                l.BackColor = Color.Transparent;
                l.AutoSize = true;
                l.Text = "{" + String.Join(", ", o) + "}";
                Table.Controls.Add(l);
            });
        }

        void fillInformationTAble()
        {
            //BackgroundWorker FillTableWorker = new BackgroundWorker();

            


            //FillTableWorker.DoWork += (sender, e) =>
            //{
                //Tuple<List<(Vector, Vector)>, TableLayoutPanel> tup = e.Argument as Tuple<List<(Vector, Vector)>, TableLayoutPanel>;
                //List<(Vector, Vector)> TableInfo = tup.Item1;
                //TableLayoutPanel _Table = tup.Item2;
                int count = 0;
                //Label label = new Label();
                //label.AutoSize=true;
                //string text = "FIRST 50 RECORDS\n";
                Table.Controls.Clear();
                Table.RowCount = 0;
                lock (TableInfoLock) {
                    foreach ((Vector, Vector) v in TableInfo)
                    {
                        Table.RowCount += 1;
                        Label l2 = new Label();
                        l2.BackColor = Color.Transparent;
                        l2.AutoSize = true;
                        l2.Text = "{" + String.Join(", ", v.Item1) + "}";
                        
                        //Table.SetColumn(l2, 0);


                        Label l = new Label();
                        l.BackColor = Color.Transparent;
                        l.AutoSize = true;
                        l.Text = "{" + String.Join(", ", v.Item2) + "}";

                        //_Table.Controls.Add(l);
                        //_Table.Controls.Add(l2);

                        if (InvokeRequired)
                        {
                            Invoke((MethodInvoker)delegate ()
                            {
                                Table.Controls.Add(l);
                                Table.Controls.Add(l2);

                            });
                        }
                        count++; if (count > 50) break;
                    }
                }

                //for (int r = 0; r < Table.RowCount; r++)
                //{
                //    for (int c = 0; c < Table.ColumnCount; c++)
                //    {
                //        if (r % 2 == 1)
                //        {
                //            Control con = this.Table.GetControlFromPosition(c, r);

                //            if (con != null)
                //            {
                //                TableLayoutPanelCellPosition l = new TableLayoutPanelCellPosition();

                //            }
                //        }

                //    }
                //}

                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate ()
                    {
                        Table.AutoScroll = true;
                        Table.RowStyles.Clear();
                    });
                }



            //};

            //FillTableWorker.RunWorkerCompleted += (sender, e) =>
            //{

            //};

            //FillTableWorker.RunWorkerAsync(argument: new Tuple<List<(Vector,Vector)>, TableLayoutPanel>(TableInfo,Table));
        }

        public DataSetViewer()
        {
            InitializeComponent();
        }

        public void ProgramaticAssign( string path )
        {//the . is needed here
            
            string c = Path.GetExtension( path );
            switch (c)
            {
                case ".ads":
                    dataset = ArrayDataSet.Load(path);
                    DataSetType = "Array";
                    InputCol = "N/A";
                    TableSourced = "N/A";
                    ValueCol = "N/A";
                    RelativeCol = "N/A";
                    Safety = "N/A";
                    break;
                case ".ds":
                    dataset = NeaLibrary.DataStructures.DataSet.Load(path);
                    DataSetType = "Standard";
                    InputCol = "N/A";
                    TableSourced = "N/A";
                    ValueCol = "N/A";
                    RelativeCol = "N/A";
                    Safety = "N/A";
                    break;
                case ".dbds":
                    dataset = DB_DataSet.Load(path);
                    DataSetType = "Database";
                    InputCol = String.Join(", ", ((DB_DataSet)dataset).cols);
                    TableSourced = String.Join(", ", ((DB_DataSet)dataset).tables);
                    ValueCol = String.Join(", ", ((DB_DataSet)dataset).ValueColumn);
                    RelativeCol = String.Join(", ", ((DB_DataSet)dataset).relative_cols);
                    Safety = ((DB_DataSet)dataset).safety.ToString();
                    //stockChart();
                    DB_DataSet_StockView_B.Visible = true;

                    break;
            }
            SourceTB.Text = Path.GetFileName(path);
            DisplayInfo();
            PlotInfo();
        }

        private void CangeSourceB_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDatafileDialogue = new OpenFileDialog();
            OpenDatafileDialogue.Multiselect = false;
            OpenDatafileDialogue.Filter = "Supported Encodings | *.ads;*.ds;*.dbds";
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
                        DataSetType = "Array";
                        InputCol = "N/A";
                        TableSourced = "N/A";
                        ValueCol = "N/A";
                        RelativeCol = "N/A";
                        Safety = "N/A";
                        break;
                    case "ds":
                        dataset = NeaLibrary.DataStructures.DataSet.Load(OpenDatafileDialogue.FileName);
                        DataSetType = "Standard";
                        InputCol = "N/A";
                        TableSourced = "N/A";
                        ValueCol = "N/A";
                        RelativeCol = "N/A";
                        Safety = "N/A";
                        break;
                    case "dbds":
                        dataset = DB_DataSet.Load(OpenDatafileDialogue.FileName);
                        DataSetType = "Database";
                        InputCol = String.Join(", ", ((DB_DataSet)dataset).cols);
                        TableSourced = String.Join(", ", ((DB_DataSet)dataset).tables);
                        ValueCol = String.Join(", ", ((DB_DataSet)dataset).ValueColumn);
                        RelativeCol = String.Join(", ", ((DB_DataSet)dataset).relative_cols);
                        Safety = ((DB_DataSet)dataset).safety.ToString();
                        //stockChart();
                        DB_DataSet_StockView_B.Visible = true;

                        break;
                }
                SourceTB.Text = OpenDatafileDialogue.FileName;
                DisplayInfo();
                PlotInfo();
                //fillInformationTAble();
                //if (dataset is DB_DataSet)
                //{
                //    plotInfoDb();
                //}
                //else
                //{
                //    PlotInfoNonDb();
                //}
            }
        }
        //StockChart stockChart()
        //{
        //    StockChart sc = new StockChart();
        //    //this.Controls.Add(sc);
        //    //this.Controls[sc.Name].BringToFront();
        //    sc.dataset = (DB_DataSet)dataset;
        //    sc.InitialiseCharts();
        //    sc.SetTokens(sc.dataset.tables);
        //    return sc;
        //}

        private void Table_Paint(object sender, PaintEventArgs e)
        {
            //Table.CellPaint += colourRows;
            //void colourRows(object? sender, TableLayoutCellPaintEventArgs e)
            //{//https://stackoverflow.com/questions/34064499/how-to-set-cell-color-in-tablelayoutpanel-dynamically
            //    if ((e.Row) % 2 == 1) e.Graphics.FillRectangle(Brushes.LightSteelBlue, e.CellBounds);
            //}
        }

        private void DB_DataSet_StockView_B_Click(object sender, EventArgs e)
        {
            //DB_DataSet_StockView_B.Visible = false;
            EventArgs ev = new EventArgs();
            RequestTabForStockChart(this, (DB_DataSet)dataset);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Exit(this, e);
        }
    }
}
