namespace WinFormsApp2.DataControls
{
    partial class StockChart
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            CandleChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            IndicatorChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            label1 = new Label();
            TotalRecordsTB = new TextBox();
            label2 = new Label();
            SQLCommandTB = new TextBox();
            PlotterWorker = new System.ComponentModel.BackgroundWorker();
            TokenSelector = new DomainUpDown();
            label3 = new Label();
            ExitB = new Button();
            ((System.ComponentModel.ISupportInitialize)CandleChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)IndicatorChart).BeginInit();
            SuspendLayout();
            // 
            // CandleChart
            // 
            chartArea1.Name = "ChartArea1";
            CandleChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            CandleChart.Legends.Add(legend1);
            CandleChart.Location = new Point(3, 30);
            CandleChart.Name = "CandleChart";
            CandleChart.Size = new Size(1066, 419);
            CandleChart.TabIndex = 0;
            CandleChart.Text = "chart1";
            //CandleChart.Click += CandleChart_Click;
            // 
            // IndicatorChart
            // 
            chartArea2.Name = "ChartArea1";
            IndicatorChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            IndicatorChart.Legends.Add(legend2);
            IndicatorChart.Location = new Point(3, 454);
            IndicatorChart.Name = "IndicatorChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "RSI";
            IndicatorChart.Series.Add(series1);
            IndicatorChart.Size = new Size(1066, 141);
            IndicatorChart.TabIndex = 1;
            IndicatorChart.Text = "chart2";
            IndicatorChart.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(5, 7);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 2;
            label1.Text = "Records Plotted";
            // 
            // TotalRecordsTB
            // 
            TotalRecordsTB.Location = new Point(96, 3);
            TotalRecordsTB.Name = "TotalRecordsTB";
            TotalRecordsTB.ReadOnly = true;
            TotalRecordsTB.Size = new Size(97, 23);
            TotalRecordsTB.TabIndex = 3;
            TotalRecordsTB.Text = "null";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(5, 608);
            label2.Name = "label2";
            label2.Size = new Size(88, 15);
            label2.TabIndex = 4;
            label2.Text = "SQL Command";
            // 
            // SQLCommandTB
            // 
            SQLCommandTB.Location = new Point(96, 605);
            SQLCommandTB.Name = "SQLCommandTB";
            SQLCommandTB.ReadOnly = true;
            SQLCommandTB.Size = new Size(973, 23);
            SQLCommandTB.TabIndex = 5;
            SQLCommandTB.Text = "null";
            SQLCommandTB.TextChanged += SQLCommandTB_TextChanged;
            // 
            // PlotterWorker
            // 
            PlotterWorker.DoWork += PlotterWorkerWork;
            // 
            // TokenSelector
            // 
            TokenSelector.Location = new Point(367, 3);
            TokenSelector.Name = "TokenSelector";
            TokenSelector.ReadOnly = true;
            TokenSelector.Size = new Size(120, 23);
            TokenSelector.TabIndex = 6;
            TokenSelector.Text = "null";
            TokenSelector.SelectedItemChanged += TokenSelector_SelectedItemChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(282, 7);
            label3.Name = "label3";
            label3.Size = new Size(79, 15);
            label3.TabIndex = 7;
            label3.Text = "Token Plotted";
            // 
            // ExitB
            // 
            ExitB.Location = new Point(1046, 3);
            ExitB.Name = "ExitB";
            ExitB.Size = new Size(23, 23);
            ExitB.TabIndex = 11;
            ExitB.Text = "✖";
            ExitB.UseVisualStyleBackColor = true;
            ExitB.Click += ExitB_Click;
            // 
            // StockChart
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ExitB);
            Controls.Add(label3);
            Controls.Add(TokenSelector);
            Controls.Add(SQLCommandTB);
            Controls.Add(label2);
            Controls.Add(TotalRecordsTB);
            Controls.Add(label1);
            Controls.Add(IndicatorChart);
            Controls.Add(CandleChart);
            Name = "StockChart";
            Size = new Size(1072, 776);
            ((System.ComponentModel.ISupportInitialize)CandleChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)IndicatorChart).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart CandleChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart IndicatorChart;
        private Label label1;
        private TextBox TotalRecordsTB;
        private Label label2;
        private TextBox SQLCommandTB;
        private System.ComponentModel.BackgroundWorker PlotterWorker;
        private DomainUpDown TokenSelector;
        private Label label3;
        private Button ExitB;
    }
}
