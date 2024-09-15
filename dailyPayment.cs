using System;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace iGOLD
{
    public partial class dailyPayment : Form
    {
        public dailyPayment()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private const int cGrip = 16;
        private const int cCaption = 32;
        SqlConnection con = new SqlConnection(GlobalVar.dataBaseLocation);
        paymentdb_Class pay = new paymentdb_Class();
        Itemdb_Class itm = new Itemdb_Class();
        Customerdb_Class cus = new Customerdb_Class();
        main_iGOLD main = new main_iGOLD();
        DataTable dataT3 = new DataTable();
        decimal rCash = 0;
        decimal rGold21 = 0;
        decimal rGold18 = 0;
        decimal rGold14 = 0;
        decimal cash = 0;
        decimal gold21 = 0;
        decimal gold18 = 0;
        decimal gold14 = 0;
        string a = "";
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
            DialogResult result = MessageBox.Show("هل أنت متأكد بالخروج من تسجيل الدفعات", "الخروج", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

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
            if (W == 1004 && H == 700)
            {
                this.SetDesktopLocation(0, 0);
                this.Size = new Size(screenW, screenH-40);
                close.Location = new Point(0, 0);
                maximize.Location = new Point(34, 0);
                minimize.Location = new Point(78, 0);
                bar.Size = new Size(this.Width- 78, 37);
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
                customerPayment form = new customerPayment();
                form.Show();
                this.Close();
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

        public void save_Click(object sender, EventArgs e)
        {
            con.Close();
            if (main.IsConnected())
            {
                try
                {
                    save_Click1();
                    cancel_Click(sender, e);
                    customerNameTxtbox.Select();
                    customerNameTxtbox.Focus();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
           
        public void save_Click1()
        {

            if (check())
            {

                MessageBox.Show("!!.. يرجى ادخال مقدار دفعة الصائغ");

            }

            else if (Equals(customerNameTxtbox.Text, "أدخل اسم الصائغ"))
            {

                MessageBox.Show("!!.. يرجى ادخال اسم الصائغ");

            }

            else
            {
                if (Equals(paymentCashTxtbox.Text, ""))
                {
                    paymentCashTxtbox.Text = "0";
                }
                if (Equals(paymentGold21Txtbox.Text, ""))
                {
                    paymentGold21Txtbox.Text = "0";
                }
                if (Equals(paymentGold18Txtbox.Text, ""))
                {
                    paymentGold18Txtbox.Text = "0";
                }
                if (Equals(paymentGold14Txtbox.Text, ""))
                {
                    paymentGold14Txtbox.Text = "0";
                }
                string customerName = customerNameTxtbox.Text;
                cus.name1 = customerName;
                if (cus.checkCustomerExist() == "true")
                {
                    if (Convert.ToDecimal(paymentCashTxtbox.Text) != 0 || Convert.ToDecimal(paymentGold21Txtbox.Text) != 0 || Convert.ToDecimal(paymentGold18Txtbox.Text) != 0 || Convert.ToDecimal(paymentGold14Txtbox.Text) != 0)
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
                            rCash = Convert.ToDecimal(paymentCashTxtbox.Text.Trim());
                            rGold21 = Convert.ToDecimal(paymentGold21Txtbox.Text.Trim());
                            rGold18 = Convert.ToDecimal(paymentGold18Txtbox.Text.Trim());
                            rGold14 = Convert.ToDecimal(paymentGold14Txtbox.Text.Trim());
                        }
                        cash = Convert.ToDecimal(paymentCashTxtbox.Text.Trim());
                        gold21 = Convert.ToDecimal(paymentGold21Txtbox.Text.Trim());
                        gold18 = Convert.ToDecimal(paymentGold18Txtbox.Text.Trim());
                        gold14 = Convert.ToDecimal(paymentGold14Txtbox.Text.Trim());

                        pay.userId1 = GlobalVar.id;
                        pay.item14Id1 = 0;
                        pay.item18Id1 = 0;
                        pay.item21Id1 = 0;
                        pay.item14Count1 = 0;
                        pay.item18Count1 = 0;
                        pay.item21Count1 = 0;
                        if (customerPaymentLabel.Text == "استلام")
                        {
                            itm.billType1 = "شراء";
                            pay.paymentTypeInt1 = -1;
                            pay.paymentTypeString1 = customerPaymentLabel.Text;
                            if (customerNameTxtbox.Text == "راس مال")
                            {
                                pay.paymentTypeString1 = "راس مال";
                                pay.paymentTypeInt1 = -1;
                            }
                            else
                            {
                                pay.paymentTypeString1 = customerPaymentLabel.Text;
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
                            pay.paymentTypeString1 = customerPaymentLabel.Text;
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
                        if (customerNameTxtbox.Text == "راس مال")
                        {
                            pay.paymentNotice1 = "زيادة راس مال (اجور)";
                        }
                        else
                        {
                            if (customerPaymentLabel.Text.Trim() == "استلام")
                            notice = ("دفعة " + customerPaymentLabel.Text + " من " + customerNameTxtbox.Text);
                            else
                            notice = ("دفعة " + customerPaymentLabel.Text + " الى " + customerNameTxtbox.Text);

                            pay.paymentNotice1 = notice;
                        }
                        //////////////////////////////////////////////////
                        pay.paymentDateTime1 = dateTimePicker1.Value.Date;
                        pay.realPaymentCash1 = rCash;
                        pay.realPayment211 = rGold21;
                        pay.realPayment181 = rGold18;
                        pay.realPayment141 = rGold14;
                        pay.paymentNo1 = no.Text;

                        pay.userId1 = GlobalVar.id;


                        string c1 = pay.insertPayment();
                        if (c1 == " تم الحفظ ")
                        {
                            if (customerPaymentLabel.Text == "استلام")
                            {
                                pay.customerName1 = customerNameTxtbox.Text;
                                pay.customerId1 = pay.GetCustomerid();
                                pay.paymentCash1 = cash;
                                pay.payment211 = gold21;
                                pay.payment181 = gold18;
                                pay.payment141 = gold14;
                                pay.paymentTypeInt1 = -1;
                            }
                            else
                            {
                                pay.customerName1 = customerNameTxtbox.Text;
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
                                    pay.paymentId1 = Convert.ToInt32(pay.getPaymentId());
                                    pay.before1 = Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value);
                                    pay.after1 = Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value);
                                    pay.convert1 = dataGridView3.Rows[i].Cells[1].Value.ToString();
                                    pay.insertPaymentDetails();
                                }
                            }
                            else
                            {
                                if (cash != 0)
                                {
                                    pay.paymentId1 = Convert.ToInt32(pay.getPaymentId());
                                    pay.before1 = cash;
                                    pay.after1 = cash;
                                    pay.convert1 = "اجور دفعة";
                                    pay.insertPaymentDetails();
                                }
                                if (gold21 != 0)
                                {
                                    pay.paymentId1 = Convert.ToInt32(pay.getPaymentId());
                                    pay.before1 = gold21;
                                    pay.after1 = gold21;
                                    pay.convert1 = "ذهب21 دفعة";
                                    pay.insertPaymentDetails();
                                }
                                if (gold18 != 0)
                                {
                                    pay.paymentId1 = Convert.ToInt32(pay.getPaymentId());
                                    pay.before1 = gold18;
                                    pay.after1 = gold18;
                                    pay.convert1 = "ذهب18 دفعة";
                                    pay.insertPaymentDetails();
                                }
                                if (gold14 != 0)
                                {
                                    pay.paymentId1 = Convert.ToInt32(pay.getPaymentId());
                                    pay.before1 = gold14;
                                    pay.after1 = gold14;
                                    pay.convert1 = "ذهب14 دفعة";
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
        //
        public void print_click()
        {
            ////((Form)printPreviewDialog1).WindowState = FormWindowState.Maximized;
            ////if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            ////{
                printDocument1.Print();
            //}
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            customerNameTxtbox.Text = "أدخل اسم الصائغ";
            paymentCashTxtbox.Text = "0";
            paymentGold21Txtbox.Text = "0";
            paymentGold14Txtbox.Text = "0";
            paymentGold18Txtbox.Text = "0";
            afterCash.Text = "0";
            after21.Text = "0";
            after14.Text = "0";
            after18.Text = "0";
            customerTotalCash.Text = "0";
            customerTotalGold21.Text = "0";
            customerTotalGold18.Text = "0";
            customerTotalGold14.Text = "0";
            realCash.Text = "0";
            real21.Text = "0";
            real18.Text = "0";
            real14.Text = "0";
            no.Text = "000";
            detailsTxtbox.Text = "اكتب ملاحظات عن الدفعة";
            goldPrice.Text = "";
            dateTimePicker1.Value = DateTime.Now.Date;
        }
        //
        private void paymentGold21Txtbox_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(paymentGold21Txtbox.Text))
            {
                paymentGold21Txtbox.Text = "0";
            }
            paymentGold21Txtbox.LineIdleColor = GlobalVar.menuHoverColor;
            //addComma(paymentGold21Txtbox);

        }

        private void paymentGold21Txtbox_MouseEnter(object sender, EventArgs e)
        {

            if (Equals(paymentGold21Txtbox.Text, "0"))
            {
                paymentGold21Txtbox.Text = "";
            }
        }

        private void paymentGold21Txtbox_OnValueChanged(object sender, EventArgs e)
        {
            if (paymentGold21Txtbox.Text.Trim().TrimStart() != "" && paymentGold21Txtbox.Text.Trim().TrimStart() != " ")

            {
                try
                {
                  decimal i = Convert.ToDecimal(paymentGold21Txtbox.Text.Replace(" ", ""));
                    if (customerPaymentLabel.Text == "استلام")
                        after21.Text = ( Convert.ToDecimal(customerTotalGold21.Text)-i).ToString();
                    else
                        after21.Text = (i + Convert.ToDecimal(customerTotalGold21.Text)).ToString();
                    if (customerNameTxtbox.Text == "راس مال")
                    {
                        pay.paymentNotice1 = "زيادة راس مال (اجور)";
                    }
                    else
                    {
                        if (customerPaymentLabel.Text.Trim() == "استلام")
                            a = ("دفعة " + customerPaymentLabel.Text + " من " + customerNameTxtbox.Text);
                        else
                            a = ("دفعة " + customerPaymentLabel.Text + " الى " + customerNameTxtbox.Text);

                        pay.paymentNotice1 = a;
                        detailsTxtbox.Text = a;
                    }
                    if (customerNameTxtbox.Text=="رأس مال")
                    {
                        paymentGold14Txtbox.Text = "0";
                        paymentGold18Txtbox.Text = "0";
                        paymentGold21Txtbox.Text = "0";
                    }

                }

                catch
                {

                    //MessageBox.Show("يرجى ادخال أرقام فقط");

                    paymentGold21Txtbox.Text = "0";

                }

            }
        }

        private void paymentGold18Txtbox_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(paymentGold18Txtbox.Text))
            {
                paymentGold18Txtbox.Text = "0";
            }
            paymentGold18Txtbox.LineIdleColor = GlobalVar.menuHoverColor;
            addComma(paymentGold18Txtbox);

        }

        private void paymentGold18Txtbox_MouseEnter(object sender, EventArgs e)
        {

            if (Equals(paymentGold18Txtbox.Text, "0"))
            {
                paymentGold18Txtbox.Text = "";
            }
        }

        private void paymentGold18Txtbox_OnValueChanged(object sender, EventArgs e)
        {
            if (paymentGold18Txtbox.Text.Trim().TrimStart() != "" && paymentGold18Txtbox.Text.Trim().TrimStart() != " ")


            {

                try

                {

                    decimal i = Convert.ToDecimal(paymentGold18Txtbox.Text.Replace(" ", ""));
                    if (customerPaymentLabel.Text == "استلام")

                        after18.Text = ( Convert.ToDecimal(customerTotalGold18.Text)-i).ToString();
                    else
                        after18.Text = (i + Convert.ToDecimal(customerTotalGold18.Text)).ToString();
                    if (customerNameTxtbox.Text == "راس مال")
                    {
                        pay.paymentNotice1 = "زيادة راس مال (اجور)";
                    }
                    else
                    {
                        if (customerPaymentLabel.Text.Trim() == "استلام")
                            a = ("دفعة " + customerPaymentLabel.Text + " من " + customerNameTxtbox.Text);
                        else
                            a = ("دفعة " + customerPaymentLabel.Text + " الى " + customerNameTxtbox.Text);

                        pay.paymentNotice1 = a;
                        detailsTxtbox.Text = a;
                    }
                    if (customerNameTxtbox.Text == "رأس مال")
                    {
                        paymentGold14Txtbox.Text = "0";
                        paymentGold18Txtbox.Text = "0";
                        paymentGold21Txtbox.Text = "0";
                    }
                }

                catch
                {

                    //MessageBox.Show("يرجى ادخال أرقام فقط");

                    paymentGold18Txtbox.Text = "0";

                }

            }
        }
         
        private void paymentGold14Txtbox_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(paymentGold14Txtbox.Text))
            {
                paymentGold14Txtbox.Text = "0";
            }
            paymentGold14Txtbox.LineIdleColor = GlobalVar.menuHoverColor;
            //addComma(paymentGold14Txtbox);
        }

        private void paymentGold14Txtbox_MouseEnter(object sender, EventArgs e)
        {

            if (Equals(paymentGold14Txtbox.Text, "0"))
            {
                paymentGold14Txtbox.Text = "";
            }
        }

        private void paymentGold14Txtbox_OnValueChanged(object sender, EventArgs e)
        {
            if (paymentGold14Txtbox.Text.Trim().TrimStart() != "" && paymentGold14Txtbox.Text.Trim().TrimStart() != " ")


            {

                try

                {

                    decimal i = Convert.ToDecimal(paymentGold14Txtbox.Text.Replace(" ", ""));
                    if (customerPaymentLabel.Text == "استلام")
                    {
                        after14.Text = (Convert.ToDecimal(customerTotalGold14.Text)-i).ToString();
                    }
                    else
                    {
                        after14.Text = (i + Convert.ToDecimal(customerTotalGold14.Text)).ToString();
                    }
                    if (customerNameTxtbox.Text == "راس مال")
                    {
                        pay.paymentNotice1 = "زيادة راس مال (اجور)";
                    }
                    else
                    {
                      if (customerPaymentLabel.Text.Trim() == "استلام")
                            a = ("دفعة " + customerPaymentLabel.Text + " من " + customerNameTxtbox.Text);
                        else
                            a = ("دفعة " + customerPaymentLabel.Text + " الى " + customerNameTxtbox.Text);

                        pay.paymentNotice1 = a;
                        detailsTxtbox.Text = a;
                    }
                    if (customerNameTxtbox.Text == "رأس مال")
                    {
                        paymentGold14Txtbox.Text = "0";
                        paymentGold18Txtbox.Text = "0";
                        paymentGold21Txtbox.Text = "0";
                    }
                }

                catch
                {

                    //MessageBox.Show("يرجى ادخال أرقام فقط");

                    paymentGold14Txtbox.Text = "0";

                }
            }
        }

        private void paymentCashTxtbox_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(paymentCashTxtbox.Text))
            {
                paymentCashTxtbox.Text = "0";
            }
            paymentCashTxtbox.LineIdleColor = Color.MediumSpringGreen;
            //addCashComma(paymentCashTxtbox);
        }

        private void paymentCashTxtbox_MouseEnter(object sender, EventArgs e)
        {

            if (Equals(paymentCashTxtbox.Text, "0"))
            {
                paymentCashTxtbox.Text = "";
            }
        }

        private void paymentCashTxtbox_OnValueChanged(object sender, EventArgs e)
        {
            if (paymentCashTxtbox.Text.Trim().TrimStart() != "" && paymentCashTxtbox.Text.Trim().TrimStart() != " ")
            {
                try

                {

                    decimal i = Convert.ToDecimal(paymentCashTxtbox.Text.Trim());

                    if (customerPaymentLabel.Text=="استلام")
                        afterCash.Text = ( Convert.ToDecimal(customerTotalCash.Text)-i).ToString();
                    else
                        afterCash.Text = (i + Convert.ToDecimal(customerTotalCash.Text)).ToString();
                    if (customerNameTxtbox.Text == "راس مال")
                    {
                        pay.paymentNotice1 = "زيادة راس مال (اجور)";
                    }
                    else
                    {
                        if (customerPaymentLabel.Text.Trim() == "استلام")
                            a = ("دفعة " + customerPaymentLabel.Text + " من " + customerNameTxtbox.Text);
                        else
                            a = ("دفعة " + customerPaymentLabel.Text + " الى " + customerNameTxtbox.Text);
                        pay.paymentNotice1 = a;
                        detailsTxtbox.Text = a;
                    }
                }

                catch
                {

                    ////MessageBox.Show("يرجى ادخال أرقام فقط");

                    paymentCashTxtbox.Text = "0";

                }
            }
        }

        private void customerTotalCash_OnValueChanged(object sender, EventArgs e)
        {
            decimal kk = 0;
            if (customerTotalCash.Text.TrimStart() != "" && customerTotalCash.Text.TrimStart() != " ")
             {

                try
                {
                    kk = Convert.ToDecimal(customerTotalCash.Text);
                }
                catch { }


                if (kk < 0)
                {
                    customerTotalCash.ForeColor = Color.OrangeRed;
                    cashL.ForeColor = Color.OrangeRed;
                    cashL.Text = "له";
                    cashL.Visible = true;
                    label25.ForeColor = Color.OrangeRed;
                    label26.ForeColor = Color.OrangeRed;
                }

                else if (kk > 0)
                {
                    customerTotalCash.ForeColor = Color.MediumSeaGreen;
                    cashL.ForeColor = Color.MediumSeaGreen;
                    cashL.Text = "لـنا";
                    cashL.Visible = true;
                    label25.ForeColor = Color.MediumSeaGreen;
                    label26.ForeColor = Color.MediumSeaGreen;
                }
                else
                {
                    customerTotalCash.ForeColor = Color.FromArgb(254, 199, 32);
                    cashL.Visible = false;
                    label25.ForeColor = Color.MediumSeaGreen;
                    label26.ForeColor = Color.MediumSeaGreen;
                }
                addCashComma(customerTotalCash);
                if (paymentGold21Txtbox.Text.Trim() == "") { paymentGold21Txtbox.Text = "0"; }
                if (paymentGold18Txtbox.Text.Trim() == "") { paymentGold18Txtbox.Text = "0"; }
                if (paymentGold14Txtbox.Text.Trim() == "") { paymentGold14Txtbox.Text = "0"; }
                if (paymentCashTxtbox.Text.Trim() == "") { paymentCashTxtbox.Text = "0"; }
                paymentCashTxtbox_OnValueChanged(sender, e);
                paymentGold14Txtbox_OnValueChanged(sender, e);
                paymentGold18Txtbox_OnValueChanged(sender, e);
                paymentGold21Txtbox_OnValueChanged(sender, e);
            }
        }

        private void customerTotalGold21_OnValueChanged(object sender, EventArgs e)
        {
            if (customerTotalGold21.Text.Trim().TrimStart() != "" && customerTotalGold21.Text.Trim().TrimStart() != " ")
            {
                double i = 0;
                try
                {
                    i = Convert.ToDouble(customerTotalGold21.Text.Replace(" ", ""));
                }
                catch { }


                if (i < 0)
                {
                    customerTotalGold21.ForeColor = Color.OrangeRed;
                    gold21L.ForeColor = Color.OrangeRed;
                    gold21L.Text = "له";
                    gold21L.Visible = true;
                }

                else if (i > 0)
                {
                    customerTotalGold21.ForeColor = Color.MediumSeaGreen;
                    gold21L.ForeColor = Color.MediumSeaGreen;
                    gold21L.Text = "لـنا";
                    gold21L.Visible = true;
                }
                else
                {
                    customerTotalGold21.ForeColor = Color.FromArgb(254, 199, 32);
                    gold21L.Visible = false;
                }
                addComma(customerTotalGold21);
                if (paymentGold21Txtbox.Text.Trim() == "") { paymentGold21Txtbox.Text = "0"; }
                if (paymentGold18Txtbox.Text.Trim() == "") { paymentGold18Txtbox.Text = "0"; }
                if (paymentGold14Txtbox.Text.Trim() == "") { paymentGold14Txtbox.Text = "0"; }
                if (paymentCashTxtbox.Text.Trim() == "") { paymentCashTxtbox.Text = "0"; }
                paymentCashTxtbox_OnValueChanged(sender, e);
                paymentGold14Txtbox_OnValueChanged(sender, e);
                paymentGold18Txtbox_OnValueChanged(sender, e);
                paymentGold21Txtbox_OnValueChanged(sender, e);
            }
        }

        private void customerTotalGold18_OnValueChanged(object sender, EventArgs e)
        {
           
            if (customerTotalGold18.Text.Trim().TrimStart() != "" && customerTotalGold18.Text.Trim().TrimStart() != " ")
        {
            double i = 0;
            try
            {
                i = Convert.ToDouble(customerTotalGold18.Text.Replace(" ", ""));
            }
            catch { }


            if (i < 0)
            {
                customerTotalGold18.ForeColor = Color.OrangeRed;
                gold18L.ForeColor = Color.OrangeRed;
                gold18L.Text = "له";
                gold18L.Visible = true;
            }

            else if (i > 0)
            {
                customerTotalGold18.ForeColor = Color.MediumSeaGreen;
                gold18L.ForeColor = Color.MediumSeaGreen;
                gold18L.Text = "لـنا";
                gold18L.Visible = true;
            }
            else
            {
                customerTotalGold18.ForeColor = Color.FromArgb(254, 199, 32);
                gold18L.Visible = false;
            }
                addComma(customerTotalGold18);
                if (paymentGold21Txtbox.Text.Trim() == "") { paymentGold21Txtbox.Text = "0"; }
                if (paymentGold18Txtbox.Text.Trim() == "") { paymentGold18Txtbox.Text = "0"; }
                if (paymentGold14Txtbox.Text.Trim() == "") { paymentGold14Txtbox.Text = "0"; }
                if (paymentCashTxtbox.Text.Trim() == "") { paymentCashTxtbox.Text = "0"; }
                paymentCashTxtbox_OnValueChanged(sender, e);
                paymentGold14Txtbox_OnValueChanged(sender, e);
                paymentGold18Txtbox_OnValueChanged(sender, e);
                paymentGold21Txtbox_OnValueChanged(sender, e);
            }
    }

        private void customerTotalGold14_OnValueChanged(object sender, EventArgs e)
        {
            

            if (customerTotalGold14.Text.Trim().TrimStart() != "" && customerTotalGold14.Text.Trim().TrimStart() != " ")
            {
                double i = 0;
                try
                {
                    i = Convert.ToDouble(customerTotalGold14.Text.Replace(" ", ""));
                }
                catch { }


                if (i < 0)
                {
                    customerTotalGold14.ForeColor = Color.OrangeRed;
                    gold14L.ForeColor = Color.OrangeRed;
                    gold14L.Text = "له";
                    gold14L.Visible = true;
                }

                else if (Convert.ToDouble(i) > 0)
                {
                    customerTotalGold14.ForeColor = Color.MediumSeaGreen;
                    gold14L.ForeColor = Color.MediumSeaGreen;
                    gold14L.Text = "لـنا";
                    gold14L.Visible = true;

                }

                else
                {
                    customerTotalGold14.ForeColor = Color.FromArgb(254, 199, 32);
                    gold14L.Visible = false;
                }
                addComma(customerTotalGold14);
                if (paymentGold21Txtbox.Text.Trim() == "") { paymentGold21Txtbox.Text = "0"; }
                if (paymentGold18Txtbox.Text.Trim() == "") { paymentGold18Txtbox.Text = "0"; }
                if (paymentGold14Txtbox.Text.Trim() == "") { paymentGold14Txtbox.Text = "0"; }
                if (paymentCashTxtbox.Text.Trim() == "") { paymentCashTxtbox.Text = "0"; }
                paymentCashTxtbox_OnValueChanged(sender, e);
                paymentGold14Txtbox_OnValueChanged(sender, e);
                paymentGold18Txtbox_OnValueChanged(sender, e);
                paymentGold21Txtbox_OnValueChanged(sender, e);
            }
        }

        private void customerNameTxtbox_MouseLeave(object sender, EventArgs e) 
        {
            if (string.IsNullOrEmpty(customerNameTxtbox.Text))
            {
                customerNameTxtbox.Text = "أدخل اسم الصائغ";
            }
        }

        private void customerNameTxtbox_MouseEnter(object sender, EventArgs e)
            {

            if (Equals(customerNameTxtbox.Text, "أدخل اسم الصائغ"))
            {
                customerNameTxtbox.Text = "";
            }
            }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time.Text = String.Format("{0:HH : mm}", DateTime.Now);
            time1.Text = String.Format("{0:tt}", DateTime.Now);
            timer1.Start();
        }

        private void customerPayment_Load(object sender, EventArgs e)
        {
            if (main.IsUsbDeviceConnected(GlobalVar.pid))
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
                        if (GlobalVar.dailyPaymentisMainMax)
                        {
                            maximize_Click(sender, e);
                        }
                        timer1.Start();
                        dateTimePicker1.Value = DateTime.Now.Date;
                        date.Text = String.Format("{0: yyyy / MM / dd}", DateTime.Now);
                        day.Text = String.Format("{0: :dddd}", DateTime.Now);
                        time.Text = String.Format("{0:HH : mm}", DateTime.Now);
                        time1.Text = String.Format("{0:tt}", DateTime.Now);
                        GlobalVar.customerEntryisMainMax = false;
                        paymentdb_Class cus = new paymentdb_Class();
                        IDTxtBox.Text = idLabel(cus.getPaymentsCount());
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        AutoCompleteStringCollection Names = new AutoCompleteStringCollection();
                        try
                        {
                            con.Open();
                            SqlDataReader DataRdr;
                            SqlCommand cmd = new SqlCommand("select customerName from customers where userId = " + GlobalVar.id.ToString(), con);
                            cmd.ExecuteNonQuery();
                            DataRdr = cmd.ExecuteReader();
                            while (DataRdr.Read())
                            {
                                Names.Add(DataRdr.GetString(0));
                            }
                            customerNameTxtbox.AutoCompleteCustomSource = Names;
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            con.Close();
                            MessageBox.Show(ex.Message);
                        }
                        if (GlobalVar.paymentType)
                        {
                            customerPaymentLabel.Text = "استلام";
                            customerPaymentLabel.ForeColor = Color.Gold;
                        }
                        else
                        {
                            customerPaymentLabel.Text = "تسليم";
                            customerPaymentLabel.ForeColor = Color.Tomato;

                        }
                        dataT3.Columns.Add("قبل التحويل", typeof(decimal));
                        dataT3.Columns.Add("بيان التحويل", typeof(string));
                        dataT3.Columns.Add("بعدالتحويل", typeof(decimal));
                        dataGridView3.DataSource = dataT3;
                        dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                        dataGridView3.EnableHeadersVisualStyles = false;
                        foreach (DataGridViewColumn col in dataGridView3.Columns)
                        {
                            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        }
                        paymentGold14Txtbox_OnValueChanged(sender, e);
                        paymentGold18Txtbox_OnValueChanged(sender, e);
                        paymentGold21Txtbox_OnValueChanged(sender, e);
                        paymentCashTxtbox_OnValueChanged(sender, e);
                        after14_OnValueChanged(sender, e);
                        after18_OnValueChanged(sender, e);
                        after21_OnValueChanged(sender, e);
                        afterCash_OnValueChanged(sender, e);
                    }
        }
            else
            {
                MessageBox.Show("ان فلاش الحماية غير متصل بالكمبيوتر , يرجى التأكد من اتصال فلاش الحماية بالكمبيوتر" + Environment.NewLine + "ثم أعد تشغيل البرنامج");
                this.Close();
                main.Show();
            }
        }

        private bool check()
        {
            return (Equals(customerNameTxtbox.Text, "أدخل اسم الصائغ") && Equals(paymentCashTxtbox.Text, "0") && Equals(paymentGold21Txtbox.Text, "0") && Equals(paymentGold14Txtbox.Text, "0") && Equals(paymentGold18Txtbox.Text, "0"));
        }

        private string idLabel(string ii)
        {

            string str = "";
            try {
                int id = Convert.ToInt32(ii);

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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return str = "";
            }
            }

        private void customerPayment_Resize(object sender, EventArgs e)
        {
            close.Location = new Point(0, 0);
            maximize.Location = new Point(34, 0);
            minimize.Location = new Point(78, 0);
            bar.Size = new Size(this.Width - 78, 37);
            bar.Location = new Point(0, 0);
            yellowPanel.Size = new Size(this.Width, 3);
            yellowPanel.Location = new Point(0, 32);
        }

        private void choose_Click(object sender, EventArgs e)
        {
            try
            {
                if (Equals(customerNameTxtbox.Text, "أدخل اسم الصائغ") || (Equals(customerNameTxtbox.Text, "")))
                {

                    MessageBox.Show("!!.. يرجى ادخال اسم الصائغ");

                }
                else
                {
                    choose_customer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void choose_customer()
        {
            if (customerNameTxtbox.TextLength > 1)
            {
                paymentdb_Class cus = new paymentdb_Class();
                cus.customerName1 = customerNameTxtbox.Text;
                int custmId = cus.GetCustomerid();
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TotalCash , Total21 , Total18 , Total14  from customers where customerId = '" + Convert.ToString(custmId) + "' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    customerTotalCash.Text = dt.Rows[0][0].ToString();
                    customerTotalGold21.Text = dt.Rows[0][1].ToString();
                    customerTotalGold18.Text = dt.Rows[0][2].ToString();
                    customerTotalGold14.Text = dt.Rows[0][3].ToString();
                }
                if (customerNameTxtbox.Text == "راس مال")
                {
                    paymentGold14Txtbox.Text = "0";
                    paymentGold18Txtbox.Text = "0";
                    paymentGold21Txtbox.Text = "0";

                }
            }
        }

        private void edit_Click(object sender, EventArgs e)
        {
            GlobalVar.editBackTo = "customerPayment";
            editPayment1 form = new editPayment1();
            form.ShowDialog();
        }

        private void customerNameTxtbox_TextChanged(object sender, EventArgs e)
        {
            choose_customer();
            if (customerNameTxtbox.Text == "راس مال")
            {
                customerTotalCash.Text = Math.Abs(Convert.ToDecimal(customerTotalCash.Text)).ToString();
                customerTotalGold14.Text = Math.Abs(Convert.ToDecimal(customerTotalGold14.Text)).ToString();
                customerTotalGold18.Text = Math.Abs(Convert.ToDecimal(customerTotalGold18.Text)).ToString();
                customerTotalGold21.Text = Math.Abs(Convert.ToDecimal(customerTotalGold21.Text)).ToString();
            }
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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على تسجيل دفعة", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على تسجيل دفعة", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على تسجيل دفعة", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على تسجيل دفعة", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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
                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على تسجيل دفعة", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على تسجيل دفعة", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على تسجيل دفعة", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على تسجيل دفعة", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على تسجيل دفعة", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على تسجيل دفعة", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

        public void addComma(Control cont)
        {
            if (cont.Text != "")
            {
                if (cont.Text.Contains(" "))
                {
                    cont.Text = cont.Text.Replace(" ", "");
                    try
                    {
                        cont.Text = String.Format("{0:N}", Convert.ToDecimal(cont.Text.Replace(" ", "")));
                    }
                    finally { }
                }
                else
                {
                    try
                    {
                        cont.Text = String.Format("{0:N}", Convert.ToDecimal(cont.Text));
                    }
                    catch (Exception x) { MessageBox.Show(x.Message); }
                    finally
                    {
                        cont.Select();
                    }
                }
            }
        }

        public void addCashComma(Control cont)
        {
            if (cont.Text != "")
            {
                if (cont.Text.Contains(" "))
                {
                    cont.Text = cont.Text.Replace(" ", "");
                    try
                    {
                        cont.Text = String.Format("{0:N0}", Convert.ToDecimal(cont.Text));
                    }
                    finally { }
                }
                else
                {
                    try
                    {
                        cont.Text = String.Format("{0:N0}", Convert.ToDecimal(cont.Text));
                    }
                    catch (Exception ex)
                    { MessageBox.Show(ex.Message); }
                    finally
                    {
                        cont.Select();
                    }
                }
            }
        }
 
        private void paymentCashTxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            
        }

        private void customerTotalCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float margin = 40;
			float marginWidth = 30;
			float marginHeight = 0;
			Font ff = new Font("Arial", 8, FontStyle.Bold);
            Font f = new Font("Arial", 14,FontStyle.Bold);
            Font f1 = new Font("Arial", 18, FontStyle.Bold|FontStyle.Underline);
            Font f2 = new Font("Arial", 16, FontStyle.Bold);
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
            string strClock = "الوقت:  " + time.Text.Substring(0, 2) + ":"+time.Text.Substring(time.Text.Length - 2, 2) +  T;
            string strID = "رقم الدفعة: " + IDTxtBox.Text;
            string strType = "وصل استلام دفعة "+customerPaymentLabel.Text;
            string cusName = "اسم الصائغ: " + customerNameTxtbox.Text;
            cus.customerName1 = customerNameTxtbox.Text;
            string cusMob = "";
            if (customerNameTxtbox.Text != "أدخل اسم الصائغ" && customerNameTxtbox.Text != "")
            {
                cusMob = "رقم الموبايل: " + cus.getMobileOfName();
            }
            string user = GlobalVar.userName+ "  "+ "مستلم الدفعة" ;
            string pLabel = "تفاصيل الدفعة";
            string pLabel1 = "حساب الصائغ بعد تسجيل الدفعة";
            string pLabel2 = "ثقتكم سر نجاحنا";
            string label = "حساب الصائغ بعد الدفعة";
            string cashLabel = "الاجور";
            string gold21Label = GlobalVar.gold21Label;
            string gold18Label = GlobalVar.gold18Label;
            string gold14Label = GlobalVar.gold14Label;

            string cash = GlobalVar.currencyLabel + " " + paymentCashTxtbox.Text + " ";
            string gold21 = GlobalVar.gramLabel + " " + paymentGold21Txtbox.Text + " ";
            string gold18 = GlobalVar.gramLabel + " " + paymentGold18Txtbox.Text + " ";
            string gold14 = GlobalVar.gramLabel + " " + paymentGold14Txtbox.Text +" " ;

            string customerCash = cashL.Text + " " + customerTotalCash.Text + " ";
            string customerGold21 = gold21L.Text + " " + customerTotalGold21.Text + " ";
            string customerGold18 = gold18L.Text + " " + customerTotalGold18.Text + " ";
            string customerGold14 = gold14L.Text + " " + customerTotalGold14.Text + " ";


            SizeF sStrR = e.Graphics.MeasureString(strR, ff);
            SizeF sStrDate = e.Graphics.MeasureString(strDate, f);
            SizeF sStrClock= e.Graphics.MeasureString(strClock, f);
            SizeF sStrID= e.Graphics.MeasureString(strID, f);
            SizeF sStrType = e.Graphics.MeasureString(strType, f1);
            SizeF sCusName = e.Graphics.MeasureString(cusName, f2);
            SizeF sCusMob = e.Graphics.MeasureString(cusMob, f2);
            SizeF sPLabel = e.Graphics.MeasureString(pLabel,f1);
            SizeF sPLabel1 = e.Graphics.MeasureString(pLabel1, f1);
            SizeF sPLabel2 = e.Graphics.MeasureString(pLabel2, f1);
            SizeF sUser = e.Graphics.MeasureString(user, f1);
            SizeF sLabel = e.Graphics.MeasureString(label, f1);
            SizeF sCashLabel = e.Graphics.MeasureString(cashLabel, f2);
            SizeF sGold21Label = e.Graphics.MeasureString(gold21Label, f2);
            SizeF sGold18Label = e.Graphics.MeasureString(gold18Label, f2);
            SizeF sGold14Label = e.Graphics.MeasureString(gold14Label, f2);
            SizeF sCash = e.Graphics.MeasureString(cash, f2);
            SizeF sGold21 = e.Graphics.MeasureString(gold21, f2);
            SizeF sGold18 = e.Graphics.MeasureString(gold18, f2);
            SizeF sGold14 = e.Graphics.MeasureString(gold14, f2);
            SizeF sCustomerCash = e.Graphics.MeasureString(customerCash, f2);
            SizeF sCustomerGold21 = e.Graphics.MeasureString(customerGold21, f2);
            SizeF sCustomerGold18 = e.Graphics.MeasureString(customerGold18, f2);
            SizeF sCustomerGold14 = e.Graphics.MeasureString(customerGold14, f2);


            //e.Graphics.DrawImage(Properties.Resources.igoldBlack, margin,2* margin-10 , 100, 22);
            e.Graphics.DrawImage(Properties.Resources.igoldBlack,e.PageBounds.Width-100- margin, margin / 2, 100, 22);
			Font ff1 = new Font("Arial", 24, FontStyle.Bold);
			string logo = GlobalVar.Logo;
			SizeF sLogo = e.Graphics.MeasureString(logo, ff1);
			e.Graphics.DrawString(logo, ff1, Brushes.Black, (e.PageBounds.Width - (2 * marginWidth) - 125) / 2, (marginWidth / 2) + marginHeight);
			//e.Graphics.DrawImage(Properties.Resources.logo,(e.PageBounds.Width-(2*margin)-125)/2  ,margin /2 , 250,80);
			e.Graphics.DrawLine(Pens.Black, margin, e.PageBounds.Height - (2 * margin), e.PageBounds.Width - margin, e.PageBounds.Height - (2 * margin));
            e.Graphics.DrawImage(Properties.Resources.igoldBlack, e.PageBounds.Width - 100 - margin, e.PageBounds.Height - (2 * margin) + 10, 100, 22);
            e.Graphics.DrawString(strR ,ff, Brushes.Black, e.PageBounds.Width - 103 - margin - sStrR.Width, e.PageBounds.Height - (2 * margin) + 15);
            ////////////////
            e.Graphics.DrawString(strID, f, Brushes.Black, e.PageBounds.Width - margin - sStrID.Width, 2 * margin+50);
            e.Graphics.DrawString(strDate,f,Brushes.Black, e.PageBounds.Width - margin - sStrDate.Width, 2 * margin +sStrID.Height +60);
            e.Graphics.DrawString(strClock, f, Brushes.Black, e.PageBounds.Width - margin - sStrClock.Width , 2 * margin +sStrID.Height+ sStrDate.Height + 70);
            e.Graphics.DrawString(strType,f1,Brushes.Black, (e.PageBounds.Width-sStrType.Width+30)/2 ,2 * margin + sStrID.Height + sStrDate.Height + 70);
            //////////////////////////////////////////////
            float preHeight = (2 * margin) + sStrID.Height + sStrDate.Height +sStrClock.Height+ 80;
            float col1width = e.PageBounds.Width / 3;
            e.Graphics.DrawRectangle(Pens.Black, margin, preHeight, e.PageBounds.Width - (2 * margin), 40);
            e.Graphics.DrawLine(Pens.Black, col1width, preHeight, col1width, preHeight + 40);
            e.Graphics.DrawString(cusName, f2, Brushes.Black, e.PageBounds.Width - margin - sCusName.Width, preHeight + 10);
            e.Graphics.DrawString(cusMob, f2, Brushes.Black, margin, preHeight + 10);
            ///////////////////////////////////////////////
            e.Graphics.DrawString(pLabel,f1,Brushes.Black, (e.PageBounds.Width - sPLabel.Width + 30) / 2, preHeight + (2*margin));
            ///////////////////////////////////////////////
            float preHeight1 = (2 * margin) + sStrID.Height + sStrDate.Height + sStrClock.Height + 200;
            float col2width = e.PageBounds.Width / 4;
            e.Graphics.DrawRectangle(Pens.Black, margin, preHeight1, e.PageBounds.Width - (2 * margin), 80);
            e.Graphics.FillRectangle(Brushes.LightGray, margin, preHeight1+2, e.PageBounds.Width - (2 * margin)-2, 40);
            e.Graphics.DrawLine(Pens.Black, col2width, preHeight1, col2width, preHeight1 + 80);
            e.Graphics.DrawLine(Pens.Black, 2*col2width, preHeight1, 2*col2width, preHeight1 + 80);
            e.Graphics.DrawLine(Pens.Black, 3*col2width, preHeight1, 3*col2width, preHeight1 + 80);
            e.Graphics.DrawLine(Pens.Black, margin, preHeight1 + 40, e.PageBounds.Width - margin, preHeight1 + 40);
            ///////////////////////////////////////////////
            e.Graphics.DrawString(gold14Label, f2, Brushes.Black,((col2width+margin-sGold14Label.Width)/2), preHeight1 + 10);
            e.Graphics.DrawString(gold18Label, f2, Brushes.Black, col2width + margin + sGold18Label.Width-33, preHeight1 + 10);
            e.Graphics.DrawString(gold21Label, f2, Brushes.Black, 2*(col2width )+ margin + sGold21Label.Width-33, preHeight1 + 10);
            e.Graphics.DrawString(cashLabel, f2, Brushes.Black, 3 * (col2width) + margin + sCashLabel.Width - 33, preHeight1 + 10);
            ////////////////////////////////////////////////
            e.Graphics.DrawString(gold14, f2, Brushes.Black, margin + (((col2width - margin) - sGold14.Width) / 2), preHeight1 + 50);
            e.Graphics.DrawString(gold18, f2, Brushes.Black, margin + col2width + (((col2width - margin) - sGold18.Width) / 2)-20, preHeight1 + 50);
            e.Graphics.DrawString(gold21, f2, Brushes.Black, margin + (2*col2width) + (((col2width - margin) - sGold21.Width) / 2)-20 , preHeight1 + 50);
            e.Graphics.DrawString(cash, f2, Brushes.Black, margin + (3 * col2width) + (((col2width - margin) - sCash.Width) / 2) - 30 , preHeight1 + 50);
            ////////////////////////////////////////////////
            float preHeight2 = (3 * margin) + sStrID.Height + sStrDate.Height + sStrClock.Height + 330;
            
            e.Graphics.DrawRectangle(Pens.Black, margin, preHeight2, e.PageBounds.Width - (2 * margin), 80);
            e.Graphics.FillRectangle(Brushes.LightGray, margin, preHeight2 + 2, e.PageBounds.Width - (2 * margin) - 2, 40);
            e.Graphics.DrawLine(Pens.Black, col2width, preHeight2, col2width, preHeight2 + 80);
            e.Graphics.DrawLine(Pens.Black, 2 * col2width, preHeight2, 2 * col2width, preHeight2 + 80);
            e.Graphics.DrawLine(Pens.Black, 3 * col2width, preHeight2, 3 * col2width, preHeight2 + 80);
            e.Graphics.DrawLine(Pens.Black, margin, preHeight2 + 40, e.PageBounds.Width - margin, preHeight2 + 40);
            ////////////////////////////////////////////////
            e.Graphics.DrawString(gold14Label, f2, Brushes.Black, ((col2width + margin - sGold14Label.Width) / 2), preHeight2 + 10);
            e.Graphics.DrawString(gold18Label, f2, Brushes.Black, col2width + margin + sGold18Label.Width - 33, preHeight2 + 10);
            e.Graphics.DrawString(gold21Label, f2, Brushes.Black, 2 * (col2width) + margin + sGold21Label.Width - 33, preHeight2 + 10);
            e.Graphics.DrawString(cashLabel, f2, Brushes.Black, 3 * (col2width) + margin + sCashLabel.Width - 33, preHeight2 + 10);
            e.Graphics.DrawString(pLabel1, f1, Brushes.Black, (e.PageBounds.Width - sPLabel1.Width + 30) / 2, preHeight1 + (3 * margin)+10);
            ////////////////////////////////////////////////
            e.Graphics.DrawString(customerGold14, f2, Brushes.Black, margin + (((col2width - margin) - sCustomerGold14.Width) / 2), preHeight2 + 50);
            e.Graphics.DrawString(customerGold18, f2, Brushes.Black, margin + col2width + (((col2width - margin) - sCustomerGold18.Width) / 2) - 20, preHeight2 + 50);
            e.Graphics.DrawString(customerGold21, f2, Brushes.Black, margin + (2 * col2width) + (((col2width - margin) - sCustomerGold21.Width) / 2) - 20, preHeight2 + 50);
            e.Graphics.DrawString(customerCash, f2, Brushes.Black, margin + (3 * col2width) + (((col2width - margin) - sCustomerCash.Width) / 2) - 30, preHeight2 + 50);
            ////////////////////////////////////////////////
            e.Graphics.DrawString(pLabel2, f2, Brushes.Black, (e.PageBounds.Width - sPLabel2.Width + 30) / 2, preHeight2 + (2 * margin)+50);
            e.Graphics.DrawString(user, f2, Brushes.Black, margin, preHeight2 + (2 * margin)+80);
     }

        private void printt_Click(object sender, EventArgs e)
        {
            try
            {
                print_click();
                printDialog1.PrinterSettings.PrinterName= GlobalVar.defaultPrinter;
                printDocument1.Print();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //MessageBox.Show(ExecuteCommandSync("wmic bios get serialnumber"));
            //            التعليمات التي يمكن استخدامها في dongle
            //wmic bios get serialnumber
            //                vol c:
            //            getmac
        }

        private void customerPaymentLabel_Click(object sender, EventArgs e)
        {
            if (customerPaymentLabel.Text == "تسليم")
            {
                customerPaymentLabel.Text = "استلام";
                customerPaymentLabel.ForeColor = Color.Gold;
                GlobalVar.paymentType = true;
            }
            else if (customerPaymentLabel.Text == "استلام")
            {
                customerPaymentLabel.Text = "تسليم";
                customerPaymentLabel.ForeColor = Color.Tomato;
                GlobalVar.paymentType = false;     
            }
            pType.Text = customerPaymentLabel.Text;
            label30.Text= customerPaymentLabel.Text;
            pType.ForeColor = customerPaymentLabel.ForeColor;
            label30.ForeColor = customerPaymentLabel.ForeColor;
            paymentGold14Txtbox_OnValueChanged(sender, e);
            paymentGold18Txtbox_OnValueChanged(sender, e);
            paymentGold21Txtbox_OnValueChanged(sender, e);
            paymentCashTxtbox_OnValueChanged(sender, e);
            after14_OnValueChanged(sender, e);
            after18_OnValueChanged(sender, e);
            after21_OnValueChanged(sender, e);
            afterCash_OnValueChanged(sender, e);
        }

        private void afterCash_OnValueChanged(object sender, EventArgs e)
        {
            decimal kk = 0;
            if (afterCash.Text.TrimStart() != "" && afterCash.Text.TrimStart() != " ")
            {

                try
                {
                    kk = Convert.ToDecimal(afterCash.Text);
                }
                catch { }


                if (kk < 0)
                {
                    afterCash.ForeColor = Color.OrangeRed;
                    label13.ForeColor = Color.OrangeRed;
                    label13.Text = "له";
                    label13.Visible = true;
                    label17.ForeColor = Color.OrangeRed;
                    label18.ForeColor = Color.OrangeRed;
                }

                else if (kk > 0)
                {
                    afterCash.ForeColor = Color.MediumSeaGreen;
                    label13.ForeColor = Color.MediumSeaGreen;
                    label13.Text = "لـنا";
                    label13.Visible = true;
                    label17.ForeColor = Color.MediumSeaGreen;
                    label18.ForeColor = Color.MediumSeaGreen;
                }
                else
                {
                    afterCash.ForeColor = Color.FromArgb(254, 199, 32);
                    label13.Visible = false;
                    label17.ForeColor = Color.MediumSeaGreen;
                    label18.ForeColor = Color.MediumSeaGreen;
                }
                addCashComma(afterCash);
            }
        }

        private void after21_OnValueChanged(object sender, EventArgs e)
        {
            if (after21.Text.Trim().TrimStart() != "" && after21.Text.Trim().TrimStart() != " ")
            {
                double i = 0;
                try
                {
                    i = Convert.ToDouble(after21.Text.Replace(" ", ""));
                }
                catch { }


                if (i < 0)
                {
                    after21.ForeColor = Color.OrangeRed;
                    label11.ForeColor = Color.OrangeRed;
                    label11.Text = "له";
                    label11.Visible = true;
                }

                else if (i > 0)
                {
                    after21.ForeColor = Color.MediumSeaGreen;
                    label11.ForeColor = Color.MediumSeaGreen;
                    label11.Text = "لـنا";
                    label11.Visible = true;
                }
                else
                {
                    after21.ForeColor = Color.FromArgb(254, 199, 32);
                    label11.Visible = false;
                }
                addComma(after21);
            }

        }

        private void after18_OnValueChanged(object sender, EventArgs e)
        {
            if (after18.Text.Trim().TrimStart() != "" && after18.Text.Trim().TrimStart() != " ")
            {
                double i = 0;
                try
                {
                    i = Convert.ToDouble(after18.Text.Replace(" ", ""));
                }
                catch { }


                if (i < 0)
                {
                    after18.ForeColor = Color.OrangeRed;
                    label9.ForeColor = Color.OrangeRed;
                    label9.Text = "له";
                    label9.Visible = true;
                }

                else if (i > 0)
                {
                    after18.ForeColor = Color.MediumSeaGreen;
                    label9.ForeColor = Color.MediumSeaGreen;
                    label9.Text = "لـنا";
                    label9.Visible = true;
                }
                else
                {
                    after18.ForeColor = Color.FromArgb(254, 199, 32);
                    label9.Visible = false;
                }
                addComma(after18);
            }


        }

        private void after14_OnValueChanged(object sender, EventArgs e)
        {
            if (after14.Text.Trim().TrimStart() != "" && after14.Text.Trim().TrimStart() != " ")
            {
                double i = 0;
                try
                {
                    i = Convert.ToDouble(after14.Text.Replace(" ", ""));
                }
                catch { }


                if (i < 0)
                {
                    after14.ForeColor = Color.OrangeRed;
                    label7.ForeColor = Color.OrangeRed;
                    label7.Text = "له";
                    label7.Visible = true;
                }

                else if (i > 0)
                {
                    after14.ForeColor = Color.MediumSeaGreen;
                    label7.ForeColor = Color.MediumSeaGreen;
                    label7.Text = "لـنا";
                    label7.Visible = true;
                }
                else
                {
                    after14.ForeColor = Color.FromArgb(254, 199, 32);
                    label7.Visible = false;
                }
                addComma(after18);
            }


        }

        private void paymentCashTxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                paymentGold21Txtbox.Select();
                paymentGold21Txtbox.Focus();
            }
        }

        private void paymentGold21Txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                paymentGold18Txtbox.Select();
                paymentGold18Txtbox.Focus();
            }
        }

        private void paymentGold18Txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                paymentGold14Txtbox.Select();
                paymentGold14Txtbox.Focus();
            }
        }

        private void customerNameTxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                paymentCashTxtbox.Select();
                paymentCashTxtbox.Focus();
            }
        }

        private void paymentGold14Txtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                save_Click(sender, e);
            }
        }

        private void detailsTxtbox_MouseHover(object sender, EventArgs e)
        {
            if (detailsTxtbox.Text == "اكتب ملاحظات عن الدفعة")
            {
                detailsTxtbox.Text = "";
            }
        }

        private void detailsTxtbox_MouseLeave(object sender, EventArgs e)
        {
            if (detailsTxtbox.Text.Trim() == "")
            {
                detailsTxtbox.Text = "اكتب ملاحظات عن الدفعة";
            }
        }

        private void dataGridView3_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string text = dataGridView3.Columns[1].HeaderText;
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

        public void add3_items(AutoCompleteStringCollection col)
        {
            col.Add("اجور دفعة");
            col.Add("ذهب21 دفعة");
            col.Add("ذهب18 دفعة");
            col.Add("ذهب14 دفعة");
            col.Add("اجور دفعة إلى ذهب21");
            col.Add("اجور دفعة إلى ذهب18");
            col.Add("اجور دفعة إلى ذهب14");
            col.Add("ذهب21 دفعة إلى اجور");
            col.Add("ذهب21 دفعة إلى ذهب18");
            col.Add("ذهب21 دفعة إلى ذهب14");
            col.Add("ذهب18 دفعة إلى اجور");
            col.Add("ذهب18 دفعة إلى ذهب21");
            col.Add("ذهب18 دفعة إلى ذهب14");
            col.Add("ذهب14 دفعة إلى اجور");
            col.Add("ذهب14 دفعة إلى ذهب21");
            col.Add("ذهب14 دفعة إلى ذهب18");
        }

        private void dataGridView3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            decimal cashSum1 = 0;
            decimal gold14Sum1 = 0;
            decimal gold18Sum1 = 0;
            decimal gold21Sum1 = 0;
            decimal RealCash = 0;
            decimal Real21 = 0;
            decimal Real18 = 0;
            decimal Real14 = 0;
            if (dataGridView3.RowCount > 1)
            {
                for (int i = 0; i < dataGridView3.RowCount - 1; i++)
                {
                    if (dataGridView3.Rows[i].Cells[0].Value.ToString().Trim() != "" && dataGridView3.Rows[i].Cells[1].Value.ToString().Trim() != "")
                    {
                        if (dataGridView3.Rows[i].Cells[1].Value.ToString() == GlobalVar.cashLabel)
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(dataGridView3.Rows[i].Cells[0].Value.ToString()));
                            RealCash += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            cashSum1 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString());
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == GlobalVar.gold21Label)
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            Real21 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold21Sum1 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString());
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == GlobalVar.gold18Label)
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            Real18 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold18Sum1 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString());
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == GlobalVar.gold14Label)
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            Real14 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold14Sum1 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString());
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "اجور إلى ذهب21")
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
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "اجور إلى ذهب18")
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
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "اجور إلى ذهب14")
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
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "ذهب21 إلى اجور")
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
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "ذهب21 إلى ذهب18")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.166667)), 2);
                            Real21 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold18Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "ذهب21 إلى ذهب14")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.5)), 2);
                            Real21 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold14Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "ذهب18 إلى اجور")
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
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "ذهب18 إلى ذهب21")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.85714)), 2);
                            Real18 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold21Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "ذهب18 إلى ذهب14")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.285714)), 2);
                            Real18 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold14Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "ذهب14 إلى اجور")
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
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "ذهب14 إلى ذهب21")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.66667)), 2);
                            Real14 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold21Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView3.Rows[i].Cells[1].Value.ToString() == "ذهب14 إلى ذهب18")
                        {
                            dataGridView3.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.77778)), 2);
                            Real14 += Convert.ToDecimal(dataGridView3.Rows[i].Cells[0].Value.ToString());
                            gold18Sum1 += Math.Round(Convert.ToDecimal(dataGridView3.Rows[i].Cells[2].Value.ToString()), 2);
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
                paymentCashTxtbox.Text = cashSum1.ToString();
                paymentGold21Txtbox.Text = gold21Sum1.ToString();
                paymentGold18Txtbox.Text = gold18Sum1.ToString();
                paymentGold14Txtbox.Text = gold14Sum1.ToString();
                //MessageBox.Show(payCash.Text + " " + payGold21.Text + " " + payGold18.Text + " " + payGold14.Text);
            }
        }


    }
}   