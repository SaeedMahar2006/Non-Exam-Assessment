namespace WinFormsApp2.MainControls
{
    partial class SettingsControl
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
            SettingsScrollPanel = new TableLayoutPanel();
            label1 = new Label();
            ExitB = new Button();
            SuspendLayout();
            // 
            // SettingsScrollPanel
            // 
            SettingsScrollPanel.AutoScroll = true;
            SettingsScrollPanel.ColumnCount = 2;
            SettingsScrollPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            SettingsScrollPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            SettingsScrollPanel.Location = new Point(3, 18);
            SettingsScrollPanel.Name = "SettingsScrollPanel";
            SettingsScrollPanel.RowCount = 2;
            SettingsScrollPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            SettingsScrollPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            SettingsScrollPanel.Size = new Size(1040, 334);
            SettingsScrollPanel.TabIndex = 0;
            SettingsScrollPanel.Paint += SettingsScrollPanel_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(49, 15);
            label1.TabIndex = 1;
            label1.Text = "Settings";
            label1.Click += label1_Click;
            // 
            // ExitB
            // 
            ExitB.Location = new Point(1020, 0);
            ExitB.Name = "ExitB";
            ExitB.Size = new Size(23, 23);
            ExitB.TabIndex = 11;
            ExitB.Text = "✖";
            ExitB.UseVisualStyleBackColor = true;
            ExitB.Click += ExitB_Click;
            // 
            // SettingsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ExitB);
            Controls.Add(label1);
            Controls.Add(SettingsScrollPanel);
            Name = "SettingsControl";
            Size = new Size(1049, 485);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel SettingsScrollPanel;
        private Label label1;
        private Button ExitB;
    }
}
