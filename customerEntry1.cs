using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;


namespace iGOLD
{
    public partial class customerEntry1 : Form
    {
        public customerEntry1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private const int cGrip = 16;
        private const int cCaption = 32;
        Customerdb_Class cus = new Customerdb_Class();
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
            Maximize_Click1();
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

        private void menu_MouseHover(object sender, EventArgs e)
        {
            menu.BackColor = GlobalVar.menuHoverColor;
        }

        private void menu_MouseLeave(object sender, EventArgs e)
        {
            menu.BackColor = GlobalVar.leaveColor;
        }

        private void maximize_Click(object sender, EventArgs e)
        {
            Maximize_Click1();
        }

        public void Maximize_Click1()
        {
            GlobalVar.mob = mobileTxtbox.Text;
            GlobalVar.name = nameTxtbox.Text;
            int W = this.Width;
            int H = this.Height;
            int screenW = Screen.PrimaryScreen.Bounds.Width;
            int screenH = Screen.PrimaryScreen.Bounds.Height;
            if (W == 661 && H == 460)
            {
                GlobalVar.customerEntryisMainMax = true;
                this.SetDesktopLocation(0, 0);
                this.Size = new Size(screenW, screenH-40);
                close.Location = new Point(2, 0);
                maximize.Location = new Point(34, 0);
                minimize.Location = new Point(66, 0);
                bar.Size = new Size(64, 37);
                bar.Location = new Point(0, 0);
                yellowPanel.Size = new Size(screenW, 3);
                yellowPanel.Location = new Point(0, 32);
                GlobalVar.customerEntryisMainMax = true;
                panel2.Width = this.Width - 41;
                panel2.Height = this.Height - 37;
                panelSlide.Height = this.Height - 37;
                main_Gold1.Height = panelSlide.Height / 11;
                customerEntry11.Height = panelSlide.Height / 11;
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
                customerEntry1 form = new customerEntry1();
                form.Show();
                GlobalVar.customerEntryisMainMax = false;
                form.nameTxtbox.Text = GlobalVar.name;
                form.mobileTxtbox.Text = GlobalVar.mob;
                panel2.Width = this.Width - 41;
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (main.IsConnected())
            {
                try
                {
                    save_Click1();
                    MessageBox.Show("تم الحفظ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void save_Click1()
        {
            if (Equals(nameTxtbox.Text, "أدخل الاسم بالكامل") )//&& Equals(mobileTxtbox.Text, ""))
            {
                MessageBox.Show("!!.. يرجى ادخال بيانات الصائغ");
            }
            else if (Equals(nameTxtbox.Text, "أدخل الاسم بالكامل"))
            {
                MessageBox.Show("!!.. يرجى ادخال اسم الصائغ");
            }
            else
            {
                string customerName = nameTxtbox.Text;
                string mobileNo = mobileTxtbox.Text;
                cus.customerName1 = customerName;
                cus.mobile1 = mobileNo;
                if (cus.CheckCustomerNameExist() != "true")
                {
                    MessageBox.Show("الاسم  " + customerName + "  موجود مسبقاً و رقم موبايله  " + cus.getMobileOfName() + Environment.NewLine + Environment.NewLine + "يرجى التأكد من اسم الزبون الذي تم ادخاله");
                }
                else if (cus.CheckCustomerNameExist() == "true" )//&& (cus.checkCustomerMobileExist() == "true" || mobileNo == "09" ) )
                {
                    cus.insertCustomer();
                    IDTxtBox.Text = idLabel(cus.getCustomerCount());
                    nameTxtbox.Text = "أدخل الاسم بالكامل";
                    mobileTxtbox.Text = "09";
                    nameTxtbox.Select();
                    nameTxtbox.Focus();                      
                }
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("هل أنت متأكد بالخروج من ادخال بيانات صائغ", "الخروج", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
            }
            else
            {
                if (GlobalVar.status_value)
                {
                    MessageBox.Show("سيتم اغلاق البرنامج"); GlobalVar.status_value = false;
                }
                main.setBackUp();
                Application.Exit();
            }
        }
        //       
        private void cancel_Click(object sender, EventArgs e)
        {
            nameTxtbox.Text = "أدخل الاسم بالكامل";
            mobileTxtbox.Text = "09";
        }
        //
        private void nameTxtbox_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTxtbox.Text))
            {
                nameTxtbox.Text = "أدخل الاسم بالكامل";
            }

        }

        private void nameTxtbox_MouseEnter(object sender, EventArgs e)
        {

            if (Equals(nameTxtbox.Text, "أدخل الاسم بالكامل"))
            {
                nameTxtbox.Text = "";
            }
        }
        //
        private void mobileTxtbox_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(mobileTxtbox.Text))
            {
                mobileTxtbox.Text = "09";
            }
            mobileTxtbox.LineIdleColor = GlobalVar.menuHoverColor;
        }

        private void mobileTxtbox_MouseEnter(object sender, EventArgs e)
        {

            if (Equals(mobileTxtbox.Text, "09"))
            {
                mobileTxtbox.Text = "";
            }
        }

        private void customerEntry_Load(object sender, EventArgs e)
        {
				if (GlobalVar.customerEntryisMainMax)
                {
                    maximize_Click(sender, e);
                }
                try
                {
                    GlobalVar.customerEntryisMainMax = false;
                    nameTxtbox.AutoCompleteCustomSource = loadNames();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
		}

        public AutoCompleteStringCollection loadNames()
        {
            Customerdb_Class cus = new Customerdb_Class();
            IDTxtBox.Text = idLabel(cus.getCustomerCount());
            SqlConnection con = new SqlConnection();
            con.ConnectionString = GlobalVar.dataBaseLocation;
            con.Open();
            SqlDataReader DataRdr;
            SqlCommand cmd = new SqlCommand("select customerName from customers where userId =" + GlobalVar.id.ToString(), con);
            cmd.ExecuteNonQuery();
            DataRdr = cmd.ExecuteReader();
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            while (DataRdr.Read())
            {
                names.Add(DataRdr.GetString(0));
            }
            con.Close();
            return names;
        }

        private bool check()
        {
            return (Equals(nameTxtbox.Text, "أدخل الاسم بالكامل") && Equals(mobileTxtbox.Text, "09"));
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
            finally { }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            this.Close();
            editCustomer form = new editCustomer();
            form.Show();
        }

        private void customerEntry_Resize(object sender, EventArgs e)
        {
            close.Location = new Point(4, 0);
            maximize.Location = new Point(38, 0);
            minimize.Location = new Point(72, 0);
            bar.Size = new Size(this.Width - 72, 37);
            bar.Location = new Point(0, 0);
            yellowPanel.Size = new Size(this.Width, 3);
            yellowPanel.Location = new Point(0, 32);
            panelSlide.Size = new Size(panelSlide.Size.Width, this.Height - 41);
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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات الصائغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

        private void mobileTxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
           e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void nameTxtbox_Leave(object sender, EventArgs e)
        {
            string customerName = nameTxtbox.Text;

            Customerdb_Class cus = new Customerdb_Class();
            cus.customerName1 = customerName;

            if (cus.CheckCustomerNameExist() == "true")
            {
                label1.Text = "الاسم متوفر";
                label1.ForeColor = Color.MediumSpringGreen;
            }
            else
            {
                label1.Text = "الاسم غير متوفر , يرجى اختيار اسم آخر";
                label1.ForeColor = Color.LightSalmon;
            }

        }

        private void nameTxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                mobileTxtbox.Select();
                mobileTxtbox.Focus();
            }
        }

        private void mobileTxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                save_Click(sender, e);
            }
        }

    }
}
