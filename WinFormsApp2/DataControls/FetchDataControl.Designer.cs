namespace WinFormsApp2.DataControls
{
    partial class FetchDataControl
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
            SourceSelectorUpDown = new DomainUpDown();
            label1 = new Label();
            label2 = new Label();
            richTextBox1 = new RichTextBox();
            ArgumentP = new TableLayoutPanel();
            SubmitB = new Button();
            UseCalcCB = new CheckBox();
            ExitB = new Button();
            SuspendLayout();
            // 
            // SourceSelectorUpDown
            // 
            SourceSelectorUpDown.Location = new Point(86, 3);
            SourceSelectorUpDown.Name = "SourceSelectorUpDown";
            SourceSelectorUpDown.ReadOnly = true;
            SourceSelectorUpDown.Size = new Size(120, 23);
            SourceSelectorUpDown.TabIndex = 0;
            SourceSelectorUpDown.Text = "null";
            SourceSelectorUpDown.SelectedItemChanged += SourceSelectorUpDown_SelectedItemChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 5);
            label1.Name = "label1";
            label1.Size = new Size(77, 15);
            label1.TabIndex = 1;
            label1.Text = "Select Source";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 298);
            label2.Name = "label2";
            label2.Size = new Size(49, 15);
            label2.TabIndex = 2;
            label2.Text = "Request";
            //label2.Click += label2_Click;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(3, 316);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(854, 96);
            richTextBox1.TabIndex = 3;
            richTextBox1.Text = "";
            //richTextBox1.TextChanged += richTextBox1_TextChanged;
            // 
            // ArgumentP
            // 
            ArgumentP.AutoScroll = true;
            ArgumentP.ColumnCount = 1;
            ArgumentP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 74.02597F));
            ArgumentP.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.9740257F));
            ArgumentP.Location = new Point(3, 40);
            ArgumentP.Name = "ArgumentP";
            ArgumentP.RowCount = 1;
            ArgumentP.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            ArgumentP.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            ArgumentP.Size = new Size(854, 255);
            ArgumentP.TabIndex = 10;
            //ArgumentP.Paint += ArgumentP_Paint;
            // 
            // SubmitB
            // 
            SubmitB.Location = new Point(3, 418);
            SubmitB.Name = "SubmitB";
            SubmitB.Size = new Size(75, 23);
            SubmitB.TabIndex = 11;
            SubmitB.Text = "Fetch";
            SubmitB.UseVisualStyleBackColor = true;
            SubmitB.Click += SubmitB_Click;
            // 
            // UseCalcCB
            // 
            UseCalcCB.AutoSize = true;
            UseCalcCB.Checked = true;
            UseCalcCB.CheckState = CheckState.Checked;
            UseCalcCB.Location = new Point(104, 422);
            UseCalcCB.Name = "UseCalcCB";
            UseCalcCB.Size = new Size(159, 19);
            UseCalcCB.TabIndex = 12;
            UseCalcCB.Text = "Calculate Other Columns";
            UseCalcCB.UseVisualStyleBackColor = true;
            // 
            // ExitB
            // 
            ExitB.Location = new Point(834, 11);
            ExitB.Name = "ExitB";
            ExitB.Size = new Size(23, 23);
            ExitB.TabIndex = 13;
            ExitB.Text = "✖";
            ExitB.UseVisualStyleBackColor = true;
            ExitB.Click += ExitB_Click;
            // 
            // FetchDataControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ExitB);
            Controls.Add(UseCalcCB);
            Controls.Add(SubmitB);
            Controls.Add(ArgumentP);
            Controls.Add(richTextBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(SourceSelectorUpDown);
            Name = "FetchDataControl";
            Size = new Size(860, 536);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DomainUpDown SourceSelectorUpDown;
        private Label label1;
        private Label label2;
        private RichTextBox richTextBox1;
        private TableLayoutPanel ArgumentP;
        private Button SubmitB;
        private CheckBox UseCalcCB;
        private Button ExitB;
    }
}
