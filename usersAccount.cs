using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace iGOLD
{
    public partial class usersAccount : Form
    {
        public usersAccount()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            ds.ReadXml("d:\\windows\\i\\DB.xml");
        }

        private const int cGrip = 16;
        private const int cCaption = 32;
        DataSet ds = new DataSet();
        userdb_Class cus = new userdb_Class();
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
            DialogResult result = MessageBox.Show("هل أنت متأكد بالخروج من حسابات المستخدمين", "الخروج", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
            }
            else
            {
                main_iGOLD form = new main_iGOLD();
                form.Show();
                if (GlobalVar.isMainMax == true)
                {
                    //form.Maximize_Click11();
                }
                this.Close();
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
            GlobalVar.name = userNameTxtbox.Text;
            GlobalVar.mob = passWord1Txtbox.Text;
            GlobalVar.item = passWord2Txtbox.Text;
            int W = this.Width;
            int H = this.Height;
            int screenW = Screen.PrimaryScreen.Bounds.Width;
            int screenH = Screen.PrimaryScreen.Bounds.Height;
            if (W == 600 && H == 430)
            {
                GlobalVar.userAccountisMainMax = true;
                this.SetDesktopLocation(0, 0);
                this.Size = new Size(screenW, screenH-40);
                close.Location = new Point(0, 0);
                maximize.Location = new Point(32, 0);
                minimize.Location = new Point(64, 0);
                bar.Size = new Size(screenW - 60, 37);
                bar.Location = new Point(0, 0);
                yellowPanel.Size = new Size(this.Width, 3);
                yellowPanel.Location = new Point(0, 32);
            }
            else
            {
                GlobalVar.userAccountisMainMax = false;
                this.Close();
                usersAccount form = new usersAccount();
                form.Show();
                form.userNameTxtbox.Text = GlobalVar.name;
                form.passWord1Txtbox.Text = GlobalVar.mob;
                form.passWord2Txtbox.Text = GlobalVar.item;
            }
        }

        private void userNameTxtbox_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(userNameTxtbox.Text))
            {
                userNameTxtbox.Text = "اسم المستخدم";
            }
            userNameTxtbox.LineIdleColor = GlobalVar.menuHoverColor;
        }

        private void userNameTxtbox_MouseEnter(object sender, EventArgs e)
        {
            if (Equals(userNameTxtbox.Text, "اسم المستخدم"))
            {
                userNameTxtbox.Text = "";
            }
        }

        private void passWord1Txtbox_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(passWord1Txtbox.Text))
            {
                passWord1Txtbox.Text = "كلمة المرور";
                passWord1Txtbox.isPassword = true;
                passWord2Txtbox.isPassword = true;
            }
            passWord1Txtbox.LineIdleColor = GlobalVar.menuHoverColor;
        }

        private void passWord1Txtbox_MouseEnter(object sender, EventArgs e)
        {
            if (Equals(passWord1Txtbox.Text, "كلمة المرور"))
            {
                passWord1Txtbox.Text = "";
                passWord1Txtbox.isPassword = true;
                passWord2Txtbox.isPassword = true;
            }
        }

        private void passWord1Txtbox_OnValueChanged(object sender, EventArgs e)
        {
            if ((passWord1Txtbox.Text.Trim() != "") && (passWord1Txtbox.Text != "كلمة المرور"))
            {
                try
                {
                    long i = Convert.ToInt64(passWord1Txtbox.Text.Trim());
                    passWord1Txtbox.isPassword = true;
                }
                catch
                {
                    MessageBox.Show("يرجى ادخال أرقام فقط");
                    passWord1Txtbox.Text = "";
                }
            }
        }

        private void passWord2Txtbox_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(passWord2Txtbox.Text))
            {
                passWord2Txtbox.Text = "كلمة المرور";
                passWord1Txtbox.isPassword = true;
                passWord2Txtbox.isPassword = true;
            }
            passWord2Txtbox.LineIdleColor = GlobalVar.menuHoverColor;
        }

        private void passWord2Txtbox_MouseEnter(object sender, EventArgs e)
        {

            if (Equals(passWord2Txtbox.Text, "كلمة المرور"))
            {
                passWord2Txtbox.Text = "";
                passWord1Txtbox.isPassword = true;
                passWord2Txtbox.isPassword = true;
            }
        }

        private void passWord2Txtbox_OnValueChanged(object sender, EventArgs e)
        {
            if ((passWord2Txtbox.Text.Trim() != "") && (passWord2Txtbox.Text != "كلمة المرور"))

            {

                try

                {

                    long i = Convert.ToInt64(passWord2Txtbox.Text.Trim());
                    passWord2Txtbox.isPassword = true;

                }

                catch
                {

                    MessageBox.Show("يرجى ادخال أرقام فقط");

                    passWord2Txtbox.Text = "";

                }

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

        private string idLabel(string i)
        {
            int id = 0;
            string str = "";
            try
            {
                id = Convert.ToInt32(i);
            }
            catch { }
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
            string userName = userNameTxtbox.Text;
            try
            {
                cus.name1 = userName;
                if (Convert.ToString(cus.checkUserExist()) == "true")
                {
                    label1.Text = "الاسم غير متوفر , يرجى اختيار اسم آخر";
                    label1.ForeColor = Color.OrangeRed;
                    MessageBox.Show(label1.Text);
                }
                else
                {
                    string no = cus.getN();
                    if (no == "true")
                    {
                        MessageBox.Show("تم انشاء عدد المستخدمين المخصص لهذه النسخة من البرنامج");
                    }
                    else if (no == "false")
                    {
                        if ((Equals(userNameTxtbox.Text, "") || Equals(userNameTxtbox.Text, "اسم المستخدم")) && (Equals(passWord1Txtbox.Text, "كلمة المرور") || Equals(passWord1Txtbox.Text, "")) && (Equals(passWord2Txtbox.Text, "0")) || Equals(passWord2Txtbox.Text, ""))
                        {
                            MessageBox.Show("!!.. يرجى ادخال بيانات المستخدم");
                            passWord1Txtbox.isPassword = true;
                            passWord2Txtbox.isPassword = true;
                        }
                        else if (Equals(userNameTxtbox.Text, "") || Equals(userNameTxtbox.Text, "اسم المستخدم"))
                        {
                            MessageBox.Show("!!.. يرجى ادخال اسم المستخدم");
                            passWord1Txtbox.isPassword = true;
                            passWord2Txtbox.isPassword = true;
                        }
                        else if (Equals(passWord1Txtbox.Text, "كلمة المرور") || Equals(passWord1Txtbox.Text, ""))
                        {
                            MessageBox.Show("!!.. يرجى ادخال كلمة المرور");
                            passWord1Txtbox.isPassword = true;
                            passWord2Txtbox.isPassword = true;
                        }
                        else if (!Equals(passWord1Txtbox.Text, passWord2Txtbox.Text))
                        {
                            MessageBox.Show("!!.. كلمة المرور غير متطابقة");
                            passWord1Txtbox.isPassword = true;
                            passWord2Txtbox.isPassword = true;
                        }
                        else if (passWord1Txtbox.Text.Length < 4 && passWord2Txtbox.Text.Length < 4)
                        {
                            MessageBox.Show("كلمة المرور قصيرة , يجب أن يكون طولها 4 أرقام على الأقل");
                            passWord1Txtbox.isPassword = true;
                            passWord2Txtbox.isPassword = true;
                        }
                        else
                        {
                            int idd = GlobalVar.id;
                            userName = userNameTxtbox.Text;
                            cus.name1 = userName;
                            if (Convert.ToString(cus.checkUserExist()) == "true")
                            {
                                label1.Text = "الاسم غير متوفر , يرجى اختيار اسم آخر";
                                label1.ForeColor = Color.Crimson;
                            }
                            else
                            {
                                label1.Text = "الاسم متوفر";
                                label1.ForeColor = Color.MediumSpringGreen;
                            }
                            string passWord = Convert.ToString(passWord1Txtbox.Text);
                            cus.userName1 = userName;
                            cus.passWord1 = passWord;
                            MessageBox.Show(cus.insertUser());
                            DataRow r = ds.Tables[0].NewRow();
                            r[0] = Convert.ToInt32(IDTxtBox.Text);
                            r[1] = userName;
                            r[2] = passWord;
                            ds.Tables[0].Rows.Add(r);
                            ds.WriteXml("d:\\windows\\i\\DB.xml");
                            IDTxtBox.Text = idLabel(cus.getUserCount());
                            userNameTxtbox.Text = "";
                            label1.Text = "";
                            passWord1Txtbox.Text = "كلمة المرور";
                            passWord2Txtbox.Text = "كلمة المرور";
                            passWord1Txtbox.isPassword = true;
                            passWord2Txtbox.isPassword = true;
                            Customerdb_Class cus1 = new Customerdb_Class();
                            cus1.name1 = "راس مال";
                            cus.userName1 = userName;
                            GlobalVar.id = cus.getUserId();
                            if (cus1.checkCustomerExist() != "true")
                            {
                                cus1.customerName1 = "راس مال";
                                cus1.mobile1 = "0900000001";
                                cus1.insertCustomer();
                            }
                            //else { MessageBox.Show("تم اضافة الصائغ (راس مال) الى شجرة الصاغة للمستخدم " + userName + " سابقا"); }
                            cus1.name1 = "مصروف";
                            if (cus1.checkCustomerExist() != "true")
                            {
                                cus1.customerName1 = "مصروف";
                                cus1.mobile1 = "0900000000";
                                cus1.insertCustomer();
                            }
                            //else { MessageBox.Show("تم اضافة الصائغ (مصروف) الى شجرة الصاغة للمستخدم " + userName + " سابقا"); }
                            itemEntry1 form = new itemEntry1();
                            Itemdb_Class itm = new Itemdb_Class();
                            itm.itemName1 = "خشر عيار 21";
                            if (itm.checkitemExist() != "true")
                            {
                                form.itemNameTxtbox.Text = "خشر";
                                form.carat21.Checked = true;
                                form.itemCountTxtbox.Text = "0";
                                form.weightTxtbox.Text = "00";
                                form.feesTxtbox.Text = "0";
                                form.save_Click11();
                                System.Threading.Thread.Sleep(10);
                            }
                            itm.itemName1 = "خشر عيار 18";
                            if (itm.checkitemExist() != "true")
                            {
                                form.itemNameTxtbox.Text = "خشر";
                                form.carat18.Checked = true;
                                form.itemCountTxtbox.Text = "0";
                                form.weightTxtbox.Text = "00";
                                form.feesTxtbox.Text = "0";
                                form.save_Click11();
                                System.Threading.Thread.Sleep(10);
                            }
                            itm.itemName1 = "خشر عيار 14";
                            if (itm.checkitemExist() != "true")
                            {
                                form.itemNameTxtbox.Text = "خشر";
                                form.carat14.Checked = true;
                                form.itemCountTxtbox.Text = "0";
                                form.weightTxtbox.Text = "00";
                                form.feesTxtbox.Text = "0";
                                form.save_Click11();
                            }
                            GlobalVar.id = idd;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
          
        private void cancel_Click(object sender, EventArgs e)
        {
            userNameTxtbox.Text = "";
            passWord1Txtbox.Text = "كلمة المرور";
            passWord2Txtbox.Text = "كلمة المرور";
        }

        private void userNameTxtbox_Leave(object sender, EventArgs e)
        {
            string userName = userNameTxtbox.Text;
            userdb_Class cus = new userdb_Class();
            cus.name1 = userName;
            if (Convert.ToString(cus.checkUserExist()) == "true")
            {
                label1.Text = "الاسم غير متوفر , يرجى اختيار اسم آخر";
                label1.ForeColor = Color.LightSalmon;
            }
            else
            {
                label1.Text = "الاسم متوفر";
                label1.ForeColor = Color.MediumSpringGreen;
            }
        }

        private void usersAccount_Load(object sender, EventArgs e)
        {
            //if (main.IsUsbDeviceConnected(GlobalVar.pid))
            //{
                try
                {
                    if (GlobalVar.userAccountisMainMax)
                    {
                        maximize_Click(sender, e);
                    }
                    IDTxtBox.Text = idLabel(cus.getUserCount());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            //}
            //else
            //{
            //    MessageBox.Show("ان فلاش الحماية غير متصل بالكمبيوتر , يرجى التأكد من اتصال فلاش الحماية بالكمبيوتر" + Environment.NewLine + "ثم أعد تشغيل البرنامج");
            //    this.Close();
            //    main.Show();
            //}

        }

        private void edit_Click(object sender, EventArgs e)
        {
            try { 
            if ((Equals(userNameTxtbox.Text, "") || Equals(userNameTxtbox.Text, "اسم المستخدم")) && (Equals(passWord1Txtbox.Text, "كلمة المرور") || Equals(passWord1Txtbox.Text, "")) && (Equals(passWord2Txtbox.Text, "0")) || Equals(passWord2Txtbox.Text, ""))
            {

                MessageBox.Show("!!.. يرجى ادخال بيانات المستخدم");
                passWord1Txtbox.isPassword = true;
                passWord2Txtbox.isPassword = true;

            }
            else if (Equals(userNameTxtbox.Text, "") || Equals(userNameTxtbox.Text, "اسم المستخدم"))
            {
                MessageBox.Show("!!.. يرجى ادخال اسم المستخدم");
                passWord1Txtbox.isPassword = true;
                passWord2Txtbox.isPassword = true;
            }
            else if (Equals(passWord1Txtbox.Text, "كلمة المرور") || Equals(passWord1Txtbox.Text, ""))
            {
                MessageBox.Show("!!.. يرجى ادخال كلمة المرور");
                passWord1Txtbox.isPassword = true;
                passWord2Txtbox.isPassword = true;
            }
            else if (!Equals(passWord1Txtbox.Text, passWord2Txtbox.Text))
            {
                MessageBox.Show("!!.. كلمة المرور غير متطابقة");
                passWord1Txtbox.isPassword = true;
                passWord2Txtbox.isPassword = true;
            }
            else if(passWord1Txtbox.Text.Length<4 && passWord2Txtbox.Text.Length<4)
            {
                MessageBox.Show("كلمة المرور قصيرة , يجب أن يكون طولها 4 أرقام على الأقل");
                passWord1Txtbox.isPassword = true;
                passWord2Txtbox.isPassword = true;
            }
            else
            {
                cus.userName1 = userNameTxtbox.Text;
                string pass = cus.getPassOfName();

                if (passWord1Txtbox.Text==pass )
                {
                    MessageBox.Show("كلمة مرور المستخدم  " + userNameTxtbox.Text + "  !! نفسها , لم يتم تعديل البيانات");
                }
                else
                    {
                        try
                        {
                            string PassWord = passWord1Txtbox.Text;
                            int id = cus.getUserId();
                            cus.passWord1 = PassWord;
                            cus.userId1 = id;
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                if (userNameTxtbox.Text == ds.Tables[0].Rows[i]["UserName"].ToString())
                                {
                                    ds.Tables[0].Rows[i].Delete();
                                    ds.WriteXml("D:\\windows\\i\\DB.xml");
                                }
                            }
                            DataRow r = ds.Tables[0].NewRow();
                            r[0] = id;
                            r[1] = userNameTxtbox.Text;
                            r[2] = PassWord;
                            ds.Tables[0].Rows.Add(r);
                            ds.WriteXml("D:\\windows\\i\\DB.xml");
                            MessageBox.Show(cus.updateUser());
                        }
                        catch (Exception ex)
                        { MessageBox.Show(ex.Message); }
                    }
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void usersAccount_Resize(object sender, EventArgs e)
        {
            close.Location = new Point(0, 0);
            maximize.Location = new Point(32, 0);
            minimize.Location = new Point(64, 0);
            bar.Size = new Size(this.Width - 60, 37);
            bar.Location = new Point(0, 0);
            yellowPanel.Size = new Size(this.Width, 3);
            yellowPanel.Location = new Point(0, 32);
        }

        private void userNameTxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                passWord1Txtbox.Focus();
                passWord1Txtbox.Select();
            }
        }

        private void passWord1Txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                passWord2Txtbox.Focus();
                passWord2Txtbox.Select();
            }
        }

        private void passWord2Txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                save_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("هل أنت متأكد برغبتك بتعديل اسم المستخدم:  "+cus.getuserName(), "التاكد من تغيير الاسم؟", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
            }
            else
            {
                try
                {
                    string PassWord = passWord1Txtbox.Text;
                    int id = GlobalVar.id;
                    cus.userId1 = id;
                    string nam=cus.getuserName();
                    cus.userName1 = nam;
                    PassWord=cus.getPassOfName();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        if (id == Convert.ToInt32(ds.Tables[0].Rows[i]["UserId"].ToString()))
                        {
                            ds.Tables[0].Rows[i].Delete();
                            ds.WriteXml("D:\\windows\\i\\DB.xml");
                            break;
                        }
                    }
                    DataRow r = ds.Tables[0].NewRow();
                    r[0] = id;
                    r[1] = userNameTxtbox.Text;
                    r[2] = PassWord;
                    ds.Tables[0].Rows.Add(r);
                    ds.WriteXml("D:\\windows\\i\\DB.xml");
                    cus.name1 = userNameTxtbox.Text;
                    MessageBox.Show(cus.updateUserName() + " على اسم المستخدم " + Environment.NewLine + " سيتم تسجيل الخروج , يرجى تسجيل الدخول مرة اخرى");
                    Login log = new Login();
                    log.userNameTxtbox.Text = userNameTxtbox.Text;
                    log.passWordTxtbox.Text = PassWord;
                    log.login();
                    this.Close();
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message); }               
            }
        }
    }
}
