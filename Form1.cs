using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iGOLD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.KeyPreview = true;
            textBox1.AutoCompleteCustomSource = acsc;
            textBox1.AutoCompleteMode = AutoCompleteMode.None;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        AutoCompleteStringCollection acsc = new AutoCompleteStringCollection();           

        void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            listBox1.Items.Clear();
            if (textBox1.Text.Length == 0)
            {
                hideResults();
                return;
            }

            foreach (String s in textBox1.AutoCompleteCustomSource)
            {
                if (s.Contains(textBox1.Text))
                {
                    listBox1.Items.Add(s);
                    listBox1.Visible = true;
                }
            }
        }

        void listBox1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            textBox1.Text = listBox1.Items[listBox1.SelectedIndex].ToString();
            hideResults();
        }

        void listBox1_LostFocus(object sender, System.EventArgs e)
        {
            hideResults();
        }

        void hideResults()
        {
            listBox1.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable d = new DataTable();
            d.Columns.Add();
            d.Columns.Add();
            d.Columns.Add();
            d.Rows.Add();
            dataGridView1.DataSource = d;
            //for (int i = 0; i < GlobalVar.customers.Count; i++)
            //{
            //    customers.Add(GlobalVar.customers[i]);
            //}
            //for (int i = 0; i < GlobalVar.items.Count; i++)
            //{
            //    items.Add(GlobalVar.items[i]);
            //}
            //acsc.Add("[001] some kind of item");
            //acsc.Add("[002] some other item");
            //acsc.Add("[003] an orange");
            //acsc.Add("[004] i like pickles");
            //GlobalVar.names = acsc;
        }
        
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var ctl = e.Control as DataGridViewTextBoxEditingControl;
            if (ctl == null)
            {
                return;
            }
            ctl.KeyUp -= ctl_KeyUp;
            ctl.KeyUp += new KeyEventHandler(ctl_KeyUp);
        }
       
        private void ctl_KeyUp(object sender, KeyEventArgs e)
        {
            var box = sender as System.Windows.Forms.TextBox;
            if (box == null)
            {
                return;
            }
            textBox1.Text = box.Text;
        }

    }
}
