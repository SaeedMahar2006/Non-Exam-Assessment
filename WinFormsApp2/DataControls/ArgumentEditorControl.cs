using Microsoft.Diagnostics.Tracing.Parsers.FrameworkEventSource;
using NeaLibrary.Data.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2.DataControls
{
    public partial class ArgumentEditorControl : UserControl
    {
        public event EventHandler ValueChanged;
        bool first = true;
        Regex regex_filter=new Regex(@".+");
        string[] categories;
        //bool isSourceParameterInfo = true;
        SourceParameterInfo _sourceParameterInfo;
        //SettingsItem _settingsItemInfo;
        public ArgumentEditorControl()
        {
            InitializeComponent();
        }
        public void SetInfo(SourceParameterInfo sourceParameterInfo)
        {
            //isSourceParameterInfo = true;
            first = true;
            if (sourceParameterInfo.IsRegex)
            {
                regex_filter = new Regex(sourceParameterInfo.Regex);
                textBox1.Show();
                textBox1.Text = "";
                domainUpDown1.Hide();
                numericUpDown.Hide();
            }
            else if (sourceParameterInfo.IsCategorical)
            {
                categories = sourceParameterInfo.Categories;
                textBox1.Hide();
                numericUpDown.Hide();
                domainUpDown1.Show();
                domainUpDown1.Items.Clear();
                foreach (string category in categories) domainUpDown1.Items.Add(category);
                domainUpDown1.SelectedItem = domainUpDown1.Items[0];
                first = false;
            }
            else if (sourceParameterInfo.IsNumeric)
            {
                textBox1.Hide();
                domainUpDown1.Hide();
                numericUpDown.Show();
                numericUpDown.Minimum = (decimal) sourceParameterInfo.BoundsAndStep[0];
                numericUpDown.Maximum = (decimal) sourceParameterInfo.BoundsAndStep[1];
                numericUpDown.Increment = (decimal) sourceParameterInfo.BoundsAndStep[2];
                numericUpDown.Value = 0;//(decimal) 0.5*(numericUpDown.Minimum+numericUpDown.Maximum);
                first = false;
            }
            else
            {
                regex_filter = new Regex(@".+"); //anything except newline, must be something
                textBox1.Show();
                textBox1.Text = "";
                domainUpDown1.Hide();
                numericUpDown.Hide();
            }
            _sourceParameterInfo = sourceParameterInfo;
        }

        public string GetInfo()
        {
            if (_sourceParameterInfo.IsRegex)
            {
                return textBox1.Text;
            }
            else if (_sourceParameterInfo.IsCategorical)
            {
                return domainUpDown1.SelectedItem.ToString();
            }
            else
            {
                return textBox1.Text;
            }
        }

        public object GetValue()
        {
            if (_sourceParameterInfo.IsRegex)
            {
                return textBox1.Text;
            }
            else if (_sourceParameterInfo.IsCategorical)
            {
                return domainUpDown1.SelectedItem;
            }
            else if (_sourceParameterInfo.IsNumeric)
            {
                return numericUpDown.Value;
            }
            else
            {
                return textBox1.Text;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


        //with reference to https://stackoverflow.com/questions/8915151/c-sharp-validating-input-for-textbox-on-winforms
        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!regex_filter.IsMatch(textBox1.Text)) e.Cancel = true;
            ValueChanged(this, new EventArgs());
        }

        private void ArgumentL_Click(object sender, EventArgs e)
        {

        }
        public void setArgumentLabelText(string t)
        {
            ArgumentL.Text = t;
        }

        private void ArgumentEditorControl_Load(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            try { if (!first) OnValueChanged(domainUpDown1.SelectedItem); } catch { }
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!first) OnValueChanged(numericUpDown.Value);
            }catch { }
        }

        private void OnValueChanged(object val)
        {
            EventHandler handler = ValueChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
