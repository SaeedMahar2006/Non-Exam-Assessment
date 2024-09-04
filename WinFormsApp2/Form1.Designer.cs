namespace WinFormsApp2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            credits = new TextBox();
            PredictionB = new Button();
            New = new Button();
            OpenB = new Button();
            buttonPanel = new Panel();
            SettingsB = new Button();
            ControlTabs = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            buttonPanel.SuspendLayout();
            ControlTabs.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(credits);
            splitContainer1.Panel1.Controls.Add(PredictionB);
            splitContainer1.Panel1.Controls.Add(New);
            splitContainer1.Panel1.Controls.Add(OpenB);
            splitContainer1.Panel1.Controls.Add(buttonPanel);
            splitContainer1.Panel1.Paint += splitContainer1_Panel1_Paint;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(ControlTabs);
            splitContainer1.Size = new Size(1555, 636);
            splitContainer1.SplitterDistance = 126;
            splitContainer1.TabIndex = 0;
            // 
            // credits
            // 
            credits.Dock = DockStyle.Bottom;
            credits.Location = new Point(0, 569);
            credits.Multiline = true;
            credits.Name = "credits";
            credits.ReadOnly = true;
            credits.Size = new Size(126, 67);
            credits.TabIndex = 3;
            credits.Text = "©Saeed Mahar\r\nApache-2.0 License";
            credits.Click += credits_Click;
            // 
            // PredictionB
            // 
            PredictionB.Location = new Point(12, 65);
            PredictionB.Name = "PredictionB";
            PredictionB.Size = new Size(75, 23);
            PredictionB.TabIndex = 0;
            PredictionB.Text = "Prediction";
            PredictionB.UseVisualStyleBackColor = true;
            PredictionB.Click += PredictionB_Click;
            // 
            // New
            // 
            New.Location = new Point(12, 36);
            New.Name = "New";
            New.Size = new Size(75, 23);
            New.TabIndex = 1;
            New.Text = "New";
            New.UseVisualStyleBackColor = true;
            New.Click += New_Click;
            // 
            // OpenB
            // 
            OpenB.Location = new Point(12, 7);
            OpenB.Name = "OpenB";
            OpenB.Size = new Size(75, 23);
            OpenB.TabIndex = 0;
            OpenB.Text = "Open";
            OpenB.UseVisualStyleBackColor = true;
            OpenB.Click += OpenB_Click;
            // 
            // buttonPanel
            // 
            buttonPanel.Controls.Add(SettingsB);
            buttonPanel.Dock = DockStyle.Top;
            buttonPanel.Location = new Point(0, 0);
            buttonPanel.Name = "buttonPanel";
            buttonPanel.Size = new Size(126, 153);
            buttonPanel.TabIndex = 0;
            // 
            // SettingsB
            // 
            SettingsB.Location = new Point(12, 105);
            SettingsB.Name = "SettingsB";
            SettingsB.Size = new Size(75, 23);
            SettingsB.TabIndex = 2;
            SettingsB.Text = "Settings";
            SettingsB.UseVisualStyleBackColor = true;
            SettingsB.Click += SettingsB_Click;
            // 
            // ControlTabs
            // 
            ControlTabs.Controls.Add(tabPage1);
            ControlTabs.Controls.Add(tabPage2);
            ControlTabs.Dock = DockStyle.Fill;
            ControlTabs.Location = new Point(0, 0);
            ControlTabs.Name = "ControlTabs";
            ControlTabs.SelectedIndex = 0;
            ControlTabs.Size = new Size(1425, 636);
            ControlTabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1417, 608);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1417, 608);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1555, 636);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "Form1";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            buttonPanel.ResumeLayout(false);
            ControlTabs.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        public SplitContainer splitContainer1;
        private Button New;
        private Button OpenB;
        private FFNNControls.FFNNCreateControl ffnnCreateControl1;
        private WindowsFormsApp1.NEATControl neatControl1;
        private WindowsFormsApp1.NEATControls.CreateNEATControl createneatControl1;
        private TabControl ControlTabs;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button SettingsB;
        private TextBox credits;
        private Button PredictionB;
        private Panel buttonPanel;
        //private DataControls.DataSetCreateControl dataSetCreateControl1;
    }
}