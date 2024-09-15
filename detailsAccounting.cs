using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Collections.Generic;

namespace iGOLD
{
    public partial class detailsAccounting : Form
    {
       
        public detailsAccounting()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }
        List<string> listOnit = new List<string>();
        List<string> listNew = new List<string>();
        SqlConnection con = new SqlConnection(GlobalVar.dataBaseLocation);
        private const int cGrip = 16;
        private const int cCaption = 32;
        paymentdb_Class cus = new paymentdb_Class();
        Itemdb_Class itm = new Itemdb_Class();
        Customerdb_Class ccus = new Customerdb_Class();
        paymentdb_Class pay = new paymentdb_Class();
        main_iGOLD main = new main_iGOLD();
        int indexRow;
        int i1 = 0;
        int ii = -1;
        string a = "";
        bool isFill = false;
        bool T = true;
        int index = 0;
        const int WS_MINIMIZEBOX = 0x20000;
        const int CS_DBLCLKS = 0x1;
        DataTable dt = new DataTable();

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

        private void close_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("هل أنت متأكد بالخروج من القيد المالي", "الخروج", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
            }
            else
            {
                if (GlobalVar.status_value) { MessageBox.Show("سيتم اغلاق البرنامج"); GlobalVar.status_value = false;}
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
            if (W == 841 && H == 700)
            {

                this.SetDesktopLocation(0, 0);
                this.Size = new Size(screenW, screenH-40);
                close.Location = new Point(0, 0);
                maximize.Location = new Point(34, 0);
                minimize.Location = new Point(78, 0);
                bar.Size = new Size(this.Width - 78, 37);
                bar.Location = new Point(0, 0);
                yellowPanel.Size = new Size(screenW, 3);
                yellowPanel.Location = new Point(0, 32);
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
                GlobalVar.detailsAccountingisMainMax = false;
                this.Close();
                detailsAccounting form = new detailsAccounting();
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

        private void detailsAccounting_Resize(object sender, EventArgs e)
        {
            close.Location = new Point(0, 0);
            maximize.Location = new Point(34, 0);
            minimize.Location = new Point(78, 0);
            bar.Size = new Size(this.Width - 78, 37);
            bar.Location = new Point(0, 0);
            yellowPanel.Size = new Size(this.Width, 3);
            yellowPanel.Location = new Point(0, 32);
        }

        private void bar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Maximize_Click1();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = String.Format("{0:HH : mm}", DateTime.Now);
            time1.Text = String.Format("{0:tt}", DateTime.Now);
            timer1.Start();
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

                DialogResult result = MessageBox.Show("هل أنت متأكد من الخروج من القيد المالي", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل أنت متأكد من الخروج من القيد المالي", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل أنت متأكد من الخروج من القيد المالي", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل أنت متأكد من الخروج من القيد المالي", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل أنت متأكد من الخروج من القيد المالي", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل أنت متأكد من الخروج من القيد المالي", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل أنت متأكد من الخروج من القيد المالي", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل أنت متأكد من الخروج من القيد المالي", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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
                DialogResult result = MessageBox.Show("هل أنت متأكد من الخروج من القيد المالي", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);
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

                DialogResult result = MessageBox.Show("هل أنت متأكد من الخروج من القيد المالي", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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
  
        private void detailsAccounting_Load(object sender, EventArgs e)
        {
            calculate.Visible = false;
            if (main.IsUsbDeviceConnected(GlobalVar.pid))
            {
                printDocument1.DefaultPageSettings.Landscape = false;
                timer1.Start();
                date.Text = String.Format("{0: yyyy / MM / dd}", DateTime.Now);
                day.Text = String.Format("{0: :dddd}", DateTime.Now);
                time.Text = String.Format("{0:HH : mm}", DateTime.Now);
                time1.Text = String.Format("{0:tt}", DateTime.Now);
                dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 01);
                dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                if (GlobalVar.detailsAccountingisMainMax)
                {
                  maximize_Click(sender, e);
                }
                comboBox1.Select();
                comboBox1.Focus();
            }
            else
            {
                MessageBox.Show("ان فلاش الحماية غير متصل بالكمبيوتر , يرجى التأكد من اتصال فلاش الحماية بالكمبيوتر" + Environment.NewLine + "ثم أعد تشغيل البرنامج");
                this.Close();
        main.Show();
            }
}

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (comboBox1.Text == "الدفعات")
            {
                editPayment1 form = new editPayment1();
                GlobalVar.editBackTo = "detailsAccounting";
                GlobalVar.fromDetialsAccounting = true;
                indexRow = e.RowIndex;
                if (indexRow != -1)
                {
                    DataGridViewRow row = dataGridView1.Rows[indexRow];
                    GlobalVar.editPaymentId = row.Cells[12].Value.ToString();
                    form.idTxtbox.Text = GlobalVar.editPaymentId;
                    form.getPaymentDetails();
                    form.ShowDialog();
                    form.getPaymentDetails();
                    form.getPaymentDetails();

                }
            }
            else if (comboBox1.Text == "الفواتير")
            {
                GlobalVar.Daily = "تعديل";
                daily form = new daily();
                indexRow = e.RowIndex;
                if (indexRow != -1)
                {
                    GlobalVar.status_value = true;
                    GlobalVar.fromDetialsAccounting = true;
                    DataGridViewRow row = dataGridView1.Rows[indexRow];
                    GlobalVar.Daily = "تعديل";
                    form.editLabel.Visible = true;
                    //form.save.Text = "تعديل الفاتورة";
                    GlobalVar.editBillId = row.Cells[6].Value.ToString();
                    form.ShowDialog();
                }

            }
            else if (comboBox1.Text == "حساب صائغ"||comboBox1.Text=="كشف حساب تفصيلي")
            {
                indexRow = e.RowIndex;
                if (indexRow != -1)
                {
                    DataGridViewRow row = dataGridView1.Rows[indexRow];
                    if (row.Cells[10].Value.ToString() == "بيع")
                    {
                        GlobalVar.Daily = "تعديل";
                        daily form = new daily();
                        GlobalVar.status_value = true;
                        GlobalVar.fromDetialsAccounting = true;
                        form.editLabel.Visible = true;
                        //form.save.Text = "تعديل الفاتورة";
                        GlobalVar.editBillId = row.Cells[11].Value.ToString();
                        form.ShowDialog();
                    }
                    else if (row.Cells[10].Value.ToString() == "شراء")
                    {
                        GlobalVar.Daily = "تعديل";
                        daily form = new daily();
                        GlobalVar.status_value = true;
                        GlobalVar.fromDetialsAccounting = true;
                        form.editLabel.Visible = true;
                        //form.save.Text = "تعديل الفاتورة";
                        form.billTypeLabel.Text = "شراء";
                        GlobalVar.editBillId = row.Cells[11].Value.ToString();
                        form.ShowDialog();
                    }
                    else
                    {
                        editPayment1 form = new editPayment1();
                        GlobalVar.editBackTo = "detailsAccounting";
                        GlobalVar.fromDetialsAccounting = true;
                        GlobalVar.editPaymentId = row.Cells[11].Value.ToString();
                        form.idTxtbox.Text = GlobalVar.editPaymentId;
                        form.getPaymentDetails();
                        form.ShowDialog();
                        form.getPaymentDetails();
                        form.getPaymentDetails();

                    }
                }
            }
            else if (comboBox1.Text == "حركة مصاغ")
            {
                indexRow = e.RowIndex;
                if (indexRow != -1)
                {
                    DataGridViewRow row = dataGridView1.Rows[indexRow];
                    if (row.Cells[2].Value.ToString() == "بيع")
                    {
                        GlobalVar.Daily = "تعديل";
                        daily form = new daily();
                        GlobalVar.status_value = true;
                        GlobalVar.fromDetialsAccounting = true;
                        form.editLabel.Visible = true;
                        //form.save.Text = "تعديل الفاتورة";
                        GlobalVar.editBillId =row.Cells[3].Value.ToString();
                        form.ShowDialog();
                    }
                    else if (row.Cells[2].Value.ToString() == "شراء")
                    {
                        GlobalVar.Daily = "تعديل";
                        daily form = new daily();
                        GlobalVar.status_value = true;
                        GlobalVar.fromDetialsAccounting = true;
                        form.editLabel.Visible = true;
                        //form.save.Text = "تعديل الفاتورة";
                        form.billTypeLabel.Text = "شراء";
                        GlobalVar.editBillId = row.Cells[3].Value.ToString();
                        form.ShowDialog();
                    }
                    else
                    {
                        editPayment1 form = new editPayment1();
                        GlobalVar.editBackTo = "detailsAccounting";
                        GlobalVar.fromDetialsAccounting = true;
                        GlobalVar.editPaymentId = row.Cells[3].Value.ToString();
                        form.idTxtbox.Text = GlobalVar.editPaymentId;
                        form.getPaymentDetails();
                        form.ShowDialog();
                        form.getPaymentDetails();
                        form.getPaymentDetails();

                    }
                }
            }
            else if (comboBox1.Text == "القيد اليومي")
            {
                indexRow = e.RowIndex;
                if (indexRow != -1)
                {
                    DataGridViewRow row = dataGridView1.Rows[indexRow];
                    if (row.Cells[7].Value.ToString() == "بيع")
                    {
                        GlobalVar.Daily = "تعديل";
                        daily form = new daily();
                        GlobalVar.status_value = true;
                        GlobalVar.fromDetialsAccounting = true;
                        form.editLabel.Visible = true;
                        //form.save.Text = "تعديل الفاتورة";
                        GlobalVar.editBillId = row.Cells[9].Value.ToString();
                        form.ShowDialog();
                    }
                    else if (row.Cells[7].Value.ToString() == "شراء")
                    {
                        GlobalVar.Daily = "تعديل";
                        daily form = new daily();
                        GlobalVar.status_value = true;
                        GlobalVar.fromDetialsAccounting = true;
                        form.editLabel.Visible = true;
                        //form.save.Text = "تعديل الفاتورة";
                        form.billTypeLabel.Text = "شراء";
                        GlobalVar.editBillId = row.Cells[9].Value.ToString();
                        form.ShowDialog();
                    }
                    else
                    {
                        editPayment1 form = new editPayment1();
                        GlobalVar.editBackTo = "detailsAccounting";
                        GlobalVar.fromDetialsAccounting = true;
                        GlobalVar.editPaymentId = row.Cells[9].Value.ToString();
                        form.idTxtbox.Text = GlobalVar.editPaymentId;
                        form.getPaymentDetails();
                        form.ShowDialog();
                        form.getPaymentDetails();
                        form.getPaymentDetails();

                    }
                }
            }
            else if (comboBox1.Text == "الحسومات")
            {
                daily form = new daily();
                indexRow = e.RowIndex;
                if (indexRow != -1)
                {
                    GlobalVar.status_value = true;
                    GlobalVar.fromDetialsAccounting = true;
                    DataGridViewRow row = dataGridView1.Rows[indexRow];
                    GlobalVar.Daily = "تعديل";
                    form.editLabel.Visible = true;
                    //form.save.Text = "تعديل الفاتورة";
                    GlobalVar.editBillId = row.Cells[2].Value.ToString();
                    form.ShowDialog();
                }
            }
            else if (comboBox1.Text == "حسابات الصاغة")
            {
                try
                {
                    indexRow = e.RowIndex;
                    if (indexRow != -1)
                    {

                        DataGridViewRow row = dataGridView1.Rows[indexRow];

                        a = row.Cells[3].Value.ToString();

                        itemNameTxtbox.Visible = true;
                        comboBox1.SelectedItem = "حساب صائغ";
                        System.Threading.Thread.Sleep(10);

                        itemNameTxtbox.SelectedItem = a;
                        itemNameTxtbox.Text = a;

                        show_Click(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (comboBox1.Text == "المصوغات")
            {
                try
                {
                    indexRow = e.RowIndex;
                    if (indexRow != -1)
                    {

                        DataGridViewRow row = dataGridView1.Rows[indexRow];

                        a = row.Cells[4].Value.ToString();

                        itemNameTxtbox.Visible = true;
                        comboBox1.SelectedItem = "حركة مصاغ";
                        System.Threading.Thread.Sleep(10);

                        itemNameTxtbox.SelectedItem = a;
                        itemNameTxtbox.Text = a;

                        show_Click(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (comboBox1.Text == "حركة الصندوق")
            {
                try
                {
                    indexRow = e.RowIndex;
                    if (indexRow != -1)
                    {
                        DataGridViewRow row = dataGridView1.Rows[indexRow];
                        DateTime dd = Convert.ToDateTime(row.Cells[10].Value.ToString());

                        comboBox1.SelectedItem = "القيد اليومي";
                        dateTimePicker1.Value = new DateTime(dd.Year, dd.Month, dd.Day);
                        dateTimePicker2.Value = new DateTime(dd.Year, dd.Month, dd.Day);
                        show_Click(sender, e);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }            
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (comboBox1.Text == "حساب الشركة")
                {
                    indexRow = e.RowIndex;
                    if (indexRow != -1)
                    {
                        DataGridViewRow row = dataGridView2.Rows[indexRow];
                        if (row.Cells[6].Value.ToString() == "بيع")
                        {
                            GlobalVar.Daily = "تعديل";
                            daily form = new daily();
                            GlobalVar.status_value = true;
                            GlobalVar.fromDetialsAccounting = true;
                            form.editLabel.Visible = true;
                            //form.save.Text = "تعديل الفاتورة";
                            GlobalVar.editBillId = row.Cells[7].Value.ToString();
                            form.ShowDialog();
                        }
                        else if (row.Cells[6].Value.ToString() == "شراء")
                        {
                            GlobalVar.Daily = "تعديل";
                            daily form = new daily();
                            GlobalVar.status_value = true;
                            GlobalVar.fromDetialsAccounting = true;
                            form.editLabel.Visible = true;
                            //form.save.Text = "تعديل الفاتورة";
                            form.billTypeLabel.Text = "شراء";
                            GlobalVar.editBillId = row.Cells[7].Value.ToString();
                            form.ShowDialog();
                        }
                        else
                        {
                            editPayment1 form = new editPayment1();
                            GlobalVar.editBackTo = "detailsAccounting";
                            GlobalVar.fromDetialsAccounting = true;
                            GlobalVar.editPaymentId = row.Cells[7].Value.ToString();
                            form.ShowDialog();
                            form.getPaymentDetails();
                            form.getPaymentDetails();
                        }

                    }
                }
                else if (comboBox1.Text == "كشف حساب تفصيلي")
                {
                    indexRow = e.RowIndex;
                    if (indexRow != -1)
                    {
                        DataGridViewRow row = dataGridView2.Rows[indexRow];
                        GlobalVar.Daily = "تعديل";
                        daily form = new daily();
                        GlobalVar.status_value = true;
                        GlobalVar.fromDetialsAccounting = true;
                        form.editLabel.Visible = true;
                        //form.save.Text = "تعديل الفاتورة";
                        GlobalVar.editBillId = row.Cells[5].Value.ToString();
                        form.ShowDialog();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void print_Click(object sender, EventArgs e)
        {
            ii = -1;
            try
            {
               
                print_click();
            }
            catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string d1 = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            string d2 = dateTimePicker2.Value.ToString("dd/MM/yyyy");
            if (comboBox1.Text == "الفواتير")
            {
                printDocument1.DefaultPageSettings.Landscape = false;
                printDocument1.DefaultPageSettings.Landscape = false;
                Font ff = new Font("Arial", 10, FontStyle.Bold);
                Font f = new Font("Arial", 13, FontStyle.Bold);
                Font fz = new Font("Arial", 11, FontStyle.Bold);
                Font fss = new Font("Arial", 12, FontStyle.Bold);

                Font f1 = new Font("Arial", 16, FontStyle.Bold | FontStyle.Underline);
                Font f11 = new Font("Arial", 24, FontStyle.Bold);
                Font f2 = new Font("Arial", 12, FontStyle.Bold);
                Font f3 = new Font("Arial", 14, FontStyle.Bold);

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
                string cashLabel = "الاجور";
                string gold21Label = GlobalVar.gold21Label;
                string gold18Label = GlobalVar.gold18Label;
                string gold14Label = GlobalVar.gold14Label;
                string tCashLabel = "الاجور";
                string strType = "كشف باجمالي فواتير  " + itemNameTxtbox.Text + " في الفترة من  " + d1 + "  إلى  " + d2;
                string user = "منظم الكشف" + Environment.NewLine + "   " + GlobalVar.userName;
                string pLabel2 = "ثقتكم سر نجاحنا";
                string vLabel = "لا يوجد فواتير ليتم طباعتها";

                SizeF sTCashLabel = e.Graphics.MeasureString(tCashLabel, f2);
                SizeF sStrDate = e.Graphics.MeasureString(strDate, f);
                SizeF sStrClock = e.Graphics.MeasureString(strClock, f);
                SizeF sCashLabel = e.Graphics.MeasureString(cashLabel, f2);
                SizeF sStrType = e.Graphics.MeasureString(strType, f2);

                SizeF sGold21Label = e.Graphics.MeasureString(gold21Label, f2);
                SizeF sGold18Label = e.Graphics.MeasureString(gold18Label, f2);
                SizeF sGold14Label = e.Graphics.MeasureString(gold14Label, f2);
                SizeF sPLabel2 = e.Graphics.MeasureString(pLabel2, f1);
                SizeF sUser = e.Graphics.MeasureString(user, f1);
                SizeF sVLabel = e.Graphics.MeasureString(vLabel, f1);

                float margin = 30;
                float shift = 10;

                float col1width = e.PageBounds.Width / 3;
                float col2width = e.PageBounds.Width / 4;
                float col22width = e.PageBounds.Width / 4;
                float col3width = (e.PageBounds.Width - (2 * margin)) / dataGridView1.ColumnCount;

                float table_height = (margin - shift) * (dataGridView1.RowCount + 2);

                float preHeight = (2 * margin) + sStrDate.Height + sStrClock.Height + shift;
                float preHeight1 = preHeight;
                float preHeight11 = preHeight;
                //////////header//////////
                e.HasMorePages = false;
                int noPages = 0;
                int noPages1 = 0;
                int noRows = 0;
                int D = 48;
                noPages1 = (dataGridView1.Rows.Count / D) + 1;
                noRows = (dataGridView1.Rows.Count % D);
                if (noRows > (D - 6))
                {
                    noPages = noPages1 + 1;
                }
                else
                {
                    noPages = noPages1;
                }
                ii++;
                printHeaderFooter(e, noPages, ii);
                ///////////////////
                e.Graphics.DrawString(strDate, f, Brushes.Black, e.PageBounds.Width - margin - sStrDate.Width, 2 * margin + shift / 2);
                e.Graphics.DrawString(strClock, f, Brushes.Black, e.PageBounds.Width - margin - sStrClock.Width, 2 * margin + sStrDate.Height + shift);
                e.Graphics.DrawString(strType, f2, Brushes.Black, (e.PageBounds.Width - (2 * margin) - sStrType.Width) / 2, 2 * margin + sStrDate.Height + sStrClock.Height + shift);
                ///////////////////
                bool last = true;
                if (dataGridView1.Rows.Count > 0)
                {
                    preHeight11 = preHeight1 + (2 * margin);
                    for (; ii < noPages; ii++)
                    {
                        if (noPages - ii > 1) { last = true; }
                        else { last = false; }
                        int SS = 0;
                        printHeaderFooter(e, noPages, ii);
                        if (last)
                        {
                            if (noRows > (D - 6) && noRows < D && (ii + 1) == (noPages - 1))
                                SS = noRows + 1;
                            else
                                SS = D + 1;
                        }
                        else
                            SS = dataGridView1.RowCount - (D * ii) + 1;

                        if (SS < 2) { SS = 0; }
                        table_height = (margin - shift) * SS;
                        /////////جدول بيانات اجمالي الفاتورة/////////////////////////////////////////
                        string A = "";
                        if (table_height != 0)
                        {
                            e.Graphics.DrawRectangle(Pens.Black, margin, preHeight + margin, e.PageBounds.Width - (2 * margin), table_height);
                            e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight + margin + 2, e.PageBounds.Width - (2 * margin) - 2, margin - shift - 3);
                            for (int i = 0 + (D * (ii)); i < D + (D * (ii)) && i < dataGridView1.RowCount; i++)
                            {
                                if (dataGridView1.Rows[i].Cells[4].Value.ToString() == "شراء")
                                    e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight + margin + ((i - (D * ii) + 1) * (2 * shift)) + 2, e.PageBounds.Width - (2 * margin) - 2, margin - shift - 3);
                            }
                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            {
                                A = dataGridView1.Columns[dataGridView1.ColumnCount - j - 1].HeaderText.ToString();
                                SizeF V = e.Graphics.MeasureString(A, f2);
                                float B = e.PageBounds.Width - margin - ((j + 1) * col3width) + ((col3width - V.Width) / 2);
                                if (j != 0)
                                {
                                    if (j != 1)
                                    {
                                        e.Graphics.DrawString(A, f2, Brushes.Black, B, preHeight + (3 * shift));
                                    }
                                    else
                                    {
                                        e.Graphics.DrawString(A, f2, Brushes.Black, B + margin, preHeight + (3 * shift));
                                    }
                                    e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width - margin - ((j + 1) * col3width), preHeight + margin, e.PageBounds.Width - margin - ((j + 1) * col3width), preHeight + margin + table_height);
                                }
                                else
                                {
                                    e.Graphics.DrawString(A, f2, Brushes.Black, B + (margin), preHeight + (3 * shift));
                                    e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width - margin - ((j + 1) * col3width) + (2 * margin), preHeight + margin, e.PageBounds.Width - margin - ((j + 1) * col3width) + (2 * margin), preHeight + margin + table_height);
                                }
                            }
                        }
                        for (int i = 0 + (D * (ii)); i < D + (D * (ii)) && i < dataGridView1.RowCount; i++)
                        {
                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            {

                                if (j == 6)
                                {
                                    A = dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - j - 1].Value.ToString().Substring(0, 10);
                                }
                                else
                                {
                                    A = dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - j - 1].Value.ToString();
                                }
                                SizeF V = e.Graphics.MeasureString(A, f);
                                float B = e.PageBounds.Width - margin - ((j + 1) * col3width) + ((col3width - V.Width) / 2);
                                if (j > 1)
                                {
                                    e.Graphics.DrawString(A, f, Brushes.Black, B, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)));
                                }
                                else
                                {
                                    e.Graphics.DrawString(A, f, Brushes.Black, B + margin, preHeight + margin + ((i - (D * ii) + 1) * (2 * shift)));
                                }

                                e.Graphics.DrawLine(Pens.Black, margin, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)), e.PageBounds.Width - margin, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)));
                            }
                        }
                        float table_height1 = (margin - shift) * 2;
                        float preHeight111 = preHeight + margin + table_height;
                        if (!last)
                        {
                            string fStr = "جدول الاجمالي";
                            SizeF sFStr = e.Graphics.MeasureString(fStr, f3);
                            e.Graphics.DrawString(fStr, f3, Brushes.Black, (e.PageBounds.Width - sFStr.Width + margin) / 2, preHeight111 + 5);
                            ////////////////////////////////////////////////////
                            e.Graphics.DrawRectangle(Pens.Black, margin, preHeight111 + margin, e.PageBounds.Width - (2 * margin) - (shift / 2), table_height1);
                            e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight111 + margin + 2, e.PageBounds.Width - (2 * margin) - (shift / 2) - 2, margin - shift - 3);
                            e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 6, preHeight111 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 6, preHeight111 + margin + table_height1);
                            e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 6 * 2, preHeight111 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 6 * 2, preHeight111 + margin + table_height1);
                            e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 6 * 3, preHeight111 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 6 * 3, preHeight111 + margin + table_height1);
                            e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 6 * 4, preHeight111 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 6 * 4, preHeight111 + margin + table_height1);
                            e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 6 * 5, preHeight111 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 6 * 5, preHeight111 + margin + table_height1);
                            e.Graphics.DrawLine(Pens.Black, margin, preHeight111 + margin + (margin - shift), e.PageBounds.Width - margin - (shift / 2), preHeight111 + margin + (margin - shift));
                            string weight = dataGridView3.Rows[0].Cells[0].Value.ToString() + " غرام";
                            string cash = dataGridView3.Rows[0].Cells[1].Value.ToString() + " ل.س";
                            string buyweight = dataGridView3.Rows[0].Cells[2].Value.ToString() + " غرام";
                            string buycash = dataGridView3.Rows[0].Cells[3].Value.ToString() + " ل.س";
                            string sellweight = dataGridView3.Rows[0].Cells[4].Value.ToString() + " غرام";
                            string sellcash = dataGridView3.Rows[0].Cells[5].Value.ToString() + " ل.س";
                            string l0 = "اجمالي الوزن";
                            string l1 = "اجمالي الاجور";
                            string l2 = "اجمالي وزن الشراء";
                            string l4 = "اجمالي اجور الشراء";
                            string l3 = "اجمالي وزن البيع";
                            string l5 = "اجمالي اجور البيع";
                            SizeF sL0 = e.Graphics.MeasureString(l0, f);
                            SizeF sL1 = e.Graphics.MeasureString(l1, f);
                            SizeF sL2 = e.Graphics.MeasureString(l2, f);
                            SizeF sL3 = e.Graphics.MeasureString(l3, f);
                            SizeF sL4 = e.Graphics.MeasureString(l4, f);
                            SizeF sL5 = e.Graphics.MeasureString(l5, f);
                            SizeF sWeight = e.Graphics.MeasureString(weight, fz);
                            SizeF sCash = e.Graphics.MeasureString(cash, fz);
                            SizeF sBuyWeight = e.Graphics.MeasureString(buyweight, fz);
                            SizeF sBuyCash = e.Graphics.MeasureString(buycash, fz);
                            SizeF sSellWeight = e.Graphics.MeasureString(sellweight, fz);
                            SizeF sSellCash = e.Graphics.MeasureString(sellcash, fz);
                            e.Graphics.DrawString(l0, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sL0.Width) / 2, preHeight111 + margin + 2);
                            e.Graphics.DrawString(l1, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sL1.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6), preHeight111 + margin + 2);
                            e.Graphics.DrawString(l2, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sL2.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6 * 2), preHeight111 + margin + 2);
                            e.Graphics.DrawString(l3, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sL3.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6 * 3), preHeight111 + margin + 2);
                            e.Graphics.DrawString(l4, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sL4.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6 * 4), preHeight111 + margin + 2);
                            e.Graphics.DrawString(l5, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sL5.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6 * 5), preHeight111 + margin + 2);
                            e.Graphics.DrawString(weight, fz, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sWeight.Width) / 2, preHeight111 + margin + (2 * shift) + 3);
                            e.Graphics.DrawString(cash, fz, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sCash.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6) - 2, preHeight111 + margin + (2 * shift) + 3);
                            e.Graphics.DrawString(buyweight, fz, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sBuyWeight.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6 * 2) - 2, preHeight111 + margin + (2 * shift) + 3);
                            e.Graphics.DrawString(buycash, fz, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sBuyCash.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6 * 3) - 2, preHeight111 + margin + (2 * shift) + 3);
                            e.Graphics.DrawString(sellweight, fz, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sSellWeight.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6 * 4) - 2, preHeight111 + margin + (2 * shift) + 3);
                            e.Graphics.DrawString(sellcash, fz, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sSellCash.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6 * 5) - 5, preHeight111 + margin + (2 * shift) + 3);
                        }
                        if (ii == (noPages - 1))
                        {
                            preHeight1 = preHeight111 + (2 * margin) + (2 * shift);
                            ////////////خاتمة الصفحة///////////////////////////
                            e.Graphics.DrawString(pLabel2, f2, Brushes.Black, (e.PageBounds.Width - sPLabel2.Width + margin) / 2, preHeight1);
                            e.Graphics.DrawString(user, f2, Brushes.Black, margin, preHeight1);
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
                    //////////خاتمة الصفحة///////////////////////////
                    e.Graphics.DrawString(pLabel2, f2, Brushes.Black, (e.PageBounds.Width - sPLabel2.Width + margin) / 2, preHeight11 + (4 * margin) - shift);
                    e.Graphics.DrawString(user, f2, Brushes.Black, margin, preHeight11 + (4 * margin) - shift);
                }
            }
            else if (comboBox1.Text == "الدفعات")
            {
                printDocument1.DefaultPageSettings.Landscape = true;
                printDocument1.DefaultPageSettings.Landscape = true;
                Font ff = new Font("Arial", 10, FontStyle.Bold);
                Font f = new Font("Arial", 13, FontStyle.Bold);
                Font fz = new Font("Arial", 11, FontStyle.Bold);
                Font fss = new Font("Arial", 12, FontStyle.Bold);

                Font f1 = new Font("Arial", 16, FontStyle.Bold | FontStyle.Underline);
                Font f11 = new Font("Arial", 24, FontStyle.Bold);
                Font f2 = new Font("Arial", 12, FontStyle.Bold);
                Font f3 = new Font("Arial", 14, FontStyle.Bold);
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
                string cashLabel = "الاجور";
                string gold21Label = GlobalVar.gold21Label;
                string gold18Label = GlobalVar.gold18Label;
                string gold14Label = GlobalVar.gold14Label;
                string tCashLabel = "الاجور";
                string strType = "كشف باجمالي دفعات " + itemNameTxtbox.Text + " في الفترة من  " + d1 + "  إلى  " + d2;
                string user = "منظم الفاتورة" + Environment.NewLine + GlobalVar.userName;
                string pLabel2 = "ثقتكم سر نجاحنا";
                string vLabel = "لا يوجد دفعات ليتم طباعتها";


                SizeF sTCashLabel = e.Graphics.MeasureString(tCashLabel, f2);
                SizeF sStrDate = e.Graphics.MeasureString(strDate, f);
                SizeF sStrClock = e.Graphics.MeasureString(strClock, f);
                SizeF sCashLabel = e.Graphics.MeasureString(cashLabel, f2);
                SizeF sStrType = e.Graphics.MeasureString(strType, f2);

                SizeF sGold21Label = e.Graphics.MeasureString(gold21Label, f2);
                SizeF sGold18Label = e.Graphics.MeasureString(gold18Label, f2);
                SizeF sGold14Label = e.Graphics.MeasureString(gold14Label, f2);
                SizeF sPLabel2 = e.Graphics.MeasureString(pLabel2, f1);
                SizeF sUser = e.Graphics.MeasureString(user, f1);
                SizeF sVLabel = e.Graphics.MeasureString(vLabel, f1);

                float margin = 30;
                float shift = 10;

                float col1width = e.PageBounds.Width / 3;
                float col2width = e.PageBounds.Width / 4;
                float col22width = e.PageBounds.Width / 4;
                float col3width = (e.PageBounds.Width - (2 * margin)) / dataGridView2.ColumnCount;

                float table_height = ((margin - shift) * (dataGridView2.RowCount));

                float preHeight = (2 * margin) + sStrDate.Height + sStrClock.Height + (shift);
                float preHeight1 = preHeight;
                float preHeight11 = preHeight1 + (2 * margin);
                //////////header//////////
                e.HasMorePages = false;
                int noPages = 0;
                int noPages1 = 0;
                int noRows = 0;
                int D = 31;
                noPages1 = (dataGridView2.Rows.Count / D) + 1;
                noRows = (dataGridView2.Rows.Count % D);
                if (noRows > D - 6)
                {
                    noPages = noPages1 + 1;
                }
                else
                {
                    noPages = noPages1;
                }
                ii++;
                printHeaderFooter(e, noPages, ii);
                ///////////////////
                e.Graphics.DrawString(strDate, f, Brushes.Black, e.PageBounds.Width - margin - sStrDate.Width, 2 * margin + shift / 2);
                e.Graphics.DrawString(strClock, f, Brushes.Black, e.PageBounds.Width - margin - sStrClock.Width, 2 * margin + sStrDate.Height + shift);
                e.Graphics.DrawString(strType, f2, Brushes.Black, (e.PageBounds.Width - (2 * margin) - sStrType.Width) / 2, 2 * margin + sStrDate.Height + sStrClock.Height + shift);
                ///////////////////
                bool last = false;
                if (dataGridView2.Rows.Count > 0)
                {
                    for (; ii < noPages; ii++)
                    {
                        if (noPages - ii > 1) { last = true; }
                        else { last = false; }
                        int SS = 0;
                        printHeaderFooter(e, noPages, ii);
                        if (last)
                        {
                            if (noRows > (D - 6) && noRows < D && (ii + 1) == (noPages - 1))
                                SS = noRows + 1;
                            else
                                SS = D + 1;
                        }
                        else
                            SS = dataGridView2.RowCount - (D * ii) + 1;

                        if (SS < 2) { SS = 0; }
                        table_height = (margin - shift) * SS;
                        /////////جدول بيانات اجمالي الفاتورة/////////////////////////////////////////
                        string A = "";
                        if (table_height != 0)
                        {
                            e.Graphics.DrawRectangle(Pens.Black, margin, preHeight + margin, e.PageBounds.Width - (2 * margin), table_height);
                            e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight + margin + 2, e.PageBounds.Width - (2 * margin) - 2, margin - shift - 3);
                            for (int i = 0 + (D * (ii)); i < D + (D * (ii)) && i < dataGridView1.RowCount; i++)
                            {
                                if (dataGridView2.Rows[i].Cells[9].Value.ToString() == "استلام")
                                    e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight + margin + ((i - (D * ii) + 1) * (2 * shift)) + 2, e.PageBounds.Width - (2 * margin) - 2, margin - shift - 3);
                            }
                            for (int j = 0; j < dataGridView2.ColumnCount; j++)
                            {
                                A = dataGridView2.Columns[dataGridView2.ColumnCount - j - 1].HeaderText.ToString();
                                SizeF V = e.Graphics.MeasureString(A, f2);
                                float B = e.PageBounds.Width - margin - ((j + 1) * col3width) + ((col3width - V.Width) / 2);
                                if (j != 0)
                                {
                                    //if (j != 1)
                                    //{
                                    e.Graphics.DrawString(A, f2, Brushes.Black, B, preHeight + (3 * shift));
                                    //}
                                    //else
                                    //{
                                    //    e.Graphics.DrawString(A, f2, Brushes.Black, B + margin, preHeight + (3 * shift));
                                    //}
                                    e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width - margin - ((j + 1) * col3width), preHeight + margin, e.PageBounds.Width - margin - ((j + 1) * col3width), preHeight + margin + table_height);
                                }
                                else
                                {
                                    e.Graphics.DrawString(A, f2, Brushes.Black, B + (margin), preHeight + (3 * shift));
                                    e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width - margin - ((j + 1) * col3width) + (2 * margin) - 15, preHeight + margin, e.PageBounds.Width - margin - ((j + 1) * col3width) + (2 * margin) - 15, preHeight + margin + table_height);
                                }
                            }
                        }
                        for (int i = 0 + (D * (ii)); i < D + (D * (ii)) && i < dataGridView2.RowCount; i++)
                        {
                            for (int j = 0; j < dataGridView2.ColumnCount; j++)
                            {
                                A = dataGridView2.Rows[i].Cells[dataGridView2.ColumnCount - j - 1].Value.ToString();

                                SizeF V = e.Graphics.MeasureString(A, f);
                                float B = e.PageBounds.Width - margin - ((j + 1) * col3width) + ((col3width - V.Width) / 2);
                                if (j == 3)
                                {
                                    A = A.Substring(0, 10);
                                    e.Graphics.DrawString(A, f, Brushes.Black, B + (2 * margin) - 8, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)));
                                }
                                else if (j > 1)
                                {
                                    e.Graphics.DrawString(A, f, Brushes.Black, B, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)));
                                }

                                else
                                {
                                    e.Graphics.DrawString(A, f, Brushes.Black, B + margin, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)));
                                }
                                e.Graphics.DrawLine(Pens.Black, margin, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)), e.PageBounds.Width - margin, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)));
                            }
                        }
                        float table_height1 = ((margin - shift) * 2);
                        float preHeight111 = preHeight + margin + table_height;
                        if (!last)
                        {
                            string fStr = "جدول الاجمالي";
                            SizeF sFStr = e.Graphics.MeasureString(fStr, f3);
                            ////////////////////////////////////////////////////
                            e.Graphics.DrawRectangle(Pens.Black, margin, preHeight111 + margin, e.PageBounds.Width - (2 * margin) - (shift / 2), table_height1);
                            e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight111 + margin + 2, e.PageBounds.Width - (2 * margin) - (shift / 2) - 2, margin - shift - 3);
                            e.Graphics.DrawString(fStr, f3, Brushes.Black, (e.PageBounds.Width - sFStr.Width + margin) / 2, preHeight111);
                            e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 4, preHeight111 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 4, preHeight111 + margin + table_height1);
                            e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 4 * 2, preHeight111 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 4 * 2, preHeight111 + margin + table_height1);
                            e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 4 * 3, preHeight111 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 4 * 3, preHeight111 + margin + table_height1);


                            e.Graphics.DrawLine(Pens.Black, margin, preHeight111 + margin + (margin - shift), e.PageBounds.Width - margin - (shift / 2), preHeight111 + margin + (margin - shift));

                            string gold14 = dataGridView3.Rows[0].Cells[0].Value.ToString() + " غرام";
                            string gold18 = dataGridView3.Rows[0].Cells[1].Value.ToString() + " غرام";
                            string gold21 = dataGridView3.Rows[0].Cells[2].Value.ToString() + " غرام";
                            string cash = dataGridView3.Rows[0].Cells[3].Value.ToString() + " ل.س";


                            string l0 = "اجمالي الاجور";
                            string l1 = "اجمالي الذهب 21";
                            string l2 = "اجمالي الذهب 18";
                            string l3 = "اجمالي الذهب 14";

                            SizeF sL0 = e.Graphics.MeasureString(l0, f);
                            SizeF sL1 = e.Graphics.MeasureString(l1, f);
                            SizeF sL2 = e.Graphics.MeasureString(l2, f);
                            SizeF sL3 = e.Graphics.MeasureString(l3, f);


                            SizeF sGold14 = e.Graphics.MeasureString(gold14, fz);
                            SizeF sCash = e.Graphics.MeasureString(cash, fz);
                            SizeF sGold18 = e.Graphics.MeasureString(gold18, fz);
                            SizeF sGold21 = e.Graphics.MeasureString(gold21, fz);


                            e.Graphics.DrawString(l3, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 4) - sL0.Width) / 2, preHeight111 + margin + 2);
                            e.Graphics.DrawString(l2, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 4) - sL1.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 4), preHeight111 + margin + 2);
                            e.Graphics.DrawString(l1, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 4) - sL2.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 4 * 2), preHeight111 + margin + 2);
                            e.Graphics.DrawString(l0, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 4) - sL3.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 4 * 3), preHeight111 + margin + 2);


                            e.Graphics.DrawString(gold14, fz, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 4) - sGold14.Width) / 2, preHeight111 + margin + (2 * shift));
                            e.Graphics.DrawString(gold18, fz, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 4) - sGold18.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 4) - 2, preHeight111 + margin + (2 * shift));
                            e.Graphics.DrawString(gold21, fz, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 4) - sGold21.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 4 * 2) - 2, preHeight111 + margin + (2 * shift));
                            e.Graphics.DrawString(cash, fz, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 4) - sCash.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 4 * 3) - 2, preHeight111 + margin + (2 * shift));

                        }
                        if (ii == (noPages - 1))
                        {
                            preHeight1 = preHeight111 + (2 * margin) + (2 * shift);
                            ////////////خاتمة الصفحة///////////////////////////
                            e.Graphics.DrawString(pLabel2, f2, Brushes.Black, (e.PageBounds.Width - sPLabel2.Width + margin) / 2, preHeight1);
                            e.Graphics.DrawString(user, f2, Brushes.Black, margin, preHeight1);
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
                    e.Graphics.DrawString(vLabel, f11, Brushes.Black, (e.PageBounds.Width / 2) - (sVLabel.Width - (2 * margin) / 2), preHeight1 - 2 * margin);
                    //////////خاتمة الصفحة///////////////////////////
                    e.Graphics.DrawString(pLabel2, f2, Brushes.Black, (e.PageBounds.Width - sPLabel2.Width + margin) / 2, preHeight1 - (2 * margin));
                    e.Graphics.DrawString(user, f2, Brushes.Black, margin, preHeight1 - (2 * margin));
                }
            }
            else if (comboBox1.Text == "المصوغات" || comboBox1.Text == "حسابات الصاغة" || comboBox1.Text == "حساب صائغ" || comboBox1.Text == "الحسومات" || comboBox1.Text == "حركة مصاغ")
            {
                printDocument1.DefaultPageSettings.Landscape = false;
                printDocument1.DefaultPageSettings.Landscape = false;
                Font ff = new Font("Arial", 10, FontStyle.Bold);
                Font f = new Font("Arial", 13, FontStyle.Bold);
                Font fss = new Font("Arial", 12, FontStyle.Bold);
                Font fz = new Font("Arial", 11, FontStyle.Bold);

                Font f1 = new Font("Arial", 16, FontStyle.Bold | FontStyle.Underline);
                Font f11 = new Font("Arial", 24, FontStyle.Bold);
                Font f2 = new Font("Arial", 12, FontStyle.Bold);
                Font f3 = new Font("Arial", 14, FontStyle.Bold);
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
                string gold21Label = GlobalVar.gold21Label;
                string gold18Label = GlobalVar.gold18Label;
                string gold14Label = GlobalVar.gold14Label;
                string cashLabel = GlobalVar.cashLabel;

                string fgold21Label = "ذهب21 لنا";
                string fgold14Label = "ذهب14 لنا";
                string fcashLabel = "اجور لنا";
                string mgold21Label = "ذهب21 له";
                string mgold14Label = "ذهب14 له";
                string mcashLabel = "اجور له";
                string vLabel = "";
                string strType = "";

                if (comboBox1.Text == "المصوغات")
                {
                    strType = "كشف باجمالي بالمصوغات يوم  " + d2;
                    vLabel = "لا يوجد مصوغات ليتم طباعتها";
                }
                else if (comboBox1.Text == "حساب صائغ")
                {
                    printDocument1.DefaultPageSettings.Landscape = true;
                    printDocument1.DefaultPageSettings.Landscape = true;
                    strType = "كشف حساب صائغ  " + itemNameTxtbox.Text + "خلال الفترة من  " + d1 + " إلى " + d2;
                    vLabel = "لا يوجد معلومات ليتم طباعتها";
                }
                else if (comboBox1.Text == "حسابات الصاغة")
                {
                    strType = "كشف باجمالي حسابات الصاغة يوم  " + d2;
                    vLabel = "لا يوجد معلومات ليتم طباعتها";
                }
                else if (comboBox1.Text == "الحسومات")
                {
                    strType = "كشف باجمالي الحسومات الممنوحة لـِ " + itemNameTxtbox.Text + "خلال الفترة من " + d1 + "إلى" + d2;
                    vLabel = "لا يوجد حسومات ليتم طباعتها";
                }
                else if (comboBox1.Text == "حركة مصاغ")
                {
                    strType = "كشف حساب للمصاغ  " + itemNameTxtbox.Text + "خلال الفترة من " + d1 + "إلى" + d2;
                    vLabel = "لا يوجد مصوغات ليتم طباعتها";
                }
                string user = "منظم الكشف" + Environment.NewLine + GlobalVar.userName;
                string pLabel2 = "ثقتكم سر نجاحنا";



                SizeF sStrDate = e.Graphics.MeasureString(strDate, f);
                SizeF sStrClock = e.Graphics.MeasureString(strClock, f);
                SizeF sStrType = e.Graphics.MeasureString(strType, f2);

                SizeF sGold21Label = e.Graphics.MeasureString(gold21Label, f2);
                SizeF sGold18Label = e.Graphics.MeasureString(gold18Label, f2);
                SizeF sGold14Label = e.Graphics.MeasureString(gold14Label, f2);
                SizeF sCashLabel = e.Graphics.MeasureString(cashLabel, f2);
                SizeF sPLabel2 = e.Graphics.MeasureString(pLabel2, f1);
                SizeF sUser = e.Graphics.MeasureString(user, f1);
                SizeF sVLabel = e.Graphics.MeasureString(vLabel, f1);

                float margin = 30;
                float shift = 10;

                float col1width = e.PageBounds.Width / 3;
                float col2width = e.PageBounds.Width / 4;
                float col22width = e.PageBounds.Width / 4;
                float col3width = (e.PageBounds.Width - (2 * margin)) / dataGridView2.ColumnCount;

                float table_height = ((margin - shift) * (dataGridView2.RowCount + 2));
                float preHeight = margin + sStrDate.Height + sStrClock.Height + (2 * shift);
                float preHeight1 = preHeight;
                float preHeight11 = preHeight1;
                //////////header//////////
                e.HasMorePages = false;
                int noPages = 0;
                int noPages1 = 0;
                int noRows = 0;
                int All = 31;
                if (comboBox1.Text == "المصوغات"|| comboBox1.Text == "حسابات الصاغة") { All = 48; }
                noPages1 = (dataGridView2.Rows.Count / All) + 1;
                noRows = (dataGridView2.Rows.Count % All);

                if (noRows > All - 6)
                {
                    noPages = noPages1 + 1;
                }
                else
                {
                    noPages = noPages1;
                }
                ii++;
                int SS = 0;
                printHeaderFooter(e, noPages, ii);
                ///////////////////
                e.Graphics.DrawString(strDate, f, Brushes.Black, e.PageBounds.Width - margin - sStrDate.Width, 2 * margin + shift / 2);
                e.Graphics.DrawString(strClock, f, Brushes.Black, e.PageBounds.Width - margin - sStrClock.Width, 2 * margin + sStrDate.Height + shift);
                e.Graphics.DrawString(strType, f2, Brushes.Black, ((e.PageBounds.Width - (2 * margin) - sStrType.Width) / 2), margin + sStrDate.Height + sStrClock.Height + 3 * shift);
                ///////////////////
                bool last = false;
                if (dataGridView2.Rows.Count > 0)
                {
                    for (; ii < noPages; ii++)
                    {
                        if (noPages - ii > 1) { last = true; }
                        else { last = false; }
                        printHeaderFooter(e, noPages, ii);
                        if (last)
                        {
                            if (noRows > (All - 6) && noRows < All && (ii + 1) == (noPages - 1))
                                SS = noRows + 1;
                            else
                                SS = All + 1;
                        }
                        else
                            SS = dataGridView1.RowCount - (All * ii) + 1;

                        if (SS < 2) { SS = 0; }
                        table_height = (margin - shift) * SS;
                        /////////جدول بيانات اجمالي الفاتورة/////////////////////////////////////////
                        string A = "";
                        if (table_height != 0)
                        {

                            e.Graphics.DrawRectangle(Pens.Black, margin, preHeight + margin, e.PageBounds.Width - (2 * margin) - (shift / 2), table_height + (margin - shift));
                            e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight + margin + 2, e.PageBounds.Width - (2 * margin) - (shift / 2) - 2, margin - shift - 3);
                            if (comboBox1.Text == "حساب صائغ")
                            for (int i = 0 + (All * (ii)); i < All + (All * (ii)) && i < dataGridView1.RowCount; i++)
                                {
                                    if (dataGridView2.Rows[i].Cells[10].Value.ToString() == "استلام" || dataGridView2.Rows[i].Cells[10].Value.ToString() == "شراء")
                                        e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight + margin + ((i - (All * ii) + 1) * (2 * shift)) + 2, e.PageBounds.Width - (2 * margin) - 7, margin - shift - 3);
                                }
                            if(comboBox1.Text=="حسابات الصاغة")
                            for (int i = 0 + (All * (ii)); i < All + (All * (ii)) && i < dataGridView1.RowCount; i++)
                                {
                                    if (i%2 == 1 )
                                        e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight + margin + ((i - (All * ii) + 1) * (2 * shift)) + 2, e.PageBounds.Width - (2 * margin) - 7, margin - shift - 3);
                                }
                            for (int j = 0; j < dataGridView2.ColumnCount; j++)
                            {
                                A = dataGridView2.Columns[dataGridView2.ColumnCount - j - 1].HeaderText.ToString();
                                SizeF V = e.Graphics.MeasureString(A, f2);
                                float BB = e.PageBounds.Width - margin - ((j + 1) * col3width);
                                float B = e.PageBounds.Width  - ((j + 1) * col3width) + ((col3width - V.Width) / 2)+margin+10;
                                if (comboBox1.Text == "حساب صائغ")
                                {
                                    if (j == 0)
                                    {
                                        //float bb = e.PageBounds.Width - (2 * margin) - (margin / 2);
                                        //e.Graphics.DrawLine(Pens.Black, bb, preHeight + margin, bb, preHeight + margin + table_height + (margin - shift));
                                        //e.Graphics.DrawString(A, f2, Brushes.Black, bb + 25, preHeight + (3 * shift));
                                    }
                                    else if (j == 1||j==2||j==3||j==4||j==5||j==6|| j == 7 || j == 8 || j == 9 || j == 10)
                                    {
                                        e.Graphics.DrawLine(Pens.Black, B , preHeight + margin, B , preHeight + margin + table_height + (margin - shift));
                                        e.Graphics.DrawString(A, f2, Brushes.Black, B + (1 * margin)-10, preHeight + (3 * shift));
                                    }                                  
                                    else
                                    {
                                        e.Graphics.DrawString(A, f2, Brushes.Black, B -20, preHeight + (3 * shift));
                                    }
                                }
                                else if (comboBox1.Text == "الحسومات")
                                {
                                    if (j == 0)
                                    {
                                        float bb = e.PageBounds.Width - (2 * margin) - (margin / 2);
                                        e.Graphics.DrawLine(Pens.Black, bb - 20, preHeight + margin, bb - 20, preHeight + margin + table_height + (margin - shift));
                                        e.Graphics.DrawString(A, f2, Brushes.Black, bb, preHeight + (3 * shift));
                                    }
                                    else
                                    {
                                        if (j != dataGridView2.ColumnCount - 1)
                                        {
                                            e.Graphics.DrawLine(Pens.Black, BB + margin + 10, preHeight + margin, BB + margin + 10, preHeight + margin + table_height + (margin - shift));
                                            e.Graphics.DrawString(A, f2, Brushes.Black, B - margin, preHeight + (3 * shift));
                                        }
                                        else
                                        {
                                            e.Graphics.DrawString(A, f2, Brushes.Black, B - margin, preHeight + (3 * shift));
                                        }
                                    }
                                }
                                else
                                {
                                    e.Graphics.DrawLine(Pens.Black, BB, preHeight + margin, BB, preHeight + margin + table_height + (margin - shift));
                                    e.Graphics.DrawString(A, f2, Brushes.Black, B - margin -15, preHeight + (3 * shift));
                                }
                            }
                        }
                        for (int i = 0 + (All * (ii)); i < All + (All * (ii)) && i < dataGridView2.RowCount; i++)
                        {
                            for (int j = 0; j < dataGridView2.ColumnCount; j++)
                            {
                                A = dataGridView2.Rows[i].Cells[dataGridView2.ColumnCount - j - 1].Value.ToString();
                                if (comboBox1.Text == "حسابات الصاغة")
                                {
                                    if (j != 0 && j != 4)
                                    {
                                        if (Convert.ToDecimal(A) < 0)
                                        {
                                            A = A + "  له";
                                        }
                                        else if (Convert.ToDecimal(A) == 0)
                                        { }
                                        else
                                        {
                                            A = A + "  لنا";
                                        }
                                    }
                                }
                                else if (comboBox1.Text == "حركة مصاغ")
                                {
                                    if (j == 3)
                                    {
                                        if (A != "بيع" && A != "شراء" && A != "استلام" && A != "تسليم")
                                        {
                                            A = "دفعة راس مال";
                                        }
                                    }
                                }
                                SizeF V = e.Graphics.MeasureString(A, f);
                                float B = e.PageBounds.Width - margin - ((j + 1) * col3width) + ((col3width - V.Width) / 2);

                                if (comboBox1.Text == "المصوغات")
                                {
                                    if (j == 0)
                                        e.Graphics.DrawString(A.Substring(0, A.Length - 8), f, Brushes.Black, B + margin, 2 + preHeight + margin + (((i - (All * (ii))) + 1) * (2 * shift)));
                                    else
                                        e.Graphics.DrawString(A, f, Brushes.Black, B, 2 + preHeight + margin + (((i - (All * (ii))) + 1) * (2 * shift)));
                                }
                                else if (comboBox1.Text == "حركة مصاغ")
                                {
                                    if (j == 0)
                                        e.Graphics.DrawString(A.Substring(0, A.Length - 10), f, Brushes.Black, B + margin + 10, 2 + preHeight + margin + (((i - (All * (ii))) + 1) * (2 * shift)));
                                    else
                                        e.Graphics.DrawString(A, f, Brushes.Black, B, 2 + preHeight + margin + (((i - (All * (ii))) + 1) * (2 * shift)));
                                }
                                else if (comboBox1.Text == "الحسومات")
                                {
                                    if (j != 2)
                                    {
                                        if (j == dataGridView2.ColumnCount - 1)
                                            e.Graphics.DrawString(A, f, Brushes.Black, B + 15, 2 + preHeight + margin + (((i - (All * (ii))) + 1) * (2 * shift)));
                                        else if (j == 0)
                                            e.Graphics.DrawString(A, f, Brushes.Black, B + margin + 10, 2 + preHeight + margin + (((i - (All * (ii))) + 1) * (2 * shift)));
                                        else if (j == 1)
                                            e.Graphics.DrawString(A, f, Brushes.Black, B + margin + 10, 2 + preHeight + margin + (((i - (All * (ii))) + 1) * (2 * shift)));
                                        else
                                            e.Graphics.DrawString(A, f, Brushes.Black, B + margin, 2 + preHeight + margin + (((i - (All * (ii))) + 1) * (2 * shift)));
                                    }
                                    else
                                    {
                                        e.Graphics.DrawString(A.Substring(0, 10), f, Brushes.Black, B + margin + 50, 2 + preHeight + margin + (((i - (All * (ii))) + 1) * (2 * shift)));
                                    }

                                }
                                else if (comboBox1.Text == "حساب صائغ")
                                {
                                    if (j == 11)
                                    {
                                        e.Graphics.DrawString(A, f, Brushes.Black, B +40 , 2 + preHeight + margin + (((i - (All * (ii))) + 1) * (2 * shift)));
                                    }

                                    else if (j == 0)
                                    {
                                        //e.Graphics.DrawString(A, f, Brushes.Black, B + margin-5, 2 + preHeight + margin + (((i - (All * (ii))) + 1) * (2 * shift)));
                                    }

                                    else if (j != 10)
                                    {
                                        e.Graphics.DrawString(A, f, Brushes.Black, B + 3*margin-10, 2 + preHeight + margin + (((i - (All * (ii))) + 1) * (2 * shift)));
                                    }
                                    else
                                    {
                                        e.Graphics.DrawString(A.Substring(0, 10), f, Brushes.Black, B + 4*margin+15, 2 + preHeight + margin + (((i - (All * (ii))) + 1) * (2 * shift)));
                                    }
                                }
                                else
                                {
                                    e.Graphics.DrawString(A, f, Brushes.Black, B, 2 + preHeight + margin + (((i - (All * (ii))) + 1) * (2 * shift)));
                                }
                                e.Graphics.DrawLine(Pens.Black, margin, preHeight + margin + (((i - (All * (ii))) + 1) * (2 * shift)), e.PageBounds.Width - margin - (shift / 2), preHeight + margin + (((i - (All * (ii))) + 1) * (2 * shift)));
                            }
                        }
                        if (ii == (noPages - 1))
                        {
                            SS = (dataGridView2.RowCount - (All * (ii)));
                            if (SS < 0) { SS = 1; }
                            table_height = ((margin - shift) * SS);
                            preHeight1 = preHeight + (3 * margin) + table_height;
                            float preHeight2 = preHeight1 + (2 * margin);
                            preHeight11 = preHeight2 + (2 * margin);
                            if (comboBox1.Text == "المصوغات")
                            {

                                float table_height1 = ((margin - shift) * 2);

                                string fStr = "اجمالي اوزان الذهب بحسب العيارات";
                                SizeF sFStr = e.Graphics.MeasureString(fStr, f3);
                                ////////////////////////////////////////////////////
                                e.Graphics.DrawRectangle(Pens.Black, margin, preHeight1 + margin, e.PageBounds.Width - (2 * margin), table_height1);
                                e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight1 + margin + 2, e.PageBounds.Width - (2 * margin) - 2, margin - shift - 3);
                                e.Graphics.DrawString(fStr, f3, Brushes.Black, (e.PageBounds.Width - sFStr.Width + margin) / 2, preHeight1);
                                e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 3, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 3, preHeight1 + margin + table_height1);
                                e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 3 * 2, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 3 * 2, preHeight1 + margin + table_height1);
                                e.Graphics.DrawLine(Pens.Black, margin, preHeight1 + margin + (margin - shift), e.PageBounds.Width - margin, preHeight1 + margin + (margin - shift));

                                string gold14 = dataGridView3.Rows[0].Cells[0].Value.ToString() + " غرام";
                                string gold18 = dataGridView3.Rows[0].Cells[1].Value.ToString() + " غرام";
                                string gold21 = dataGridView3.Rows[0].Cells[2].Value.ToString() + " غرام";

                                SizeF sGold14 = e.Graphics.MeasureString(gold14, f);
                                SizeF sGold18 = e.Graphics.MeasureString(gold18, f);
                                SizeF sGold21 = e.Graphics.MeasureString(gold21, f);

                                e.Graphics.DrawString(gold14Label, f, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 3) - sGold14Label.Width) / 2, preHeight1 + margin + 2);
                                e.Graphics.DrawString(gold18Label, f, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 3) - sGold18Label.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 3), preHeight1 + margin + 2);
                                e.Graphics.DrawString(gold21Label, f, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 3) - sGold21Label.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 3 * 2), preHeight1 + margin + 2);
                                e.Graphics.DrawString(gold14, f, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 3) - sGold14.Width) / 2, preHeight1 + margin + (2 * shift));
                                e.Graphics.DrawString(gold18, f, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 3) - sGold18.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 3), preHeight1 + margin + (2 * shift));
                                e.Graphics.DrawString(gold21, f, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 3) - sGold21.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 3 * 2), preHeight1 + margin + (2 * shift));
                            }
                            else if (comboBox1.Text == "حساب صائغ" || comboBox1.Text == "حركة مصاغ")
                            {
                                float table_height1 = ((margin - shift) * 2);
                                string fStr = "";
                                if (comboBox1.Text == "حساب صائغ")
                                {
                                    fStr = "اجمالي حساب الصائغ " + itemNameTxtbox.Text + " بتاريخ " + d2;
                                }
                                else
                                {
                                    fStr = "بيان المصاغ " + itemNameTxtbox.Text + " بتاريخ " + d2;
                                }
                                SizeF sFStr = e.Graphics.MeasureString(fStr, f3);
                                ////////////////////////////////////////////////////
                                string gold14;
                                string gold18;
                                string gold21;
                                string cash;
                                if (comboBox1.Text == "حساب صائغ")
                                {
                                    e.Graphics.DrawString(fStr, f3, Brushes.Black, (e.PageBounds.Width - sFStr.Width + margin) / 2, preHeight1);
                                    ////////////////////////////////////////////////////
                                    e.Graphics.DrawRectangle(Pens.Black, margin, preHeight1 + margin, e.PageBounds.Width - (2 * margin) - (shift / 2), table_height1);
                                    e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight1 + margin + 2, e.PageBounds.Width - (2 * margin) - (shift / 2) - 2, margin - shift - 3);
                                    e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 8, preHeight1 + margin + table_height1);
                                    e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 2, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 2, preHeight1 + margin + table_height1);
                                    e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 3, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 3, preHeight1 + margin + table_height1);
                                    e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 4, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 4, preHeight1 + margin + table_height1);
                                    e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 5, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 5, preHeight1 + margin + table_height1);
                                    e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 6, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 6, preHeight1 + margin + table_height1);
                                    e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 7, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 7, preHeight1 + margin + table_height1);
                                    gold14 = "";
                                    gold18 = "";
                                    gold21 = "";
                                    cash = "";
                                    string ogold14 = "";
                                    string ogold18 = "";
                                    string ogold21 = "";
                                    string ocash = "";

                                    e.Graphics.DrawLine(Pens.Black, margin, preHeight1 + margin + (margin - shift), e.PageBounds.Width - margin - (shift / 2), preHeight1 + margin + (margin - shift));

                                    if (dataGridView3.Rows[0].Cells[0].Value.ToString() != "رصيد ذهب14")
                                    {
                                        if (Convert.ToDecimal(dataGridView3.Rows[0].Cells[0].Value.ToString()) > 0 && dataGridView3.Rows[0].Cells[0].Value.ToString() != "رصيد ذهب14")
                                            gold14 = " لنا " + dataGridView3.Rows[0].Cells[0].Value.ToString() + " غرام";
                                        else
                                            gold14 = " له " + dataGridView3.Rows[0].Cells[0].Value.ToString() + " غرام";
                                    }
                                    else
                                    {
                                        gold14 = "رصيد";
                                    }
                                    if (dataGridView3.Rows[0].Cells[1].Value.ToString() != "رصيد ذهب18")
                                    {
                                        if (Convert.ToDecimal(dataGridView3.Rows[0].Cells[1].Value.ToString()) > 0 && dataGridView3.Rows[0].Cells[1].Value.ToString() != "رصيد ذهب18")
                                            gold18 = " لنا " + dataGridView3.Rows[0].Cells[1].Value.ToString() + " غرام";
                                        else
                                            gold18 = " له " + dataGridView3.Rows[0].Cells[1].Value.ToString() + " غرام";
                                    }
                                    else
                                    {
                                        gold18 = "رصيد";
                                    }
                                    if (dataGridView3.Rows[0].Cells[2].Value.ToString() != "رصيد ذهب21")
                                    {
                                        if (Convert.ToDecimal(dataGridView3.Rows[0].Cells[2].Value.ToString()) > 0 && dataGridView3.Rows[0].Cells[2].Value.ToString() != "رصيد ذهب21")
                                            gold21 = " لنا " + dataGridView3.Rows[0].Cells[2].Value.ToString() + " غرام";
                                        else
                                            gold21 = " له " + dataGridView3.Rows[0].Cells[2].Value.ToString() + " غرام";
                                    }
                                    else
                                    {
                                        gold21 = "رصيد";
                                    }
                                    if (dataGridView3.Rows[0].Cells[3].Value.ToString() != "رصيد اجور")
                                    {
                                        if (Convert.ToDecimal(dataGridView3.Rows[0].Cells[3].Value.ToString()) > 0 && dataGridView3.Rows[0].Cells[3].Value.ToString() != "رصيد اجور")
                                            cash = " لنا " + dataGridView3.Rows[0].Cells[3].Value.ToString() + " ل.س";
                                        else
                                            cash = " له " + dataGridView3.Rows[0].Cells[3].Value.ToString() + " ل.س";
                                    }
                                    else
                                    {
                                        cash = "رصيد";
                                    }
                                    if (dataGridView3.Rows[0].Cells[4].Value.ToString() != "رصيد ذهب14")
                                    {
                                        if (Convert.ToDecimal(dataGridView3.Rows[0].Cells[4].Value.ToString()) > 0 && dataGridView3.Rows[0].Cells[4].Value.ToString() != "رصيد ذهب14")
                                            ogold14 = " لنا " + dataGridView3.Rows[0].Cells[4].Value.ToString() + " غرام";
                                        else
                                            ogold14 = " له " + dataGridView3.Rows[0].Cells[4].Value.ToString() + " غرام";
                                    }
                                    else
                                    {
                                        ogold14 = "رصيد";
                                    }
                                    if (dataGridView3.Rows[0].Cells[5].Value.ToString() != "رصيد ذهب18")
                                    {
                                        if (Convert.ToDecimal(dataGridView3.Rows[0].Cells[5].Value.ToString()) > 0 && dataGridView3.Rows[0].Cells[5].Value.ToString() != "رصيد ذهب18")
                                            ogold18 = " لنا " + dataGridView3.Rows[0].Cells[5].Value.ToString() + " غرام";
                                        else
                                            ogold18 = " له " + dataGridView3.Rows[0].Cells[5].Value.ToString() + " غرام";
                                    }
                                    else
                                    {
                                        ogold18 = "رصيد";
                                    }
                                    if (dataGridView3.Rows[0].Cells[6].Value.ToString() != "رصيد ذهب21")
                                    {
                                        if (Convert.ToDecimal(dataGridView3.Rows[0].Cells[6].Value.ToString()) > 0)
                                            ogold21 = " لنا " + dataGridView3.Rows[0].Cells[6].Value.ToString() + " غرام";
                                        else
                                            ogold21 = " له " + dataGridView3.Rows[0].Cells[6].Value.ToString() + " غرام";
                                    }
                                    else
                                    {
                                        ogold21 = "رصيد";
                                    }
                                    if (dataGridView3.Rows[0].Cells[7].Value.ToString() != "رصيد اجور")
                                    {
                                        if (Convert.ToDecimal(dataGridView3.Rows[0].Cells[7].Value.ToString()) > 0 && dataGridView3.Rows[0].Cells[7].Value.ToString() != "رصيد اجور")
                                            ocash = " لنا " + dataGridView3.Rows[0].Cells[7].Value.ToString() + " ل.س";
                                        else
                                            ocash = " له " + dataGridView3.Rows[0].Cells[7].Value.ToString() + " ل.س";
                                    }
                                    else
                                    {
                                        ocash = "رصيد";
                                    }

                                    string l0 = "الاجور";
                                    string l1 = "الذهب 21";
                                    string l2 = "الذهب 18";
                                    string l3 = "الذهب 14";

                                    SizeF sL0 = e.Graphics.MeasureString(l0, f);
                                    SizeF sL1 = e.Graphics.MeasureString(l1, f);
                                    SizeF sL2 = e.Graphics.MeasureString(l2, f);
                                    SizeF sL3 = e.Graphics.MeasureString(l3, f);

                                    string l00 = "بدابة الاجور";
                                    string l01 = "بداية الذهب 21";
                                    string l02 = "بداية الذهب 18";
                                    string l03 = "بداية الذهب 14";

                                    SizeF sL00 = e.Graphics.MeasureString(l00, f);
                                    SizeF sL01 = e.Graphics.MeasureString(l01, f);
                                    SizeF sL02 = e.Graphics.MeasureString(l02, f);
                                    SizeF sL03 = e.Graphics.MeasureString(l03, f);

                                    SizeF sGold14 = e.Graphics.MeasureString(gold14, fz);
                                    SizeF sCash = e.Graphics.MeasureString(cash, fz);
                                    SizeF sGold18 = e.Graphics.MeasureString(gold18, fz);
                                    SizeF sGold21 = e.Graphics.MeasureString(gold21, fz);
                                    SizeF soGold14 = e.Graphics.MeasureString(ogold14, fz);
                                    SizeF soCash = e.Graphics.MeasureString(ocash, fz);
                                    SizeF soGold18 = e.Graphics.MeasureString(ogold18, fz);
                                    SizeF soGold21 = e.Graphics.MeasureString(ogold21, fz);

                                    e.Graphics.DrawString(l3, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL0.Width) / 2, preHeight1 + margin + 2);
                                    e.Graphics.DrawString(l2, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL1.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8), preHeight1 + margin + 2);
                                    e.Graphics.DrawString(l1, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL2.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 2), preHeight1 + margin + 2);
                                    e.Graphics.DrawString(l0, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL3.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 3), preHeight1 + margin + 2);
                                    e.Graphics.DrawString(l03, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL1.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 4) - 20, preHeight1 + margin + 2);
                                    e.Graphics.DrawString(l02, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL2.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 5) - 20, preHeight1 + margin + 2);
                                    e.Graphics.DrawString(l01, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL3.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 6) - 20, preHeight1 + margin + 2);
                                    e.Graphics.DrawString(l00, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL3.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 7) - 20, preHeight1 + margin + 2);

                                    e.Graphics.DrawString(gold14, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sGold14.Width) / 2 - 15, preHeight1 + margin + (2 * shift));
                                    e.Graphics.DrawString(gold18, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sGold18.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8) - 17, preHeight1 + margin + (2 * shift));
                                    e.Graphics.DrawString(gold21, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sGold21.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 2) - 13, preHeight1 + margin + (2 * shift));
                                    e.Graphics.DrawString(cash, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sCash.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 3) - 10, preHeight1 + margin + (2 * shift));
                                    e.Graphics.DrawString(ogold14, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - soGold14.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 4) - 25, preHeight1 + margin + (2 * shift));
                                    e.Graphics.DrawString(ogold18, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - soGold18.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 5) - 7, preHeight1 + margin + (2 * shift));
                                    e.Graphics.DrawString(ogold21, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - soGold21.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 6) - 7, preHeight1 + margin + (2 * shift));
                                    e.Graphics.DrawString(ocash, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - soCash.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 7) - 7, preHeight1 + margin + (2 * shift));
                                }
                                else
                                {
                                    e.Graphics.DrawRectangle(Pens.Black, margin, preHeight1 + margin, e.PageBounds.Width - (2 * margin), table_height1);
                                    e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight1 + margin + 2, e.PageBounds.Width - (2 * margin) - 2, margin - shift - 3);
                                    e.Graphics.DrawString(fStr, f3, Brushes.Black, (e.PageBounds.Width - sFStr.Width + margin) / 2, preHeight1);
                                    e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 4, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 4, preHeight1 + margin + table_height1);
                                    e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 4 * 2, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 4 * 2, preHeight1 + margin + table_height1);
                                    e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 4 * 3, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 4 * 3, preHeight1 + margin + table_height1);
                                    e.Graphics.DrawLine(Pens.Black, margin, preHeight1 + margin + (margin - shift), e.PageBounds.Width - margin, preHeight1 + margin + (margin - shift));

                                    gold14Label = "اجور المصاغ";
                                    gold18Label = "عدد القطع";
                                    gold21Label = "الوزن المتوفر";
                                    cashLabel = "العيار";
                                    sGold14Label = e.Graphics.MeasureString(gold14Label, f);
                                    sGold18Label = e.Graphics.MeasureString(gold18Label, f);
                                    sGold21Label = e.Graphics.MeasureString(gold21Label, f);
                                    sCashLabel = e.Graphics.MeasureString(cashLabel, f);
                                    gold14 = dataGridView3.Rows[0].Cells[0].Value.ToString() + " ل.س";
                                    gold18 = dataGridView3.Rows[0].Cells[1].Value.ToString() + " قطعة";
                                    gold21 = dataGridView3.Rows[0].Cells[2].Value.ToString() + " غرام";
                                    cash = dataGridView3.Rows[0].Cells[6].Value.ToString();

                                    SizeF sGold14 = e.Graphics.MeasureString(gold14, f);
                                    SizeF sGold18 = e.Graphics.MeasureString(gold18, f);
                                    SizeF sGold21 = e.Graphics.MeasureString(gold21, f);
                                    SizeF sCash = e.Graphics.MeasureString(cash, f);

                                    e.Graphics.DrawString(gold14Label, f, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 4) - sGold14Label.Width) / 2, preHeight1 + margin + 2);
                                    e.Graphics.DrawString(gold18Label, f, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 4) - sGold18Label.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 4), preHeight1 + margin + 2);
                                    e.Graphics.DrawString(gold21Label, f, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 4) - sGold21Label.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 4 * 2), preHeight1 + margin + 2);
                                    e.Graphics.DrawString(cashLabel, f, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 4) - sCashLabel.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 4 * 3), preHeight1 + margin + 2);
                                    e.Graphics.DrawString(gold14, f, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 4) - sGold14.Width) / 2, preHeight1 + margin + (2 * shift));
                                    e.Graphics.DrawString(gold18, f, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 4) - sGold18.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 4), preHeight1 + margin + (2 * shift));
                                    e.Graphics.DrawString(gold21, f, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 4) - sGold21.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 4 * 2), preHeight1 + margin + (2 * shift));
                                    e.Graphics.DrawString(cash, f, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 4) - sCash.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 4 * 3), preHeight1 + margin + (2 * shift));
                                }
                            }
                            else if (comboBox1.Text == "الحسومات")
                            {
                                float table_height1 = ((margin - shift) * 2);

                                string fStr = "اجمالي الحسومات";
                                SizeF sFStr = e.Graphics.MeasureString(fStr, f3);
                                ////////////////////////////////////////////////////
                                e.Graphics.DrawString(fStr, f3, Brushes.Black, (e.PageBounds.Width - sFStr.Width + margin) / 2, preHeight1);
                                sFStr = e.Graphics.MeasureString(fStr, f3);
                                ////////////////////////////////////////////////////
                                e.Graphics.DrawRectangle(Pens.Black, margin, preHeight1 + margin, e.PageBounds.Width - (2 * margin) - (shift / 2), table_height1);
                                e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight1 + margin + 2, e.PageBounds.Width - (2 * margin) - (shift / 2) - 2, margin - shift - 3);
                                e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 8, preHeight1 + margin + table_height1);
                                e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 2, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 2, preHeight1 + margin + table_height1);
                                e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 3, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 3, preHeight1 + margin + table_height1);
                                e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 4, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 4, preHeight1 + margin + table_height1);
                                e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 5, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 5, preHeight1 + margin + table_height1);
                                e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 6, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 6, preHeight1 + margin + table_height1);
                                e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 7, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 7, preHeight1 + margin + table_height1);
                                string gold14 = dataGridView3.Rows[0].Cells[0].Value.ToString();
                                string gold18 = dataGridView3.Rows[0].Cells[1].Value.ToString();
                                string gold21 = dataGridView3.Rows[0].Cells[2].Value.ToString();
                                string cash = dataGridView3.Rows[0].Cells[3].Value.ToString();
                                string ogold14 = dataGridView3.Rows[0].Cells[4].Value.ToString();
                                string ogold18 = dataGridView3.Rows[0].Cells[5].Value.ToString();
                                string ogold21 = dataGridView3.Rows[0].Cells[6].Value.ToString();
                                string ocash = dataGridView3.Rows[0].Cells[7].Value.ToString();

                                e.Graphics.DrawLine(Pens.Black, margin, preHeight1 + margin + (margin - shift), e.PageBounds.Width - margin - (shift / 2), preHeight1 + margin + (margin - shift));
                                
                                string l0 = "ذهب18 له";
                                string l1 = "ذهب18 لنا";
                                string l2 = "ذهب14 له";
                                string l3 = "ذهب14 لنا";

                                SizeF sL0 = e.Graphics.MeasureString(l0, f);
                                SizeF sL1 = e.Graphics.MeasureString(l1, f);
                                SizeF sL2 = e.Graphics.MeasureString(l2, f);
                                SizeF sL3 = e.Graphics.MeasureString(l3, f);

                                string l00 = "اجور له";
                                string l01 = "اجور لنا";
                                string l02 = "ذهب21 له";
                                string l03 = "ذهب21 لنا";

                                SizeF sL00 = e.Graphics.MeasureString(l00, f);
                                SizeF sL01 = e.Graphics.MeasureString(l01, f);
                                SizeF sL02 = e.Graphics.MeasureString(l02, f);
                                SizeF sL03 = e.Graphics.MeasureString(l03, f);

                                SizeF sGold14 = e.Graphics.MeasureString(gold14, fz);
                                SizeF sCash = e.Graphics.MeasureString(cash, fz);
                                SizeF sGold18 = e.Graphics.MeasureString(gold18, fz);
                                SizeF sGold21 = e.Graphics.MeasureString(gold21, fz);
                                SizeF soGold14 = e.Graphics.MeasureString(ogold14, fz);
                                SizeF soCash = e.Graphics.MeasureString(ocash, fz);
                                SizeF soGold18 = e.Graphics.MeasureString(ogold18, fz);
                                SizeF soGold21 = e.Graphics.MeasureString(ogold21, fz);


                                e.Graphics.DrawString(l3, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL0.Width) / 2, preHeight1 + margin + 2);
                                e.Graphics.DrawString(l2, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL1.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8), preHeight1 + margin + 2);
                                e.Graphics.DrawString(l1, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL2.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 2), preHeight1 + margin + 2);
                                e.Graphics.DrawString(l0, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL3.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 3), preHeight1 + margin + 2);
                                e.Graphics.DrawString(l03, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL1.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 4) - 20, preHeight1 + margin + 2);
                                e.Graphics.DrawString(l02, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL2.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 5) - 20, preHeight1 + margin + 2);
                                e.Graphics.DrawString(l01, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL3.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 6) - 20, preHeight1 + margin + 2);
                                e.Graphics.DrawString(l00, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL3.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 7) - 20, preHeight1 + margin + 2);


                                e.Graphics.DrawString(gold14, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sGold14.Width) / 2 - 15, preHeight1 + margin + (2 * shift));
                                e.Graphics.DrawString(gold18, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sGold18.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8) - 17, preHeight1 + margin + (2 * shift));
                                e.Graphics.DrawString(gold21, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sGold21.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 2) - 13, preHeight1 + margin + (2 * shift));
                                e.Graphics.DrawString(cash, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sCash.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 3) - 10, preHeight1 + margin + (2 * shift));
                                e.Graphics.DrawString(ogold14, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - soGold18.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 4) - 25, preHeight1 + margin + (2 * shift));
                                e.Graphics.DrawString(ogold18, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - soGold21.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 5) - 7, preHeight1 + margin + (2 * shift));
                                e.Graphics.DrawString(ogold21, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - soCash.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 6) - 7, preHeight1 + margin + (2 * shift));
                                e.Graphics.DrawString(ocash, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - soCash.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 7) - 7, preHeight1 + margin + (2 * shift));

                            }
                            else if (comboBox1.Text == "حسابات الصاغة")
                            {
                                float table_height1 = ((margin - shift) * 2);

                                string fStr = "جدول الاجمالي";
                                SizeF sFStr = e.Graphics.MeasureString(fStr, f3);
                                ////////////////////////////////////////////////////
                                e.Graphics.DrawRectangle(Pens.Black, margin, preHeight1 + margin, e.PageBounds.Width - (2 * margin) - (shift / 2), table_height1);
                                e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight1 + margin + 2, e.PageBounds.Width - (2 * margin) - (shift / 2) - 2, margin - shift - 3);
                                e.Graphics.DrawString(fStr, f3, Brushes.Black, (e.PageBounds.Width - sFStr.Width + margin) / 2, preHeight1);
                                e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 6, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 6, preHeight1 + margin + table_height1);
                                e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 6 * 2, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 6 * 2, preHeight1 + margin + table_height1);
                                e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 6 * 3, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 6 * 3, preHeight1 + margin + table_height1);
                                e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 6 * 4, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 6 * 4, preHeight1 + margin + table_height1);
                                e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 6 * 5, preHeight1 + margin, (margin + e.PageBounds.Width - (2 * margin)) / 6 * 5, preHeight1 + margin + table_height1);

                                e.Graphics.DrawLine(Pens.Black, margin, preHeight1 + margin + (margin - shift), e.PageBounds.Width - margin, preHeight1 + margin + (margin - shift));

                                string mGold14 = dataGridView3.Rows[0].Cells[0].Value.ToString() + " غرام";
                                string fGold14 = dataGridView3.Rows[0].Cells[1].Value.ToString() + " غرام";
                                string mGold21 = dataGridView3.Rows[0].Cells[2].Value.ToString() + " غرام";
                                string fGold21 = dataGridView3.Rows[0].Cells[3].Value.ToString() + " غرام";
                                string mCash = dataGridView3.Rows[0].Cells[4].Value.ToString() + " ل.س";
                                string fCash = dataGridView3.Rows[0].Cells[5].Value.ToString() + " ل.س";

                                SizeF sfGold14 = e.Graphics.MeasureString(fGold14, f);
                                SizeF smGold14 = e.Graphics.MeasureString(mGold14, f);
                                SizeF sfGold21 = e.Graphics.MeasureString(fGold21, f);
                                SizeF smGold21 = e.Graphics.MeasureString(mGold21, f);
                                SizeF sfCash = e.Graphics.MeasureString(fCash, f);
                                SizeF smCash = e.Graphics.MeasureString(mCash, f);

                                e.Graphics.DrawString(mgold14Label, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sGold14Label.Width) / 2, preHeight1 + margin + 2);
                                e.Graphics.DrawString(fgold14Label, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sGold18Label.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6), preHeight1 + margin + 2);
                                e.Graphics.DrawString(mgold21Label, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sGold21Label.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6 * 2), preHeight1 + margin + 2);
                                e.Graphics.DrawString(fgold21Label, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sGold18Label.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6 * 3), preHeight1 + margin + 2);
                                e.Graphics.DrawString(mcashLabel, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sGold21Label.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6 * 4), preHeight1 + margin + 2);
                                e.Graphics.DrawString(fcashLabel, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sGold21Label.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6 * 5), preHeight1 + margin + 2);

                                e.Graphics.DrawString(mGold14, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - smGold14.Width) / 2, preHeight1 + margin + (2 * shift));
                                e.Graphics.DrawString(fGold14, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sfGold14.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6), preHeight1 + margin + (2 * shift));
                                e.Graphics.DrawString(mGold21, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - smGold21.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6 * 2), preHeight1 + margin + (2 * shift));
                                e.Graphics.DrawString(fGold21, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sfGold21.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6 * 3), preHeight1 + margin + (2 * shift));
                                e.Graphics.DrawString(mCash, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - smCash.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6 * 4), preHeight1 + margin + (2 * shift));
                                e.Graphics.DrawString(fCash, fss, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 6) - sfCash.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 6 * 5), preHeight1 + margin + (2 * shift));

                            }
                            ////////////خاتمة الصفحة///////////////////////////
                            e.Graphics.DrawString(pLabel2, f2, Brushes.Black, (e.PageBounds.Width - sPLabel2.Width + margin) / 2, preHeight11 - margin);
                            e.Graphics.DrawString(user, f2, Brushes.Black, margin, preHeight11 - margin);
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
                    //////////خاتمة الصفحة///////////////////////////
                    e.Graphics.DrawString(pLabel2, f2, Brushes.Black, (e.PageBounds.Width - sPLabel2.Width + margin) / 2, preHeight11 + (1 * margin) - shift);
                    e.Graphics.DrawString(user, f2, Brushes.Black, margin, preHeight11 + (1 * margin) - shift);
                }
            }
            else if (comboBox1.Text == "القيد اليومي")
            {
                Font ff = new Font("Arial", 8, FontStyle.Bold);
                Font f = new Font("Arial", 10, FontStyle.Bold);
                Font f1 = new Font("Arial", 14, FontStyle.Bold | FontStyle.Underline);
                Font f11 = new Font("Arial", 24, FontStyle.Bold);
                Font f2 = new Font("Arial", 12, FontStyle.Bold);
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
                string cashLabel = "الاجور";
                string gold21Label = GlobalVar.gold21Label;
                string gold18Label = GlobalVar.gold18Label;
                string gold14Label = GlobalVar.gold14Label;
                string tCashLabel = "الاجور";
                string strType = "القيد اليومي " + " في الفترة من  " + d1 + "  إلى  " + d2;
                string user = "منظم الفاتورة" + Environment.NewLine + GlobalVar.userName;
                string pLabel2 = "ثقتكم سر نجاحنا";
                string vLabel = "لا يوجد دفعات ليتم طباعتها";


                SizeF sTCashLabel = e.Graphics.MeasureString(tCashLabel, f2);
                SizeF sStrDate = e.Graphics.MeasureString(strDate, f);
                SizeF sStrClock = e.Graphics.MeasureString(strClock, f);
                SizeF sCashLabel = e.Graphics.MeasureString(cashLabel, f2);
                SizeF sStrType = e.Graphics.MeasureString(strType, f2);

                SizeF sGold21Label = e.Graphics.MeasureString(gold21Label, f2);
                SizeF sGold18Label = e.Graphics.MeasureString(gold18Label, f2);
                SizeF sGold14Label = e.Graphics.MeasureString(gold14Label, f2);
                SizeF sPLabel2 = e.Graphics.MeasureString(pLabel2, f1);
                SizeF sUser = e.Graphics.MeasureString(user, f1);
                SizeF sVLabel = e.Graphics.MeasureString(vLabel, f1);

                float margin = 30;
                float shift = 10;

                float col1width = e.PageBounds.Width / 3;
                float col2width = e.PageBounds.Width / 4;
                float col22width = e.PageBounds.Width / 4;
                float col3width = (e.PageBounds.Width - (2 * margin)) / dataGridView1.ColumnCount;

                float table_height = ((margin - shift) * (dataGridView1.RowCount));

                float preHeight = (2 * margin) + sStrDate.Height + sStrClock.Height + (shift);
                float preHeight1 = preHeight;
                float preHeight11 = preHeight1 + (2 * margin);
                //////////header//////////
                e.HasMorePages = false;
                int noPages = 0;
                int noPages1 = 0;
                int noRows = 0;
                int D = 48;
                noPages1 = (dataGridView1.Rows.Count / D) + 1;
                noRows = (dataGridView1.Rows.Count % D);
                if (noRows > D)
                {
                    noPages = noPages1 + 1;
                }
                else
                {
                    noPages = noPages1;
                }
                ii++;
                printHeaderFooter(e, noPages, ii);
                ///////////////////
                e.Graphics.DrawString(strDate, f, Brushes.Black, e.PageBounds.Width - margin - sStrDate.Width, 2 * margin + shift / 2);
                e.Graphics.DrawString(strClock, f, Brushes.Black, e.PageBounds.Width - margin - sStrClock.Width, 2 * margin + sStrDate.Height + shift);
                e.Graphics.DrawString(strType, f2, Brushes.Black, (e.PageBounds.Width - (2 * margin) - sStrType.Width) / 2, 2 * margin + sStrDate.Height + sStrClock.Height + shift);
                ///////////////////
                bool last = true;
                if (dataGridView1.Rows.Count > 0)
                {
                    preHeight11 = preHeight1 + (2 * margin);
                    for (; ii < noPages; ii++)
                    {
                        if (noPages - ii > 1) { last = true; }
                        else { last = false; }
                        int SS = 0;
                        printHeaderFooter(e, noPages, ii);
                        if (last)
                            SS = D + 1;
                        else
                            SS = (dataGridView1.RowCount+1) - (D * ii);
                        if (SS < 0) { SS = 1; }
                        table_height = (margin - shift) * SS;
                        /////////جدول بيانات اجمالي الفاتورة/////////////////////////////////////////
                        e.Graphics.DrawRectangle(Pens.Black, margin, preHeight + margin, e.PageBounds.Width - (2 * margin), table_height);
                        e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight + margin + 2, e.PageBounds.Width - (2 * margin) - 2, margin - shift - 3);
                        string A = "";
                        for (int i = 0 + (D * (ii)); i < D + (D * (ii)) && i < dataGridView1.RowCount; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[7].Value.ToString() == "استلام" || dataGridView1.Rows[i].Cells[7].Value.ToString() == "شراء")
                                e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight + margin + ((i - (D * ii) + 1) * (2 * shift)) + 2, e.PageBounds.Width - (2 * margin) - 2, margin - shift - 3);
                        }
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            A = dataGridView1.Columns[dataGridView1.ColumnCount - j - 1].HeaderText.ToString();
                            SizeF V = e.Graphics.MeasureString(A, f2);
                            float B = e.PageBounds.Width - margin - ((j + 1) * col3width) + ((col3width - V.Width) / 2);
                            if (j != 0)
                            {
                                if (j != 1)
                                {
                                    e.Graphics.DrawString(A, f2, Brushes.Black, B, preHeight + (3 * shift));
                                }
                                else
                                {
                                    e.Graphics.DrawString(A, f2, Brushes.Black, B + margin, preHeight + (3 * shift));
                                }
                                e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width - margin - ((j + 1) * col3width), preHeight + margin, e.PageBounds.Width - margin - ((j + 1) * col3width), preHeight + margin + table_height);
                            }
                            else
                            {
                                e.Graphics.DrawString(A, f2, Brushes.Black, B +10, preHeight + (3 * shift));
                                e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width  - ((j + 1) * col3width), preHeight + margin, e.PageBounds.Width  - ((j + 1) * col3width) , preHeight + margin + table_height);
                            }
                        }
                        for (int i = 0 + (D * (ii)); i < D + (D * (ii)) && i < dataGridView1.RowCount ; i++)
                        {
                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            {
                                A = dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - j - 1].Value.ToString();
                             
                                SizeF V = e.Graphics.MeasureString(A, f);
                                float B = e.PageBounds.Width - margin - ((j + 1) * col3width) + ((col3width - V.Width) / 2);
                                if (j == 1)
                                {
                                    e.Graphics.DrawString(A.Substring(0, 10), f, Brushes.Black, B+2*margin, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)));
                                }
                                else if (j == 0)
                                {
                                    e.Graphics.DrawString(A, f, Brushes.Black, B + margin-10, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)));
                                }
                                else if (j > 1)
                                {
                                    e.Graphics.DrawString(A, f, Brushes.Black, B, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)));
                                }
                                else
                                {
                                    e.Graphics.DrawString(A, f, Brushes.Black, B + margin, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)));
                                }
                                e.Graphics.DrawLine(Pens.Black, margin, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)), e.PageBounds.Width - margin, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)));
                            }
                        }
                        if (ii == (noPages - 1))
                        {
                            preHeight1 = preHeight + (2 * margin) + table_height;
                            preHeight11 = preHeight1 + (2 * margin);
                            ////////////خاتمة الصفحة///////////////////////////
                            e.Graphics.DrawString(pLabel2, f2, Brushes.Black, (e.PageBounds.Width - sPLabel2.Width + margin) / 2, preHeight1);
                            e.Graphics.DrawString(user, f2, Brushes.Black, margin, preHeight1);
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
                    //////////خاتمة الصفحة///////////////////////////
                    e.Graphics.DrawString(pLabel2, f2, Brushes.Black, (e.PageBounds.Width - sPLabel2.Width + margin) / 2, preHeight11 + (2 * margin) - shift);
                    e.Graphics.DrawString(user, f2, Brushes.Black, margin, preHeight11 + (2 * margin) - shift);
                }
            }
            else if (comboBox1.Text == "حركة الصندوق")
            {
                printDocument1.DefaultPageSettings.Landscape = true;
                printDocument1.DefaultPageSettings.Landscape = true;
                Font ff = new Font("Arial", 8, FontStyle.Bold);
                Font f = new Font("Arial", 10, FontStyle.Bold);
                Font f1 = new Font("Arial", 14, FontStyle.Bold | FontStyle.Underline);
                Font f11 = new Font("Arial", 24, FontStyle.Bold);
                Font f2 = new Font("Arial", 12, FontStyle.Bold);
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
                string cashLabel = "الاجور";
                string gold21Label = GlobalVar.gold21Label;
                string gold18Label = GlobalVar.gold18Label;
                string gold14Label = GlobalVar.gold14Label;
                string tCashLabel = "الاجور";
                string strType = "حركة الصندوق " + " في الفترة من  " + d1 + "  إلى  " + d2;
                string user = "منظم الفاتورة" + Environment.NewLine + GlobalVar.userName;
                string pLabel2 = "ثقتكم سر نجاحنا";
                string vLabel = "لا يوجد بيانات ليتم طباعتها";


                SizeF sTCashLabel = e.Graphics.MeasureString(tCashLabel, f2);
                SizeF sStrDate = e.Graphics.MeasureString(strDate, f);
                SizeF sStrClock = e.Graphics.MeasureString(strClock, f);
                SizeF sCashLabel = e.Graphics.MeasureString(cashLabel, f2);
                SizeF sStrType = e.Graphics.MeasureString(strType, f2);

                SizeF sGold21Label = e.Graphics.MeasureString(gold21Label, f2);
                SizeF sGold18Label = e.Graphics.MeasureString(gold18Label, f2);
                SizeF sGold14Label = e.Graphics.MeasureString(gold14Label, f2);
                SizeF sPLabel2 = e.Graphics.MeasureString(pLabel2, f1);
                SizeF sUser = e.Graphics.MeasureString(user, f1);
                SizeF sVLabel = e.Graphics.MeasureString(vLabel, f1);

                float margin = 30;
                float shift = 10;

                float col1width = e.PageBounds.Width / 3;
                float col2width = e.PageBounds.Width / 4;
                float col22width = e.PageBounds.Width / 4;
                float col3width = (e.PageBounds.Width - (2 * margin)) / dataGridView1.ColumnCount;

                float table_height = ((margin - shift) * (dataGridView1.RowCount));

                float preHeight = (2 * margin) + sStrDate.Height + sStrClock.Height + (shift);
                float preHeight1 = preHeight;
                float preHeight11 = preHeight1 + (2 * margin);
                //////////header//////////
                e.HasMorePages = false;
                int noPages = 0;
                int noPages1 = 0;
                int noRows = 0;
                int D = 31;
                noPages1 = (dataGridView1.Rows.Count / D) + 1;
                noRows = (dataGridView1.Rows.Count % D);

                if (noRows > D)
                {
                    noPages = noPages1 + 1;
                }
                else
                {
                    noPages = noPages1;
                }
                ii++;

                printHeaderFooter(e, noPages, ii);
                ///////////////////
                e.Graphics.DrawString(strDate, f, Brushes.Black, e.PageBounds.Width - margin - sStrDate.Width, 2 * margin + shift / 2);
                e.Graphics.DrawString(strClock, f, Brushes.Black, e.PageBounds.Width - margin - sStrClock.Width, 2 * margin + sStrDate.Height + shift);
                e.Graphics.DrawString(strType, f2, Brushes.Black, (e.PageBounds.Width - (2 * margin) - sStrType.Width) / 2, 2 * margin + sStrDate.Height + sStrClock.Height + shift);
                ///////////////////
                ///
                bool last = true;
                if (dataGridView1.Rows.Count > 0)
                {
                    for (; ii < noPages; ii++)
                    {
                        printHeaderFooter(e, noPages, ii);
                        if (noPages - ii > 1) { last = true; }
                        else { last = false; }
                        int SS = 0;
                        printHeaderFooter(e, noPages, ii);
                        if (last)
                            SS = D + 1;
                        else
                            SS = dataGridView1.RowCount - (D * ii);
                        if (SS < 0) { SS = 1; }
                        table_height = (margin - shift) * SS;

                        /////////جدول بيانات اجمالي الفاتورة/////////////////////////////////////////
                        e.Graphics.DrawRectangle(Pens.Black, margin, preHeight + margin, e.PageBounds.Width - (2 * margin), table_height);
                        e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight + margin + 2, e.PageBounds.Width - (2 * margin) - 2, margin - shift - 3);
                        string A = "";
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            A = dataGridView1.Columns[dataGridView1.ColumnCount - j - 1].HeaderText.ToString();

                            SizeF V = e.Graphics.MeasureString(A, f2);
                            float B = e.PageBounds.Width - margin - ((j + 1) * col3width) + ((col3width - V.Width) / 2) + 2;
                            e.Graphics.DrawString(A, f2, Brushes.Black, B + shift + 2, preHeight + (3 * shift));
                            e.Graphics.DrawLine(Pens.Black, B, preHeight + margin, B, preHeight + margin + table_height);
                        }
                        for (int i = 0 + (D * (ii)); i < D + (D * (ii)) && i < dataGridView1.RowCount - 1; i++)
                        {
                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            {
                                A = dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - j - 1].Value.ToString();
                                if (j == 0)
                                {
                                    A = A.Remove(A.IndexOf(' ') + 1);
                                }
                                SizeF V = e.Graphics.MeasureString(A, f);
                                float B = e.PageBounds.Width - margin - ((j + 1) * col3width) + ((col3width - V.Width) / 2);
                                e.Graphics.DrawString(A, f, Brushes.Black, B + shift + 3, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)));
                                e.Graphics.DrawLine(Pens.Black, margin, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)), e.PageBounds.Width - margin, preHeight + margin + (((i - (D * (ii))) + 1) * (2 * shift)));
                            }
                        }
                        if (ii == (noPages - 1))
                        {
                            preHeight1 = preHeight + (2 * margin) + table_height;
                            preHeight11 = preHeight1 + (2 * margin);
                            ////////////خاتمة الصفحة///////////////////////////
                            e.Graphics.DrawString(pLabel2, f2, Brushes.Black, (e.PageBounds.Width - sPLabel2.Width + margin) / 2, preHeight1);
                            e.Graphics.DrawString(user, f2, Brushes.Black, margin, preHeight1);
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
                    //////////خاتمة الصفحة///////////////////////////
                    e.Graphics.DrawString(pLabel2, f2, Brushes.Black, (e.PageBounds.Width - sPLabel2.Width + margin) / 2, preHeight11 + (2 * margin) - shift);
                    e.Graphics.DrawString(user, f2, Brushes.Black, margin, preHeight11 + (2 * margin) - shift);

                }

            }
            else if (comboBox1.Text == "كشف حساب تفصيلي")
            {
                printDocument1.DefaultPageSettings.Landscape = true;
                printDocument1.DefaultPageSettings.Landscape = true;

                Font ff = new Font("Arial", 10, FontStyle.Bold);
                Font f = new Font("Arial", 13, FontStyle.Bold);
                Font fz = new Font("Arial", 11, FontStyle.Bold);
                Font fss = new Font("Arial", 12, FontStyle.Bold);

                Font f1 = new Font("Arial", 16, FontStyle.Bold | FontStyle.Underline);
                Font f11 = new Font("Arial", 24, FontStyle.Bold);
                Font f2 = new Font("Arial", 12, FontStyle.Bold);
                Font f3 = new Font("Arial", 14, FontStyle.Bold);

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
                string cashLabel = "الاجور";
                string gold21Label = GlobalVar.gold21Label;
                string gold18Label = GlobalVar.gold18Label;
                string gold14Label = GlobalVar.gold14Label;
                string tCashLabel = "الاجور";
                string strType = "كشف حساب تفصيلي  " + itemNameTxtbox.Text + " في الفترة من  " + d1 + "  إلى  " + d2;
                string user = "منظم الفاتورة" + Environment.NewLine + GlobalVar.userName;
                string pLabel2 = "ثقتكم سر نجاحنا";
                string vLabel = "لا يوجد دفعات ليتم طباعتها";

                SizeF sTCashLabel = e.Graphics.MeasureString(tCashLabel, f2);
                SizeF sStrDate = e.Graphics.MeasureString(strDate, f);
                SizeF sStrClock = e.Graphics.MeasureString(strClock, f);
                SizeF sCashLabel = e.Graphics.MeasureString(cashLabel, f2);
                SizeF sStrType = e.Graphics.MeasureString(strType, f2);

                SizeF sGold21Label = e.Graphics.MeasureString(gold21Label, f2);
                SizeF sGold18Label = e.Graphics.MeasureString(gold18Label, f2);
                SizeF sGold14Label = e.Graphics.MeasureString(gold14Label, f2);
                SizeF sPLabel2 = e.Graphics.MeasureString(pLabel2, f1);
                SizeF sUser = e.Graphics.MeasureString(user, f1);
                SizeF sVLabel = e.Graphics.MeasureString(vLabel, f1);

                float margin = 30;
                float shift = 10;

                float col1width = e.PageBounds.Width / 3;
                float col2width = e.PageBounds.Width / 4;
                float col22width = e.PageBounds.Width / 4;
                float col3width = (e.PageBounds.Width - (2 * margin)) / dataGridView1.ColumnCount;
                float table_height;
                if (dataGridView2.RowCount > 0)
                {
                    table_height = ((margin - shift) * (dataGridView1.RowCount + dataGridView2.RowCount + 2));
                }
                else
                {
                    table_height = (margin - shift) * dataGridView1.RowCount;
                }
                float preHeight = (2 * margin) + sStrDate.Height + sStrClock.Height + (shift);
                float preHeight1 = preHeight;
                float preHeight11 = preHeight1 + (2 * margin);
                //////////header//////////
                e.HasMorePages = false;
                int noPages = 0;
                int noPages1 = 0;
                int noRows = 0;
                int num = 31;
                noPages1 = ((dataGridView1.RowCount + dataGridView2.RowCount + 2) / num) + 1;
                noRows = ((dataGridView1.RowCount + dataGridView2.RowCount + 2) % num);
                if (noRows > num - 4)
                {
                    noPages = noPages1 + 1;
                }
                else
                {
                    noPages = noPages1;
                }
                ii++;
                printHeaderFooter(e, noPages, ii);
                ///////////////////
                e.Graphics.DrawString(strDate, f, Brushes.Black, e.PageBounds.Width - margin - sStrDate.Width, 2 * margin + shift / 2);
                e.Graphics.DrawString(strClock, f, Brushes.Black, e.PageBounds.Width - margin - sStrClock.Width, 2 * margin + sStrDate.Height + shift);
                e.Graphics.DrawString(strType, f2, Brushes.Black, (e.PageBounds.Width - (2 * margin) - sStrType.Width) / 2, margin + sStrDate.Height + sStrClock.Height + 3 * shift);
                ///////////////////
                bool last = false;
                if (dataGridView1.RowCount + dataGridView2.RowCount > 0)
                {
                    for (; ii < noPages; ii++)
                    {
                        if (noPages - ii > 1) { last = true; }
                        else { last = false; }
                        int SS = 0;
                        printHeaderFooter(e, noPages, ii);
                        if (last)
                        {
                            if (noRows > (num - 4) && noRows < num && (ii + 1) == (noPages - 1))
                                SS = noRows + 1;
                            else
                                SS = num + 1;
                        }
                        else
                            SS = (dataGridView1.RowCount + dataGridView2.RowCount + 2) - (num * ii) + 1;

                        if (SS < 2) { SS = 0; }
                        table_height = (margin - shift) * SS;
                        /////////جدول بيانات اجمالي الفاتورة/////////////////////////////////////////
                        string A = "";
                        if (table_height != 0)
                        {
                            /////////جدول بيانات اجمالي الفاتورة/////////////////////////////////////////
                            e.Graphics.DrawRectangle(Pens.Black, margin, preHeight + shift, e.PageBounds.Width - (2 * margin), table_height);
                            e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight + shift + 2, e.PageBounds.Width - (2 * margin) - 2, margin - shift - 3);
                            A = "";
                            for (int i = 0 + (num * (ii)); i < num + (num * (ii)) && i < (dataGridView1.RowCount + dataGridView2.RowCount); i++)
                            {
                                
                                if (i<dataGridView1.RowCount)
                                {
                                    if (dataGridView1.Rows[i].Cells[6].Value.ToString() == "استلام" || dataGridView1.Rows[i].Cells[6].Value.ToString() == "شراء")
                                        e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight + shift + ((i - (num * ii) + 1) * (2 * shift)) + 2, e.PageBounds.Width - (2 * margin) - 2, margin - shift - 3);
                                }
                                else
                                {
                                    MessageBox.Show(dataGridView2.Rows[i-dataGridView1.RowCount].Cells[0].Value.ToString()+" "+i.ToString());
                                    if (dataGridView2.Rows[i-dataGridView1.RowCount].Cells[0].Value.ToString() == "شراء")
                                        e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight + shift + (((i+2) - (num * ii) + 1) * (2 * shift)) + 2, e.PageBounds.Width - (2 * margin) - 2, margin - shift - 3);
                                }
                            }
                            for (int j = 0; j < dataGridView1.ColumnCount - 1; j++)
                            {
                                A = dataGridView1.Columns[dataGridView1.ColumnCount - j - 1].HeaderText.ToString();
                                SizeF V = e.Graphics.MeasureString(A, f2);
                                float B = e.PageBounds.Width - margin - ((j + 1) * col3width) + ((col3width - V.Width) / 2);
                                if (j == 0)
                                {
                                    e.Graphics.DrawString(A, f2, Brushes.Black, B + margin, preHeight + (1 * shift));
                                    e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width + margin - ((j + 1) * col3width) + 15, preHeight + shift, e.PageBounds.Width + margin - ((j + 1) * col3width) + 15, preHeight + shift + table_height);
                                }
                                else if (j == dataGridView1.ColumnCount - 2)
                                {
                                    e.Graphics.DrawString(A, f2, Brushes.Black, B + margin + 25, preHeight + (1 * shift));
                                    e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width + margin - ((j + 1) * col3width) + 20, preHeight + shift, e.PageBounds.Width + margin - ((j + 1) * col3width) + 20, preHeight + shift + table_height);
                                }
                                else if (j == dataGridView1.ColumnCount - 3)
                                {
                                    e.Graphics.DrawString(A, f2, Brushes.Black, B + (1 * margin), preHeight + (1 * shift));
                                    e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width - margin - ((j + 1) * col3width) + (2 * margin) - 15, preHeight + shift, e.PageBounds.Width - margin - ((j + 1) * col3width) + (2 * margin) - 15, preHeight + shift + table_height);
                                }
                                else
                                {
                                    e.Graphics.DrawString(A, f2, Brushes.Black, B + 15, preHeight + (1 * shift));
                                    e.Graphics.DrawLine(Pens.Black, e.PageBounds.Width - margin - ((j + 1) * col3width) + (1 * margin) - 15, preHeight + shift, e.PageBounds.Width - margin - ((j + 1) * col3width) + (1 * margin) - 15, preHeight + shift + table_height);
                                }
                            }
                        }
                        for (int i = 0 + (num * (ii)); i < num + (num * (ii)) && i < (dataGridView1.RowCount + dataGridView2.RowCount + 2); i++)
                        {
                            if (i < dataGridView1.RowCount)
                            {
                                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                                {
                                    A = dataGridView1.Rows[i].Cells[dataGridView1.ColumnCount - j - 1].Value.ToString();
                                    SizeF V = e.Graphics.MeasureString(A, f);
                                    float B = e.PageBounds.Width - margin - ((j + 1) * col3width) + ((col3width - V.Width) / 2);
                                    if (j == 6)
                                    {
                                        A = A.Substring(0, 10);
                                        e.Graphics.DrawString(A, f, Brushes.Black, B + (3 * margin) + 25, preHeight + shift + (((i - (num * (ii))) + 1) * (2 * shift)));
                                    }
                                    else if (j == 0)
                                    {
                                        e.Graphics.DrawString(A, f, Brushes.Black, B + (1 * margin), preHeight + shift + (((i - (num * (ii))) + 1) * (2 * shift)));
                                    }
                                    else if (j == 7)
                                    {
                                        e.Graphics.DrawString(A, f, Brushes.Black, B + (1 * margin), preHeight + shift + (((i - (num * (ii))) + 1) * (2 * shift)));
                                    }
                                    else
                                    {
                                        e.Graphics.DrawString(A, f, Brushes.Black, B + 15, preHeight + shift + (((i - (num * (ii))) + 1) * (2 * shift)));
                                    }
                                    e.Graphics.DrawLine(Pens.Black, margin, preHeight + shift + (((i - (num * (ii))) + 1) * (2 * shift)), e.PageBounds.Width - margin, preHeight + shift + (((i - (num * (ii))) + 1) * (2 * shift)));
                                }
                            }
                            else if (i == dataGridView1.RowCount)
                            {
                                if (dataGridView2.RowCount > 0)
                                {
                                    e.Graphics.FillRectangle(Brushes.White, margin - 3, preHeight + shift + ((((i) - (num * (ii))) + 1) * (2 * shift)), e.PageBounds.Width - (2 * margin) + 6, margin - shift - 1);
                                    //for (int j = 0; j < dataGridView2.ColumnCount; j++)
                                    //{
                                    A = "جدول بتفاصيل مصوغات الفواتير";
                                    SizeF V = e.Graphics.MeasureString(A, f);

                                    //if (j == 0)
                                    e.Graphics.DrawString(A, f, Brushes.Black, e.PageBounds.Width / 2 - margin, preHeight + shift + ((((i) - (num * (ii))) + 1) * (2 * shift)));
                                    //    else

                                    e.Graphics.DrawLine(Pens.Black, margin, preHeight + shift + (((i - (num * (ii))) + 1) * (2 * shift)), e.PageBounds.Width - margin, preHeight + shift + (((i - (num * (ii))) + 1) * (2 * shift)));
                                }
                            }
                            else if (i == dataGridView1.RowCount + 1)
                            {
                                if (dataGridView2.RowCount > 0)
                                {
                                    e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight + shift + ((((i) - (num * (ii))) + 1) * (2 * shift)), e.PageBounds.Width - (2 * margin) - 2, margin - shift - 1);
                                    for (int j = 0; j < dataGridView2.ColumnCount; j++)
                                    {
                                        A = dataGridView2.Columns[dataGridView2.ColumnCount - j - 1].HeaderText.ToString();
                                        SizeF V = e.Graphics.MeasureString(A, f);
                                        float B = e.PageBounds.Width - margin - ((j + 1) * col3width) + ((col3width - V.Width) / 2);
                                        if (j== dataGridView2.ColumnCount-2)
                                            e.Graphics.DrawString(A, f, Brushes.Black, B + (1 * margin)+25, preHeight + shift + ((((i) - (num * (ii))) + 1) * (2 * shift)));
                                        else
                                        e.Graphics.DrawString(A, f, Brushes.Black, B + (1 * margin), preHeight + shift + ((((i) - (num * (ii))) + 1) * (2 * shift)));
                                    }
                                    e.Graphics.DrawLine(Pens.Black, margin, preHeight + shift + (((i - (num * (ii))) + 1) * (2 * shift)), e.PageBounds.Width - margin, preHeight + shift + (((i - (num * (ii))) + 1) * (2 * shift)));
                                }
                            }
                            else if (dataGridView2.RowCount > 0)
                            {
                                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                                {
                                    A = dataGridView2.Rows[i - dataGridView1.RowCount - 2].Cells[dataGridView2.ColumnCount - j - 1].Value.ToString();

                                    SizeF V = e.Graphics.MeasureString(A, f);
                                    float B = e.PageBounds.Width - margin - ((j + 1) * col3width) + ((col3width - V.Width) / 2);
                                    if (j == 0)
                                    {
                                        e.Graphics.DrawString(A, f, Brushes.Black, B + 1 * margin, preHeight + shift + ((((i) - (num * (ii))) + 1) * (2 * shift)));
                                    }
                                    else if (j == 1)
                                    {
                                        if (Convert.ToInt16(A.Substring(A.Length - 2, 2)) == 14)
                                            e.Graphics.DrawString(A.Substring(0, A.Length - 8) + " 14", f, Brushes.Black, B + (2 * margin) - 15, preHeight + shift + ((((i) - (num * (ii))) + 1) * (2 * shift)));
                                        else
                                            e.Graphics.DrawString(A.Substring(0, A.Length - 8), f, Brushes.Black, B + (2 * margin) - 15, preHeight + shift + ((((i) - (num * (ii))) + 1) * (2 * shift)));
                                    }
                                    else if (j == dataGridView2.ColumnCount - 2)
                                    {
                                        e.Graphics.DrawString(A, f, Brushes.Black, B + margin +32, preHeight + shift + ((((i) - (num * (ii))) + 1) * (2 * shift)));
                                    }
                                    else if (j == dataGridView2.ColumnCount - 1)
                                    {
                                        e.Graphics.DrawString(A, f, Brushes.Black, B+15, preHeight + shift + ((((i) - (num * (ii))) + 1) * (2 * shift)));
                                    }
                                    else
                                    {
                                        e.Graphics.DrawString(A, f, Brushes.Black, B + margin - 15, preHeight + shift + ((((i) - (num * (ii))) + 1) * (2 * shift)));
                                    }
                                    e.Graphics.DrawLine(Pens.Black, margin, preHeight + shift + ((((i) - (num * (ii))) + 1) * (2 * shift)), e.PageBounds.Width - margin, preHeight + shift + ((((i) - (num * (ii))) + 1) * (2 * shift)));
                                }
                            }
                        }
                        float table_height1 = ((margin - shift) * 2);
                        float preHeight111 = preHeight + margin + table_height;
                        if (!last)
                        {
                            string fStr = "جدول اجمالي حساب الصائغ";
                            SizeF sFStr = e.Graphics.MeasureString(fStr, f3);
                            ////////////////////////////////////////////////////
                            e.Graphics.DrawRectangle(Pens.Black, margin, preHeight111 + shift, e.PageBounds.Width - (2 * margin) - (shift / 2), table_height1);
                            e.Graphics.FillRectangle(Brushes.LightGray, margin + 1, preHeight111 + shift + 2, e.PageBounds.Width - (2 * margin) - (shift / 2) - 2, margin - shift - 3);
                            e.Graphics.DrawString(fStr, f3, Brushes.Black, (e.PageBounds.Width - sFStr.Width + margin) / 2, preHeight111 - 20);
                            e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8, preHeight111 + shift, (margin + e.PageBounds.Width - (2 * margin)) / 8, preHeight111 + shift + table_height1);
                            e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 2, preHeight111 + shift, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 2, preHeight111 + shift + table_height1);
                            e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 3, preHeight111 + shift, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 3, preHeight111 + shift + table_height1);
                            e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 4, preHeight111 + shift, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 4, preHeight111 + shift + table_height1);
                            e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 5, preHeight111 + shift, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 5, preHeight111 + shift + table_height1);
                            e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 6, preHeight111 + shift, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 6, preHeight111 + shift + table_height1);
                            e.Graphics.DrawLine(Pens.Black, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 7, preHeight111 + shift, (margin + e.PageBounds.Width - (2 * margin)) / 8 * 7, preHeight111 + shift + table_height1);
                            string gold14 = "";
                            string gold18 = "";
                            string gold21 = "";
                            string cash = "";
                            string ogold14 = "";
                            string ogold18 = "";
                            string ogold21 = "";
                            string ocash = "";

                            e.Graphics.DrawLine(Pens.Black, margin, preHeight111 + shift + (margin - shift), e.PageBounds.Width - margin - (shift / 2), preHeight111 + shift + (margin - shift));

                            if (dataGridView3.Rows[0].Cells[0].Value.ToString() != "رصيد ذهب14")
                            {
                                if (Convert.ToDecimal(dataGridView3.Rows[0].Cells[0].Value.ToString()) > 0 && dataGridView3.Rows[0].Cells[0].Value.ToString() != "رصيد ذهب14")
                                    gold14 = " لنا " + dataGridView3.Rows[0].Cells[0].Value.ToString() + " غرام";
                                else
                                    gold14 = " له " + dataGridView3.Rows[0].Cells[0].Value.ToString() + " غرام";
                            }
                            else
                            {
                                gold14 = "رصيد";
                            }
                            if (dataGridView3.Rows[0].Cells[1].Value.ToString() != "رصيد ذهب18")
                            {
                                if (Convert.ToDecimal(dataGridView3.Rows[0].Cells[1].Value.ToString()) > 0 && dataGridView3.Rows[0].Cells[1].Value.ToString() != "رصيد ذهب18")
                                    gold18 = " لنا " + dataGridView3.Rows[0].Cells[1].Value.ToString() + " غرام";
                                else
                                    gold18 = " له " + dataGridView3.Rows[0].Cells[1].Value.ToString() + " غرام";
                            }
                            else
                            {
                                gold18 = "رصيد";
                            }
                            if (dataGridView3.Rows[0].Cells[2].Value.ToString() != "رصيد ذهب21")
                            {
                                if (Convert.ToDecimal(dataGridView3.Rows[0].Cells[2].Value.ToString()) > 0 && dataGridView3.Rows[0].Cells[2].Value.ToString() != "رصيد ذهب21")
                                    gold21 = " لنا " + dataGridView3.Rows[0].Cells[2].Value.ToString() + " غرام";
                                else
                                    gold21 = " له " + dataGridView3.Rows[0].Cells[2].Value.ToString() + " غرام";
                            }
                            else
                            {
                                gold21 = "رصيد";
                            }
                            if (dataGridView3.Rows[0].Cells[3].Value.ToString() != "رصيد اجور")
                            {
                                if (Convert.ToDecimal(dataGridView3.Rows[0].Cells[3].Value.ToString()) > 0 && dataGridView3.Rows[0].Cells[3].Value.ToString() != "رصيد اجور")
                                    cash = " لنا " + dataGridView3.Rows[0].Cells[3].Value.ToString() + " ل.س";
                                else
                                    cash = " له " + dataGridView3.Rows[0].Cells[3].Value.ToString() + " ل.س";
                            }
                            else
                            {
                                cash = "رصيد";
                            }
                            if (dataGridView3.Rows[0].Cells[4].Value.ToString() != "رصيد ذهب14")
                            {
                                if (Convert.ToDecimal(dataGridView3.Rows[0].Cells[4].Value.ToString()) > 0 && dataGridView3.Rows[0].Cells[4].Value.ToString() != "رصيد ذهب14")
                                    ogold14 = " لنا " + dataGridView3.Rows[0].Cells[4].Value.ToString() + " غرام";
                                else
                                    ogold14 = " له " + dataGridView3.Rows[0].Cells[4].Value.ToString() + " غرام";
                            }
                            else
                            {
                                ogold14 = "رصيد";
                            }
                            if (dataGridView3.Rows[0].Cells[5].Value.ToString() != "رصيد ذهب18")
                            {
                                if (Convert.ToDecimal(dataGridView3.Rows[0].Cells[5].Value.ToString()) > 0 && dataGridView3.Rows[0].Cells[5].Value.ToString() != "رصيد ذهب18")
                                    ogold18 = " لنا " + dataGridView3.Rows[0].Cells[5].Value.ToString() + " غرام";
                                else
                                    ogold18 = " له " + dataGridView3.Rows[0].Cells[5].Value.ToString() + " غرام";
                            }
                            else
                            {
                                ogold18 = "رصيد";
                            }
                            if (dataGridView3.Rows[0].Cells[6].Value.ToString() != "رصيد ذهب21")
                            {
                                if (Convert.ToDecimal(dataGridView3.Rows[0].Cells[6].Value.ToString()) > 0)
                                    ogold21 = " لنا " + dataGridView3.Rows[0].Cells[6].Value.ToString() + " غرام";
                                else
                                    ogold21 = " له " + dataGridView3.Rows[0].Cells[6].Value.ToString() + " غرام";
                            }
                            else
                            {
                                ogold21 = "رصيد";
                            }
                            if (dataGridView3.Rows[0].Cells[7].Value.ToString() != "رصيد اجور")
                            {
                                if (Convert.ToDecimal(dataGridView3.Rows[0].Cells[7].Value.ToString()) > 0 && dataGridView3.Rows[0].Cells[7].Value.ToString() != "رصيد اجور")
                                    ocash = " لنا " + dataGridView3.Rows[0].Cells[7].Value.ToString() + " ل.س";
                                else
                                    ocash = " له " + dataGridView3.Rows[0].Cells[7].Value.ToString() + " ل.س";
                            }
                            else
                            {
                                ocash = "رصيد";
                            }

                            string l0 = "الاجور";
                            string l1 = "الذهب 21";
                            string l2 = "الذهب 18";
                            string l3 = "الذهب 14";

                            SizeF sL0 = e.Graphics.MeasureString(l0, f);
                            SizeF sL1 = e.Graphics.MeasureString(l1, f);
                            SizeF sL2 = e.Graphics.MeasureString(l2, f);
                            SizeF sL3 = e.Graphics.MeasureString(l3, f);

                            string l00 = "بدابة الاجور";
                            string l01 = "بداية الذهب 21";
                            string l02 = "بداية الذهب 18";
                            string l03 = "بداية الذهب 14";

                            SizeF sL00 = e.Graphics.MeasureString(l00, f);
                            SizeF sL01 = e.Graphics.MeasureString(l01, f);
                            SizeF sL02 = e.Graphics.MeasureString(l02, f);
                            SizeF sL03 = e.Graphics.MeasureString(l03, f);


                            SizeF sGold14 = e.Graphics.MeasureString(gold14, fz);
                            SizeF sCash = e.Graphics.MeasureString(cash, fz);
                            SizeF sGold18 = e.Graphics.MeasureString(gold18, fz);
                            SizeF sGold21 = e.Graphics.MeasureString(gold21, fz);
                            SizeF soGold14 = e.Graphics.MeasureString(ogold14, fz);
                            SizeF soCash = e.Graphics.MeasureString(ocash, fz);
                            SizeF soGold18 = e.Graphics.MeasureString(ogold18, fz);
                            SizeF soGold21 = e.Graphics.MeasureString(ogold21, fz);


                            e.Graphics.DrawString(l3, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL0.Width) / 2, preHeight111 + shift + 2);
                            e.Graphics.DrawString(l2, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL1.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8), preHeight111 + shift + 2);
                            e.Graphics.DrawString(l1, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL2.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 2), preHeight111 + shift + 2);
                            e.Graphics.DrawString(l0, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL3.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 3), preHeight111 + shift + 2);
                            e.Graphics.DrawString(l03, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL1.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 4) - 20, preHeight111 + shift + 2);
                            e.Graphics.DrawString(l02, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL2.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 5) - 20, preHeight111 + shift + 2);
                            e.Graphics.DrawString(l01, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL3.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 6) - 20, preHeight111 + shift + 2);
                            e.Graphics.DrawString(l00, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sL3.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 7) - 20, preHeight111 + shift + 2);


                            e.Graphics.DrawString(gold14, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sGold14.Width) / 2 - 15, preHeight111 + shift + (2 * shift));
                            e.Graphics.DrawString(gold18, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sGold18.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8) - 17, preHeight111 + shift + (2 * shift));
                            e.Graphics.DrawString(gold21, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sGold21.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 2) - 13, preHeight111 + shift + (2 * shift));
                            e.Graphics.DrawString(cash, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sCash.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 3) - 10, preHeight111 + shift + (2 * shift));
                            e.Graphics.DrawString(gold14, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sGold18.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 4) - 25, preHeight111 + shift + (2 * shift));
                            e.Graphics.DrawString(gold18, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sGold21.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 5) - 7, preHeight111 + shift + (2 * shift));
                            e.Graphics.DrawString(ogold21, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sCash.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 6) - 7, preHeight111 + shift + (2 * shift));
                            e.Graphics.DrawString(ocash, ff, Brushes.Black, ((margin + (margin + e.PageBounds.Width - (2 * margin)) / 8) - sCash.Width) / 2 + ((margin + e.PageBounds.Width - (2 * margin)) / 8 * 7) - 7, preHeight111 + shift + (2 * shift));
                        }
                        if (ii == (noPages - 1))
                        {
                            preHeight1 = preHeight111 + (2 * margin) + (2 * shift);
                            ////////////خاتمة الصفحة///////////////////////////
                            e.Graphics.DrawString(pLabel2, f2, Brushes.Black, (e.PageBounds.Width - sPLabel2.Width + margin) / 2, preHeight1);
                            e.Graphics.DrawString(user, f2, Brushes.Black, margin, preHeight1);
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
                    e.Graphics.DrawString(vLabel, f11, Brushes.Black, (e.PageBounds.Width / 2) - (sVLabel.Width - (2 * margin) / 2), preHeight1 - 2 * margin);
                    //////////خاتمة الصفحة///////////////////////////
                    e.Graphics.DrawString(pLabel2, f2, Brushes.Black, (e.PageBounds.Width - sPLabel2.Width + margin) / 2, preHeight1 - (2 * margin));
                    e.Graphics.DrawString(user, f2, Brushes.Black, margin, preHeight1 - (2 * margin));
                }
            }
        }

        public void add3_items(AutoCompleteStringCollection col)
        {
            col.Add("اجور صائغ إلى ذهب21");
            col.Add("اجور صائغ إلى ذهب14");
            col.Add("ذهب21 صائغ إلى اجور");
            col.Add("ذهب21 صائغ إلى ذهب14");
            col.Add("ذهب14 صائغ إلى اجور");
            col.Add("ذهب14 صائغ إلى ذهب21");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dt.Clear();
            dataGridView4.Visible = false;
            label1.Visible = true;
            label2.Visible = true;
            label7.Visible = false;
            label8.Visible = false;
            goldPrice.Visible = false;
            dateTimePicker1.Visible = true;
            dateTimePicker2.Visible = true;
            calculate.Visible = false;
            calc.Visible = false;
            deletePay.Visible = false;
            calculate.Text = "اجمع";
            int DvgHeight = 475;
            if (comboBox1.Text == "الفواتير")
            {
                calculate.Visible = true;
                L1.Visible = false;
                L2.Visible = false;
                L3.Visible = false;
                L4.Visible = false;
                myDiscount.Visible = false;
                forDiscount.Visible = false;
                dataGridView1.Height = DvgHeight;
                dataGridView2.Visible = false;
                dataGridView3.Visible = true;
                itemNameTxtbox.Visible = true;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
                calculate.Visible = true;
                try
                {
                    SqlDataAdapter DataRdr;
                    DataTable dt = new DataTable();
                    DataRdr = new SqlDataAdapter("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);
                    DataRdr.Fill(dt);
                    DataRow toInsert = dt.NewRow();
                    DataRow toInsert1 = dt.NewRow();
                    toInsert[0] = "اختر اسم الصائغ ...";
                    dt.Rows.InsertAt(toInsert, 0);
                    toInsert1[0] = "جميع الصاغة";
                    dt.Rows.InsertAt(toInsert1, 1);
                    itemNameTxtbox.DataSource = dt;
                    itemNameTxtbox.DisplayMember = "customerName";
                    con.Open();
                    SqlDataReader DataRdr1;
                    SqlCommand cmd1 = new SqlCommand("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);
                    cmd1.ExecuteNonQuery();
                    DataRdr1 = cmd1.ExecuteReader();
                    AutoCompleteStringCollection items = new AutoCompleteStringCollection();
                    while (DataRdr1.Read())
                    {
                        items.Add(DataRdr1.GetString(0));
                    }
                    itemNameTxtbox.AutoCompleteCustomSource = items;
                    con.Close();

                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }

            }
            else if (comboBox1.Text == "الدفعات")
            {
                L1.Visible = false;
                L2.Visible = false;
                L3.Visible = false;
                L4.Visible = false;
                myDiscount.Visible = false;
                forDiscount.Visible = false;
                dataGridView1.Height = DvgHeight;
                dataGridView2.Visible = false;
                dataGridView3.Visible = true;
                itemNameTxtbox.Visible = true;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
                calculate.Visible = true;

                try
                {
                    SqlDataAdapter DataRdr;
                    DataTable dt = new DataTable();
                    DataRdr = new SqlDataAdapter("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);
                    DataRdr.Fill(dt);
                    DataRow toInsert = dt.NewRow();
                    DataRow toInsert1 = dt.NewRow();
                    toInsert[0] = "اختر اسم الصائغ ...";
                    dt.Rows.InsertAt(toInsert, 0);
                    toInsert1[0] = "جميع الصاغة";
                    dt.Rows.InsertAt(toInsert1, 1);
                    itemNameTxtbox.DataSource = dt;
                    itemNameTxtbox.DisplayMember = "customerName";
                    con.Open();
                    SqlDataReader DataRdr1;
                    SqlCommand cmd1 = new SqlCommand("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);
                    cmd1.ExecuteNonQuery();
                    DataRdr1 = cmd1.ExecuteReader();
                    AutoCompleteStringCollection items = new AutoCompleteStringCollection();
                    while (DataRdr1.Read())
                    {
                        items.Add(DataRdr1.GetString(0));
                    }
                    itemNameTxtbox.AutoCompleteCustomSource = items;
                    con.Close();

                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }

            }
            else if (comboBox1.Text == "الحسومات")
            {
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
                itemNameTxtbox.Visible = true;

                dataGridView1.Height = DvgHeight;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
                try
                {
                    SqlDataAdapter DataRdr;
                    DataTable dt = new DataTable();
                    DataRdr = new SqlDataAdapter("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);
                    DataRdr.Fill(dt);
                    DataRow toInsert = dt.NewRow();
                    DataRow toInsert1 = dt.NewRow();
                    toInsert[0] = "اختر اسم الصائغ ...";
                    dt.Rows.InsertAt(toInsert, 0);
                    toInsert1[0] = "جميع الصاغة";
                    dt.Rows.InsertAt(toInsert1, 1);
                    itemNameTxtbox.DataSource = dt;
                    itemNameTxtbox.DisplayMember = "customerName";
                    con.Open();
                    SqlDataReader DataRdr1;
                    SqlCommand cmd1 = new SqlCommand("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);
                    cmd1.ExecuteNonQuery();
                    DataRdr1 = cmd1.ExecuteReader();
                    AutoCompleteStringCollection items = new AutoCompleteStringCollection();
                    while (DataRdr1.Read())
                    {
                        items.Add(DataRdr1.GetString(0));
                    }
                    itemNameTxtbox.AutoCompleteCustomSource = items;
                    con.Close();

                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }

            }
            else if (comboBox1.Text == "حركة الصندوق")
            {
                L1.Visible = false;
                L2.Visible = false;
                L3.Visible = false;
                L4.Visible = false;
                myDiscount.Visible = false;
                forDiscount.Visible = false;
                itemNameTxtbox.Visible = false;

                dataGridView1.Height = DvgHeight;
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
            }
            else if (comboBox1.Text == "حساب الشركة")
            {
                L1.Visible = false;
                L2.Visible = false;
                L3.Visible = false;
                L4.Visible = false;
                myDiscount.Visible = false;
                forDiscount.Visible = false;
                itemNameTxtbox.Visible = false;

                dataGridView1.Height = 70;
                dataGridView2.Height = 435;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
            }
            else if (comboBox1.Text == "حركة مصاغ")
            {
                //calculate.Visible = true;
                L1.Visible = false;
                L2.Visible = false;
                L3.Visible = false;
                L4.Visible = false;
                myDiscount.Visible = false;
                forDiscount.Visible = false;
                dataGridView1.Height = DvgHeight;
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
                itemNameTxtbox.Visible = true;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
                ////////
                con.ConnectionString = GlobalVar.dataBaseLocation;
                try
                {
                    SqlDataAdapter DataRdr;
                    DataTable dt = new DataTable();
                    DataRdr = new SqlDataAdapter("select itemName from items where userId = " + GlobalVar.id.ToString(), con);
                    DataRdr.Fill(dt);
                    DataRow toInsert = dt.NewRow();
                    toInsert[0] = "اختر اسم المصاغ ...";
                    dt.Rows.InsertAt(toInsert, 0);
                    itemNameTxtbox.DataSource = dt;
                    itemNameTxtbox.DisplayMember = "itemName";
                    con.Open();
                    SqlDataReader DataRdr1;
                    SqlCommand cmd1 = new SqlCommand("select itemName from items where userId = " + GlobalVar.id.ToString(), con);
                    cmd1.ExecuteNonQuery();
                    DataRdr1 = cmd1.ExecuteReader();
                    AutoCompleteStringCollection items = new AutoCompleteStringCollection();
                    while (DataRdr1.Read())
                    {
                        items.Add(DataRdr1.GetString(0));
                    }
                    itemNameTxtbox.AutoCompleteCustomSource = items;
                    con.Close();
                    System.Threading.Thread.Sleep(10);
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
                ////////
            }
            else if (comboBox1.Text == "المصوغات")
            {
                //calculate.Visible = true;
                L1.Visible = false;
                L2.Visible = false;
                L3.Visible = false;
                L4.Visible = false;
                myDiscount.Visible = false;
                forDiscount.Visible = false;
                dataGridView1.Height = DvgHeight;
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
                itemNameTxtbox.Visible = true;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
                ////////
                con.ConnectionString = GlobalVar.dataBaseLocation;
                System.Threading.Thread.Sleep(10);
                try
                {
                    SqlDataAdapter DataRdr;
                    DataTable dt = new DataTable();
                    DataRdr = new SqlDataAdapter("select itemName from items where userId = " + GlobalVar.id.ToString(), con);
                    DataRdr.Fill(dt);
                    DataRow toInsert = dt.NewRow();
                    toInsert[0] = "اختر اسم المصاغ ...";
                    dt.Rows.InsertAt(toInsert, 0);
                    itemNameTxtbox.DataSource = dt;
                    itemNameTxtbox.DisplayMember = "itemName";
                    con.Open();
                    SqlDataReader DataRdr1;
                    SqlCommand cmd1 = new SqlCommand("select itemName from items where userId = " + GlobalVar.id.ToString(), con);
                    cmd1.ExecuteNonQuery();
                    DataRdr1 = cmd1.ExecuteReader();
                    AutoCompleteStringCollection items = new AutoCompleteStringCollection();
                    while (DataRdr1.Read())
                    {
                        items.Add(DataRdr1.GetString(0));
                    }
                    itemNameTxtbox.AutoCompleteCustomSource = items;
                    con.Close();

                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else if (comboBox1.Text == "حساب صائغ")
            {
                L1.Visible = false;
                L2.Visible = false;
                L3.Visible = false;
                L4.Visible = false;
                myDiscount.Visible = false;
                forDiscount.Visible = false;
                dataGridView1.Height = DvgHeight;
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
                itemNameTxtbox.Visible = true;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
                ////////
                con.ConnectionString = GlobalVar.dataBaseLocation;
                System.Threading.Thread.Sleep(10);
                try
                {
                    SqlDataAdapter DataRdr;
                    DataTable dt = new DataTable();
                    DataRdr = new SqlDataAdapter("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);
                    DataRdr.Fill(dt);
                    DataRow toInsert = dt.NewRow();
                    toInsert[0] = "اختر اسم الصائغ ...";
                    dt.Rows.InsertAt(toInsert, 0);
                    itemNameTxtbox.DataSource = dt;
                    itemNameTxtbox.DisplayMember = "customerName";
                    con.Open();
                    SqlDataReader DataRdr1;
                    SqlCommand cmd1 = new SqlCommand("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);
                    cmd1.ExecuteNonQuery();
                    DataRdr1 = cmd1.ExecuteReader();
                    AutoCompleteStringCollection items = new AutoCompleteStringCollection();
                    while (DataRdr1.Read())
                    {
                        items.Add(DataRdr1.GetString(0));
                    }
                    itemNameTxtbox.AutoCompleteCustomSource = items;
                    con.Close();

                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }

            }
            else if (comboBox1.Text == "حسابات الصاغة")
            {
                calculate.Visible = true;
                L1.Visible = false;
                L2.Visible = false;
                L3.Visible = false;
                L4.Visible = false;
                myDiscount.Visible = false;
                forDiscount.Visible = false;
                dataGridView1.Height = DvgHeight;
                dataGridView2.Visible = false;
                dataGridView3.Visible = true;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
                itemNameTxtbox.Visible = true;
                con.ConnectionString = GlobalVar.dataBaseLocation;
                System.Threading.Thread.Sleep(10);
                try
                {
                    SqlDataAdapter DataRdr;
                    DataTable dt = new DataTable();
                    DataRdr = new SqlDataAdapter("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);
                    DataRdr.Fill(dt);
                    DataRow toInsert = dt.NewRow();
                    toInsert[0] = "اختر اسم الصائغ ...";
                    dt.Rows.InsertAt(toInsert, 0);
                    itemNameTxtbox.DataSource = dt;
                    itemNameTxtbox.DisplayMember = "customerName";
                    con.Open();
                    SqlDataReader DataRdr1;
                    SqlCommand cmd1 = new SqlCommand("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);
                    cmd1.ExecuteNonQuery();
                    DataRdr1 = cmd1.ExecuteReader();
                    AutoCompleteStringCollection items = new AutoCompleteStringCollection();
                    while (DataRdr1.Read())
                    {
                        items.Add(DataRdr1.GetString(0));
                    }
                    itemNameTxtbox.AutoCompleteCustomSource = items;
                    con.Close();
                    isFill = true;
                    show_Click(sender, e);
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else if (comboBox1.Text == "القيد اليومي")
            {
                L1.Visible = false;
                L2.Visible = false;
                L3.Visible = false;
                L4.Visible = false;
                myDiscount.Visible = false;
                forDiscount.Visible = false;
                dataGridView1.Height = DvgHeight;
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
                itemNameTxtbox.Visible = false;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
            }
            else if (comboBox1.Text == "كشف حساب تفصيلي")
            {
                L1.Visible = false;
                L2.Visible = false;
                L3.Visible = false;
                L4.Visible = false;
                myDiscount.Visible = false;
                forDiscount.Visible = false;
                dataGridView1.Height = 230;
                dataGridView2.Height = 260;
                dataGridView2.Location = new Point(8, 385);
                dataGridView2.Visible = true;
                dataGridView3.Visible = true;
                itemNameTxtbox.Visible = true;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
                ////////
                con.ConnectionString = GlobalVar.dataBaseLocation;
                System.Threading.Thread.Sleep(10);
                try
                {
                    SqlDataAdapter DataRdr;
                    DataTable dt = new DataTable();
                    DataRdr = new SqlDataAdapter("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);
                    DataRdr.Fill(dt);
                    DataRow toInsert = dt.NewRow();
                    toInsert[0] = "اختر اسم الصائغ ...";
                    dt.Rows.InsertAt(toInsert, 0);
                    itemNameTxtbox.DataSource = dt;
                    itemNameTxtbox.DisplayMember = "customerName";
                    con.Open();
                    SqlDataReader DataRdr1;
                    SqlCommand cmd1 = new SqlCommand("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);
                    cmd1.ExecuteNonQuery();
                    DataRdr1 = cmd1.ExecuteReader();
                    AutoCompleteStringCollection items = new AutoCompleteStringCollection();
                    while (DataRdr1.Read())
                    {
                        items.Add(DataRdr1.GetString(0));
                    }
                    itemNameTxtbox.AutoCompleteCustomSource = items;
                    con.Close();

                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }

            }
            else if (comboBox1.Text == "تنفيذ حسومات"||comboBox1.Text=="تنفيذ تحويلات")
            {
                itemNameTxtbox.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                L1.Visible = false;
                L2.Visible = false;
                L3.Visible = false;
                L4.Visible = false;
                myDiscount.Visible = false;
                forDiscount.Visible = false;
                dataGridView1.Height = 218;
                dataGridView4.Height = 260;
                dataGridView4.Location = new Point(8, 371);
                dataGridView4.Visible = true;
                dataGridView3.Visible = false;
                dataGridView2.Visible = false;
                itemNameTxtbox.Visible = true;
                dataGridView1.DataSource = null;
                dataGridView2.DataSource = null;
                dataGridView3.DataSource = null;
                dataGridView4.DataSource = null;
                print.Visible = false;
                calculate.Visible = true;
                if (comboBox1.Text == "تنفيذ حسومات")
                    calculate.Text = "تنفيذ الحسم";
                else if (comboBox1.Text == "تنفيذ تحويلات")
                {
                    calculate.Text = "تنفيذ التحويل";
                    label7.Visible = true;
                    label8.Visible = true;
                    goldPrice.Visible = true;
                    calc.Visible = true;
                    deletePay.Visible = true;
                }
                ////////
                con.ConnectionString = GlobalVar.dataBaseLocation;
                System.Threading.Thread.Sleep(10);
                try
                {
                    SqlDataAdapter DataRdr;
                    DataTable dt = new DataTable();
                    DataRdr = new SqlDataAdapter("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);
                    DataRdr.Fill(dt);
                    DataRow toInsert = dt.NewRow();
                    toInsert[0] = "اختر اسم الصائغ ...";
                    dt.Rows.InsertAt(toInsert, 0);
                    itemNameTxtbox.DataSource = dt;
                    itemNameTxtbox.DisplayMember = "customerName";
                    con.Open();
                    SqlDataReader DataRdr1;
                    SqlCommand cmd1 = new SqlCommand("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);
                    cmd1.ExecuteNonQuery();
                    DataRdr1 = cmd1.ExecuteReader();
                    AutoCompleteStringCollection items = new AutoCompleteStringCollection();
                    while (DataRdr1.Read())
                    {
                        items.Add(DataRdr1.GetString(0));
                    }
                    itemNameTxtbox.AutoCompleteCustomSource = items;
                    con.Close();

                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                L1.Visible = false;
                L2.Visible = false;
                L3.Visible = false;
                L4.Visible = false;
                myDiscount.Visible = false;
                forDiscount.Visible = false;

                MessageBox.Show("يرجى اختيار نوع البيانات المطلوبة");
            }
            if (comboBox1.Text != "حركة مصاغ")
            {
                if (comboBox1.Text != "كشف حساب تفصيلي")
                {
                    show_Click(sender, e);
                }
            }
        }

        public void printHeaderFooter(System.Drawing.Printing.PrintPageEventArgs e,int noPages,int ii)
        {
            float marginWidth = 35;
            float marginHeight = 0;
            Font ff = new Font("Arial", 8, FontStyle.Bold);
            Font ff1 = new Font("Arial", 24, FontStyle.Bold);
			string logo = GlobalVar.Logo;
			SizeF sLogo = e.Graphics.MeasureString(logo, ff1);
			e.Graphics.DrawString(logo, ff1, Brushes.Black, (e.PageBounds.Width - sLogo.Width) / 2, (marginWidth / 2) + marginHeight);

			string strR = "الاتصال بالرقم 0956799996" + " iGOLD " + "للاستعلام عن برنامج";
            SizeF sStrR = e.Graphics.MeasureString(strR, ff);
            string page = "الصفحة " + (ii + 1) + " من " + noPages + " صفحات";
            SizeF sPage = e.Graphics.MeasureString(page, ff);
            e.Graphics.DrawString(page, ff, Brushes.Black, marginWidth, marginHeight+65);
            e.Graphics.DrawImage(Properties.Resources.igoldBlack, e.PageBounds.Width - 100 - marginWidth, (marginWidth / 2) + marginHeight, 100, 22);
			//e.Graphics.DrawImage(Properties.Resources.logo, (e.PageBounds.Width - (2 * marginWidth) - 125) / 2, (marginWidth / 2) + marginHeight, 250, 80);
            e.Graphics.DrawLine(Pens.Black, marginWidth, e.PageBounds.Height - (1 * marginWidth)-3 , e.PageBounds.Width - marginWidth, e.PageBounds.Height - (1 * marginWidth)-3);
            e.Graphics.DrawImage(Properties.Resources.igoldBlack, e.PageBounds.Width - 100 - marginWidth, e.PageBounds.Height - marginWidth , 100, 22);
            e.Graphics.DrawString(strR, ff, Brushes.Black, e.PageBounds.Width - 103 - marginWidth - sStrR.Width, e.PageBounds.Height - marginWidth +3 );
            e.Graphics.DrawLine(Pens.Black, marginWidth, 61 + marginHeight, e.PageBounds.Width - marginWidth, 61 + marginHeight);
            e.Graphics.DrawLine(Pens.Black, marginWidth, 60 + marginHeight, e.PageBounds.Width - marginWidth, 60 + marginHeight);
        }

        private void itemNameTxtbox_SelectedIndexChanged(object sender, EventArgs e)
        {       
            if (comboBox1.Text=="المصوغات")
            {
                for ( i1 = 0 ; i1 < dataGridView1.RowCount - 1 ; i1++)
                {
                    if (itemNameTxtbox.Text == dataGridView1.Rows[i1].Cells[4].Value.ToString())
                    {
                        dataGridView1.Rows[i1].Selected = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[i1].Cells[4];
                    }                 
                }
                show_Click(sender, e);
            }
            else if (comboBox1.Text == "حسابات الصاغة")
            {
               
                    for (i1 = 0; i1 < dataGridView1.RowCount - 1; i1++)
                {
                    if (itemNameTxtbox.Text == dataGridView1.Rows[i1].Cells[4].Value.ToString())
                    {
                        dataGridView1.Rows[i1].Selected = true;
                        dataGridView1.CurrentCell = dataGridView1.Rows[i1].Cells[4];
                        return;
                    }
                }
                if (isFill)
                {
                    if (itemNameTxtbox.Text != "جميع الصاغة")
                    {
                        if (comboBox1.Text != "اختر اسم الصائغ ...")
                            MessageBox.Show("حساب الصائغ " + itemNameTxtbox.Text + " رصيد ");
                    }
                    show_Click(sender, e);
                }
           
            }
            else if (comboBox1.Text=="حركة مصاغ")
            {
                if (itemNameTxtbox.Text!= "اختر اسم المصاغ ..."&& itemNameTxtbox.Text.Trim() != "")
                show_Click(sender, e);
            }
        }

        private void show_Click(object sender, EventArgs e)
        {
            con.Close();
            try
            {
                if (comboBox1.Text == "الفواتير")
                {
                    printDocument1.DefaultPageSettings.Landscape = false;
                    print.Visible = true;
                    string d1 = dateTimePicker1.Value.AddDays(-1).ToString("MM/dd/yyyy 23:59:59");
                    string d2 = dateTimePicker2.Value.ToString("MM/dd/yyyy 23:59:59");
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    if (itemNameTxtbox.Text == "جميع الصاغة")
                    {
                        cmd.CommandText = "select billDateTime,billTotal14,billTotal21,billTotalCash,billType,customers.customerName,billId from bills " +
                            "inner join customers on bills.customerId=customers.customerId where bills.userId = " + GlobalVar.id.ToString() + "and billDateTime between '" + d1 + "' and '" + d2 + "' order by billDateTime";
                    }
                    else if (itemNameTxtbox.Text == "اختر اسم الصائغ ...")
                    {
                        //MessageBox.Show("يرجى اختيار اسم صائغ من القائمة");
                        con.Close();
                        return;
                    }
                    else
                    {
                        itm.customerName1 = itemNameTxtbox.Text;
                        int id = itm.GetCustomerid();
                        cmd.CommandText = "select billDateTime,billTotal14,billTotal21,billTotalCash,billType,customers.customerName,billId from bills " +
                           "inner join customers on bills.customerId=customers.customerId where bills.customerId='" + id + "' and billDateTime between '" + d1 + "' and '" + d2 + "' order by billDateTime ";
                    }
                    cmd.ExecuteNonQuery();
                    dataGridView1.DataSource = null;
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    con.Close();
                    dataGridView1.Columns[1].HeaderText = GlobalVar.gold14Label;
                    dataGridView1.Columns[0].HeaderText = "التاريخ";
                    dataGridView1.Columns[2].HeaderText = GlobalVar.gold21Label;
                    dataGridView1.Columns[3].HeaderText = "الاجور";
                    dataGridView1.Columns[5].HeaderText = "اسم الصائغ";
                    dataGridView1.Columns[4].HeaderText = "نوع الفاتورة";
                    dataGridView1.Columns[6].HeaderText = "Id";
                    dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Ascending);
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                    dataGridView1.EnableHeadersVisualStyles = false;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    //dataGridView1.Columns[6].Width = -10;
                    calculate_Click(sender, e);
                }
                else if (comboBox1.Text == "الدفعات")
                {
                    printDocument1.DefaultPageSettings.Landscape = true;
                    dataGridView3.Visible = true;
                    print.Visible = true;
                    string d1 = dateTimePicker1.Value.AddDays(-1).ToString("MM/dd/yyyy 23:59:59");
                    string d2 = dateTimePicker2.Value.ToString("MM/dd/yyyy 23:59:59");
                    try
                    { con.Open(); }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                    SqlCommand cmd = con.CreateCommand();
                    SqlCommand cmd1 = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd1.CommandType = CommandType.Text;
                    if (itemNameTxtbox.Text == "جميع الصاغة")
                    {
                        cmd.CommandText = "select paymentNotice,payment14,payment18,payment21,paymentCash,realPayment14,realPayment18,realPayment21,realPaymentCash,paymentDateTime,paymentTypeString,customers.customerName,paymentId from payments inner join customers on payments.customerId=customers.customerId where payments.userId = " + GlobalVar.id.ToString() + " and paymentDateTime between '" + d1 + "' and '" + d2 + "' order by paymentDateTime";
                        cmd1.CommandText = "select payment14,payment18,payment21,paymentCash,realPayment14,realPayment18,realPayment21,realPaymentCash,paymentDateTime,paymentTypeString,customers.customerName,paymentId from payments inner join customers on payments.customerId=customers.customerId where payments.userId = " + GlobalVar.id.ToString() + " and paymentDateTime between '" + d1 + "' and '" + d2 + "' order by paymentDateTime";
                    }
                    else if (itemNameTxtbox.Text == "اختر اسم الصائغ ...")
                    {
                        //MessageBox.Show("يرجى اختيار اسم صائغ من القائمة");
                        con.Close();
                        return;
                    }
                    else
                    {
                        itm.customerName1 = itemNameTxtbox.Text;
                        int id = itm.GetCustomerid();
                        cmd.CommandText = "select paymentNotice,payment14,payment18,payment21,paymentCash,realPayment14,realPayment18,realPayment21," +
                            "realPaymentCash,customers.customerName,paymentTypeString,paymentDateTime,paymentId" +
                            " from payments " +
                            "inner join customers on payments.customerId=customers.customerId where " +
                            "payments.customerId='" + id + "' and paymentDateTime between '" + d1 + "' and '" +
                            d2 + "' order by paymentDateTime";
                        cmd1.CommandText = "select payment14,payment18,payment21," +
                            "paymentCash,realPayment14,realPayment18,realPayment21,realPaymentCash,paymentDateTime," +
                            "paymentTypeString,customers.customerName,paymentId from payments " +
                            "inner join customers on payments.customerId=customers.customerId where " +
                            "payments.customerId='" + id + "' and paymentDateTime between '" + d1 + "' and '" +
                            d2 + "' order by paymentDateTime";
                    }
                    cmd.ExecuteNonQuery();
                    cmd1.ExecuteNonQuery();
                    dataGridView1.DataSource = null;
                    dataGridView2.DataSource = null;
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    DataTable dt1 = new DataTable();

                    dataGridView1.DataSource = dt;
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    da1.Fill(dt1);
                    dataGridView2.DataSource = dt1;
                    con.Close();
                    dataGridView1.Columns[0].HeaderText = "ملاحظات";
                    dataGridView1.Columns[1].HeaderText = GlobalVar.gold14Label;
                    dataGridView1.Columns[2].HeaderText = GlobalVar.gold18Label;
                    dataGridView1.Columns[3].HeaderText = GlobalVar.gold21Label;
                    dataGridView1.Columns[4].HeaderText = GlobalVar.cashLabel;
                    dataGridView1.Columns[5].HeaderText = "فعلي ذهب14";
                    dataGridView1.Columns[6].HeaderText = "فعلي ذهب18";
                    dataGridView1.Columns[7].HeaderText = "فعلي ذهب21";
                    dataGridView1.Columns[8].HeaderText = "فعلي اجور";
                    dataGridView1.Columns[9].HeaderText = "التاريخ";
                    dataGridView1.Columns[10].HeaderText = "نوع الدفعة";
                    dataGridView1.Columns[11].HeaderText = "اسم الصائغ";
                    dataGridView1.Columns[12].HeaderText = "Id";
                    //dataGridView1.Columns[8].Width = -10;
                    //dataGridView1.Columns[0].Width = 150;
                    dataGridView2.Columns[0].HeaderText = GlobalVar.gold14Label;
                    dataGridView2.Columns[1].HeaderText = GlobalVar.gold18Label;
                    dataGridView2.Columns[2].HeaderText = GlobalVar.gold21Label;
                    dataGridView2.Columns[3].HeaderText = GlobalVar.cashLabel;
                    dataGridView2.Columns[4].HeaderText = "فعلي ذهب14";
                    dataGridView2.Columns[5].HeaderText = "فعلي ذهب18";
                    dataGridView2.Columns[6].HeaderText = "فعلي ذهب21";
                    dataGridView2.Columns[7].HeaderText = "فعلي اجور";
                    dataGridView2.Columns[8].HeaderText = "التاريخ";
                    dataGridView2.Columns[9].HeaderText = "نوع الدفعة";
                    dataGridView2.Columns[10].HeaderText = "اسم الصائغ";
                    dataGridView2.Columns[11].HeaderText = "Id";
                    dataGridView2.Columns[11].Width = -10;
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                    dataGridView1.EnableHeadersVisualStyles = false;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                    dataGridView3.EnableHeadersVisualStyles = false;
                    foreach (DataGridViewColumn col in dataGridView3.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    calculate_Click(sender, e);
                }
                else if (comboBox1.Text == "الحسومات")
                {
                    printDocument1.DefaultPageSettings.Landscape = false;
                    print.Visible = true;
                    dataGridView3.Visible = true;
                    string d1 = dateTimePicker1.Value.AddDays(-1).ToString("MM/dd/yyyy 23:59:59");
                    string d2 = dateTimePicker2.Value.ToString("MM/dd/yyyy 23:59:59");
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    if (itemNameTxtbox.Text == "جميع الصاغة")
                    {
                        cmd.CommandText = "select discountDetails,discountAmount,billId,discountDateTime,customers.customerName," +
                        "discountId from discount inner join customers on discount.customerId" +
                        "=customers.customerId where discount.userId = " + GlobalVar.id.ToString() + "and  discountDateTime between '" + d1 + "' and '" + d2 + "' order " +
                        "by discountDateTime";
                    }
                    else if (itemNameTxtbox.Text == "اختر اسم الصائغ ...")
                    {
                        //MessageBox.Show("يرجى اختيار اسم صائغ من القائمة");
                        con.Close();
                        return;
                    }
                    else
                    {
                        itm.customerName1 = itemNameTxtbox.Text;
                        int id = itm.GetCustomerid();
                        cmd.CommandText = "select discountDetails,discountAmount,billId,discountDateTime,customers.customerName," +
                                                "discountId from discount inner join customers on " +
                                                "discount.customerId=customers.customerId where discount.customerId='" +
                                                id + "' and discountDateTime between '" + d1 + "' and '" + d2 + "' order " +
                                                "by discountDateTime";
                    }
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    dataGridView1.DataSource = null;
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    dataGridView2.DataSource = dt;
                    con.Close();
                    dataGridView1.Columns[0].HeaderText = "تفاصيل الحسم";
                    dataGridView1.Columns[1].HeaderText = "مقدار الحسم";
                    dataGridView1.Columns[2].HeaderText = "رقم الفاتورة";
                    dataGridView1.Columns[3].HeaderText = "اسم الصائغ";
                    dataGridView1.Columns[4].HeaderText = "التاريخ";
                    dataGridView1.Columns[5].HeaderText = "Id";
                    dataGridView2.Columns[0].HeaderText = "تفاصيل الحسم";
                    dataGridView2.Columns[1].HeaderText = "مقدار الحسم";
                    dataGridView2.Columns[2].HeaderText = "رقم الفاتورة";
                    dataGridView2.Columns[4].HeaderText = "اسم الصائغ";
                    dataGridView2.Columns[3].HeaderText = "التاريخ";
                    dataGridView2.Columns[5].HeaderText = "Id";
                    dataGridView1.Columns[5].Width = -10;
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("ذهب14 لنا", typeof(string));
                    dt1.Columns.Add("ذهب14 له", typeof(string));
                    dt1.Columns.Add("ذهب18 لنا", typeof(string));
                    dt1.Columns.Add("ذهب18 له", typeof(string));
                    dt1.Columns.Add("ذهب21 لنا", typeof(string));
                    dt1.Columns.Add("ذهب21 له", typeof(string));
                    dt1.Columns.Add("اجور لنا", typeof(string));
                    dt1.Columns.Add("اجور له", typeof(string));

                    dt1.Rows.Add("0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0", "0.0");
                    dataGridView3.DataSource = dt1;
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(4, 4) == GlobalVar.cashLabel)
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(9, 2) == "له")
                            {
                                dataGridView3.Rows[0].Cells[7].Value = Convert.ToString(Convert.ToDecimal(dataGridView3.Rows[0].Cells[7].Value.ToString()) + Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString()));
                            }
                            else if (dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(9, 3) == "لنا")
                            {
                                dataGridView3.Rows[0].Cells[6].Value = Convert.ToString(Convert.ToDecimal(dataGridView3.Rows[0].Cells[6].Value.ToString()) + Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString()));
                            }
                        }
                        else if (dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(4, 5) == GlobalVar.gold21Label)
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(10, 2) == "له")
                            {
                                dataGridView3.Rows[0].Cells[5].Value = Convert.ToString(Convert.ToDecimal(dataGridView3.Rows[0].Cells[5].Value.ToString()) + Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString()));
                            }
                            else if (dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(10, 3) == "لنا")
                            {
                                dataGridView3.Rows[0].Cells[4].Value = Convert.ToString(Convert.ToDecimal(dataGridView3.Rows[0].Cells[4].Value.ToString()) + Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString()));
                            }
                        }
                        else if (dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(4, 5) == GlobalVar.gold18Label)
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(10, 2) == "له")
                            {
                                dataGridView3.Rows[0].Cells[3].Value = Convert.ToString(Convert.ToDecimal(dataGridView3.Rows[0].Cells[3].Value.ToString()) + Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString()));
                            }
                            else if (dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(10, 3) == "لنا")
                            {
                                dataGridView3.Rows[0].Cells[2].Value = Convert.ToString(Convert.ToDecimal(dataGridView3.Rows[0].Cells[2].Value.ToString()) + Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString()));
                            }
                        }
                        else if (dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(4, 5) == GlobalVar.gold14Label)
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(10, 2) == "له")
                            {
                                dataGridView3.Rows[0].Cells[1].Value = Convert.ToString(Convert.ToDecimal(dataGridView3.Rows[0].Cells[1].Value.ToString()) + Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString()));
                            }
                            else if (dataGridView1.Rows[i].Cells[0].Value.ToString().Substring(10, 3) == "لنا")
                            {
                                dataGridView3.Rows[0].Cells[0].Value = Convert.ToString(Convert.ToDecimal(dataGridView3.Rows[0].Cells[0].Value.ToString()) + Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value.ToString()));
                            }
                        }
                    }
                    //con.Open();
                    //SqlCommand cmd1 = con.CreateCommand();
                    //cmd1.CommandType = CommandType.Text;
                    //cmd1.CommandText = "select totalMyDiscounts,totalForDiscounts from fonding where userId = " + GlobalVar.id.ToString();
                    //cmd1.ExecuteNonQuery();
                    //DataTable dt1 = new DataTable();
                    //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    //da1.Fill(dt1);
                    //con.Close();
                    //if (dt.Rows.Count > 0)
                    //{
                    //    L1.Visible = true;
                    //    L2.Visible = true;
                    //    L3.Visible = true;
                    //    L4.Visible = true;
                    //    myDiscount.Visible = true;
                    //    forDiscount.Visible = true;
                    //    myDiscount.Text = dt1.Rows[0][0].ToString();
                    //    forDiscount.Text = dt1.Rows[0][1].ToString();
                    //}
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                    dataGridView1.EnableHeadersVisualStyles = false;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                    dataGridView3.EnableHeadersVisualStyles = false;
                    foreach (DataGridViewColumn col in dataGridView3.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (comboBox1.Text == "حركة الصندوق")
                {
                    printDocument1.DefaultPageSettings.Landscape = true;
                    print.Visible = true;
                    string d1 = dateTimePicker1.Value.AddDays(-1).ToString("MM/dd/yyyy 23:59:59");
                    string d2 = dateTimePicker2.Value.AddDays(1).ToString("MM/dd/yyyy 23:59:59");
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select gold14Add,gold18Add,gold21Add,cashAdd,lastPaymentId,lastbillId,daily14,daily18,daily21,dailyCash,datedate from dailyReport where userId = " + GlobalVar.id.ToString() + " and datedate between '" + d1 + "' and '" + d2 + "' order by datedate";
                    cmd.ExecuteNonQuery();
                    dataGridView1.DataSource = null;
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    con.Close();
                    dataGridView1.Columns[0].HeaderText = "تغير ذهب14";
                    dataGridView1.Columns[1].HeaderText = "تغير ذهب18";
                    dataGridView1.Columns[2].HeaderText = "تغير ذهب21";
                    dataGridView1.Columns[3].HeaderText = "تغير اجور";
                    dataGridView1.Columns[4].HeaderText = "رقم آخر دفعة";
                    dataGridView1.Columns[5].HeaderText = "رقم آخر فاتورة";
                    dataGridView1.Columns[6].HeaderText = "رصيد ذهب14";
                    dataGridView1.Columns[7].HeaderText = "رصيد ذهب18";
                    dataGridView1.Columns[8].HeaderText = "رصيد ذهب21";
                    dataGridView1.Columns[9].HeaderText = "رصيد اجور";
                    dataGridView1.Columns[10].HeaderText = "التاريخ";
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                    dataGridView1.EnableHeadersVisualStyles = false;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
                else if (comboBox1.Text == "حساب الشركة")
                {
                    print.Visible = false; ;
                    string d1 = dateTimePicker1.Value.AddDays(-1).ToString("MM/dd/yyyy 23:59:59");
                    string d2 = dateTimePicker1.Value.ToString("MM/dd/yyyy 23:59:59");
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select gold14Add,gold18Add,gold21Add,cashAdd,lastPaymentId,lastbillId,daily14,daily18,daily21,dailyCash,datedate from dailyReport where userId = " + GlobalVar.id.ToString() + " and datedate between '" + d1 + "' and '" + d2 + "' order by datedate";
                    cmd.ExecuteNonQuery();
                    dataGridView1.DataSource = null;
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    con.Close();
                    dataGridView1.Columns[0].HeaderText = "تغير ذهب14";
                    dataGridView1.Columns[1].HeaderText = "تغير ذهب18";
                    dataGridView1.Columns[2].HeaderText = "تغير ذهب21";
                    dataGridView1.Columns[3].HeaderText = "تغير اجور";
                    dataGridView1.Columns[4].HeaderText = "رقم آخر دفعة";
                    dataGridView1.Columns[5].HeaderText = "رقم آخر فاتورة";
                    dataGridView1.Columns[6].HeaderText = "رصيد ذهب14";
                    dataGridView1.Columns[7].HeaderText = "رصيد ذهب18";
                    dataGridView1.Columns[8].HeaderText = "رصيد ذهب21";
                    dataGridView1.Columns[9].HeaderText = "رصيد اجور";
                    dataGridView1.Columns[10].HeaderText = "بداية الكشف";
                    System.Threading.Thread.Sleep(10);
                    /////////////////////////////////
                    string d11 = dateTimePicker1.Value.AddDays(-1).ToString("MM/dd/yyyy 23:59:59");
                    string d21 = dateTimePicker2.Value.AddDays(1).ToString("MM/dd/yyyy 23:59:59");
                    con.Open();
                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    paymentdb_Class cus = new paymentdb_Class();
                    cmd1.CommandText = "(select paymentDateTime , payment14, payment18, payment21, paymentCash, customers.customerName , paymentTypeString , paymentId from payments inner join customers on payments.customerId=customers.customerId where (payments.userId = " + GlobalVar.id.ToString() + "and paymentDateTime between '" + d11 + "' and '" + d21 + "')) UNION (select billDateTime , billTotal14 , billTotal18 , billTotal21 , billTotalCash , customers.customerName , billType , billId  from bills inner join customers on bills.customerId=customers.customerId where (bills.userId = " + GlobalVar.id.ToString() + " and billDateTime between '" + d11 + "' and '" + d21 + "'))";
                    try
                    {
                        cmd1.ExecuteNonQuery();

                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                    dataGridView2.DataSource = null;
                    DataTable dt1 = new DataTable();
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    da1.Fill(dt1);
                    dataGridView2.DataSource = dt1;
                    con.Close();
                    dataGridView2.Columns[7].HeaderText = "id";
                    dataGridView2.Columns[6].HeaderText = "نوع الحركة";
                    dataGridView2.Columns[5].HeaderText = "اسم الصائغ";
                    dataGridView2.Columns[4].HeaderText = "الاجور";
                    dataGridView2.Columns[3].HeaderText = GlobalVar.gold21Label;
                    dataGridView2.Columns[2].HeaderText = GlobalVar.gold18Label;
                    dataGridView2.Columns[1].HeaderText = GlobalVar.gold14Label;
                    dataGridView2.Columns[0].HeaderText = "تاريخ الحركة";
                    dataGridView2.Visible = true;
                    System.Threading.Thread.Sleep(10);
                    ///////////////////////////////////////////
                    string d12 = dateTimePicker2.Value.AddDays(-1).ToString("MM/dd/yyyy 12:00:00");
                    string d22 = dateTimePicker2.Value.ToString("MM/dd/yyyy 23:59:59");
                    con.Open();
                    SqlCommand cmd2 = con.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "select gold14Add,gold18Add,gold21Add,cashAdd,lastPaymentId,lastbillId,daily14,daily18,daily21,dailyCash,datedate from dailyReport where (userId = " + GlobalVar.id.ToString() + " and datedate between '" + d12 + "' and '" + d22 + "') order by datedate";
                    cmd2.ExecuteNonQuery();
                    dataGridView3.DataSource = null;
                    DataTable dt2 = new DataTable();
                    SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                    da2.Fill(dt2);
                    dataGridView3.DataSource = dt2;
                    con.Close();
                    dataGridView3.Columns[0].HeaderText = "تغير ذهب14";
                    dataGridView3.Columns[1].HeaderText = "تغير ذهب18";
                    dataGridView3.Columns[2].HeaderText = "تغير ذهب21";
                    dataGridView3.Columns[3].HeaderText = "تغير اجور";
                    dataGridView3.Columns[4].HeaderText = "رقم آخر دفعة";
                    dataGridView3.Columns[5].HeaderText = "رقم آخر فاتورة";
                    dataGridView3.Columns[6].HeaderText = "رصيد ذهب14";
                    dataGridView3.Columns[7].HeaderText = "رصيد ذهب18";
                    dataGridView3.Columns[8].HeaderText = "رصيد ذهب21";
                    dataGridView3.Columns[9].HeaderText = "رصيد اجور";
                    dataGridView3.Columns[10].HeaderText = "نهاية الكشف";
                    dataGridView3.Visible = true;
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

                }
                else if (comboBox1.Text == "حركة مصاغ")
                {
                    if (itemNameTxtbox.Text != "اختر اسم المصاغ ..." && itemNameTxtbox.Text.Trim() != "")
                    {
                        try
                        {
                            printDocument1.DefaultPageSettings.Landscape = false;
                            print.Visible = true;
                            string d1 = dateTimePicker1.Value.AddDays(-1).ToString("MM/dd/yyyy 23:59:59");
                            string d2 = dateTimePicker2.Value.ToString("MM/dd/yyyy 23:59:59");
                            itm.itemName1 = itemNameTxtbox.Text;
                            int iId = itm.getItemId();
                            itm.itemName1 = itemNameTxtbox.Text;
                            int carat = itm.getItemCarat();
                            if (carat == 21 || carat == 18 || carat == 14)
                            {
                                con.Open();
                                SqlCommand cmd = con.CreateCommand();
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = "(select itemCount,itemWeight,bills.billType,billDetails.billId as ID," +
                                    " customers.customerName,bills.billDateTime from billDetails inner join bills on" +
                                    " billDetails.billId = bills.billId   inner join customers on customers.customerId = " +
                                    " bills.customerId where itemId ='" + iId + "' and billDateTime between '" + d1 + "' and '" +
                                    d2 + "') union (select item" + carat + "Count,realPayment" + carat + ", paymentNotice ," +
                                    " paymentId ,customers.customerName,paymentDateTime  from payments inner join customers" +
                                    " on payments.customerId = customers.customerId where item" + carat + "Id ='" + iId +
                                    "' and paymentDateTime between '" + d1 + "' and '" + d2 + "' )";
                                cmd.ExecuteNonQuery();
                                dataGridView1.DataSource = null;
                                DataTable dt = new DataTable();
                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                da.Fill(dt);
                                dataGridView1.DataSource = dt;
                                dataGridView2.DataSource = dt;
                                con.Close();
                                dataGridView1.Columns[5].HeaderText = "تاريخ العملية";
                                dataGridView1.Columns[3].HeaderText = "ID";
                                dataGridView1.Columns[4].HeaderText = "اسم العميل";
                                dataGridView1.Columns[2].HeaderText = "نوع العملية";
                                dataGridView1.Columns[1].HeaderText = "الوزن";
                                dataGridView1.Columns[0].HeaderText = "العدد";
                                dataGridView2.Columns[5].HeaderText = "تاريخ العملية";
                                dataGridView2.Columns[3].HeaderText = "ID";
                                dataGridView2.Columns[4].HeaderText = "اسم العميل";
                                dataGridView2.Columns[2].HeaderText = "نوع العملية";
                                dataGridView2.Columns[1].HeaderText = "الوزن";
                                dataGridView2.Columns[0].HeaderText = "العدد";
                                ////////////////////////
                                con.Open();
                                SqlCommand cmd1 = con.CreateCommand();
                                cmd1.CommandType = CommandType.Text;
                                cmd1.CommandText = "select itemFees ,itemCount,itemWeight,openFees,openCount,openWeight,itemCarat,itemName from items where itemId = '" + iId + "'";
                                cmd1.ExecuteNonQuery();
                                DataTable dt1 = new DataTable();
                                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                                da1.Fill(dt1);
                                dataGridView3.DataSource = dt1;
                                con.Close();
                                dataGridView3.Columns[7].HeaderText = "اسم المصاغ";
                                dataGridView3.Columns[6].HeaderText = "عيار المصاغ";
                                dataGridView3.Columns[5].HeaderText = "وزن المصاغ الأولي";
                                dataGridView3.Columns[4].HeaderText = "عدد قطع المصاغ الأولي";
                                dataGridView3.Columns[3].HeaderText = "اجور صياغة الأولية";
                                dataGridView3.Columns[2].HeaderText = "وزن المصاغ الحالي";
                                dataGridView3.Columns[1].HeaderText = "عدد قطع المصاغ الحالي";
                                dataGridView3.Columns[0].HeaderText = "اجور صياغة الحالية";
                                dataGridView3.Visible = true;
                                ////////////////////////
                                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                                dataGridView1.EnableHeadersVisualStyles = false;
                                foreach (DataGridViewColumn col in dataGridView1.Columns)
                                {
                                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                                dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                                dataGridView3.EnableHeadersVisualStyles = false;
                                foreach (DataGridViewColumn col in dataGridView3.Columns)
                                {
                                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            con.Close();
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else if (comboBox1.Text == "المصوغات")
                {
                    try
                    {
                        printDocument1.DefaultPageSettings.Landscape = false;
                        print.Visible = true;
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        SqlCommand cmd11 = con.CreateCommand();
                        cmd11.CommandType = CommandType.Text;
                        cmd.CommandText = "select itemFees ,itemCount,itemWeight,itemCarat,itemName from items where userId = " + GlobalVar.id.ToString();
                        cmd11.CommandText = "select itemFees ,itemCount,itemWeight,itemName from items where userId = " + GlobalVar.id.ToString();
                        cmd.ExecuteNonQuery();
                        cmd11.ExecuteNonQuery();
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        DataTable dt11 = new DataTable();
                        SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
                        da11.Fill(dt11);
                        dataGridView2.DataSource = dt11;
                        con.Close();
                        dataGridView1.Columns[4].HeaderText = "اسم المصاغ";
                        dataGridView1.Columns[3].HeaderText = "عيار المصاغ";
                        dataGridView1.Columns[2].HeaderText = "الوزن الحالي";
                        dataGridView1.Columns[1].HeaderText = "عدد القطع الحالي";
                        dataGridView1.Columns[0].HeaderText = "الاجور الحالية";
                        dataGridView2.Columns[3].HeaderText = "اسم المصاغ";
                        dataGridView2.Columns[2].HeaderText = "الوزن الحالي";
                        dataGridView2.Columns[1].HeaderText = "عدد القطع الحالي";
                        dataGridView2.Columns[0].HeaderText = "الاجور الحالية";
                        con.Open();
                        SqlCommand cmd1 = con.CreateCommand();
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = "select gold14 ,gold18,gold21 from fonding";
                        cmd1.ExecuteNonQuery();
                        DataTable dt1 = new DataTable();
                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        da1.Fill(dt1);
                        dataGridView3.DataSource = dt1;
                        con.Close();

                        dataGridView3.Columns[2].HeaderText = "اجمالي ذهب 21(غرام)";
                        dataGridView3.Columns[1].HeaderText = "اجمالي ذهب 18(غرام)";
                        dataGridView3.Columns[0].HeaderText = "اجمالي ذهب 14(غرام)";
                        dataGridView3.Visible = true;
                        dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                        dataGridView1.EnableHeadersVisualStyles = false;
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                        dataGridView3.EnableHeadersVisualStyles = false;
                        foreach (DataGridViewColumn col in dataGridView3.Columns)
                        {
                            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (comboBox1.Text == "حساب صائغ")
                {

                    if (itemNameTxtbox.Text != "اختر اسم الصائغ ...")
                    {
                        try
                        {
                            printDocument1.DefaultPageSettings.Landscape = true;
                            print.Visible = true;
                            calculate.Visible = true;
                            string d1 = dateTimePicker1.Value.AddDays(-1).ToString("MM/dd/yyyy 23:59:59");
                            string d2 = dateTimePicker2.Value.ToString("MM/dd/yyyy 23:59:59");
                            con.Open();
                            SqlCommand cmd = con.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            paymentdb_Class cus = new paymentdb_Class();
                            cus.customerName1 = itemNameTxtbox.Text;
                            cmd.CommandText = "(select paymentNotice,paymentDateTime , payment14,payment14, payment18,payment18, payment21, payment21," +
                                " paymentCash,paymentCash, paymentTypeString , paymentId from payments where customerId=" +
                                Convert.ToString(cus.GetCustomerid()) + "and paymentDateTime between '" + d1 + "' and '" + d2 + "') UNION (select billDetail,billDateTime , billTotal14, billTotal14" +
                                " , billTotal18 , billTotal18 , billTotal21 ,billTotal21 , billTotalCash ,billTotalCash , billType , billId  from bills where " +
                                "customerId=" + Convert.ToString(cus.GetCustomerid()) + "and billDateTime between '" + d1 + "' and '" + d2 + "')";
                            cmd.ExecuteNonQuery();
                            DataTable dt = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dt);
                            for(int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (Convert.ToDecimal(dt.Rows[i][2].ToString()) == 0 && Convert.ToDecimal(dt.Rows[i][3].ToString()) == 0 && Convert.ToDecimal(dt.Rows[i][4].ToString()) == 0 && Convert.ToDecimal(dt.Rows[i][5].ToString()) == 0 && Convert.ToDecimal(dt.Rows[i][6].ToString()) == 0 && Convert.ToDecimal(dt.Rows[i][7].ToString()) == 0 && Convert.ToDecimal(dt.Rows[i][8].ToString()) == 0 && Convert.ToDecimal(dt.Rows[i][9].ToString()) == 0)
                                {
                                    dt.Rows[i].Delete();
                                }
                            }
                            dataGridView1.DataSource = dt;
                            dataGridView2.DataSource = dt;
                            con.Close();
                            dataGridView1.Columns[11].HeaderText = "id";
                            dataGridView1.Columns[10].HeaderText = "نوع الحركة";
                            dataGridView1.Columns[9].HeaderText = "لنا الاجور";
                            dataGridView1.Columns[8].HeaderText = "له الاجور";
                            dataGridView1.Columns[7].HeaderText = "لنا ذهب21";
                            dataGridView1.Columns[6].HeaderText = "له ذهب21";
                            dataGridView1.Columns[5].HeaderText = "لنا ذهب18";
                            dataGridView1.Columns[4].HeaderText = "له ذهب18";
                            dataGridView1.Columns[3].HeaderText = "لنا ذهب14";
                            dataGridView1.Columns[2].HeaderText = "له ذهب14";
                            dataGridView1.Columns[1].HeaderText = "تاريخ الحركة";
                            dataGridView1.Columns[0].HeaderText = "ملاحظات";
                            dataGridView2.Columns[11].HeaderText = "id";
                            dataGridView2.Columns[10].HeaderText = "نوع الحركة";
                            dataGridView2.Columns[9].HeaderText = "لنا الاجور";
                            dataGridView2.Columns[8].HeaderText = "له الاجور";
                            dataGridView2.Columns[7].HeaderText = "لنا ذهب21";
                            dataGridView2.Columns[6].HeaderText = "له ذهب21";
                            dataGridView2.Columns[5].HeaderText = "لنا ذهب18";
                            dataGridView2.Columns[4].HeaderText = "له ذهب18";
                            dataGridView2.Columns[3].HeaderText = "لنا ذهب14";
                            dataGridView2.Columns[2].HeaderText = "له ذهب14";
                            dataGridView2.Columns[1].HeaderText = "تاريخ الحركة";
                            dataGridView2.Columns[0].HeaderText = "ملاحظات";
                            for (int i = 0; i < dataGridView1.RowCount ; i++)
                            {
                                for (int j = 2; j < 10; j++)
                                {
                                    if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "0.00")
                                    {
                                        dataGridView1.Rows[i].Cells[j].Value = 0;
                                    }
                                    else
                                    {
                                        if (dataGridView1.Rows[i].Cells[10].Value.ToString() == "مصروف" || dataGridView1.Rows[i].Cells[10].Value.ToString() == "تسليم" || dataGridView1.Rows[i].Cells[10].Value.ToString() == "ازالة مصاغ" || dataGridView1.Rows[i].Cells[10].Value.ToString() == "سحب راس مال" || dataGridView1.Rows[i].Cells[10].Value.ToString() == "بيع")
                                        {
                                            dataGridView1.Rows[i].Cells[3].Value = 0;
                                            dataGridView1.Rows[i].Cells[5].Value = 0;
                                            dataGridView1.Rows[i].Cells[7].Value = 0;
                                            dataGridView1.Rows[i].Cells[9].Value = 0;
                                            dataGridView2.Rows[i].Cells[3].Value = 0;
                                            dataGridView2.Rows[i].Cells[5].Value = 0;
                                            dataGridView2.Rows[i].Cells[7].Value = 0;
                                            dataGridView2.Rows[i].Cells[9].Value = 0;
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[i].Cells[2].Value = 0;
                                            dataGridView1.Rows[i].Cells[4].Value = 0;
                                            dataGridView1.Rows[i].Cells[6].Value = 0;
                                            dataGridView1.Rows[i].Cells[8].Value = 0;
                                            dataGridView2.Rows[i].Cells[2].Value = 0;
                                            dataGridView2.Rows[i].Cells[4].Value = 0;
                                            dataGridView2.Rows[i].Cells[6].Value = 0;
                                            dataGridView2.Rows[i].Cells[8].Value = 0;

                                        }
                                        if (dataGridView1.Rows[i].Cells[10].Value.ToString() == "بيع" || dataGridView1.Rows[i].Cells[10].Value.ToString() == "شراء")
                                        {
                                            dataGridView1.Rows[i].Cells[8].Value = 0;
                                            dataGridView1.Rows[i].Cells[9].Value = 0;
                                            dataGridView2.Rows[i].Cells[8].Value = 0;
                                            dataGridView2.Rows[i].Cells[9].Value = 0;
                                        }
                                    }
                                }                                
                            }
                            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                            dataGridView1.EnableHeadersVisualStyles = false;
                            foreach (DataGridViewColumn col in dataGridView1.Columns)
                            {
                                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            }
                            con.Open();
                            SqlCommand cmd1 = con.CreateCommand();
                            cmd1.CommandType = CommandType.Text;
                            SqlCommand cmd11 = con.CreateCommand();
                            cmd11.CommandType = CommandType.Text;
                            paymentdb_Class cus1 = new paymentdb_Class();
                            cus.customerName1 = itemNameTxtbox.Text;
                            cmd1.CommandText = "select Total14,Total18,Total21,TotalCash,customerName from customers where customerId='" + cus.GetCustomerid().ToString() + "'";
                            cmd1.ExecuteNonQuery();
                            DataTable dt1 = new DataTable();
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            da1.Fill(dt1);
                            cmd11.CommandText = "select Total14,Total18,Total21,TotalCash from dailyCustomer where (customerId='" + cus.GetCustomerid().ToString() + "' and date= '" + dateTimePicker1.Value.ToString("MM/dd/yyyy 23:59:59") + "')";
                            cmd11.ExecuteNonQuery();
                            DataTable dt11 = new DataTable();
                            SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
                            da11.Fill(dt11);
                            con.Close();
                            DataTable dt2 = new DataTable();
                            dt2.Columns.Add(GlobalVar.gold14Label, typeof(string));
                            dt2.Columns.Add(GlobalVar.gold18Label, typeof(string));
                            dt2.Columns.Add(GlobalVar.gold21Label, typeof(string));
                            dt2.Columns.Add(GlobalVar.cashLabel, typeof(string));
                            dt2.Columns.Add("بداية ذهب14", typeof(string));
                            dt2.Columns.Add("بداية ذهب18", typeof(string));
                            dt2.Columns.Add("بداية ذهب21", typeof(string));
                            dt2.Columns.Add("بداية اجور", typeof(string));
                            dt2.Columns.Add("اسم الصائغ", typeof(string));
                            if (dt11.Rows.Count > 0)
                                dt2.Rows.Add(dt1.Rows[0][0].ToString(), dt1.Rows[0][1].ToString(), dt1.Rows[0][2].ToString(), dt1.Rows[0][3].ToString(), dt11.Rows[0][0].ToString(), dt11.Rows[0][1].ToString(), dt11.Rows[0][2].ToString(), dt11.Rows[0][3].ToString(), dt1.Rows[0][4].ToString());
                            else
                                dt2.Rows.Add(Convert.ToDecimal(dt1.Rows[0][0].ToString()), Convert.ToDecimal(dt1.Rows[0][1]), Convert.ToDecimal(dt1.Rows[0][2].ToString()), Convert.ToDecimal(dt1.Rows[0][3].ToString()), 0, 0, 0, 0, dt1.Rows[0][4].ToString());
                            if (Convert.ToDecimal(dt2.Rows[0][0]) == 0)
                                dt2.Rows[0][0] = "رصيد ذهب14";
                            if (Convert.ToDecimal(dt2.Rows[0][1]) == 0)
                                dt2.Rows[0][1] = "رصيد ذهب18";
                            if (Convert.ToDecimal(dt2.Rows[0][2]) == 0)
                                dt2.Rows[0][2] = "رصيد ذهب21";
                            if (Convert.ToDecimal(dt2.Rows[0][3]) == 0)
                                dt2.Rows[0][3] = "رصيد اجور";
                            if (Convert.ToDecimal(dt2.Rows[0][4]) == 0)
                                dt2.Rows[0][4] = "رصيد ذهب14";
                            if (Convert.ToDecimal(dt2.Rows[0][5]) == 0)
                                dt2.Rows[0][5] = "رصيد ذهب18";
                            if (Convert.ToDecimal(dt2.Rows[0][6]) == 0)
                                dt2.Rows[0][6] = "رصيد ذهب21";
                            if (Convert.ToDecimal(dt2.Rows[0][7]) == 0)
                                dt2.Rows[0][7] = "رصيد اجور";
                            dataGridView3.DataSource = dt2;
                            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                            dataGridView3.EnableHeadersVisualStyles = false;
                            foreach (DataGridViewColumn col in dataGridView3.Columns)
                            {
                                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            }
                            dataGridView3.Visible = true;
                            cus.customerName1 = itemNameTxtbox.Text;
                        }
                        catch (Exception ex)
                        {
                            con.Close();
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else if (comboBox1.Text == "حسابات الصاغة")
                {
                    try
                    {
                        printDocument1.DefaultPageSettings.Landscape = false;
                        print.Visible = true;
                        dataGridView3.Visible = true;
                        calculate.Visible = true;
                        if (dateTimePicker2.Value.Date == DateTime.Now.Date)
                        {
                            con.Open();
                            SqlCommand cmd = con.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            paymentdb_Class cus = new paymentdb_Class();
                            cus.customerName1 = itemNameTxtbox.Text;
                            cmd.CommandText = "select Total14,Total21,TotalCash,customerName from customers where userId = " + GlobalVar.id.ToString();

                            cmd.ExecuteNonQuery();
                            DataTable dt = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dt);
                            con.Close();
                            dt.Columns.Add("آخر عملية", typeof(string));
                            foreach (DataRow row in dt.Rows)
                            {
                                row["آخر عملية"] = "0";
                            }
                            dt.Columns["آخر عملية"].SetOrdinal(0);
                            string dat = "";
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if (dt.Rows[i][4].ToString() == "راس مال" || dt.Rows[i][4].ToString() == "مصروف")
                                    dt.Rows[i].Delete();
                                else if (Convert.ToDecimal(dt.Rows[i][3].ToString()) == 0 && Convert.ToDecimal(dt.Rows[i][1].ToString()) == 0 && Convert.ToDecimal(dt.Rows[i][2].ToString()) == 0)
                                    dt.Rows[i].Delete();
                            }
                            dataGridView1.DataSource = dt;
                            dataGridView2.DataSource = dt;

                            dataGridView1.Columns[3].HeaderText = "الاجور";
                            dataGridView1.Columns[2].HeaderText = GlobalVar.gold21Label;
                            dataGridView1.Columns[1].HeaderText = GlobalVar.gold14Label;
                            dataGridView1.Columns[4].HeaderText = "اسم الصائغ";
                            dataGridView2.Columns[3].HeaderText = "الاجور";
                            dataGridView2.Columns[2].HeaderText = GlobalVar.gold21Label;
                            dataGridView2.Columns[1].HeaderText = GlobalVar.gold14Label;
                            dataGridView2.Columns[4].HeaderText = "اسم الصائغ";
                            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                            dataGridView1.EnableHeadersVisualStyles = false;
                            foreach (DataGridViewColumn col in dataGridView1.Columns)
                            {
                                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            }
                            this.Update();
                            System.Threading.Thread.Sleep(10);
                            for (int i = 0; i < dataGridView1.Rows.Count; i++)
                            {
                                cus.customerName1 = dataGridView1.Rows[i].Cells[4].Value.ToString();
                                dat = cus.GetCustomerid().ToString();
                                con.Open();
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = "(select paymentDateTime from payments where customerId = " + dat +
                                    ") union (select billDateTime from bills where customerId = " + dat +
                                    ") order by paymentDateTime desc ";
                                cmd.ExecuteNonQuery();
                                DataTable dt11 = new DataTable();
                                SqlDataAdapter da11 = new SqlDataAdapter(cmd);
                                da11.Fill(dt11);
                                con.Close();
                                if (dt11.Rows.Count > 0)
                                {
                                    dataGridView2.Rows[i].Cells[0].Value = dt11.Rows[0][0];//.ToString().Substring(0, 10);
                                    dataGridView1.Rows[i].Cells[0].Value = dt11.Rows[0][0];//.ToString().Substring(0, 10);
                                }//dataGridView5.DataSource = null;
                                dt11.Clear();
                            }
                            dataGridView1.Sort(dataGridView1.Columns[4], ListSortDirection.Ascending);
                        }
                        else
                        {
                            try {
                                DataTable dt = new DataTable();

                                dt.Clear();

                                dataGridView1.DataSource = null;
                                dataGridView2.DataSource = null;
                                con.Open();
                                SqlCommand cmd = con.CreateCommand();
                                cmd.CommandType = CommandType.Text;
                                paymentdb_Class cus = new paymentdb_Class();
                                cus.customerName1 = itemNameTxtbox.Text;
                                cmd.CommandText = "select dailyCustomer.Total14,dailyCustomer.Total21,dailyCustomer.TotalCash,customers.customerName from dailyCustomer inner join customers on dailyCustomer.customerId=customers.customerId where (dailyCustomer.userId = " + GlobalVar.id.ToString() + " and dailyCustomer.date = '" + dateTimePicker2.Value.Date.ToString("MM/dd/yyyy").Substring(0,10)+"')";
                                cmd.ExecuteNonQuery();
                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                da.Fill(dt);
                                con.Close();
                                dt.Columns.Add("آخر عملية", typeof(string));
                                foreach (DataRow row in dt.Rows)
                                {
                                    row["آخر عملية"] = "0";
                                }
                                dt.Columns["آخر عملية"].SetOrdinal(0);
                                string dat = "";
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (dt.Rows[i][4].ToString() == "راس مال" || dt.Rows[i][4].ToString() == "مصروف")
                                        dt.Rows[i].Delete();
                                    else if (Convert.ToDecimal(dt.Rows[i][3].ToString()) == 0 && Convert.ToDecimal(dt.Rows[i][1].ToString()) == 0 && Convert.ToDecimal(dt.Rows[i][2].ToString()) == 0)
                                        dt.Rows[i].Delete();
                                }
                                dataGridView1.DataSource = dt;
                                dataGridView2.DataSource = dt;

                                dataGridView1.Columns[3].HeaderText = "الاجور";
                                dataGridView1.Columns[2].HeaderText = GlobalVar.gold21Label;
                                dataGridView1.Columns[1].HeaderText = GlobalVar.gold14Label;
                                dataGridView1.Columns[4].HeaderText = "اسم الصائغ";
                                dataGridView2.Columns[3].HeaderText = "الاجور";
                                dataGridView2.Columns[2].HeaderText = GlobalVar.gold21Label;
                                dataGridView2.Columns[1].HeaderText = GlobalVar.gold14Label;
                                dataGridView2.Columns[4].HeaderText = "اسم الصائغ";
                                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                                dataGridView1.EnableHeadersVisualStyles = false;
                                foreach (DataGridViewColumn col in dataGridView1.Columns)
                                {
                                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                }
                                this.Update();
                                System.Threading.Thread.Sleep(10);
                                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                                {
                                    cus.customerName1 = dataGridView1.Rows[i].Cells[4].Value.ToString();
                                    dat = cus.GetCustomerid().ToString();
                                    con.Open();
                                    cmd.CommandType = CommandType.Text;
                                    cmd.CommandText = "(select paymentDateTime from payments where customerId = " + dat +
                                        ") union (select billDateTime from bills where customerId = " + dat +
                                        ") order by paymentDateTime desc ";
                                    cmd.ExecuteNonQuery();
                                    DataTable dt11 = new DataTable();
                                    SqlDataAdapter da11 = new SqlDataAdapter(cmd);
                                    da11.Fill(dt11);
                                    con.Close();
                                    if (dt11.Rows.Count > 0)
                                    {
                                        dataGridView2.Rows[i].Cells[0].Value = dt11.Rows[0][0];//.ToString().Substring(0, 10);
                                        dataGridView1.Rows[i].Cells[0].Value = dt11.Rows[0][0];//.ToString().Substring(0, 10);
                                    }//dataGridView5.DataSource = null;
                                    dt11.Clear();
                                }
                                dataGridView1.Sort(dataGridView1.Columns[4], ListSortDirection.Ascending);
                            }
                            catch(Exception mm) { MessageBox.Show(mm.Message); }
                            }
                    }
                    catch
                    {
                        con.Close();
                    }
                    calculate_Click(sender, e);
                }
                else if (comboBox1.Text == "القيد اليومي")
                {
                    try
                    {
                        printDocument1.DefaultPageSettings.Landscape = true;
                        print.Visible = true;
                        string d1 = dateTimePicker1.Value.AddDays(-1).ToString("MM/dd/yyyy 23:59:59");
                        string d2 = dateTimePicker2.Value.ToString("MM/dd/yyyy 23:59:59");
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        //cmd.CommandText = "(select billTotal14,billTotal18,billTotal21,billTotalCash,billTotal14" +
                        //    ",billTotal18,billTotal21,billTotalCash,customers.customerName,billType,billDateTime,billId from" +
                        //    " bills inner join customers on bills.customerId=customers.customerId where billDateTime" +
                        //    " between '" + d1 + "' and '" + d2 + "') union ( select payment14,payment18,payment21," +
                        //    "paymentCash,payment14,payment18,payment21,paymentCash,customers.customerName,paymentTypeString" +
                        //    ",paymentDateTime,paymentId from payments inner join customers on payments.customerId=customers.customerId" +
                        //    " where paymentDateTime between '" + d1 + "' and '" + d2 + "')";
                        cmd.CommandText = "(select billTotal14,billTotal14,billTotal21,billTotal21" +
                           ",billTotalCash,billTotalCash,customers.customerName,billType,billDateTime,billId from" +
                           " bills inner join customers on bills.customerId=customers.customerId where bills.userId = " + GlobalVar.id.ToString() + "and billDateTime" +
                           " between '" + d1 + "' and '" + d2 + "') union ( select realPayment14,realPayment14," +
                           "realPayment21,realPayment21,realPaymentCash,realPaymentCash,customers.customerName,paymentTypeString" +
                           ",paymentDateTime,paymentId from payments inner join customers on payments.customerId=customers.customerId" +
                           " where payments.userId = " + GlobalVar.id.ToString() + "and paymentDateTime between '" + d1 + "' and '" + d2 + "')";
                        cmd.ExecuteNonQuery();
                        dataGridView1.DataSource = null;
                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                        con.Close();
                        //dataGridView1.Columns[10].HeaderText = "تاريخ ووقت الحركة";
                        //dataGridView1.Columns[0].HeaderText = "له ذهب14";
                        //dataGridView1.Columns[1].HeaderText = "له ذهب18";
                        //dataGridView1.Columns[2].HeaderText = "له ذهب21";
                        //dataGridView1.Columns[3].HeaderText = "له اجور";
                        //dataGridView1.Columns[4].HeaderText = "لنا ذهب14";
                        //dataGridView1.Columns[5].HeaderText = "لنا ذهب18";
                        //dataGridView1.Columns[6].HeaderText = "لنا ذهب21";
                        //dataGridView1.Columns[7].HeaderText = "لنا اجور";
                        //dataGridView1.Columns[8].HeaderText = "اسم الصائغ";
                        //dataGridView1.Columns[9].HeaderText = "نوع الحركة";
                        //dataGridView1.Columns[11].HeaderText = "Id";
                        dataGridView1.Columns[0].HeaderText = "له ذهب14";
                        dataGridView1.Columns[1].HeaderText = "لنا ذهب14";
                        dataGridView1.Columns[2].HeaderText = "له ذهب21";
                        dataGridView1.Columns[3].HeaderText = "لنا ذهب21";
                        dataGridView1.Columns[4].HeaderText = "له اجور";
                        dataGridView1.Columns[5].HeaderText = "لنا اجور";
                        dataGridView1.Columns[6].HeaderText = "اسم الصائغ";
                        dataGridView1.Columns[7].HeaderText = "نوع الحركة";
                        dataGridView1.Columns[8].HeaderText = "تاريخ الحركة";
                        dataGridView1.Columns[9].HeaderText = "Id";
                        dataGridView1.Columns[9].Width = 0;
                        this.dataGridView1.Sort(this.dataGridView1.Columns[8], ListSortDirection.Ascending);
                        if (dataGridView1.Rows.Count > 1)
                        {
                            for (int i = 0; i < dataGridView1.Rows.Count; ++i)

                            {
                                dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.OrangeRed;
                                dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.MediumSeaGreen;
                                dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.OrangeRed;
                                dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.MediumSeaGreen;
                                dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.OrangeRed;
                                dataGridView1.Rows[i].Cells[5].Style.BackColor = Color.MediumSeaGreen;
                                dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.White;
                                dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.White;
                                dataGridView1.Rows[i].Cells[2].Style.ForeColor = Color.White;
                                dataGridView1.Rows[i].Cells[3].Style.ForeColor = Color.White;
                                dataGridView1.Rows[i].Cells[4].Style.ForeColor = Color.White;
                                dataGridView1.Rows[i].Cells[5].Style.ForeColor = Color.White;
                                //dataGridView1.Rows[i].Cells[1].Style.BackColor = Color.OrangeRed;
                                //dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.OrangeRed;
                                //dataGridView1.Rows[i].Cells[3].Style.BackColor = Color.OrangeRed;
                                //dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.OrangeRed;
                                //dataGridView1.Rows[i].Cells[6].Style.BackColor = Color.MediumSeaGreen;
                                //dataGridView1.Rows[i].Cells[7].Style.BackColor = Color.MediumSeaGreen;
                                //dataGridView1.Rows[i].Cells[4].Style.BackColor = Color.MediumSeaGreen;
                                //dataGridView1.Rows[i].Cells[1].Style.ForeColor = Color.White;
                                //dataGridView1.Rows[i].Cells[2].Style.ForeColor = Color.White;
                                //dataGridView1.Rows[i].Cells[3].Style.ForeColor = Color.White;
                                //dataGridView1.Rows[i].Cells[4].Style.ForeColor = Color.White;
                                //dataGridView1.Rows[i].Cells[5].Style.ForeColor = Color.White;
                                //dataGridView1.Rows[i].Cells[6].Style.ForeColor = Color.White;
                                //dataGridView1.Rows[i].Cells[7].Style.ForeColor = Color.White;
                                //dataGridView1.Rows[i].Cells[0].Style.ForeColor = Color.White;
                                for (int j = 0; j < 6; j++)
                                {
                                    if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "0.00")
                                    {
                                        dataGridView1.Rows[i].Cells[j].Value = 0;
                                    }

                                    else
                                    {
                                        if (dataGridView1.Rows[i].Cells[7].Value.ToString() == "مصروف" || dataGridView1.Rows[i].Cells[7].Value.ToString() == "تسليم" || dataGridView1.Rows[i].Cells[7].Value.ToString() == "ازالة مصاغ" || dataGridView1.Rows[i].Cells[7].Value.ToString() == "سحب راس مال" || dataGridView1.Rows[i].Cells[7].Value.ToString() == "بيع")
                                        {
                                            dataGridView1.Rows[i].Cells[1].Value = 0;
                                            dataGridView1.Rows[i].Cells[3].Value = 0;
                                            dataGridView1.Rows[i].Cells[5].Value = 0;
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[i].Cells[0].Value = 0;
                                            dataGridView1.Rows[i].Cells[2].Value = 0;
                                            dataGridView1.Rows[i].Cells[4].Value = 0;
                                        }
                                        if (dataGridView1.Rows[i].Cells[7].Value.ToString() == "بيع" || dataGridView1.Rows[i].Cells[7].Value.ToString() == "شراء")
                                        {
                                            dataGridView1.Rows[i].Cells[4].Value = 0;
                                            dataGridView1.Rows[i].Cells[5].Value = 0;
                                        }
                                    }
                                }

                            }
                        }

                        else
                        {
                            L1.Visible = false;
                            L2.Visible = false;
                            L3.Visible = false;
                            L4.Visible = false;
                            myDiscount.Visible = false;
                            forDiscount.Visible = false;

                            MessageBox.Show("يرجى اختيار نوع البيانات المطلوبة");
                        }
                        dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                        dataGridView1.EnableHeadersVisualStyles = false;
                        foreach (DataGridViewColumn col in dataGridView1.Columns)
                        {
                            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                    }
                    catch (Exception ex)
                    {
                        con.Close();
                        MessageBox.Show(ex.Message);
                    }
                }
                else if (comboBox1.Text == "كشف حساب تفصيلي")
                {
                    if (itemNameTxtbox.Text != "اختر اسم الصائغ ..." && itemNameTxtbox.Text != "")
                    {
                        try
                        {
                            print.Visible = true;
                            printDocument1.DefaultPageSettings.Landscape = true;
                            string d1 = dateTimePicker1.Value.AddDays(-1).ToString("MM/dd/yyyy 23:59:59");
                            string d2 = dateTimePicker2.Value.ToString("MM/dd/yyyy 23:59:59");
                            con.Open();
                            SqlCommand cmd = con.CreateCommand();
                            SqlCommand cmd2 = con.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd2.CommandType = CommandType.Text;
                            paymentdb_Class cus = new paymentdb_Class();
                            cus.customerName1 = itemNameTxtbox.Text;
                            cmd.CommandText = "(select paymentNotice , paymentDateTime , payment14, payment18, payment21," +
                                " paymentCash, paymentTypeString , paymentId from payments where customerId=" +
                                Convert.ToString(cus.GetCustomerid()) + "and paymentDateTime between '" + d1 + "' and '" + d2 + "') UNION (select billDetail , billDateTime , billTotal14" +
                                " , billTotal18 , billTotal21 , billTotalCash , billType , billId  from bills where " +
                                "customerId=" + Convert.ToString(cus.GetCustomerid()) + "and billDateTime between '" + d1 + "' and '" + d2 + "')";
                            cmd2.CommandText = "SELECT  billDetails.itemFees ,billDetails.itemCount,billDetails.itemWeight,itemTotalFees,items.itemName, billId FROM billDetails inner join items on billDetails.itemId = items.itemId where billId in" +
                                " (SELECT billId FROM bills where customerId = " + Convert.ToString(cus.GetCustomerid()) + "and billDateTime between '" + d1 + "' and '" + d2 + "')";
                            cmd.ExecuteNonQuery();
                            cmd2.ExecuteNonQuery();
                            DataTable dt = new DataTable();
                            DataTable dt2 = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                            da.Fill(dt);                            
                            da2.Fill(dt2);                       
                            con.Close();
                            dt2.Columns.Add("نوع الفاتورة", typeof(string));
                            dt2.Columns.Add("التاريخ", typeof(string));
                            dt2.Columns["التاريخ"].SetOrdinal(0);
                            dt2.Columns["نوع الفاتورة"].SetOrdinal(0);
                            foreach (DataRow row in dt2.Rows)
                            {
                                row["نوع الفاتورة"] = "ا";
                                row["التاريخ"] = "1/1/2018";
                            }                           
                            for (int i = 0; i < dt2.Rows.Count; i++)
                            {
                                con.Open();
                                cmd.CommandType = CommandType.Text;
                                //MessageBox.Show(dt2.Rows[i][7].ToString());
                                cmd.CommandText = "(select billType,billDateTime from bills where billId = " + dt2.Rows[i][7].ToString() + ")";
                                cmd.ExecuteNonQuery();
                                DataTable dt111 = new DataTable();
                                SqlDataAdapter da111 = new SqlDataAdapter(cmd);
                                da111.Fill(dt111);
                                con.Close();
                                //MessageBox.Show(dt2.Rows[i][0].ToString() + Environment.NewLine + dt2.Rows[i][1].ToString() + Environment.NewLine + dt2.Rows[i][2].ToString() + Environment.NewLine + dt2.Rows[i][3].ToString() + Environment.NewLine + dt2.Rows[i][4].ToString() + Environment.NewLine + dt2.Rows[i][5].ToString() + Environment.NewLine + dt2.Rows[i][6].ToString() + Environment.NewLine + dt2.Rows[i][7].ToString());
                                if (dt111.Rows.Count > 0)
                                {
                                    dt2.Rows[i][0] = dt111.Rows[0][0];//.ToString().Substring(0, 10);
                                    dt2.Rows[i][1] = dt111.Rows[0][1].ToString().Substring(0, 10);
                                }//dataGridView5.DataSource = null;
                                dt111.Clear();
                            }
                            dataGridView1.DataSource = dt;
                            dataGridView1.Columns[7].HeaderText = "id";
                            dataGridView1.Columns[6].HeaderText = "نوع الحركة";
                            dataGridView1.Columns[5].HeaderText = "الاجور";
                            dataGridView1.Columns[4].HeaderText = GlobalVar.gold21Label;
                            dataGridView1.Columns[3].HeaderText = GlobalVar.gold18Label;
                            dataGridView1.Columns[2].HeaderText = GlobalVar.gold14Label;
                            dataGridView1.Columns[1].HeaderText = "تاريخ الحركة";
                            dataGridView1.Columns[0].HeaderText = "ملاحظات";

                            dataGridView2.DataSource = dt2;
                            dataGridView2.Columns[7].HeaderText = "الفاتورة";
                            dataGridView2.Columns[6].HeaderText = "المصاغ";
                            dataGridView2.Columns[5].HeaderText = "الاجور الكلية";
                            dataGridView2.Columns[4].HeaderText = "الوزن";
                            dataGridView2.Columns[3].HeaderText = "العدد";
                            dataGridView2.Columns[2].HeaderText = "اجور الصياغة";
                            dataGridView2.Columns[1].HeaderText = "التاريخ";
                            dataGridView2.Columns[0].HeaderText = "نوع الفاتورة";




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
                            con.Open();
                            SqlCommand cmd1 = con.CreateCommand();
                            cmd1.CommandType = CommandType.Text;
                            SqlCommand cmd11 = con.CreateCommand();
                            cmd11.CommandType = CommandType.Text;
                            paymentdb_Class cus1 = new paymentdb_Class();
                            cus.customerName1 = itemNameTxtbox.Text;
                            cmd1.CommandText = "select Total14,Total18,Total21,TotalCash,customerName from customers where customerId='" + cus.GetCustomerid().ToString() + "'";
                            cmd1.ExecuteNonQuery();
                            DataTable dt1 = new DataTable();
                            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                            da1.Fill(dt1);
                            cus.customerName1 = itemNameTxtbox.Text;
                            cmd11.CommandText = "select Total14,Total18,Total21,TotalCash from dailyCustomer where (customerId='" + cus.GetCustomerid().ToString() + "' and date= '" + dateTimePicker1.Value.ToString("MM/dd/yyyy 23:59:59") + "')";
                            cmd11.ExecuteNonQuery();
                            DataTable dt11 = new DataTable();
                            SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
                            da11.Fill(dt11);
                            con.Close();
                            DataTable dt22 = new DataTable();
                            dt22.Columns.Add(GlobalVar.gold14Label, typeof(string));
                            dt22.Columns.Add(GlobalVar.gold18Label, typeof(string));
                            dt22.Columns.Add(GlobalVar.gold21Label, typeof(string));
                            dt22.Columns.Add(GlobalVar.cashLabel, typeof(string));
                            dt22.Columns.Add("بداية ذهب14", typeof(string));
                            dt22.Columns.Add("بداية ذهب18", typeof(string));
                            dt22.Columns.Add("بداية ذهب21", typeof(string));
                            dt22.Columns.Add("بداية اجور", typeof(string));
                            dt22.Columns.Add("اسم الصائغ", typeof(string));
                            if (dt11.Rows.Count > 0)
                                dt22.Rows.Add(dt1.Rows[0][0].ToString(), dt1.Rows[0][1].ToString(), dt1.Rows[0][2].ToString(), dt1.Rows[0][3].ToString(), dt11.Rows[0][0].ToString(), dt11.Rows[0][1].ToString(), dt11.Rows[0][2].ToString(), dt11.Rows[0][3].ToString(), dt1.Rows[0][4].ToString());
                            else
                                dt22.Rows.Add(Convert.ToDecimal(dt1.Rows[0][0].ToString()), Convert.ToDecimal(dt1.Rows[0][1]), Convert.ToDecimal(dt1.Rows[0][2].ToString()), Convert.ToDecimal(dt1.Rows[0][3].ToString()), 0, 0, 0, 0, dt1.Rows[0][4].ToString());

                            if (Convert.ToDecimal(dt22.Rows[0][0]) == 0)
                                dt22.Rows[0][0] = "رصيد ذهب14";
                            if (Convert.ToDecimal(dt22.Rows[0][1]) == 0)
                                dt22.Rows[0][1] = "رصيد ذهب18";
                            if (Convert.ToDecimal(dt22.Rows[0][2]) == 0)
                                dt22.Rows[0][2] = "رصيد ذهب21";
                            if (Convert.ToDecimal(dt22.Rows[0][3]) == 0)
                                dt22.Rows[0][3] = "رصيد اجور";
                            if (Convert.ToDecimal(dt22.Rows[0][4]) == 0)
                                dt22.Rows[0][4] = "رصيد ذهب14";
                            if (Convert.ToDecimal(dt22.Rows[0][5]) == 0)
                                dt22.Rows[0][5] = "رصيد ذهب18";
                            if (Convert.ToDecimal(dt22.Rows[0][6]) == 0)
                                dt22.Rows[0][6] = "رصيد ذهب21";
                            if (Convert.ToDecimal(dt22.Rows[0][7]) == 0)
                                dt22.Rows[0][7] = "رصيد اجور";

                            dataGridView3.DataSource = dt22;
                            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                            dataGridView3.EnableHeadersVisualStyles = false;
                            foreach (DataGridViewColumn col in dataGridView3.Columns)
                            {
                                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            }
                            dataGridView3.Visible = true;
                            cus.customerName1 = itemNameTxtbox.Text;
                        }
                        catch (Exception ex)
                        {
                            con.Close();
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
                else if (comboBox1.Text == "تنفيذ حسومات"||comboBox1.Text=="تنفيذ تحويلات")
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    paymentdb_Class cus = new paymentdb_Class();
                    cus.customerName1 = itemNameTxtbox.Text;
                    cmd.CommandText = "select total14,Total21,TotalCash,customerName from customers where userId = " + GlobalVar.id.ToString();
                    cmd.ExecuteNonQuery();
                    DataTable d4 = new DataTable();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    con.Close();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToDecimal(dt.Rows[i][0].ToString()) == 0 && Convert.ToDecimal(dt.Rows[i][1].ToString()) == 0 && Convert.ToDecimal(dt.Rows[i][2].ToString()) == 0)
                            dt.Rows[i].Delete();
                    }
                    dataGridView1.DataSource = dt;
                    dataGridView1.Columns[2].HeaderText = "الاجور";
                    dataGridView1.Columns[1].HeaderText = GlobalVar.gold21Label;
                    dataGridView1.Columns[0].HeaderText = GlobalVar.gold14Label;
                    dataGridView1.Columns[3].HeaderText = "اسم الصائغ";
                    if (comboBox1.Text == "تنفيذ حسومات")
                    {
                        d4.Columns.Add("اسم الصائغ", typeof(string));
                        d4.Columns.Add("حسم اجور لنا", typeof(decimal));
                        d4.Columns.Add("حسم اجور له", typeof(decimal));
                        d4.Columns.Add("حسم ذهب21 لنا", typeof(decimal));
                        d4.Columns.Add("حسم ذهب21 له", typeof(decimal));
                        d4.Columns.Add("حسم ذهب14 لنا", typeof(decimal));
                        d4.Columns.Add("حسم ذهب14 له", typeof(decimal));
                        dataGridView4.DataSource = d4;
                    }
                    else if (comboBox1.Text=="تنفيذ تحويلات")
                    {
                        d4.Columns.Add("اسم الصائغ", typeof(string));
                        d4.Columns.Add("الكمية قبل التحويل", typeof(decimal));
                        d4.Columns.Add("بيان التحويل", typeof(string));
                        d4.Columns.Add("الكمية بعد التحويل", typeof(decimal));
                        dataGridView4.DataSource = d4;
                    }
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                    dataGridView1.EnableHeadersVisualStyles = false;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    dataGridView4.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                    dataGridView4.EnableHeadersVisualStyles = false;
                    foreach (DataGridViewColumn col in dataGridView4.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            decimal g14 = 0;
            decimal g18 = 0;
            decimal g21 = 0;
            decimal Cash = 0;
            decimal f14 = 0;
            decimal m14 = 0;
            decimal m18 = 0;
            decimal f21 = 0;
            decimal m21 = 0;
            decimal fCash = 0;
            decimal mCash = 0;
            decimal SellBillCash = 0;
            decimal BuyBillCash = 0;
            decimal SellBillWeight = 0;
            decimal BuyBillWeight = 0;

            DataTable dd = new DataTable();
            DataTable dd1 = new DataTable();

            if (comboBox1.Text == "حسابات الصاغة")
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value) > 0)
                        f14 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value);

                    else if (Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value) < 0)
                        m14 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value);

                    if (Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value) > 0)
                        f21 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value);

                    else if (Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value) < 0)
                        m21 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value);

                    if (Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value) > 0)
                        fCash += Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value);

                    else if (Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value) < 0)
                        mCash += Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value);
                }
                m14 = m14 * -1;
                m21 = m21 * -1;
                mCash = mCash * -1;
                dd.Columns.Add("ذهب14 له", typeof(decimal));
                dd.Columns.Add("ذهب14 لنا", typeof(decimal));
                dd.Columns.Add("ذهب21 له", typeof(decimal));
                dd.Columns.Add("ذهب21 لنا", typeof(decimal));
                dd.Columns.Add("اجور له", typeof(decimal));
                dd.Columns.Add("اجور لنا", typeof(decimal));
                dd.Rows.Add(mCash, fCash,m14, f14, m21, f21);
                dataGridView3.DataSource = dd;
                foreach (DataGridViewColumn col in dataGridView3.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else if (comboBox1.Text == "الفواتير")
            {
                for (int i=0;i<dataGridView1.RowCount;i++)
                {
                    if (dataGridView1.Rows[i].Cells[4].Value.ToString().Trim() == "بيع")
                    {
                        SellBillCash += Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value);
                        SellBillWeight += Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value);
                    }
                    else
                    {
                        BuyBillCash += Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value);
                        BuyBillWeight += Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value);
                    }
                }

                dd.Columns.Add("اجمالي الوزن", typeof(decimal));
                dd.Columns.Add("اجمالي الاجور", typeof(decimal));
                dd.Columns.Add("اجمالي وزن شراء", typeof(decimal));
                dd.Columns.Add("اجمالي وزن بيع", typeof(decimal));
                dd.Columns.Add("اجمالي اجور شراء", typeof(decimal));
                dd.Columns.Add("اجمالي اجور بيع", typeof(decimal));
                dd.Rows.Add(SellBillWeight-BuyBillWeight, SellBillCash-BuyBillCash, BuyBillWeight, SellBillWeight, BuyBillCash,SellBillCash);
                dataGridView3.DataSource = dd;
                foreach (DataGridViewColumn col in dataGridView3.Columns)
                {
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
            else if (comboBox1.Text == "الدفعات")
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells[6].Value.ToString().Trim() == "استلام")
                    {
                        Cash += Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);
                        g21 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value);
                        g18 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value);
                        g14 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value);
                    }
                    else
                    {
                        Cash -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);
                        g21 -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value);
                        g18 -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value);
                        g14 -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value);
                    }
                }
                dd.Columns.Add("اجمالي ذهب14", typeof(decimal));
                dd.Columns.Add("اجمالي ذهب18", typeof(decimal));
                dd.Columns.Add("اجمالي ذهب21", typeof(decimal));
                dd.Columns.Add("اجمالي اجور", typeof(decimal));
                dd.Rows.Add(g14, g18, g21, Cash);
                dataGridView3.DataSource = dd;
            }
            else if (comboBox1.Text == "تنفيذ حسومات")
            {
                if (dataGridView4.RowCount>1)
                {
                    for (int i=0;i<dataGridView4.RowCount-1;i++)
                    {
                        if (dataGridView4.Rows[i].Cells[0].Value.ToString().Trim() != "")
                        {
                            for (int j=0;j<dataGridView1.RowCount;j++)
                            {
                                if (dataGridView1.Rows[j].Cells[3].Value.ToString()==dataGridView4.Rows[i].Cells[0].Value.ToString())
                                {
                                    index = j;
                                }
                            }
                            if (Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString()) != 0)
                            {                               
                                    cus.customerName1 = dataGridView4.Rows[i].Cells[0].Value.ToString();
                                    cus.customerId1 = cus.GetCustomerid();
                                    cus.paymentTypeInt1 = 1;
                                    cus.paymentCash1 = Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString());
                                    cus.payment211 = 0;
                                    cus.payment181 = 0;
                                    cus.payment141 = 0;
                                    if (cus.addPayment1() == " تم اضافة الدفعة في حساب الصائغ ") 
                                    {
                                        itm.billType1 = "حسم اجور لنا بعد الدفع";
                                        itm.customerName1 = dataGridView4.Rows[i].Cells[0].Value.ToString();
                                        itm.customerId1 = itm.GetCustomerid();
                                        itm.userId1 = GlobalVar.id;
                                        itm.dateT1 = dateTimePicker1.Value.Date;
                                        itm.discountAmount = Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString());
                                        itm.billId1 = 1;
                                        itm.discountType1 = "+1";
                                        itm.insertDiscount();
                                    }
                            }
                            if (Convert.ToDecimal(dataGridView4.Rows[i].Cells[3].Value.ToString()) != 0)
                            {
                              
                                    cus.customerName1 = dataGridView4.Rows[i].Cells[0].Value.ToString();
                                    cus.customerId1 = cus.GetCustomerid();
                                    cus.paymentTypeInt1 = 1;
                                    cus.paymentCash1 = 0;
                                    cus.payment211 = Convert.ToDecimal(dataGridView4.Rows[i].Cells[3].Value.ToString());
                                    cus.payment181 = 0;
                                    cus.payment141 = 0;
                                    if (cus.addPayment1() == " تم اضافة الدفعة في حساب الصائغ ")
                                    {
                                        itm.billType1 = "حسم ذهب21 لنا بعد الدفع";
                                        itm.customerName1 = dataGridView4.Rows[i].Cells[0].Value.ToString();
                                        itm.customerId1 = itm.GetCustomerid();
                                        itm.userId1 = GlobalVar.id;
                                        itm.dateT1 = dateTimePicker1.Value.Date;
                                        itm.discountAmount = Convert.ToDecimal(dataGridView4.Rows[i].Cells[3].Value.ToString());
                                        itm.billId1 = 1;
                                        itm.discountType1 = "+1";
                                        itm.insertDiscount();
                                    }
                            }
                            if (Convert.ToDecimal(dataGridView4.Rows[i].Cells[5].Value.ToString()) != 0)
                            {                             
                                    cus.customerName1 = dataGridView4.Rows[i].Cells[0].Value.ToString();
                                    cus.customerId1 = cus.GetCustomerid();
                                    cus.paymentTypeInt1 = 1;
                                    cus.paymentCash1 = 0;
                                    cus.payment211 = 0;
                                    cus.payment181 = 0;
                                    cus.payment141 = Convert.ToDecimal(dataGridView4.Rows[i].Cells[5].Value.ToString());
                                    if (cus.addPayment1() == " تم اضافة الدفعة في حساب الصائغ ")
                                    {
                                        itm.billType1 = "حسم ذهب14 لنا بعد الدفع";
                                        itm.customerName1 = dataGridView4.Rows[i].Cells[0].Value.ToString();
                                        itm.customerId1 = itm.GetCustomerid();
                                        itm.userId1 = GlobalVar.id;
                                        itm.dateT1 = dateTimePicker1.Value.Date;
                                        itm.discountAmount = Convert.ToDecimal(dataGridView4.Rows[i].Cells[5].Value.ToString());
                                        itm.billId1 = 1;
                                        itm.discountType1 = "+1";
                                        itm.insertDiscount();
                                    }
                            }
                            if (Convert.ToDecimal(dataGridView4.Rows[i].Cells[2].Value.ToString()) != 0)
                            {
                                    cus.customerName1 = dataGridView4.Rows[i].Cells[0].Value.ToString();
                                    cus.customerId1 = cus.GetCustomerid();
                                    cus.paymentTypeInt1 = -1;
                                    cus.paymentCash1 = Convert.ToDecimal(dataGridView4.Rows[i].Cells[2].Value.ToString());
                                    cus.payment211 = 0;
                                    cus.payment181 = 0;
                                    cus.payment141 = 0;
                                    if (cus.addPayment1() == " تم اضافة الدفعة في حساب الصائغ ")
                                    {
                                        itm.billType1 = "حسم اجور له بعد الدفع";
                                        itm.customerName1 = dataGridView4.Rows[i].Cells[0].Value.ToString();
                                        itm.customerId1 = itm.GetCustomerid();
                                        itm.userId1 = GlobalVar.id;
                                        itm.dateT1 = dateTimePicker1.Value.Date;
                                        itm.discountAmount = Convert.ToDecimal(dataGridView4.Rows[i].Cells[2].Value.ToString());
                                        itm.billId1 = 1;
                                        itm.discountType1 = "-1";
                                        itm.insertDiscount();
                                    }              
                            }
                            if (Convert.ToDecimal(dataGridView4.Rows[i].Cells[4].Value.ToString()) != 0)
                            {
                                    cus.customerName1 = dataGridView4.Rows[i].Cells[0].Value.ToString();
                                    cus.customerId1 = cus.GetCustomerid();
                                    cus.paymentTypeInt1 = -1;
                                    cus.paymentCash1 = 0;
                                    cus.payment211 = Convert.ToDecimal(dataGridView4.Rows[i].Cells[4].Value.ToString());
                                    cus.payment181 = 0;
                                    cus.payment141 = 0;
                                    if (cus.addPayment1() == " تم اضافة الدفعة في حساب الصائغ ")
                                    {
                                        itm.billType1 = "حسم ذهب21 له بعد الدفع";
                                        itm.customerName1 = dataGridView4.Rows[i].Cells[0].Value.ToString();
                                        itm.customerId1 = itm.GetCustomerid();
                                        itm.userId1 = GlobalVar.id;
                                        itm.dateT1 = dateTimePicker1.Value.Date;
                                        itm.discountAmount = Convert.ToDecimal(dataGridView4.Rows[i].Cells[4].Value.ToString());
                                        itm.billId1 = 1;
                                        itm.discountType1 = "-1";
                                        itm.insertDiscount();
                                    }                     
                            }
                            if (Convert.ToDecimal(dataGridView4.Rows[i].Cells[6].Value.ToString()) != 0)
                            {
                                cus.customerName1 = dataGridView4.Rows[i].Cells[0].Value.ToString();
                                cus.customerId1 = cus.GetCustomerid();
                                cus.paymentTypeInt1 = -1;
                                cus.paymentCash1 = 0;
                                cus.payment211 = 0;
                                cus.payment181 = 0;
                                cus.payment141 = Convert.ToDecimal(dataGridView4.Rows[i].Cells[6].Value.ToString());
                                if (cus.addPayment1() == " تم اضافة الدفعة في حساب الصائغ ")
                                {
                                    itm.billType1 = "حسم ذهب14 له بعد الدفع";
                                    itm.customerName1 = dataGridView4.Rows[i].Cells[0].Value.ToString();
                                    itm.customerId1 = itm.GetCustomerid();
                                    itm.userId1 = GlobalVar.id;
                                    itm.dateT1 = dateTimePicker1.Value.Date;
                                    itm.discountAmount = Convert.ToDecimal(dataGridView4.Rows[i].Cells[6].Value.ToString());
                                    itm.billId1 = 1;
                                    itm.discountType1 = "-1";
                                    itm.insertDiscount();
                                }
                            }
                        }
                    }
                    MessageBox.Show("تم تنفيذ الحسومات المدخلة");
                    show_Click(sender, e);
                }
            }
            else if (comboBox1.Text=="حساب صائغ")
            {
                for (int i=0;i<dataGridView1.RowCount;i++)
                {
                    if (dataGridView1.Rows[i].Cells[5].Value.ToString()=="بيع"|| dataGridView1.Rows[i].Cells[5].Value.ToString() == "تسليم")
                    {
                        mCash += Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);
                        m21 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value);
                        m18 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value);
                        m14 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value);
                    }
                    else if (dataGridView1.Rows[i].Cells[5].Value.ToString() == "شراء" || dataGridView1.Rows[i].Cells[5].Value.ToString() == "استلام"|| dataGridView1.Rows[i].Cells[5].Value.ToString() == "راس مال" || dataGridView1.Rows[i].Cells[5].Value.ToString() == "مصروف")
                    {
                        mCash -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[4].Value);
                        m21 -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[3].Value);
                        m18 -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value);
                        m14 -= Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value);
                    }
                    //dd1.Columns.Add(GlobalVar.gold14Label, typeof(decimal));
                    //dd1.Columns.Add(GlobalVar.gold18Label, typeof(decimal));
                    //dd1.Columns.Add(GlobalVar.gold21Label, typeof(decimal));
                    //dd1.Columns.Add(GlobalVar.cashLabel, typeof(decimal));
                    //dd1.Rows.Add(m14, m18, m21, mCash);
                    //dataGridView5.DataSource = dd1;
                    //dataGridView5.Visible = true;
                    cus.customerName1 = itemNameTxtbox.Text;
                    SqlCommand cmd11 = con.CreateCommand();
                    cmd11.CommandType = CommandType.Text;
                    cmd11.CommandText = "select Total14,Total18,Total21,TotalCash from dailyCustomer where (customerId='" + cus.GetCustomerid().ToString() + "' and date= '" + dateTimePicker1.Value.ToString("MM/dd/yyyy 23:59:59") + "')";
                    con.Open();
                    cmd11.ExecuteNonQuery();
                    DataTable dt11 = new DataTable();
                    SqlDataAdapter da11 = new SqlDataAdapter(cmd11);
                    da11.Fill(dt11);
                    con.Close();
                    DataTable dt2 = new DataTable();
                    dt2.Columns.Add(GlobalVar.gold14Label, typeof(string));
                    dt2.Columns.Add(GlobalVar.gold18Label, typeof(string));
                    dt2.Columns.Add(GlobalVar.gold21Label, typeof(string));
                    dt2.Columns.Add(GlobalVar.cashLabel, typeof(string));
                    dt2.Columns.Add("بداية ذهب14", typeof(string));
                    dt2.Columns.Add("بداية ذهب18", typeof(string));
                    dt2.Columns.Add("بداية ذهب21", typeof(string));
                    dt2.Columns.Add("بداية اجور", typeof(string));
                    dt2.Columns.Add("اسم الصائغ", typeof(string));
                    if (dt11.Rows.Count > 0)
                    {
                        dt2.Rows.Add(m14.ToString(), m18.ToString(), m21.ToString(), mCash.ToString(), dt11.Rows[0][0].ToString(), dt11.Rows[0][1].ToString(), dt11.Rows[0][2].ToString(), dt11.Rows[0][3].ToString(), itemNameTxtbox.Text);
                    }
                    else
                    {
                        dt2.Rows.Add(m14.ToString(), m18.ToString(), m21.ToString(), mCash.ToString(), 0, 0, 0, 0, itemNameTxtbox.Text);
                    }
                    if (Convert.ToDecimal(dt2.Rows[0][0]) == 0)
                        dt2.Rows[0][0] = "رصيد ذهب14";
                    if (Convert.ToDecimal(dt2.Rows[0][1]) == 0)
                        dt2.Rows[0][1] = "رصيد ذهب18";
                    if (Convert.ToDecimal(dt2.Rows[0][2]) == 0)
                        dt2.Rows[0][2] = "رصيد ذهب21";
                    if (Convert.ToDecimal(dt2.Rows[0][3]) == 0)
                        dt2.Rows[0][3] = "رصيد اجور";
                    if (Convert.ToDecimal(dt2.Rows[0][4]) == 0)
                        dt2.Rows[0][4] = "رصيد ذهب14";
                    if (Convert.ToDecimal(dt2.Rows[0][5]) == 0)
                        dt2.Rows[0][5] = "رصيد ذهب18";
                    if (Convert.ToDecimal(dt2.Rows[0][6]) == 0)
                        dt2.Rows[0][6] = "رصيد ذهب21";
                    if (Convert.ToDecimal(dt2.Rows[0][7]) == 0)
                        dt2.Rows[0][7] = "رصيد اجور";
                    dataGridView3.DataSource = dt2;
                    dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                    dataGridView3.EnableHeadersVisualStyles = false;
                    foreach (DataGridViewColumn col in dataGridView3.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    dataGridView3.Visible = true;

                    foreach (DataGridViewColumn col in dataGridView3.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            else if (comboBox1.Text=="تنفيذ تحويلات")
            {
                int custmId = 0;
                if (dataGridView4.RowCount > 1)
                {
                    for (int i = 0; i < dataGridView4.RowCount - 1; i++)
                    {
                        if (dataGridView4.Rows[i].Cells[0].Value.ToString() != "")
                        {
                            SqlConnection con = new SqlConnection(GlobalVar.dataBaseLocation);
                            pay.customerName1 = dataGridView4.Rows[i].Cells[0].Value.ToString();
                            custmId = pay.GetCustomerid();
                            con.Open();
                            SqlCommand cmd = con.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "SELECT TotalCash , Total21 , Total14 from customers where customerId = '" + Convert.ToString(custmId) + "' ";
                            cmd.ExecuteNonQuery();
                            DataTable dt = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            da.Fill(dt);
                            con.Close();
                            if (dt.Rows.Count > 0)
                            {
                                mCash = Convert.ToDecimal(dt.Rows[i][0].ToString());
                                m21 = Convert.ToDecimal(dt.Rows[i][1].ToString());
                                m14 = Convert.ToDecimal(dt.Rows[i][2].ToString());
                            }
                        }
                        if (dataGridView4.Rows[i].Cells[0].Value.ToString().Trim() != "" && dataGridView4.Rows[i].Cells[1].Value.ToString().Trim() != "" && dataGridView4.Rows[i].Cells[2].Value.ToString().Trim() != "")
                        {
                            pay.customerId1 = custmId;
                            if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "اجور صائغ إلى ذهب21")
                            {
                                fCash = mCash - Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString());
                                f21 = m21 + Convert.ToDecimal(dataGridView4.Rows[i].Cells[3].Value.ToString());
                                pay.daily141 = m14;
                                pay.daily211 = f21;
                                pay.dailyCash1 = fCash;
                                MessageBox.Show(pay.updateCustomerAccount());
                            }
                            else if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "اجور صائغ إلى ذهب14")
                            {
                                fCash = mCash - Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString());
                                f14 = m14 + Convert.ToDecimal(dataGridView4.Rows[i].Cells[3].Value.ToString());
                                pay.daily141 = f14;
                                pay.daily211 = m21;
                                pay.dailyCash1 = fCash;
                                MessageBox.Show(pay.updateCustomerAccount());
                            }
                            else if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "ذهب21 صائغ إلى اجور")
                            {
                                f21 = m21 - Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString());
                                fCash = mCash + Convert.ToDecimal(dataGridView4.Rows[i].Cells[3].Value.ToString());
                                pay.daily141 = m14;
                                pay.daily211 = f21;
                                pay.dailyCash1 = fCash;
                                MessageBox.Show(pay.updateCustomerAccount());
                            }
                            else if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "ذهب21 صائغ إلى ذهب14")
                            {
                                f21 = m21 - Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString());
                                f14 = m14 + Convert.ToDecimal(dataGridView4.Rows[i].Cells[3].Value.ToString());
                                pay.daily141 = f14;
                                pay.daily211 = f21;
                                pay.dailyCash1 = mCash;
                                MessageBox.Show(pay.updateCustomerAccount());
                            }
                            else if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "ذهب14 صائغ إلى اجور")
                            {
                                f14 = m14 - Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString());
                                fCash = mCash + Convert.ToDecimal(dataGridView4.Rows[i].Cells[3].Value.ToString());
                                pay.daily141 = f14;
                                pay.daily211 = m21;
                                pay.dailyCash1 = fCash;
                                MessageBox.Show(pay.updateCustomerAccount());
                            }
                            else if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "ذهب14 صائغ إلى ذهب21")
                            {
                                f14 = m14 - Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString());
                                f21 = m21 + Convert.ToDecimal(dataGridView4.Rows[i].Cells[3].Value.ToString());
                                pay.daily141 = f14;
                                pay.daily211 = f21;
                                pay.dailyCash1 = mCash;
                                MessageBox.Show(pay.updateCustomerAccount());
                            }
                        }
                    }
                }
                show_Click(sender, e);
            }
            //else if (comboBox1.Text == "المصوغات")
            //{
            //    for (int i=0;i<dataGridView1.RowCount;i++)
            //    {
            //        if (Convert.ToInt16(dataGridView1.Rows[i].Cells[3].Value) == 14)
            //        {
            //            if (Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value) > 0)
            //                m14 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value);
            //            else if (Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value) < 0)
            //                m14 += (-1 * Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value));
            //        }
            //        else if (Convert.ToInt16(dataGridView1.Rows[i].Cells[3].Value) == 18)
            //        {
            //            if (Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value) > 0)
            //                m18 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value);
            //            else if (Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value) < 0)
            //                m18 += (-1 * Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value));
            //        }
            //        else if (Convert.ToInt16(dataGridView1.Rows[i].Cells[3].Value) == 21)
            //        {
            //            if (Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value) > 0)
            //                m21 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value);
            //            else if (Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value) < 0)
            //                m21 += (-1 * Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value));
            //        }
            //    }
            //    dataGridView3.Rows[0].Cells[2].Value = m21.ToString();
            //    dataGridView3.Rows[0].Cells[1].Value = m18.ToString();
            //    dataGridView3.Rows[0].Cells[0].Value = m14.ToString();
            //    foreach (DataGridViewColumn col in dataGridView3.Columns)
            //    {
            //        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    }
            //}
            //else if (comboBox1.Text=="حركة مصاغ")
            //{
            //    for (int i = 0; i < dataGridView1.RowCount; i++)
            //    {
            //        mCash += Convert.ToDecimal(dataGridView1.Rows[i].Cells[0].Value);
            //        m21 += Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value);
            //    }
            //    dataGridView3.Rows[0].Cells[1].Value = mCash.ToString();
            //    dataGridView3.Rows[0].Cells[2].Value = m21.ToString();
            //    foreach (DataGridViewColumn col in dataGridView3.Columns)
            //    {
            //        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //    }
            //}
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (comboBox1.Text=="حسابات الصاغة")
            {
                calculate_Click(sender,e);
            }
        }

        private void dataGridView4_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (comboBox1.Text == "تنفيذ حسومات")
                {
                    if (dataGridView4.RowCount > 0)
                    {
                        for (int i = 0; i < dataGridView4.RowCount - 1; i++)
                        {
                            if (dataGridView4.Rows[i].Cells[0].Value.ToString().Trim() != "")
                            {
                                for (int j = 1; j < 7; j++)
                                {
                                    if (dataGridView4.Rows[i].Cells[j].Value.ToString().Trim() == "")
                                        dataGridView4.Rows[i].Cells[j].Value = "0";
                                }
                            }
                        }
                    }
                }
                else if (comboBox1.Text == "تنفيذ تحويلات")
                {
                    if (dataGridView4.RowCount > 1)
                    {
                        if (dataGridView4.CurrentCell.ColumnIndex != 3)
                        {
                            for (int i = 0; i < dataGridView4.RowCount - 1; i++)
                            {

                                if (dataGridView4.Rows[i].Cells[0].Value.ToString().Trim() != "" && dataGridView4.Rows[i].Cells[1].Value.ToString().Trim() != "" && dataGridView4.Rows[i].Cells[2].Value.ToString().Trim() != "")
                                {
                                    if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "اجور صائغ إلى ذهب21")
                                    {
                                        if (goldPrice.Text.Trim() != "")
                                        {
                                            dataGridView4.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString()) / Convert.ToDecimal(goldPrice.Text)), 2);
                                        }
                                        else
                                        {
                                            MessageBox.Show("ادخل سعر غرام ذهب21");
                                            goldPrice.Select();
                                            goldPrice.Focus();
                                        }
                                    }
                                    else if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "اجور صائغ إلى ذهب14")
                                    {
                                        if (goldPrice.Text.Trim() != "")
                                        {
                                            dataGridView4.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString()) * Convert.ToDecimal(1.5)) / Convert.ToDecimal(goldPrice.Text)), 2);
                                        }
                                        else
                                        {
                                            MessageBox.Show("ادخل سعر غرام ذهب21");
                                            goldPrice.Select();
                                            goldPrice.Focus();
                                        }
                                    }
                                    else if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "ذهب21 صائغ إلى اجور")
                                    {
                                        if (goldPrice.Text.Trim() != "")
                                        {
                                            dataGridView4.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[1].Value.ToString()) * Convert.ToDecimal(goldPrice.Text)), 2);
                                        }
                                        else
                                        {
                                            MessageBox.Show("ادخل سعر غرام ذهب21");
                                            goldPrice.Select();
                                            goldPrice.Focus();
                                        }
                                    }
                                    else if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "ذهب21 صائغ إلى ذهب14")
                                    {
                                        dataGridView4.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString()) * Convert.ToDecimal(1.5)), 2);
                                    }
                                    else if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "ذهب14 صائغ إلى اجور")
                                    {
                                        if (goldPrice.Text.Trim() != "")
                                        {
                                            dataGridView4.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString()) * Convert.ToDecimal(goldPrice.Text) * Convert.ToDecimal(0.666667)), 2);
                                        }
                                        else
                                        {
                                            MessageBox.Show("ادخل سعر غرام ذهب21");
                                            goldPrice.Select();
                                            goldPrice.Focus();
                                        }
                                    }
                                    else if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "ذهب14 صائغ إلى ذهب21")
                                    {
                                        dataGridView4.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString()) * Convert.ToDecimal(0.66667)), 2);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void dataGridView4_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (T)
            { string text = dataGridView4.Columns[0].HeaderText;
                if (text.Contains("اسم الصائغ"))
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
            else if (!T)
            {
                    string text = dataGridView4.Columns[2].HeaderText;
                    if (text.Equals("بيان التحويل"))
                    {
                        TextBox tx = e.Control as TextBox;
                        if (tx != null)
                        {
                            tx.AutoCompleteMode = AutoCompleteMode.Suggest;
                            tx.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            AutoCompleteStringCollection sc = new AutoCompleteStringCollection();
                            add3_items(sc);
                            tx.AutoCompleteCustomSource = sc;
                        }
                    }
            }
        }

        public void add_items(AutoCompleteStringCollection col)
        {
            try
            {
                con.Open();
                SqlDataReader DataRdr1;
                SqlCommand cmd1 = new SqlCommand("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);//+ "and customerName like %"+dataGridView4.CurrentCell.Value.ToString()+"%", con);
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
        
        private void itemNameTxtbox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AutoCompleteStringCollection items = new AutoCompleteStringCollection();
                items.Clear();
                itemNameTxtbox.DataSource = null;
                bool a1 = true;
                //items.Add("");
                if (itemNameTxtbox.Text.Length > 0)
                {
                    //if (itemNameTxtbox.Text!= "اختر اسم الصائغ ...")
                    //{
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString().Contains(itemNameTxtbox.Text))
                        {
                            for (int j = 0; j < items.Count; j++)
                            {
                                if (dt.Rows[i][0].ToString() == items[j].ToString())
                                    a1 = false;
                            }
                            if (a1)
                                items.Add(dt.Rows[i][0].ToString());
                        }
                    }
                    //MessageBox.Show(items.Count.ToString());
                    //for (int j = 0; j < items.Count; j++)
                    //    MessageBox.Show(items[j].ToString());
                    //itemNameTxtbox.AutoCompleteCustomSource = items;  
                    //}
                }
                itemNameTxtbox.DataSource = items;
                itemNameTxtbox.DroppedDown = true;
            }
            catch(Exception er) { MessageBox.Show(er.Message); }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            if (comboBox1.Text == "حسابات الصاغة")
            {
                calculate_Click(sender, e);
            }
            else if(comboBox1.Text=="الدفعات")
            {
                var sortedCol = dataGridView1.SortedColumn.DataPropertyName;
                var sortDirection = dataGridView1.SortOrder;
                if (sortDirection.ToString() == "Ascending")
                    dataGridView2.Sort(dataGridView2.Columns[sortedCol], ListSortDirection.Ascending);
                else
                    dataGridView2.Sort(dataGridView2.Columns[sortedCol], ListSortDirection.Descending);
            }
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (comboBox1.Text == "الدفعات")
            {
                int tmpId = (int)e.Row.Index;
                dataGridView2.Rows.RemoveAt(tmpId);
            }
        }

        private void dataGridView4_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView4.CurrentCellAddress.X == 0)
            {
                T = true;
            }
            else if (dataGridView4.CurrentCellAddress.X == 2)
            {
                T = false;
            }
        }

        private void calc_Click(object sender, EventArgs e)
        {
            if (dataGridView4.RowCount > 1)
            {
                for (int i = 0; i < dataGridView4.RowCount - 1; i++)
                {
                    if (dataGridView4.Rows[i].Cells[0].Value.ToString().Trim() != "" && dataGridView4.Rows[i].Cells[1].Value.ToString().Trim() != "" && dataGridView4.Rows[i].Cells[2].Value.ToString().Trim() != "")
                    {
                        if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "اجور صائغ إلى ذهب21")
                        {
                            if (dataGridView4.Rows[i].Cells[2].Value.ToString().Trim() != "")
                            {
                                dataGridView4.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString()) / Convert.ToDecimal(goldPrice.Text)), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "اجور صائغ إلى ذهب14")
                        {
                            if (dataGridView4.Rows[i].Cells[1].Value.ToString().Trim() != "")
                            {
                                dataGridView4.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString()) * Convert.ToDecimal(1.5)) / Convert.ToDecimal(goldPrice.Text)), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "ذهب21 صائغ إلى اجور")
                        {
                            if (dataGridView4.Rows[i].Cells[1].Value.ToString().Trim() != "")
                            {                            
                                dataGridView4.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString()) * Convert.ToDecimal(goldPrice.Text)), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "ذهب21 صائغ إلى ذهب14")
                        {
                            dataGridView4.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString()) * Convert.ToDecimal(1.5)), 2);
                        }
                        else if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "ذهب14 صائغ إلى اجور")
                        {
                            if (dataGridView4.Rows[i].Cells[1].Value.ToString().Trim() != "")
                            {
                                dataGridView4.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString()) * Convert.ToDecimal(goldPrice.Text) * Convert.ToDecimal(0.666667)), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView4.Rows[i].Cells[2].Value.ToString() == "ذهب14 صائغ إلى ذهب21")
                        {
                            dataGridView4.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView4.Rows[i].Cells[1].Value.ToString()) * Convert.ToDecimal(0.66667)), 2);
                        }
                    }
                }
            }
        }

        private void deletePay_Click(object sender, EventArgs e)
        {
            try
            {
                if (!check())
                {
                    int rowIndex3 = dataGridView4.CurrentCell.RowIndex;
                    dataGridView4.Rows.RemoveAt(rowIndex3);
                    calc_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void goldPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridView4.Select();
                dataGridView4.Focus();
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (comboBox1.Text=="تنفيذ تحويلات"||comboBox1.Text=="تنفيذ حسومات")
            {
                dataGridView4.Select();
                dataGridView4.Focus();
            }
            else if (comboBox1.Text == "الدفعات" || comboBox1.Text == "الفواتير" || comboBox1.Text == "كشف حساب تفصيلي" || comboBox1.Text == "حساب صائغ" || comboBox1.Text == "حركة مصاغ" )
            {
                itemNameTxtbox.Select();
                itemNameTxtbox.Focus();
            }
            else if (comboBox1.Text == "القيد اليومي" || comboBox1.Text == "حسابات الصاغة" || comboBox1.Text == "الحسومات" || comboBox1.Text == "المصوغات" )
            {
                dataGridView1.Select();
                dataGridView1.Focus();
            }
        }

        private void itemNameTxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (comboBox1.Text == "الدفعات" || comboBox1.Text == "الفواتير" || comboBox1.Text == "كشف حساب تفصيلي" || comboBox1.Text == "حساب صائغ" || comboBox1.Text == "حركة مصاغ")
                {
                    show_Click(sender, e);
                }
            }
        }
    }
}




