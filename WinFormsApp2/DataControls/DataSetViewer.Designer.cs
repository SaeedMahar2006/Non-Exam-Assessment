namespace WinFormsApp2.DataControls
{
    partial class DataSetViewer
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            label1 = new Label();
            SourceTB = new TextBox();
            CangeSourceB = new Button();
            Table = new TableLayoutPanel();
            panel1 = new Panel();
            InformationTB = new RichTextBox();
            DataChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ChartLoadingLabel = new Label();
            DB_DataSet_StockView_B = new Button();
            CloseButton = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataChart).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 8);
            label1.Name = "label1";
            label1.Size = new Size(43, 15);
            label1.TabIndex = 0;
            label1.Text = "Source";
            // 
            // SourceTB
            // 
            SourceTB.Location = new Point(50, 5);
            SourceTB.Name = "SourceTB";
            SourceTB.ReadOnly = true;
            SourceTB.Size = new Size(295, 23);
            SourceTB.TabIndex = 1;
            SourceTB.Text = "null";
            // 
            // CangeSourceB
            // 
            CangeSourceB.Location = new Point(351, 5);
            CangeSourceB.Name = "CangeSourceB";
            CangeSourceB.Size = new Size(75, 23);
            CangeSourceB.TabIndex = 2;
            CangeSourceB.Text = "Change";
            CangeSourceB.UseVisualStyleBackColor = true;
            CangeSourceB.Click += CangeSourceB_Click;
            // 
            // Table
            // 
            Table.ColumnCount = 2;
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Table.Location = new Point(0, 1);
            Table.Name = "Table";
            Table.RowCount = 2;
            Table.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Table.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Table.Size = new Size(1056, 619);
            Table.TabIndex = 3;
            Table.Paint += Table_Paint;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(Table);
            panel1.Location = new Point(3, 34);
            panel1.Name = "panel1";
            panel1.Size = new Size(1059, 623);
            panel1.TabIndex = 4;
            // 
            // InformationTB
            // 
            InformationTB.Location = new Point(1068, 413);
            InformationTB.Name = "InformationTB";
            InformationTB.ReadOnly = true;
            InformationTB.Size = new Size(406, 244);
            InformationTB.TabIndex = 6;
            InformationTB.Text = "null";
            // 
            // DataChart
            // 
            chartArea1.Name = "ChartArea1";
            DataChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            DataChart.Legends.Add(legend1);
            DataChart.Location = new Point(1065, 33);
            DataChart.Name = "DataChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Count";
            DataChart.Series.Add(series1);
            DataChart.Size = new Size(406, 366);
            DataChart.TabIndex = 5;
            DataChart.Text = "chart1";
            // 
            // ChartLoadingLabel
            // 
            ChartLoadingLabel.AutoSize = true;
            ChartLoadingLabel.BackColor = Color.Transparent;
            ChartLoadingLabel.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            ChartLoadingLabel.Location = new Point(1192, 187);
            ChartLoadingLabel.Name = "ChartLoadingLabel";
            ChartLoadingLabel.Size = new Size(143, 25);
            ChartLoadingLabel.TabIndex = 7;
            ChartLoadingLabel.Text = "Chart Loading...";
            ChartLoadingLabel.Visible = false;
            // 
            // DB_DataSet_StockView_B
            // 
            DB_DataSet_StockView_B.Location = new Point(1171, 384);
            DB_DataSet_StockView_B.Name = "DB_DataSet_StockView_B";
            DB_DataSet_StockView_B.Size = new Size(143, 23);
            DB_DataSet_StockView_B.TabIndex = 8;
            DB_DataSet_StockView_B.Text = "View Candle Chart";
            DB_DataSet_StockView_B.UseVisualStyleBackColor = true;
            DB_DataSet_StockView_B.Visible = false;
            DB_DataSet_StockView_B.Click += DB_DataSet_StockView_B_Click;
            // 
            // CloseButton
            // 
            CloseButton.Location = new Point(1446, 4);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(25, 23);
            CloseButton.TabIndex = 9;
            CloseButton.Text = "🗙";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // DataSetViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(CloseButton);
            Controls.Add(DB_DataSet_StockView_B);
            Controls.Add(ChartLoadingLabel);
            Controls.Add(DataChart);
            Controls.Add(InformationTB);
            Controls.Add(panel1);
            Controls.Add(CangeSourceB);
            Controls.Add(SourceTB);
            Controls.Add(label1);
            Name = "DataSetViewer";
            Size = new Size(1477, 669);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DataChart).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox SourceTB;
        private Button CangeSourceB;
        private TableLayoutPanel Table;
        private Panel panel1;
        private RichTextBox InformationTB;
        private System.Windows.Forms.DataVisualization.Charting.Chart DataChart;
        private Label ChartLoadingLabel;
        private Button DB_DataSet_StockView_B;
        private Button CloseButton;
    }
}
