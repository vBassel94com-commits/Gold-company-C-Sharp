using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace iGOLD
{
    public partial class editCustomer : Form
    {
        public editCustomer()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        int indexRow;
        SqlConnection con = new SqlConnection(GlobalVar.dataBaseLocation);
        private const int cGrip = 16;
        private const int cCaption = 32;
        main_iGOLD main = new main_iGOLD();
        Customerdb_Class cus = new Customerdb_Class();
        paymentdb_Class Pcus = new paymentdb_Class();
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

        private void close_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("هل أنت متأكد بالخروج من تعديل بيانات صائغ", "الخروج", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
            }
            else
            {
                this.Close();
                customerEntry form = new customerEntry();
                if (GlobalVar.customerEntryisMainMax == true)
                {
                    form.Maximize_Click1();
                }
                form.Show();
            }
        }

        private void maximize_Click(object sender, EventArgs e)
        {
            Maximize_Click1();
            dataGridView1.Columns[0].Width = 0;
        }

        public void Maximize_Click1()
        {
            int W = this.Width;
            int H = this.Height;
            int screenW = Screen.PrimaryScreen.Bounds.Width;
            int screenH = Screen.PrimaryScreen.Bounds.Height;
            if (W == 700 && H == 500)
            {
                GlobalVar.editCustomerisMainMax = true;
                this.SetDesktopLocation(0, 0);
                this.Size = new Size(screenW, screenH-40);
                close.Location = new Point(2, 0);
                maximize.Location = new Point(34, 0);
                minimize.Location = new Point(66, 0);
                bar.Size = new Size(64, 37);
                bar.Location = new Point(0, 0);
                yellowPanel.Size = new Size(screenW, 3);
                yellowPanel.Location = new Point(0, 32);

            }
            else
            {
                GlobalVar.editCustomerisMainMax = false;
                this.Hide();
                editCustomer form = new editCustomer();
                form.Show();
            }
        }

        private void editCustomer_Load(object sender, EventArgs e)
        {
            if (main.IsUsbDeviceConnected(GlobalVar.pid))
            {
                try
                {
                    if (GlobalVar.editCustomerisMainMax)
                    {
                        maximize_Click(sender, e);
                    }
                    loadCustomers();
                    customerEntry1 customer = new customerEntry1();
                    nameTxtbox.AutoCompleteCustomSource = customer.loadNames();
                    dataGridView1.Columns[0].Width = 0;
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

        public void loadCustomers()
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select customerId,customerName,customerMobile from customers where userId = " + GlobalVar.id.ToString();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
                dataGridView1.Columns[0].HeaderText = "id";
                dataGridView1.Columns[1].HeaderText = "اسم الصائغ";
                dataGridView1.Columns[2].HeaderText = "رقم موبايل الصائغ";
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            if (indexRow != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[indexRow];
                nameTxtbox.Text = row.Cells[1].Value.ToString();
                mobileTxtbox.Text = row.Cells[2].Value.ToString();
                idTxtbox.Text = row.Cells[0].Value.ToString();
            }

        }
      
        private void edit_Click(object sender, EventArgs e)
        {
            try
            {
                cus.customerName1 = nameTxtbox.Text;
                cus.mobile1 = mobileTxtbox.Text;
                cus.customerId1 = idTxtbox.Text;
                MessageBox.Show(cus.updateCustomer());
                loadCustomers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void editCustomer_Resize(object sender, EventArgs e)
        {
            close.Location = new Point(4, 0);
            maximize.Location = new Point(38, 0);
            minimize.Location = new Point(72, 0);
            bar.Size = new Size(this.Width - 72, 37);
            bar.Location = new Point(0, 0);
            yellowPanel.Size = new Size(this.Width, 3);
            yellowPanel.Location = new Point(0, 32);
            //dataGridView1.Columns[0].Width = 0;
        }

        private void mobileTxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void nameTxtbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (nameTxtbox.TextLength > 1)
                {
                    Pcus.customerName1 = nameTxtbox.Text;
                    idTxtbox.Text = Convert.ToString(Pcus.GetCustomerid());
                    cus.customerName1 = nameTxtbox.Text;
                    mobileTxtbox.Text = cus.getMobOfNam();
                    for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[1].Value.ToString()==nameTxtbox.Text)
                        {
                            dataGridView1.Rows[i].Selected = true;
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
