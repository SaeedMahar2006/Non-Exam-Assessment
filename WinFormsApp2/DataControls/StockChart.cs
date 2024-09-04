using NeaLibrary.Data;
using Skender.Stock.Indicators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using NeaLibrary.DataStructures;

using System.Reflection;

namespace WinFormsApp2.DataControls
{
    public partial class StockChart : UserControl
    {
        public const int CHUNK_SIZE = 30;
        string SQLCommand;
        public DB_DataSet dataset;
        List<string> choices = new List<string>();
        public delegate void OnPointToBePlotted(string table, Quote q, double marker);
        public event EventHandler Exit;
        public StockChart()
        {
            InitializeComponent();
        }

        public void setDataset(DB_DataSet ds)
        {
            dataset = ds;
        }
        public void InitialiseCharts()
        {
            //CandleChart.Series.Add("Price");
            //CandleChart.Series["Price"].AxisLabel = 
            ClearChart();
            CandleChart.ChartAreas["ChartArea1"].AxisX.ScrollBar.Enabled = true;
            CandleChart.ChartAreas["ChartArea1"].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;

            //CandleChart.ChartAreas["ChartArea1"].AxisX2.Enabled = AxisEnabled.True;
            //CandleChart.ChartAreas["ChartArea1"].AxisX2.MajorGrid.Enabled = false;
            //CandleChart.ChartAreas["ChartArea1"].AxisX2.MinorGrid.Enabled = false;

            CandleChart.ChartAreas["ChartArea1"].AxisY.IsStartedFromZero = false;
            CandleChart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Format = "dd-MM-yy";
            CandleChart.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            CandleChart.ChartAreas["ChartArea1"].AxisX.IntervalType = DateTimeIntervalType.Auto;


            //CandleChart.ChartAreas["ChartArea1"].AxisX2.Interval = 1;
            //CandleChart.ChartAreas["ChartArea1"].AxisX2.IsInterlaced = false;
            //CandleChart.ChartAreas["ChartArea1"].AxisX2.InterlacedColor = Color.Transparent;
            //CandleChart.ChartAreas["ChartArea1"].AxisX2.IntervalAutoMode = IntervalAutoMode.VariableCount;

            CandleChart.ChartAreas["ChartArea1"].AxisX2.IntervalType = DateTimeIntervalType.Auto;

            // enable autoscroll
            CandleChart.ChartAreas["ChartArea1"].CursorX.AutoScroll = true;

            // let's zoom to [0,blockSize] (e.g. [0,100])
            CandleChart.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoomable = true;
            CandleChart.ChartAreas["ChartArea1"].AxisX.ScaleView.SizeType = DateTimeIntervalType.Number;
            //CandleChart.ChartAreas["ChartArea1"].AxisX2.ScaleView.Zoomable = true;
            //CandleChart.ChartAreas["ChartArea1"].AxisX2.ScaleView.SizeType = DateTimeIntervalType.Number;
            int position = 0;
            int size = 30;
            CandleChart.ChartAreas["ChartArea1"].AxisX.ScaleView.Zoom(position, size);
            //CandleChart.ChartAreas["ChartArea1"].AxisX2.ScaleView.Zoom(position, size);

            // disable zoom-reset button (only scrollbar's arrows are available)
            CandleChart.ChartAreas["ChartArea1"].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            //CandleChart.ChartAreas["ChartArea1"].AxisX2.ScrollBar.ButtonStyle = ScrollBarButtonStyles.None;


            // set scrollbar small change to blockSize (e.g. 100)
            CandleChart.ChartAreas["ChartArea1"].AxisX.ScaleView.SmallScrollSize = 30;
            //CandleChart.ChartAreas["ChartArea1"].AxisX2.ScaleView.SmallScrollSize = 30;

            //CandleChart.ChartAreas["ChartArea1"].AxisX2.StripLines.Clear();


            //CandleChart.Series["Price"].ChartType = SeriesChartType.Candlestick;

            //CandleChart.Series["Price"]["OpenCloseStyle"] = "Triangle";

            //CandleChart.Series["Price"]["ShowOpenClose"] = "Both";

            //CandleChart.Series["Price"]["PointWidth"] = "1.0";

            //CandleChart.Series["Price"]["PriceUpColor"] = "Green";
            //CandleChart.Series["Price"]["PriceDownColor"] = "Red";

            IndicatorChart.ChartAreas["ChartArea1"].AxisX.ScrollBar.Enabled = true;

        }

        void ClearChart()
        {
            if (CandleChart == null) return;
            Invoke(() => { CandleChart.Series.Clear(); });
        }


        void PlotPoint(string table, Quote q, double marker)
        {
            // adding date and high
            int i = CandleChart.Series[table].Points.AddXY(q.Date, q.High);
            // adding low
            CandleChart.Series[table].Points[i].YValues[1] = (double)q.Low;
            //adding open
            CandleChart.Series[table].Points[i].YValues[2] = (double)q.Open;
            // adding close
            CandleChart.Series[table].Points[i].YValues[3] = (double)q.Close;
            //plotpoint(table, q);
            CandleChart.Series[table + " Close"].Points.AddXY(q.Date, q.Close);

            if (marker == 1)
            {
                //buy
                CandleChart.Series[table + " Close"].Points[i].MarkerStyle = MarkerStyle.Circle;
                CandleChart.Series[table + " Close"].Points[i].MarkerColor = Color.Green;
                CandleChart.Series[table + " Close"].Points[i].MarkerSize = 10;
            }
            else if (marker == -1)
            {
                //sell
                CandleChart.Series[table + " Close"].Points[i].MarkerStyle = MarkerStyle.Circle;
                CandleChart.Series[table + " Close"].Points[i].MarkerColor = Color.Red;
                CandleChart.Series[table + " Close"].Points[i].MarkerSize = 10;
            }
        }


