using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;


namespace iGOLD
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            ds.ReadXml("D:\\windows\\i\\DB.xml");
            ds1.ReadXml("D:\\windows\\i\\o.xml");
        }
        private const int cGrip = 16;
        private const int cCaption = 32;
        private int count = 1;
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();
        Customerdb_Class cus = new Customerdb_Class();
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
        private void bar_Click(object sender, EventArgs e)
        {
            this.bar.BackColor = GlobalVar.barHoverColor;
        }
        private void bar_MouseHover(object sender, EventArgs e)
        {
            this.bar.BackColor = GlobalVar.barHoverColor;
        }
        private void bar_MouseLeave(object sender, EventArgs e)
        {
            this.bar.BackColor = GlobalVar.leaveColor;
        }
        private void exit_Click(object sender, EventArgs e)
        {
           Application.Exit(); 
        }
        private void exit_MouseHover(object sender, EventArgs e)
        {
            this.exit.BackColor =GlobalVar.closeHoverColor;
        }
        private void exit_MouseLeave(object sender, EventArgs e)
        {
            this.exit.BackColor = GlobalVar.leaveColor;
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
        private void passWordTxtbox_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(passWordTxtbox.Text))
            {
                passWordTxtbox.Text = "كلمة المرور";
                passWordTxtbox.isPassword = !passVisible.Checked;
            }
            passWordTxtbox.LineIdleColor = GlobalVar.menuHoverColor;
        }
        private void passWordTxtbox_MouseEnter(object sender, EventArgs e)
        {

            if (Equals(passWordTxtbox.Text, "كلمة المرور"))
            {
                passWordTxtbox.Text = "";
                passWordTxtbox.isPassword = !passVisible.Checked;
            }
        }
        private void passWordTxtbox_OnValueChanged(object sender, EventArgs e)
        {
            passWordTxtbox.isPassword = !passVisible.Checked;
        }
        private void login_btn_Click(object sender, EventArgs e)
        {
            login();
        }
        public void login()
        {
            if (Equals(userNameTxtbox.Text, "اسم المستخدم") && Equals(passWordTxtbox.Text, "كلمة المرور"))
            {

                MessageBox.Show("!!.. يرجى ادخال اسم المستخدم وكلمة المرور");

            }
            else if (Equals(userNameTxtbox.Text, "اسم المستخدم") && Equals(passWordTxtbox.Text, ""))
            {

                MessageBox.Show("!!.. يرجى ادخال اسم المستخدم وكلمة المرور");

            }
            else if (Equals(userNameTxtbox.Text, "اسم المستخدم"))
            {

                MessageBox.Show("!!.. يرجى ادخال اسم المستخدم");

            }
            else if (Equals(passWordTxtbox.Text, "كلمة المرور") || Equals(passWordTxtbox.Text, ""))
            {

                MessageBox.Show("!!.. يرجى ادخال كلمة المرور");

            }
            else if (Convert.ToString(checkUserExist()) == "true" && passWordTxtbox.Text == getPassOfName() && count < 5)
            {
                try
                {
                    foreach (var process in Process.GetProcessesByName("sqlservr.exe"))
                    {
                        process.Kill();
                    }
                    GlobalVar.userName = userNameTxtbox.Text;
                    GlobalVar.dataBaseLocation = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + getDbFolder() + "\\iGOLD_DB.mdf;  Integrated Security = True;database=Db1";
                    //GlobalVar.dataBaseLocation = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=" + getDbFolder() + "\\iGOLD_DB.MDF;" +"database=Db1;Integrated Security=True";

                    this.Close();
					main_iGOLD form = new main_iGOLD();
					form.Show();
				}
                catch(Exception ex)
                { MessageBox.Show(ex.Message); }
            }
            else if (count > 4)
            {
                MessageBox.Show("لقد تجاوزت العدد المسموح به لمحاولات تسجيل الدخول");
                System.Threading.Thread.Sleep(3000);
                Application.Exit();  
            }
            else
            {
                MessageBox.Show("!!.. بيانات غير صحيحة " + Environment.NewLine + Environment.NewLine + "  لديك " + Convert.ToString(5 - count) + "  محاولة لتسجيل الدخول");
                count = count + 1;
            }

        }
        private void passVisible_CheckedChanged(object sender, EventArgs e)
        {
            passWordTxtbox.isPassword = !passVisible.Checked;
        }
        private void passWordTxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }
        private void userNameTxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                passWordTxtbox.Focus();
                passWordTxtbox.Select();
            }
        }
        public string checkUserExist()
        {
            string A = "false";
            for (int i = 0; i < ds.Tables[0].Rows.Count ; i++)
            {
                if (userNameTxtbox.Text == ds.Tables[0].Rows[i]["UserName"].ToString())
                {
                    A= "true";
                }
            }
            return A;
        }
        public string getPassOfName()
        {
            string A = "false";
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (userNameTxtbox.Text == ds.Tables[0].Rows[i]["UserName"].ToString())
                {
                    A = ds.Tables[0].Rows[i]["PassWord"].ToString();
                }
            }
            return A;
        }
		public string getDbFolder()
		{
			return ds1.Tables[0].Rows[0]["source"].ToString();
		}
        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void minimize_MouseHover(object sender, EventArgs e)
        {
            this.minimize.BackColor = GlobalVar.minMaxHoverColor;
        }
        private void minimize_MouseLeave(object sender, EventArgs e)
        {
            this.minimize.BackColor = GlobalVar.leaveColor;
        }
    }
}
