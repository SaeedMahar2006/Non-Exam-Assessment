using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NeaLibrary.DataStructures;

namespace WinFormsApp2.DataControls
{
    public partial class VectorViewer : UserControl
    {
        public event EventHandler Close;
        public VectorViewer()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            try { Close(this, EventArgs.Empty); }
            catch { }
        }
        public void Display(Vector v)
        {
            Table.Controls.Clear();
            Table.ColumnCount = 1;
            Table.RowCount = 1;
            foreach (double d in v)
            {
                TextBox textBox = new TextBox();
                textBox.ReadOnly = true;
                textBox.Text = d.ToString();
                Table.Controls.Add(textBox, 1, Table.RowCount);
                Table.RowCount++;
                textBox.Dock = DockStyle.Fill;
                textBox.Show();
            }
            Table.RowStyles.Clear();
            Table.Update();
        }
    }
}
