namespace WinFormsApp2.DataControls
{
    partial class MakePredictionControl
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
            InputTablePanel = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            SourceTB = new TextBox();
            ChangeB = new Button();
            InfoLabel = new Label();
            MakePredictionB = new Button();
            label3 = new Label();
            PredictionLabel = new Label();
            ExitB = new Button();
            panel1 = new Panel();
            TrackBar = new TrackBar();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TrackBar).BeginInit();
            SuspendLayout();
            // 
            // InputTablePanel
            // 
            InputTablePanel.AutoScroll = true;
            InputTablePanel.ColumnCount = 2;
            InputTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            InputTablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            InputTablePanel.Location = new Point(6, 144);
            InputTablePanel.Name = "InputTablePanel";
            InputTablePanel.RowCount = 2;
            InputTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 53F));
            InputTablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 47F));
            InputTablePanel.Size = new Size(660, 154);
            InputTablePanel.TabIndex = 0;
            InputTablePanel.Paint += InputTablePanel_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 1;
            label1.Text = "Prediction";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 24);
            label2.Name = "label2";
            label2.Size = new Size(43, 15);
            label2.TabIndex = 2;
            label2.Text = "Source";
            // 
            // SourceTB
            // 
            SourceTB.Location = new Point(55, 21);
            SourceTB.Name = "SourceTB";
            SourceTB.ReadOnly = true;
            SourceTB.Size = new Size(525, 23);
            SourceTB.TabIndex = 3;
            // 
            // ChangeB
            // 
            ChangeB.Location = new Point(586, 24);
            ChangeB.Name = "ChangeB";
            ChangeB.Size = new Size(75, 23);
            ChangeB.TabIndex = 4;
            ChangeB.Text = "Change";
            ChangeB.UseVisualStyleBackColor = true;
            ChangeB.Click += ChangeB_Click;
            // 
            // InfoLabel
            // 
            InfoLabel.AutoSize = true;
            InfoLabel.Location = new Point(8, 9);
            InfoLabel.Name = "InfoLabel";
            InfoLabel.Size = new Size(0, 15);
            InfoLabel.TabIndex = 5;
            InfoLabel.Click += InfoLabel_Click;
            // 
            // MakePredictionB
            // 
            MakePredictionB.Location = new Point(0, 312);
            MakePredictionB.Name = "MakePredictionB";
            MakePredictionB.Size = new Size(127, 24);
            MakePredictionB.TabIndex = 6;
            MakePredictionB.Text = "Make Prediction";
            MakePredictionB.UseVisualStyleBackColor = true;
            MakePredictionB.Click += MakePredictionB_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 360);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 7;
            label3.Text = "Prediction:";
            label3.Click += label3_Click;
            // 
            // PredictionLabel
            // 
            PredictionLabel.AutoSize = true;
            PredictionLabel.Location = new Point(173, 342);
            PredictionLabel.Name = "PredictionLabel";
            PredictionLabel.Size = new Size(27, 15);
            PredictionLabel.TabIndex = 8;
            PredictionLabel.Text = "null";
            PredictionLabel.Click += PredictionLabel_Click;
            // 
            // ExitB
            // 
            ExitB.Location = new Point(638, 0);
            ExitB.Name = "ExitB";
            ExitB.Size = new Size(23, 23);
            ExitB.TabIndex = 9;
            ExitB.Text = "✖";
            ExitB.UseVisualStyleBackColor = true;
            ExitB.Click += ExitB_Click;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(InfoLabel);
            panel1.Location = new Point(6, 50);
            panel1.Name = "panel1";
            panel1.Size = new Size(660, 88);
            panel1.TabIndex = 10;
            // 
            // TrackBar
            // 
            TrackBar.Enabled = false;
            TrackBar.LargeChange = 1;
            TrackBar.Location = new Point(82, 360);
            TrackBar.Minimum = -10;
            TrackBar.Name = "TrackBar";
            TrackBar.Size = new Size(204, 45);
            TrackBar.TabIndex = 11;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(259, 390);
            label4.Name = "label4";
            label4.Size = new Size(27, 15);
            label4.TabIndex = 12;
            label4.Text = "buy";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(82, 390);
            label5.Name = "label5";
            label5.Size = new Size(24, 15);
            label5.TabIndex = 13;
            label5.Text = "sell";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(169, 390);
            label6.Name = "label6";
            label6.Size = new Size(31, 15);
            label6.TabIndex = 14;
            label6.Text = "hold";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(319, 360);
            label7.Name = "label7";
            label7.Size = new Size(347, 30);
            label7.TabIndex = 15;
            label7.Text = "Only neural networks with a 1 dimesnional output in range [-1,1]\r\nare supported for the prediction slider\r\n";
            // 
            // MakePredictionControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(PredictionLabel);
            Controls.Add(TrackBar);
            Controls.Add(panel1);
            Controls.Add(ExitB);
            Controls.Add(label3);
            Controls.Add(MakePredictionB);
            Controls.Add(ChangeB);
            Controls.Add(SourceTB);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(InputTablePanel);
            Name = "MakePredictionControl";
            Size = new Size(678, 411);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)TrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel InputTablePanel;
        private Label label1;
        private Label label2;
        private TextBox SourceTB;
        private Button ChangeB;
        private Label InfoLabel;
        private Button MakePredictionB;
        private Label label3;
        private Label PredictionLabel;
        private Button ExitB;
        private Panel panel1;
        private TrackBar TrackBar;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
    }
}
