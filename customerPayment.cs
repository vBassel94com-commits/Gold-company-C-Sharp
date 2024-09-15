using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace iGOLD
{
    public partial class customerPayment : Form
    {
        public customerPayment()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);

        }
        SqlConnection con = new SqlConnection(GlobalVar.dataBaseLocation);
        DataTable d2 = new DataTable();
        DataTable d3 = new DataTable();
        DataTable d4 = new DataTable();

        DataTable d5 = new DataTable();
        DataTable d6 = new DataTable();
        DataTable d7 = new DataTable();
        decimal cash = 0;
        decimal gold21 = 0;
        decimal gold14 = 0;

        private const int cGrip = 16;
        private const int cCaption = 32;
        DataTable dataT = new DataTable();
        main_iGOLD main = new main_iGOLD();
        dailyPayment f = new dailyPayment();
        Itemdb_Class itm = new Itemdb_Class();
        outcome f1 = new outcome();
        daily form = new daily();
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
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
            rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
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
            DialogResult result = MessageBox.Show("هل أنت متأكد بالخروج من ترحيل الدفعات", "الخروج", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
            }
            else
            {
                if (!GlobalVar.fromDetialsAccounting)
                {
                    if (GlobalVar.status_value)
                    {
                        MessageBox.Show("سيتم اغلاق البرنامج");
                        GlobalVar.status_value = false;
                    }
                    main.setBackUp();
                    Application.Exit();
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void close_MouseHover(object sender, EventArgs e)
        {
            this.close.BackColor = GlobalVar.closeHoverColor;
        }

        private void close_MouseLeave(object sender, EventArgs e)
        {
            this.close.BackColor = GlobalVar.leaveColor;
        }

        private void close_MouseEnter(object sender, EventArgs e)
        {
            this.close.BackColor = GlobalVar.closeHoverColor;
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
            if (W == screenW && H == (screenH - 40))
            {
                screenH = 733;
                screenW = 980;
                int A = (Screen.PrimaryScreen.Bounds.Width - screenW) / 2;
                this.SetDesktopLocation(A, 0);
                this.Size = new Size(screenW, screenH-40);
                close.Location = new Point(0, 0);
                maximize.Location = new Point(32, 0);
                minimize.Location = new Point(64, 0);
                bar.Size = new Size(screenW - 64, 37);
                bar.Location = new Point(0, 0);
                yellowPanel.Size = new Size(screenW, 3);
                yellowPanel.Location = new Point(0, 32);
                ////////////////////////////////////////////
                panel2.Width = this.Width - 41;
            }

            else
            {
                this.SetDesktopLocation(0, 0);
                this.Size = new Size(screenW, screenH - 40);
                close.Location = new Point(0, 0);
                maximize.Location = new Point(32, 0);
                minimize.Location = new Point(64, 0);
                bar.Size = new Size(screenW - 64, 37);
                bar.Location = new Point(0, 0);
                yellowPanel.Size = new Size(screenW, 3);
                yellowPanel.Location = new Point(0, 32);
                ////////////////////////////////////////////
                panel2.Width = this.Width - 41;
                panel2.Height = this.Height - 37;
                panelSlide.Height = this.Height - 37;
                main_Gold1.Height = panelSlide.Height / 11;
                customerEntry1.Height = panelSlide.Height / 11;
                itemEntry1.Height = panelSlide.Height / 11;
                sellBill1.Height = panelSlide.Height / 11;
                BuyBill1.Height = panelSlide.Height / 11;
                customerPayment1.Height = panelSlide.Height / 11;
                outCome1.Height = panelSlide.Height / 11;
                financeAccount1.Height = panelSlide.Height / 11;
                companyTotalAccount1.Height = panelSlide.Height / 11;
                customerTotalAccount1.Height = panelSlide.Height / 11;
            }

        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = String.Format("{0:HH : mm}", DateTime.Now);
            time1.Text = String.Format("{0:tt}", DateTime.Now);
            timer1.Start();
        }

        private void BuyBill1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ترحيل الدفعات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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
                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ترحيل الدفعات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ترحيل الدفعات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ترحيل الدفعات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

        private void customerPayment1_Click(object sender, EventArgs e)
        {

            if (check())
            {
                this.Close();
                customerPayment form = new customerPayment();
                form.Show();
            }
            else
            {

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ترحيل الدفعات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    customerPayment form = new customerPayment();
                    form.Show();
                }
                else
                {
                }
            }

        }

        private void ToCustomerPayment1_Click(object sender, EventArgs e)
        {
            if (check())
            {
                this.Close();
                GoldPrice form = new GoldPrice();
                form.Show();
            }
            else
            {

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ترحيل الدفعات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ترحيل الدفعات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ترحيل الدفعات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ترحيل الدفعات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ترحيل الدفعات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

        private bool check()
        {
            if (dataGridView1.Rows.Count > 1)
            {
                return false;
            }
            else { return true; }
        }

        private void save_Click1()
        {

        }

        private void deleteBill_MouseEnter(object sender, EventArgs e)
        {
            form.mouse_Enter_Control((Control)sender);
        }

        private void deleteBill_MouseLeave(object sender, EventArgs e)
        {
            form.mouse_Leave_Control((Control)sender);
        }

        private void daily_Load(object sender, EventArgs e)
        {
            if (GlobalVar.PaymentisMainMax)
            {
                Maximize_Click1();
            }
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
                        if (GlobalVar.Payment == "الكل")
                        {
                            try
                            {
                                timer1.Start();
                                date.Text = String.Format("{0: yyyy / MM / dd}", DateTime.Now);
                                day.Text = String.Format("{0: :dddd}", DateTime.Now);
                                time.Text = String.Format("{0:HH : mm}", DateTime.Now);
                                time1.Text = String.Format("{0:tt}", DateTime.Now);
                                dateTimePicker1.Value = DateTime.Now.Date;
                                System.Threading.Thread.Sleep(10);
                                dataT.Columns.Add("اسم الصائغ", typeof(string));
                                dataT.Columns.Add("استـلام اجور", typeof(decimal));
                                dataT.Columns.Add("تسليم اجور", typeof(decimal));
                                dataT.Columns.Add("استـلام ذهب21", typeof(decimal));
                                dataT.Columns.Add("تسليم ذهب21", typeof(decimal));
                                dataT.Columns.Add("استـلام ذهب14", typeof(decimal));
                                dataT.Columns.Add("تسليم ذهب14", typeof(decimal));
                                dataT.Columns.Add("ملاحظات", typeof(string));
                                dataGridView1.DataSource = dataT;
                                dataGridView1.Columns[7].Width = 320;
                                dataGridView1.Columns[0].Width = 320;
                                con.Open();
                                SqlCommand cmd2 = con.CreateCommand();
                                cmd2.CommandType = CommandType.Text;
                                cmd2.CommandText = "select dailyCash,cashAdd from dailyReport where reportId = (select max(reportId) from dailyReport where userId = " + GlobalVar.id.ToString() + ")";
                                cmd2.ExecuteNonQuery();
                                dataGridView2.DataSource = null;
                                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                                da2.Fill(d2);
                                dataGridView2.DataSource = d2;
                                con.Close();
                                dataGridView2.Columns[0].HeaderText = "الرصيد الافتتاحي";
                                dataGridView2.Columns[1].HeaderText = "تغير الاجور";
                                dataGridView2.AllowUserToAddRows = false;
                                itm.itemName1 = "خشر عيار 14";
                                con.Open();
                                SqlCommand cmd4 = con.CreateCommand();
                                cmd4.CommandType = CommandType.Text;
                                cmd4.CommandText = "select itemWeight from items where itemId = " + itm.getItemId();
                                cmd4.ExecuteNonQuery();
                                dataGridView3.DataSource = null;
                                SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
                                da4.Fill(d4);
                                dataGridView4.DataSource = d4;
                                con.Close();
                                dataGridView4.Columns[0].HeaderText = "رصيد خشر14";
                                dataGridView4.AllowUserToAddRows = false;
                                itm.itemName1 = "خشر عيار 21";
                                con.Open();
                                SqlCommand cmd3 = con.CreateCommand();
                                cmd3.CommandType = CommandType.Text;
                                cmd3.CommandText = "select itemWeight from items where itemId = " + itm.getItemId();//,(select itemWeight from items where itemId = 3)";
                                cmd3.ExecuteNonQuery();
                                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                                da3.Fill(d3);
                                dataGridView3.DataSource = d3;
                                con.Close();
                                dataGridView3.Columns[0].HeaderText = "رصيد خشر21";
                                dataGridView3.AllowUserToAddRows = false;
                                dataGridView5.AllowUserToAddRows = false;
                                dataGridView6.AllowUserToAddRows = false;
                                dataGridView7.AllowUserToAddRows = false;

                                d7.Columns.Add("الرصيد الحالي - اجور", typeof(decimal));
                                cash = Convert.ToDecimal(dataGridView2.Rows[0].Cells[0].Value) + Convert.ToDecimal(dataGridView2.Rows[0].Cells[1].Value);
                                d7.Rows.Add(cash);
                                d6.Columns.Add("الرصيد الحالي - خشر21", typeof(decimal));
                                gold21 = Convert.ToDecimal(dataGridView3.Rows[0].Cells[0].Value);
                                d6.Rows.Add(gold21);
                                d5.Columns.Add("الرصيد الحالي - خشر14", typeof(decimal));
                                gold14 = Convert.ToDecimal(dataGridView4.Rows[0].Cells[0].Value);
                                d5.Rows.Add(gold14);
                                dataGridView5.DataSource = d5;
                                dataGridView6.DataSource = d6;
                                dataGridView7.DataSource = d7;
                                ////////////////////////////////////////////////////
                                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                                dataGridView1.EnableHeadersVisualStyles = false;
                                foreach (DataGridViewColumn col in dataGridView1.Columns)
                                {
                                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                                dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                                dataGridView2.EnableHeadersVisualStyles = false;
                                foreach (DataGridViewColumn col in dataGridView2.Columns)
                                {
                                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                                dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                                dataGridView3.EnableHeadersVisualStyles = false;
                                foreach (DataGridViewColumn col in dataGridView3.Columns)
                                {
                                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                                dataGridView4.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                                dataGridView4.EnableHeadersVisualStyles = false;
                                foreach (DataGridViewColumn col in dataGridView4.Columns)
                                {
                                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                                dataGridView5.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                                dataGridView5.EnableHeadersVisualStyles = false;
                                foreach (DataGridViewColumn col in dataGridView5.Columns)
                                {
                                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                                dataGridView6.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                                dataGridView6.EnableHeadersVisualStyles = false;
                                foreach (DataGridViewColumn col in dataGridView6.Columns)
                                {
                                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                                dataGridView7.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                                dataGridView7.EnableHeadersVisualStyles = false;
                                foreach (DataGridViewColumn col in dataGridView7.Columns)
                                {
                                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                            }
                            catch (Exception ex)
                            { MessageBox.Show(ex.Message); }
                        }
                        else if (GlobalVar.Payment == "دفعة")
                        {
                            dailyPayment form = new dailyPayment();
                            form.Show();
                            this.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
                finally { con.Close(); }
            }
            else
            {
                MessageBox.Show("ان فلاش الحماية غير متصل بالكمبيوتر , يرجى التأكد من اتصال فلاش الحماية بالكمبيوتر" + Environment.NewLine + "ثم أعد تشغيل البرنامج");
                this.Close();
                main.Show();
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {            
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
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

        private void add_items(AutoCompleteStringCollection col)
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
   
        private void save_Click11()
        {
            dateTimePicker1.Value = DateTime.Now.Date;
            dataT.Clear();
            dataGridView1.Columns[7].Width = 320;
            dataGridView1.Columns[0].Width = 320;
        }     
       
        private void savePayment_Click(object sender, EventArgs e)
        {
            con.Close();
            if (dataGridView1.RowCount>1)
            {
                label9.Text = "0";
                label11.Text = (dataGridView1.RowCount * 2).ToString();
                label8.Visible = true;
                label9.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label8.Update();
                label9.Update();
                label10.Update();
                label11.Update();
                label12.Update();
                for ( int i=0 ; i < dataGridView1.Rows.Count - 1 ; i++ )
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString().Trim()!="")
                    {
                        f.dateTimePicker1.Value = dateTimePicker1.Value.Date;
                        if (Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString().Trim()) == 0 && Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value.ToString().Trim()) == 0 && Convert.ToDecimal(dataGridView1.Rows[i].Cells[5].Value.ToString().Trim()) == 0)
                        { }
                        else
                        {
                            if (dataGridView1.Rows[i].Cells[1].Value.ToString().Trim() == "")
                                dataGridView1.Rows[i].Cells[1].Value = 0;
                            if (dataGridView1.Rows[i].Cells[3].Value.ToString().Trim() == "")
                                dataGridView1.Rows[i].Cells[3].Value = 0;
                            if (dataGridView1.Rows[i].Cells[5].Value.ToString().Trim() == "")
                                dataGridView1.Rows[i].Cells[5].Value = 0;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "مصروف")
                            {
                                f1.cashTxtbox.Text= dataGridView1.Rows[i].Cells[1].Value.ToString().Trim();
                                f1.detailsTxtbox.Text= dataGridView1.Rows[i].Cells[7].Value.ToString();
                                f1.dateTimePicker1.Value = dateTimePicker1.Value.Date;
                                f1.save_Click1();
                            }
                            else
                            {
                                f.customerNameTxtbox.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                                f.customerPaymentLabel.Text = "استلام";
                                f.paymentCashTxtbox.Text = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim();
                                f.paymentGold21Txtbox.Text = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();
                                f.paymentGold14Txtbox.Text = dataGridView1.Rows[i].Cells[5].Value.ToString().Trim();
                                f.paymentGold18Txtbox.Text = "0";
                                f.realCash.Text = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim();
                                f.real21.Text = dataGridView1.Rows[i].Cells[3].Value.ToString().Trim();
                                f.real14.Text = dataGridView1.Rows[i].Cells[5].Value.ToString().Trim();
                                f.real18.Text = "0";
                                if (dataGridView1.Rows[i].Cells[7].Value.ToString().Trim() != "")
                                    f.detailsTxtbox.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
                                f.save_Click(sender, e);
                            }
                        }
                        label9.Text = (Convert.ToInt32(label9.Text) + 1).ToString();
                        label9.Update();
                        if (Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value.ToString().Trim()) == 0 && Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString().Trim()) == 0 && Convert.ToDecimal(dataGridView1.Rows[i].Cells[6].Value.ToString().Trim()) == 0)
                        {
                        }
                        else
                        {
                            if (dataGridView1.Rows[i].Cells[2].Value.ToString().Trim() == "")
                                dataGridView1.Rows[i].Cells[2].Value = 0;
                            if (dataGridView1.Rows[i].Cells[4].Value.ToString().Trim() == "")
                                dataGridView1.Rows[i].Cells[4].Value = 0;
                            if (dataGridView1.Rows[i].Cells[6].Value.ToString().Trim() == "")
                                dataGridView1.Rows[i].Cells[6].Value = 0;
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == "مصروف")
                            {
                                f1.cashTxtbox.Text = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim();
                                f1.detailsTxtbox.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
                                f1.dateTimePicker1.Value = dateTimePicker1.Value.Date;
                                f1.save_Click1();
                            }
                            else
                            {
                                f.customerNameTxtbox.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                                f.customerPaymentLabel.Text = "تسليم";
                                f.paymentCashTxtbox.Text = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim();
                                f.paymentGold21Txtbox.Text = dataGridView1.Rows[i].Cells[4].Value.ToString().Trim();
                                f.paymentGold14Txtbox.Text = dataGridView1.Rows[i].Cells[6].Value.ToString().Trim();
                                f.paymentGold18Txtbox.Text = "0";
                                f.realCash.Text = dataGridView1.Rows[i].Cells[2].Value.ToString().Trim();
                                f.real21.Text = dataGridView1.Rows[i].Cells[4].Value.ToString().Trim();
                                f.real14.Text = dataGridView1.Rows[i].Cells[6].Value.ToString().Trim();
                                f.real18.Text = "0";
                                if (dataGridView1.Rows[i].Cells[7].Value.ToString().Trim() != "")
                                    f.detailsTxtbox.Text = dataGridView1.Rows[i].Cells[7].Value.ToString();
                                f.save_Click(sender, e);
                            }
                        }
                        label9.Text = (Convert.ToInt32(label9.Text) + 1).ToString();
                        label9.Update();
                    }
                }
                MessageBox.Show("تم ترحيل كافة الدفعات");
                save_Click11();
                label8.Visible = false;
                label9.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
            }
            else
            {
                MessageBox.Show("لا يوجد دفعات لحفظها");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount>1)
            {
                for (int i=0;i<dataGridView1.RowCount-1;i++)
                {
                    for (int j=1;j<7;j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() =="")
                        {
                            dataGridView1.Rows[i].Cells[j].Value = 0;
                        }
                    }
                    for (int j = 1; j < 7; j++)
                    {
                        if (j == 1)
                            cash += Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                        else if (j == 2)
                            cash -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                        else if (j == 3)
                            gold21 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                        else if (j == 4)
                            gold21 -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                        else if (j == 5)
                            gold14 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                        else if (j == 6)
                            gold14 -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                    }
                }
                d5 = null;
                d6 = null;
                d7 = null;
                d7.Columns.Add("الرصيد الحالي - اجور", typeof(decimal));
                d6.Columns.Add("الرصيد الحالي - خشر21", typeof(decimal));
                d5.Columns.Add("الرصيد الحالي - خشر14", typeof(decimal));
                d7.Rows.Add(cash);
                d6.Rows.Add(gold21);
                d5.Rows.Add(gold14);
            }
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dataGridView1.RowCount > 1)
            {
                cash = 0;
                gold21 = 0;
                gold14 = 0;
                cash = Convert.ToDecimal(dataGridView2.Rows[0].Cells[0].Value) + Convert.ToDecimal(dataGridView2.Rows[0].Cells[1].Value);
                gold21 = Convert.ToDecimal(dataGridView3.Rows[0].Cells[0].Value);
                gold14 = Convert.ToDecimal(dataGridView4.Rows[0].Cells[0].Value);
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    for (int j = 1; j < 7; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() == "")
                        {
                            dataGridView1.Rows[i].Cells[j].Value = 0;
                        }
                    }
                    for (int j = 1; j < 7; j++)
                    {
                        if (j == 1)
                            cash += Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                        else if (j == 2)
                            cash -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                        else if (j == 3)
                            gold21 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                        else if (j == 4)
                            gold21 -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                        else if (j == 5)
                            gold14 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                        else if (j == 6)
                            gold14 -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                    }
                }
                dataGridView7.Rows[0].Cells[0].Value = cash;
                dataGridView6.Rows[0].Cells[0].Value = gold21;
                dataGridView5.Rows[0].Cells[0].Value = gold14;
            }
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 1)
            {
                cash = 0;
                gold21 = 0;
                gold14 = 0;
                cash = Convert.ToDecimal(dataGridView2.Rows[0].Cells[0].Value) + Convert.ToDecimal(dataGridView2.Rows[0].Cells[1].Value);
                gold21 = Convert.ToDecimal(dataGridView3.Rows[0].Cells[0].Value);
                gold14 = Convert.ToDecimal(dataGridView4.Rows[0].Cells[0].Value);
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    for (int j = 1; j < 7; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() == "")
                        {
                            dataGridView1.Rows[i].Cells[j].Value = 0;
                        }
                    }
                    for (int j = 1; j < 7; j++)
                    {
                        if (j == 1)
                            cash += Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                        else if (j == 2)
                            cash -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                        else if (j == 3)
                            gold21 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                        else if (j == 4)
                            gold21 -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                        else if (j == 5)
                            gold14 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                        else if (j == 6)
                            gold14 -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                    }
                }
                dataGridView7.Rows[0].Cells[0].Value = cash;
                dataGridView6.Rows[0].Cells[0].Value = gold21;
                dataGridView5.Rows[0].Cells[0].Value = gold14;
            }


        }

        public void addComma(Control cont)
        {
            if (cont.Text != "")
            {
                if (cont.Text.Contains(" "))
                {
                    cont.Text = cont.Text.Replace(" ", "");
                    try
                    {
                        cont.Text = String.Format("{0:N}", Convert.ToDecimal(cont.Text.Replace(" ", "")));
                    }
                    finally { }
                }
                else
                {
                    try
                    {
                        cont.Text = String.Format("{0:N}", Convert.ToDecimal(cont.Text));
                    }
                    catch (Exception x) { MessageBox.Show(x.Message); }
                    finally
                    {
                        cont.Select();
                    }
                }
            }
        }

        public void addCashComma(Control cont)
        {
            if (cont.Text != "")
            {
                if (cont.Text.Contains(" "))
                {
                    cont.Text = cont.Text.Replace(" ", "");
                    try
                    {
                        cont.Text = String.Format("{0:N0}", Convert.ToDecimal(cont.Text));
                    }
                    finally { }
                }
                else
                {
                    try
                    {
                        cont.Text = String.Format("{0:N0}", Convert.ToDecimal(cont.Text));
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                    finally
                    {
                        cont.Select();
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!check())
                {
                    int rowIndex1 = dataGridView1.CurrentCell.RowIndex;
                    dataGridView1.Rows.RemoveAt(rowIndex1);
                    if (dataGridView1.RowCount > 1)
                    {
                        for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                        {
                            for (int j = 1; j < 7; j++)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() == "")
                                {
                                    dataGridView1.Rows[i].Cells[j].Value = 0;
                                }
                            }
                            for (int j = 1; j < 7; j++)
                            {
                                if (j == 1)
                                    cash += Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                                else if (j == 2)
                                    cash -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                                else if (j == 3)
                                    gold21 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                                else if (j == 4)
                                    gold21 -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                                else if (j == 5)
                                    gold14 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                                else if (j == 6)
                                    gold14 -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value);
                            }
                        }
                        d5 = null;
                        d6 = null;
                        d7 = null;
                        d7.Columns.Add("الرصيد الحالي - اجور", typeof(decimal));
                        d6.Columns.Add("الرصيد الحالي - خشر21", typeof(decimal));
                        d5.Columns.Add("الرصيد الحالي - خشر14", typeof(decimal));
                        d7.Rows.Add(cash);
                        d6.Rows.Add(gold21);
                        d5.Rows.Add(gold14);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.ForeColor = Color.FromArgb(254, 199, 32);
            button2.BackColor = Color.FromArgb(35, 38, 32);
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.ForeColor = Color.FromArgb(254, 199, 32);
            button2.BackColor = Color.FromArgb(35, 38, 32);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackColor = Color.FromArgb(254, 199, 32);
            button2.ForeColor = Color.Maroon;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savePayment_Click(sender, e);
        }
    }
}
