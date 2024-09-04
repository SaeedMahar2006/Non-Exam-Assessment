namespace WinFormsApp2.DataControls
{
    partial class VectorViewer
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
            Table = new TableLayoutPanel();
            CloseButton = new Button();
            label1 = new Label();
            SuspendLayout();
            // 
            // Table
            // 
            Table.AutoScroll = true;
            Table.ColumnCount = 1;
            Table.ColumnStyles.Add(new ColumnStyle());
            Table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            Table.Dock = DockStyle.Bottom;
            Table.Location = new Point(0, 32);
            Table.Name = "Table";
            Table.RowCount = 1;
            Table.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Table.Size = new Size(339, 301);
            Table.TabIndex = 0;
            // 
            // CloseButton
            // 
            CloseButton.Location = new Point(311, 3);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(25, 23);
            CloseButton.TabIndex = 23;
            CloseButton.Text = "🗙";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 7);
            label1.Name = "label1";
            label1.Size = new Size(78, 15);
            label1.TabIndex = 24;
            label1.Text = "Vector Viewer";
            // 
            // VectorViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label1);
            Controls.Add(CloseButton);
            Controls.Add(Table);
            Name = "VectorViewer";
            Size = new Size(339, 333);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel Table;
        private Button CloseButton;
        private Label label1;
    }
}
