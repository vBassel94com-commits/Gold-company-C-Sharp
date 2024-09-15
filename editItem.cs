using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace iGOLD
{
    public partial class editItem : Form
    {
        public editItem()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        int indexRow;
        SqlConnection con = new SqlConnection(GlobalVar.dataBaseLocation);
        private const int cGrip = 16;
        private const int cCaption = 32;
        Itemdb_Class itm = new Itemdb_Class();
        main_iGOLD main = new main_iGOLD();
        const int WS_MINIMIZEBOX = 0x20000;
        const int CS_DBLCLKS = 0x1;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= WS_MINIMIZEBOX;
                cp.ClassStyle |= CS_DBLCLKS;
                return cp;
            }
        }

        protected override void WndProc(ref Message m)
        {

            if (m.Msg == 0x84)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;
                    return;
                }

                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17;
                    return;
                }
            }

            base.WndProc(ref m);
        }

        private void bar_MouseHover(object sender, EventArgs e)
        {
            this.bar.BackColor = GlobalVar.barHoverColor;
        }

        private void bar_MouseLeave(object sender, EventArgs e)
        {
            this.bar.BackColor = GlobalVar.leaveColor;
        }

        private void bar_Click(object sender, EventArgs e)
        {
            this.bar.BackColor = GlobalVar.barHoverColor;
        }

        private void close_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("هل أنت متأكد بالخروج من تعديل بيانات مصاغ", "الخروج", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
            }
            else
            {
                this.Close();
                itemEntry form = new itemEntry();
                if (GlobalVar.itemEntryisMainMax == true)
                {
                    form.Maximize_Click1();
                }
                form.Show();
            }
        }

        private void close_MouseHover(object sender, EventArgs e)
        {
            this.close.BackColor =GlobalVar.closeHoverColor;
        }

        private void close_MouseLeave(object sender, EventArgs e)
        {
            this.close.BackColor = GlobalVar.leaveColor;
        }

        private void close_MouseEnter(object sender, EventArgs e)
        {
            this.close.BackColor =GlobalVar.closeHoverColor;
        }

        private void minimize_MouseHover(object sender, EventArgs e)
        {
            this.minimize.BackColor = GlobalVar.minMaxHoverColor;
        }

        private void minimize_MouseLeave(object sender, EventArgs e)
        {
            this.minimize.BackColor = GlobalVar.leaveColor;
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void maximize_MouseHover(object sender, EventArgs e)
        {
            this.maximize.BackColor = GlobalVar.minMaxHoverColor;
        }

        private void maximize_MouseLeave(object sender, EventArgs e)
        {
            this.maximize.BackColor = GlobalVar.leaveColor;
        }

        private void maximize_Click(object sender, EventArgs e)
        {
            Maximize_Click1();
        }

        public void Maximize_Click1()
        {
            int W = this.Width;
            int H = this.Height;
            int screenW = Screen.PrimaryScreen.Bounds.Width;
            int screenH = Screen.PrimaryScreen.Bounds.Height;
            if (W == 950 && H == 600)
            {
                GlobalVar.editItemisMainMax = true;
                this.SetDesktopLocation(0, 0);
                this.Size = new Size(screenW, screenH-40);
                close.Location = new Point(2, 0);
                maximize.Location = new Point(34, 0);
                minimize.Location = new Point(66, 0);
                bar.Size = new Size(this.Width, 37);
                bar.Location = new Point(0, 0);
                yellowPanel.Size = new Size(screenW, 3);
                yellowPanel.Location = new Point(0, 32);
            }
            else
            {
                GlobalVar.editItemisMainMax = false;
                this.Hide();
                editItem form = new editItem();
                form.Show();
            }
        }

        private void editItem_Load(object sender, EventArgs e)
        {
            if (main.IsUsbDeviceConnected(GlobalVar.pid))
            {
                try
            {
                if (GlobalVar.editItemisMainMax)
                {
                    maximize_Click(sender, e);
                }
                loadItems();
                loadItemsTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }
            else
            {
                MessageBox.Show("ان فلاش الحماية غير متصل بالكمبيوتر , يرجى التأكد من اتصال فلاش الحماية بالكمبيوتر" + Environment.NewLine + "ثم أعد تشغيل البرنامج");
                this.Close();
                main.Show();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            if (indexRow != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[indexRow];
                idTxtbox.Text = row.Cells[0].Value.ToString();
                itemNameTxtbox.Text = row.Cells[1].Value.ToString();
                string carat = row.Cells[2].Value.ToString();
                itemWeightTxtbox.Text = row.Cells[3].Value.ToString();
                itemCountTxtbox.Text = row.Cells[4].Value.ToString();
                itemFeesTxtbox.Text = row.Cells[5].Value.ToString();
                newWeightTextbox.Text = row.Cells[3].Value.ToString();
                newCountTextbox.Text = row.Cells[4].Value.ToString();
                newFeesTextbox.Text = row.Cells[5].Value.ToString();

                if (carat == "21")
                {
                    carat21.Checked=true;
                }
                else if (carat == "18")
                {
                    carat18.Checked = true;
                }
                else if (carat == "14")
                {
                    carat14.Checked = true;
                }

            }

        }

        private void edit_Click(object sender, EventArgs e)
        {
            edit1();
            loadItems();
            loadItemsTable();
        }

        public void edit1()
        {
            try
            {
                string itemName = itemNameTxtbox.Text;
                itemName = itemName.Remove(itemName.Length - 8);
                int carat = 0;
                decimal weight = Convert.ToDecimal(newWeightTextbox.Text);
                decimal fees = Convert.ToDecimal(newFeesTextbox.Text);
                int count = Convert.ToInt32(newCountTextbox.Text);
                int id = Convert.ToInt32(idTxtbox.Text);
                int countt = Convert.ToInt32(itemCountTxtbox.Text);
                decimal weight1 = Convert.ToDecimal(newWeightTextbox.Text) - Convert.ToDecimal(itemWeightTxtbox.Text);
                if (Convert.ToDecimal(newCountTextbox.Text)!=0 || Convert.ToDecimal(newFeesTextbox.Text)!=0|| Convert.ToDecimal(newWeightTextbox.Text)!=0)
                {
                    itm.itemId1 = id;
                    itm.paymentCash1 = 0;
                    itm.userId1 = GlobalVar.id;
                    itm.customerName1 = "راس مال";
                    itm.customerId1 = itm.GetCustomerid();
                    itm.paymentDateTime1 = DateTime.Now.Date;
                    itm.itemWeight1 = weight;
                    itm.itemFees1 = fees;
                    itm.itemCount1 = count;
                    itm.difWeight1 = weight1;

                    if (carat21.Checked || carat18.Checked || carat14.Checked)
                    {
                        if (carat21.Checked)
                        {
                            carat = 21;
                            itm.itemName1 = itemName + " عيار " + Convert.ToString(carat);
                            itm.itemCarat1 = carat;
                            if (weight1 > 0)
                            {
                                itm.paymentTypeInt1 = -1;
                                itm.paymentNotice1 = "تعديل راس مال (" + itemNameTxtbox.Text + ") زيادة";
                                itm.paymentTypeString1 = "زيادة راس مال";
                                itm.payment141 = 0;
                                itm.payment181 = 0;
                                itm.payment211 = weight1;
                                itm.updateItem21();
                            }
                            else if (weight1 < 0)
                            {
                                itm.paymentTypeInt1 = 1;
                                itm.paymentNotice1 = "تعديل راس مال (" + itemNameTxtbox.Text + ") نقصان";
                                itm.paymentTypeString1 = "سحب راس مال";
                                weight1 = Math.Abs(weight1);
                                itm.payment141 = 0;
                                itm.payment181 = 0;
                                itm.payment211 = weight1;
                                itm.updateItem21();
                            }
                        }
                        else if (carat18.Checked)
                        {
                            carat = 18;
                            itm.itemName1 = itemName + " عيار " + Convert.ToString(carat);
                            itm.itemCarat1 = carat;
                            if (weight1 > 0)
                            {
                                itm.paymentTypeInt1 = -1;
                                itm.paymentNotice1 = "تعديل راس مال (" + itemNameTxtbox.Text + ") زيادة";
                                itm.paymentTypeString1 = "زيادة راس مال";
                                itm.payment141 = 0;
                                itm.payment181 = weight1;
                                itm.payment211 = 0;
                                itm.updateItem18();
                            }
                            else if (weight1 < 0)
                            {
                                itm.paymentTypeInt1 = 1;
                                itm.paymentNotice1 = "تعديل راس مال (" + itemNameTxtbox.Text + ") نقصان";
                                itm.paymentTypeString1 = "سحب راس مال";
                                weight1 = Math.Abs(weight1);
                                itm.payment141 = 0;
                                itm.payment181 = weight1;
                                itm.payment211 = 0;
                                itm.updateItem18();
                            }
                        }
                        else if (carat14.Checked)
                        {
                            carat = 14;
                            itm.itemName1 = itemName + " عيار " + Convert.ToString(carat);
                            itm.itemCarat1 = carat;
                            if (weight1 > 0)
                            {
                                itm.paymentTypeInt1 = -1;
                                itm.paymentNotice1 = "تعديل راس مال (" + itemNameTxtbox.Text + ") زيادة";
                                itm.paymentTypeString1 = "زيادة راس مال";
                                itm.payment141 = weight1;
                                itm.payment181 = 0;
                                itm.payment211 = 0;
                                itm.updateItem14();
                            }
                            else if (weight1 < 0)
                            {
                                itm.paymentTypeInt1 = 1;
                                itm.paymentNotice1 = "تعديل راس مال (" + itemNameTxtbox.Text + ") نقصان";
                                itm.paymentTypeString1 = "سحب راس مال";
                                weight1 = Math.Abs(weight1);
                                itm.payment141 = weight1;
                                itm.payment181 = 0;
                                itm.payment211 = 0;
                                itm.updateItem14();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("يرجى التاكد من عيار المصاغ");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void editItem_Resize(object sender, EventArgs e)
        {
            close.Location = new Point(4, 0);
            maximize.Location = new Point(38, 0);
            minimize.Location = new Point(72, 0);
            bar.Size = new Size(this.Width, 37);
            bar.Location = new Point(0, 0);
            yellowPanel.Size = new Size(this.Width, 3);
            yellowPanel.Location = new Point(0, 32);
        }

        private void itemWeightTxtbox_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                itm.itemName1 = itemNameTxtbox.Text;
                itm.itemId1 = itm.getItemId();
                decimal c = itm.getItemWeight();
                if (Convert.ToDecimal(itemWeightTxtbox.Text) != c)
                {
                    itemWeightTxtbox.Text = c.ToString();
                }
            }
            catch
            {
                itm.itemName1 = itemNameTxtbox.Text;
                itm.itemId1 = itm.getItemId();
                decimal c = itm.getItemWeight();
                if (Convert.ToDecimal(itemWeightTxtbox.Text) != c)
                {
                    itemWeightTxtbox.Text = c.ToString();
                }
            }
        }

        private void itemCountTxtbox_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                itm.itemName1 = itemNameTxtbox.Text;
                itm.itemId1 = itm.getItemId();
                decimal c = itm.getItemCount();
                if (Convert.ToDecimal(itemCountTxtbox.Text) != c)
                {
                    itemCountTxtbox.Text = c.ToString();
                }
            }
            catch
            {
                itm.itemName1 = itemNameTxtbox.Text;
                itm.itemId1 = itm.getItemId();
                decimal c = itm.getItemCount();
                if (Convert.ToDecimal(itemCountTxtbox.Text) != c)
                {
                    itemCountTxtbox.Text = c.ToString();
                }
            }
        }

        private void itemFeesTxtbox_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                itm.itemName1 = itemNameTxtbox.Text;
                itm.itemId1 = itm.getItemId();
                decimal c = itm.getItemFees();
                if (Convert.ToDecimal(itemFeesTxtbox.Text) != c)
                {
                    itemFeesTxtbox.Text = c.ToString();
                }
            }
            catch
            {
                itm.itemName1 = itemNameTxtbox.Text;
                itm.itemId1 = itm.getItemId();
                decimal c = itm.getItemFees();
                if (Convert.ToDecimal(itemFeesTxtbox.Text) != c)
                {
                    itemFeesTxtbox.Text = c.ToString();
                }
            }
        }

        public void loadItems()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                con.Open();
                SqlDataReader DataRdr;
                SqlCommand cmd = new SqlCommand("select itemName from items where userId = "+ GlobalVar.id.ToString(), con);
                cmd.ExecuteNonQuery();
                DataRdr = cmd.ExecuteReader();
                AutoCompleteStringCollection items = new AutoCompleteStringCollection();
                while (DataRdr.Read())
                {
                    items.Add(DataRdr.GetString(0));
                }
                itemNameTxtbox.AutoCompleteCustomSource = items;
                con.Close();
            }
            catch
            {
                con.Close();
            }
        }

        public void loadItemsTable()
        {
            try { 
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select itemId,itemName,itemCarat,itemWeight,itemCount,itemFees from items where userId = " + GlobalVar.id.ToString();
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView2.DataSource = dt;
            con.Close();
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns[0].HeaderText = "id";
            dataGridView1.Columns[1].HeaderText = "اسم المصاغ";
            dataGridView1.Columns[2].HeaderText = "العيار";
            dataGridView1.Columns[3].HeaderText = "الوزن الحالي";
            dataGridView1.Columns[4].HeaderText = "عدد القطع الحالي";
            dataGridView1.Columns[5].HeaderText = "الاجور الحالية";
            dataGridView2.Columns[0].HeaderText = "id";
            dataGridView2.Columns[1].HeaderText = "اسم المصاغ";
            dataGridView2.Columns[2].HeaderText = "العيار";
            dataGridView2.Columns[3].HeaderText = "الوزن الحالي";
            dataGridView2.Columns[4].HeaderText = "عدد القطع الحالي";
            dataGridView2.Columns[5].HeaderText = "الاجور الحالية";
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
            dataGridView1.EnableHeadersVisualStyles = false;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            itemNameTxtbox.Text = "";
            newCountTextbox.Text = "0";
            newFeesTextbox.Text = "0";
            newWeightTextbox.Text = "0";
        }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(idTxtbox.Text);

                decimal weight1 = 0 - Convert.ToDecimal(itemWeightTxtbox.Text);

                itm.itemId1 = id;
                itm.paymentCash1 = 0;
                itm.userId1 = GlobalVar.id;
                itm.customerName1 = "راس مال";
                itm.customerId1 = itm.GetCustomerid();
                itm.paymentDateTime1 = DateTime.Now.Date;
                itm.difWeight1 = weight1;

                if (carat21.Checked || carat18.Checked || carat14.Checked)
                {
                    if (carat21.Checked)
                    {
                        itm.paymentTypeInt1 = 1;
                        itm.paymentNotice1 = "حذف راس مال ("+itemNameTxtbox.Text+")";
                        itm.paymentTypeString1 = "ازالة مصاغ";
                        weight1 = Math.Abs(weight1);
                        itm.payment141 = 0;
                        itm.payment181 = 0;
                        itm.payment211 = weight1;
                        MessageBox.Show(itm.deleteItem21());
                        loadItemsTable();
                    }
                    else if (carat18.Checked)
                    {
                        itm.paymentTypeInt1 = 1;
                        itm.paymentNotice1 = "حذف راس مال (" + itemNameTxtbox.Text + ")";
                        itm.paymentTypeString1 = "ازالة مصاغ";
                        weight1 = Math.Abs(weight1);
                        itm.payment141 = 0;
                        itm.payment181 = weight1;
                        itm.payment211 = 0;
                        MessageBox.Show(itm.deleteItem18());
                        loadItemsTable();
                    }
                    else if (carat14.Checked)
                    {
                        itm.paymentTypeInt1 = 1;
                        itm.paymentNotice1 = "حذف راس مال (" + itemNameTxtbox.Text + ")";
                        itm.paymentTypeString1 = "ازالة مصاغ";
                        weight1 = Math.Abs(weight1);
                        itm.payment141 = weight1;
                        itm.payment181 = 0;
                        itm.payment211 = 0;
                        MessageBox.Show(itm.deleteItem14());
                        loadItemsTable();
                    }
                    itemNameTxtbox.Text = "";
                    itemWeightTxtbox.Text = "0";
                    itemCountTxtbox.Text = "0";
                    itemFeesTxtbox.Text = "0";
                    loadItemsTable();
                }
                else
                {
                    loadItemsTable();
                    MessageBox.Show("يرجى التاكد من عيار المصاغ");
                }
                loadItemsTable();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void itemNameTxtbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (itemNameTxtbox.TextLength > 1)
                {
                    itm.itemName1 = itemNameTxtbox.Text;
                    int itemId = itm.getItemId();
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT itemCarat , itemWeight , itemCount , itemFees from items where itemId = '" + Convert.ToString(itemId) + "' ";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        int carat = Convert.ToInt32(dt.Rows[0][0].ToString());
                        itemWeightTxtbox.Text = dt.Rows[0][1].ToString();
                        itemCountTxtbox.Text = dt.Rows[0][2].ToString();
                        itemFeesTxtbox.Text = dt.Rows[0][3].ToString();
                        if (carat == 21) { carat21.Checked = true; }
                        else if (carat == 18) { carat18.Checked = true; }
                        else if (carat == 14) { carat14.Checked = true; }
                    }
                    else
                    {

                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void newCountTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void newFeesTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void editAll_Click(object sender, EventArgs e)
        {
            label8.Visible = true;
            label9.Visible = true;
            label10.Visible = true;
            label11.Visible = true;
            label12.Visible = true;
            label11.Text = dataGridView1.RowCount.ToString();
            this.Update();
            for ( int i=0 ; i < dataGridView1.RowCount - 1 ; i++ )
            {
                label9.Text = (i + 1).ToString();
                label9.Update();
                idTxtbox.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                itemNameTxtbox.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value) == 14)
                    carat14.Checked = true;
                else if (Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value) == 18)
                    carat18.Checked = true;
                else if (Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value) == 21)
                    carat21.Checked = true;
                itemWeightTxtbox.Text = dataGridView2.Rows[i].Cells[3].Value.ToString();
                itemCountTxtbox.Text = dataGridView2.Rows[i].Cells[4].Value.ToString();
                itemFeesTxtbox.Text = dataGridView2.Rows[i].Cells[5].Value.ToString();
                newWeightTextbox.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
                newCountTextbox.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
                newFeesTextbox.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
                edit1();
            }
            MessageBox.Show("تم تعديل كل المصوغات");
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            loadItems();
            loadItemsTable();
        }
    }
}
