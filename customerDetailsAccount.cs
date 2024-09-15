using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace iGOLD
{
    public partial class customerDetailsAccount : Form
    {
        public customerDetailsAccount()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }
        const int WS_MINIMIZEBOX = 0x20000;
        const int CS_DBLCLKS = 0x1;
     
        private const int cGrip = 16;
        private const int cCaption = 32;
        SqlConnection con = new SqlConnection(GlobalVar.dataBaseLocation);
        main_iGOLD main = new main_iGOLD();
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
            DialogResult result = MessageBox.Show("هل أنت متأكد بالخروج من حساب الصائغ التفصيلي", "الخروج", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
            }
            else
            {
                this.Close();
                customerTotalAccount form = new customerTotalAccount();
                if (GlobalVar.isMainMax == true)
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
            GlobalVar.name = nameTxtBox.Text;
            GlobalVar.mob = mobileTxtBox.Text;
            int W = this.Width;
            int H = this.Height;
            int screenW = Screen.PrimaryScreen.Bounds.Width;
            int screenH = Screen.PrimaryScreen.Bounds.Height;
            if (W == 760 && H == 560)
            {
                GlobalVar.customerTotalAccountisMainMax = true;
                this.SetDesktopLocation(0, 0);
                this.Size = new Size(screenW, screenH-40);
                close.Location = new Point(0, 0);
                maximize.Location = new Point(36, 0);
                minimize.Location = new Point(72, 0);
                bar.Size = new Size(screenW - 72, 37);
                bar.Location = new Point(0, 0);
                nameTxtBox.Text = GlobalVar.name;
                mobileTxtBox.Text = GlobalVar.mob;
                yellowPanel.Size = new Size(screenW, 3);
                yellowPanel.Location = new Point(0, 32);
                dataGridView1.Size = new Size(dataGridView1.Size.Width, screenH - 190);
            }
            else
            {
                GlobalVar.customerTotalAccountisMainMax = false;
                this.Close();
                customerDetailsAccount form = new customerDetailsAccount();
                form.Show();
                form.nameTxtBox.Text = GlobalVar.name;
                form.mobileTxtBox.Text = GlobalVar.mob;
            }
        }

        private void customerDetailsAccount_Resize(object sender, EventArgs e)
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

        private void customerDetailsAccount_Load(object sender, EventArgs e)
        { 
            //if (main.IsUsbDeviceConnected(GlobalVar.pid))
            //{
				if (GlobalVar.customerDetailsAccountisMainMax)
            {
                maximize_Click(sender, e);
            }
            try
            {
                timer1.Start();
                date.Text = String.Format("{0: yyyy / MM / dd}", DateTime.Now);
                day.Text = String.Format("{0: :dddd}", DateTime.Now);
                time.Text = String.Format("{0:HH : mm}", DateTime.Now);
                time1.Text = String.Format("{0:tt}", DateTime.Now);
                SqlConnection con = new SqlConnection();
                con.ConnectionString = GlobalVar.dataBaseLocation;
                con.Open();
                SqlDataReader DataRdr;
                SqlCommand cmd = new SqlCommand("select customerName from customers", con);
                cmd.ExecuteNonQuery();
                DataRdr = cmd.ExecuteReader();
                AutoCompleteStringCollection names = new AutoCompleteStringCollection();
                while (DataRdr.Read())
                {
                    names.Add(DataRdr.GetString(0));
                }
                nameTxtBox.AutoCompleteCustomSource = names;
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
            //}
            //else
            //{
            //    MessageBox.Show("ان فلاش الحماية غير متصل بالكمبيوتر , يرجى التأكد من اتصال فلاش الحماية بالكمبيوتر" + Environment.NewLine + "ثم أعد تشغيل البرنامج");
            //    this.Close();
            //    main.Show();
            //}
        }

        private void loadPaymentDb()
        {
            
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            paymentdb_Class cus = new paymentdb_Class();
            cus.customerName1 = nameTxtBox.Text;
            cmd.CommandText = "(select paymentDateTime , payment14, payment18, payment21, paymentCash, paymentTypeString , paymentId from payments where customerId=" + Convert.ToString(cus.GetCustomerid()) + ") UNION (select billDateTime , billTotal14 , billTotal18 , billTotal21 , billTotalCash , billType , billId  from bills where customerId=" + Convert.ToString(cus.GetCustomerid()) + ")";
            try
            {
                cmd.ExecuteNonQuery();
             
            }

            catch { }
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            dataGridView1.Columns[6].HeaderText = "id";
            dataGridView1.Columns[5].HeaderText = "نوع الحركة";
            dataGridView1.Columns[4].HeaderText = "الاجور";
            dataGridView1.Columns[3].HeaderText = GlobalVar.gold21Label;
            dataGridView1.Columns[2].HeaderText = GlobalVar.gold18Label;
            dataGridView1.Columns[1].HeaderText = GlobalVar.gold14Label;
            dataGridView1.Columns[0].HeaderText = "تاريخ و وقت الحركة";
            dataGridView1.Columns[6].MinimumWidth = 25;
            dataGridView1.Columns[5].MinimumWidth = 80;
            dataGridView1.Columns[4].MinimumWidth = 75;
            dataGridView1.Columns[3].MinimumWidth = 65;
            dataGridView1.Columns[2].MinimumWidth = 65;
            dataGridView1.Columns[1].MinimumWidth = 65;
            dataGridView1.Columns[0].MinimumWidth = 200;

        }

        private void nameTxtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Customerdb_Class cus1 = new Customerdb_Class();
                cus1.customerName1 = nameTxtBox.Text;
                mobileTxtBox.Text = cus1.getMobOfNam();
                loadPaymentDb();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int indexRow;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                indexRow = e.RowIndex;
                DataGridViewRow row = dataGridView1.Rows[indexRow];

                if (indexRow != -1)
                {
                   
                    if (row.Cells[5].Value.ToString() == "بيع")
                    {
                        GlobalVar.Daily = "تعديل";
                        daily form = new daily();
                        GlobalVar.status_value = true;
                        GlobalVar.fromDetialsAccounting = true;
                        form.editLabel.Visible = true;
                        //form.save.Text = "تعديل الفاتورة";
                        GlobalVar.editBillId = row.Cells[6].Value.ToString();
                        form.ShowDialog();
                    }
                    else if (row.Cells[5].Value.ToString() == "شراء")
                    {
                        GlobalVar.Daily = "تعديل";
                        daily form = new daily();
                        GlobalVar.status_value = true;
                        GlobalVar.fromDetialsAccounting = true;
                        form.editLabel.Visible = true;
                        //form.save.Text = "تعديل الفاتورة";
                        form.billTypeLabel.Text = "شراء";
                        GlobalVar.editBillId = row.Cells[6].Value.ToString();
                        form.ShowDialog();
                    }
                    else
                    {
                        editPayment1 form = new editPayment1();
                        GlobalVar.editBackTo = "detailsAccounting";
                        GlobalVar.fromDetialsAccounting = true;
                        GlobalVar.editPaymentId = row.Cells[6].Value.ToString();
                        form.ShowDialog();
                    }
                }
            }
            catch(Exception ex)
            {
      
                MessageBox.Show(ex.Message);
            }
        }
    }
}