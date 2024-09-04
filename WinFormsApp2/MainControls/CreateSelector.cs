using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.NEATControls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace WinFormsApp2.MainControls
{
    public partial class CreateSelector : UserControl
    {
        public Choice Selected = Choice.Invalid;
        public event EventHandler ChoiceMade;

        public CreateSelector()
        {
            InitializeComponent();
        }

        public enum Choice
        {
            DataSet,
            DataTable,
            FFNN,
            NEAT,
            Invalid
        }








        private void Create_B_Click(object sender, EventArgs e)
        {
            if (NEAT_RB.Checked == true)
            {
                Selected = Choice.NEAT;
                ChoiceMade(sender, e);
            }
            else if (DataTable_RB.Checked == true)
            {
                Selected = Choice.DataTable;
                ChoiceMade(sender, e);
            }
            else if (FFNN_RB.Checked == true)
            {
                Selected = Choice.FFNN;
                ChoiceMade(sender, e);
            }
            else if (DataSet_RB.Checked == true)
            {
                Selected = Choice.DataSet;
                ChoiceMade(sender, e);
            }
            else
            {
                MessageBox.Show("Please select a value");
            }
        }

        private void DataTable_RB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ExitB_Click(object sender, EventArgs e)
        {
            ChoiceMade(sender, e);
        }

        //public async int ShowAndGetChoice()
        //{
        //    Visible = true;
        //    //while (SELECTED==0) { continue; }
        //    Choice c = await
        //    return SELECTED;
        //}
    }
}
