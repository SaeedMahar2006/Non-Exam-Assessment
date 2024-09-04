namespace WinFormsApp2.MainControls
{
    partial class CreateSelector
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
            DataSet_RB = new RadioButton();
            FFNN_RB = new RadioButton();
            NEAT_RB = new RadioButton();
            Create_B = new Button();
            DataTable_RB = new RadioButton();
            ExitB = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(114, 15);
            label1.TabIndex = 0;
            label1.Text = "Select item to create";
            // 
            // DataSet_RB
            // 
            DataSet_RB.AutoSize = true;
            DataSet_RB.Location = new Point(14, 18);
            DataSet_RB.Name = "DataSet_RB";
            DataSet_RB.Size = new Size(68, 19);
            DataSet_RB.TabIndex = 1;
            DataSet_RB.TabStop = true;
            DataSet_RB.Text = "Data Set";
            DataSet_RB.UseVisualStyleBackColor = true;
            // 
            // FFNN_RB
            // 
            FFNN_RB.AutoSize = true;
            FFNN_RB.Location = new Point(14, 68);
            FFNN_RB.Name = "FFNN_RB";
            FFNN_RB.Size = new Size(55, 19);
            FFNN_RB.TabIndex = 2;
            FFNN_RB.TabStop = true;
            FFNN_RB.Text = "FFNN";
            FFNN_RB.UseVisualStyleBackColor = true;
            // 
            // NEAT_RB
            // 
            NEAT_RB.AutoSize = true;
            NEAT_RB.Location = new Point(14, 93);
            NEAT_RB.Name = "NEAT_RB";
            NEAT_RB.Size = new Size(53, 19);
            NEAT_RB.TabIndex = 3;
            NEAT_RB.TabStop = true;
            NEAT_RB.Text = "NEAT";
            NEAT_RB.UseVisualStyleBackColor = true;
            // 
            // Create_B
            // 
            Create_B.Location = new Point(3, 123);
            Create_B.Name = "Create_B";
            Create_B.Size = new Size(128, 23);
            Create_B.TabIndex = 4;
            Create_B.Text = "Create";
            Create_B.UseVisualStyleBackColor = true;
            Create_B.Click += Create_B_Click;
            // 
            // DataTable_RB
            // 
            DataTable_RB.AutoSize = true;
            DataTable_RB.Location = new Point(14, 43);
            DataTable_RB.Name = "DataTable_RB";
            DataTable_RB.Size = new Size(79, 19);
            DataTable_RB.TabIndex = 5;
            DataTable_RB.TabStop = true;
            DataTable_RB.Text = "Data Table";
            DataTable_RB.UseVisualStyleBackColor = true;
            DataTable_RB.CheckedChanged += DataTable_RB_CheckedChanged;
            // 
            // ExitB
            // 
            ExitB.Location = new Point(212, 0);
            ExitB.Name = "ExitB";
            ExitB.Size = new Size(23, 23);
            ExitB.TabIndex = 10;
            ExitB.Text = "✖";
            ExitB.UseVisualStyleBackColor = true;
            ExitB.Click += ExitB_Click;
            // 
            // CreateSelector
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ExitB);
            Controls.Add(DataTable_RB);
            Controls.Add(Create_B);
            Controls.Add(NEAT_RB);
            Controls.Add(FFNN_RB);
            Controls.Add(DataSet_RB);
            Controls.Add(label1);
            Name = "CreateSelector";
            Size = new Size(238, 154);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private RadioButton DataSet_RB;
        private RadioButton FFNN_RB;
        private RadioButton NEAT_RB;
        private Button Create_B;
        private RadioButton DataTable_RB;
        private Button ExitB;
    }
}
