namespace WinFormsApp2.FFNNControls
{
    partial class FFNNCreateControl
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
            label1 = new Label();
            label2 = new Label();
            Clients_NUpDown = new NumericUpDown();
            label3 = new Label();
            LearningRate_NUpDown = new NumericUpDown();
            label4 = new Label();
            InputDimension_NUpDown = new NumericUpDown();
            label5 = new Label();
            OutputDimension_NUpDown = new NumericUpDown();
            AddHiddenLayer_B = new Button();
            HiddenLayerP = new TableLayoutPanel();
            Create_B = new Button();
            SaveFFNNDialogue = new SaveFileDialog();
            CloseButton = new Button();
            ((System.ComponentModel.ISupportInitialize)Clients_NUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LearningRate_NUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)InputDimension_NUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)OutputDimension_NUpDown).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(99, 15);
            label1.TabIndex = 0;
            label1.Text = "Create new FFNN";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 57);
            label2.Name = "label2";
            label2.Size = new Size(79, 15);
            label2.TabIndex = 1;
            label2.Text = "Learning Rate";
            // 
            // Clients_NUpDown
            // 
            Clients_NUpDown.Location = new Point(114, 26);
            Clients_NUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            Clients_NUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            Clients_NUpDown.Name = "Clients_NUpDown";
            Clients_NUpDown.Size = new Size(120, 23);
            Clients_NUpDown.TabIndex = 2;
            Clients_NUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 27);
            label3.Name = "label3";
            label3.Size = new Size(43, 15);
            label3.TabIndex = 3;
            label3.Text = "Clients";
            // 
            // LearningRate_NUpDown
            // 
            LearningRate_NUpDown.DecimalPlaces = 3;
            LearningRate_NUpDown.Increment = new decimal(new int[] { 5, 0, 0, 196608 });
            LearningRate_NUpDown.Location = new Point(114, 55);
            LearningRate_NUpDown.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            LearningRate_NUpDown.Name = "LearningRate_NUpDown";
            LearningRate_NUpDown.Size = new Size(120, 23);
            LearningRate_NUpDown.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 90);
            label4.Name = "label4";
            label4.Size = new Size(95, 15);
            label4.TabIndex = 5;
            label4.Text = "Input Dimension";
            // 
            // InputDimension_NUpDown
            // 
            InputDimension_NUpDown.Location = new Point(114, 88);
            InputDimension_NUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            InputDimension_NUpDown.Name = "InputDimension_NUpDown";
            InputDimension_NUpDown.Size = new Size(120, 23);
            InputDimension_NUpDown.TabIndex = 6;
            InputDimension_NUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(3, 256);
            label5.Name = "label5";
            label5.Size = new Size(105, 15);
            label5.TabIndex = 7;
            label5.Text = "Output Dimension";
            label5.Click += label5_Click;
            // 
            // OutputDimension_NUpDown
            // 
            OutputDimension_NUpDown.Location = new Point(114, 254);
            OutputDimension_NUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            OutputDimension_NUpDown.Name = "OutputDimension_NUpDown";
            OutputDimension_NUpDown.Size = new Size(120, 23);
            OutputDimension_NUpDown.TabIndex = 8;
            OutputDimension_NUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // AddHiddenLayer_B
            // 
            AddHiddenLayer_B.Location = new Point(236, 254);
            AddHiddenLayer_B.Name = "AddHiddenLayer_B";
            AddHiddenLayer_B.Size = new Size(75, 23);
            AddHiddenLayer_B.TabIndex = 0;
            AddHiddenLayer_B.Text = "Add";
            AddHiddenLayer_B.UseVisualStyleBackColor = true;
            AddHiddenLayer_B.Click += AddHiddenLayer_B_Click;
            // 
            // HiddenLayerP
            // 
            HiddenLayerP.AutoScroll = true;
            HiddenLayerP.ColumnCount = 1;
            HiddenLayerP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 74.02597F));
            HiddenLayerP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.9740257F));
            HiddenLayerP.Location = new Point(3, 117);
            HiddenLayerP.Name = "HiddenLayerP";
            HiddenLayerP.RowCount = 1;
            HiddenLayerP.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            HiddenLayerP.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            HiddenLayerP.Size = new Size(308, 136);
            HiddenLayerP.TabIndex = 9;
            HiddenLayerP.Paint += HiddenLayerP_Paint;
            // 
            // Create_B
            // 
            Create_B.Location = new Point(3, 282);
            Create_B.Name = "Create_B";
            Create_B.Size = new Size(308, 23);
            Create_B.TabIndex = 10;
            Create_B.Text = "Create";
            Create_B.UseVisualStyleBackColor = true;
            Create_B.Click += Create_B_Click;
            // 
            // CloseButton
            // 
            CloseButton.Location = new Point(286, 3);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(25, 23);
            CloseButton.TabIndex = 23;
            CloseButton.Text = "🗙";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // FFNNCreateControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(CloseButton);
            Controls.Add(Create_B);
            Controls.Add(HiddenLayerP);
            Controls.Add(AddHiddenLayer_B);
            Controls.Add(OutputDimension_NUpDown);
            Controls.Add(label5);
            Controls.Add(InputDimension_NUpDown);
            Controls.Add(label4);
            Controls.Add(LearningRate_NUpDown);
            Controls.Add(label3);
            Controls.Add(Clients_NUpDown);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "FFNNCreateControl";
            Size = new Size(314, 308);
            ((System.ComponentModel.ISupportInitialize)Clients_NUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)LearningRate_NUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)InputDimension_NUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)OutputDimension_NUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private NumericUpDown Clients_NUpDown;
        private Label label3;
        private NumericUpDown LearningRate_NUpDown;
        private Label label4;
        private NumericUpDown InputDimension_NUpDown;
        private Label label5;
        private NumericUpDown OutputDimension_NUpDown;
        private Button AddHiddenLayer_B;
        private TableLayoutPanel HiddenLayerP;
        private Button Create_B;
        private SaveFileDialog SaveFFNNDialogue;
        private Button CloseButton;
    }
}
