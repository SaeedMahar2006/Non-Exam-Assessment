using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
//using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeaLibrary.Data.Other;
using NeaLibrary.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScottPlot.Drawing.Colormaps;
using WinFormsApp2.DataControls;

namespace WinFormsApp2.MainControls
{
    public partial class SettingsControl : UserControl
    {

        public event EventHandler Exit;
        //List<SettingsItem> _settings = new List<SettingsItem>();
        public SettingsControl()
        {
            InitializeComponent();
            loadSettings();

        }
        void OnSettingsChanged(object sender, object e)
        {
            //Tuple<string,SettingsItem> t=sender as Tuple<string,SettingsItem>;
            //if (t!=null)
            //{
            //    ((SettingsSelector)(SettingsScrollPanel.Controls[t.Item1])).SetInfo(t.Item2);
            //}
            SettingsSelector s = (SettingsSelector)sender;
            //e is the value
            //Console.WriteLine();
            string val = (string)e;
            Tools.SetGlobalVar(s.Name, val);//controls name should match the setting name
        }
        void OnToolsSettingsChanged(object sender, object e)
        {

        }
        void loadSettings()
        {
            //_settings.Clear();
            SettingsScrollPanel.Controls.Clear();
            SettingsScrollPanel.RowCount = 1;
            SettingsScrollPanel.ColumnCount = 1;
            SettingsScrollPanel.RowStyles.Clear();
            string s = Tools.GetAllSettings();
            JObject j = JObject.Parse(s);
            foreach (KeyValuePair<string, JToken> pair in j)
            {
                SettingsItem settingsItem = pair.Value.ToObject<SettingsItem>()!;
                string SettingName = pair.Key;
                SettingsSelector control = new SettingsSelector();
                control.Name = SettingName;
                control.SetLabel(pair.Key);
                control.SetInfo(settingsItem);
                control.SettingsSelectorValueChanged += OnSettingsChanged;
                //_settings.Add(settingsItem);
                SettingsScrollPanel.Controls.Add(control);

                control.Show();
            }
            SettingsScrollPanel.RowStyles.Clear();
            Tools.SettingsChanged += OnToolsSettingsChanged;
        }

        private void SettingsScrollPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ExitB_Click(object sender, EventArgs e)
        {
            Exit(this, e);
        }
    }
}
