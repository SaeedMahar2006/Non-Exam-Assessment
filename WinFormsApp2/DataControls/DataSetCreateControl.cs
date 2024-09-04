using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NeaLibrary.Data;
using NeaLibrary.DataStructures;

namespace WinFormsApp2.DataControls
{
    public partial class DataSetCreateControl : UserControl
    {
        string DB_PATH = NeaLibrary.Tools.Tools.GetGlobalVar("db_dir");
        List<string> Assets = new List<string>();
        List<string> tables;
        List<string> selected_tables = new List<string>();
        List<string> columns;
        object selected_cols_lock = new object();
        List<string> selected_columns = new List<string>();
        List<string> normalise_columns = new List<string>();
        List<string> relative_columns = new List<string>();
        List<string> value_column = new List<string>();
        SQL_Driver db;

        public object creatinglock = new object();
        public event EventHandler FinishedCreating;

        //private event EventHandler ColumnsSelectedChange;


        class CheckBoxDataBinding
        {
            public bool IsChecked { get; set; }
            public string Name { get; set; }
            public CheckBoxDataBinding()
            {
                this.IsChecked = false;
                Name = "null";
            }
        }


        public DataSetCreateControl()
        {
            InitializeComponent();
            OpenDB();
            GetAssetTypes();
            //NormaliseRelative();
        }

        private void OpenDB()
        {
            db = new SQL_Driver(DB_PATH);
            DBPathTextBox.Text = DB_PATH;
        }

        private void GetAssetTypes()
        {
            Assets.Clear();
            AssetSpecifierUpDown.Items.Clear();
            foreach (string a in SQL_Driver.ReadColumn<string>(db.conn, "AssetType", "AssetType"))
            {
                Assets.Add(a);
                AssetSpecifierUpDown.Items.Add(a);
            }
        }

        private void LoadTablesCheckBox(string AssetType)
        {
            TableSelectionCheckBox.Items.Clear();
            tables = new List<string>();
            tables.AddRange(SQL_Driver.ReadColumn<string>(db.conn, "TokenNames", "Token", $"AssetType='{AssetType}'"));
            foreach (string s in tables) TableSelectionCheckBox.Items.Add(s);


        }
        private void LoadColumnsCheckBox()
        {
            //SELECT name FROM PRAGMA_TABLE_INFO('IBM') WHERE type='REAL';

            columns = new List<string>();
            try
            {
                columns.AddRange(SQL_Driver.ReadColumn<string>(db.conn, $"PRAGMA_TABLE_INFO('{tables.First()}')", "name", "type = 'REAL'"));

                //List<string> tables = new List<string>();
                ColumnSelectionCheckBox.Items.Clear();
                for (int i = 0; i < columns.Count; i++) { ColumnSelectionCheckBox.Items.Add(columns[i]); }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Perhaps there are no data tables, try make a data table first: {e.ToString()}");
            }



        }

        private void AssetSpecifierUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            //TODO
            LoadTablesCheckBox(AssetSpecifierUpDown.SelectedItem.ToString());
            LoadColumnsCheckBox();
        }

