namespace WinFormsApp2.MainControls
{
    partial class GraphDisplay
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
            panel1 = new Panel();
            vScrollBar1 = new VScrollBar();
            hScrollBar1 = new HScrollBar();
            ExitB = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(vScrollBar1);
            panel1.Controls.Add(hScrollBar1);
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(724, 471);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // vScrollBar1
            // 
            vScrollBar1.Location = new Point(707, 0);
            vScrollBar1.Name = "vScrollBar1";
            vScrollBar1.Size = new Size(17, 454);
            vScrollBar1.TabIndex = 1;
            // 
            // hScrollBar1
            // 
            hScrollBar1.Location = new Point(-3, 454);
            hScrollBar1.Name = "hScrollBar1";
            hScrollBar1.Size = new Size(710, 17);
            hScrollBar1.TabIndex = 0;
            // 
            // ExitB
            // 
            ExitB.Location = new Point(733, 0);
            ExitB.Name = "ExitB";
            ExitB.Size = new Size(23, 23);
            ExitB.TabIndex = 12;
            ExitB.Text = "✖";
            ExitB.UseVisualStyleBackColor = true;
            ExitB.Click += ExitB_Click;
            // 
            // GraphDisplay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ExitB);
            Controls.Add(panel1);
            Name = "GraphDisplay";
            Size = new Size(768, 477);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private VScrollBar vScrollBar1;
        private HScrollBar hScrollBar1;
        private Button ExitB;
    }
}
