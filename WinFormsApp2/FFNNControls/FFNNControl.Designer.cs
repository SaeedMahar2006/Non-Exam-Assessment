namespace WinFormsApp2.FFNNControls
{
    partial class FFNNControl
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
            label2 = new Label();
            label3 = new Label();
            SaveB = new Button();
            PauseB = new Button();
            TrainB = new Button();
            label4 = new Label();
            label5 = new Label();
            SelectFFNNB = new Button();
            FFNNSourceTextBox = new TextBox();
            SelectDataSourceB = new Button();
            DataSourceLocation = new TextBox();
            FFNNChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            LearningRateNUpDown = new NumericUpDown();
            ClientsCountLabel = new Label();
            CloseButton = new Button();
            ViewGraphB = new Button();
            ((System.ComponentModel.ISupportInitialize)FFNNChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LearningRateNUpDown).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(69, 30);
            label1.TabIndex = 1;
            label1.Text = "FFNN";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 274);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 2;
            label2.Text = "FFNN Source:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 249);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 3;
            label3.Text = "Data Set:";
            // 
            // SaveB
            // 
            SaveB.Location = new Point(761, 306);
            SaveB.Name = "SaveB";
            SaveB.Size = new Size(61, 25);
            SaveB.TabIndex = 10;
            SaveB.Text = "Save";
            SaveB.UseVisualStyleBackColor = true;
            SaveB.Click += SaveB_Click;
            // 
            // PauseB
            // 
            PauseB.Location = new Point(761, 275);
            PauseB.Name = "PauseB";
            PauseB.Size = new Size(61, 25);
            PauseB.TabIndex = 9;
            PauseB.Text = "Pause";
            PauseB.UseVisualStyleBackColor = true;
            PauseB.Click += PauseB_Click;
            // 
            // TrainB
            // 
            TrainB.Location = new Point(761, 244);
            TrainB.Name = "TrainB";
            TrainB.Size = new Size(61, 25);
            TrainB.TabIndex = 8;
            TrainB.Text = "Train";
            TrainB.UseVisualStyleBackColor = true;
            TrainB.Click += TrainB_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 303);
            label4.Name = "label4";
            label4.Size = new Size(79, 15);
            label4.TabIndex = 11;
            label4.Text = "Learning Rate";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 329);
            label5.Name = "label5";
            label5.Size = new Size(43, 15);
            label5.TabIndex = 12;
            label5.Text = "Clients";
            // 
            // SelectFFNNB
            // 
            SelectFFNNB.Location = new Point(413, 271);
            SelectFFNNB.Name = "SelectFFNNB";
            SelectFFNNB.Size = new Size(56, 23);
            SelectFFNNB.TabIndex = 16;
            SelectFFNNB.Text = "Select";
            SelectFFNNB.UseVisualStyleBackColor = true;
            SelectFFNNB.Click += SelectFFNNB_Click;
            // 
            // FFNNSourceTextBox
            // 
            FFNNSourceTextBox.Location = new Point(88, 271);
            FFNNSourceTextBox.Name = "FFNNSourceTextBox";
            FFNNSourceTextBox.ReadOnly = true;
            FFNNSourceTextBox.Size = new Size(329, 23);
            FFNNSourceTextBox.TabIndex = 15;
            FFNNSourceTextBox.Text = "null";
            // 
            // SelectDataSourceB
            // 
            SelectDataSourceB.Location = new Point(414, 245);
            SelectDataSourceB.Name = "SelectDataSourceB";
            SelectDataSourceB.Size = new Size(56, 23);
            SelectDataSourceB.TabIndex = 14;
            SelectDataSourceB.Text = "Select";
            SelectDataSourceB.UseVisualStyleBackColor = true;
            SelectDataSourceB.Click += SelectDataSourceB_Click;
            // 
            // DataSourceLocation
            // 
            DataSourceLocation.Location = new Point(88, 245);
            DataSourceLocation.Name = "DataSourceLocation";
            DataSourceLocation.ReadOnly = true;
            DataSourceLocation.Size = new Size(329, 23);
            DataSourceLocation.TabIndex = 13;
            DataSourceLocation.Text = "null";
            // 
            // FFNNChart
            // 
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.Name = "ChartArea1";
            FFNNChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            FFNNChart.Legends.Add(legend1);
            FFNNChart.Location = new Point(3, 33);
            FFNNChart.Name = "FFNNChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Error";
            FFNNChart.Series.Add(series1);
            FFNNChart.Size = new Size(819, 206);
            FFNNChart.TabIndex = 19;
            FFNNChart.Text = "chart1";
            // 
            // LearningRateNUpDown
            // 
            LearningRateNUpDown.DecimalPlaces = 3;
            LearningRateNUpDown.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            LearningRateNUpDown.Location = new Point(88, 300);
            LearningRateNUpDown.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            LearningRateNUpDown.Name = "LearningRateNUpDown";
            LearningRateNUpDown.Size = new Size(68, 23);
            LearningRateNUpDown.TabIndex = 20;
            LearningRateNUpDown.ValueChanged += LearningRateNUpDown_ValueChanged;
            // 
            // ClientsCountLabel
            // 
            ClientsCountLabel.AutoSize = true;
            ClientsCountLabel.Location = new Point(88, 329);
            ClientsCountLabel.Name = "ClientsCountLabel";
            ClientsCountLabel.Size = new Size(27, 15);
            ClientsCountLabel.TabIndex = 21;
            ClientsCountLabel.Text = "null";
            // 
            // CloseButton
            // 
            CloseButton.Location = new Point(797, 3);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(25, 23);
            CloseButton.TabIndex = 22;
            CloseButton.Text = "🗙";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // ViewGraphB
            // 
            ViewGraphB.Location = new Point(585, 245);
            ViewGraphB.Name = "ViewGraphB";
            ViewGraphB.Size = new Size(75, 23);
            ViewGraphB.TabIndex = 23;
            ViewGraphB.Text = "View Graph";
            ViewGraphB.UseVisualStyleBackColor = true;
            ViewGraphB.Click += ViewGraphB_Click;
            // 
            // FFNNControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ViewGraphB);
            Controls.Add(CloseButton);
            Controls.Add(ClientsCountLabel);
            Controls.Add(LearningRateNUpDown);
            Controls.Add(FFNNChart);
            Controls.Add(SelectFFNNB);
            Controls.Add(FFNNSourceTextBox);
            Controls.Add(SelectDataSourceB);
            Controls.Add(DataSourceLocation);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(SaveB);
            Controls.Add(PauseB);
            Controls.Add(TrainB);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FFNNControl";
            Size = new Size(825, 420);
            ((System.ComponentModel.ISupportInitialize)FFNNChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)LearningRateNUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label label3;
        private Button SaveB;
        private Button PauseB;
        private Button TrainB;
        private Label label4;
        private Label label5;
        private Button SelectFFNNB;
        private TextBox FFNNSourceTextBox;
        private Button SelectDataSourceB;
        private TextBox DataSourceLocation;
        private System.Windows.Forms.DataVisualization.Charting.Chart FFNNChart;
        private NumericUpDown LearningRateNUpDown;
        private Label ClientsCountLabel;
        private Button CloseButton;
        private Button ViewGraphB;
    }
}
