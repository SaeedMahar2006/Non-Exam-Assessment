using NeaLibrary.Data;
using NeaLibrary.Data.Other;
using NeaLibrary.Tools;
using NeaLibrary.DataStructures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsApp2.MainControls
{
    public partial class SettingsSelector : UserControl
    {
        public event EventHandler<object> SettingsSelectorValueChanged;
        bool first = true;
        Regex rg;
        Type t;
        object[] categories;
        string fileExt;
        //bool isSourceParameterInfo = true;
        // SourceParameterInfo _sourceParameterInfo;
        public SettingsItem? settingsItem = null;
        public SettingsSelector()
        {
            InitializeComponent();
        }
        public void SetLabel(string s)
        {
            ArgumentL.Text = s;
        }
        private void hide()
        {
            generalTB.Visible = false;
            PathSelectorB.Visible = false;
            PathSelectorTB.Visible = false;
            categorySelectorUpDown.Visible = false;
            numericUpDown1.Visible = false;
        }
        public void SetInfo(SettingsItem settingsItem)
        {
            first = true;
            this.settingsItem = settingsItem;
            if (settingsItem.PathSelector)
            {
                hide();
                PathSelectorB.Visible = true;
                PathSelectorTB.Visible = true;
                fileExt = settingsItem.FileExtension;
                PathSelectorTB.Text = settingsItem.Value;
            }
            else if (settingsItem.Categorical)
            {
                hide();
                
                categories = settingsItem.Categories;
                try
                {
                    t = Type.GetType(settingsItem.TypeName)!;
                }
                catch
                {
                    MessageBox.Show("Invalid type encountered");
                }
                finally
                {
                    if (t == null) MessageBox.Show("Not found type");
                }
                foreach (object o in settingsItem.Categories)
                {
                    try
                    {
                        var c = (Convert.ChangeType(o, t));
                        categorySelectorUpDown.Items.Add(c);
                    }
                    catch
                    {
                        MessageBox.Show($"Can not cast to type {settingsItem.TypeName}");
                    }
                }
                try { categorySelectorUpDown.SelectedItem = Convert.ChangeType(settingsItem.Value, t); } catch {/*shouldnt fail as it worked in loop above unless settings were corrupted*/ }
                categorySelectorUpDown.Visible = true;
            }
            else if (settingsItem.IsNumeric)
            {
                hide();
                numericUpDown1.Minimum = (decimal)settingsItem.BoundsAndStep[0];
                first = true;
                numericUpDown1.Maximum = (decimal)settingsItem.BoundsAndStep[1];
                first = true;
                numericUpDown1.Increment = (decimal)settingsItem.BoundsAndStep[2];
                numericUpDown1.Value = Decimal.Parse(settingsItem.Value);
                numericUpDown1.Visible = true;
            }
            else//simple text box
            {
                if (settingsItem.IsRegex)
                {
                    rg = new Regex(settingsItem.Regex);
                    generalTB.Validating += generalTB_Validating;
                }
                generalTB.Text = settingsItem.Value;
                generalTB.Visible = true;
            }
        }
        private void PathSelectorB_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDatafileDialogue = new OpenFileDialog();
            OpenDatafileDialogue.Multiselect = false;
            OpenDatafileDialogue.Filter = $"Supported Encodings | {fileExt}";
            DialogResult result = OpenDatafileDialogue.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(OpenDatafileDialogue.FileName))
            {
                string c = OpenDatafileDialogue.FileName;
                //Tools.SetGlobalVar();

                PathSelectorTB.Text = c;
                string clean = c.Replace("\\", "/");
                settingsItem.Value = clean;
                SettingsSelectorValueChanged(this, clean);
            }
        }

        private void generalTB_TextChanged(object sender, EventArgs e)
        {
            if (first) { first = false; return; }
            SettingsSelectorValueChanged(this, generalTB.Text);
        }

        // reference to https://stackoverflow.com/questions/8915151/c-sharp-validating-input-for-textbox-on-winforms
        private void generalTB_Validating(object sender, CancelEventArgs e)
        {
            //if (first) { first = false; return; }
            if (!rg.IsMatch(generalTB.Text))
            {
                e.Cancel = true;
            }
        }

        private void categorySelectorUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            if (first) { first = false; return; }
            if (categorySelectorUpDown.SelectedItem == null) return;
            SettingsSelectorValueChanged(this, categorySelectorUpDown.SelectedItem.ToString()!);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (first) { first = false; return; }
            SettingsSelectorValueChanged(this, numericUpDown1.Value.ToString());
        }
    }
}
