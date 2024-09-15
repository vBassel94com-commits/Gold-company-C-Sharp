using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace iGOLD
{
    public partial class outcome : Form
    {
        public outcome()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        string details = "";
        private const int cGrip = 16;
        private const int cCaption = 32;
        SqlConnection con = new SqlConnection(GlobalVar.dataBaseLocation);
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
            DialogResult result = MessageBox.Show("هل أنت متأكد بالخروج من تسجيل دفعة مصروف", "الخروج", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
            }
            else
            {
                if (GlobalVar.status_value) { MessageBox.Show("سيتم اغلاق البرنامج"); GlobalVar.status_value = false; }
                main.setBackUp();
                Application.Exit();
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
            if (W == 521 && H == 462)
            {
                this.SetDesktopLocation(0, 0);
                this.Size = new Size(screenW, screenH-40);
                close.Location = new Point(0, 0);
                maximize.Location = new Point(32, 0);
                minimize.Location = new Point(64, 0);
                bar.Size = new Size(this.Width-60, 37);
                bar.Location = new Point(0, 0);
                yellowPanel.Size = new Size(screenW, 3);
                yellowPanel.Location = new Point(0, 32);
                GlobalVar.outcomeisMainMax = true;
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
                GlobalVar.outcomeisMainMax = false;
                this.Close();
                outcome form = new outcome();
                form.Show();
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

        private void cancel_Click(object sender, EventArgs e)
        {
            detailsTxtbox.Text = "اكتب ملاحظات عن المصروف";
            cashTxtbox.Text = "0";
        }

        private void bunifuMaterialTextbox4_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cashTxtbox.Text))
            {
                cashTxtbox.Text = "0";
            }
            cashTxtbox.LineIdleColor = Color.MediumSpringGreen;
        }

        private void bunifuMaterialTextbox4_MouseEnter(object sender, EventArgs e)
        {

            if (Equals(cashTxtbox.Text, "0"))
            {
                cashTxtbox.Text = "";
            }
        }

        private void BunifuMaterialTextbox4_OnValueChanged(object sender, EventArgs e)
        {
            if (cashTxtbox.Text.Trim() != "")

            {

                try

                {

                    decimal i = Convert.ToDecimal(cashTxtbox.Text.Trim());

                }

                catch
                {

                    MessageBox.Show("يرجى ادخال أرقام فقط");

                    cashTxtbox.Text = "0";

                }

            }
        }

        private void detailsTxtBox1_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(detailsTxtbox.Text))
            {
                detailsTxtbox.Text = "اكتب ملاحظات عن المصروف";
            }
        }

        private void detailsTxtBox1_MouseEnter(object sender, EventArgs e)
        {

            if (Equals(detailsTxtbox.Text, "اكتب ملاحظات عن المصروف"))
            {
                detailsTxtbox.Text = "";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = String.Format("{0:HH : mm}", DateTime.Now);
            time1.Text = String.Format("{0:tt}", DateTime.Now);
            timer1.Start();
        }

        private void outcome_Load(object sender, EventArgs e)
        {
            if (main.IsUsbDeviceConnected(GlobalVar.pid))
            { try { 
            if (!GlobalVar.status_value)
            {
                MessageBox.Show("لم يتم افتتاح البرنامج لليوم , يرجى الضغط على زر الافتتاح");
                main_iGOLD form = new main_iGOLD();
                form.Show();
                this.Close();
            }
            else
            {if (GlobalVar.outcomeisMainMax)
                    {
                        maximize_Click(sender, e);
                    }
                        dateTimePicker1.Value = DateTime.Now.Date;
                timer1.Start();
                date.Text = String.Format("{0: yyyy / MM / dd}", DateTime.Now);
                day.Text = String.Format("{0: :dddd}", DateTime.Now);
                time.Text = String.Format("{0:HH : mm}", DateTime.Now);
                time1.Text = String.Format("{0:tt}", DateTime.Now);
                paymentdb_Class cus = new paymentdb_Class();
                IDTxtBox.Text = idLabel(cus.getPaymentsCount());
                SqlConnection con = new SqlConnection();
                con.ConnectionString = GlobalVar.dataBaseLocation;
                SqlDataAdapter adapter = new SqlDataAdapter();
                bool a = false;
                try
                {
                    con.Open();
                    SqlDataReader DataRdr;
                    SqlCommand cmd = new SqlCommand("select customerName from customers where (customerName like @m and userId = "+ GlobalVar.id.ToString()+")", con);
                    cmd.Parameters.Add(new SqlParameter("@m", "مصروف"));
                    cmd.ExecuteNonQuery();
                    DataRdr = cmd.ExecuteReader();
                    while (DataRdr.Read())
                    {
                        if (Equals(DataRdr.GetString(0), "مصروف"))
                        { a = true; }
                    }
                    con.Close();
                    if (!a)
                    {
                        MessageBox.Show(" قم باضافة صائغ باسم (مصروف) في نافذة اضافة صائغ");
                        this.Close();
                        System.Threading.Thread.Sleep(1000);
                        customerEntry1 form = new customerEntry1();
                        form.Show();
                        form.nameTxtbox.Text = "مصروف";
                        form.mobileTxtbox.Text = "0900000000";
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
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

        private string idLabel(string i)
        {

            string str = "";
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

        private void save_Click(object sender, EventArgs e)
        {
            try { 
            save_Click1();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void save_Click1()
        {
            try { 
                if (Equals(cashTxtbox.Text, "0"))
                {

                    MessageBox.Show("!!.. يرجى ادخال مقدار دفعة المصروف");

                }

                else
                {
                details = detailsTxtbox.Text;

                if (details == "اكتب ملاحظات عن المصروف" || details.Trim() == "")
                {
                      details = "المصروف يوم " + dateTimePicker1.Value.Date.ToString().Substring(0,10);
                }
                decimal cash = Convert.ToDecimal(cashTxtbox.Text.Trim());
                paymentdb_Class cus = new paymentdb_Class();
                cus.paymentCash1 = cash;
                cus.paymentNotice1 = details;
                cus.paymentTypeInt1 = 1;
                cus.paymentTypeString1 = Label.Text;
                cus.paymentDateTime1 = dateTimePicker1.Value.Date;
                cus.payment211 = 0;
                cus.payment181 = 0;
                cus.payment141 = 0;
                cus.realPayment211 = 0;
                cus.realPayment181 = 0;
                cus.realPayment141 = 0;
                cus.realPaymentCash1 = cash;
                cus.item14Id1 = 0;
                cus.item18Id1 = 0;
                cus.item21Id1 = 0;
                cus.item14Count1 = 0;
                cus.item18Count1 = 0;
                cus.item21Count1 = 0;
                cus.customerName1 = "مصروف";
                cus.customerId1 = cus.GetCustomerid();
                cus.userId1 = GlobalVar.id;
                cus.paymentNo1 = "000";
                string c1 = cus.insertPayment();
                if (c1 == " تم الحفظ ")
                {
                    Itemdb_Class itm = new Itemdb_Class();
                    itm.cash1 = cash;
                    c1 = itm.subCashFromFonding();
                    if (c1 == " تم الحفظ ")
                    {
                        MessageBox.Show(cus.addPayment());
                        IDTxtBox.Text = idLabel(cus.getPaymentsCount());
                        detailsTxtbox.Text = "اكتب ملاحظات عن المصروف";
                        cashTxtbox.Text = "0";
                        dateTimePicker1.Value = DateTime.Now.Date;
                    }
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
            return (Equals(cashTxtbox.Text, "0") && Equals(detailsTxtbox.Text, "اكتب ملاحظات عن المصروف"));
        }

        private void outcome_Resize(object sender, EventArgs e)
        {
            close.Location = new Point(0, 0);
            maximize.Location = new Point(32, 0);
            minimize.Location = new Point(64, 0);
            bar.Size = new Size(this.Width - 60, 37);
            bar.Location = new Point(0, 0);
            yellowPanel.Size = new Size(this.Width, 3);
            yellowPanel.Location = new Point(0, 32);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ادخال المصروف", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ادخال المصروف", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ادخال المصروف", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ادخال المصروف", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ادخال المصروف", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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
                GlobalVar.paymentType = true;
                form.Show();
            }
            else
            {

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ادخال المصروف", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ادخال المصروف", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ادخال المصروف", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ادخال المصروف", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على ادخال المصروف", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

        private void cashTxtbox_Leave(object sender, EventArgs e)
        {
            customerPayment f = new customerPayment();
            f.addCashComma(cashTxtbox);
        }

        private void cashTxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }


    }
}   
