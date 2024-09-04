using System.Windows.Forms;

namespace WindowsFormsApp1.NEATControls
{
    partial class CreateNEATControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateNEATControl));
            panel1 = new Panel();
            CloseButton = new Button();
            OutputSelector = new NumericUpDown();
            InputSelector = new NumericUpDown();
            ConfirmCreate = new Button();
            WeightShiftStrength = new NumericUpDown();
            WeightShiftMutate = new NumericUpDown();
            WeightRandomMutate = new NumericUpDown();
            ConnectionToggleMutate = new NumericUpDown();
            NewConnectionMutate = new NumericUpDown();
            NewNodeMutate = new NumericUpDown();
            SpecieDistanceSpecifier = new NumericUpDown();
            KillRateSpecifier = new NumericUpDown();
            ClientsNumSpecifier = new NumericUpDown();
            Info2 = new Label();
            SaveLocationDialogue = new SaveFileDialog();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)OutputSelector).BeginInit();
            ((System.ComponentModel.ISupportInitialize)InputSelector).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WeightShiftStrength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WeightShiftMutate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)WeightRandomMutate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ConnectionToggleMutate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NewConnectionMutate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)NewNodeMutate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpecieDistanceSpecifier).BeginInit();
            ((System.ComponentModel.ISupportInitialize)KillRateSpecifier).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ClientsNumSpecifier).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(CloseButton);
            panel1.Controls.Add(OutputSelector);
            panel1.Controls.Add(InputSelector);
            panel1.Controls.Add(ConfirmCreate);
            panel1.Controls.Add(WeightShiftStrength);
            panel1.Controls.Add(WeightShiftMutate);
            panel1.Controls.Add(WeightRandomMutate);
            panel1.Controls.Add(ConnectionToggleMutate);
            panel1.Controls.Add(NewConnectionMutate);
            panel1.Controls.Add(NewNodeMutate);
            panel1.Controls.Add(SpecieDistanceSpecifier);
            panel1.Controls.Add(KillRateSpecifier);
            panel1.Controls.Add(ClientsNumSpecifier);
            panel1.Controls.Add(Info2);
            panel1.Location = new Point(4, 3);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(394, 476);
            panel1.TabIndex = 0;
            // 
            // CloseButton
            // 
            CloseButton.Location = new Point(361, 23);
            CloseButton.Name = "CloseButton";
            CloseButton.Size = new Size(25, 23);
            CloseButton.TabIndex = 23;
            CloseButton.Text = "🗙";
            CloseButton.UseVisualStyleBackColor = true;
            CloseButton.Click += CloseButton_Click;
            // 
            // OutputSelector
            // 
            OutputSelector.Location = new Point(246, 100);
            OutputSelector.Margin = new Padding(4, 3, 4, 3);
            OutputSelector.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            OutputSelector.Name = "OutputSelector";
            OutputSelector.Size = new Size(140, 23);
            OutputSelector.TabIndex = 12;
            OutputSelector.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // InputSelector
            // 
            InputSelector.Location = new Point(246, 76);
            InputSelector.Margin = new Padding(4, 3, 4, 3);
            InputSelector.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            InputSelector.Name = "InputSelector";
            InputSelector.Size = new Size(140, 23);
            InputSelector.TabIndex = 11;
            InputSelector.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // ConfirmCreate
            // 
            ConfirmCreate.Location = new Point(4, 423);
            ConfirmCreate.Margin = new Padding(4, 3, 4, 3);
            ConfirmCreate.Name = "ConfirmCreate";
            ConfirmCreate.Size = new Size(382, 23);
            ConfirmCreate.TabIndex = 10;
            ConfirmCreate.Text = "Create";
            ConfirmCreate.UseVisualStyleBackColor = true;
            ConfirmCreate.Click += ConfirmCreate_Click;
            // 
            // WeightShiftStrength
            // 
            WeightShiftStrength.DecimalPlaces = 1;
            WeightShiftStrength.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            WeightShiftStrength.Location = new Point(246, 377);
            WeightShiftStrength.Margin = new Padding(4, 3, 4, 3);
            WeightShiftStrength.Maximum = new decimal(new int[] { 2, 0, 0, 0 });
            WeightShiftStrength.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            WeightShiftStrength.Name = "WeightShiftStrength";
            WeightShiftStrength.Size = new Size(140, 23);
            WeightShiftStrength.TabIndex = 8;
            WeightShiftStrength.Value = new decimal(new int[] { 15, 0, 0, 65536 });
            // 
            // WeightShiftMutate
            // 
            WeightShiftMutate.DecimalPlaces = 2;
            WeightShiftMutate.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            WeightShiftMutate.Location = new Point(246, 352);
            WeightShiftMutate.Margin = new Padding(4, 3, 4, 3);
            WeightShiftMutate.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            WeightShiftMutate.Name = "WeightShiftMutate";
            WeightShiftMutate.Size = new Size(140, 23);
            WeightShiftMutate.TabIndex = 7;
            WeightShiftMutate.Value = new decimal(new int[] { 5, 0, 0, 131072 });
            // 
            // WeightRandomMutate
            // 
            WeightRandomMutate.DecimalPlaces = 2;
            WeightRandomMutate.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            WeightRandomMutate.Location = new Point(246, 325);
            WeightRandomMutate.Margin = new Padding(4, 3, 4, 3);
            WeightRandomMutate.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            WeightRandomMutate.Name = "WeightRandomMutate";
            WeightRandomMutate.Size = new Size(140, 23);
            WeightRandomMutate.TabIndex = 6;
            WeightRandomMutate.Value = new decimal(new int[] { 5, 0, 0, 131072 });
            // 
            // ConnectionToggleMutate
            // 
            ConnectionToggleMutate.DecimalPlaces = 2;
            ConnectionToggleMutate.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            ConnectionToggleMutate.Location = new Point(246, 299);
            ConnectionToggleMutate.Margin = new Padding(4, 3, 4, 3);
            ConnectionToggleMutate.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            ConnectionToggleMutate.Name = "ConnectionToggleMutate";
            ConnectionToggleMutate.Size = new Size(140, 23);
            ConnectionToggleMutate.TabIndex = 5;
            ConnectionToggleMutate.Value = new decimal(new int[] { 5, 0, 0, 131072 });
            // 
            // NewConnectionMutate
            // 
            NewConnectionMutate.DecimalPlaces = 2;
            NewConnectionMutate.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            NewConnectionMutate.Location = new Point(246, 273);
            NewConnectionMutate.Margin = new Padding(4, 3, 4, 3);
            NewConnectionMutate.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            NewConnectionMutate.Name = "NewConnectionMutate";
            NewConnectionMutate.Size = new Size(140, 23);
            NewConnectionMutate.TabIndex = 4;
            NewConnectionMutate.Value = new decimal(new int[] { 5, 0, 0, 131072 });
            // 
            // NewNodeMutate
            // 
            NewNodeMutate.DecimalPlaces = 2;
            NewNodeMutate.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            NewNodeMutate.Location = new Point(246, 248);
            NewNodeMutate.Margin = new Padding(4, 3, 4, 3);
            NewNodeMutate.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            NewNodeMutate.Name = "NewNodeMutate";
            NewNodeMutate.Size = new Size(140, 23);
            NewNodeMutate.TabIndex = 3;
            NewNodeMutate.Value = new decimal(new int[] { 5, 0, 0, 131072 });
            // 
            // SpecieDistanceSpecifier
            // 
            SpecieDistanceSpecifier.DecimalPlaces = 2;
            SpecieDistanceSpecifier.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            SpecieDistanceSpecifier.Location = new Point(246, 198);
            SpecieDistanceSpecifier.Margin = new Padding(4, 3, 4, 3);
            SpecieDistanceSpecifier.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            SpecieDistanceSpecifier.Name = "SpecieDistanceSpecifier";
            SpecieDistanceSpecifier.Size = new Size(140, 23);
            SpecieDistanceSpecifier.TabIndex = 2;
            SpecieDistanceSpecifier.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // KillRateSpecifier
            // 
            KillRateSpecifier.DecimalPlaces = 2;
            KillRateSpecifier.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            KillRateSpecifier.Location = new Point(246, 174);
            KillRateSpecifier.Margin = new Padding(4, 3, 4, 3);
            KillRateSpecifier.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            KillRateSpecifier.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            KillRateSpecifier.Name = "KillRateSpecifier";
            KillRateSpecifier.Size = new Size(140, 23);
            KillRateSpecifier.TabIndex = 1;
            KillRateSpecifier.Value = new decimal(new int[] { 2, 0, 0, 65536 });
            // 
            // ClientsNumSpecifier
            // 
            ClientsNumSpecifier.Location = new Point(246, 150);
            ClientsNumSpecifier.Margin = new Padding(4, 3, 4, 3);
            ClientsNumSpecifier.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            ClientsNumSpecifier.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ClientsNumSpecifier.Name = "ClientsNumSpecifier";
            ClientsNumSpecifier.Size = new Size(140, 23);
            ClientsNumSpecifier.TabIndex = 0;
            ClientsNumSpecifier.Tag = "More creatures leads to faster evolution, but slower training.";
            ClientsNumSpecifier.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // Info2
            // 
            Info2.AutoSize = true;
            Info2.Font = new Font("Microsoft Sans Serif", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            Info2.Location = new Point(4, 23);
            Info2.Margin = new Padding(4, 0, 4, 0);
            Info2.Name = "Info2";
            Info2.Size = new Size(243, 375);
            Info2.TabIndex = 9;
            Info2.Text = resources.GetString("Info2.Text");
            // 
            // CreateNEATControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Margin = new Padding(4, 3, 4, 3);
            Name = "CreateNEATControl";
            Size = new Size(397, 479);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)OutputSelector).EndInit();
            ((System.ComponentModel.ISupportInitialize)InputSelector).EndInit();
            ((System.ComponentModel.ISupportInitialize)WeightShiftStrength).EndInit();
            ((System.ComponentModel.ISupportInitialize)WeightShiftMutate).EndInit();
            ((System.ComponentModel.ISupportInitialize)WeightRandomMutate).EndInit();
            ((System.ComponentModel.ISupportInitialize)ConnectionToggleMutate).EndInit();
            ((System.ComponentModel.ISupportInitialize)NewConnectionMutate).EndInit();
            ((System.ComponentModel.ISupportInitialize)NewNodeMutate).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpecieDistanceSpecifier).EndInit();
            ((System.ComponentModel.ISupportInitialize)KillRateSpecifier).EndInit();
            ((System.ComponentModel.ISupportInitialize)ClientsNumSpecifier).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private NumericUpDown ClientsNumSpecifier;
        private NumericUpDown KillRateSpecifier;
        private NumericUpDown SpecieDistanceSpecifier;
        private NumericUpDown WeightRandomMutate;
        private NumericUpDown ConnectionToggleMutate;
        private NumericUpDown NewConnectionMutate;
        private NumericUpDown NewNodeMutate;
        private NumericUpDown WeightShiftStrength;
        private NumericUpDown WeightShiftMutate;
        private Label Info2;
        private Button ConfirmCreate;
        private SaveFileDialog SaveLocationDialogue;
        private NumericUpDown OutputSelector;
        private NumericUpDown InputSelector;
        private Button CloseButton;
    }

}
