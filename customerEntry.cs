using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace iGOLD
{
    public partial class customerEntry : Form
    {
        public customerEntry()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        SqlConnection con = new SqlConnection(GlobalVar.dataBaseLocation);
        private const int cGrip = 16;
        private const int cCaption = 32;
        Itemdb_Class itm = new Itemdb_Class();
        main_iGOLD main = new main_iGOLD();
        customerEntry1 frm = new customerEntry1();
        dailyPayment f = new dailyPayment();
        DataTable d = new DataTable();
        DataTable dt = new DataTable();
        string str = "";
        bool repeat = true;
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
            DialogResult result = MessageBox.Show("   هل أنت متأكد بالخروج من ادخال بيانات مصاغ  ", "الخروج", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
            }
            else
            {
                if (GlobalVar.status_value)
                {
                    MessageBox.Show("سيتم اغلاق البرنامج");
                    GlobalVar.status_value = false;
                    main.setBackUp();
                    Application.Exit();
                }
            }
        }

        private void close_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox1.BackColor = GlobalVar.closeHoverColor;
        }

        private void close_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox1.BackColor = GlobalVar.leaveColor;
        }

        private void close_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox1.BackColor = GlobalVar.closeHoverColor;
        }

        private void minimize_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox3.BackColor = GlobalVar.minMaxHoverColor;
        }

        private void minimize_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox3.BackColor = GlobalVar.leaveColor;
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void maximize_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox2.BackColor = GlobalVar.minMaxHoverColor;
        }

        private void maximize_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox2.BackColor = GlobalVar.leaveColor;
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
            if (W == 646 && H == 500)
            {
                this.SetDesktopLocation(0, 0);
                this.Size = new Size(screenW, screenH - 40);
                pictureBox1.Location = new Point(2, 0);
                pictureBox2.Location = new Point(36, 0);
                pictureBox3.Location = new Point(70, 0);
                bar.Size = new Size(screenW - 95, 37);
                bar.Location = new Point(0, 0);
                yellowPanel.Size = new Size(screenW, 3);
                yellowPanel.Location = new Point(0, 32);
                GlobalVar.itemEntryisMainMax = true;
                panel2.Width = this.Width - 41;
                panel2.Height = this.Height - 37;
                panelSlide.Height = this.Height - 37;
                main_Gold1.Height = panelSlide.Height / 11;
                customerEntry1.Height = panelSlide.Height / 11;
                itemEntry1.Height = panelSlide.Height / 11;
                sellBill1.Height = panelSlide.Height / 11;
                BuyBill1.Height = panelSlide.Height / 11;
                customerPayment1.Height = panelSlide.Height / 11;
                ToCustomerPayment1.Height = panelSlide.Height / 11;
                outCome1.Height = panelSlide.Height / 11;
                financeAccount1.Height = panelSlide.Height / 11;
                companyTotalAccount1.Height = panelSlide.Height / 11;
                customerTotalAccount1.Height = panelSlide.Height / 11;
            }
            else
            {

                this.Close();
                itemEntry form = new itemEntry();
                form.Show();
                GlobalVar.itemEntryisMainMax = false;
                panel2.Width = this.Width - 41;
            }
        }

        private void save_MouseHover(object sender, EventArgs e)
        {
            save.ForeColor = GlobalVar.barHoverColor;
        }

        private void save_MouseLeave(object sender, EventArgs e)
        {
            save.ForeColor = GlobalVar.leaveColor;
        }

        private void save_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlDataReader DataRdr1;
                SqlCommand cmd1 = new SqlCommand("select customerName from customers where userId = " + GlobalVar.id.ToString() , con);
                cmd1.ExecuteNonQuery();
                DataRdr1 = cmd1.ExecuteReader();
                while (DataRdr1.Read())
                {
                    dt.Rows.Add(DataRdr1.GetString(0));
                }
            }
            catch
            {
                con.Close();
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str = dt.Rows[i]["customerName"].ToString();
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == str)
                {
                    dataGridView1.Rows[i].Selected = true;
                    MessageBox.Show("الصائغ " + str + "  موجود مسبقاً يرجى اختيار اسم آخر");
                    repeat = false;
                    return;
                }
            }
            if (repeat)
            {
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label11.Text = (dataGridView1.RowCount).ToString();
                label9.Text = "1";
                if (dataGridView1.RowCount > 1)
                {
                      for (int i = 0; i < dataGridView1.RowCount-1; i++)
                        {
                            label9.Text = (i + 1).ToString();
                            label9.Update();
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() != "")
                            {
                                frm.nameTxtbox.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                                frm.mobileTxtbox.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                                frm.save_Click1();
                            }
                        }
                }
                MessageBox.Show("تم حفظ بيانات الزبائن");
                label8.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
            }
            d.Clear();
        }

        public void save_Click1()
        {
         
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            d = null;
        }

        private void itemEntry_Resize(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(2, 0);
            pictureBox2.Location = new Point(36, 0);
            pictureBox3.Location = new Point(70, 0);
            bar.Size = new Size(this.Width - 95, 37);
            bar.Location = new Point(0, 0);
            yellowPanel.Size = new Size(this.Width, 3);
            yellowPanel.Location = new Point(0, 32);
        }

        private void edit_Click(object sender, EventArgs e)
        {
            this.Close();
            editCustomer form = new editCustomer();
            form.Show();
        }

        private void itemEntry_Load(object sender, EventArgs e)
        {
            if (main.IsUsbDeviceConnected(GlobalVar.pid))
            {
                try
                {
                    if (!GlobalVar.status_value)
                    {
                        MessageBox.Show("لم يتم افتتاح البرنامج لليوم , يرجى الضغط على زر الافتتاح");
                        main_iGOLD form = new main_iGOLD();
                        form.Show();
                        this.Close();
                    }
                    else
                    {
                        if (GlobalVar.Items == "الكل")
                        {
                            if (GlobalVar.itemEntryisMainMax)
                            {
                                maximize_Click(sender, e);
                            }
                            d.Columns.Add("اسم الصائغ", typeof(string));
                            d.Columns.Add("رقم الموبايل", typeof(string));
                            dataGridView1.DataSource = d;
                            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                            dataGridView1.EnableHeadersVisualStyles = false;
                            foreach (DataGridViewColumn col in dataGridView1.Columns)
                            {
                                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            }
                        }
                        else if (GlobalVar.Items == "مصاغ")
                        {
                            customerEntry1 form = new customerEntry1();
                            this.Close();
                            form.Show();
                        }
                    }
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

        private bool check()
        {
            if (dataGridView1.RowCount > 1)
                return true;
            else
                return false;
        }

        private void menu_Click(object sender, EventArgs e)
        {
            if (panel2.Width == (this.Width - 41))
            {
                panel2.Width = this.Width - 141;
                panel2.Height = this.Height - 37;
            }
            else if (panel2.Width == (this.Width - 141))
            {
                panel2.Width = this.Width - 41;
                panel2.Height = this.Height - 37;
            }
        }

        private void main_Gold1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                this.Close();
                main_iGOLD form = new main_iGOLD();
                form.Show();
            }
            else
            {
                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    main_iGOLD form = new main_iGOLD();
                    form.Show();
                }
                else
                {
                }
            }

        }

        private void customerEntry1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                this.Close();
                customerEntry form = new customerEntry();
                form.Show();
            }
            else
            {

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    customerEntry form = new customerEntry();
                    form.Show();
                }
                else
                {
                }
            }

        }

        private void itemEntry1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                this.Close();
                itemEntry form = new itemEntry();
                form.Show();
            }
            else
            {

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    itemEntry form = new itemEntry();
                    form.Show();
                }
                else
                {
                }
            }

        }

        private void sellBill1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                this.Close();
                daily form = new daily();
                form.Show();
            }
            else
            {

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    daily form = new daily();
                    form.Show();
                }
                else
                {
                }
            }

        }

        private void BuyBill1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                this.Close();
                GoldPrice form = new GoldPrice();
                form.Show();
            }
            else
            {

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    GoldPrice form = new GoldPrice();
                    form.Show();
                }
                else
                {
                }
            }

        }

        private void customerPayment1_Click(object sender, EventArgs e)
        {

            if (check())
            {
                customerPayment form = new customerPayment();
                GlobalVar.paymentType = true;
                form.Show();
                this.Close();

            }
            else
            {

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    customerPayment form = new customerPayment();
                    GlobalVar.paymentType = true;
                    form.Show();
                }
                else
                {
                }
            }

        }

        private void ToCustomerPayment1_Click(object sender, EventArgs e)
        {
            Process.Start("calc.exe");
        }

        private void outCome1_Click(object sender, EventArgs e)
        {

            if (check())
            {
                this.Close();
                outcome form = new outcome();
                form.Show();
            }
            else
            {

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    outcome form = new outcome();
                    form.Show();
                }
                else
                {
                }
            }

        }

        private void financeAccount1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                this.Close();
                detailsAccounting form = new detailsAccounting();
                form.Show();
            }
            else
            {

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    detailsAccounting form = new detailsAccounting();
                    form.Show();
                }
                else
                {
                }
            }

        }

        private void companyTotalAccount1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                this.Close();
                companyTotalAccount form = new companyTotalAccount();
                form.Show();
            }
            else
            {

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    companyTotalAccount form = new companyTotalAccount();
                    form.Show();
                }
                else
                {
                }
            }

        }

        private void customerTotalAccount1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                this.Close();
                customerTotalAccount form = new customerTotalAccount();
                form.Show();
            }
            else
            {

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    customerTotalAccount form = new customerTotalAccount();
                    form.Show();
                }
                else
                {
                }
            }
        }

        private void menu_MouseHover(object sender, EventArgs e)
        {
            menu.BackColor = GlobalVar.menuHoverColor;
        }

        private void menu_MouseLeave(object sender, EventArgs e)
        {
            menu.BackColor = GlobalVar.leaveColor;
        } 

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string text = dataGridView1.Columns[1].HeaderText;
            if (text.Equals("اسم الصائغ"))
            {
                TextBox tx = e.Control as TextBox;
                if (tx != null)
                {
                    tx.AutoCompleteMode = AutoCompleteMode.Suggest;
                    tx.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    AutoCompleteStringCollection sc = new AutoCompleteStringCollection();
                    add_items(sc);
                    tx.AutoCompleteCustomSource = sc;
                }
            }
        }
        //
        public void add_items(AutoCompleteStringCollection col)
        {
            try
            {
                con.Open();
                SqlDataReader DataRdr1;
                SqlCommand cmd1 = new SqlCommand("select customerName from customers where userId = " + GlobalVar.id.ToString() , con);
                cmd1.ExecuteNonQuery();
                DataRdr1 = cmd1.ExecuteReader();
                while (DataRdr1.Read())
                {
                    col.Add(DataRdr1.GetString(0));
                }
            }
            catch
            {
                con.Close();
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                for (int i = 0; i < dataGridView1.RowCount -1 ; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() != "")
                    {
                        for (int j = 1; j < dataGridView1.ColumnCount ; j++)
                        {

                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() == "")
                                dataGridView1.Rows[i].Cells[j].Value = "0";
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() != "")
                    {
                        for (int j = 1; j < dataGridView1.ColumnCount; j++)
                        {

                            if (dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() == "")
                                dataGridView1.Rows[i].Cells[j].Value = "0";
                        }
                    }
                }
            }
        }
    }
}
