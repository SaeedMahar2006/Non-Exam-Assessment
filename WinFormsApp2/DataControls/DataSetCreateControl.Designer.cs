namespace WinFormsApp2.DataControls
{
    partial class DataSetCreateControl
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
            AssetSpecifierUpDown = new DomainUpDown();
            TableSelectPanel = new Panel();
            TableSelectionCheckBox = new CheckedListBox();
            SelectColumnsPanel = new Panel();
            ColumnSelectionCheckBox = new CheckedListBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            RelativeSelectorPanel = new Panel();
            RelativeSelectorCheckBox = new CheckedListBox();
            label6 = new Label();
            NormaliseSelectorPanel = new Panel();
            NormaliseSelectorCheckBox = new CheckedListBox();
            SafetySelectorNUpDown = new NumericUpDown();
            label7 = new Label();
            ExportAsDatasetCheckBox = new CheckBox();
            InternalDBOffsetNUpDown = new NumericUpDown();
            label8 = new Label();
            CreateButton = new Button();
            DBPathTextBox = new TextBox();
            ChangeDBButton = new Button();
            label9 = new Label();
            ValueColumnUpDown = new DomainUpDown();
            label10 = new Label();
            ExitB = new Button();
            TableSelectPanel.SuspendLayout();
            SelectColumnsPanel.SuspendLayout();
            RelativeSelectorPanel.SuspendLayout();
            NormaliseSelectorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SafetySelectorNUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)InternalDBOffsetNUpDown).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(7, 16);
            label1.Name = "label1";
            label1.Size = new Size(142, 25);
            label1.TabIndex = 0;
            label1.Text = "Create Data Set";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 56);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 1;
            label2.Text = "Asset Type";
            // 
            // AssetSpecifierUpDown
            // 
            AssetSpecifierUpDown.Location = new Point(71, 54);
            AssetSpecifierUpDown.Name = "AssetSpecifierUpDown";
            AssetSpecifierUpDown.ReadOnly = true;
            AssetSpecifierUpDown.Size = new Size(120, 23);
            AssetSpecifierUpDown.TabIndex = 2;
            AssetSpecifierUpDown.Text = "Asset";
            AssetSpecifierUpDown.SelectedItemChanged += AssetSpecifierUpDown_SelectedItemChanged;
            // 
            // TableSelectPanel
            // 
            TableSelectPanel.Controls.Add(TableSelectionCheckBox);
            TableSelectPanel.Location = new Point(3, 94);
            TableSelectPanel.Name = "TableSelectPanel";
            TableSelectPanel.Size = new Size(358, 175);
            TableSelectPanel.TabIndex = 3;
            // 
            // TableSelectionCheckBox
            // 
            TableSelectionCheckBox.FormattingEnabled = true;
            TableSelectionCheckBox.Location = new Point(3, 3);
            TableSelectionCheckBox.Name = "TableSelectionCheckBox";
            TableSelectionCheckBox.Size = new Size(352, 166);
            TableSelectionCheckBox.TabIndex = 0;
            TableSelectionCheckBox.ItemCheck += TableSelectionCheckBox_ItemCheck;
            // 
            // SelectColumnsPanel
            // 
            SelectColumnsPanel.Controls.Add(ColumnSelectionCheckBox);
            SelectColumnsPanel.Location = new Point(367, 94);
            SelectColumnsPanel.Name = "SelectColumnsPanel";
            SelectColumnsPanel.Size = new Size(362, 175);
            SelectColumnsPanel.TabIndex = 4;
            // 
            // ColumnSelectionCheckBox
            // 
            ColumnSelectionCheckBox.FormattingEnabled = true;
            ColumnSelectionCheckBox.Location = new Point(3, 3);
            ColumnSelectionCheckBox.Name = "ColumnSelectionCheckBox";
            ColumnSelectionCheckBox.Size = new Size(356, 166);
            ColumnSelectionCheckBox.TabIndex = 0;
            ColumnSelectionCheckBox.ItemCheck += ColumnSelectionCheckBox_CheckChanged;
            ColumnSelectionCheckBox.SelectedIndexChanged += ColumnSelectionCheckBox_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 79);
            label3.Name = "label3";
            label3.Size = new Size(73, 15);
            label3.TabIndex = 5;
            label3.Text = "Select Tables";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(364, 79);
            label4.Name = "label4";
            label4.Size = new Size(89, 15);
            label4.TabIndex = 6;
            label4.Text = "Select Columns";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(0, 272);
            label5.Name = "label5";
            label5.Size = new Size(99, 15);
            label5.TabIndex = 8;
            label5.Text = "Relative Columns";
            // 
            // RelativeSelectorPanel
            // 
            RelativeSelectorPanel.Controls.Add(RelativeSelectorCheckBox);
            RelativeSelectorPanel.Location = new Point(3, 287);
            RelativeSelectorPanel.Name = "RelativeSelectorPanel";
            RelativeSelectorPanel.Size = new Size(358, 175);
            RelativeSelectorPanel.TabIndex = 7;
            // 
            // RelativeSelectorCheckBox
            // 
            RelativeSelectorCheckBox.FormattingEnabled = true;
            RelativeSelectorCheckBox.Location = new Point(3, 3);
            RelativeSelectorCheckBox.Name = "RelativeSelectorCheckBox";
            RelativeSelectorCheckBox.Size = new Size(352, 166);
            RelativeSelectorCheckBox.TabIndex = 0;
            RelativeSelectorCheckBox.ItemCheck += RelativeSelectorCheckBox_ItemCheck;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(364, 272);
            label6.Name = "label6";
            label6.Size = new Size(112, 15);
            label6.TabIndex = 10;
            label6.Text = "Normalise Columns";
            // 
            // NormaliseSelectorPanel
            // 
            NormaliseSelectorPanel.Controls.Add(NormaliseSelectorCheckBox);
            NormaliseSelectorPanel.Location = new Point(367, 287);
            NormaliseSelectorPanel.Name = "NormaliseSelectorPanel";
            NormaliseSelectorPanel.Size = new Size(362, 175);
            NormaliseSelectorPanel.TabIndex = 9;
            // 
            // NormaliseSelectorCheckBox
            // 
            NormaliseSelectorCheckBox.FormattingEnabled = true;
            NormaliseSelectorCheckBox.Location = new Point(3, 3);
            NormaliseSelectorCheckBox.Name = "NormaliseSelectorCheckBox";
            NormaliseSelectorCheckBox.Size = new Size(356, 166);
            NormaliseSelectorCheckBox.TabIndex = 0;
            NormaliseSelectorCheckBox.ItemCheck += NormaliseSelectorCheckBox_ItemCheck;
            NormaliseSelectorCheckBox.SelectedIndexChanged += NormaliseSelectorCheckBox_SelectedIndexChanged;
            // 
            // SafetySelectorNUpDown
            // 
            SafetySelectorNUpDown.DecimalPlaces = 2;
            SafetySelectorNUpDown.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            SafetySelectorNUpDown.Location = new Point(92, 466);
            SafetySelectorNUpDown.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            SafetySelectorNUpDown.Name = "SafetySelectorNUpDown";
            SafetySelectorNUpDown.Size = new Size(99, 23);
            SafetySelectorNUpDown.TabIndex = 11;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(6, 470);
            label7.Name = "label7";
            label7.Size = new Size(75, 15);
            label7.TabIndex = 12;
            label7.Text = "Safety Factor";
            // 
            // ExportAsDatasetCheckBox
            // 
            ExportAsDatasetCheckBox.AutoSize = true;
            ExportAsDatasetCheckBox.Location = new Point(613, 467);
            ExportAsDatasetCheckBox.Name = "ExportAsDatasetCheckBox";
            ExportAsDatasetCheckBox.Size = new Size(116, 19);
            ExportAsDatasetCheckBox.TabIndex = 13;
            ExportAsDatasetCheckBox.Text = "Export as Dataset";
            ExportAsDatasetCheckBox.UseVisualStyleBackColor = true;
            // 
            // InternalDBOffsetNUpDown
            // 
            InternalDBOffsetNUpDown.Location = new Point(92, 495);
            InternalDBOffsetNUpDown.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            InternalDBOffsetNUpDown.Name = "InternalDBOffsetNUpDown";
            InternalDBOffsetNUpDown.Size = new Size(99, 23);
            InternalDBOffsetNUpDown.TabIndex = 14;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(7, 498);
            label8.Name = "label8";
            label8.Size = new Size(57, 15);
            label8.TabIndex = 15;
            label8.Text = "DB Offset";
            // 
            // CreateButton
            // 
            CreateButton.Location = new Point(3, 519);
            CreateButton.Name = "CreateButton";
            CreateButton.Size = new Size(720, 23);
            CreateButton.TabIndex = 16;
            CreateButton.Text = "Create";
            CreateButton.UseVisualStyleBackColor = true;
            CreateButton.Click += CreateButton_Click;
            // 
            // DBPathTextBox
            // 
            DBPathTextBox.Location = new Point(369, 53);
            DBPathTextBox.Name = "DBPathTextBox";
            DBPathTextBox.ReadOnly = true;
            DBPathTextBox.Size = new Size(298, 23);
            DBPathTextBox.TabIndex = 17;
            DBPathTextBox.Text = "null";
            // 
            // ChangeDBButton
            // 
            ChangeDBButton.Location = new Point(667, 53);
            ChangeDBButton.Name = "ChangeDBButton";
            ChangeDBButton.Size = new Size(62, 23);
            ChangeDBButton.TabIndex = 18;
            ChangeDBButton.Text = "Change";
            ChangeDBButton.UseVisualStyleBackColor = true;
            ChangeDBButton.Click += ChangeDBButton_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(308, 57);
            label9.Name = "label9";
            label9.Size = new Size(55, 15);
            label9.TabIndex = 19;
            label9.Text = "Database";
            // 
            // ValueColumnUpDown
            // 
            ValueColumnUpDown.Location = new Point(370, 462);
            ValueColumnUpDown.Name = "ValueColumnUpDown";
            ValueColumnUpDown.ReadOnly = true;
            ValueColumnUpDown.Size = new Size(160, 23);
            ValueColumnUpDown.TabIndex = 20;
            ValueColumnUpDown.Text = "null";
            ValueColumnUpDown.SelectedItemChanged += VAlueColumnUpDown_SelectedItemChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(280, 466);
            label10.Name = "label10";
            label10.Size = new Size(81, 15);
            label10.TabIndex = 21;
            label10.Text = "Value Column";
            // 
            // ExitB
            // 
            ExitB.Location = new Point(700, 16);
            ExitB.Name = "ExitB";
            ExitB.Size = new Size(23, 23);
            ExitB.TabIndex = 22;
            ExitB.Text = "✖";
            ExitB.UseVisualStyleBackColor = true;
            ExitB.Click += ExitB_Click;
            // 
            // DataSetCreateControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ExitB);
            Controls.Add(label10);
            Controls.Add(ValueColumnUpDown);
            Controls.Add(label9);
            Controls.Add(ChangeDBButton);
            Controls.Add(DBPathTextBox);
            Controls.Add(CreateButton);
            Controls.Add(label8);
            Controls.Add(InternalDBOffsetNUpDown);
            Controls.Add(ExportAsDatasetCheckBox);
            Controls.Add(label7);
            Controls.Add(SafetySelectorNUpDown);
            Controls.Add(label6);
            Controls.Add(NormaliseSelectorPanel);
            Controls.Add(label5);
            Controls.Add(RelativeSelectorPanel);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(SelectColumnsPanel);
            Controls.Add(TableSelectPanel);
            Controls.Add(AssetSpecifierUpDown);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "DataSetCreateControl";
            Size = new Size(738, 545);
            TableSelectPanel.ResumeLayout(false);
            SelectColumnsPanel.ResumeLayout(false);
            RelativeSelectorPanel.ResumeLayout(false);
            NormaliseSelectorPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)SafetySelectorNUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)InternalDBOffsetNUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private DomainUpDown AssetSpecifierUpDown;
        private Panel TableSelectPanel;
        private CheckedListBox TableSelectionCheckBox;
        private Panel SelectColumnsPanel;
        private CheckedListBox ColumnSelectionCheckBox;
        private Label label3;
        private Label label4;
        private Label label5;
        private Panel RelativeSelectorPanel;
        private CheckedListBox RelativeSelectorCheckBox;
        private Label label6;
        private Panel NormaliseSelectorPanel;
        private CheckedListBox NormaliseSelectorCheckBox;
        private NumericUpDown SafetySelectorNUpDown;
        private Label label7;
        private CheckBox ExportAsDatasetCheckBox;
        private NumericUpDown InternalDBOffsetNUpDown;
        private Label label8;
        private Button CreateButton;
        private TextBox DBPathTextBox;
        private Button ChangeDBButton;
        private Label label9;
        private DomainUpDown ValueColumnUpDown;
        private Label label10;
        private Button ExitB;
    }
}
