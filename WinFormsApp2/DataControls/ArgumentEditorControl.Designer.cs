namespace WinFormsApp2.DataControls
{
    partial class ArgumentEditorControl
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
            textBox1 = new TextBox();
            domainUpDown1 = new DomainUpDown();
            numericUpDown = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)numericUpDown).BeginInit();
            SuspendLayout();
            // 
            // ArgumentL
            // 
            ArgumentL.AutoSize = true;
            ArgumentL.Location = new Point(3, 9);
            ArgumentL.Name = "ArgumentL";
            ArgumentL.Size = new Size(27, 15);
            ArgumentL.TabIndex = 0;
            ArgumentL.Text = "null";
            ArgumentL.Click += ArgumentL_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(143, 6);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(376, 23);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.Validating += textBox1_Validating;
            // 
            // domainUpDown1
            // 
            domainUpDown1.Location = new Point(143, 7);
            domainUpDown1.Name = "domainUpDown1";
            domainUpDown1.Size = new Size(376, 23);
            domainUpDown1.TabIndex = 2;
            domainUpDown1.Text = "domainUpDown1";
            domainUpDown1.SelectedItemChanged += domainUpDown1_SelectedItemChanged;
            // 
            // numericUpDown
            // 
            numericUpDown.DecimalPlaces = 3;
            numericUpDown.Location = new Point(143, 7);
            numericUpDown.Name = "numericUpDown";
            numericUpDown.Size = new Size(376, 23);
            numericUpDown.TabIndex = 3;
            numericUpDown.ValueChanged += numericUpDown_ValueChanged;
            // 
            // ArgumentEditorControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(numericUpDown);
            Controls.Add(domainUpDown1);
            Controls.Add(textBox1);
            Controls.Add(ArgumentL);
            Name = "ArgumentEditorControl";
            Size = new Size(527, 32);
            Load += ArgumentEditorControl_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ArgumentL;
        private TextBox textBox1;
        private DomainUpDown domainUpDown1;
        private NumericUpDown numericUpDown;
    }
}
