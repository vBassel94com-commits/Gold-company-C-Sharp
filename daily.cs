using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace iGOLD
{
    public partial class daily : Form
    {
        public daily()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.KeyPreview = true;
            textBox7.AutoCompleteCustomSource = customers;
            textBox7.AutoCompleteMode = AutoCompleteMode.None;
            textBox7.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox7.Visible = false;
            textBox1.AutoCompleteCustomSource = items;
            textBox1.AutoCompleteMode = AutoCompleteMode.None;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.Visible = false;
        }

        SqlConnection con = new SqlConnection(GlobalVar.dataBaseLocation);
        private const int cGrip = 16;
        private const int cCaption = 32;
        //DataTable dataT = new DataTable();
        DataTable dataT1 = new DataTable();
        DataTable dataT3 = new DataTable();
        DataTable dataT4 = new DataTable();
        DataTable dataT5 = new DataTable();
        DataTable dataT7 = new DataTable();
        main_iGOLD main = new main_iGOLD();

        Customerdb_Class cus = new Customerdb_Class();
        Itemdb_Class itm = new Itemdb_Class();
        paymentdb_Class pay = new paymentdb_Class();
        dailyPayment f = new dailyPayment();
        int ii = -1;
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        decimal rCash = 0;
        decimal rGold21 = 0;
        decimal rGold18 = 0;
        decimal rGold14 = 0;
        decimal cash = 0;
        decimal gold21 = 0;
        decimal gold18 = 0;
        decimal gold14 = 0;
        string name = "";
        string s = "";
        const int WS_MINIMIZEBOX = 0x20000;
        const int CS_DBLCLKS = 0x1;
        AutoCompleteStringCollection items = new AutoCompleteStringCollection();
        AutoCompleteStringCollection customers = new AutoCompleteStringCollection();     

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
            DialogResult result = MessageBox.Show("هل أنت متأكد بالخروج من الفواتير", "الخروج", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

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
                screenH = 703;
                screenW = 1141;
                int A = (Screen.PrimaryScreen.Bounds.Width - screenW) / 2;
                this.SetDesktopLocation(A, 0);
                this.Size = new Size(screenW, screenH);
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
                main_Gold1.Height = panelSlide.Height / 7;
                itemEntry1.Height = panelSlide.Height / 7;
                BuyBill1.Height = panelSlide.Height / 7;
                customerPayment1.Height = panelSlide.Height / 7;
                financeAccount1.Height = panelSlide.Height / 7;
                companyTotalAccount1.Height = panelSlide.Height / 7;
            }
            //width(size)
            panel8.Width = panel2.Width - 3;
            panel1.Width = panel2.Width - 3;
            panel10.Width = panel2.Width - 3;
            panel6.Width = panel2.Width - 3;
            //height(size)
            panel8.Height = Convert.ToInt32(panel2.Height * 0.0778);
            panel1.Height = Convert.ToInt32(panel2.Height * 0.26);
            panel10.Height = Convert.ToInt32(panel2.Height * 0.448);
            panel6.Height = Convert.ToInt32(panel2.Height * 0.21);
            //panel6.Height = panel2.Height - panel1.Height - panel8.Height - panel10.Height-5;
            //location(Y)
            panel8.Location = new Point(0, 0);
            panel1.Location = new Point(0, panel8.Location.Y + panel8.Height + 1);
            panel10.Location = new Point(0, panel1.Location.Y + panel1.Height);
            panel6.Location = new Point(0, panel10.Location.Y + panel10.Height);
            //DGV7
            dataGridView7.Height = Convert.ToInt32(panel1.Height * 0.325) - 8;
            dataGridView7.Width = panel7.Width + panel9.Width - 1;
            dataGridView7.Location = new Point(panel7.Location.X, panel7.Location.Y - dataGridView7.Height);
            //DGV3
            dataGridView3.Width = Convert.ToInt32(panel10.Width * 0.30);
            dataGridView3.Height = panel10.Height;
            dataGridView3.Location = new Point(panel10.Location.X + 3, 0);
            //DGV1
            dataGridView1.Width = Convert.ToInt32(panel10.Width * 0.70);
            dataGridView1.Height = panel10.Height;
            dataGridView1.Location = new Point(dataGridView3.Location.X + dataGridView3.Width, 0);
            //panel11
            panel11.Width = Convert.ToInt32(panel6.Width * 0.30);
            panel11.Height = panel6.Height;
            panel11.Location = new Point(panel6.Location.X + 3, 0);
            //DGV5
            dataGridView5.Width = Convert.ToInt32(panel6.Width * 0.70);
            dataGridView5.Height = panel6.Height;
            dataGridView5.Location = new Point(panel11.Location.X + panel11.Width, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = String.Format("{0:HH : mm}", DateTime.Now);
            time1.Text = String.Format("{0:tt}", DateTime.Now);
            timer1.Start();
        }

        private void daily_Load(object sender, EventArgs e)
        {
            if (GlobalVar.billsisMainMax)
            {
                maximize_Click(sender, e);
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
                        timer1.Start();
                        date.Text = String.Format("{0: yyyy / MM / dd}", DateTime.Now);
                        day.Text = String.Format("{0: :dddd}", DateTime.Now);
                        time.Text = String.Format("{0:HH : mm}", DateTime.Now);
                        time1.Text = String.Format("{0:tt}", DateTime.Now);
                        cashTxtbox.Text = "0";
                        totalCashTxtbox.Text = "0";
                        discountTxtbox.Text = "0";
                        gold14Txtbox.Text = "0";
                        gold18Txtbox.Text = "0";
                        gold21Txtbox.Text = "0";
                        real14.Text = "0";
                        real18.Text = "0";
                        real21.Text = "0";
                        realCash.Text = "0";
                        cashAfter.Text = "0";
                        gold14After.Text = "0";
                        gold18After.Text = "0";
                        gold21After.Text = "0";
                        dateTimePicker1.Value = DateTime.Now.Date;
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        System.Threading.Thread.Sleep(10);

                        System.Threading.Thread.Sleep(10);
                        dataT7.Columns.Add("الصائغ", typeof(string));
                        dataT7.Columns.Add(GlobalVar.gold21Label, typeof(string));
                        dataT7.Columns.Add(GlobalVar.gold18Label, typeof(string));
                        dataT7.Columns.Add(GlobalVar.gold14Label, typeof(string));
                        dataT7.Columns.Add(GlobalVar.cashLabel, typeof(string));
                        dataT7.Rows.Add("", "0", "0", "0", "0");
                        dataGridView7.DataSource = dataT7;
                        dataT5.Columns.Add("نوع الحركة", typeof(string));
                        dataT5.Columns.Add(GlobalVar.gold21Label, typeof(decimal));
                        dataT5.Columns.Add(GlobalVar.gold18Label, typeof(decimal));
                        dataT5.Columns.Add(GlobalVar.gold14Label, typeof(decimal));
                        dataT5.Columns.Add(GlobalVar.cashLabel, typeof(decimal));
                        dataT5.Columns.Add("ملاحظات", typeof(decimal));
                        dataGridView5.DataSource = dataT5;
                        if (!GlobalVar.isGold21)
                        {
                            dataGridView7.Columns[1].Visible = GlobalVar.isGold21;
                            dataGridView5.Columns[1].Visible = GlobalVar.isGold21;
                        }
                        if (!GlobalVar.isGold18)
                        {
                            dataGridView7.Columns[2].Visible = GlobalVar.isGold18;
                            dataGridView5.Columns[2].Visible = GlobalVar.isGold18;
                        }
                        if (!GlobalVar.isGold14)
                        {
                            dataGridView7.Columns[3].Visible = GlobalVar.isGold14;
                            dataGridView5.Columns[3].Visible = GlobalVar.isGold14;
                        }
                        if (!GlobalVar.isCash)
                        {
                            dataGridView7.Columns[4].Visible = GlobalVar.isCash;
                            dataGridView5.Columns[4].Visible = GlobalVar.isCash;
                        }
                        dataT1.Columns.Add("الكود", typeof(string));
                        dataT1.Columns.Add("المصوغـــــــات", typeof(string));
                        dataT1.Columns.Add("الــوزن", typeof(decimal));
                        dataT1.Columns.Add("العــدد", typeof(decimal));
                        dataT1.Columns.Add("أجور الصياغة", typeof(decimal));
                        dataT1.Columns.Add("خياس الصياغة", typeof(decimal));
                        dataT1.Columns.Add("القيمةالاجمالية", typeof(decimal));
                        //dataT.Rows.Add("لا تستخدم هذا السطر", 0, 0, 0, 0);
                        dataGridView1.DataSource = dataT1;
                        dataGridView1.Columns["المصوغـــــــات"].Width = 300;
                        dataGridView1.Columns["العــدد"].Width = 60;
                        dataGridView1.Columns["الــوزن"].Width = 80;
                        dataGridView1.Columns["أجور الصياغة"].Width = 109;
                        dataT3.Columns.Add("قبل التحويل", typeof(decimal));
                        dataT3.Columns.Add("بيان التحويل", typeof(string));
                        dataT3.Columns.Add("بعدالتحويل", typeof(decimal));
                        dataGridView3.DataSource = dataT3;
                        dataGridView3.Columns[1].Width = 150;
                        dataT4.Columns.Add("قبل التحويل", typeof(decimal));
                        dataT4.Columns.Add("بيان التحويل", typeof(string));
                        dataT4.Columns.Add("بعدالتحويل", typeof(decimal));
                        dataGridView4.DataSource = dataT4;
                        dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                        dataGridView1.EnableHeadersVisualStyles = false;
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        dataGridView7.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                        dataGridView7.EnableHeadersVisualStyles = false;
                        foreach (DataGridViewColumn col in dataGridView7.Columns)
                        {
                            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        dataGridView5.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                        dataGridView5.EnableHeadersVisualStyles = false;
                        foreach (DataGridViewColumn col in dataGridView5.Columns)
                        {
                            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                        dataGridView3.EnableHeadersVisualStyles = false;
                        foreach (DataGridViewColumn col in dataGridView3.Columns)
                        {
                            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        customers = GlobalVar.Auto_customers;
                        items = GlobalVar.Auto_items;
                        textBox7.AutoCompleteCustomSource = customers;
                        textBox1.AutoCompleteCustomSource = items;

                        IDTxtBox.Text = GlobalVar.editBillId;
                        if (editLabel.Visible)
                        {
                            updateBill.Visible = true;
                            updateBill.ForeColor = Color.Red;
                            saveBill.Visible = false;
                            //updateBill.Size = new Size(215, 30);
                            panel4.Visible = false;
                            panel5.Visible = false;
                            panel6.Visible = false;
                            panelSlide.Visible = false;
                            menu.Visible = false;
                            dataGridView3.Visible = true;
                            calc.Visible = false;
                            currencyLabel5.Visible = false;
                            label8.Visible = false;
                            goldPrice.Visible = false;
                            printBill.Visible = false;
                            deletePay.Visible = false;
                        }
                        else
                        {
                            saveBill.Visible = true;
                            updateBill.Visible = false;
                            //saveBill.Size = new Size(215, 30);
                            panel4.Visible = true;
                            panel5.Visible = true;
                            panel6.Visible = true;
                            panelSlide.Visible = true;
                            menu.Visible = true;
                            dataGridView3.Visible = true;
                            calc.Visible = true;
                            currencyLabel5.Visible = true;
                            label8.Visible = true;
                            goldPrice.Visible = true;
                            printBill.Visible = true;
                            deletePay.Visible = true;
                        }
                        if (billTypeLabel.Text.Trim() == "شراء")
                        {
                            billTypeLabel.ForeColor = Color.Salmon;
                        }
                        GlobalVar.editBillId = "";
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
                main_iGOLD fo = new main_iGOLD();
                fo.Show();
            }
            else
            {

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على فواتير", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    main_iGOLD fo = new main_iGOLD();
                    fo.Show();
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
                itemEntry fo = new itemEntry();
                fo.Show();
            }
            else
            {
                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على فواتير", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    itemEntry fo = new itemEntry();
                    fo.Show();
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
                customerPayment fo = new customerPayment();
                fo.Show();
                this.Close();

            }
            else
            {
                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على فواتير", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    customerPayment fo = new customerPayment();
                    fo.Show();
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
                GoldPrice fo = new GoldPrice();
                fo.Show();
            }
            else
            {
                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على فواتير", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    GoldPrice fo = new GoldPrice();
                    fo.Show();
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
                detailsAccounting fo = new detailsAccounting();
                fo.Show();
            }
            else
            {
                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على فواتير", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    detailsAccounting fo = new detailsAccounting();
                    fo.Show();
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
                companyTotalAccount fo = new companyTotalAccount();
                fo.Show();
            }
            else
            {
                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على فواتير", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    companyTotalAccount fo = new companyTotalAccount();
                    fo.Show();
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
            if (dataGridView7.Rows[0].Cells[0].Value.ToString() == "")
            {
                MessageBox.Show("يرجى كتابة اسم الصائغ");
            }
            else if (!check())
            {
            }
        }

        private string idLabel(string i)
        {
            string str = "";
            try
            {
                int id = Convert.ToInt32(i);

                if (id < 10)
                {
                    str = ("000" + id);
                }
                else if (id < 100)
                {
                    str = ("00" + id);
                }
                else if (id < 1000)
                {
                    str = ("0" + id);
                }
                else
                {
                    str = Convert.ToString(id);
                }
                return str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
        }

        private void availableName()
        {
            try
            {
                if (dataGridView7.Rows[0].Cells[0].Value.ToString().Length > 1)
                {
                    SqlConnection con = new SqlConnection(GlobalVar.dataBaseLocation);
                    pay.customerName1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
                    int custmId = pay.GetCustomerid();
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT TotalCash , Total21 , Total18 , Total14 from customers where customerId = '" + Convert.ToString(custmId) + "' ";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView7.Rows[0].Cells[1].Value = dt.Rows[0][1].ToString();
                        dataGridView7.Rows[0].Cells[2].Value = dt.Rows[0][2].ToString();
                        dataGridView7.Rows[0].Cells[3].Value = dt.Rows[0][3].ToString();
                        dataGridView7.Rows[0].Cells[4].Value = dt.Rows[0][0].ToString();
                    }
                    dataGridView7.Rows[0].Cells[1].Style.BackColor = Color.FromArgb(254, 199, 32);
                    dataGridView7.Rows[0].Cells[2].Style.BackColor = Color.FromArgb(254, 199, 32);
                    dataGridView7.Rows[0].Cells[3].Style.BackColor = Color.FromArgb(254, 199, 32);
                    dataGridView7.Rows[0].Cells[4].Style.BackColor = Color.FromArgb(254, 199, 32);

                    if (Convert.ToDecimal(dataGridView7.Rows[0].Cells[1].Value.ToString()) > 0)
                    {
                        dataT7.Rows[0][1] = dataT7.Rows[0][1] + " لنا";
                        dataGridView7.Rows[0].Cells[1].Style.BackColor = Color.MediumSeaGreen;
                        dataGridView7.Rows[0].Cells[1].Style.ForeColor = Color.White;
                    }
                    else if (Convert.ToDecimal(dataGridView7.Rows[0].Cells[1].Value.ToString()) < 0)
                    {
                        dataT7.Rows[0][1] = dataT7.Rows[0][1] + " لكم";
                        dataGridView7.Rows[0].Cells[1].Style.BackColor = Color.OrangeRed;
                        dataGridView7.Rows[0].Cells[1].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        dataT7.Rows[0][1] = "لايوجد";
                    }

                    if (Convert.ToDecimal(dataGridView7.Rows[0].Cells[2].Value.ToString()) > 0)
                    {
                        dataT7.Rows[0][2] = dataT7.Rows[0][2] + " لنا";
                        dataGridView7.Rows[0].Cells[2].Style.BackColor = Color.MediumSeaGreen;
                        dataGridView7.Rows[0].Cells[2].Style.ForeColor = Color.White;
                    }
                    else if (Convert.ToDecimal(dataGridView7.Rows[0].Cells[2].Value.ToString()) < 0)
                    {
                        dataT7.Rows[0][2] = dataT7.Rows[0][2] + " لكم";
                        dataGridView7.Rows[0].Cells[2].Style.BackColor = Color.OrangeRed;
                        dataGridView7.Rows[0].Cells[2].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        dataT7.Rows[0][2] = "لايوجد";
                    }

                    if (Convert.ToDecimal(dataGridView7.Rows[0].Cells[3].Value.ToString()) > 0)
                    {
                        dataT7.Rows[0][3] = dataT7.Rows[0][3] + " لنا";
                        dataGridView7.Rows[0].Cells[3].Style.BackColor = Color.MediumSeaGreen;
                        dataGridView7.Rows[0].Cells[3].Style.ForeColor = Color.White;
                    }
                    else if (Convert.ToDecimal(dataGridView7.Rows[0].Cells[3].Value.ToString()) < 0)
                    {
                        dataT7.Rows[0][3] = dataT7.Rows[0][3] + " لكم";
                        dataGridView7.Rows[0].Cells[3].Style.BackColor = Color.OrangeRed;
                        dataGridView7.Rows[0].Cells[3].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        dataT7.Rows[0][3] = "لايوجد";
                    }

                    if (Convert.ToDecimal(dataGridView7.Rows[0].Cells[4].Value.ToString()) > 0)
                    {
                        dataT7.Rows[0][4] = dataT7.Rows[0][4] + " لنا";
                        dataGridView7.Rows[0].Cells[4].Style.BackColor = Color.MediumSeaGreen;
                        dataGridView7.Rows[0].Cells[4].Style.ForeColor = Color.White;
                    }
                    else if (Convert.ToDecimal(dataGridView7.Rows[0].Cells[4].Value.ToString()) < 0)
                    {
                        dataT7.Rows[0][4] = dataT7.Rows[0][4] + " لكم";
                        dataGridView7.Rows[0].Cells[4].Style.BackColor = Color.OrangeRed;
                        dataGridView7.Rows[0].Cells[4].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        dataT7.Rows[0][4] = "لايوجد";
                    }
                    dataGridView1.Focus();
                    dataGridView1.Select();
                }
            }
            catch(Exception er) { MessageBox.Show(er.Message); }
            finally { con.Close(); }
        }

        private void totalsCalc()
        {
            decimal cashSum = 0;
            decimal gold14Sum = 0;
            decimal gold18Sum = 0;
            decimal gold21Sum = 0;

            if (dataGridView1.Rows.Count > 1)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString().Trim() != "" && dataGridView1.Rows[i].Cells[1].Value.ToString().Trim() != "" && dataGridView1.Rows[i].Cells[2].Value.ToString().Trim() != "" && dataGridView1.Rows[i].Cells[3].Value.ToString().Trim() != "")
                    {
                        cashSum += Convert.ToInt64(dataGridView1.Rows[i].Cells[4].Value);
                        s = dataGridView1.Rows[i].Cells[0].Value.ToString();
                        if (Equals(s.Substring(s.Length - 2).Trim(), "14"))
                        {
                            if (s.Substring(0, 3) != "خشر")
                                gold14Sum += Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value);
                        }
                        else if (Equals(s.Substring(s.Length - 2).Trim(), "18"))
                        {
                            if (s.Substring(0, 3) != "خشر")
                                gold18Sum += Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value);
                        }
                        else if (Equals(s.Substring(s.Length - 2).Trim(), "21"))
                        {
                            if (s.Substring(0, 3) != "خشر")
                                gold21Sum += Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value);
                        }
                    }
                }
                totalCashTxtbox.Text = cashSum.ToString();
                gold14Txtbox.Text = gold14Sum.ToString();
                gold18Txtbox.Text = gold18Sum.ToString();
                gold21Txtbox.Text = gold21Sum.ToString();
            }
            else
            {
                cashSum = 0;
                gold14Sum = 0;
                gold18Sum = 0;
                gold21Sum = 0;
                totalCashTxtbox.Text = cashSum.ToString();
                gold14Txtbox.Text = gold14Sum.ToString();
                gold18Txtbox.Text = gold18Sum.ToString();
                gold21Txtbox.Text = gold21Sum.ToString();
            }
        }

        private void label44_Click(object sender, EventArgs e)
        {
            if (label44.Text == "-")
            {
                label44.Text = "+";
                label44.ForeColor = Color.MediumSeaGreen;

            }
            else if (label44.Text == "+")
            {
                label44.Text = "-";
                label44.ForeColor = Color.Salmon;


            }
            discountTxtbox_OnValueChanged(sender, e);
        }

        private void billTypeLabel_Click(object sender, EventArgs e)
        {
            if (billTypeLabel.Text == "بيع")
            {
                billTypeLabel.Text = "شراء";
                billTypeLabel.ForeColor = Color.Salmon;
            }
            else if (billTypeLabel.Text == "شراء")
            {
                billTypeLabel.Text = "بيع";
                billTypeLabel.ForeColor = Color.Gold;
            }
            payCash_OnValueChanged(sender, e);
            payGold21_OnValueChanged(sender, e);
            payGold18_OnValueChanged(sender, e);
            payGold14_OnValueChanged(sender, e);
        }

        private void detailsTxtbox_MouseHover(object sender, EventArgs e)
        {
            if (detailsTxtbox.Text == "اكتب ملاحظات عن الفاتورة")
            {
                detailsTxtbox.Text = "";
            }
        }

        private void detailsTxtbox_MouseLeave(object sender, EventArgs e)
        {
            if (detailsTxtbox.Text.Trim() == "")
            {
                detailsTxtbox.Text = "اكتب ملاحظات عن الفاتورة";
            }
        }

        private void discountTxtbox_OnValueChanged(object sender, EventArgs e)
        {
            if (totalCashTxtbox.Text == "")
            { totalCashTxtbox.Text = "0"; }
            if (discountTxtbox.Text == "")
            { discountTxtbox.Text = "0"; }
            if (label44.Text == "-")
            {
                cashTxtbox.Text = Convert.ToString(Convert.ToDecimal(totalCashTxtbox.Text) - Convert.ToDecimal(discountTxtbox.Text));
            }
            else if (label44.Text == "+")
            {
                cashTxtbox.Text = Convert.ToString(Convert.ToDecimal(totalCashTxtbox.Text) + Convert.ToDecimal(discountTxtbox.Text));
            }
        }

        public void mouse_Enter_Control(Control cont)
        {
            cont.ForeColor = Color.FromArgb(254, 199, 32);
            cont.BackColor = Color.FromArgb(35, 38, 32);
        }

        public void mouse_Leave_Control(Control cont)
        {
            cont.BackColor = Color.FromArgb(254, 199, 32);
            cont.ForeColor = Color.FromArgb(35, 38, 32);
        }

        private void deleteBill_MouseEnter(object sender, EventArgs e)
        {
            mouse_Enter_Control((Control)sender);
        }

        private void deleteBill_MouseLeave(object sender, EventArgs e)
        {
            mouse_Leave_Control((Control)sender);
        }

        public void mouse_Enter_Control_Maroon(Control cont)
        {
            cont.ForeColor = Color.FromArgb(254, 199, 32);
            cont.BackColor = Color.FromArgb(35, 38, 32);
        }

        public void mouse_Leave_Control_Maroon(Control cont)
        {
            cont.BackColor = Color.FromArgb(254, 199, 32);
            cont.ForeColor = Color.Maroon;
        }

        private void deleteBill_MouseEnter_Maroon(object sender, EventArgs e)
        {
            mouse_Enter_Control_Maroon((Control)sender);
        }

        private void deleteBill_MouseLeave_Maroon(object sender, EventArgs e)
        {
            mouse_Leave_Control_Maroon((Control)sender);
        }

        private void totalCashTxtbox_OnValueChanged(object sender, EventArgs e)
        {
            if (totalCashTxtbox.Text == "")
            { totalCashTxtbox.Text = "0"; }
            if (cashTxtbox.Text == "")
            { cashTxtbox.Text = "0"; }
            cashTxtbox.Text = totalCashTxtbox.Text;
            discountTxtbox.Text = Convert.ToString(Convert.ToDecimal(totalCashTxtbox.Text) - Convert.ToDecimal(cashTxtbox.Text));
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount > 1)
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString().Trim() != "" && dataGridView1.Rows[i].Cells[2].Value.ToString().Trim() != "" && dataGridView1.Rows[i].Cells[3].Value.ToString().Trim() != "")
                    {
                        if (dataGridView1.Rows[i].Cells[2].Value.ToString().Trim() == "0")
                        {
                            dataGridView1.Rows[i].Cells[4].Value = Convert.ToDecimal(Convert.ToInt64(Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString().Trim()) * Convert.ToInt64(dataGridView1.Rows[i].Cells[3].Value.ToString().Trim())));
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[4].Value = Convert.ToDecimal(Convert.ToInt64(Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value.ToString().Trim()) * Convert.ToInt64(dataGridView1.Rows[i].Cells[3].Value.ToString().Trim())));
                        }

                    }
                }
                totalsCalc();
            }
        }

        public void save_payment()
        {
            if (Equals(dataGridView7.Rows[0].Cells[0].Value.ToString(), "أدخل اسم الصائغ"))
            {
                MessageBox.Show("!!.. يرجى ادخال اسم الصائغ");
            }
            else
            {
                if (Equals(payCash.Text, ""))
                {
                    payCash.Text = "0";
                }
                if (Equals(payGold21.Text, ""))
                {
                    payGold21.Text = "0";
                }
                if (Equals(payGold18.Text, ""))
                {
                    payGold18.Text = "0";
                }
                if (Equals(payGold14.Text, ""))
                {
                    payGold14.Text = "0";
                }
                string customerName = dataGridView7.Rows[0].Cells[0].Value.ToString();
                cus.name1 = customerName;
                if (cus.checkCustomerExist() == "true")
                {
                    if (Convert.ToDecimal(payCash.Text) != 0 || Convert.ToDecimal(payGold21.Text) != 0 || Convert.ToDecimal(payGold18.Text) != 0 || Convert.ToDecimal(payGold14.Text) != 0)
                    {
                        pay.customerName1 = customerName;
                        pay.customerId1 = pay.GetCustomerid();
                        if (dataGridView3.RowCount > 1)
                        {
                            rCash = Convert.ToDecimal(realCash.Text.Trim());
                            rGold21 = Convert.ToDecimal(real21.Text.Trim());
                            rGold18 = Convert.ToDecimal(real18.Text.Trim());
                            rGold14 = Convert.ToDecimal(real14.Text.Trim());
                        }
                        else
                        {
                            rCash = Convert.ToDecimal(payCash.Text.Trim());
                            rGold21 = Convert.ToDecimal(payGold21.Text.Trim());
                            rGold18 = Convert.ToDecimal(payGold18.Text.Trim());
                            rGold14 = Convert.ToDecimal(payGold14.Text.Trim());
                        }
                        cash = Convert.ToDecimal(payCash.Text.Trim());
                        gold21 = Convert.ToDecimal(payGold21.Text.Trim());
                        gold18 = Convert.ToDecimal(payGold18.Text.Trim());
                        gold14 = Convert.ToDecimal(payGold14.Text.Trim());

                        string usrName = GlobalVar.userName;

                        pay.userName1 = usrName;
                        pay.userId1 = pay.getUserId();
                        pay.item14Id1 = 0;
                        pay.item18Id1 = 0;
                        pay.item21Id1 = 0;
                        pay.item14Count1 = 0;
                        pay.item18Count1 = 0;
                        pay.item21Count1 = 0;
                        if (payType.Text == "استلام")
                        {
                            itm.billType1 = "شراء";
                            pay.paymentTypeInt1 = -1;
                            if (dataGridView7.Rows[0].Cells[0].Value.ToString() == "راس مال")
                            {
                                pay.paymentTypeString1 = "راس مال";
                                pay.paymentTypeInt1 = -1;
                            }
                            else
                            {
                                pay.paymentTypeString1 = payType.Text;
                            }
                            itm.cash1 = rCash;
                            itm.addCashToFonding();
                            if (rGold14 != 0)
                            {
                                itm.itemCount1 = 0;
                                itm.itemFees1 = 0;
                                itm.itemCarat1 = 14;
                                itm.itemWeight1 = rGold14;
                                itm.itemName1 = "خشر عيار 14";
                                itm.itemId1 = itm.getItemId();
                                itm.buyItem1();
                                itm.itemCarat1 = 14;
                                itm.itemWeight1 = rGold14;
                                itm.addGoldToFonding();
                            }
                            if (rGold18 != 0)
                            {
                                itm.itemCount1 = 0;
                                itm.itemFees1 = 0;
                                itm.itemCarat1 = 18;
                                itm.itemWeight1 = rGold18;
                                itm.itemName1 = "خشر عيار 18";
                                itm.itemId1 = itm.getItemId();
                                itm.buyItem1();
                                itm.itemCarat1 = 18;
                                itm.itemWeight1 = rGold18;
                                itm.addGoldToFonding();
                            }
                            if (rGold21 != 0)
                            {
                                itm.itemCount1 = 0;
                                itm.itemFees1 = 0;
                                itm.itemCarat1 = 21;
                                itm.itemWeight1 = rGold21;
                                itm.itemName1 = "خشر عيار 21";
                                itm.itemId1 = itm.getItemId();
                                itm.buyItem1();
                                itm.itemCarat1 = 21;
                                itm.itemWeight1 = rGold21;
                                itm.addGoldToFonding();
                            }
                        }
                        else
                        {
                            itm.billType1 = "بيع";
                            pay.paymentTypeInt1 = 1;
                            pay.paymentTypeString1 = payType.Text;
                            itm.cash1 = rCash;
                            itm.subCashFromFonding();
                            if (rGold14 != 0)
                            {
                                itm.itemCount1 = 0;
                                itm.itemFees1 = 0;
                                itm.itemCarat1 = 14;
                                itm.itemWeight1 = rGold14;
                                itm.itemName1 = "خشر عيار 14";
                                itm.itemId1 = itm.getItemId();
                                itm.sellItem();
                                itm.itemCarat1 = 14;
                                itm.itemWeight1 = rGold14;
                                itm.subGoldFromFonding();
                            }
                            if (rGold18 != 0)
                            {
                                itm.itemCount1 = 0;
                                itm.itemFees1 = 0;
                                itm.itemCarat1 = 18;
                                itm.itemWeight1 = rGold18;
                                itm.itemName1 = "خشر عيار 18";
                                itm.itemId1 = itm.getItemId();
                                itm.sellItem();
                                itm.itemCarat1 = 18;
                                itm.itemWeight1 = rGold18;
                                itm.subGoldFromFonding();
                            }
                            if (rGold21 != 0)
                            {
                                itm.itemCount1 = 0;
                                itm.itemFees1 = 0;
                                itm.itemCarat1 = 21;
                                itm.itemWeight1 = rGold21;
                                itm.itemName1 = "خشر عيار 21";
                                itm.itemId1 = itm.getItemId();
                                itm.sellItem();
                                itm.itemCarat1 = 21;
                                itm.itemWeight1 = rGold21;
                                itm.subGoldFromFonding();
                            }
                        }
                        pay.paymentCash1 = cash;
                        pay.payment211 = gold21;
                        pay.payment181 = gold18;
                        pay.payment141 = gold14;
                        string notice = "";
                        if (rGold21 != 0)
                        {
                            pay.itemName1 = "خشر عيار 21";
                            pay.item21Id1 = pay.getItemId();
                        }
                        if (rGold18 != 0)
                        {
                            pay.itemName1 = "خشر عيار 18";
                            pay.item18Id1 = pay.getItemId();
                        }
                        if (rGold14 != 0)
                        {
                            pay.itemName1 = "خشر عيار 14";
                            pay.item14Id1 = pay.getItemId();
                        }
                        if (dataGridView7.Rows[0].Cells[0].Value.ToString() == "راس مال")
                        {
                            pay.paymentNotice1 = "زيادة راس مال (اجور)";
                        }
                        else
                        {
                            if (payType.Text.Trim() == "استلام")
                                notice = ("دفعة " + payType.Text + " من " + dataGridView7.Rows[0].Cells[0].Value.ToString());
                            else
                                notice = ("دفعة " + payType.Text + " الى " + dataGridView7.Rows[0].Cells[0].Value.ToString());
                            pay.paymentNotice1 = notice;
                        }
                        //////////////////////////////////////////////////
                        pay.paymentDateTime1 = dateTimePicker1.Value.Date;
                        pay.realPaymentCash1 = rCash;
                        pay.realPayment211 = rGold21;
                        pay.realPayment181 = rGold18;
                        pay.realPayment141 = rGold14;
                        pay.paymentNo1 = no.Text;
                        string c1 = pay.insertPayment();
                        if (c1 == " تم الحفظ ")
                        {
                            if (payType.Text == "استلام")
                            {
                                pay.customerName1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
                                pay.customerId1 = pay.GetCustomerid();
                                pay.paymentCash1 = cash;
                                pay.payment211 = gold21;
                                pay.payment181 = gold18;
                                pay.payment141 = gold14;
                                pay.paymentTypeInt1 = -1;
                            }
                            else
                            {
                                pay.customerName1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
                                pay.customerId1 = pay.GetCustomerid();
                                pay.paymentCash1 = cash;
                                pay.payment211 = gold21;
                                pay.payment181 = gold18;
                                pay.payment141 = gold14;
                                pay.paymentTypeInt1 = 1;
                            }
                            pay.addPayment1();
                            if (dataGridView3.RowCount > 1)
                            {
                                for (int i = 0; i < dataGridView3.RowCount - 1; i++)
                                {
                                    if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام اجور" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب21" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب18" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب14" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام اجور إلى ذهب21" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام اجور إلى ذهب18" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام اجور إلى ذهب14" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب21 إلى اجور" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب21 إلى ذهب18" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب21 إلى ذهب14" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب18 إلى اجور" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب18 إلى ذهب21" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب18 إلى ذهب14" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب14 إلى اجور" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب14 إلى ذهب21" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب14 إلى ذهب18")
                                    {
                                        pay.paymentId1 = Convert.ToInt32(pay.getPaymentId());
                                        pay.before1 = Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value);
                                        pay.after1 = Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value);
                                        pay.convert1 = dataGridView3.Rows[i].Cells[1].Value.ToString();
                                        pay.insertPaymentDetails();
                                    }
                                }
                            }
                            else
                            {
                                if (cash != 0)
                                {
                                    pay.paymentId1 = Convert.ToInt32(pay.getPaymentId());
                                    pay.before1 = cash;
                                    pay.after1 = cash;
                                    pay.convert1 = "استلام اجور";
                                    pay.insertPaymentDetails();
                                }
                                if (gold21 != 0)
                                {
                                    pay.paymentId1 = Convert.ToInt32(pay.getPaymentId());
                                    pay.before1 = gold21;
                                    pay.after1 = gold21;
                                    pay.convert1 = "استلام ذهب21";
                                    pay.insertPaymentDetails();
                                }
                                if (gold18 != 0)
                                {
                                    pay.paymentId1 = Convert.ToInt32(pay.getPaymentId());
                                    pay.before1 = gold18;
                                    pay.after1 = gold18;
                                    pay.convert1 = "استلام ذهب18";
                                    pay.insertPaymentDetails();
                                }
                                if (gold14 != 0)
                                {
                                    pay.paymentId1 = Convert.ToInt32(pay.getPaymentId());
                                    pay.before1 = gold14;
                                    pay.after1 = gold14;
                                    pay.convert1 = "استلام ذهب14";
                                    pay.insertPaymentDetails();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(c1);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("اسم الصائغ الذي ادخلته غير موجود , يرجى اختيار اسم صائغ تم اضافته إلى البرنامج");
                }
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (main.IsConnected())
            {
                try
                {
                    DialogResult result = MessageBox.Show("هل أنت متأكد بحفظ الفاتورة", "حفظ فاتورة", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);
                    if (result == DialogResult.No)
                    {
                    }
                    else
                    {
                        if (isOk())
                        {
                            if (dataGridView3.RowCount > 1 || (payCash.Text.Trim() != "" && Convert.ToDecimal(payCash.Text) != 0) || (payGold21.Text.Trim() != "" && Convert.ToDecimal(payGold21.Text) != 0) || (payGold18.Text.Trim() != "" && Convert.ToDecimal(payGold18.Text) != 0) || (payGold14.Text.Trim() != "" && Convert.ToDecimal(payGold14.Text) != 0))
                            {
                                try
                                {
                                    save_payment();
                                    MessageBox.Show("تم اضافة الدفعة في حساب الصائغ");
                                }
                                catch (Exception ex)
                                { MessageBox.Show(ex.Message); }
                            }
                            if (dataGridView1.RowCount > 1)
                            {
                                if (checkBox1.Checked)
                                {
                                    save_Click11();
                                    MessageBox.Show("تم حفظ الفاتورة");
                                    for (int i = 0; i < Convert.ToDecimal(printNo.Text); i++)
                                    {
                                        printBill_Click(sender, e);
                                    }
                                    dataGridView7.Rows[0].Cells[0].Value = "أدخل اسم الصائغ";
                                    cashTxtbox.Text = "0";
                                    totalCashTxtbox.Text = "0";
                                    discountTxtbox.Text = "0";
                                    gold14Txtbox.Text = "0";
                                    gold18Txtbox.Text = "0";
                                    gold21Txtbox.Text = "0";
                                    payCash.Text = "0";
                                    payGold21.Text = "0";
                                    payGold18.Text = "0";
                                    payGold14.Text = "0";
                                    real21.Text = "0";
                                    real18.Text = "0";
                                    real14.Text = "0";
                                    realCash.Text = "0";
                                    cashAfter.Text = "0";
                                    gold14After.Text = "0";
                                    gold18After.Text = "0";
                                    gold21After.Text = "0";
                                    caratTxtbox.Text = "0";
                                    no.Text = "000";
                                    detailsTxtbox.Text = "اكتب ملاحظات عن الفاتورة";
                                    dateTimePicker1.Value = DateTime.Now.Date;
                                    dataGridView7.Rows[0].Cells[1].Value = "0";
                                    dataGridView7.Rows[0].Cells[2].Value = "0";
                                    dataGridView7.Rows[0].Cells[3].Value = "0";
                                    dataGridView7.Rows[0].Cells[4].Value = "0";

                                    //dataGridView1.DataSource = null;
                                    dataT1.Clear();
                                    dataT3.Clear();

                                    //dataT.Rows.Add("لا تستخدم هذا السطر",0, 0, 0, 0);


                                    dataGridView1.Columns[0].Width = 300;
                                    //dataGridView1.Rows[0].Height = 0;
                                    dataGridView7.Select();
                                    dataGridView7.Focus();
                                }
                                else
                                {
                                    save_Click11();
                                    MessageBox.Show("تم حفظ الفاتورة");
                                    dataGridView7.Rows[0].Cells[0].Value = "أدخل اسم الصائغ";
                                    cashTxtbox.Text = "0";
                                    totalCashTxtbox.Text = "0";
                                    discountTxtbox.Text = "0";
                                    gold14Txtbox.Text = "0";
                                    gold18Txtbox.Text = "0";
                                    gold21Txtbox.Text = "0";
                                    payCash.Text = "0";
                                    payGold21.Text = "0";
                                    payGold18.Text = "0";
                                    payGold14.Text = "0";
                                    real21.Text = "0";
                                    real18.Text = "0";
                                    real14.Text = "0";
                                    realCash.Text = "0";
                                    cashAfter.Text = "0";
                                    gold14After.Text = "0";
                                    gold18After.Text = "0";
                                    gold21After.Text = "0";
                                    caratTxtbox.Text = "0";
                                    no.Text = "000";
                                    detailsTxtbox.Text = "اكتب ملاحظات عن الفاتورة";
                                    dateTimePicker1.Value = DateTime.Now.Date;
                                    dataGridView7.Rows[0].Cells[1].Value = "0";
                                    dataGridView7.Rows[0].Cells[2].Value = "0";
                                    dataGridView7.Rows[0].Cells[3].Value = "0";
                                    dataGridView7.Rows[0].Cells[4].Value = "0";
                                    //dataGridView1.DataSource = null;
                                    dataT1.Clear();
                                    dataT3.Clear();

                                    //dataT.Rows.Add("لا تستخدم هذا السطر",0, 0, 0, 0);


                                    dataGridView1.Columns[0].Width = 300;
                                    //dataGridView1.Rows[0].Height = 0;
                                    dataGridView7.Select();
                                    dataGridView7.Focus();
                                }
                            }
                            this.Update();
                            dataGridView7.Rows[0].Cells[0].Value = "أدخل اسم الصائغ";
                            cashTxtbox.Text = "0";
                            totalCashTxtbox.Text = "0";
                            discountTxtbox.Text = "0";
                            gold14Txtbox.Text = "0";
                            gold18Txtbox.Text = "0";
                            gold21Txtbox.Text = "0";
                            payCash.Text = "0";
                            payGold21.Text = "0";
                            payGold18.Text = "0";
                            payGold14.Text = "0";
                            real21.Text = "0";
                            real18.Text = "0";
                            real14.Text = "0";
                            realCash.Text = "0";
                            cashAfter.Text = "0";
                            gold14After.Text = "0";
                            gold18After.Text = "0";
                            gold21After.Text = "0";
                            caratTxtbox.Text = "0";
                            no.Text = "000";
                            detailsTxtbox.Text = "اكتب ملاحظات عن الفاتورة";
                            dateTimePicker1.Value = DateTime.Now.Date;
                            dataGridView7.Rows[0].Cells[1].Value = "0";
                            dataGridView7.Rows[0].Cells[2].Value = "0";
                            dataGridView7.Rows[0].Cells[3].Value = "0";
                            dataGridView7.Rows[0].Cells[4].Value = "0";
                            dataT1.Clear();
                            dataT3.Clear();
                            dataGridView1.Columns[0].Width = 300;
                            dataGridView3.Columns[1].Width = 150;
                            dataGridView7.Select();
                            dataGridView7.Focus();
                            this.Update();
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        public bool isOk()
        {
            bool ok = true;
            string chk = "";
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                itm.itemName1 = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                chk = itm.checkitemExist();
                if (chk == "true")
                    ok = true;
                else if (chk == "false")
                {
                    ok = false;
                    dataGridView1.Rows[i].Cells[0].Selected = true;
                    MessageBox.Show("تحقق من المصاغ  " + dataGridView1.Rows[i].Cells[0].Value.ToString() + "  غير موجود في شجرة المصوغات");
                    break;
                }
                else
                    MessageBox.Show(chk);
            }
            return ok;
        }

        public void addNewBill()
        {
            if (main.IsConnected())
            {
                itm.customerName1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
                itm.customerId1 = itm.GetCustomerid();
                itm.userName1 = GlobalVar.userName;
                itm.userId1 = itm.getUserId();
                itm.billType1 = billTypeLabel.Text;
                itm.billTotalCash1 = Convert.ToDecimal(cashTxtbox.Text);
                itm.billTotal211 = Convert.ToDecimal(gold21Txtbox.Text);
                itm.billTotal181 = Convert.ToDecimal(gold18Txtbox.Text);
                itm.billTotal141 = Convert.ToDecimal(gold14Txtbox.Text);
                itm.dateT = dateTimePicker1.Value.Date;
                itm.discountAmount1 = Convert.ToInt32(discountTxtbox.Text);
                if (detailsTxtbox.Text == "اكتب ملاحظات عن الفاتورة" || detailsTxtbox.Text.Trim() == "")
                {
                    if (billTypeLabel.Text == "بيع")
                        itm.billNotice1 = "فاتورة بيع الى " + dataGridView7.Rows[0].Cells[0].Value.ToString();
                    else
                        itm.billNotice1 = "فاتورة شراء من " + dataGridView7.Rows[0].Cells[0].Value.ToString();
                }
                else
                {
                    itm.billNotice1 = detailsTxtbox.Text;
                }
                if (no.Text.Trim() != "")
                {
                    itm.billNo = no.Text.Trim();
                }
                else
                {
                    itm.billNo1 = "000";
                }
                if (label44.Text == "+")
                    itm.discountType1 = "+1";
                else if (label44.Text == "-")
                    itm.discountType1 = "-1";
                ////////////
                string a1 = itm.insertBill();
                if (a1 == " تم الحفظ ")
                {
                    itm.billId1 = Convert.ToInt32(itm.getBillId());
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        //DataGridViewRow selectedRow = dataGridView1.Rows[i];
                        s = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim();
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[0].Value)))
                        {
                            itm.itemName1 = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                            itm.itemId1 = itm.getItemId();
                            itm.itemTotalFees1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString());
                            itm.itemFees1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value.ToString());
                            itm.itemCount1 = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());
                            itm.itemWeight1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString());
                            itm.itemCarat1 = Convert.ToInt32(s.Substring(s.Length - 2).Trim());
                            itm.billType1 = billTypeLabel.Text;
                            itm.insertBillDetails();
                            if (billTypeLabel.Text.Trim() == "بيع")
                            {
                                itm.itemName1 = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                                itm.itemId1 = itm.getItemId();
                                itm.itemCount1 = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());
                                itm.itemWeight1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString());
                                itm.sellItem();
                            }
                            else if (billTypeLabel.Text.Trim() == "شراء")
                            {
                                itm.itemName1 = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                                itm.itemId1 = itm.getItemId();
                                itm.itemFees1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value.ToString());
                                itm.itemCount1 = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());
                                itm.itemWeight1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString());
                                itm.buyItem();
                            }
                        }
                    }
                    for (int i = 0; i < dataGridView3.RowCount - 1; i++)
                    {
                        if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة اجور إلى ذهب21" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة اجور إلى ذهب18" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة اجور إلى ذهب14" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب21 إلى اجور" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب21 إلى ذهب18" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب21 إلى ذهب14" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب18 إلى اجور" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب18 إلى ذهب21" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب18 إلى ذهب14" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب14 إلى اجور" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب14 إلى ذهب21" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب14 إلى ذهب18")
                        {
                            pay.paymentId1 = Convert.ToInt32(itm.getBillId());
                            pay.before1 = Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value);
                            pay.after1 = Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value);
                            pay.convert1 = dataGridView3.Rows[i].Cells[1].Value.ToString();
                            pay.insertBillConvertDetails();
                        }
                    }
                    itm.cash1 = Convert.ToDecimal(cashTxtbox.Text);
                    name = dataGridView7.Rows[0].Cells[0].Value.ToString();
                    if (billTypeLabel.Text.Trim() == "بيع")
                    {
                        pay.customerName1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
                        pay.customerId1 = itm.GetCustomerid();
                        pay.paymentTypeInt1 = 1;
                        pay.paymentCash1 = Convert.ToDecimal(cashTxtbox.Text);
                        pay.payment211 = Convert.ToDecimal(gold21Txtbox.Text);
                        pay.payment181 = Convert.ToDecimal(gold18Txtbox.Text);
                        pay.payment141 = Convert.ToDecimal(gold14Txtbox.Text);
                        pay.addPayment1();
                        gold21 = Convert.ToDecimal(gold21Txtbox.Text);
                        gold18 = Convert.ToDecimal(gold18Txtbox.Text);
                        gold14 = Convert.ToDecimal(gold14Txtbox.Text);
                        if (gold14 != 0)
                        {
                            itm.itemCarat1 = 14;
                            itm.itemWeight1 = gold14;
                            itm.subGoldFromFonding();
                        }
                        if (gold18 != 0)
                        {
                            itm.itemCarat1 = 18;
                            itm.itemWeight1 = gold18;
                            itm.subGoldFromFonding();
                        }
                        if (gold21 != 0)
                        {
                            itm.itemCarat1 = 21;
                            itm.itemWeight1 = gold21;
                            itm.subGoldFromFonding();
                        }
                        if (Convert.ToInt32(discountTxtbox.Text) > 0)
                        {
                            itm.billType1 = billTypeLabel.Text;
                            itm.customerName1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
                            itm.customerId1 = itm.GetCustomerid();
                            itm.userName1 = GlobalVar.userName;
                            itm.userId1 = itm.getUserId();
                            itm.dateT1 = dateTimePicker1.Value.Date;
                            itm.discountAmount = Convert.ToInt32(discountTxtbox.Text);
                            itm.billId1 = Convert.ToInt32(itm.getBillId());
                            if (label44.Text == "+")
                                itm.discountType1 = "+1";
                            else if (label44.Text == "-")
                                itm.discountType1 = "-1";
                            itm.insertDiscount();
                        }
                    }
                    else if (billTypeLabel.Text.Trim() == "شراء")
                    {
                        pay.customerName1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
                        pay.customerId1 = itm.GetCustomerid();
                        pay.paymentTypeInt1 = -1;
                        pay.paymentCash1 = Convert.ToDecimal(cashTxtbox.Text);
                        pay.payment211 = Convert.ToDecimal(gold21Txtbox.Text);
                        pay.payment181 = Convert.ToDecimal(gold18Txtbox.Text);
                        pay.payment141 = Convert.ToDecimal(gold14Txtbox.Text);
                        pay.addPayment1();
                        decimal gold21 = Convert.ToDecimal(gold21Txtbox.Text);
                        decimal gold18 = Convert.ToDecimal(gold18Txtbox.Text);
                        decimal gold14 = Convert.ToDecimal(gold14Txtbox.Text);
                        if (gold14 != 0)
                        {
                            itm.itemCarat1 = 14;
                            itm.itemWeight1 = gold14;
                            itm.addGoldToFonding();
                        }
                        if (gold18 != 0)
                        {
                            itm.itemCarat1 = 18;
                            itm.itemWeight1 = gold18;
                            itm.addGoldToFonding();
                        }
                        if (gold21 != 0)
                        {
                            itm.itemCarat1 = 21;
                            itm.itemWeight1 = gold21;
                            itm.addGoldToFonding();
                        }
                        if (Convert.ToInt32(discountTxtbox.Text) > 0)
                        {
                            itm.billType1 = billTypeLabel.Text;
                            itm.customerName1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
                            itm.customerId1 = itm.GetCustomerid();
                            itm.userName1 = GlobalVar.userName;
                            itm.userId1 = itm.getUserId();
                            itm.dateT1 = dateTimePicker1.Value.Date;
                            itm.discountAmount = Convert.ToInt32(discountTxtbox.Text);
                            if (label44.Text == "+")
                                itm.discountType1 = "+1";
                            else if (label44.Text == "-")
                                itm.discountType1 = "-1";
                            itm.billId1 = Convert.ToInt32(itm.getBillId());
                            itm.insertDiscount();
                        }

                    }
                    //////////////////
                }
                else
                {
                    MessageBox.Show(a1);
                }
            }
        }

        private void save_Click11()
        {
            if (main.IsConnected())
            {
                if (dataGridView7.Rows[0].Cells[0].Value.ToString() == "أدخل اسم الصائغ" || dataGridView7.Rows[0].Cells[0].Value.ToString() == "")
                {
                    MessageBox.Show("يرجى كتابة اسم الصائغ");
                }
                else if (!check())
                {

                    cus.name1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
                    if (cus.checkCustomerExist() == "true")
                    {
                        addNewBill();
                    }
                    else
                    {
                        MessageBox.Show("اسم الصائغ الذي ادخلته غير موجود في البرنامج");
                    }
                }
            }
        }

        private void availableName1()
        {
            try
            {
                if (dataGridView7.Rows[0].Cells[0].Value.ToString().Length > 1)
                {
                    SqlConnection con = new SqlConnection(GlobalVar.dataBaseLocation);
                    pay.customerName1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
                    int custmId = pay.GetCustomerid();
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT TotalCash , Total21 , Total18 , Total14 , customerMobile from customers where customerId = '" + Convert.ToString(custmId) + "' ";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView7.Rows[0].Cells[1].Value = dt.Rows[0][1].ToString();
                        dataGridView7.Rows[0].Cells[2].Value = dt.Rows[0][2].ToString();
                        dataGridView7.Rows[0].Cells[3].Value = dt.Rows[0][3].ToString();
                        dataGridView7.Rows[0].Cells[4].Value = dt.Rows[0][0].ToString();
                    }
                    else { MessageBox.Show("الاسم الذي ادخلته غير موجود"); }
                }
            }
            finally { }
        }

        private void deleteBill_Click(object sender, EventArgs e)
        {
            if (main.IsConnected())
            {
                try
                {
                    DialogResult result = MessageBox.Show("هل أنت متأكد بحذف الفاتورة رقم" + IDTxtBox.Text, "حذف فاتورة", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                    }
                    else
                    {
                        delete_bill();
                        dataGridView7.Select();
                        MessageBox.Show("تم حذف الفاتورة");
                        dataGridView7.Rows[0].Cells[0].Value = "أدخل اسم الصائغ";
                        cashTxtbox.Text = "0";
                        totalCashTxtbox.Text = "0";
                        discountTxtbox.Text = "0";
                        gold14Txtbox.Text = "0";
                        gold18Txtbox.Text = "0";
                        gold21Txtbox.Text = "0";
                        payCash.Text = "0";
                        payGold21.Text = "0";
                        payGold18.Text = "0";
                        payGold14.Text = "0";
                        real21.Text = "0";
                        real18.Text = "0";
                        real14.Text = "0";
                        realCash.Text = "0";
                        cashAfter.Text = "0";
                        gold14After.Text = "0";
                        gold18After.Text = "0";
                        gold21After.Text = "0";
                        caratTxtbox.Text = "0";
                        no.Text = "000";
                        detailsTxtbox.Text = "اكتب ملاحظات عن الفاتورة";
                        dateTimePicker1.Value = DateTime.Now.Date;
                        dataGridView7.Rows[0].Cells[1].Value = "0";
                        dataGridView7.Rows[0].Cells[2].Value = "0";
                        dataGridView7.Rows[0].Cells[3].Value = "0";
                        dataGridView7.Rows[0].Cells[4].Value = "0";
                        //dataGridView1.DataSource = null;
                        dataT1.Clear();
                        dataT3.Clear();

                        //dataT.Rows.Add("لا تستخدم هذا السطر",0, 0, 0, 0);


                        dataGridView1.Columns[0].Width = 300;
                        //dataGridView1.Rows[0].Height = 0;
                        dataGridView7.Select();
                        dataGridView7.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void IDTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (main.IsConnected())
            {
                try
                {
                    if (IDTxtBox.Text.Trim() != "")
                    {
                        getBillDetails();
                    }
                    else { }
                }
                catch
                { }
            }
        }

        public void getBillDetails()
        {
            if (main.IsConnected())
            {
                try
                {
                    totalCashTxtbox.Text = "0";
                    totalCashTxtbox1.Text = "0";
                    SqlConnection con = new SqlConnection(GlobalVar.dataBaseLocation);
                    SqlCommand cmd1 = con.CreateCommand();
                    con.Open();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT   items.itemName , billDetails.itemWeight , billDetails.itemCount , billDetails.itemFees ,billDetails.itemTotalFees from billDetails inner join items on billDetails.itemId=items.itemId  where billId = @Id";
                    cmd1.Parameters.Add(new SqlParameter("@Id", Convert.ToInt32(IDTxtBox.Text)));
                    DataTable dt1 = new DataTable();
                    try
                    {
                        cmd1.ExecuteNonQuery();
                    }
                    catch { }
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    da1.Fill(dt1);
                    dataGridView1.DataSource = dt1;
                    dataGridView2.DataSource = dt1;
                    con.Close();
                    dataGridView1.Columns[4].HeaderText = "القيمة الاجمالية";
                    dataGridView1.Columns[3].HeaderText = "اجور التصنيع";
                    dataGridView1.Columns[2].HeaderText = "العدد";
                    dataGridView1.Columns[1].HeaderText = "الوزن";
                    dataGridView1.Columns[0].HeaderText = "المصاغ";
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                    dataGridView1.EnableHeadersVisualStyles = false;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    SqlCommand cmd = con.CreateCommand();
                    SqlCommand cmd11 = con.CreateCommand();
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT billTotalCash , billTotal21 , billTotal18 , billTotal14 , customers.customerName , billType , discountAmount,billDateTime,billDetail,discountType,billNo from bills inner join customers on bills.customerId=customers.customerId where billId = @Id";
                    cmd.Parameters.Add(new SqlParameter("@Id", Convert.ToInt32(IDTxtBox.Text)));
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            if (dt.Rows[0][8].ToString().Trim() != null)
                            { detailsTxtbox.Text = dt.Rows[0][8].ToString(); }
                            dtTxtBox.Text = dt.Rows[0][7].ToString();
                            dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0][7].ToString()).Date;
                            gold21Txtbox.Text = dt.Rows[0][1].ToString();
                            gold18Txtbox.Text = dt.Rows[0][2].ToString();
                            gold14Txtbox.Text = dt.Rows[0][3].ToString();
                            gold21Txtbox1.Text = dt.Rows[0][1].ToString();
                            gold18Txtbox1.Text = dt.Rows[0][2].ToString();
                            gold14Txtbox1.Text = dt.Rows[0][3].ToString();
                            if (dt.Rows[0][9].ToString() == "+1")
                                label44.Text = "+";
                            else if (dt.Rows[0][9].ToString() == "-1")
                                label44.Text = "-";
                            dataGridView7.Rows[0].Cells[0].Value = dt.Rows[0][4].ToString();
                            customerNameTxtbox1.Text = dt.Rows[0][4].ToString();
                            billTypeLabel.Text = dt.Rows[0][5].ToString();
                            no.Text = dt.Rows[0][10].ToString();
                            billTypeLabel1.Text = dt.Rows[0][5].ToString();
                            discountTxtbox.Text = dt.Rows[0][6].ToString();
                            discountTxtbox1.Text = dt.Rows[0][6].ToString();
                            cashTxtbox.Text = dt.Rows[0][0].ToString();
                            cashTxtbox1.Text = dt.Rows[0][0].ToString();
                            if (label44.Text == "+1")
                            {
                                label44.Text = "+";
                                totalCashTxtbox.Text = (Convert.ToDecimal(dt.Rows[0][0].ToString()) - Convert.ToDecimal(dt.Rows[0][6].ToString())).ToString();
                            }
                            else if (label44.Text == "-1")
                            {
                                label44.Text = "-";
                                totalCashTxtbox.Text = (Convert.ToDecimal(dt.Rows[0][0].ToString()) + Convert.ToDecimal(dt.Rows[0][6].ToString())).ToString();
                            }
                            discountTxtbox.Text = dt.Rows[0][6].ToString();
                            discountTxtbox1.Text = dt.Rows[0][6].ToString();
                            cashTxtbox.Text = dt.Rows[0][0].ToString();
                            cashTxtbox1.Text = dt.Rows[0][0].ToString();
                            con.Open();
                            cmd11.CommandType = CommandType.Text;
                            cmd11.CommandText = "SELECT beforeAmount , convertRole , afterAmount  from billConvertDetails where billId = @Id";
                            cmd11.Parameters.Add(new SqlParameter("@Id", Convert.ToInt32(IDTxtBox.Text)));
                            try
                            {
                                cmd11.ExecuteNonQuery();
                            }
                            catch { }
                            dataT4.Clear();
                            SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
                            da11.Fill(dataT4);
                            if (dataT4.Rows.Count > 0)
                            {
                                dataT3 = dataT4;
                                dataGridView4.Columns[0].HeaderText = "قبل التحويل";
                                dataGridView4.Columns[1].HeaderText = "بيان التحويل";
                                dataGridView4.Columns[2].HeaderText = "بعد التحويل";
                            }
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }
                }
                catch { }
                cashAfter.Text = "0";
                gold21After.Text = "0";
                gold18After.Text = "0";
                gold14After.Text = "0";
            }
        }

        private void payCash_OnValueChanged(object sender, EventArgs e)
        {
            if (payCash.Text.Trim().TrimStart() != "" && payCash.Text.Trim().TrimStart() != " ")
            {
                try
                {
                    decimal i = Convert.ToDecimal(payCash.Text.Trim());
                    if (payType.Text == "استلام" && billTypeLabel.Text == "بيع")
                        cashAfter.Text = (Convert.ToDecimal(dataGridView7.Rows[0].Cells[4].Value.ToString()) + Convert.ToDecimal(cashTxtbox.Text) - i).ToString();
                    else if (payType.Text == "تسليم" && billTypeLabel.Text == "بيع")
                        cashAfter.Text = (i + Convert.ToDecimal(dataGridView7.Rows[0].Cells[4].Value.ToString()) + Convert.ToDecimal(cashTxtbox.Text)).ToString();
                    else if (payType.Text == "استلام" && billTypeLabel.Text == "شراء")
                        cashAfter.Text = (Convert.ToDecimal(dataGridView7.Rows[0].Cells[4].Value.ToString()) - Convert.ToDecimal(cashTxtbox.Text) - i).ToString();
                    else if (payType.Text == "تسليم" && billTypeLabel.Text == "شراء")
                        cashAfter.Text = (i + Convert.ToDecimal(dataGridView7.Rows[0].Cells[4].Value.ToString()) - Convert.ToDecimal(cashTxtbox.Text)).ToString();
                }
                catch
                {
                    //payCash.Text = "0";
                }
            }
        }

        private void payGold21_OnValueChanged(object sender, EventArgs e)
        {
            if (payGold21.Text.Trim().TrimStart() != "" && payGold21.Text.Trim().TrimStart() != " ")
            {
                try
                {
                    decimal i = Convert.ToDecimal(payGold21.Text.Trim());
                    if (payType.Text == "استلام" && billTypeLabel.Text == "بيع")
                        gold21After.Text = (Convert.ToDecimal(dataGridView7.Rows[0].Cells[1].Value.ToString()) + Convert.ToDecimal(gold21Txtbox.Text) - i).ToString();
                    else if (payType.Text == "تسليم" && billTypeLabel.Text == "بيع")
                        gold21After.Text = (i + Convert.ToDecimal(dataGridView7.Rows[0].Cells[1].Value.ToString()) + Convert.ToDecimal(gold21Txtbox.Text)).ToString();
                    else if (payType.Text == "استلام" && billTypeLabel.Text == "شراء")
                        gold21After.Text = (Convert.ToDecimal(dataGridView7.Rows[0].Cells[1].Value.ToString()) - Convert.ToDecimal(gold21Txtbox.Text) - i).ToString();
                    else if (payType.Text == "تسليم" && billTypeLabel.Text == "شراء")
                        gold21After.Text = (i + Convert.ToDecimal(dataGridView7.Rows[0].Cells[1].Value.ToString()) - Convert.ToDecimal(gold21Txtbox.Text)).ToString();
                }
                catch (Exception m)

                {
                    MessageBox.Show(m.Message);
                }
            }
        }

        private void payGold18_OnValueChanged(object sender, EventArgs e)
        {
            if (payGold18.Text.Trim().TrimStart() != "" && payGold18.Text.Trim().TrimStart() != " ")
            {
                try
                {
                    decimal i = Convert.ToDecimal(payGold18.Text.Trim());
                    if (payType.Text == "استلام" && billTypeLabel.Text == "بيع")
                        gold18After.Text = (Convert.ToDecimal(dataGridView7.Rows[0].Cells[2].Value.ToString()) + Convert.ToDecimal(gold18Txtbox.Text) - i).ToString();
                    else if (payType.Text == "تسليم" && billTypeLabel.Text == "بيع")
                        gold18After.Text = (i + Convert.ToDecimal(dataGridView7.Rows[0].Cells[2].Value.ToString()) + Convert.ToDecimal(gold18Txtbox.Text)).ToString();
                    else if (payType.Text == "استلام" && billTypeLabel.Text == "شراء")
                        gold18After.Text = (Convert.ToDecimal(dataGridView7.Rows[0].Cells[2].Value.ToString()) - Convert.ToDecimal(gold18Txtbox.Text) - i).ToString();
                    else if (payType.Text == "تسليم" && billTypeLabel.Text == "شراء")
                        gold18After.Text = (i + Convert.ToDecimal(dataGridView7.Rows[0].Cells[2].Value.ToString()) - Convert.ToDecimal(gold18Txtbox.Text)).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void payGold14_OnValueChanged(object sender, EventArgs e)
        {
            if (payGold14.Text.Trim().TrimStart() != "" && payGold14.Text.Trim().TrimStart() != " ")
            {
                try
                {
                    decimal i = Convert.ToDecimal(payGold14.Text.Trim());
                    if (payType.Text == "استلام" && billTypeLabel.Text == "بيع")
                        gold14After.Text = (Convert.ToDecimal(dataGridView7.Rows[0].Cells[3].Value.ToString()) + Convert.ToDecimal(gold14Txtbox.Text) - i).ToString();
                    else if (payType.Text == "تسليم" && billTypeLabel.Text == "بيع")
                        gold14After.Text = (i + Convert.ToDecimal(dataGridView7.Rows[0].Cells[3].Value.ToString()) + Convert.ToDecimal(gold14Txtbox.Text)).ToString();
                    else if (payType.Text == "استلام" && billTypeLabel.Text == "شراء")
                        gold14After.Text = (Convert.ToDecimal(dataGridView7.Rows[0].Cells[3].Value.ToString()) - Convert.ToDecimal(gold14Txtbox.Text) - i).ToString();
                    else if (payType.Text == "تسليم" && billTypeLabel.Text == "شراء")
                        gold14After.Text = (i + Convert.ToDecimal(dataGridView7.Rows[0].Cells[3].Value.ToString()) - Convert.ToDecimal(gold14Txtbox.Text)).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void cashTxtbox_OnValueChanged(object sender, EventArgs e)
        {
            payCash_OnValueChanged(sender, e);
            payGold21_OnValueChanged(sender, e);
            payGold18_OnValueChanged(sender, e);
            payGold14_OnValueChanged(sender, e);
        }

        private void gold21Txtbox_OnValueChanged(object sender, EventArgs e)
        {
            payCash_OnValueChanged(sender, e);
            payGold21_OnValueChanged(sender, e);
            payGold18_OnValueChanged(sender, e);
            payGold14_OnValueChanged(sender, e);
        }

        private void gold18Txtbox_OnValueChanged(object sender, EventArgs e)
        {
            payCash_OnValueChanged(sender, e);
            payGold21_OnValueChanged(sender, e);
            payGold18_OnValueChanged(sender, e);
            payGold14_OnValueChanged(sender, e);
        }

        private void gold14Txtbox_OnValueChanged(object sender, EventArgs e)
        {
            payCash_OnValueChanged(sender, e);
            payGold21_OnValueChanged(sender, e);
            payGold18_OnValueChanged(sender, e);
            payGold14_OnValueChanged(sender, e);
        }

        private void payType_Click(object sender, EventArgs e)
        {
            if (payType.Text == "تسليم")
            {
                payType.Text = "استلام";
                pType.Text = payType.Text;
                payType.ForeColor = Color.Gold;
                pType.ForeColor = Color.Gold;
                GlobalVar.paymentType = true;
            }
            else
            {
                payType.Text = "تسليم";
                pType.Text = payType.Text;
                payType.ForeColor = Color.Salmon;
                pType.ForeColor = Color.Salmon;
                GlobalVar.paymentType = false;
            }
            payCash_OnValueChanged(sender, e);
            payGold21_OnValueChanged(sender, e);
            payGold18_OnValueChanged(sender, e);
            payGold14_OnValueChanged(sender, e);
        }

        private void before_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        public void dataGridView3_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                TextBox tx = e.Control as TextBox;
                e.Control.KeyPress -= new KeyPressEventHandler(dataGridView1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(dataGridView11_KeyPress);
                if (dataGridView3.CurrentCell.ColumnIndex == 1)
                {
                    if (tx != null)
                    {
                        tx.AutoCompleteMode = AutoCompleteMode.Suggest;
                        tx.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        AutoCompleteStringCollection sc = new AutoCompleteStringCollection();
                        add3_items(sc);
                        tx.AutoCompleteCustomSource = sc;
                    }
                }
                else if (dataGridView3.CurrentCell.ColumnIndex == 0 || dataGridView3.CurrentCell.ColumnIndex == 2)
                {
                    e.Control.KeyPress += new KeyPressEventHandler(dataGridView11_KeyPress);
                }
            }
            catch { }
        }

        public void add3_items(AutoCompleteStringCollection col)
        {
            col.Add("استلام اجور إلى ذهب21");
            col.Add("استلام اجور إلى ذهب18");
            col.Add("استلام اجور إلى ذهب14");
            col.Add("استلام ذهب21 إلى اجور");
            col.Add("استلام ذهب21 إلى ذهب18");
            col.Add("استلام ذهب21 إلى ذهب14");
            col.Add("استلام ذهب18 إلى اجور");
            col.Add("استلام ذهب18 إلى ذهب21");
            col.Add("استلام ذهب18 إلى ذهب14");
            col.Add("استلام ذهب14 إلى اجور");
            col.Add("استلام ذهب14 إلى ذهب21");
            col.Add("استلام ذهب14 إلى ذهب18");
            col.Add("تسليم اجور إلى ذهب21");
            col.Add("تسليم اجور إلى ذهب18");
            col.Add("تسليم اجور إلى ذهب14");
            col.Add("تسليم ذهب21 إلى اجور");
            col.Add("تسليم ذهب21 إلى ذهب18");
            col.Add("تسليم ذهب21 إلى ذهب14");
            col.Add("تسليم ذهب18 إلى اجور");
            col.Add("تسليم ذهب18 إلى ذهب21");
            col.Add("تسليم ذهب18 إلى ذهب14");
            col.Add("تسليم ذهب14 إلى اجور");
            col.Add("تسليم ذهب14 إلى ذهب21");
            col.Add("تسليم ذهب14 إلى ذهب18");
            col.Add("فاتورة اجور إلى ذهب21");
            col.Add("فاتورة اجور إلى ذهب18");
            col.Add("فاتورة اجور إلى ذهب14");
            col.Add("فاتورة ذهب21 إلى اجور");
            col.Add("فاتورة ذهب21 إلى ذهب18");
            col.Add("فاتورة ذهب21 إلى ذهب14");
            col.Add("فاتورة ذهب18 إلى اجور");
            col.Add("فاتورة ذهب18 إلى ذهب21");
            col.Add("فاتورة ذهب18 إلى ذهب14");
            col.Add("فاتورة ذهب14 إلى اجور");
            col.Add("فاتورة ذهب14 إلى ذهب21");
            col.Add("فاتورة ذهب14 إلى ذهب18");

            col.Add("حول اجور صائغ إلى ذهب21 بيع");
            col.Add("حول اجور صائغ إلى ذهب18 بيع");
            col.Add("حول اجور صائغ إلى ذهب14 بيع");
            col.Add("حول ذهب21 صائغ إلى اجور بيع");
            col.Add("حول ذهب18 صائغ إلى اجور بيع");
            col.Add("حول ذهب14 صائغ إلى اجور بيع");

            col.Add("حول اجور صائغ إلى ذهب21 شراء");
            col.Add("حول اجور صائغ إلى ذهب18 شراء");
            col.Add("حول اجور صائغ إلى ذهب14 شراء");
            col.Add("حول ذهب21 صائغ إلى اجور شراء");
            col.Add("حول ذهب18 صائغ إلى اجور شراء");
            col.Add("حول ذهب14 صائغ إلى اجور شراء");

            col.Add("حول ذهب21 صائغ إلى ذهب14");
            col.Add("حول ذهب21 صائغ إلى ذهب18");
            col.Add("حول ذهب18 صائغ إلى ذهب14");
            col.Add("حول ذهب18 صائغ إلى ذهب21");
            col.Add("حول ذهب14 صائغ إلى ذهب21");
            col.Add("حول ذهب14 صائغ إلى ذهب18");            

        }

        public void add5_items(AutoCompleteStringCollection col)
        {
            col.Add("دفعة استلام");
            col.Add("دفعة استلام مرتجع");
            col.Add("دفعة تسليم");
            col.Add("دفعة تسليم مرتجع");
        }


        public void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            decimal cashSum1 = 0;
            decimal gold14Sum1 = 0;
            decimal gold18Sum1 = 0;
            decimal gold21Sum1 = 0;
            decimal RealCash = 0;
            decimal Real21 = 0;
            decimal Real18 = 0;
            decimal Real14 = 0;
            decimal Rcash1 = Convert.ToDecimal(totalCashTxtbox.Text);
            decimal R211 = Convert.ToDecimal(gold21Txtbox.Text);
            decimal R181 = Convert.ToDecimal(gold18Txtbox.Text);
            decimal R141 = Convert.ToDecimal(gold14Txtbox.Text);

            if (dataGridView3.RowCount > 1)
            {
                for (int i = 0; i < dataGridView3.RowCount - 1; i++)
                {
                    if (dataGridView3.Rows[i].Cells[0].Value.ToString().Trim() != "" && dataGridView3.Rows[i].Cells[1].Value.ToString().Trim() != "")
                    {
                        if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام اجور إلى ذهب21")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) / Convert.ToDecimal(goldPrice.Text)), 2);
                                RealCash += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                gold21Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام اجور إلى ذهب18")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.166667)) / Convert.ToDecimal(goldPrice.Text)), 2);
                                RealCash += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                gold18Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام اجور إلى ذهب14")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.5)) / Convert.ToDecimal(goldPrice.Text)), 2);
                                RealCash += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                gold14Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب21 إلى اجور")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text)), 2)));
                                Real21 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                cashSum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب21 إلى ذهب18")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.166667)), 2);
                            Real21 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold18Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب21 إلى ذهب14")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.5)), 2);
                            Real21 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold14Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب18 إلى اجور")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text) * Convert.ToDecimal(0.85714)), 2)));
                                Real18 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                cashSum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب18 إلى ذهب21")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.85714)), 2);
                            Real18 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold21Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب18 إلى ذهب14")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.285714)), 2);
                            Real18 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold14Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب14 إلى اجور")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text) * Convert.ToDecimal(0.666667)), 2)));
                                Real14 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                cashSum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب14 إلى ذهب21")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.66667)), 2);
                            Real14 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold21Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب14 إلى ذهب18")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.77778)), 2);
                            Real14 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold18Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        //else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة اجور إلى ذهب21")
                        //{
                        //    if (goldPrice.Text.Trim() != "")
                        //    {
                        //        dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) / Convert.ToDecimal(goldPrice.Text)), 2);
                        //        Rcash -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                        //        R21 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        //        dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                        //        dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        //        dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("ادخل سعر غرام ذهب21");
                        //        goldPrice.Select();
                        //        goldPrice.Focus();
                        //    }
                        //}
                        //else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة اجور إلى ذهب18")
                        //{
                        //    if (goldPrice.Text.Trim() != "")
                        //    {
                        //        dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.166667)) / Convert.ToDecimal(goldPrice.Text)), 2);
                        //        Rcash -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                        //        R18 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        //        dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                        //        dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        //        dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("ادخل سعر غرام ذهب21");
                        //        goldPrice.Select();
                        //        goldPrice.Focus();
                        //    }
                        //}
                        //else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة اجور إلى ذهب14")
                        //{
                        //    if (goldPrice.Text.Trim() != "")
                        //    {
                        //        dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.5)) / Convert.ToDecimal(goldPrice.Text)), 2);
                        //        Rcash -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                        //        R14 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        //        dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                        //        dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        //        dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("ادخل سعر غرام ذهب21");
                        //        goldPrice.Select();
                        //        goldPrice.Focus();
                        //    }
                        //}
                        //else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب21 إلى اجور")
                        //{
                        //    if (goldPrice.Text.Trim() != "")
                        //    {
                        //        dataGridView3.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text)), 2)));
                        //        R21 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                        //        Rcash += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        //        dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                        //        dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        //        dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("ادخل سعر غرام ذهب21");
                        //        goldPrice.Select();
                        //        goldPrice.Focus();
                        //    }
                        //}
                        //else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب21 إلى ذهب18")
                        //{
                        //    dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.166667)), 2);
                        //    R21 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                        //    R18 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        //    dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                        //    dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        //    dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        //}
                        //else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب21 إلى ذهب14")
                        //{
                        //    dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.5)), 2);
                        //    R21 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                        //    R14 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        //    dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                        //    dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        //    dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        //}
                        //else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب18 إلى اجور")
                        //{
                        //    if (goldPrice.Text.Trim() != "")
                        //    {
                        //        dataGridView3.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text) * Convert.ToDecimal(0.85714)), 2)));
                        //        R18 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                        //        Rcash += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        //        dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                        //        dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        //        dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("ادخل سعر غرام ذهب21");
                        //        goldPrice.Select();
                        //        goldPrice.Focus();
                        //    }
                        //}
                        //else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب18 إلى ذهب21")
                        //{
                        //    dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.85714)), 2);
                        //    R18 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                        //    R21 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        //    dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                        //    dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        //    dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        //}
                        //else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب18 إلى ذهب14")
                        //{
                        //    dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.285714)), 2);
                        //    R18 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                        //    R14 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        //    dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                        //    dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        //    dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        //}
                        //else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب14 إلى اجور")
                        //{
                        //    if (goldPrice.Text.Trim() != "")
                        //    {
                        //        dataGridView3.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text) * Convert.ToDecimal(0.666667)), 2)));
                        //        R14 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                        //        Rcash += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        //        dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                        //        dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        //        dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("ادخل سعر غرام ذهب21");
                        //        goldPrice.Select();
                        //        goldPrice.Focus();
                        //    }
                        //}
                        //else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب14 إلى ذهب21")
                        //{
                        //    dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.66667)), 2);
                        //    R14 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                        //    R21 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        //    dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                        //    dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        //    dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        //}
                        //else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب14 إلى ذهب18")
                        //{
                        //    dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.77778)), 2);
                        //    R14 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                        //    R18 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        //    dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                        //    dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                        //    dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        //}
                    }
                }
                realCash.Text = RealCash.ToString();
                real21.Text = Real21.ToString();
                real18.Text = Real18.ToString();
                real14.Text = Real14.ToString();
                payCash.Text = cashSum1.ToString();
                payGold21.Text = gold21Sum1.ToString();
                payGold18.Text = gold18Sum1.ToString();
                payGold14.Text = gold14Sum1.ToString();
                //totalCashTxtbox.Text = (Rcash1 + Rcash).ToString();
                //gold14Txtbox.Text = (R141 + R14).ToString();
                //gold18Txtbox.Text = (R181 + R18).ToString();
                //gold21Txtbox.Text = (R211 + R21).ToString();
            }
        }

        private void calc_Click(object sender, EventArgs e)
        {
            totalsCalc();
            decimal cashSum1 = 0;
            decimal gold14Sum1 = 0;
            decimal gold18Sum1 = 0;
            decimal gold21Sum1 = 0;
            decimal RealCash = 0;
            decimal Real21 = 0;
            decimal Real18 = 0;
            decimal Real14 = 0;
            decimal Rcash = Convert.ToDecimal(totalCashTxtbox.Text);
            decimal R21 = Convert.ToDecimal(gold21Txtbox.Text);
            decimal R18 = Convert.ToDecimal(gold18Txtbox.Text);
            decimal R14 = Convert.ToDecimal(gold14Txtbox.Text);
            if (dataGridView3.RowCount > 1)
            {
                for (int i = 0; i < dataGridView3.RowCount - 1; i++)
                {
                    if (dataGridView3.Rows[i].Cells[0].Value.ToString().Trim() != "" && dataGridView3.Rows[i].Cells[1].Value.ToString().Trim() != "")
                    {
                        if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام اجور إلى ذهب21")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) / Convert.ToDecimal(goldPrice.Text)), 2);
                                RealCash += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                gold21Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام اجور إلى ذهب18")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.166667)) / Convert.ToDecimal(goldPrice.Text)), 2);
                                RealCash += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                gold18Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام اجور إلى ذهب14")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.5)) / Convert.ToDecimal(goldPrice.Text)), 2);
                                RealCash += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                gold14Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب21 إلى اجور")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text)), 2)));
                                Real21 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                cashSum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب21 إلى ذهب18")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.166667)), 2);
                            Real21 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold18Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب21 إلى ذهب14")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.5)), 2);
                            Real21 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold14Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب18 إلى اجور")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text) * Convert.ToDecimal(0.85714)), 2)));
                                Real18 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                cashSum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب18 إلى ذهب21")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.85714)), 2);
                            Real18 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold21Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب18 إلى ذهب14")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.285714)), 2);
                            Real18 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold14Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب14 إلى اجور")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text) * Convert.ToDecimal(0.666667)), 2)));
                                Real14 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                cashSum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب14 إلى ذهب21")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.66667)), 2);
                            Real14 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold21Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "استلام ذهب14 إلى ذهب18")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.77778)), 2);
                            Real14 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold18Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة اجور إلى ذهب21")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) / Convert.ToDecimal(goldPrice.Text)), 2);
                                Rcash -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                R21 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                                dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                                dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                                dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة اجور إلى ذهب18")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.166667)) / Convert.ToDecimal(goldPrice.Text)), 2);
                                Rcash -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                R18 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                                dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                                dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                                dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة اجور إلى ذهب14")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.5)) / Convert.ToDecimal(goldPrice.Text)), 2);
                                Rcash -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                R14 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                                dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                                dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                                dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب21 إلى اجور")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text)), 2)));
                                R21 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                Rcash += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                                dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                                dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                                dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب21 إلى ذهب18")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.166667)), 2);
                            R21 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            R18 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                            dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                            dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب21 إلى ذهب14")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.5)), 2);
                            R21 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            R14 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                            dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                            dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب18 إلى اجور")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text) * Convert.ToDecimal(0.85714)), 2)));
                                R18 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                Rcash += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                                dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                                dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                                dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب18 إلى ذهب21")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.85714)), 2);
                            R18 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            R21 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                            dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                            dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب18 إلى ذهب14")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.285714)), 2);
                            R18 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            R14 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                            dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                            dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب14 إلى اجور")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView3.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text) * Convert.ToDecimal(0.666667)), 2)));
                                R14 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                                Rcash += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                                dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                                dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                                dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب14 إلى ذهب21")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.66667)), 2);
                            R14 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            R21 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                            dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                            dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب14 إلى ذهب18")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.77778)), 2);
                            R14 -= Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            R18 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                            dataGridView3.Rows[i].Cells[0].Style.ForeColor = Color.Red;
                            dataGridView3.Rows[i].Cells[1].Style.ForeColor = Color.Red;
                            dataGridView3.Rows[i].Cells[2].Style.ForeColor = Color.Red;
                        }
                        //MessageBox.Show(dataGridView3.Rows[i].Cells[2].Value.ToString());
                        //MessageBox.Show(cashSum1.ToString() + " " + gold21Sum1.ToString() + " " + gold18Sum1.ToString() + " " + gold14Sum1.ToString());
                    }
                }
                //MessageBox.Show(cashSum1.ToString() + " " + gold21Sum1.ToString() + " " + gold18Sum1.ToString() + " " + gold14Sum1.ToString());
                realCash.Text = RealCash.ToString();
                real21.Text = Real21.ToString();
                real18.Text = Real18.ToString();
                real14.Text = Real14.ToString();
                payCash.Text = cashSum1.ToString();
                payGold21.Text = gold21Sum1.ToString();
                payGold18.Text = gold18Sum1.ToString();
                payGold14.Text = gold14Sum1.ToString();
                totalCashTxtbox.Text = Rcash.ToString();
                gold14Txtbox.Text = R14.ToString();
                gold18Txtbox.Text = R18.ToString();
                gold21Txtbox.Text = R21.ToString();
            }
        }

        public void delete_bill()
        {
            if (main.IsConnected())
            {
                itm.customerName1 = customerNameTxtbox1.Text;
                itm.customerId1 = itm.GetCustomerid();
                itm.userName1 = GlobalVar.userName;
                itm.userId1 = itm.getUserId();
                itm.billId1 = Convert.ToInt32(IDTxtBox.Text);
                itm.billType1 = billTypeLabel1.Text;
                itm.dateT1 = Convert.ToDateTime(dtTxtBox.Text);
                itm.billNotice1 = "حذف الفاتورة " + IDTxtBox.Text;
                itm.billNo1 = "000";
                if (label44.Text == "+")
                    itm.discountType1 = "+1";
                else if (label44.Text == "-")
                    itm.discountType1 = "-1";
                ////////////
                string a1 = itm.deleteBill();
                if (a1 == "تم الحذف")
                {
                    for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView2.Rows[i].Cells[0].Value)))
                        {
                            if (billTypeLabel1.Text == "بيع")
                            {
                                itm.itemName1 = Convert.ToString(dataGridView2.Rows[i].Cells[0].Value);
                                itm.itemId1 = itm.getItemId();
                                itm.itemCount1 = Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value.ToString());
                                itm.itemWeight1 = Convert.ToDecimal(dataGridView2.Rows[i].Cells[1].Value.ToString());
                                itm.buyItem1();
                            }
                            else if (billTypeLabel1.Text == "شراء")
                            {
                                itm.itemName1 = Convert.ToString(dataGridView2.Rows[i].Cells[0].Value);
                                itm.itemId1 = itm.getItemId();
                                itm.itemCount1 = Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value.ToString());
                                itm.itemWeight1 = Convert.ToDecimal(dataGridView2.Rows[i].Cells[1].Value.ToString());
                                itm.sellItem();
                            }
                        }
                    }
                    itm.customerName1 = customerNameTxtbox1.Text;
                    itm.customerId1 = itm.GetCustomerid();
                    itm.userName1 = GlobalVar.userName;
                    itm.userId1 = itm.getUserId();
                    itm.billId1 = Convert.ToInt32(IDTxtBox.Text);
                    itm.billType1 = billTypeLabel1.Text;
                    itm.discountAmount1 = Convert.ToInt32(discountTxtbox1.Text);
                    itm.billTotalCash1 = Convert.ToDecimal(totalCashTxtbox1.Text);
                    itm.billTotal211 = Convert.ToDecimal(gold21Txtbox1.Text);
                    itm.billTotal181 = Convert.ToDecimal(gold18Txtbox1.Text);
                    itm.billTotal141 = Convert.ToDecimal(gold14Txtbox1.Text);
                    itm.newdiscountAmount1 = 0;
                    itm.newbillTotalCash1 = 0;
                    itm.newbillTotal211 = 0;
                    itm.newbillTotal181 = 0;
                    itm.newbillTotal141 = 0;
                    itm.dateT = dateTimePicker1.Value.Date;
                    itm.editBillNotice1 = "حذف الفاتورة" + IDTxtBox.Text;
                    itm.insertEditBill();
                    itm.billId1 = Convert.ToInt32(IDTxtBox.Text);
                    itm.deleteBillDetails1();
                    itm.billId1 = Convert.ToInt32(IDTxtBox.Text);
                    itm.deleteBillConvertDetails1();
                    for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView2.Rows[i].Cells[0].Value)))
                        {
                            itm.itemName1 = Convert.ToString(dataGridView2.Rows[i].Cells[0].Value);
                            itm.itemId1 = itm.getItemId();
                            itm.itemTotalFees1 = Convert.ToDecimal(dataGridView2.Rows[i].Cells[4].Value.ToString());
                            itm.itemFees1 = Convert.ToDecimal(dataGridView2.Rows[i].Cells[3].Value.ToString());
                            itm.itemCount1 = Convert.ToInt32(dataGridView2.Rows[i].Cells[2].Value.ToString());
                            itm.itemWeight1 = Convert.ToDecimal(dataGridView2.Rows[i].Cells[1].Value.ToString());
                            itm.itemCarat1 = Convert.ToInt32(Convert.ToString(dataGridView2.Rows[i].Cells[0].Value).Trim().Substring(Math.Max(0, Convert.ToString(dataGridView2.Rows[i].Cells[0].Value).Trim().Length - 2)));
                            itm.newitemTotalFees1 = 0;
                            itm.newitemFees1 = 0;
                            itm.newitemCount1 = 0;
                            itm.newitemWeight1 = 0;
                            itm.newitemCarat1 = 0;
                            itm.insertEditBillDetails();
                        }
                    }
                    for (int i = 0; i < dataGridView4.RowCount - 1; i++)
                    {
                        pay.paymentId1 = Convert.ToInt32(IDTxtBox.Text);
                        pay.newBefore1 = 0;
                        pay.newAfter1 = 0;
                        pay.newConvert1 = "حذف";
                        pay.before1 = Convert.ToDecimal(dataGridView4.Rows[i].Cells[0].Value);
                        pay.after1 = Convert.ToDecimal(dataGridView4.Rows[i].Cells[2].Value);
                        pay.convert1 = dataGridView4.Rows[i].Cells[1].Value.ToString();
                        pay.insertEditBillConvertDetails();
                    }
                    ////////////////
                    itm.cash1 = Convert.ToDecimal(cashTxtbox1.Text);
                    if (billTypeLabel1.Text == "بيع")
                    {
                        pay.customerName1 = customerNameTxtbox1.Text;
                        pay.customerId1 = itm.GetCustomerid();
                        pay.paymentTypeInt1 = -1;
                        pay.paymentCash1 = Convert.ToDecimal(cashTxtbox1.Text);
                        pay.payment211 = Convert.ToDecimal(gold21Txtbox1.Text);
                        pay.payment181 = Convert.ToDecimal(gold18Txtbox1.Text);
                        pay.payment141 = Convert.ToDecimal(gold14Txtbox1.Text);
                        pay.addPayment1();
                        decimal gold21 = Convert.ToDecimal(gold21Txtbox1.Text);
                        decimal gold18 = Convert.ToDecimal(gold18Txtbox1.Text);
                        decimal gold14 = Convert.ToDecimal(gold14Txtbox1.Text);
                        if (gold14 != 0)
                        {
                            itm.itemCarat1 = 14;
                            itm.itemWeight1 = gold14;
                            itm.addGoldToFonding();
                        }
                        if (gold18 != 0)
                        {
                            itm.itemCarat1 = 18;
                            itm.itemWeight1 = gold18;
                            itm.addGoldToFonding();
                        }
                        if (gold21 != 0)
                        {
                            itm.itemCarat1 = 21;
                            itm.itemWeight1 = gold21;
                            itm.addGoldToFonding();
                        }
                        if (Convert.ToInt32(discountTxtbox.Text) > 0)
                        {
                            itm.billType1 = billTypeLabel1.Text;
                            if (label44.Text == "-")
                                itm.discountAmount1 = Convert.ToInt32(discountTxtbox1.Text) * -1;
                            else if (label44.Text == "+")
                                itm.discountAmount1 = Convert.ToInt32(discountTxtbox1.Text);
                            itm.newdiscountAmount = 0;
                            itm.billId1 = Convert.ToInt32(IDTxtBox.Text);
                            itm.updateDiscount();
                        }
                    }
                    else if (billTypeLabel1.Text == "شراء")
                    {
                        pay.customerName1 = customerNameTxtbox1.Text;
                        pay.customerId1 = itm.GetCustomerid();
                        pay.paymentTypeInt1 = 1;
                        pay.paymentCash1 = Convert.ToDecimal(cashTxtbox1.Text);
                        pay.payment211 = Convert.ToDecimal(gold21Txtbox1.Text);
                        pay.payment181 = Convert.ToDecimal(gold18Txtbox1.Text);
                        pay.payment141 = Convert.ToDecimal(gold14Txtbox1.Text);
                        pay.addPayment1();
                        decimal gold21 = Convert.ToDecimal(gold21Txtbox1.Text);
                        decimal gold18 = Convert.ToDecimal(gold18Txtbox1.Text);
                        decimal gold14 = Convert.ToDecimal(gold14Txtbox1.Text);
                        if (gold14 != 0)
                        {
                            itm.itemCarat1 = 14;
                            itm.itemWeight1 = gold14;
                            itm.subGoldFromFonding();
                        }
                        if (gold18 != 0)
                        {
                            itm.itemCarat1 = 18;
                            itm.itemWeight1 = gold18;
                            itm.subGoldFromFonding();
                        }
                        if (gold21 != 0)
                        {
                            itm.itemCarat1 = 21;
                            itm.itemWeight1 = gold21;
                            itm.subGoldFromFonding();
                        }
                        if (Convert.ToInt32(discountTxtbox.Text) > 0)
                        {
                            itm.billType1 = billTypeLabel1.Text;
                            if (label44.Text == "-")
                                itm.discountAmount1 = Convert.ToInt32(discountTxtbox1.Text) * -1;
                            else if (label44.Text == "+")
                                itm.discountAmount1 = Convert.ToInt32(discountTxtbox1.Text);
                            itm.newdiscountAmount = 0;
                            itm.billId1 = Convert.ToInt32(IDTxtBox.Text);
                            itm.updateDiscount();
                        }

                    }
                    //////////////////
                }
                else
                {
                    MessageBox.Show(a1);
                }
            }
        }

        public void addNewBill1()
        {
            if (main.IsConnected())
            {
                itm.customerName1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
                itm.customerId1 = itm.GetCustomerid();
                itm.userName1 = GlobalVar.userName;
                itm.userId1 = itm.getUserId();
                itm.billTotalCash1 = Convert.ToDecimal(cashTxtbox.Text);
                itm.billTotal211 = Convert.ToDecimal(gold21Txtbox.Text);
                itm.billTotal181 = Convert.ToDecimal(gold18Txtbox.Text);
                itm.billTotal141 = Convert.ToDecimal(gold14Txtbox.Text);
                itm.discountAmount1 = Convert.ToInt32(discountTxtbox.Text);
                if (label44.Text.Trim() == "+")
                    itm.discountType1 = "+1";
                else if (label44.Text.Trim() == "-")
                    itm.discountType1 = "-1";
                itm.billId1 = Convert.ToInt32(IDTxtBox.Text);
                if (detailsTxtbox.Text != "اكتب ملاحظات عن الفاتورة")
                {
                    itm.billNotice1 = detailsTxtbox.Text;
                }
                else
                {
                    itm.billNotice1 = "تعديل الفاتورة رقم  " + IDTxtBox.Text;
                }
                if (no.Text.Trim() != "")
                {
                    itm.billNo1 = no.Text;
                }
                itm.billType1 = billTypeLabel.Text;
                ////////////
                string a1 = itm.updateBill();
                if (a1 == "تم تعديل الفاتورة")
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[0].Value)))
                        {
                            if (billTypeLabel.Text.Trim() == "بيع")
                            {
                                itm.itemName1 = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                                itm.itemId1 = itm.getItemId();
                                itm.itemCount1 = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());
                                itm.itemWeight1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString());
                                itm.sellItem();
                            }
                            else
                            {
                                itm.itemName1 = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                                itm.itemId1 = itm.getItemId();
                                itm.itemFees1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value.ToString());
                                itm.itemCount1 = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());
                                itm.itemWeight1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString());
                                itm.buyItem();
                            }
                        }
                    }
                    itm.customerName1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
                    itm.customerId1 = itm.GetCustomerid();
                    itm.userName1 = GlobalVar.userName;
                    itm.userId1 = itm.getUserId();
                    itm.billId1 = Convert.ToInt32(IDTxtBox.Text);
                    itm.billType1 = billTypeLabel.Text;
                    itm.newdiscountAmount1 = Convert.ToInt32(discountTxtbox.Text);
                    itm.newbillTotalCash1 = Convert.ToDecimal(totalCashTxtbox.Text);
                    itm.newbillTotal211 = Convert.ToDecimal(gold21Txtbox.Text);
                    itm.newbillTotal181 = Convert.ToDecimal(gold18Txtbox.Text);
                    itm.newbillTotal141 = Convert.ToDecimal(gold14Txtbox.Text);
                    itm.dateT = dateTimePicker1.Value.Date;
                    itm.editBillNotice1 = "تعديل الفاتورة" + IDTxtBox.Text;
                    itm.discountAmount1 = 0;
                    itm.billTotalCash1 = 0;
                    itm.billTotal211 = 0;
                    itm.billTotal181 = 0;
                    itm.billTotal141 = 0;

                    itm.insertEditBill();

                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dataGridView1.Rows[i].Cells[0].Value)))
                        {
                            itm.itemName1 = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                            itm.itemId1 = itm.getItemId();
                            itm.billType1 = billTypeLabel.Text;
                            itm.itemTotalFees1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString());
                            itm.itemFees1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value.ToString());
                            itm.itemCount1 = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());
                            itm.itemWeight1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString());
                            itm.itemCarat1 = Convert.ToInt32(Convert.ToString(dataGridView2.Rows[i].Cells[0].Value).Trim().Substring(Math.Max(0, Convert.ToString(dataGridView2.Rows[i].Cells[0].Value).Trim().Length - 2)));
                            itm.insertBillDetails();

                            itm.itemName1 = Convert.ToString(dataGridView1.Rows[i].Cells[0].Value);
                            itm.itemId1 = itm.getItemId();
                            itm.itemTotalFees1 = 0;
                            itm.itemFees1 = 0;
                            itm.itemCount1 = 0;
                            itm.itemWeight1 = 0;
                            itm.itemCarat1 = 0;
                            itm.newitemTotalFees1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value.ToString());
                            itm.newitemFees1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value.ToString());
                            itm.newitemCount1 = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value.ToString());
                            itm.newitemWeight1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString());
                            itm.newitemCarat1 = Convert.ToInt32(Convert.ToString(dataGridView2.Rows[i].Cells[0].Value).Trim().Substring(Math.Max(0, Convert.ToString(dataGridView2.Rows[i].Cells[0].Value).Trim().Length - 2)));
                            itm.billId1 = Convert.ToInt32(IDTxtBox.Text);
                            itm.insertEditBillDetails();
                        }
                    }
                    if (dataGridView3.RowCount > 1)
                    {
                        for (int i = 0; i < dataGridView3.RowCount - 1; i++)
                        {
                            if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة اجور إلى ذهب21" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة اجور إلى ذهب18" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة اجور إلى ذهب14" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب21 إلى اجور" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب21 إلى ذهب18" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب21 إلى ذهب14" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب18 إلى اجور" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب18 إلى ذهب21" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب18 إلى ذهب14" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب14 إلى اجور" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب14 إلى ذهب21" || dataGridView3.Rows[i].Cells[1].Value.ToString() == "فاتورة ذهب14 إلى ذهب18")
                            {
                                pay.paymentId1 = Convert.ToInt32(IDTxtBox.Text);
                                pay.before1 = Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value);
                                pay.after1 = Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value);
                                pay.convert1 = dataGridView3.Rows[i].Cells[1].Value.ToString();
                                pay.insertBillConvertDetails();

                                pay.paymentId1 = Convert.ToInt32(IDTxtBox.Text);
                                pay.before1 = 0;
                                pay.after1 = 0;
                                pay.convert1 = "اضافة";
                                pay.newBefore1 = Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value);
                                pay.newAfter1 = Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value);
                                pay.newConvert1 = dataGridView3.Rows[i].Cells[1].Value.ToString();
                                pay.insertEditBillConvertDetails();
                            }
                        }
                    }
                    ////////////////
                    itm.cash1 = Convert.ToDecimal(cashTxtbox.Text);
                    if (billTypeLabel.Text.Trim() == "بيع")
                    {
                        pay.customerName1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
                        pay.customerId1 = itm.GetCustomerid();
                        pay.paymentTypeInt1 = 1;
                        pay.paymentCash1 = Convert.ToDecimal(cashTxtbox.Text);
                        pay.payment211 = Convert.ToDecimal(gold21Txtbox.Text);
                        pay.payment181 = Convert.ToDecimal(gold18Txtbox.Text);
                        pay.payment141 = Convert.ToDecimal(gold14Txtbox.Text);
                        pay.addPayment1();
                        decimal gold21 = Convert.ToDecimal(gold21Txtbox.Text);
                        decimal gold18 = Convert.ToDecimal(gold18Txtbox.Text);
                        decimal gold14 = Convert.ToDecimal(gold14Txtbox.Text);
                        if (gold14 != 0)
                        {
                            itm.itemCarat1 = 14;
                            itm.itemWeight1 = gold14;
                            itm.subGoldFromFonding();
                        }
                        if (gold18 != 0)
                        {
                            itm.itemCarat1 = 18;
                            itm.itemWeight1 = gold18;
                            itm.subGoldFromFonding();
                        }
                        if (gold21 != 0)
                        {
                            itm.itemCarat1 = 21;
                            itm.itemWeight1 = gold21;
                            itm.subGoldFromFonding();
                        }
                        if (Convert.ToInt32(discountTxtbox.Text) > 0)
                        {
                            itm.billType1 = billTypeLabel.Text;
                            itm.customerName1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
                            itm.customerId1 = itm.GetCustomerid();
                            itm.userName1 = GlobalVar.userName;
                            itm.userId1 = itm.getUserId();
                            itm.dateT1 = dateTimePicker1.Value.Date;
                            itm.discountAmount = Convert.ToInt32(discountTxtbox.Text);
                            itm.discountType1 = label44.Text;
                            itm.billId1 = Convert.ToInt32(IDTxtBox.Text);
                            itm.insertDiscount();
                        }
                    }
                    else if (billTypeLabel.Text == "شراء")
                    {
                        pay.customerId1 = itm.GetCustomerid();
                        pay.paymentTypeInt1 = -1;
                        pay.paymentCash1 = Convert.ToDecimal(cashTxtbox.Text);
                        pay.payment211 = Convert.ToDecimal(gold21Txtbox.Text);
                        pay.payment181 = Convert.ToDecimal(gold18Txtbox.Text);
                        pay.payment141 = Convert.ToDecimal(gold14Txtbox.Text);
                        pay.addPayment1();
                        decimal gold21 = Convert.ToDecimal(gold21Txtbox.Text);
                        decimal gold18 = Convert.ToDecimal(gold18Txtbox.Text);
                        decimal gold14 = Convert.ToDecimal(gold14Txtbox.Text);
                        if (gold14 != 0)
                        {
                            itm.itemCarat1 = 14;
                            itm.itemWeight1 = gold14;
                            itm.addGoldToFonding();
                        }
                        if (gold18 != 0)
                        {
                            itm.itemCarat1 = 18;
                            itm.itemWeight1 = gold18;
                            itm.addGoldToFonding();
                        }
                        if (gold21 != 0)
                        {
                            itm.itemCarat1 = 21;
                            itm.itemWeight1 = gold21;
                            itm.addGoldToFonding();
                        }
                        if (Convert.ToInt32(discountTxtbox.Text) > 0)
                        {
                            itm.billType1 = billTypeLabel.Text;
                            itm.customerName1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
                            itm.customerId1 = itm.GetCustomerid();
                            itm.userName1 = GlobalVar.userName;
                            itm.userId1 = itm.getUserId();
                            itm.dateT1 = dateTimePicker1.Value.Date;
                            itm.discountType1 = label44.Text;
                            itm.discountAmount = Convert.ToInt32(discountTxtbox.Text);
                            itm.billId1 = Convert.ToInt32(IDTxtBox.Text);
                            itm.insertDiscount();
                        }
                    }
                    //////////////////
                }
                else
                {
                    MessageBox.Show(a1);
                }
            }
        }

        private void updateBill_Click(object sender, EventArgs e)
        {
            if (main.IsConnected())
            {
                if (dataGridView7.Rows[0].Cells[0].Value.ToString() == "أدخل اسم الصائغ" || dataGridView7.Rows[0].Cells[0].Value.ToString() == "")
                {
                    MessageBox.Show("يرجى كتابة اسم الصائغ");
                }

                else if (!check())
                {
                    DialogResult result = MessageBox.Show("هل أنت متأكد بتعديل الفاتورة رقم" + IDTxtBox.Text, "تعديل فاتورة", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                    }
                    else
                    {
                        cus.name1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
                        if (cus.checkCustomerExist() == "true")
                        {
                            name = dataGridView7.Rows[0].Cells[0].Value.ToString();

                            delete_bill();
                            addNewBill1();
                            MessageBox.Show("تم تعديل الفاتورة");

                            no.Text = "000";
                            detailsTxtbox.Text = "اكتب ملاحظات عن الفاتورة";
                            dateTimePicker1.Value = DateTime.Now.Date;
                            IDTxtBox.Text = idLabel(itm.getBillsCount());
                            dt1.Clear();
                            dt2.Clear();
                            dataT1.Clear();
                            dataGridView7.Rows[0].Cells[0].Value = "أدخل اسم الصائغ";
                            cashTxtbox.Text = "0";
                            totalCashTxtbox.Text = "0";
                            discountTxtbox.Text = "0";
                            gold14Txtbox.Text = "0";
                            gold18Txtbox.Text = "0";
                            gold21Txtbox.Text = "0";
                            payCash.Text = "0";
                            payGold21.Text = "0";
                            payGold18.Text = "0";
                            payGold14.Text = "0";
                            real21.Text = "0";
                            real18.Text = "0";
                            real14.Text = "0";
                            realCash.Text = "0";
                            cashAfter.Text = "0";
                            gold14After.Text = "0";
                            gold18After.Text = "0";
                            gold21After.Text = "0";
                            caratTxtbox.Text = "0";
                            no.Text = "000";
                            detailsTxtbox.Text = "اكتب ملاحظات عن الفاتورة";
                            dateTimePicker1.Value = DateTime.Now.Date;
                            dataGridView7.Rows[0].Cells[4].Value = "0";
                            dataGridView7.Rows[0].Cells[3].Value = "0";
                            dataGridView7.Rows[0].Cells[2].Value = "0";
                            dataGridView7.Rows[0].Cells[1].Value = "0";
                            //dataGridView1.DataSource = null;
                            dataT1.Clear();
                            dataT3.Clear();

                            //dataT.Rows.Add("لا تستخدم هذا السطر",0, 0, 0, 0);


                            dataGridView1.Columns[0].Width = 300;
                            //dataGridView1.Rows[0].Height = 0;

                            dataGridView7.Select();
                            dataGridView7.Focus();
                        }
                        else
                        {
                            MessageBox.Show("اسم الصائغ الذي ادخلته غير موجود في البرنامج");
                        }
                    }
                }
            }
        }

        private void printBill_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView7.Rows[0].Cells[0].Value = name;
                ii = -1;
                print_click();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void print_click()
        {
            //((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized;
            //if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            //{
            printDocument1.Print();
            //}
        }

        public void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font ff = new Font("Arial", 8, FontStyle.Bold);
            Font f = new Font("Arial", 10, FontStyle.Bold);
            Font f1 = new Font("Arial", 14, FontStyle.Bold | FontStyle.Underline);
            Font f11 = new Font("Arial", 24, FontStyle.Bold);
            Font f2 = new Font("Arial", 12, FontStyle.Bold);
            string strR = "الاتصال بالرقم 0956799996" + " iGOLD " + "للاستعلام عن برنامج";
            string strDate = "التاريخ: " + date.Text;
            string T = "";
            if (time1.Text == "AM")
            {
                T = " صباحاً ";
            }
            else
            {
                T = " مساءً ";
            }
            string strClock = "الوقت:  " + time.Text.Substring(0, 2) + ":" + time.Text.Substring(time.Text.Length - 2, 2) + T;
            string strID = "رقم الفاتورة: " + IDTxtBox.Text;
            string strType = label28.Text + " " + billTypeLabel.Text;
            string cusName = "اسم الصائغ: " + dataGridView7.Rows[0].Cells[0].Value.ToString();
            cus.customerName1 = dataGridView7.Rows[0].Cells[0].Value.ToString();
            string cusMob = "";
            if (dataGridView7.Rows[0].Cells[0].Value.ToString() != "أدخل اسم الصائغ" && dataGridView7.Rows[0].Cells[0].Value.ToString() != "")
            {
                cusMob = "رقم الموبايل: " + cus.getMobileOfName();
            }
            string user = "منظم الفاتورة" + Environment.NewLine + GlobalVar.userName;
            string pLabel = "اجمالي الفاتورة";
            string pLabel1 = "حساب الصائغ بعد تسجيل الفاتورة";
            string pLabel2 = "ثقتكم سر نجاحنا";
            string label = "حساب الصائغ بعد الفاتورة";
            string blLabel = "تفاصيل الفاتورة";
            string vLabel = "لم يتم اضافة مصوغات إلى الفاتورة";
            string cashLabel = "الاجور المطلوب";
            string cashLabel1 = "الاجور";
            string gold21Label = GlobalVar.gold21Label;
            string gold18Label = GlobalVar.gold18Label;
            string gold14Label = GlobalVar.gold14Label;
            string discountLabel = "الحسم";
            string tCashLabel = "الاجور";

            string cash = GlobalVar.currencyLabel + " " + cashTxtbox.Text + " ";
            string cash1 = GlobalVar.currencyLabel + " " + cashTxtbox.Text + " ";
            string tCash = GlobalVar.currencyLabel + " " + totalCashTxtbox.Text + " ";
            string discount = GlobalVar.currencyLabel + " " + discountTxtbox.Text + " ";
            string gold21 = GlobalVar.gramLabel + " " + gold21Txtbox.Text + " ";
            string gold18 = GlobalVar.gramLabel + " " + gold18Txtbox.Text + " ";
            string gold14 = GlobalVar.gramLabel + " " + gold14Txtbox.Text + " ";

            string customerCash = label68.Text + " " + cashAfter.Text + " ";
            string customerGold21 = label67.Text + " " + gold21After.Text + " ";
            string customerGold18 = label71.Text + " " + gold18After.Text + " ";
            string customerGold14 = label64.Text + " " + gold14After.Text + " ";

            SizeF sDiscountLabel = e.Graphics.MeasureString(discountLabel, f2);
            SizeF sTCashLabel = e.Graphics.MeasureString(tCashLabel, f2);
            SizeF sDiscount = e.Graphics.MeasureString(discount, f2);
            SizeF sTCash = e.Graphics.MeasureString(tCash, f2);
            SizeF sStrR = e.Graphics.MeasureString(strR, ff);
            SizeF sStrDate = e.Graphics.MeasureString(strDate, f);
            SizeF sStrClock = e.Graphics.MeasureString(strClock, f);
            SizeF sStrID = e.Graphics.MeasureString(strID, f);
            SizeF sStrType = e.Graphics.MeasureString(strType, f1);
            SizeF sCusName = e.Graphics.MeasureString(cusName, f2);
            SizeF sCusMob = e.Graphics.MeasureString(cusMob, f2);
            SizeF sPLabel = e.Graphics.MeasureString(pLabel, f1);
            SizeF sBlLabel = e.Graphics.MeasureString(blLabel, f1);
            SizeF sPLabel1 = e.Graphics.MeasureString(pLabel1, f1);
            SizeF sPLabel2 = e.Graphics.MeasureString(pLabel2, f1);
            SizeF sUser = e.Graphics.MeasureString(user, f1);
            SizeF sLabel = e.Graphics.MeasureString(label, f1);
            SizeF sVLabel = e.Graphics.MeasureString(vLabel, f1);
            SizeF sCashLabel = e.Graphics.MeasureString(cashLabel, f2);
            SizeF sCashLabel1 = e.Graphics.MeasureString(cashLabel1, f2);

            SizeF sGold21Label = e.Graphics.MeasureString(gold21Label, f2);
            SizeF sGold18Label = e.Graphics.MeasureString(gold18Label, f2);
            SizeF sGold14Label = e.Graphics.MeasureString(gold14Label, f2);
            SizeF sCash = e.Graphics.MeasureString(cash, f2);
            SizeF sCash1 = e.Graphics.MeasureString(cash1, f2);
            SizeF sGold21 = e.Graphics.MeasureString(gold21, f2);
            SizeF sGold18 = e.Graphics.MeasureString(gold18, f2);
            SizeF sGold14 = e.Graphics.MeasureString(gold14, f2);
            SizeF sCustomerCash = e.Graphics.MeasureString(customerCash, f2);
            SizeF sCustomerGold21 = e.Graphics.MeasureString(customerGold21, f2);
            SizeF sCustomerGold18 = e.Graphics.MeasureString(customerGold18, f2);
            SizeF sCustomerGold14 = e.Graphics.MeasureString(customerGold14, f2);

            float margin = 30;
            float marginWidth = margin;
            float marginHeight = 0;
            float shift = 10;

            float col1width = e.PageBounds.Width / 3;
            float col2width = e.PageBounds.Width / 4;
            float col22width = e.PageBounds.Width / 4;
            float col3width = (e.PageBounds.Width - (2 * margin)) / dataGridView1.ColumnCount;

            float table_height1 = (2 * margin) - shift;
            float table_height2 = ((margin - shift) * (dataGridView1.RowCount));

            float preHeight = (4 * margin) + sStrID.Height + sStrDate.Height + sStrClock.Height + (2 * shift);
            float preHeight2 = preHeight;
            float preHeight1 = preHeight2;
            float preHeight3 = preHeight1 + (2 * margin);
            //////////header//////////
            e.HasMorePages = false;
            int noPages = 0;
            int noPages1 = 0;
            int noRows = 0;
            noPages1 = (dataGridView1.Rows.Count / 40) + 1;
            noRows = (dataGridView1.Rows.Count % 40);
            if (noRows > 29)
            {
                noPages = noPages1 + 1;
            }
            else
            {
                noPages = noPages1;
            }
            ii++;
            string page = "الصفحة " + (ii + 1) + " من " + noPages + " صفحات";
            SizeF sPage = e.Graphics.MeasureString(page, ff);
            printHeaderFooter(marginWidth, marginHeight, strR, ff, sStrR, page, sPage, e);
            /////////بيانات الفاتورة والصائغ/////////////////////
            e.Graphics.DrawString(strID, f, Brushes.Black, e.PageBounds.Width - margin - sStrID.Width, 4 * margin - shift);
            e.Graphics.DrawString(strDate, f, Brushes.Black, e.PageBounds.Width - margin - sStrDate.Width, 4 * margin + sStrID.Height);
            e.Graphics.DrawString(strClock, f, Brushes.Black, e.PageBounds.Width - margin - sStrClock.Width, 4 * margin + sStrID.Height + sStrDate.Height + shift);
            e.Graphics.DrawString(strType, f1, Brushes.Black, (e.PageBounds.Width - sStrType.Width + margin) / 2, 4 * margin + sStrID.Height + sStrDate.Height + shift);
            //////////جدول اسم الصائغ//////////////////////////////////////
            e.Graphics.DrawRectangle(Pens.Black, margin, preHeight, e.PageBounds.Width - (2 * margin), margin);
            e.Graphics.DrawLine(Pens.Black, col1width, preHeight, col1width, preHeight + margin);
            e.Graphics.DrawString(cusName, f2, Brushes.Black, e.PageBounds.Width - margin - sCusName.Width, preHeight + (shift / 2));
            e.Graphics.DrawString(cusMob, f2, Brushes.Black, 2 * margin, preHeight + (shift / 2));
            if (dataGridView1.Rows.Count > 0)
            {
                preHeight2 = preHeight + (2 * margin) - shift;
                preHeight1 = preHeight2 + (2 * margin) + table_height2 + shift;
                preHeight3 = preHeight1 + (2 * margin);
                for (; ii < noPages; ii++)
                {
                    printHeaderFooter(marginWidth, marginHeight, strR, ff, sStrR, page, sPage, e);
                    int SS = (dataGridView1.RowCount - (40 * (ii)));
                    if (SS < 0) { SS = 1; }
                    table_height2 = ((margin - shift) * SS);
                    /////////جدول بيانات اجمالي الفاتورة/////////////////////////////////////////
                    e.Graphics.DrawString(blLabel, f1, Brushes.Black, (e.PageBounds.Width - sBlLabel.Width) / 2, preHeight2);
                    e.Graphics.DrawRectangle(Pens.Black, margin, preHeight2 + margin, e.PageBounds.Width - (2 * margin), table_height2);
                    e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight2 + margin + 2, e.PageBounds.Width - (2 * margin) - 2, margin - shift - 3);
                    string A = "";
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        A = dataGridView1.Columns[j].HeaderText.ToString();
                        SizeF V = e.Graphics.MeasureString(A, f2);
                        float B = e.PageBounds.Width - margin - ((j + 1) * col3width) + ((col3width - V.Width) / 2);
                        if (j != 0)
                        {
                            if (j != 1)
                            {
                                e.Graphics.DrawString(A, f2, Brushes.Black, B, preHeight2 + (3 * shift));
                            }
                            else
                            {
                                e.Graphics.DrawString(A, f2, Brushes.Black, B - margin, preHeight2 + (3 * shift));
                            }
                            e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width - margin - ((j + 1) * col3width), preHeight2 + margin, e.PageBounds.Width - margin - ((j + 1) * col3width), preHeight2 + margin + table_height2);
                        }
                        else
                        {
                            e.Graphics.DrawString(A, f2, Brushes.Black, B - margin, preHeight2 + (3 * shift));
                            e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width - margin - ((j + 1) * col3width) - (2 * margin), preHeight2 + margin, e.PageBounds.Width - margin - ((j + 1) * col3width) - (2 * margin), preHeight2 + margin + table_height2);
                        }
                    }
                    for (int i = 0 + (40 * (ii)); i < 40 + (40 * (ii)) && i < dataGridView1.RowCount - 1; i++)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            A = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            SizeF V = e.Graphics.MeasureString(A, f);
                            float B = e.PageBounds.Width - margin - ((j + 1) * col3width) + ((col3width - V.Width) / 2);
                            if (j > 0)
                            {
                                if (j > 1)
                                    e.Graphics.DrawString(A, f, Brushes.Black, B, preHeight2 + margin + (((i - (40 * (ii))) + 1) * (2 * shift)));
                                else
                                    e.Graphics.DrawString(A, f, Brushes.Black, B - margin, preHeight2 + margin + (((i - (40 * (ii))) + 1) * (2 * shift)));
                            }
                            else
                            {
                                e.Graphics.DrawString(A, f, Brushes.Black, B - margin, preHeight2 + margin + (((i - (40 * (ii))) + 1) * (2 * shift)));
                            }
                            e.Graphics.DrawLine(Pens.Black, margin, preHeight2 + margin + (((i - (40 * (ii))) + 1) * (2 * shift)), e.PageBounds.Width - margin, preHeight2 + margin + (((i - (40 * (ii))) + 1) * (2 * shift)));
                        }
                    }
                    if (ii == (noPages - 1))
                    {
                        preHeight1 = preHeight2 + (2 * margin) + table_height2 + shift;
                        preHeight3 = preHeight1 + (2 * margin);
                        if (Convert.ToDecimal(discountTxtbox.Text) == 0)
                        {
                            //////////لافتة اجمالي الفاتورة///////////////////////////////////////
                            e.Graphics.DrawString(pLabel, f1, Brushes.Black, (e.PageBounds.Width - sPLabel.Width + margin) / 2, preHeight1 - margin);
                            e.Graphics.DrawRectangle(Pens.Black, margin, preHeight1, e.PageBounds.Width - (2 * margin), table_height1);
                            e.Graphics.FillRectangle(Brushes.LightGray, margin + 2, preHeight1 + 2, e.PageBounds.Width - (2 * margin) - 3, margin - shift - 3);
                            e.Graphics.DrawLine(Pens.Black, col2width, preHeight1, col2width, preHeight1 + table_height1);
                            e.Graphics.DrawLine(Pens.Black, 2 * col2width, preHeight1, 2 * col2width, preHeight1 + table_height1);
                            e.Graphics.DrawLine(Pens.Black, 3 * col2width, preHeight1, 3 * col2width, preHeight1 + table_height1);
                            e.Graphics.DrawLine(Pens.Black, margin, preHeight1 + margin - shift, e.PageBounds.Width - margin, preHeight1 + margin - shift);
                            /////////////////////////////////////////////////
                            e.Graphics.DrawString(gold14Label, f2, Brushes.Black, ((col2width + margin - sGold14Label.Width) / 2), preHeight1 + 3);
                            e.Graphics.DrawString(gold18Label, f2, Brushes.Black, col2width + sGold18Label.Width + margin, preHeight1 + 3);
                            e.Graphics.DrawString(gold21Label, f2, Brushes.Black, (2 * col2width) + sGold21Label.Width + margin, preHeight1 + 3);
                            e.Graphics.DrawString(cashLabel1, f2, Brushes.Black, (3 * col2width) + sCashLabel1.Width + margin, preHeight1 + 3);
                            //////////////////////////////////////////////////
                            e.Graphics.DrawString(gold14, f2, Brushes.Black, margin + (((col2width - margin) - sGold14.Width) / 2), preHeight1 + margin - (shift / 2));
                            e.Graphics.DrawString(gold18, f2, Brushes.Black, margin + col2width + (((col2width - margin) - sGold18.Width) / 2) - (2 * shift), preHeight1 + margin - (shift / 2));
                            e.Graphics.DrawString(gold21, f2, Brushes.Black, margin + (2 * col2width) + (((col2width - margin) - sGold21.Width) / 2) - (2 * shift), preHeight1 + margin - (shift / 2));
                            e.Graphics.DrawString(cash1, f2, Brushes.Black, margin + (3 * col2width) + (((col2width - margin) - sCash1.Width) / 2) - margin, preHeight1 + margin - (shift / 2));
                        }
                        else
                        {
                            //////////لافتة اجمالي الفاتورة///////////////////////////////////////
                            e.Graphics.DrawString(pLabel, f1, Brushes.Black, (e.PageBounds.Width - sPLabel.Width + margin) / 2, preHeight1 - margin);
                            col2width = e.PageBounds.Width / 6;
                            e.Graphics.DrawRectangle(Pens.Black, margin, preHeight1, e.PageBounds.Width - (2 * margin), table_height1);
                            e.Graphics.FillRectangle(Brushes.LightGray, margin + 2, preHeight1 + 2, e.PageBounds.Width - (2 * margin) - 3, margin - shift - 3);
                            e.Graphics.DrawLine(Pens.Black, col2width, preHeight1, col2width, preHeight1 + table_height1);
                            e.Graphics.DrawLine(Pens.Black, 2 * col2width, preHeight1, 2 * col2width, preHeight1 + table_height1);
                            e.Graphics.DrawLine(Pens.Black, 3 * col2width, preHeight1, 3 * col2width, preHeight1 + table_height1);
                            e.Graphics.DrawLine(Pens.Black, 4 * col2width, preHeight1, 4 * col2width, preHeight1 + table_height1);
                            e.Graphics.DrawLine(Pens.Black, 5 * col2width, preHeight1, 5 * col2width, preHeight1 + table_height1);
                            e.Graphics.DrawLine(Pens.Black, margin, preHeight1 + margin - shift, e.PageBounds.Width - margin, preHeight1 + margin - shift);
                            /////////////////////////////////////////////////
                            e.Graphics.DrawString(gold14Label, f2, Brushes.Black, ((col2width + margin - sGold14Label.Width) / 2), preHeight1 + 3);
                            e.Graphics.DrawString(gold18Label, f2, Brushes.Black, col2width + sGold18Label.Width + (shift / 2), preHeight1 + 3);
                            e.Graphics.DrawString(gold21Label, f2, Brushes.Black, (2 * col2width) + sGold21Label.Width + (shift / 2), preHeight1 + 3);
                            e.Graphics.DrawString(cashLabel, f2, Brushes.Black, (3 * col2width) + sCashLabel.Width - (2 * margin) - (shift / 2), preHeight1 + 3);
                            e.Graphics.DrawString(discountLabel, f2, Brushes.Black, (4 * col2width) + sDiscountLabel.Width + shift + (shift / 2), preHeight1 + 3);
                            e.Graphics.DrawString(tCashLabel, f2, Brushes.Black, (5 * col2width) + sTCashLabel.Width, preHeight1 + 3);
                            //////////////////////////////////////////////////
                            e.Graphics.DrawString(gold14, f2, Brushes.Black, margin + (((col2width - margin) - sGold14.Width) / 2), preHeight1 + margin - (shift / 2));
                            e.Graphics.DrawString(gold18, f2, Brushes.Black, margin + col2width + (((col2width - margin) - sGold18.Width) / 2) - (2 * shift), preHeight1 + margin - (shift / 2));
                            e.Graphics.DrawString(gold21, f2, Brushes.Black, margin + (2 * col2width) + (((col2width - margin) - sGold21.Width) / 2) - (2 * shift), preHeight1 + margin - (shift / 2));
                            e.Graphics.DrawString(cash, f2, Brushes.Black, margin + (3 * col2width) + (((col2width - margin) - sCash.Width) / 2) - margin + (2 * shift), preHeight1 + margin - (shift / 2));
                            e.Graphics.DrawString(discount, f2, Brushes.Black, margin + (4 * col2width) + (((col2width - margin) - sGold21.Width) / 2) - (2 * shift), preHeight1 + margin - (shift / 2));
                            e.Graphics.DrawString(tCash, f2, Brushes.Black, margin + (5 * col2width) + (((col2width - margin) - sCash.Width) / 2) - margin, preHeight1 + margin - (shift / 2));
                        }
                        /////////جدول بيانات حساب الصائغ/////////////////////////////////////////
                        e.Graphics.DrawRectangle(Pens.Black, margin, preHeight3 + margin, e.PageBounds.Width - (2 * margin), table_height1);
                        e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight3 + margin + 2, e.PageBounds.Width - (2 * margin) - 2, margin - shift - 3);
                        e.Graphics.DrawLine(Pens.Black, col22width, preHeight3 + margin, col22width, preHeight3 + table_height1 + margin);
                        e.Graphics.DrawLine(Pens.Black, 2 * col22width, preHeight3 + margin, 2 * col22width, preHeight3 + table_height1 + margin);
                        e.Graphics.DrawLine(Pens.Black, 3 * col22width, preHeight3 + margin, 3 * col22width, preHeight3 + table_height1 + margin);
                        e.Graphics.DrawLine(Pens.Black, 4 * col22width, preHeight3 + margin, 4 * col22width, preHeight3 + table_height1 + margin);


                        e.Graphics.DrawLine(Pens.Black, margin, preHeight3 + table_height1, e.PageBounds.Width - margin, preHeight3 + table_height1);
                        //////////////////////////////////////////////////
                        e.Graphics.DrawString(gold14Label, f2, Brushes.Black, ((col22width + margin - sGold14Label.Width) / 2), preHeight3 + margin);
                        e.Graphics.DrawString(gold18Label, f2, Brushes.Black, col22width + margin + sGold18Label.Width, preHeight3 + margin);
                        e.Graphics.DrawString(gold21Label, f2, Brushes.Black, 2 * (col22width) + margin + sGold21Label.Width, preHeight3 + margin);
                        e.Graphics.DrawString(cashLabel1, f2, Brushes.Black, 3 * (col22width) + margin + sCashLabel1.Width, preHeight3 + margin);
                        e.Graphics.DrawString(pLabel1, f1, Brushes.Black, (e.PageBounds.Width - sPLabel1.Width + margin) / 2, preHeight3 + (shift / 2));
                        //////////////////////////////////////////////////
                        e.Graphics.DrawString(customerGold14, f2, Brushes.Black, margin + (((col22width - margin) - sCustomerGold14.Width) / 2), preHeight3 + (2 * margin) - (shift / 2));
                        e.Graphics.DrawString(customerGold18, f2, Brushes.Black, margin + col22width + (((col22width - margin) - sCustomerGold18.Width) / 2) - (2 * shift), preHeight3 + (2 * margin) - (shift / 2));
                        e.Graphics.DrawString(customerGold21, f2, Brushes.Black, margin + (2 * col22width) + (((col22width - margin) - sCustomerGold21.Width) / 2) - (2 * shift), preHeight3 + (2 * margin) - (shift / 2));
                        e.Graphics.DrawString(customerCash, f2, Brushes.Black, margin + (3 * col22width) + (((col22width - margin) - sCustomerCash.Width) / 2) - margin, preHeight3 + (2 * margin) - (shift / 2));
                        //////////////////////////////////////////////////
                        //////////خاتمة الصفحة///////////////////////////
                        e.Graphics.DrawString(pLabel2, f2, Brushes.Black, (e.PageBounds.Width - sPLabel2.Width + margin) / 2, preHeight3 + (4 * margin) - shift);
                        e.Graphics.DrawString(user, f2, Brushes.Black, margin, preHeight3 + (4 * margin) - shift);
                    }
                    else
                    {
                        e.HasMorePages = true;
                        break;
                    }
                }
            }
            else
            {
                preHeight1 += 15;
                e.Graphics.DrawString(vLabel, f11, Brushes.Black, (e.PageBounds.Width / 2) - (sVLabel.Width - (2 * margin) / 2), preHeight1 + margin);
                preHeight3 += margin;
                /////////جدول بيانات حساب الصائغ/////////////////////////////////////////
                e.Graphics.DrawRectangle(Pens.Black, margin, preHeight3 + margin, e.PageBounds.Width - (2 * margin), table_height1);
                e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight3 + margin + 2, e.PageBounds.Width - (2 * margin) - 2, margin - shift - 3);
                e.Graphics.DrawLine(Pens.Black, col22width, preHeight3 + margin, col22width, preHeight3 + table_height1 + margin);
                e.Graphics.DrawLine(Pens.Black, 2 * col22width, preHeight3 + margin, 2 * col22width, preHeight3 + table_height1 + margin);
                e.Graphics.DrawLine(Pens.Black, 3 * col22width, preHeight3 + margin, 3 * col22width, preHeight3 + table_height1 + margin);
                e.Graphics.DrawLine(Pens.Black, margin, preHeight3 + table_height1, e.PageBounds.Width - margin, preHeight3 + table_height1);
                //////////////////////////////////////////////////
                e.Graphics.DrawString(gold14Label, f2, Brushes.Black, ((col22width + margin - sGold14Label.Width) / 2), preHeight3 + margin);
                e.Graphics.DrawString(gold18Label, f2, Brushes.Black, col22width + margin + sGold18Label.Width, preHeight3 + margin);
                e.Graphics.DrawString(gold21Label, f2, Brushes.Black, 2 * (col22width) + margin + sGold21Label.Width, preHeight3 + margin);
                e.Graphics.DrawString(cashLabel1, f2, Brushes.Black, 3 * (col22width) + margin + sCashLabel1.Width, preHeight3 + margin);
                e.Graphics.DrawString(pLabel1, f1, Brushes.Black, (e.PageBounds.Width - sPLabel1.Width + margin) / 2, preHeight3 + (shift / 2));
                //////////////////////////////////////////////////
                e.Graphics.DrawString(customerGold14, f2, Brushes.Black, margin + (((col22width - margin) - sCustomerGold14.Width) / 2), preHeight3 + (2 * margin) - (shift / 2));
                e.Graphics.DrawString(customerGold18, f2, Brushes.Black, margin + col22width + (((col22width - margin) - sCustomerGold18.Width) / 2) - (2 * shift), preHeight3 + (2 * margin) - (shift / 2));
                e.Graphics.DrawString(customerGold21, f2, Brushes.Black, margin + (2 * col22width) + (((col22width - margin) - sCustomerGold21.Width) / 2) - (2 * shift), preHeight3 + (2 * margin) - (shift / 2));
                e.Graphics.DrawString(customerCash, f2, Brushes.Black, margin + (3 * col22width) + (((col22width - margin) - sCustomerCash.Width) / 2) - margin, preHeight3 + (2 * margin) - (shift / 2));
                //////////////////////////////////////////////////
                //////////خاتمة الصفحة///////////////////////////
                e.Graphics.DrawString(pLabel2, f2, Brushes.Black, (e.PageBounds.Width - sPLabel2.Width + margin) / 2, preHeight3 + (4 * margin) - shift);
                e.Graphics.DrawString(user, f2, Brushes.Black, margin, preHeight3 + (4 * margin) - shift);

            }
        }

        public void printHeaderFooter(float marginWidth, float marginHeight, string strR, Font ff, SizeF sStrR, string page, SizeF sPage, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(page, ff, Brushes.Black, marginWidth, 102);
            e.Graphics.DrawImage(Properties.Resources.igoldBlack, e.PageBounds.Width - 100 - marginWidth, (marginWidth / 2) + marginHeight, 100, 22);
            //e.Graphics.DrawImage(Properties.Resources.logo, (e.PageBounds.Width - (2 * marginWidth) - 125) / 2, (marginWidth / 2) + marginHeight, 250, 80);
            e.Graphics.DrawLine(Pens.Black, marginWidth, e.PageBounds.Height - (2 * marginWidth) + marginHeight, e.PageBounds.Width - marginWidth, e.PageBounds.Height - (2 * marginWidth));
            e.Graphics.DrawImage(Properties.Resources.igoldBlack, e.PageBounds.Width - 100 - marginWidth, e.PageBounds.Height - (2 * marginWidth) + 10 + marginHeight, 100, 22);
            e.Graphics.DrawString(strR, ff, Brushes.Black, e.PageBounds.Width - 103 - marginWidth - sStrR.Width, e.PageBounds.Height - (2 * marginWidth) + (marginWidth / 2) + marginHeight);
            e.Graphics.DrawLine(Pens.Black, marginWidth, 98 + marginHeight, e.PageBounds.Width - marginWidth, 98 + marginHeight);
            e.Graphics.DrawLine(Pens.Black, marginWidth, 97 + marginHeight, e.PageBounds.Width - marginWidth, 97 + marginHeight);
        }

        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!check())
                {
                    int rowIndex = dataGridView1.CurrentCell.RowIndex;
                    dataGridView1.Rows.RemoveAt(rowIndex);
                    totalsCalc();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deletePay_Click(object sender, EventArgs e)
        {
            try
            {
                if (!check())
                {
                    int rowIndex3 = dataGridView3.CurrentCell.RowIndex;
                    dataGridView3.Rows.RemoveAt(rowIndex3);
                    calc_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label2.Visible = true;
                printNo.Visible = true;
            }
            else
            {
                label2.Visible = false;
                printNo.Visible = false;
            }
        }

        private void customerNameTxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridView1.Select();
                dataGridView1.Focus();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save_Click(sender, e);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updateBill_Click(sender, e);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printBill_Click(sender, e);
        }

        private void dataGridView7_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress -= new KeyPressEventHandler(dataGridView1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(dataGridView11_KeyPress);
                if (dataGridView7.CurrentCell.ColumnIndex == 0)
                {
                    var ctl = e.Control as DataGridViewTextBoxEditingControl;
                    if (ctl == null)
                    {
                        return;
                    }
                    ctl.KeyUp -= dataGridView7_KeyUp;
                    ctl.KeyUp += new KeyEventHandler(dataGridView7_KeyUp);
                }
                else if (dataGridView7.CurrentCell.ColumnIndex == 1 || dataGridView7.CurrentCell.ColumnIndex == 2 || dataGridView7.CurrentCell.ColumnIndex == 3)
                {
                    e.Control.KeyPress += new KeyPressEventHandler(dataGridView11_KeyPress);
                }
                else if (dataGridView7.CurrentCell.ColumnIndex == 4) //Desired Column
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(dataGridView1_KeyPress);
                    }
                }
            }
            catch { }
        }

        private void dataGridView7_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView7.CurrentCell.ColumnIndex == 0)
            {
                var box = sender as TextBox;
                if (box == null)
                {
                    return;
                }
                textBox7.Text = box.Text;
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress -= new KeyPressEventHandler(dataGridView1_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(dataGridView11_KeyPress);
                if (dataGridView1.CurrentCell.ColumnIndex == 1)
                {
                    var ctl = e.Control as DataGridViewTextBoxEditingControl;
                    if (ctl == null)
                    {
                        return;
                    }
                    ctl.KeyUp -= dataGridView1_KeyUp;
                    ctl.KeyUp += new KeyEventHandler(dataGridView1_KeyUp);
                }
                if (dataGridView1.CurrentCell.ColumnIndex == 2 || dataGridView1.CurrentCell.ColumnIndex == 5)
                {
                    e.Control.KeyPress += new KeyPressEventHandler(dataGridView11_KeyPress);
                }
                if (dataGridView1.CurrentCell.ColumnIndex == 0 || dataGridView1.CurrentCell.ColumnIndex == 3 || dataGridView1.CurrentCell.ColumnIndex == 4 || dataGridView1.CurrentCell.ColumnIndex == 6) //Desired Column
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(dataGridView1_KeyPress);
                    }
                }
            }
            catch { }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 1)
            {
                var box = sender as TextBox;
                if (box == null)
                {
                    return;
                }
                textBox1.Text = box.Text;
                var columnRectangle = dataGridView1.GetColumnDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, true);
                var rowRectangle = dataGridView1.GetRowDisplayRectangle(dataGridView1.CurrentCell.RowIndex, true);
                listBox1.Location = new Point(dataGridView1.Location.X + columnRectangle.X, rowRectangle.Y + rowRectangle.Height);
            }
        }

        private void dataGridView5_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                e.Control.KeyPress -= new KeyPressEventHandler(dataGridView11_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(dataGridView1_KeyPress);
                if (dataGridView5.CurrentCell.ColumnIndex == 0)
                {
                    TextBox tx = e.Control as TextBox;
                    if (tx != null)
                    {
                        tx.AutoCompleteMode = AutoCompleteMode.Suggest;
                        tx.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        AutoCompleteStringCollection sc = new AutoCompleteStringCollection();
                        add5_items(sc);
                        tx.AutoCompleteCustomSource = sc;
                    }
                }
                else if (dataGridView5.CurrentCell.ColumnIndex == 1 || dataGridView5.CurrentCell.ColumnIndex == 2 || dataGridView5.CurrentCell.ColumnIndex == 3)
                {
                    e.Control.KeyPress += new KeyPressEventHandler(dataGridView11_KeyPress);
                }
                else if (dataGridView5.CurrentCell.ColumnIndex == 4) //Desired Column
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress += new KeyPressEventHandler(dataGridView1_KeyPress);
                    }
                }
                else if (dataGridView5.CurrentCell.ColumnIndex == 5)
                {
                    TextBox tx = e.Control as TextBox;
                    tx.AutoCompleteMode = AutoCompleteMode.Suggest;
                    tx.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    tx.AutoCompleteCustomSource = null;
                }
            }
            catch { }
        }

        private void daily_Resize(object sender, EventArgs e)
        {
            //width(size)
            panel8.Width = panel2.Width - 3;
            panel1.Width = panel2.Width - 3;
            panel10.Width = panel2.Width - 3;
            panel6.Width = panel2.Width - 3;
            //height(size)
            panel8.Height = Convert.ToInt32(panel2.Height * 0.0778);
            panel1.Height = Convert.ToInt32(panel2.Height * 0.26);
            panel10.Height = Convert.ToInt32(panel2.Height * 0.448);
            panel6.Height = Convert.ToInt32(panel2.Height * 0.21);
            //panel6.Height = panel2.Height - panel1.Height - panel8.Height - panel10.Height-5;
            //location(Y)
            panel8.Location = new Point(0, 0);
            panel1.Location = new Point(0, panel8.Location.Y + panel8.Height + 1);
            panel10.Location = new Point(0, panel1.Location.Y + panel1.Height);
            panel6.Location = new Point(0, panel10.Location.Y + panel10.Height);
            //DGV7
            dataGridView7.Height = Convert.ToInt32(panel1.Height * 0.325) - 8;
            dataGridView7.Width = panel7.Width + panel9.Width - 1;
            dataGridView7.Location = new Point(panel7.Location.X, panel7.Location.Y - dataGridView7.Height);
            //DGV3
            dataGridView3.Width = Convert.ToInt32(panel10.Width * 0.30);
            dataGridView3.Height = panel10.Height;
            dataGridView3.Location = new Point(panel10.Location.X + 3, 0);
            //DGV1
            dataGridView1.Width = Convert.ToInt32(panel10.Width * 0.70);
            dataGridView1.Height = panel10.Height;
            dataGridView1.Location = new Point(dataGridView3.Location.X + dataGridView3.Width, 0);
            //panel11
            panel11.Width = Convert.ToInt32(panel6.Width * 0.30);
            panel11.Height = panel6.Height;
            panel11.Location = new Point(panel6.Location.X + 3, 0);
            //DGV5
            dataGridView5.Width = Convert.ToInt32(panel6.Width * 0.70);
            dataGridView5.Height = panel6.Height;
            dataGridView5.Location = new Point(panel11.Location.X + panel11.Width, 0);
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void dataGridView11_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != (char)8 && e.KeyChar != (char)12 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
            if ((sender as TextBox).Text.Contains(".") && (sender as TextBox).Text.Length > (sender as TextBox).Text.IndexOf('.') + 2 && e.KeyChar != (char)8 && e.KeyChar != (char)12 && e.KeyChar != (char)46)
            {
                e.Handled = true;
            }
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {                
                e.Handled = true;
            }


        }     

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentCell.Selected = true;
            dataGridView1.BeginEdit(true);
        }

        void textBox1_TextChanged(object sender, System.EventArgs e)
        {
            listBox1.Items.Clear();
            if (textBox1.Text.Length == 0)
            {
                listBox1.Visible = false;
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
            dataGridView1.CurrentCell.Value = listBox1.Items[listBox1.SelectedIndex].ToString();
            listBox1.Visible = false;
        }

        void listBox1_LostFocus(object sender, System.EventArgs e)
        {
            listBox1.Visible = false;
        }

        void textBox7_TextChanged(object sender, System.EventArgs e)
        {
            listBox7.Items.Clear();
            if (textBox7.Text.Length == 0)
            {
                listBox7.Visible = false;
                return;
            }

            foreach (String s in textBox7.AutoCompleteCustomSource)
            {
                if (s.Contains(textBox7.Text))
                {
                    listBox7.Items.Add(s);
                    listBox7.Visible = true;
                }
            }
        }

        void listBox7_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            dataGridView7.Rows[0].Cells[0].Value = listBox7.Items[listBox7.SelectedIndex].ToString();
            listBox7.Visible = false;
            name = dataGridView7.Rows[0].Cells[0].Value.ToString();
            availableName();
        }

        void listBox7_LostFocus(object sender, System.EventArgs e)
        {
            listBox7.Visible = false;
        }


        private void dataGridView7_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
