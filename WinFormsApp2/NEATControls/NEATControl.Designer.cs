namespace WindowsFormsApp1
{
    partial class NEATControl
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
            NEAT_Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            label1 = new Label();
            OpenDatafileDialogue = new OpenFileDialog();
            label = new Label();
            DataSourceLocation = new TextBox();
            SelectDataSourceB = new Button();
            TrainB = new Button();
            PauseB = new Button();
            SaveB = new Button();
            label2 = new Label();
            NeatSourceTextBox = new TextBox();
            SelectNeatB = new Button();
            ParametersLabel = new Label();
            CloseButton = new Button();
            ViewAsGraphB = new Button();
            CreaturesNUpDown = new NumericUpDown();
            C2NUpDown = new NumericUpDown();
            KillRateNUpDown = new NumericUpDown();
            SpecieDistanceNUpDown = new NumericUpDown();
            C1NUpDown = new NumericUpDown();
            C3NUpDown = new NumericUpDown();
            label3 = new Label();
            MToggleNUpDown = new NumericUpDown();
            MWeightShiftNUpDown = new NumericUpDown();
            MConnNUpDown = new NumericUpDown();
            WeightShiftStrengthNUpDown = new NumericUpDown();
            MNodeNUpDown = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)NEAT_Chart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)CreaturesNUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)C2NUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)KillRateNUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpecieDistanceNUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)C1NUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)C3NUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MToggleNUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MWeightShiftNUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MConnNUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WeightShiftStrengthNUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MNodeNUpDown).BeginInit();
            SuspendLayout();
            // 
            // NEAT_Chart
            // 
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.Name = "ChartArea1";
            NEAT_Chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            NEAT_Chart.Legends.Add(legend1);
            NEAT_Chart.Location = new Point(3, 23);
            NEAT_Chart.Name = "NEAT_Chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Performance";
            series1.YValuesPerPoint = 2;
            NEAT_Chart.Series.Add(series1);
            NEAT_Chart.Size = new Size(840, 184);
            NEAT_Chart.TabIndex = 0;
            NEAT_Chart.Text = "chart1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(48, 20);
            label1.TabIndex = 1;
            label1.Text = "NEAT";
            // 
            // OpenDatafileDialogue
            // 
            OpenDatafileDialogue.FileName = "openFileDialog1";
            // 
            // label
            // 
            label.AutoSize = true;
            label.Location = new Point(6, 214);
            label.Name = "label";
            label.Size = new Size(73, 15);
            label.TabIndex = 2;
            label.Text = "Data Source:";
            // 
            // DataSourceLocation
            // 
            DataSourceLocation.Location = new Point(82, 210);
            DataSourceLocation.Name = "DataSourceLocation";
            DataSourceLocation.ReadOnly = true;
            DataSourceLocation.Size = new Size(329, 23);
            DataSourceLocation.TabIndex = 3;
            DataSourceLocation.Text = "null";
            // 
            // SelectDataSourceB
            // 
            SelectDataSourceB.Location = new Point(408, 210);
            SelectDataSourceB.Name = "SelectDataSourceB";
            SelectDataSourceB.Size = new Size(56, 23);
            SelectDataSourceB.TabIndex = 4;
            SelectDataSourceB.Text = "Select";
            SelectDataSourceB.UseVisualStyleBackColor = true;
            SelectDataSourceB.Click += SelectDataSourceB_Click;
            // 
            // TrainB
            // 
            TrainB.Location = new Point(550, 210);
            TrainB.Name = "TrainB";
            TrainB.Size = new Size(54, 25);
            TrainB.TabIndex = 5;
            TrainB.Text = "Train";
            TrainB.UseVisualStyleBackColor = true;
            TrainB.Click += TrainB_Click;
            // 
            // PauseB
            // 
            PauseB.Location = new Point(610, 210);
            PauseB.Name = "PauseB";
            PauseB.Size = new Size(54, 25);
            PauseB.TabIndex = 6;
            PauseB.Text = "Pause";
            PauseB.UseVisualStyleBackColor = true;
            PauseB.Click += PauseB_Click;
            // 
            // SaveB
            // 
            SaveB.Location = new Point(670, 210);
            SaveB.Name = "SaveB";
            SaveB.Size = new Size(54, 25);
            SaveB.TabIndex = 7;
            SaveB.Text = "Save";
            SaveB.UseVisualStyleBackColor = true;
            SaveB.Click += SaveB_Click_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 241);
            label2.Name = "label2";
            label2.Size = new Size(77, 15);
            label2.TabIndex = 8;
            label2.Text = "NEAT Source:";
            // 
            // NeatSourceTextBox
            // 
            NeatSourceTextBox.Location = new Point(82, 236);
            NeatSourceTextBox.Name = "NeatSourceTextBox";
            NeatSourceTextBox.ReadOnly = true;
            NeatSourceTextBox.Size = new Size(329, 23);
            NeatSourceTextBox.TabIndex = 9;
            NeatSourceTextBox.Text = "null";
            // 
            // SelectNeatB
            // 
            SelectNeatB.Location = new Point(407, 236);
            SelectNeatB.Name = "SelectNeatB";
            SelectNeatB.Size = new Size(56, 23);
            SelectNeatB.TabIndex = 10;
            SelectNeatB.Text = "Select";
            SelectNeatB.UseVisualStyleBackColor = true;
            SelectNeatB.Click += SelectNeatB_Click;
            // 
            // ParametersLabel
            // 
            ParametersLabel.AutoSize = true;
            ParametersLabel.Location = new Point(6, 262);
            ParametersLabel.Name = "ParametersLabel";
            ParametersLabel.Size = new Size(92, 210);
            ParametersLabel.TabIndex = 11;
            ParametersLabel.Text = "Parameters\r\n\r\nCreatures:\r\nSpecies:\r\n\r\nKill Rate:\r\n\r\nSpecie Distance:\r\n\r\nC1:\r\n\r\nC2:\r\n\r\nC3:\r\n";
            //ParametersLabel.Click += ParametersLabel_Click;
            // 
            // CloseButton
            // 
            CloseButton.Location = new Point(849, 3);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(25, 23);
            CloseButton.TabIndex = 23;
            CloseButton.Text = "🗙";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // ViewAsGraphB
            // 
            ViewAsGraphB.Location = new Point(789, 213);
            ViewAsGraphB.Name = "ViewAsGraphB";
            ViewAsGraphB.Size = new Size(54, 23);
            ViewAsGraphB.TabIndex = 24;
            ViewAsGraphB.Text = "Graph";
            ViewAsGraphB.UseVisualStyleBackColor = true;
            ViewAsGraphB.Click += ViewAsGraphB_Click;
            // 
            // CreaturesNUpDown
            // 
            CreaturesNUpDown.Location = new Point(104, 288);
            CreaturesNUpDown.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            CreaturesNUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            CreaturesNUpDown.Name = "CreaturesNUpDown";
            CreaturesNUpDown.ReadOnly = true;
            CreaturesNUpDown.Size = new Size(150, 23);
            CreaturesNUpDown.TabIndex = 25;
            CreaturesNUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            CreaturesNUpDown.ValueChanged += CreaturesNUpDown_ValueChanged;
            // 
            // C2NUpDown
            // 
            C2NUpDown.DecimalPlaces = 2;
            C2NUpDown.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            C2NUpDown.Location = new Point(104, 418);
            C2NUpDown.Name = "C2NUpDown";
            C2NUpDown.Size = new Size(150, 23);
            C2NUpDown.TabIndex = 26;
            C2NUpDown.ValueChanged += C2NUpDown_ValueChanged;
            // 
            // KillRateNUpDown
            // 
            KillRateNUpDown.DecimalPlaces = 3;
            KillRateNUpDown.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            KillRateNUpDown.Location = new Point(104, 331);
            KillRateNUpDown.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            KillRateNUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 196608 });
            KillRateNUpDown.Name = "KillRateNUpDown";
            KillRateNUpDown.Size = new Size(150, 23);
            KillRateNUpDown.TabIndex = 27;
            KillRateNUpDown.Value = new decimal(new int[] { 1, 0, 0, 131072 });
            KillRateNUpDown.ValueChanged += KillRateNUpDown_ValueChanged;
            // 
            // SpecieDistanceNUpDown
            // 
            SpecieDistanceNUpDown.DecimalPlaces = 2;
            SpecieDistanceNUpDown.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            SpecieDistanceNUpDown.Location = new Point(104, 360);
            SpecieDistanceNUpDown.Name = "SpecieDistanceNUpDown";
            SpecieDistanceNUpDown.Size = new Size(150, 23);
            SpecieDistanceNUpDown.TabIndex = 28;
            SpecieDistanceNUpDown.Value = new decimal(new int[] { 1, 0, 0, 65536 });
            SpecieDistanceNUpDown.ValueChanged += SpecieDistanceNUpDown_ValueChanged;
            // 
            // C1NUpDown
            // 
            C1NUpDown.DecimalPlaces = 2;
            C1NUpDown.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            C1NUpDown.Location = new Point(104, 389);
            C1NUpDown.Name = "C1NUpDown";
            C1NUpDown.Size = new Size(150, 23);
            C1NUpDown.TabIndex = 29;
            C1NUpDown.ValueChanged += C1NUpDown_ValueChanged;
            // 
            // C3NUpDown
            // 
            C3NUpDown.DecimalPlaces = 2;
            C3NUpDown.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            C3NUpDown.Location = new Point(104, 449);
            C3NUpDown.Name = "C3NUpDown";
            C3NUpDown.Size = new Size(150, 23);
            C3NUpDown.TabIndex = 30;
            C3NUpDown.ValueChanged += C3NUpDown_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(397, 262);
            label3.Name = "label3";
            label3.Size = new Size(151, 165);
            label3.TabIndex = 31;
            label3.Text = "\r\n\r\nMutate Node:\r\n\r\nMutate Connection:\r\n\r\nMutate Weight Shift:\r\n\r\nWeight Shift Strength:\r\n\r\nMutate Toggle Connection:\r\n";
            // 
            // MToggleNUpDown
            // 
            MToggleNUpDown.DecimalPlaces = 3;
            MToggleNUpDown.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            MToggleNUpDown.Location = new Point(554, 407);
            MToggleNUpDown.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            MToggleNUpDown.Name = "MToggleNUpDown";
            MToggleNUpDown.Size = new Size(150, 23);
            MToggleNUpDown.TabIndex = 35;
            MToggleNUpDown.ValueChanged += MToggleNUpDown_ValueChanged;
            // 
            // MWeightShiftNUpDown
            // 
            MWeightShiftNUpDown.DecimalPlaces = 3;
            MWeightShiftNUpDown.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            MWeightShiftNUpDown.Location = new Point(554, 347);
            MWeightShiftNUpDown.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            MWeightShiftNUpDown.Name = "MWeightShiftNUpDown";
            MWeightShiftNUpDown.Size = new Size(150, 23);
            MWeightShiftNUpDown.TabIndex = 34;
            MWeightShiftNUpDown.ValueChanged += MWeightShiftNUpDown_ValueChanged;
            // 
            // MConnNUpDown
            // 
            MConnNUpDown.DecimalPlaces = 3;
            MConnNUpDown.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            MConnNUpDown.Location = new Point(554, 318);
            MConnNUpDown.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            MConnNUpDown.Name = "MConnNUpDown";
            MConnNUpDown.Size = new Size(150, 23);
            MConnNUpDown.TabIndex = 33;
            MConnNUpDown.ValueChanged += MConnNUpDown_ValueChanged;
            // 
            // WeightShiftStrengthNUpDown
            // 
            WeightShiftStrengthNUpDown.DecimalPlaces = 2;
            WeightShiftStrengthNUpDown.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            WeightShiftStrengthNUpDown.Location = new Point(554, 376);
            WeightShiftStrengthNUpDown.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            WeightShiftStrengthNUpDown.Name = "WeightShiftStrengthNUpDown";
            WeightShiftStrengthNUpDown.Size = new Size(150, 23);
            WeightShiftStrengthNUpDown.TabIndex = 32;
            WeightShiftStrengthNUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            WeightShiftStrengthNUpDown.ValueChanged += WeightShiftStrengthNUpDown_ValueChanged;
            // 
            // MNodeNUpDown
            // 
            MNodeNUpDown.DecimalPlaces = 3;
            MNodeNUpDown.Increment = new decimal(new int[] { 1, 0, 0, 196608 });
            MNodeNUpDown.Location = new Point(554, 288);
            MNodeNUpDown.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            MNodeNUpDown.Name = "MNodeNUpDown";
            MNodeNUpDown.Size = new Size(150, 23);
            MNodeNUpDown.TabIndex = 36;
            MNodeNUpDown.ValueChanged += MNodeNUpDown_ValueChanged;
            // 
            // NEATControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(MNodeNUpDown);
            Controls.Add(MToggleNUpDown);
            Controls.Add(MWeightShiftNUpDown);
            Controls.Add(MConnNUpDown);
            Controls.Add(WeightShiftStrengthNUpDown);
            Controls.Add(label3);
            Controls.Add(C3NUpDown);
            Controls.Add(C1NUpDown);
            Controls.Add(SpecieDistanceNUpDown);
            Controls.Add(KillRateNUpDown);
            Controls.Add(C2NUpDown);
            Controls.Add(CreaturesNUpDown);
            Controls.Add(ViewAsGraphB);
            Controls.Add(CloseButton);
            Controls.Add(ParametersLabel);
            Controls.Add(SelectNeatB);
            Controls.Add(NeatSourceTextBox);
            Controls.Add(label2);
            Controls.Add(SaveB);
            Controls.Add(PauseB);
            Controls.Add(TrainB);
            Controls.Add(SelectDataSourceB);
            Controls.Add(DataSourceLocation);
            Controls.Add(label);
            Controls.Add(label1);
            Controls.Add(NEAT_Chart);
            Margin = new Padding(4, 3, 4, 3);
            Name = "NEATControl";
            Size = new Size(877, 508);
            //Load += NEATControl_Load;
            ((System.ComponentModel.ISupportInitialize)NEAT_Chart).EndInit();
            ((System.ComponentModel.ISupportInitialize)CreaturesNUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)C2NUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)KillRateNUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpecieDistanceNUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)C1NUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)C3NUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)MToggleNUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)MWeightShiftNUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)MConnNUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)WeightShiftStrengthNUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)MNodeNUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart NEAT_Chart;
        private Label label1;
        private OpenFileDialog OpenDatafileDialogue;
        private Label label;
        private TextBox DataSourceLocation;
        private Button SelectDataSourceB;
        SimpleTextBinding DataSourceTextBox_Binding = new SimpleTextBinding();
        private Button TrainB;
        private Button PauseB;
        private Button SaveB;
        private Label label2;
        private TextBox NeatSourceTextBox;
        private Button SelectNeatB;
        SimpleTextBinding NeatSourceTextBox_Binding = new SimpleTextBinding();
        private Label ParametersLabel;
        private Button CloseButton;
        private Button ViewAsGraphB;
        private NumericUpDown CreaturesNUpDown;
        private NumericUpDown C2NUpDown;
        private NumericUpDown KillRateNUpDown;
        private NumericUpDown SpecieDistanceNUpDown;
        private NumericUpDown C1NUpDown;
        private NumericUpDown C3NUpDown;
        private Label label3;
        private NumericUpDown MToggleNUpDown;
        private NumericUpDown MWeightShiftNUpDown;
        private NumericUpDown MConnNUpDown;
        private NumericUpDown WeightShiftStrengthNUpDown;
        private NumericUpDown MNodeNUpDown;
    }
}
