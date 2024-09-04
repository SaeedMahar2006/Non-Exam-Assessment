namespace WinFormsApp2.MainControls
{
    partial class SettingsSelector
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
            ArgumentL = new Label();
            PathSelectorB = new Button();
            PathSelectorTB = new TextBox();
            generalTB = new TextBox();
            categorySelectorUpDown = new DomainUpDown();
            numericUpDown1 = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // ArgumentL
            // 
            ArgumentL.AutoSize = true;
            ArgumentL.Location = new Point(3, 12);
            ArgumentL.Name = "ArgumentL";
            ArgumentL.Size = new Size(27, 15);
            ArgumentL.TabIndex = 0;
            ArgumentL.Text = "null";
            // 
            // PathSelectorB
            // 
            PathSelectorB.Location = new Point(732, 8);
            PathSelectorB.Name = "PathSelectorB";
            PathSelectorB.Size = new Size(75, 23);
            PathSelectorB.TabIndex = 1;
            PathSelectorB.Text = "Select";
            PathSelectorB.UseVisualStyleBackColor = true;
            PathSelectorB.Visible = false;
            PathSelectorB.Click += PathSelectorB_Click;
            // 
            // PathSelectorTB
            // 
            PathSelectorTB.Location = new Point(198, 8);
            PathSelectorTB.Name = "PathSelectorTB";
            PathSelectorTB.ReadOnly = true;
            PathSelectorTB.Size = new Size(528, 23);
            PathSelectorTB.TabIndex = 2;
            PathSelectorTB.Visible = false;
            // 
            // generalTB
            // 
            generalTB.Location = new Point(198, 9);
            generalTB.Name = "generalTB";
            generalTB.Size = new Size(609, 23);
            generalTB.TabIndex = 3;
            generalTB.Visible = false;
            generalTB.TextChanged += generalTB_TextChanged;
            // 
            // categorySelectorUpDown
            // 
            categorySelectorUpDown.Location = new Point(198, 9);
            categorySelectorUpDown.Name = "categorySelectorUpDown";
            categorySelectorUpDown.Size = new Size(609, 23);
            categorySelectorUpDown.TabIndex = 4;
            categorySelectorUpDown.Visible = false;
            categorySelectorUpDown.SelectedItemChanged += categorySelectorUpDown_SelectedItemChanged;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(198, 9);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(609, 23);
            numericUpDown1.TabIndex = 5;
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // SettingsSelector
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(numericUpDown1);
            Controls.Add(categorySelectorUpDown);
            Controls.Add(generalTB);
            Controls.Add(PathSelectorTB);
            Controls.Add(PathSelectorB);
            Controls.Add(ArgumentL);
            Name = "SettingsSelector";
            Size = new Size(818, 40);
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ArgumentL;
        private Button PathSelectorB;
        private TextBox PathSelectorTB;
        private TextBox generalTB;
        private DomainUpDown categorySelectorUpDown;
        private NumericUpDown numericUpDown1;
    }
}