        public void PlotAsset(string table)
        {
            if (PlotterWorker.IsBusy) return;
            if (CandleChart.Series.IsUniqueName(table))
            {
                CandleChart.Series.Add(table);
                CandleChart.Series.Add(table + " Close");
            }
            //new Series()

            CandleChart.Series[table].ChartType = SeriesChartType.Candlestick;
            CandleChart.Series[table + " Close"].ChartType = SeriesChartType.Line;

            CandleChart.Series[table]["OpenCloseStyle"] = "Triangle";

            CandleChart.Series[table]["ShowOpenClose"] = "Both";

            CandleChart.Series[table]["PointWidth"] = "0.9";

            CandleChart.Series[table]["PriceUpColor"] = "Green";
            CandleChart.Series[table]["PriceDownColor"] = "Red";

            CandleChart.Series[table].BorderWidth = 3;
            //CandleChart.Series[table]. = 3;
            //CandleChart.Series[table].BorderColor = Color.Black;


            //CandleChart.Series[table].AxisLabel = "Date";

            CandleChart.Series[table].SetCustomProperty("PriceUpColor", "Green");
            CandleChart.Series[table].SetCustomProperty("PriceDownColor", "Red");


            PlotterWorker.RunWorkerCompleted += (sender, e) =>
            {
                if (e.Error != null) return;
                CandleChart = e.Result as Chart;
                if (CandleChart == null) { MessageBox.Show($"check if data table has missing values"); return; }
                CandleChart.Update();
            };

            try
            {
                PlotterWorker.RunWorkerAsync(argument: new Tuple<Chart, string, DB_DataSet>(CandleChart, table, dataset));
                
            }
            catch(Exception e)
            {
                MessageBox.Show($"Check if some tables have missing data \n{e.Message}");
            }



        }




        private void SQLCommandTB_TextChanged(object sender, EventArgs e)
        {

        }
        private void ChangeSQlTB(string s)
        {
            Invoke(() => SQLCommandTB.Text = s);
        }
        private void ChangeRecordsPlotted(int s)
        {
            Invoke(() => TotalRecordsTB.Text = s.ToString());
        }
        private void PlotterWorkerWork(object sender, DoWorkEventArgs e)
        {

                Tuple<Chart, string, DB_DataSet> tup = e.Argument as Tuple<Chart, string, DB_DataSet>; // uhhh
                if (tup != null)
                {
                    string table = tup.Item2;


                    Chart MyChart = tup.Item1;
                    DB_DataSet dataset = tup.Item3;
                    SQLiteConnection conn = dataset.driver.conn;

                    ChangeSQlTB($"SELECT {String.Join(", ", dataset.cols)} FROM {table} WHERE 1=1 ORDER BY Date ASC LIMIT -1 OFFSET {dataset.GetInternalDbOffset()};");

                    IEnumerable<Vector> input = SQL_Driver.ReadRow_AsVector(conn, /*"(" + from_string + ")"*/ table, dataset.cols, $"1=1 ORDER BY Date ASC LIMIT -1 OFFSET {dataset.GetInternalDbOffset()}");
                    OutputDataProducer output = new OutputDataProducer(dataset.safety, 3, input, dataset.GetValueIndexes(), dataset.GetValueIndexes().Count() / 2);


                    IEnumerator<Vector> signalSource = output.GetEnumerator();
                    IEnumerator<Quote> quoteSource = SQL_Driver.ReadRow_AsQuote(conn, table, new List<string> { "Date", "open", "high", "low", "close", "volume" }, dataset.GetInternalDbOffset()).GetEnumerator();
                    //signalSource.MoveNext(); //its 1 ahead idk why
                    int i = 0;
                    while (signalSource.MoveNext() && quoteSource.MoveNext())
                    {
                        Quote q = quoteSource.Current;



                        var safeInvoker = new OnPointToBePlotted(PlotPoint);
                        Invoke(safeInvoker, table, q, signalSource.Current[0]);
                        i++;
                        if (i > 500) break;
                    }
                    ChangeRecordsPlotted(i);
                    e.Result = MyChart;
                }
            }
        

        public void LockTokenSelector()
        {
            TokenSelector.ReadOnly = false;
        }
        public void UnLockTokenSelector()
        {
            TokenSelector.ReadOnly = true;
        }
        public void SetTokens(IEnumerable<string> tokens)
        {
            TokenSelector.Items.Clear();
            Invoke(() => { foreach (string token in tokens) { TokenSelector.Items.Add(token); } });
        }
        private void TokenSelector_SelectedItemChanged(object sender, EventArgs e)
        {
            bool nothingwrong = true;
            if (!PlotterWorker.IsBusy)
            {
                if (TokenSelector.SelectedItem != null)
                {
                    TokenSelector.Enabled = false;
                    PlotterWorker.RunWorkerCompleted += (o, e) =>
                    {
                        if (e.Error != null)
                        {
                            MessageBox.Show($"check if data table has missing values {e.Error.Message}");
                            nothingwrong = false;
                        }
                        TokenSelector.Enabled = true;
                    };
                    if (nothingwrong)
                    {
                        ClearChart();
                        PlotAsset(TokenSelector.SelectedItem.ToString());
                    }
                }
            }
        }

        private void ExitB_Click(object sender, EventArgs e)
        {
            Exit(this, e);
        }
    }
}