        private void ChangeDBButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenDatafileDialogue = new OpenFileDialog();
            OpenDatafileDialogue.Multiselect = false;
            OpenDatafileDialogue.Filter = "Sqlite3 database | *.sqlite3";
            DialogResult result = OpenDatafileDialogue.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(OpenDatafileDialogue.FileName))
            {
                DB_PATH = OpenDatafileDialogue.FileName;
                OpenDB();
                DBPathTextBox.Text = OpenDatafileDialogue.FileName;
            }
        }

        private void Update_Normalise_Relative_CheckBox()
        {
            NormaliseSelectorCheckBox.Items.Clear();
            RelativeSelectorCheckBox.Items.Clear();
            foreach (string s in selected_columns)
            {
                RelativeSelectorCheckBox.Items.Add(s);
                NormaliseSelectorCheckBox.Items.Add(s);
            }
        }


        private void ColumnSelectionCheckBox_CheckChanged(object sender, ItemCheckEventArgs e)
        {

            selected_columns.Clear();

            foreach (var item in ColumnSelectionCheckBox.CheckedItems) selected_columns.Add(item.ToString());

            if (e.NewValue == CheckState.Checked)  // Found on Stack Overflow
            {
                selected_columns.Add(ColumnSelectionCheckBox.Items[e.Index].ToString());
            }
            else
            {
                selected_columns.Remove(ColumnSelectionCheckBox.Items[e.Index].ToString());
            }

            Update_Normalise_Relative_CheckBox();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            //OutputDataProducer out_p = new OutputDataProducer((double)SafetySelectorNUpDown.Value);
            if (AssetSpecifierUpDown.SelectedItem == null)
            {
                MessageBox.Show("Select an asset type");
                return;
            }
            if (selected_tables.Count==0)
            {
                MessageBox.Show("Select a tables");
                return;
            }
            if (selected_columns.Count == 0)
            {
                MessageBox.Show("Select columns");
                return;
            }
            if (ValueColumnUpDown.SelectedItem == null)
            {
                MessageBox.Show("Select a vlaue column");
                return;
            }

            lock (creatinglock)
            {
                ExitB.Enabled = false;
                ExitB.Update();
                SaveFileDialog SaveFileDialogue = new SaveFileDialog();
                string ext = (ExportAsDatasetCheckBox.Checked) ? ".ds" : ".dbds";
                SaveFileDialogue.DefaultExt = ext;
                DialogResult result = SaveFileDialogue.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(SaveFileDialogue.FileName))
                {
                    double safety = (double)SafetySelectorNUpDown.Value;
                    DB_DataSet dbds = new DB_DataSet(selected_tables, selected_columns, normalise_columns, relative_columns, new List<string>(), value_column, safety, 1, offset: (int)InternalDBOffsetNUpDown.Value);

                    if (ExportAsDatasetCheckBox.Checked)
                    {
                        NeaLibrary.DataStructures.DataSet ds = dbds.ToDataSet();
                        ds.Save(SaveFileDialogue.FileName);
                    }
                    else
                    {
                        dbds.Save(SaveFileDialogue.FileName);
                    }
                    FinishedCreating(this, new EventArgs());
                }
            }
        }

        private void RelativeSelectorCheckBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            relative_columns.Clear();

            foreach (var item in RelativeSelectorCheckBox.CheckedItems) relative_columns.Add(item.ToString());

            if (e.NewValue == CheckState.Checked)  // Found on Stack Overflow
            {
                relative_columns.Add(RelativeSelectorCheckBox.Items[e.Index].ToString());
            }
            else
            {
                relative_columns.Remove(RelativeSelectorCheckBox.Items[e.Index].ToString());
            }
            //Console.WriteLine();
            ValueColumnUpDown.Items.Clear();
            foreach (string s in relative_columns) ValueColumnUpDown.Items.Add(s);
        }

        private void NormaliseSelectorCheckBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            normalise_columns.Clear();

            foreach (var item in NormaliseSelectorCheckBox.CheckedItems) normalise_columns.Add(item.ToString());

            if (e.NewValue == CheckState.Checked)  // Found on Stack Overflow
            {
                normalise_columns.Add(NormaliseSelectorCheckBox.Items[e.Index].ToString());
            }
            else
            {
                normalise_columns.Remove(NormaliseSelectorCheckBox.Items[e.Index].ToString());
            }

        }

        private void TableSelectionCheckBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            selected_tables.Clear();

            foreach (var item in TableSelectionCheckBox.CheckedItems) selected_tables.Add(item.ToString());

            if (e.NewValue == CheckState.Checked)  // Found on Stack Overflow
            {
                if (TableSelectionCheckBox.Items.Count == 0) return;

                selected_tables.Add(TableSelectionCheckBox.Items[e.Index].ToString());
            }
            else
            {
                selected_tables.Remove(TableSelectionCheckBox.Items[e.Index].ToString());
            }
        }


        private void VAlueColumnUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            value_column.Clear();
            value_column.Add(ValueColumnUpDown.SelectedItem.ToString());
        }

        private void NormaliseSelectorCheckBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ColumnSelectionCheckBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ExitB_Click(object sender, EventArgs e)
        {
            //no need to check because button is disabled if something is being created
            FinishedCreating(this, EventArgs.Empty);
        }
    }
}
