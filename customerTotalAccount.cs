using System;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace iGOLD
{
    public partial class customerTotalAccount : Form
    {
        public customerTotalAccount()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private const int cGrip = 16;
        private const int cCaption = 32;
        dailyPayment f = new dailyPayment();
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
            DialogResult result = MessageBox.Show("هل أنت متأكد بالخروج من حساب الصائغ", "الخروج", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
            }
            else
            {
                if (GlobalVar.status_value)
                { MessageBox.Show("سيتم اغلاق البرنامج"); GlobalVar.status_value = false; }
                main.setBackUp();
                Application.Exit();
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

        public void maximize_Click(object sender, EventArgs e)
        {
            Maximize_Click1();
        }

        public void Maximize_Click1()
        {
            GlobalVar.name = customerNameTxtbox.Text;
            GlobalVar.mob = mobileTxtBox.Text;
            int W = this.Width;
            int H = this.Height;
            int screenW = Screen.PrimaryScreen.Bounds.Width;
            int screenH = Screen.PrimaryScreen.Bounds.Height;
            if (W == 800 && H == 577)
            {
                this.SetDesktopLocation(0, 0);
                this.Size = new Size(screenW, screenH-40);
                close.Location = new Point(0, 0);
                maximize.Location = new Point(36, 0);
                minimize.Location = new Point(72, 0);
                bar.Size = new Size(screenW - 72, 37);
                bar.Location = new Point(0, 0);
                yellowPanel.Size = new Size(screenW, 3);
                yellowPanel.Location = new Point(0, 32);
                GlobalVar.customerTotalAccountisMainMax = true;
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
                GlobalVar.customerTotalAccountisMainMax = false;
                this.Close();
                customerTotalAccount form = new customerTotalAccount();
                form.Show();
                form.customerNameTxtbox.Text = GlobalVar.name;
                form.mobileTxtBox.Text = GlobalVar.mob;
                panel2.Width = this.Width - 41;
            }
        }

        private void customerDetailsAccountBtn_MouseEnter(object sender, EventArgs e)
        {
            customerDetailsAccountBtn.ForeColor = Color.FromArgb(254, 199, 32);
            customerDetailsAccountBtn.BackColor = Color.FromArgb(35, 38, 32);
        }

        private void customerDetailsAccountBtn_MouseLeave(object sender, EventArgs e)
        {
            customerDetailsAccountBtn.BackColor = Color.FromArgb(254, 199, 32);
            customerDetailsAccountBtn.ForeColor = Color.FromArgb(35, 38, 32);
        }

        private void customerDetailsAccountBtn_Click(object sender, EventArgs e)
        {
            customerDetailsAccount form = new customerDetailsAccount();
            form.Show();
            form.nameTxtBox.Text = customerNameTxtbox.Text;
            form.mobileTxtBox.Text = mobileTxtBox.Text;
            this.Hide();
        }

        private void customerTotalAccount_Resize(object sender, EventArgs e)
        {
            close.Location = new Point(0, 0);
            maximize.Location = new Point(36, 0);
            minimize.Location = new Point(72, 0);
            bar.Size = new Size(this.Width - 72, 37);
            bar.Location = new Point(0, 0);
            yellowPanel.Size = new Size(this.Width, 3);
            yellowPanel.Location = new Point(0, 32);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = String.Format("{0:HH : mm}", DateTime.Now);
            time1.Text = String.Format("{0:tt}", DateTime.Now);
            timer1.Start();
        }

        private void customerTotalAccount_Load(object sender, EventArgs e)
        {
            if (main.IsUsbDeviceConnected(GlobalVar.pid))
            {
                try
                {
                    if (GlobalVar.customerTotalAccountisMainMax)
                    {
                        maximize_Click(sender, e);
                    }
                    timer1.Start();
                    date.Text = String.Format("{0: yyyy / MM / dd}", DateTime.Now);
                    day.Text = String.Format("{0: :dddd}", DateTime.Now);
                    time.Text = String.Format("{0:HH : mm}", DateTime.Now);
                    time1.Text = String.Format("{0:tt}", DateTime.Now);
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = GlobalVar.dataBaseLocation;
                    con.Open();
                    SqlDataReader DataRdr;
                    SqlCommand cmd = new SqlCommand("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);
                    cmd.ExecuteNonQuery();
                    DataRdr = cmd.ExecuteReader();
                    AutoCompleteStringCollection names = new AutoCompleteStringCollection();
                    while (DataRdr.Read())
                    {
                        names.Add(DataRdr.GetString(0));
                    }
                    customerNameTxtbox.AutoCompleteCustomSource = names;
                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { }
            }
            else
            {
                MessageBox.Show("ان فلاش الحماية غير متصل بالكمبيوتر , يرجى التأكد من اتصال فلاش الحماية بالكمبيوتر" + Environment.NewLine + "ثم أعد تشغيل البرنامج");
                this.Close();
                main.Show();
            }
        }

        private void nameTxtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                mobileTxtBox.Text = "";
                meCashTxtbox.Text = "";
                forCashTxtbox.Text = "";
                meGold14Txtbox.Text = "";
                forGold14Txtbox.Text = "";
                meGold18Txtbox.Text = "";
                forGold18Txtbox.Text = "";
                meGold21Txtbox.Text = "";
                forGold21Txtbox.Text = "";
                availableName();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void availableName()
        {
            try
            {
                if (customerNameTxtbox.TextLength > 1)
                {
                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = GlobalVar.dataBaseLocation;
                    paymentdb_Class cus = new paymentdb_Class();
                    cus.customerName1 = customerNameTxtbox.Text;
                    int custmId = cus.GetCustomerid();
                    SqlCommand cmd = con.CreateCommand();
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT TotalCash , Total21 , Total18 , Total14 , customerMobile from customers where (customerName like  @name and userId = "+GlobalVar.id+")";
                    cmd.Parameters.Add(new SqlParameter("@name", "%" + customerNameTxtbox.Text + "%"));
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        if (customerNameTxtbox.Text == "راس مال")
                        {
                            meCashTxtbox.Text = Math.Abs(Convert.ToDecimal(dt.Rows[0][0].ToString())).ToString();
                            forCashTxtbox.Text = "";
                            meGold21Txtbox.Text = Math.Abs(Convert.ToDecimal(dt.Rows[0][1].ToString())).ToString();
                            forGold21Txtbox.Text = "";
                            meGold18Txtbox.Text = Math.Abs(Convert.ToDecimal(dt.Rows[0][2].ToString())).ToString();
                            forGold18Txtbox.Text = "";
                            meGold14Txtbox.Text = Math.Abs(Convert.ToDecimal(dt.Rows[0][3].ToString())).ToString();
                            forGold14Txtbox.Text = "";
                        }
                        else
                        {
                            if (Convert.ToDecimal(dt.Rows[0][0].ToString()) > 0)
                            {
                                meCashTxtbox.Text = dt.Rows[0][0].ToString();
                                forCashTxtbox.Text = "";
                            }
                            else
                            {
                                forCashTxtbox.Text = dt.Rows[0][0].ToString();
                                meCashTxtbox.Text = "";

                            }

                            if (Convert.ToDecimal(dt.Rows[0][1].ToString()) > 0)
                            {
                                meGold21Txtbox.Text = dt.Rows[0][1].ToString();
                                forGold21Txtbox.Text = "";
                            }
                            else
                            {
                                forGold21Txtbox.Text = dt.Rows[0][1].ToString();
                                meGold21Txtbox.Text = "";
                            }

                            if (Convert.ToDecimal(dt.Rows[0][2].ToString()) > 0)
                            {
                                meGold18Txtbox.Text = dt.Rows[0][2].ToString();
                                forGold18Txtbox.Text = "";
                            }
                            else
                            {
                                forGold18Txtbox.Text = dt.Rows[0][2].ToString();
                                meGold18Txtbox.Text = "";
                            }

                            if (Convert.ToDecimal(dt.Rows[0][3].ToString()) > 0)
                            {
                                meGold14Txtbox.Text = dt.Rows[0][3].ToString();
                                forGold14Txtbox.Text = "";
                            }
                            else
                            {
                                forGold14Txtbox.Text = dt.Rows[0][3].ToString();
                                meGold14Txtbox.Text = "";
                            }
                        }
                        mobileTxtBox.Text = dt.Rows[0][4].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool check()
        {
            return true;
        }

        private void save_Click1()
        { }

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

                DialogResult result = MessageBox.Show("هل تريد الخروج من حساب الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد الخروج من حساب الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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
                DialogResult result = MessageBox.Show("هل تريد الخروج من حساب الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);
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
                DialogResult result = MessageBox.Show("هل تريد الخروج من حساب الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);
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

                DialogResult result = MessageBox.Show("هل تريد الخروج من حساب الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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
                this.Close();
                customerPayment form = new customerPayment();
                GlobalVar.paymentType = true;
                form.Show();
            }
            else
            {

                DialogResult result = MessageBox.Show("هل تريد الخروج من حساب الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد الخروج من حساب الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد الخروج من حساب الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    save_Click1();
                }
                else if (result == DialogResult.No)
                {
                    this.Close();
                    detailsAccounting form = new detailsAccounting();
                    if (GlobalVar.detailsAccountingisMainMax == true)
                    {
                        form.Maximize_Click1();
                    }
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

                DialogResult result = MessageBox.Show("هل تريد الخروج من حساب الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد الخروج من حساب الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

        private void meCashTxtbox_OnValueChanged(object sender, EventArgs e)
        {
            f.addCashComma((Control)sender);
        }

        private void forCashTxtbox_OnValueChanged(object sender, EventArgs e)
        {
            f.addCashComma((Control)sender);
        }

        private void meGold21Txtbox_OnValueChanged(object sender, EventArgs e)
        {
            f.addComma((Control)sender);
        }

        private void meGold18Txtbox_OnValueChanged(object sender, EventArgs e)
        {
            f.addComma((Control)sender);
        }

        private void meGold14Txtbox_OnValueChanged(object sender, EventArgs e)
        {
            f.addComma((Control)sender);
        }

        private void forGold21Txtbox_OnValueChanged(object sender, EventArgs e)
        {
            f.addComma((Control)sender);
        }

        private void forGold18Txtbox_OnValueChanged(object sender, EventArgs e)
        {
            f.addComma((Control)sender);
        }

        private void forGold14Txtbox_OnValueChanged(object sender, EventArgs e)
        {
            f.addComma((Control)sender);
        }

    }
}
