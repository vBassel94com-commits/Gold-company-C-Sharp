using System;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;

namespace iGOLD
{
    public partial class newDb : Form
    {
        public newDb()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            ds.ReadXml("D:\\windows\\i\\o.xml");
        }
      
        private const int cGrip = 16;
        private const int cCaption = 32;     
        DataSet ds = new DataSet();
        DataTable dataT = new DataTable();
        DataTable dataT1 = new DataTable();
        DataTable dataT4 = new DataTable();
        DataTable dataT3 = new DataTable();
        Login log = new Login();
        bool m1 = false;
        bool m2 = false;
        bool m3 = false;
        bool m4 = false;
        bool m5 = false;
        SqlConnection con = new SqlConnection(GlobalVar.dataBaseLocation);

        int indexRow1;
        SqlCommand cmd;
        Customerdb_Class cus = new Customerdb_Class();
        Itemdb_Class itm = new Itemdb_Class();
        paymentdb_Class pay = new paymentdb_Class();
        main_iGOLD main = new main_iGOLD();
        customerPayment f = new customerPayment();
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
            DialogResult result = MessageBox.Show("   هل أنت متأكد بالخروج من اعدادت التحكم  ", "الخروج", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
            }
            else
            {
                if (GlobalVar.status_value)
                {
                    MessageBox.Show("سيتم اغلاق البرنامج");
                    GlobalVar.status_value = false;
                }
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
            if (W == 790 && H == 702)
            {
                this.SetDesktopLocation(0, 0);
                this.Size = new Size(screenW, screenH-40);
                close.Location = new Point(0, 0);
                maximize.Location = new Point(34, 0);
                minimize.Location = new Point(68, 0);
                bar.Size = new Size(screenW - 72, 37);
                bar.Location = new Point(0, 0);
                yellowPanel.Size = new Size(screenW, 3);
                yellowPanel.Location = new Point(0, 32);
                GlobalVar.newDBisMainMax = true;
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
                //dataGridView1.Width = (panel5.Width - 6) / 2;
                //dataGridView2.Width = (panel5.Width - 6) / 2;
                //dataGridView1.Location = new Point(panel5.Location.X + (panel5.Width/ 2), dataGridView1.Location.Y);
                //dataGridView2.Location = new Point(panel5.Location.X, dataGridView2.Location.Y);
                //dataGridView1.Columns[0].Width = 0;
               


            }
            else
            {
                GlobalVar.newDBisMainMax = false;
                this.Close();
                newDb form = new newDb();
                form.Show();
                panel2.Width = this.Width - 41;
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

                DialogResult result = MessageBox.Show("هل تريد الخروج من التحكم بقاعدة البيانات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد الخروج من التحكم بقاعدة البيانات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد الخروج من التحكم بقاعدة البيانات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد الخروج من التحكم بقاعدة البيانات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد الخروج من التحكم بقاعدة البيانات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد الخروج من التحكم بقاعدة البيانات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد الخروج من التحكم بقاعدة البيانات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد الخروج من التحكم بقاعدة البيانات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد الخروج من التحكم بقاعدة البيانات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد الخروج من التحكم بقاعدة البيانات", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text=="تدوير المصوغات")
            {
                comboBox2.Visible = true;
                //dataGridView2.DataSource = false;
                //dataGridView2.Columns.Clear();
                execute.Text = "حفظ للتدوير";
                //add.Text = "اضافة مصاغ";
                edit.Text = "تعديل مصاغ";
                delete.Text = "حذف المصاغ";
                bL1.Text = "بيانات المصاغ الأساسية";
                bL2.Text = "بيانات المصاغ المنقول";
                panel3.Visible = true;
                panel4.Visible = true;
                panel5.Visible = true;
                gold14Txtbox.Visible = false;
                newGold14Textbox.Visible=false;
                L4.Visible = false;
                L14.Visible = false;
                newL4.Visible = false;
                newL14.Visible = false;
                L1.Text = "عدد القطع";
                L2.Text = "وزن المصاغ";
                L3.Text = "أجور الصياغة";
                L11.Text = "قطعة";
                L12.Text = GlobalVar.gramLabel;
                L13.Text = GlobalVar.currencyLabel;
                newL1.Text = "عدد القطع";
                newL2.Text = "وزن المصاغ";
                newL3.Text = "أجور الصياغة";
                newL11.Text = "قطعة";
                newL12.Text = GlobalVar.gramLabel;
                newL13.Text = GlobalVar.currencyLabel;
                nameLabel.Text = "اسم المصاغ";
                nameLabel1.Text = "اسم المصاغ";
                itemNameTxtbox.Text = "";
                itemNameTxtbox1.Text = "";
                itemCountTxtbox.Text = "";
                newitemCountTxtbox.Text = "";
                itemWeightTxtbox.Text = "";
                newitemWeightTxtbox.Text = "";
                itemFeesTxtbox.Text = "";
                newitemFeesTxtbox.Text="";
                gold14Txtbox.Text = "";
                newGold14Textbox.Text = "";
                try
                {
                    loadItemsTable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (comboBox1.Text=="تدوير حسابات الصاغة")
            {
                comboBox2.Visible = true;
                //dataGridView2.DataSource = false;
                //dataGridView2.Columns.Clear();
                //add.Text = "اضافة بيانات صائغ";
                edit.Text = "تعديل بيانات صائغ";
                delete.Text = "حذف بيانات صائغ";
                bL1.Text = "بيانات الصائغ الأساسية";
                nameLabel1.Text = "الموبايل";
                bL2.Text = "بيانات الصائغ المنقول";
                panel3.Visible = true;
                panel4.Visible = true;
                panel5.Visible = true;
                execute.Text = "حفظ للتدوير";
                gold14Txtbox.Visible = false;
                newGold14Textbox.Visible = false;
                L4.Visible = true;
                L14.Visible = true;
                newL4.Visible = true;
                newL14.Visible = true;
                L1.Text = "الاجور";
                L2.Text = GlobalVar.gold21Label;
                L3.Text = GlobalVar.gold18Label;
                L11.Text = GlobalVar.currencyLabel;
                L12.Text = GlobalVar.gramLabel;
                L13.Text = GlobalVar.gramLabel;
                newL1.Text = "الاجور";
                newL2.Text = GlobalVar.gold21Label;
                newL3.Text = GlobalVar.gold18Label;
                newL11.Text = GlobalVar.currencyLabel;
                newL12.Text = GlobalVar.gramLabel;
                newL13.Text = GlobalVar.gramLabel;
                nameLabel.Text = "اسم الصائغ";
                itemNameTxtbox.Text = "";
                itemNameTxtbox1.Text = "";
                itemCountTxtbox.Text = "";
                newitemCountTxtbox.Text = "";
                itemWeightTxtbox.Text = "";
                newitemWeightTxtbox.Text = "";
                itemFeesTxtbox.Text = "";
                newitemFeesTxtbox.Text = "";
                gold14Txtbox.Text = "";
                newGold14Textbox.Text = "";
                gold14Txtbox.Visible = true;
                newGold14Textbox.Visible = true;

                try
                {
                    loadCustomersTable();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {

                comboBox2.Visible = false;
                panel3.Visible = false;
                panel4.Visible = false;
                panel5.Visible = false;
                execute.Text = "تنفيذ العملية";
            }
        }

        public void loadItemsTable()
		{
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = con.CreateCommand();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.CommandType = CommandType.Text;
                dataGridView1.DataSource = null;
                con.Open();
                cmd.CommandText = "select itemId,itemName,itemCount,itemWeight,itemFees from items where userId = '" + GlobalVar.id.ToString() + "'";
                cmd.ExecuteNonQuery();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
                dataGridView1.Columns[0].Width = 0;
                dataGridView1.Columns[0].HeaderText = "id";
                dataGridView1.Columns[1].HeaderText = "اسم المصاغ";
                dataGridView1.Columns[3].HeaderText = "وزن المصاغ الحالي";
                dataGridView1.Columns[2].HeaderText = "عدد قطع المصاغ الحالي";
                dataGridView1.Columns[4].HeaderText = "اجور صياغة الحالية";
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        public void loadCustomersTable()
        {
			try
			{	
                dataGridView1.DataSource = null;
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                paymentdb_Class cus = new paymentdb_Class();
                cus.customerName1 = itemNameTxtbox.Text;
                cmd.CommandText = "select customerName,customerMobile,TotalCash,Total21,Total18,total14 from customers where userId = " + GlobalVar.id.ToString();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
                dataGridView1.Columns[2].HeaderText = "الاجور";
                dataGridView1.Columns[1].HeaderText = "الموبايل";
                dataGridView1.Columns[3].HeaderText = GlobalVar.gold21Label;
                dataGridView1.Columns[4].HeaderText = GlobalVar.gold18Label;
                dataGridView1.Columns[5].HeaderText = GlobalVar.gold14Label;
                dataGridView1.Columns[0].HeaderText = "اسم الصائغ";
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow1 = e.RowIndex;
            if (indexRow1 != -1)
            {
                newitemCountTxtbox.Text = "";
                newitemFeesTxtbox.Text = "";
                newitemWeightTxtbox.Text = "";
                newGold14Textbox.Text="";
                if (comboBox1.Text=="تدوير حسابات الصاغة")
                {
                    DataGridViewRow row = dataGridView1.Rows[indexRow1];
                    itemNameTxtbox.Text = row.Cells[0].Value.ToString();
                    itemNameTxtbox1.Text = row.Cells[1].Value.ToString();
                    mobile.Text = row.Cells[1].Value.ToString();
                    itemCountTxtbox.Text = row.Cells[2].Value.ToString();
                    itemWeightTxtbox.Text = row.Cells[3].Value.ToString();
                    itemFeesTxtbox.Text = row.Cells[4].Value.ToString();
                    gold14Txtbox.Text = row.Cells[5].Value.ToString();
                }
                else if (comboBox1.Text == "تدوير المصوغات")
                {
                    DataGridViewRow row = dataGridView1.Rows[indexRow1];
                    itemNameTxtbox.Text = row.Cells[1].Value.ToString();
                    itemNameTxtbox1.Text = row.Cells[1].Value.ToString();
                    itemCountTxtbox.Text = row.Cells[2].Value.ToString();
                    itemWeightTxtbox.Text = row.Cells[3].Value.ToString();
                    itemFeesTxtbox.Text = row.Cells[4].Value.ToString();
                }                
            }
        }

        private void execute_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtServers = SmoApplication.EnumAvailableSqlServers(true);
            }
            catch
            {
                //MessageBox.Show(ex.Message);
            }
			dataT3.Clear();
            dataT4.Clear();
            if (comboBox1.Text == "حفظ نسخة احتياطية")
            {
                string path;
                FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
                folderBrowserDlg.ShowNewFolderButton = true;
                DialogResult dlgResult = folderBrowserDlg.ShowDialog();
                if (dlgResult.Equals(DialogResult.OK))
                {
                    txtPath.Text = folderBrowserDlg.SelectedPath;
                    Environment.SpecialFolder rootFolder = folderBrowserDlg.RootFolder ;
                    if (!txtPath.Text.EndsWith("\\"))
                    {
                        path = txtPath.Text + "\\";
                    }
                    else
                    {
                        path = txtPath.Text;
                    }
                    try
                    {

                        cmd = new SqlCommand("Backup DataBase Db1 To Disk= '" + path + DateTime.Now.ToString("dd_MM_yyyy") + "'", con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        File.Move(path + DateTime.Now.ToString("dd_MM_yyyy"), path + DateTime.Now.ToString("dd_MM_yyyy") + ".bak");
                        MessageBox.Show("تم حفظ نسخة احتياطية عن قاعدة البيانات بنجاح");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                        con.Close();
                    }
                }

            }
            else if (comboBox1.Text == "استعادة نسخة سابقة")
            {
                txtPath.Text = "";
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Database restore";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = dlg.FileName;
                }
                string database = con.Database.ToString();
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                try
                {
                    string sqlStmt2 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                    SqlCommand bu2 = new SqlCommand(sqlStmt2, con);
                    bu2.ExecuteNonQuery();

                    string sqlStmt3 = "USE MASTER RESTORE DATABASE [" + database + "] FROM DISK='" + txtPath.Text + "'WITH REPLACE;";
                    SqlCommand bu3 = new SqlCommand(sqlStmt3, con);
                    bu3.ExecuteNonQuery();

                    string sqlStmt4 = string.Format("ALTER DATABASE [" + database + "] SET MULTI_USER");
                    SqlCommand bu4 = new SqlCommand(sqlStmt4, con);
                    bu4.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("تم تحميل نسخة عن قاعدة البيانات بنجاح");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    con.Close();
                }
            }
            else if (comboBox1.Text == "اعادة ضبط قاعدة بيانات المستخدم")
            {
                try
                {
                    string a = itm.deleteAllUser();
                    pay.dateTim1 = DateTime.Today.Date;
                    pay.dailyCash1 = 0;
                    pay.daily211 = 0;
                    pay.daily181 = 0;
                    pay.daily141 = 0;
                    pay.firstBillId1 = 0;
                    pay.firstPaymentId1 = 0;
                    pay.lastBillId1 = 0;
                    pay.lastPaymentId1 = 0;
                    pay.cashAdd1 = 0;
                    pay.gold21Add1 = 0;
                    pay.gold18Add1 = 0;
                    pay.gold14Add1 = 0;
                    string a1 = pay.openDaily();

                    if (a == "تم افراغ قاعدة البيانات المستخدم")
                    {
                        cus.customerName1 = "راس مال";
                        cus.mobile1 = "0900000001";
                        cus.insertCustomer();
                        cus.customerName1 = "مصروف";
                        cus.mobile1 = "0900000000";
                        cus.insertCustomer();
                        itemEntry1 form = new itemEntry1();
                        form.itemNameTxtbox.Text = "خشر";
                        form.carat21.Checked = true;
                        form.itemCountTxtbox.Text = "0";
                        form.weightTxtbox.Text = "00";
                        form.feesTxtbox.Text = "0";
                        form.save_Click11();
                        System.Threading.Thread.Sleep(10);
                        form.itemNameTxtbox.Text = "خشر";
                        form.carat18.Checked = true;
                        form.itemCountTxtbox.Text = "0";
                        form.weightTxtbox.Text = "00";
                        form.feesTxtbox.Text = "0";
                        form.save_Click11();
                        System.Threading.Thread.Sleep(10);
                        form.itemNameTxtbox.Text = "خشر";
                        form.carat14.Checked = true;
                        form.itemCountTxtbox.Text = "0";
                        form.weightTxtbox.Text = "00";
                        form.feesTxtbox.Text = "0";
                        form.save_Click11();
                        MessageBox.Show("تم اعادة ضبط قاعدة بيانات المستخدم");
                    }
                    else
                    { execute_Click(sender, e); }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }
            else if (comboBox1.Text == "تدوير حسابات الصاغة")
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("راس مال"))
                        {
                            m1 = true;
                        }
                        else if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("مصروف"))
                        {
                            m2 = true;
                        }
                    }
                }
                if (m1 && m2)
                {
                    try
                    {
                        if (comboBox2.Text == "كامل العناصر مع الكميات")
                        {
                            if (dataGridView1.Rows.Count > 1)
                            {
                                string path = "D:\\U";
                                string fileName = "a.txt";
                                if (!Directory.Exists(path))
                                {
                                    DirectoryInfo di = Directory.CreateDirectory(path);
                                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                                    if (!File.Exists(path + "\\" + fileName))
                                    {
                                        File.Create(path + "\\" + fileName).Dispose();
                                        using (StreamWriter writer = new StreamWriter(path + "\\" + fileName, false))
                                        {

                                            for (int i = 0; i < (dataGridView1.Rows.Count - 1); i++)
                                            {
                                                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                                {
                                                    if (j == (dataGridView1.Columns.Count - 1))
                                                    {
                                                        writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString());
                                                    }
                                                    else
                                                    {
                                                        writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t|");
                                                    }
                                                }
                                                writer.WriteLine("");
                                            }
                                            writer.Close();
                                            MessageBox.Show("تم حفظ البيانات");

                                        }
                                    }
                                    else
                                    {
                                        File.Delete(path + "\\" + fileName);
                                        execute_Click(sender, e);
                                    }
                                }
                                else
                                {
                                    if (!File.Exists(path + "\\" + fileName))
                                    {
                                        File.Create(path + "\\" + fileName).Dispose();
                                        using (StreamWriter writer = new StreamWriter(path + "\\" + fileName, false))
                                        {
                                            for (int i = 0; i < (dataGridView1.Rows.Count - 1); i++)
                                            {
                                                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                                {
                                                    if (j == (dataGridView1.Columns.Count - 1))
                                                    {
                                                        writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString());
                                                    }
                                                    else
                                                    {
                                                        writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t|");
                                                    }
                                                }
                                                writer.WriteLine("");
                                            }
                                            writer.Close();
                                            MessageBox.Show("تم حفظ البيانات");
                                        }
                                    }
                                    else
                                    {
                                        File.Delete(path + "\\" + fileName);
                                        execute_Click(sender, e);
                                    }

                                }
                            }
                            else
                            {
                                MessageBox.Show("قاعدة البيانات فارغة");
                            }
                        }
                        else if (comboBox2.Text == "كامل العناصر بدون كميات")
                        {
                            if (dataGridView1.Rows.Count > 1)
                            {
                                string path = "D:\\U";
                                string fileName = "a.txt";
                                if (!Directory.Exists(path))
                                {
                                    DirectoryInfo di = Directory.CreateDirectory(path);
                                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                                    if (!File.Exists(path + "\\" + fileName))
                                    {
                                        File.Create(path + "\\" + fileName).Dispose();
                                        using (StreamWriter writer = new StreamWriter(path + "\\" + fileName, false))
                                        {
                                            for (int i = 0; i < (dataGridView1.Rows.Count - 1); i++)
                                            {
                                                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                                {
                                                    if (j == 0 || j == 1)
                                                    {

                                                        writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t|");

                                                    }
                                                    else
                                                    {
                                                        if (j == (dataGridView1.Columns.Count - 1))
                                                        {
                                                            writer.Write("\t0");
                                                        }
                                                        else
                                                        {
                                                            writer.Write("\t0\t|");
                                                        }
                                                    }
                                                }
                                                writer.WriteLine("");
                                            }
                                            writer.Close();
                                            MessageBox.Show("تم حفظ البيانات");
                                        }
                                    }
                                    else
                                    {
                                        File.Delete(path + "\\" + fileName);
                                        execute_Click(sender, e);
                                    }
                                }
                                else
                                {
                                    if (!File.Exists(path + "\\" + fileName))
                                    {
                                        File.Create(path + "\\" + fileName).Dispose();
                                        using (StreamWriter writer = new StreamWriter(path + "\\" + fileName, false))
                                        {
                                            for (int i = 0; i < (dataGridView1.Rows.Count - 1); i++)
                                            {
                                                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                                {
                                                    if (j == 0)
                                                    {
                                                        if (j == (dataGridView1.Columns.Count - 1))
                                                        {
                                                            writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString());
                                                        }
                                                        else
                                                        {
                                                            writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t|");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (j == (dataGridView1.Columns.Count - 1))
                                                        {
                                                            writer.Write("\t0");
                                                        }
                                                        else
                                                        {
                                                            writer.Write("\t0\t|");
                                                        }
                                                    }
                                                }
                                                writer.WriteLine("");
                                            }
                                            writer.Close();
                                            MessageBox.Show("تم حفظ البيانات");
                                        }
                                    }
                                    else
                                    {
                                        File.Delete(path + "\\" + fileName);
                                        execute_Click(sender, e);
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show("قاعدة البيانات فارغة");
                            }
                        }
                        else
                        {
                            MessageBox.Show("يرجى اختيار طريقة تدوير البيانات");
                        }
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                }
                else if (!m1 && m2)
                { MessageBox.Show("يجب اضافة الصائغ راس مال في قاعدة البيانات"); }
                else if (!m2 && m1)
                { MessageBox.Show("يجب اضافة الصائغ مصروف في قاعدة البيانات"); }
                else if (!m1 && !m2)
                { MessageBox.Show("يجب اضافة الصائغ راس مال و الصائغ مصروف في قاعدة البيانات"); }
            }
            else if (comboBox1.Text == "تدوير المصوغات")
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count - 1; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("خشر عيار 21"))
                        {
                            m3 = true;
                        }
                        else if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("خشر عيار 18"))
                        {
                            m4 = true;
                        }
                        else if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains("خشر عيار 14"))
                        {
                            m5 = true;
                        }
                    }
                }
                if (m3 && m4 && m5)
                {
                    try
                    {
                        if (comboBox2.Text == "كامل العناصر مع الكميات")
                        {
                            if (dataGridView1.Rows.Count > 1)
                            {
                                string path = "D:\\U";
                                string fileName = "b.txt";
                                if (!Directory.Exists(path))
                                {
                                    DirectoryInfo di = Directory.CreateDirectory(path);
                                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                                    if (!File.Exists(path + "\\" + fileName))
                                    {
                                        File.Create(path + "\\" + fileName).Dispose();
                                        using (StreamWriter writer = new StreamWriter(path + "\\" + fileName, false))
                                        {

                                            for (int i = 0; i < (dataGridView1.Rows.Count - 1); i++)
                                            {
                                                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                                {
                                                    if (j == (dataGridView1.Columns.Count - 1))
                                                    {
                                                        writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString());
                                                    }
                                                    else
                                                    {
                                                        writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t|");
                                                    }
                                                }
                                                writer.WriteLine("");
                                            }
                                            writer.Close();
                                            MessageBox.Show("تم حفظ البيانات");
                                        }
                                    }
                                    else
                                    {
                                        File.Delete(path + "\\" + fileName);
                                        execute_Click(sender, e);
                                    }
                                }
                                else
                                {
                                    if (!File.Exists(path + "\\" + fileName))
                                    {
                                        File.Create(path + "\\" + fileName).Dispose();
                                        using (StreamWriter writer = new StreamWriter(path + "\\" + fileName, false))
                                        {
                                            for (int i = 0; i < (dataGridView1.Rows.Count - 1); i++)
                                            {
                                                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                                {
                                                    if (j == (dataGridView1.Columns.Count - 1))
                                                    {
                                                        writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString());
                                                    }
                                                    else
                                                    {
                                                        writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t|");
                                                    }
                                                }
                                                writer.WriteLine("");
                                            }
                                            writer.Close();
                                            MessageBox.Show("تم حفظ البيانات");
                                        }
                                    }
                                    else
                                    {
                                        File.Delete(path + "\\" + fileName);
                                        execute_Click(sender, e);
                                    }

                                }
                            }
                            else
                            {
                                MessageBox.Show("قاعدة البيانات فارغة");
                            }
                        }
                        else if (comboBox2.Text == "كامل العناصر بدون كميات")
                        {
                            if (dataGridView1.Rows.Count > 1)
                            {
                                string path = "D:\\U";
                                string fileName = "b.txt";
                                if (!Directory.Exists(path))
                                {
                                    DirectoryInfo di = Directory.CreateDirectory(path);
                                    di.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                                    if (!File.Exists(path + "\\" + fileName))
                                    {
                                        File.Create(path + "\\" + fileName).Dispose();
                                        using (StreamWriter writer = new StreamWriter(path + "\\" + fileName, false))
                                        {
                                            if (m3 && m4 && m5)
                                            {
                                                for (int i = 0; i < (dataGridView1.Rows.Count - 1); i++)
                                                {
                                                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                                    {
                                                        if (j == 0 || j == 1)
                                                        {
                                                            if (j == (dataGridView1.Columns.Count - 1))
                                                            {
                                                                writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString());
                                                            }
                                                            else
                                                            {
                                                                writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t|");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (j == (dataGridView1.Columns.Count - 1))
                                                            {
                                                                writer.Write("\t0");
                                                            }
                                                            else
                                                            {
                                                                writer.Write("\t0\t|");
                                                            }
                                                        }
                                                    }
                                                    writer.WriteLine("");
                                                }
                                                writer.Close();
                                                MessageBox.Show("تم حفظ البيانات");
                                            }
                                            else if (!m3 && m4 && m5)
                                            { MessageBox.Show("يجب اضافة المصاغ خشر عيار 21 في قاعدة البيانات"); }
                                            else if (!m4 && m3 && m5)
                                            { MessageBox.Show("يجب اضافة المصاغ خشر عيار 18 في قاعدة البيانات"); }
                                            else if (!m5 && m4 && m3)
                                            { MessageBox.Show("يجب اضافة المصاغ خشر عيار 14 في قاعدة البيانات"); }
                                            else if (!m3 && !m4 && m5)
                                            { MessageBox.Show("يجب اضافة المصاغ خشر عيار 21 و المصاغ خشر عيار 18 في قاعدة البيانات"); }
                                            else if (!m4 && m3 && !m5)
                                            { MessageBox.Show("يجب اضافة المصاغ خشر عيار 18 و المصاغ خشر عيار 14 في قاعدة البيانات"); }
                                            else if (!m5 && m4 && !m3)
                                            { MessageBox.Show("يجب اضافة المصاغ خشر عيار 21 و المصاغ خشر عيار 14 في قاعدة البيانات"); }
                                            else if (!m5 && !m4 && !m3)
                                            { MessageBox.Show("يجب اضافة المصاغ خشر عيار 21 و المصاغ خشر عيار 18 و المصاغ خشر عيار 14 في قاعدة البيانات"); }

                                        }
                                    }
                                    else
                                    {
                                        File.Delete(path + "\\" + fileName);
                                        execute_Click(sender, e);
                                    }
                                }
                                else
                                {
                                    if (!File.Exists(path + "\\" + fileName))
                                    {
                                        File.Create(path + "\\" + fileName).Dispose();
                                        using (StreamWriter writer = new StreamWriter(path + "\\" + fileName, false))
                                        {
                                            for (int i = 0; i < (dataGridView1.Rows.Count - 1); i++)
                                            {
                                                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                                {
                                                    if (j == 0 || j == 1)
                                                    {
                                                        if (j == (dataGridView1.Columns.Count - 1))
                                                        {
                                                            writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString());
                                                        }
                                                        else
                                                        {
                                                            writer.Write("\t" + dataGridView1.Rows[i].Cells[j].Value.ToString() + "\t|");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (j == (dataGridView1.Columns.Count - 1))
                                                        {
                                                            writer.Write("\t0");
                                                        }
                                                        else
                                                        {
                                                            writer.Write("\t0\t|");
                                                        }
                                                    }
                                                }
                                                writer.WriteLine("");
                                            }
                                            writer.Close();
                                            MessageBox.Show("تم حفظ البيانات");
                                        }
                                    }
                                    else
                                    {
                                        File.Delete(path + "\\" + fileName);
                                        execute_Click(sender, e);
                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show("قاعدة البيانات فارغة");
                            }
                        }
                        else
                        {
                            MessageBox.Show("يرجى اختيار طريقة تدوير البيانات");
                        }
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                }

                else if (!m3 && m4 && m5)
                { MessageBox.Show("يجب اضافة المصاغ خشر عيار 21 في قاعدة البيانات"); }
                else if (!m4 && m3 && m5)
                { MessageBox.Show("يجب اضافة المصاغ خشر عيار 18 في قاعدة البيانات"); }
                else if (!m5 && m4 && m3)
                { MessageBox.Show("يجب اضافة المصاغ خشر عيار 14 في قاعدة البيانات"); }
                else if (!m3 && !m4 && m5)
                { MessageBox.Show("يجب اضافة المصاغ خشر عيار 21 و المصاغ خشر عيار 18 في قاعدة البيانات"); }
                else if (!m4 && m3 && !m5)
                { MessageBox.Show("يجب اضافة المصاغ خشر عيار 18 و المصاغ خشر عيار 14 في قاعدة البيانات"); }
                else if (!m5 && m4 && !m3)
                { MessageBox.Show("يجب اضافة المصاغ خشر عيار 21 و المصاغ خشر عيار 14 في قاعدة البيانات"); }
                else if (!m5 && !m4 && !m3)
                { MessageBox.Show("يجب اضافة المصاغ خشر عيار 21 و المصاغ خشر عيار 18 و المصاغ خشر عيار 14 في قاعدة البيانات"); }

            }
            else if (comboBox1.Text == "انشاء قاعدة البيانات المطلوبة")
            {
                try
                {
                    bool aa = File.Exists("D:\\U\a.txt");
                    bool bb = File.Exists("D:\\U\b.txt");
                    if (!aa && !bb)
                    {
                        string[] lines = File.ReadAllLines(@"D:\U\a.txt");
                        string[] lines1 = File.ReadAllLines(@"D:\U\b.txt");
                        string[] data;
                        string[] data1;
                        string a = itm.deleteAllUser();
                        pay.dateTim1 = DateTime.Today.Date;
                        pay.dailyCash1 = 0;
                        pay.daily211 = 0;
                        pay.daily181 = 0;
                        pay.daily141 = 0;
                        pay.firstBillId1 = 0;
                        pay.firstPaymentId1 = 0;
                        pay.lastBillId1 = 0;
                        pay.lastPaymentId1 = 0;
                        pay.cashAdd1 = 0;
                        pay.gold21Add1 = 0;
                        pay.gold18Add1 = 0;
                        pay.gold14Add1 = 0;
                        string a1 = pay.openDaily();
                        MessageBox.Show(a1);
                        //a = itm.deleteAll();
                        //if (a == "تم افراغ قاعدة البيانات المستخدم")
                        //{
                            for (int i = 0; i < lines.Length; i++)
                            {
                                data = lines[i].ToString().Split('|');
                                string[] row = new string[data.Length];
                                for (int j = 0; j < data.Length; j++)
                                {
                                    row[j] = data[j].Trim();
                                }
                                if (i == 0)
                                {
                                    dataT3.Columns.Add("اسم الصائغ", typeof(string));
                                    dataT3.Columns.Add("الموبايل", typeof(string));
                                    dataT3.Columns.Add("الاجور", typeof(string));
                                    dataT3.Columns.Add(GlobalVar.gold21Label, typeof(string));
                                    dataT3.Columns.Add(GlobalVar.gold18Label, typeof(string));
                                    dataT3.Columns.Add(GlobalVar.gold14Label, typeof(string));
                                }
                                dataT3.Rows.Add(row);
                                dataGridView3.DataSource = dataT3;
                            }
                            for (int i = 0; i < lines1.Length; i++)
                            {
                                data1 = lines1[i].ToString().Split('|');
                                string[] row1 = new string[data1.Length];
                                for (int j = 0; j < data1.Length; j++)
                                {
                                    row1[j] = data1[j].Trim();
                                }
                                if (i == 0)
                                {
                                    dataT4.Columns.Add("id", typeof(string));
                                    dataT4.Columns.Add("اسم المصاغ", typeof(string));
                                    dataT4.Columns.Add("عدد قطع المصاغ الحالي", typeof(string));
                                    dataT4.Columns.Add("وزن المصاغ الحالي", typeof(string));
                                    dataT4.Columns.Add("اجور صياغة الحالية", typeof(string));
                                }
                                dataT4.Rows.Add(row1);
                                dataGridView4.DataSource = dataT4;
                            }
                            MessageBox.Show(dataGridView3.RowCount.ToString() + " " + dataGridView4.RowCount.ToString());
                            for (int i = 0; i < (dataGridView3.RowCount - 1); i++)
                            {
                                cus.name1 = dataGridView3.Rows[i].Cells[0].Value.ToString();
                                cus.mobile1 = dataGridView3.Rows[i].Cells[1].Value.ToString();
                                cus.cashAdd1 = Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString());
                                cus.gold21Add1 = Convert.ToDecimal(dataGridView3.Rows[i].Cells[3].Value.ToString());
                                cus.gold18Add1 = Convert.ToDecimal(dataGridView3.Rows[i].Cells[4].Value.ToString());
                                cus.gold14Add1 = Convert.ToDecimal(dataGridView3.Rows[i].Cells[5].Value.ToString());
                                cus.insertCustomerFull();// +" "+i.ToString();
                                System.Threading.Thread.Sleep(10);
                            }
                            itemEntry1 form = new itemEntry1();
                            for (int i = 0; i < (dataGridView4.RowCount - 1); i++)
                            {
                                int index = dataGridView4.Rows[i].Cells[1].Value.ToString().IndexOf("عيار");
                                if (index >= 0)
                                {
                                    form.itemNameTxtbox.Text = dataGridView4.Rows[i].Cells[1].Value.ToString().Remove(index - 1);
                                }
                                string ss = dataGridView4.Rows[i].Cells[1].Value.ToString();
                                if (ss.Substring(ss.Length - 2) == "21")
                                { form.carat21.Checked = true; }
                                else if (ss.Substring(ss.Length - 2) == "18")
                                { form.carat18.Checked = true; }
                                else if (ss.Substring(ss.Length - 2) == "14")
                                { form.carat14.Checked = true; }
                                if (dataGridView4.Rows[i].Cells[2].Value.ToString().Contains("-"))
                                    form.itemCountTxtbox.Text = "0";
                                else
                                    form.itemCountTxtbox.Text = dataGridView4.Rows[i].Cells[2].Value.ToString();
                                if (dataGridView4.Rows[i].Cells[3].Value.ToString() == "0" || dataGridView4.Rows[i].Cells[3].Value.ToString().Contains("-"))
                                {
                                    form.weightTxtbox.Text = "00";
                                }
                                else
                                {
                                    form.weightTxtbox.Text = dataGridView4.Rows[i].Cells[3].Value.ToString();
                                }

                                if (dataGridView4.Rows[i].Cells[4].Value.ToString() == "0")
                                {
                                    form.feesTxtbox.Text = "00";
                                }
                                else
                                {
                                    form.feesTxtbox.Text = dataGridView4.Rows[i].Cells[4].Value.ToString();
                                }
                                form.save_Click1();
                                System.Threading.Thread.Sleep(10);
                            }
                            File.Delete("D:\\U\\a.txt");
                            File.Delete("D:\\U\\b.txt");
                            Directory.Delete("D:\\U");
                            MessageBox.Show("تم انشاء قاعدة البيانات المطلوبة");
                        //}
                    }
                    else if (!aa && bb)
                    {
                        MessageBox.Show("لم تقم بتدوير حسابات الصاغة , اختر تدوير حسابات الصاغة من القائمة");
                    }
                    else if (aa && !bb)
                    {
                        MessageBox.Show("لم تقم بتدوير جدول المصوغات , اختر تدوير المصوغات من القائمة");
                    }
                    else
                    {
                        MessageBox.Show("يجب تدوير حسابات الصاغة و تدوير المصوغات أولا");
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            else if (comboBox1.Text == "اعادة ضبط قاعدة البيانات بالكامل")
            {
                try
                {
                    string a = itm.deleteAll();
                    pay.dateTim1 = DateTime.Today.Date;
                    pay.dailyCash1 = 0;
                    pay.daily211 = 0;
                    pay.daily181 = 0;
                    pay.daily141 = 0;
                    pay.firstBillId1 = 0;
                    pay.firstPaymentId1 = 0;
                    pay.lastBillId1 = 0;
                    pay.lastPaymentId1 = 0;
                    pay.cashAdd1 = 0;
                    pay.gold21Add1 = 0;
                    pay.gold18Add1 = 0;
                    pay.gold14Add1 = 0;
                    string a1 = pay.openDaily();
                    if (a == "تم افراغ قاعدة البيانات بالكامل")
                    {
                        cus.customerName1 = "راس مال";
                        cus.mobile1 = "0900000001";
                        cus.insertCustomer();
                        cus.customerName1 = "مصروف";
                        cus.mobile1 = "0900000000";
                        cus.insertCustomer();
                        itemEntry1 form = new itemEntry1();
                        form.itemNameTxtbox.Text = "خشر";
                        form.carat21.Checked = true;
                        form.itemCountTxtbox.Text = "0";
                        form.weightTxtbox.Text = "00";
                        form.feesTxtbox.Text = "0";
                        form.save_Click11();
                        System.Threading.Thread.Sleep(10);
                        form.itemNameTxtbox.Text = "خشر";
                        form.carat18.Checked = true;
                        form.itemCountTxtbox.Text = "0";
                        form.weightTxtbox.Text = "00";
                        form.feesTxtbox.Text = "0";
                        form.save_Click11();
                        System.Threading.Thread.Sleep(10);
                        form.itemNameTxtbox.Text = "خشر";
                        form.carat14.Checked = true;
                        form.itemCountTxtbox.Text = "0";
                        form.weightTxtbox.Text = "00";
                        form.feesTxtbox.Text = "0";
                        form.save_Click11();
                        MessageBox.Show("تم اعادة ضبط قاعدة البيانات لجميع المستخدمين");
                    }
                    else
                    { execute_Click(sender, e); }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }
            }           
            else
            {
                MessageBox.Show("يرجى اختيار نوع العملية التي تريد تنفيذها");
            }
            //       else if (comboBox1.Text == "تغيير المسار")
            //       {
            //           try
            //           {
            //string source1= getDbFolder() + "\\iGOLD_DB.mdf";
            //string source2 = getDbFolder() + "\\iGOLD_DB_log.ldf";

            //string FileName = "حدد مسار المجلد الذي يوجد به قاعدة البيانات";
            //               SaveFileDialog sf = new SaveFileDialog();
            //               sf.FileName = FileName;

            //               if (sf.ShowDialog() == DialogResult.OK)
            //               {
            //                   string savePath = Path.GetDirectoryName(sf.FileName);

            //	ds.Tables[0].Rows[0].Delete();
            //                   ds.WriteXml("D:\\windows\\i\\o.xml");
            //                   DataRow r = ds.Tables[0].NewRow();
            //                   r[0] = 1;
            //                   r[1] = savePath;
            //                   ds.Tables[0].Rows.Add(r);
            //                   ds.WriteXml("D:\\windows\\i\\o.xml");
            //                   GlobalVar.dataBaseLocation = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename=" + getDbFolder() + "\\iGOLD_DB.mdf;Integrated Security = True";
            //                   cus.name1 = GlobalVar.dataBaseLocation;
            //                   string a = cus.updateSource();
            //                   if (a == " تم التعديل ")
            //                   {


            //		MessageBox.Show("تم تغيير مسار قاعدة البيانات");
            //	}
            //                   else { MessageBox.Show(a); }
            //               }

            //           }
            //           catch (Exception ex)
            //           {
            //               MessageBox.Show(ex.Message);
            //           }
            //       }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 1)
                {
                    if (comboBox1.Text == "تدوير المصوغات")
                    {
                        DataGridViewRow newDataRow = dataGridView1.Rows[indexRow1];
                        newDataRow.Cells[4].Value = newitemFeesTxtbox.Text;
                        newDataRow.Cells[3].Value = newitemWeightTxtbox.Text;
                        newDataRow.Cells[2].Value = newitemCountTxtbox.Text;
                        newDataRow.Cells[1].Value = itemNameTxtbox1.Text;
                        newDataRow.Cells[0].Value = indexRow1.ToString();
                    }
                    else if(comboBox1.Text=="تدوير حسابات الصاغة")
                    {                       
                        DataGridViewRow newDataRow = dataGridView1.Rows[indexRow1];
                        newDataRow.Cells[5].Value = newGold14Textbox.Text;
                        newDataRow.Cells[4].Value = newitemFeesTxtbox.Text;
                        newDataRow.Cells[3].Value = newitemWeightTxtbox.Text;
                        newDataRow.Cells[2].Value = newitemCountTxtbox.Text;
                        newDataRow.Cells[1].Value = itemNameTxtbox1.Text;
                        newDataRow.Cells[0].Value = itemNameTxtbox.Text;
                    }
                }
            }
            catch (Exception ex)
            {
				MessageBox.Show(ex.Message);
            }
        }
        
        private void delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 1)
                {

                    int rowIndex = dataGridView1.CurrentCell.RowIndex;
                    if (comboBox1.Text == "تدوير حسابات الصاغة")
                    {
                        if (dataGridView1.Rows[rowIndex].Cells[0].Value.ToString().Contains("راس مال") ||
                        dataGridView1.Rows[rowIndex].Cells[0].Value.ToString().Contains("مصروف"))
                        {
                            MessageBox.Show("لا يمكن حذف بيانات الصائغ");
                        }
                        else
                        {
                            dataGridView1.Rows.RemoveAt(rowIndex);
                        }
                    }
                    else if(comboBox1.Text=="تدوير المصوغات")
                    {
                        if (dataGridView1.Rows[rowIndex].Cells[1].Value.ToString().Contains("خشر عيار 21") ||
                        dataGridView1.Rows[rowIndex].Cells[1].Value.ToString().Contains("خشر عيار 18")||
                        dataGridView1.Rows[rowIndex].Cells[1].Value.ToString().Contains("خشر عيار 14"))
                        {
                            MessageBox.Show("لا يمكن حذف المصاغ");
                        }
                        else
                        {
                            dataGridView1.Rows.RemoveAt(rowIndex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
