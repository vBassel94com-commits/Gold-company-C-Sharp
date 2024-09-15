using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace iGOLD
{
    public partial class editPayment1 : Form
    {
        public editPayment1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private const int cGrip = 16;
        private const int cCaption = 32;
        SqlConnection con = new SqlConnection(GlobalVar.dataBaseLocation);
        decimal rCash = 0;
        decimal rGold21 = 0;
        decimal rGold18 = 0;
        decimal rGold14 = 0;
        decimal cash = 0;
        decimal gold21 = 0;
        decimal gold18 = 0;
        decimal gold14 = 0;
        decimal newRealCash = 0;
        decimal newRealGold21 = 0;
        decimal newRealGold18 = 0;
        decimal newRealGold14 = 0;
        decimal newCash = 0;
        decimal newGold21 = 0;
        decimal newGold18 = 0;
        decimal newGold14 = 0;
        paymentdb_Class pay = new paymentdb_Class();
        Itemdb_Class itm = new Itemdb_Class();
        customerPayment f = new customerPayment();
        int item21Id = 0;
        int item21Count = 0;
        int item18Id = 0;
        int item18Count = 0;
        int item14Id = 0;
        int item14Count = 0;
        decimal pCash = 0;
        decimal p21 = 0;
        decimal p18 = 0;
        decimal p14 = 0;
        decimal rrcash = 0;
        decimal rr21 = 0;
        decimal rr18 = 0;
        decimal rr14 = 0;


        main_iGOLD main = new main_iGOLD();
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        string usrName = GlobalVar.userName;

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
            MessageBox.Show("سيتم اغلاق نافذة تعديل الدفعات");
            this.Close();
        }

        private void close_MouseHover(object sender, EventArgs e)
        {
            this.close.BackColor = GlobalVar.closeHoverColor;
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
            if (W == 1035 && H == 670)
            {
                GlobalVar.editPaymentisMainMax = true;
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
            
                //dataGridView1.Columns[9].Width = 0;
            }
            else
            {
                GlobalVar.editPaymentisMainMax = false;
                this.Close();
                editPayment1 form = new editPayment1();
                form.Show();
                panel2.Width = this.Width - 41;
            }
        }

        private void editPayment_Resize(object sender, EventArgs e)
        {
            close.Location = new Point(4, 0);
            maximize.Location = new Point(38, 0);
            minimize.Location = new Point(72, 0);
            bar.Size = new Size(this.Width, 37);
            bar.Location = new Point(0, 0);
            yellowPanel.Size = new Size(this.Width, 3);
            yellowPanel.Location = new Point(0, 32);
        }

        private void edit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!type.Text.Contains("محذوفة"))
                {
                    edit_Click1();
                }
                else { MessageBox.Show("الدفعة محذوفة , لا يمكن تعديلها"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string addPayment()
        {
            pay.customerName1 = customerNameTxtbox.Text;
            pay.customerId1 = pay.GetCustomerid();

            pay.userId1 = GlobalVar.id;
            if (dataGridView2.RowCount > 1)
            {
                newRealCash = Convert.ToDecimal(realNewCashTxtbox.Text.Trim());
                newRealGold21 = Convert.ToDecimal(realNew21Txtbox.Text.Trim());
                newRealGold18 = Convert.ToDecimal(realNew18Txtbox.Text.Trim());
                newRealGold14 = Convert.ToDecimal(realNew14Txtbox.Text.Trim());
            }
            else
            {
                newRealCash = Convert.ToDecimal(newCashTxtbox.Text.Trim());
                newRealGold21 = Convert.ToDecimal(new21Txtbox.Text.Trim());
                newRealGold18 = Convert.ToDecimal(new18Txtbox.Text.Trim());
                newRealGold14 = Convert.ToDecimal(new14Txtbox.Text.Trim());
            }
            newCash = Convert.ToDecimal(newCashTxtbox.Text.Trim());
            newGold21 = Convert.ToDecimal(new21Txtbox.Text.Trim());
            newGold18 = Convert.ToDecimal(new18Txtbox.Text.Trim());
            newGold14 = Convert.ToDecimal(new14Txtbox.Text.Trim());
            pay.item14Count1 = 0;
            pay.item18Count1 = 0;
            pay.item21Count1 = 0;
            pay.item14Id1 = 0;
            pay.item18Id1 = 0;
            pay.item21Id1 = 0;
            if (newRealGold21 != 0)
            {
                pay.itemName1 = "خشر عيار 21";
                pay.item21Id1 = pay.getItemId();
            }
            if (newRealGold18 != 0)
            {
                pay.itemName1 = "خشر عيار 18";
                pay.item18Id1 = pay.getItemId();
            }
            if (newRealGold14 != 0)
            {
                pay.itemName1 = "خشر عيار 14";
                pay.item14Id1 = pay.getItemId();
            }
            if (fromCustomer.Checked)
            {
                itm.billType1 = "شراء";
                pay.paymentTypeInt1 = -1;
                if (customerNameTxtbox.Text == "راس مال")
                {
                    pay.paymentTypeString1 = "راس مال";
                    pay.paymentTypeInt1 = -1;
                }
                else
                {
                    pay.paymentTypeString1 = "استلام";
                }
                itm.cash1 = newRealCash;
                itm.addCashToFonding();
                if (rGold14 != 0)
                {
                    itm.itemCount1 = 0;
                    itm.itemFees1 = 0;
                    itm.itemCarat1 = 14;
                    itm.itemWeight1 = newRealGold14;
                    itm.itemName1 = "خشر عيار 14";
                    itm.itemId1 = itm.getItemId();
                    itm.buyItem1();
                    itm.itemCarat1 = 14;
                    itm.itemWeight1 = newRealGold14;
                    itm.addGoldToFonding();
                }
                if (rGold18 != 0)
                {
                    itm.itemCount1 = 0;
                    itm.itemFees1 = 0;
                    itm.itemCarat1 = 18;
                    itm.itemWeight1 = newRealGold18;
                    itm.itemName1 = "خشر عيار 18";
                    itm.itemId1 = itm.getItemId();
                    itm.buyItem1();
                    itm.itemCarat1 = 18;
                    itm.itemWeight1 = newRealGold18;
                    itm.addGoldToFonding();
                }
                if (rGold21 != 0)
                {
                    itm.itemCount1 = 0;
                    itm.itemFees1 = 0;
                    itm.itemCarat1 = 21;
                    itm.itemWeight1 = newRealGold21;
                    itm.itemName1 = "خشر عيار 21";
                    itm.itemId1 = itm.getItemId();
                    itm.buyItem1();
                    itm.itemCarat1 = 21;
                    itm.itemWeight1 = newRealGold21;
                    itm.addGoldToFonding();
                }
            }
            else
            {
                itm.billType1 = "بيع";
                pay.paymentTypeInt1 = 1;
                pay.paymentTypeString1 = "تسليم";
                itm.cash1 = newRealCash;
                itm.subCashFromFonding();
                if (rGold14 != 0)
                {
                    itm.itemCount1 = 0;
                    itm.itemFees1 = 0;
                    itm.itemCarat1 = 14;
                    itm.itemWeight1 = newRealGold14;
                    itm.itemName1 = "خشر عيار 14";
                    itm.itemId1 = itm.getItemId();
                    itm.sellItem();
                    itm.itemCarat1 = 14;
                    itm.itemWeight1 = newRealGold14;
                    itm.subGoldFromFonding();
                }
                if (rGold18 != 0)
                {
                    itm.itemCount1 = 0;
                    itm.itemFees1 = 0;
                    itm.itemCarat1 = 18;
                    itm.itemWeight1 = newRealGold18;
                    itm.itemName1 = "خشر عيار 18";
                    itm.itemId1 = itm.getItemId();
                    itm.sellItem();
                    itm.itemCarat1 = 18;
                    itm.itemWeight1 = newRealGold18;
                    itm.subGoldFromFonding();
                }
                if (rGold21 != 0)
                {
                    itm.itemCount1 = 0;
                    itm.itemFees1 = 0;
                    itm.itemCarat1 = 21;
                    itm.itemWeight1 = newRealGold21;
                    itm.itemName1 = "خشر عيار 21";
                    itm.itemId1 = itm.getItemId();
                    itm.sellItem();
                    itm.itemCarat1 = 21;
                    itm.itemWeight1 = newRealGold21;
                    itm.subGoldFromFonding();
                }
            }
            pay.paymentCash1 = newCash;
            pay.payment211 = newGold21;
            pay.payment181 = newGold18;
            pay.payment141 = newGold14;
            pay.realPaymentCash1 = newRealCash;
            pay.realPayment211 = newRealGold21;
            pay.realPayment181 = newRealGold18;
            pay.realPayment141 = newRealGold14;
            string notice = "";
            if (fromCustomer.Checked)
                notice = ("تعديل دقعة استلام من  " + customerNameTxtbox.Text);
            else
                notice = ("تعديل دفعة تسليم إلى  " + customerNameTxtbox.Text);
            pay.paymentNotice1 = notice;
            pay.paymentDateTime1 = dateTimePicker1.Value.Date;
            pay.paymentNo1 = no.Text;
            pay.paymentId1 = Convert.ToInt32(idTxtbox.Text);
            string c1 = pay.updatePayment();
            ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////
            pay.customerName1 = customerNameTxtbox.Text;
            pay.customerId1 = pay.GetCustomerid();
            pay.userId1 = GlobalVar.id;
            pay.paymentId1 = Convert.ToInt32(idTxtbox.Text);
            if (type.Text == "استلام")
                pay.primaryType1 = -1;
            else
                pay.primaryType1 = 1;
            pay.item14Id1 = 0;
            pay.item18Id1 = 0;
            pay.item21Id1 = 0;
            if (newRealGold21 != 0)
            {
                pay.itemName1 = "خشر عيار 21";
                pay.item21Id1 = pay.getItemId();
            }
            if (newRealGold18 != 0)
            {
                pay.itemName1 = "خشر عيار 18";
                pay.item18Id1 = pay.getItemId();
            }
            if (newRealGold14 != 0)
            {
                pay.itemName1 = "خشر عيار 14";
                pay.item14Id1 = pay.getItemId();
            }
            pay.item14Count1 = 0;
            pay.item18Count1 = 0;
            pay.item21Count1 = 0;
            pay.primary14Count1 = 0;
            pay.primary18Count1 = 0;
            pay.primary21Count1 = 0;
            pay.paymentDateTime1 = dateTimePicker1.Value.Date;
            pay.paymentNo1 = no.Text;
            notice = "";
            if (fromCustomer.Checked)
            {
                pay.newType1 = -1;
                notice = ("تعديل دقعة استلام من  " + customerNameTxtbox.Text);
            }
            else
            {
                pay.newType1 = 1;
                notice = ("تعديل دفعة تسليم إلى  " + customerNameTxtbox.Text);
            }
            pay.paymentNotice1 = notice;
            //17
            if (dataGridView2.RowCount > 1)
            {
                newRealCash = Convert.ToDecimal(realNewCashTxtbox.Text.Trim());
                newRealGold21 = Convert.ToDecimal(realNew21Txtbox.Text.Trim());
                newRealGold18 = Convert.ToDecimal(realNew18Txtbox.Text.Trim());
                newRealGold14 = Convert.ToDecimal(realNew14Txtbox.Text.Trim());
            }
            else
            {
                newRealCash = Convert.ToDecimal(newCashTxtbox.Text.Trim());
                newRealGold21 = Convert.ToDecimal(new21Txtbox.Text.Trim());
                newRealGold18 = Convert.ToDecimal(new18Txtbox.Text.Trim());
                newRealGold14 = Convert.ToDecimal(new14Txtbox.Text.Trim());
            }
            cash = 0;
            gold21 = 0;
            gold18 = 0;
            gold14 = 0;
            rCash = 0;
            rGold21 = 0;
            rGold18 = 0;
            rGold14 = 0;
            newCash = Convert.ToDecimal(newCashTxtbox.Text.Trim());
            newGold21 = Convert.ToDecimal(new21Txtbox.Text.Trim());
            newGold18 = Convert.ToDecimal(new18Txtbox.Text.Trim());
            newGold14 = Convert.ToDecimal(new14Txtbox.Text.Trim());
            pay.paymentCash1 = cash;
            pay.payment211 = gold21;
            pay.payment181 = gold18;
            pay.payment141 = gold14;
            pay.realPaymentCash1 = rCash;
            pay.realPayment211 = rGold21;
            pay.realPayment181 = rGold18;
            pay.realPayment141 = rGold14;
            pay.realNewpaymentCash1 = newRealCash;
            pay.realNewpayment211 = newRealGold21;
            pay.realNewpayment181 = newRealGold18;
            pay.realNewpayment141 = newRealGold14;
            pay.newPaymentCash1 = newCash;
            pay.newPayment211 = newGold21;
            pay.newPayment181 = newGold18;
            pay.newPayment141 = newGold14;
            c1 = pay.insertEditPayment();
            ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////
            if (c1 == " تم الحفظ ")
            {
                pay.customerName1 = customerNameTxtbox.Text;
                pay.customerId1 = pay.GetCustomerid();
                pay.paymentCash1 = newCash;
                pay.payment211 = newGold21;
                pay.payment181 = newGold18;
                pay.payment141 = newGold14;
                if (fromCustomer.Checked)
                {
                    pay.paymentTypeInt1 = -1;
                }
                else
                {
                    pay.paymentTypeInt1 = 1;
                }
                pay.addPayment1();
                ///باقي ادخال editpaymentDetails
                if (dataGridView2.RowCount > 1)
                {
                    for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                    {
                        pay.paymentId1 = Convert.ToInt32(idTxtbox.Text);
                        pay.before1 = Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value);
                        pay.after1 = Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value);
                        pay.convert1 = dataGridView2.Rows[i].Cells[1].Value.ToString();
                        pay.insertPaymentDetails();

                        pay.paymentId1 = Convert.ToInt32(idTxtbox.Text);
                        pay.before1 = 0;
                        pay.after1 = 0;
                        pay.convert1 = "اضافة";
                        pay.newBefore1 = Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value);
                        pay.newAfter1 = Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value);
                        pay.newConvert1 = dataGridView2.Rows[i].Cells[1].Value.ToString();
                        pay.insertEditPaymentDetails();
                    }
                }
                else
                {
                    if (newCash != 0)
                    {
                        pay.paymentId1 = Convert.ToInt32(pay.getPaymentId());
                        pay.before1 = newCash;
                        pay.after1 = newCash;
                        pay.convert1 = GlobalVar.cashLabel;
                        pay.insertPaymentDetails();
                        pay.paymentId1 = Convert.ToInt32(idTxtbox.Text);
                        pay.before1 = 0;
                        pay.after1 = 0;
                        pay.convert1 = "اضافة";
                        pay.newBefore1 = newCash;
                        pay.newAfter1 = newCash;
                        pay.newConvert1 = GlobalVar.cashLabel;
                        pay.insertEditPaymentDetails();
                    }
                    if (newGold21 != 0)
                    {
                        pay.paymentId1 = Convert.ToInt32(pay.getPaymentId());
                        pay.before1 = newGold21;
                        pay.after1 = newGold21;
                        pay.convert1 = GlobalVar.gold21Label;
                        pay.insertPaymentDetails();
                        pay.paymentId1 = Convert.ToInt32(idTxtbox.Text);
                        pay.before1 = 0;
                        pay.after1 = 0;
                        pay.convert1 = "اضافة";
                        pay.newBefore1 = newGold21;
                        pay.newAfter1 = newGold21;
                        pay.newConvert1 = GlobalVar.gold21Label;
                        pay.insertEditPaymentDetails();
                    }
                    if (newGold18 != 0)
                    {
                        pay.paymentId1 = Convert.ToInt32(pay.getPaymentId());
                        pay.before1 = newGold18;
                        pay.after1 = newGold18;
                        pay.convert1 = GlobalVar.gold18Label;
                        pay.insertPaymentDetails();
                        pay.paymentId1 = Convert.ToInt32(idTxtbox.Text);
                        pay.before1 = 0;
                        pay.after1 = 0;
                        pay.convert1 = "اضافة";
                        pay.newBefore1 = newGold18;
                        pay.newAfter1 = newGold18;
                        pay.newConvert1 = GlobalVar.gold18Label;
                        pay.insertEditPaymentDetails();
                    }
                    if (newGold14 != 0)
                    {
                        pay.paymentId1 = Convert.ToInt32(pay.getPaymentId());
                        pay.before1 = newGold14;
                        pay.after1 = newGold14;
                        pay.convert1 = GlobalVar.gold14Label;
                        pay.insertPaymentDetails();
                        pay.paymentId1 = Convert.ToInt32(idTxtbox.Text);
                        pay.before1 = 0;
                        pay.after1 = 0;
                        pay.convert1 = "اضافة";
                        pay.newBefore1 = newGold14;
                        pay.newAfter1 = newGold14;
                        pay.newConvert1 = GlobalVar.gold14Label;
                        pay.insertEditPaymentDetails();
                    }
                }
                return "تم تعديل الدفعة";
            }
            else
            {
                return c1;
            }
        }

        private string deletePayment()
        {
            pay.customerName1 = customerNameTxtbox1.Text;
            pay.customerId1 = pay.GetCustomerid();
            pay.userId1 = GlobalVar.id;
            cash = Convert.ToDecimal(paymentCashTxtbox.Text);
            rCash = Convert.ToDecimal(realPaymentCashTxtbox.Text);
            gold21 = Convert.ToDecimal(paymentGold21Txtbox.Text);
            rGold21 = Convert.ToDecimal(realPaymentGold21Txtbox.Text);
            gold18 = Convert.ToDecimal(paymentGold18Txtbox.Text);
            rGold18 = Convert.ToDecimal(realPaymentGold18Txtbox.Text);
            gold14 = Convert.ToDecimal(paymentGold14Txtbox.Text);
            rGold14 = Convert.ToDecimal(realPaymentGold14Txtbox.Text);
            newRealCash = 0;
            newRealGold21 = 0;
            newRealGold18 = 0;
            newRealGold14 = 0;
            newCash = 0;
            newGold21 = 0;
            newGold18 = 0;
            newGold14 = 0;
            pay.item14Count1 = 0;
            pay.item18Count1 = 0;
            pay.item21Count1 = 0;
            pay.item14Id1 = 0;
            pay.item18Id1 = 0;
            pay.item21Id1 = 0;
            if (type.Text=="استلام")
            {
                itm.billType1 = "بيع";
                pay.paymentTypeInt1 = 1;
                if (customerNameTxtbox1.Text == "راس مال")
                {
                    pay.paymentTypeString1 = "راس مال";
                    pay.paymentTypeInt1 = 1;
                }
                else
                {
                    pay.paymentTypeString1 = "تسليم";
                }
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
            else
            {
                itm.billType1 = "شراء";
                pay.paymentTypeInt1 = 1;
                pay.paymentTypeString1 = "استلام";
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
            pay.customerName1 = customerNameTxtbox1.Text;
            pay.customerId1 = pay.GetCustomerid();
            pay.userId1 = GlobalVar.id;
            if (type.Text == "استلام")
            {
                pay.paymentTypeString1 = "تسليم";
                pay.paymentTypeInt1 = 1;
            }
            else
            {
                pay.paymentTypeString1 = "استلام";
                pay.paymentTypeInt1 = -1;
            }
            pay.item14Count1 = 0;
            pay.item18Count1 = 0;
            pay.item21Count1 = 0;
            pay.item14Id1 = 0;
            pay.item18Id1 = 0;
            pay.item21Id1 = 0;
            pay.paymentCash1 = 0;
            pay.payment211 = 0;
            pay.payment181 = 0;
            pay.payment141 = 0;
            pay.realPaymentCash1 = 0;
            pay.realPayment211 = 0;
            pay.realPayment181 = 0;
            pay.realPayment141 = 0;
            pay.paymentNotice1 = "الدفعة محذوفة";
            pay.paymentDateTime1 = dateTimePicker1.Value.Date;
            pay.paymentNo1 = no.Text;
            pay.paymentId1 = Convert.ToInt32(idTxtbox.Text);
            string c1 = pay.updatePayment();

            ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////
            pay.customerName1 = customerNameTxtbox1.Text;
            pay.customerId1 = pay.GetCustomerid();
            pay.userId1 = GlobalVar.id;
            pay.paymentId1 = Convert.ToInt32(idTxtbox.Text);
            if (type.Text == "استلام")
                pay.primaryType1 = -1;
            else
                pay.primaryType1 = 1;
            pay.item14Id1 = 0;
            pay.item18Id1 = 0;
            pay.item21Id1 = 0;
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
            pay.item14Count1 = 0;
            pay.item18Count1 = 0;
            pay.item21Count1 = 0;
            pay.primary14Count1 = 0;
            pay.primary18Count1 = 0;
            pay.primary21Count1 = 0;
            
            pay.paymentDateTime1 = dateTimePicker1.Value.Date;
            if (no.Text.Trim() == "")
                pay.paymentNo1 = "000";
            else
                pay.paymentNo1 = no.Text;
            pay.paymentNotice1 = "دفعة محذوفة";
            if (fromCustomer.Checked)
                pay.newType1 = -1;
            else
                pay.newType1 = 1;
            //17
            
            cash = Convert.ToDecimal(paymentCashTxtbox.Text);
            gold21 = Convert.ToDecimal(paymentGold21Txtbox.Text);
            gold18 = Convert.ToDecimal(paymentGold18Txtbox.Text);
            gold14 = Convert.ToDecimal(paymentGold14Txtbox.Text);
            rCash = Convert.ToDecimal(realPaymentCashTxtbox.Text);
            rGold21 = Convert.ToDecimal(realPaymentGold21Txtbox.Text);
            rGold18 = Convert.ToDecimal(realPaymentGold18Txtbox.Text);
            rGold14 =  Convert.ToDecimal(realPaymentGold14Txtbox.Text);
            pay.paymentCash1 = cash;
            pay.payment211 = gold21;
            pay.payment181 = gold18;
            pay.payment141 = gold14;
            pay.realPaymentCash1 = rCash;
            pay.realPayment211 = rGold21;
            pay.realPayment181 = rGold18;
            pay.realPayment141 = rGold14;
            pay.realNewpaymentCash1 = 0;
            pay.realNewpayment211 = 0;
            pay.realNewpayment181 = 0;
            pay.realNewpayment141 = 0;
            pay.newPaymentCash1 = 0;
            pay.newPayment211 = 0;
            pay.newPayment181 = 0;
            pay.newPayment141 = 0;
            c1=pay.insertEditPayment();
            ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////
            ////////////////////////////////////////////////////
            if (c1 == " تم الحفظ ")
            {
                if (type.Text == "استلام")
                {
                    pay.customerName1 = customerNameTxtbox1.Text;
                    pay.customerId1 = pay.GetCustomerid();
                    pay.paymentCash1 = cash;
                    pay.payment211 = gold21;
                    pay.payment181 = gold18;
                    pay.payment141 = gold14;
                    pay.paymentTypeInt1 = -1;
                }
                else
                {
                    pay.customerName1 = customerNameTxtbox1.Text;
                    pay.customerId1 = pay.GetCustomerid();
                    pay.paymentCash1 = cash;
                    pay.payment211 = gold21;
                    pay.payment181 = gold18;
                    pay.payment141 = gold14;
                    pay.paymentTypeInt1 = 1;
                }
                c1=pay.subPayment1();
                pay.paymentId1 = Convert.ToInt32(idTxtbox.Text);
                pay.deletePaymentDetails();
                ///باقي ادخال editpaymentDetails
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    pay.paymentId1 = Convert.ToInt32(idTxtbox.Text);
                    pay.newBefore1 = 0;
                    pay.newAfter1 = 0;
                    pay.newConvert1 = "حذف";
                    pay.before1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[0].Value);
                    pay.after1 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[2].Value);
                    pay.convert1 = dataGridView1.Rows[i].Cells[1].Value.ToString();
                    pay.insertEditPaymentDetails();
                }
                return "تم حذف الدفعة";
            }
            else
            {
                 return c1;
            }
        }

        private void edit_Click1()
        {
            try {
                if (main.IsConnected())
                {
                    if (deletePayment() == "تم حذف الدفعة")
                    /////////////////////////////////////////////////////////////////////
                    //add_new_payment();
                    { MessageBox.Show(addPayment()); }
                    reset1();
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
                if (main.IsConnected())
                {
                    if (!type.Text.Contains("محذوفة"))
                    {
                        MessageBox.Show(deletePayment());
                        reset();
                    }
                }
                else { MessageBox.Show("الدفعة محذوفة , لايمكن حذفها"); }            
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           reset();
        }
          
        private void toCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (customerNameTxtbox.Text == GlobalVar.cashLabel)
            {
                if (toCustomer.Checked)
                {
                    toCustomer.Checked = false;
                    MessageBox.Show("اسم الزبون (اجور)" + Environment.NewLine + "نوع الدفعة يجب أن يكون استلام فقط " + Environment.NewLine + " لايمكن تغيير نوع الدفعة تسليم");
                    fromCustomer.Checked = true;
                    newCashTxtbox.ForeColor = Color.OrangeRed;
                    newCashTxtbox.HintForeColor = Color.OrangeRed;
                    newCashTxtbox.LineIdleColor = Color.OrangeRed;
                    L5.ForeColor = Color.OrangeRed;
                    L6.ForeColor = Color.OrangeRed;
                }
                else
                {
                    newCashTxtbox.ForeColor = Color.MediumSeaGreen;
                    newCashTxtbox.HintForeColor = Color.MediumSeaGreen;
                    newCashTxtbox.LineIdleColor = Color.MediumSeaGreen;
                    L5.ForeColor = Color.MediumSeaGreen;
                    L6.ForeColor = Color.MediumSeaGreen;
                }
            }
            else if (customerNameTxtbox.Text == "راس مال")
            {
                if (toCustomer.Checked)
                {
                    toCustomer.Checked = false;
                    MessageBox.Show("اسم الزبون (راس مال)" + Environment.NewLine + "نوع الدفعة يجب أن يكون استلام فقط " + Environment.NewLine + " لايمكن تغيير نوع الدفعة تسليم");
                    fromCustomer.Checked = true;
                    paymentCashTxtbox.ForeColor = Color.OrangeRed;
                    paymentCashTxtbox.HintForeColor = Color.OrangeRed;
                    paymentCashTxtbox.LineIdleColor = Color.OrangeRed;
                    L5.ForeColor = Color.OrangeRed;
                    L6.ForeColor = Color.OrangeRed;
                }
                else
                {
                    paymentCashTxtbox.ForeColor = Color.MediumSeaGreen;
                    paymentCashTxtbox.HintForeColor = Color.MediumSeaGreen;
                    paymentCashTxtbox.LineIdleColor = Color.MediumSeaGreen;
                    L5.ForeColor = Color.MediumSeaGreen;
                    L6.ForeColor = Color.MediumSeaGreen;
                }
            }


        }

        public void getPaymentDetails()
        {
            try
            {
                if (idTxtbox.TextLength > 0)
                {
                    dt1.Clear();
                    dt2.Clear();
                    SqlCommand cmd = con.CreateCommand();
                    SqlCommand cmd1 = con.CreateCommand();
                    con.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT paymentCash , payment21 , payment18 , payment14 , customers.customerName , paymentNotice ,paymentTypeInt , payments.userId ,item21Id , item18Id , item14Id , item21Count , item18Count , item14Count,paymentTypeString ,realPaymentCash ,realPayment21,realPayment18,realPayment14,paymentDateTime from payments inner join customers on payments.customerId=customers.customerId where paymentId = @Id ";
                    cmd.Parameters.Add(new SqlParameter("@Id", Convert.ToInt32(idTxtbox.Text)));
                    //cmd.Parameters.Add(new SqlParameter("@uId", Convert.ToInt32(GlobalVar.id)));
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch(Exception ee) { MessageBox.Show(ee.Message);
                    con.Close();
                    }
                    con.Close();
                    con.Open();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "SELECT beforeAmount , convertRole , afterAmount  from paymentDetails where paymentDetails.paymentId = @Id";
                    cmd1.Parameters.Add(new SqlParameter("@Id", Convert.ToInt32(idTxtbox.Text)));
                    //cmd1.Parameters.Add(new SqlParameter("@uId", Convert.ToInt32(GlobalVar.id)));
                    try
                    {
                        cmd1.ExecuteNonQuery();
                    }
                    catch (Exception ee)
                    {
                        con.Close();
                        MessageBox.Show(ee.Message);
                    }
                    con.Close();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    da.Fill(dt);
                    da1.Fill(dt1);
                    da1.Fill(dt2);
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt1;
                        dataGridView2.DataSource = dt2;
                        dataGridView1.Columns[0].HeaderText = "قبل التحويل";
                        dataGridView1.Columns[1].HeaderText = "بيان التحويل";
                        dataGridView1.Columns[2].HeaderText = "بعد التحويل";
                        dataGridView2.Columns[0].HeaderText = "قبل التحويل";
                        dataGridView2.Columns[1].HeaderText = "بيان التحويل";
                        dataGridView2.Columns[2].HeaderText = "بعد التحويل";
                        dataGridView1.Columns[1].Width = 120;
                        dataGridView2.Columns[1].Width = 120;

                        paymentCashTxtbox.Text = dt.Rows[0][0].ToString();
                        paymentGold21Txtbox.Text = dt.Rows[0][1].ToString();
                        paymentGold18Txtbox.Text = dt.Rows[0][2].ToString();
                        paymentGold14Txtbox.Text = dt.Rows[0][3].ToString();
                        newCashTxtbox.Text = dt.Rows[0][0].ToString();
                        new21Txtbox.Text = dt.Rows[0][1].ToString();
                        new18Txtbox.Text = dt.Rows[0][2].ToString();
                        new14Txtbox.Text = dt.Rows[0][3].ToString();
                        pCash = Convert.ToDecimal(dt.Rows[0][0].ToString());
                        p21 = Convert.ToDecimal(dt.Rows[0][1].ToString());
                        p18 = Convert.ToDecimal(dt.Rows[0][2].ToString());
                        p14 = Convert.ToDecimal(dt.Rows[0][3].ToString());
                        realPaymentCashTxtbox.Text = dt.Rows[0][15].ToString();
                        realPaymentGold21Txtbox.Text = dt.Rows[0][16].ToString();
                        realPaymentGold18Txtbox.Text = dt.Rows[0][17].ToString();
                        realPaymentGold14Txtbox.Text = dt.Rows[0][18].ToString();
                        realNewCashTxtbox.Text = dt.Rows[0][15].ToString();
                        realNew21Txtbox.Text = dt.Rows[0][16].ToString();
                        realNew18Txtbox.Text = dt.Rows[0][17].ToString();
                        realNew14Txtbox.Text = dt.Rows[0][18].ToString();
                        rrcash = Convert.ToDecimal(dt.Rows[0][15].ToString());
                        rr21 = Convert.ToDecimal(dt.Rows[0][16].ToString());
                        rr18 = Convert.ToDecimal(dt.Rows[0][17].ToString());
                        rr14 = Convert.ToDecimal(dt.Rows[0][18].ToString());
                        dateTimePicker1.Value = Convert.ToDateTime(dt.Rows[0][19].ToString());
                        customerNameTxtbox.Text = dt.Rows[0][4].ToString();
                        customerNameTxtbox1.Text = dt.Rows[0][4].ToString();
                        detailsTxtbox.Text = dt.Rows[0][5].ToString();
                        type.Text = dt.Rows[0][6].ToString();
                        userId.Text = dt.Rows[0][7].ToString();
                        item21Id = Convert.ToInt32(dt.Rows[0][8].ToString());
                        item18Id = Convert.ToInt32(dt.Rows[0][9].ToString());
                        item14Id = Convert.ToInt32(dt.Rows[0][10].ToString());
                        item21Count = Convert.ToInt32(dt.Rows[0][11].ToString());
                        item18Count = Convert.ToInt32(dt.Rows[0][12].ToString());
                        item14Count = Convert.ToInt32(dt.Rows[0][13].ToString());
                        string item21 = "خشر عيار 21";
                        string item18 = "خشر عيار 18";
                        string item14 = "خشر عيار 14";
                        itm.itemName1 = item21;
                        int i21Id = itm.getItemId();
                        itm.itemName1 = item18;
                        int i18Id = itm.getItemId();
                        itm.itemName1 = item14;
                        int i14Id = itm.getItemId();
                        if (item21Id == i21Id && item18Id == i18Id && item14Id == i14Id)
                        {
                            itemNameTxtBox.Text = "خشر متعدد العيارات";
                        }
                        else if (item21Id == i21Id && item18Id == i18Id)
                        {
                            itemNameTxtBox.Text = "خشر متعدد العيارات";
                        }
                        else if (item21Id == i21Id && item14Id == i14Id)
                        {
                            itemNameTxtBox.Text = "خشر متعدد العيارات";
                        }
                        else if (item14Id == i14Id && item18Id == i18Id)
                        {
                            itemNameTxtBox.Text = "خشر متعدد العيارات";
                        }
                        else
                        {
                            if (item21Id != 0 && item21Id != i21Id && item18Id == 0 && item14Id == 0)
                            {
                                itm.itemId1 = item21Id;
                                itemNameTxtBox.Text = itm.getItemName();
                                paymentCountTxtbox.Text = item21Count.ToString();
                            }
                            else if (item18Id != 0 && item18Id != i18Id && item21Id == 0 && item14Id == 0)
                            {
                                itm.itemId1 = item18Id;
                                itemNameTxtBox.Text = itm.getItemName();
                                paymentCountTxtbox.Text = item18Count.ToString();
                            }
                            else if (item14Id != 0 && item14Id != i14Id && item21Id == 0 && item18Id == 0)
                            {
                                itm.itemId1 = item14Id;
                                itemNameTxtBox.Text = itm.getItemName();
                                paymentCountTxtbox.Text = item14Count.ToString();
                            }

                        }

                        if (dt.Rows[0][6].ToString() == "1")
                        {
                            fromCustomer.Checked = false;
                            toCustomer.Checked = true;
                            type.Text = "تسليم";
                        }
                        else
                        {
                            toCustomer.Checked = false;
                            fromCustomer.Checked = true;
                            type.Text = "استلام";
                        }
                    }
                    else
                    {
                        dt1.Clear();
                        dt2.Clear();
                        reset();
                    }
                    dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                    dataGridView1.EnableHeadersVisualStyles = false;
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            }
            catch
            {
                //MessageBox.Show(ex.Message);
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

        private void fromCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (toCustomer.Checked)
            {
                newCashTxtbox.ForeColor = Color.OrangeRed;
                newCashTxtbox.HintForeColor = Color.OrangeRed;
                newCashTxtbox.LineIdleColor = Color.OrangeRed;
                L5.ForeColor = Color.OrangeRed;
                L6.ForeColor = Color.OrangeRed;
            }
            else if (fromCustomer.Checked)
            {
                newCashTxtbox.ForeColor = Color.MediumSeaGreen;
                newCashTxtbox.HintForeColor = Color.MediumSeaGreen;
                newCashTxtbox.LineIdleColor = Color.MediumSeaGreen;
                L5.ForeColor = Color.MediumSeaGreen;
                L6.ForeColor = Color.MediumSeaGreen;
            }
        }

        private void editPayment1_Load(object sender, EventArgs e)
        {
            int X = this.Location.X;
            int Y = this.Location.Y;
            this.Location = new Point(X, 0);

            if (main.IsUsbDeviceConnected(GlobalVar.pid))
            {
                try {
                    if (GlobalVar.editPaymentisMainMax)
                    {
                        maximize_Click(sender, e);
                    }
                    //dt2.Columns.Add("قبل التحويل", typeof(decimal));
                    //dt2.Columns.Add("بيان التحويل", typeof(string));
                    //dt2.Columns.Add("بعدالتحويل", typeof(decimal));
                    dataGridView2.DataSource = dt2;
                    dataGridView2.Columns[1].Width = 120;

                    dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = GlobalVar.headerDgvColor;
                    dataGridView2.EnableHeadersVisualStyles = false;
                    foreach (DataGridViewColumn col in dataGridView2.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                    idTxtbox.Text = GlobalVar.editPaymentId;
                    getPaymentDetails();
                    try
                    {
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
                        customerNameTxtbox.AutoCompleteCustomSource = names;
                        con.Close();
                    }
                    catch { }
                    }
                catch
                {
                    //MessageBox.Show(ex.Message);
                }
                }
            else
            {
                MessageBox.Show("ان فلاش الحماية غير متصل بالكمبيوتر , يرجى التأكد من اتصال فلاش الحماية بالكمبيوتر" + Environment.NewLine + "ثم أعد تشغيل البرنامج");
                this.Close();
                main.Show();
            }
        }

        private void newCashTxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void newCountTxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        public void search_Click(object sender, EventArgs e)
        {
            dt1.Clear();
            dt2.Clear();
            pCash = 0;
            p21 = 0;
            p18 = 0;
            p14 = 0;
            rCash = 0;
            rr21 = 0;
            rr18 = 0;
            rr14 = 0;
            this.Update();
            getPaymentDetails();
            this.Update();
        }

        private void idTxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            this.Update();

            if (e.KeyCode == Keys.Enter)
            {
                getPaymentDetails();
            }
            this.Update();

        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            decimal cashSum1 = 0;
            decimal gold14Sum1 = 0;
            decimal gold18Sum1 = 0;
            decimal gold21Sum1 = 0;
            decimal RealCash = 0;
            decimal Real21 = 0;
            decimal Real18 = 0;
            decimal Real14 = 0;
            //MessageBox.Show(dataGridView2.RowCount.ToString());
            if (dataGridView2.RowCount > 1)
            {
                for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                {
                    if (dataGridView2.Rows[i].Cells[0].Value.ToString().Trim() != "" && dataGridView2.Rows[i].Cells[1].Value.ToString().Trim() != "")
                    {
                        if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "اجور دفعة")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            RealCash += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            cashSum1 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString());
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب21 دفعة")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            Real21 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold21Sum1 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString());
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب18 دفعة")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            Real18 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold18Sum1 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString());
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب14 دفعة")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            Real14 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold14Sum1 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString());
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "اجور دفعة إلى ذهب21")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) / Convert.ToDecimal(goldPrice.Text)), 2);
                                RealCash += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                                gold21Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "اجور دفعة إلى ذهب18")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.166667)) / Convert.ToDecimal(goldPrice.Text)), 2);
                                RealCash += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                                gold18Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "اجور دفعة إلى ذهب14")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.5)) / Convert.ToDecimal(goldPrice.Text)), 2);

                                RealCash += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                                gold14Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب21 دفعة إلى اجور")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView2.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text)), 2)));
                                Real21 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                                cashSum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب21 دفعة إلى ذهب18")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.166667)), 2);
                            Real21 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold18Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب21 دفعة إلى ذهب14")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.5)), 2);
                            Real21 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold14Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب18 دفعة إلى اجور")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView2.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text) * Convert.ToDecimal(0.85714)), 2)));
                                Real18 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                                cashSum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب18 دفعة إلى ذهب21")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.85714)), 2);
                            Real18 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold21Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب18 دفعة إلى ذهب14")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.285714)), 2);
                            Real18 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold14Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب14 دفعة إلى اجور")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView2.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text) * Convert.ToDecimal(0.666667)), 2)));
                                Real14 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                                cashSum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب14 دفعة إلى ذهب21")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.66667)), 2);
                            Real14 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold21Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب14 دفعة إلى ذهب18")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.77778)), 2);
                            Real14 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold18Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                        }

                        //MessageBox.Show(dataGridView3.Rows[i].Cells[2].Value.ToString());
                        //MessageBox.Show(cashSum1.ToString() + " " + gold21Sum1.ToString() + " " + gold18Sum1.ToString() + " " + gold14Sum1.ToString());
                    }
                }
                //MessageBox.Show(cashSum1.ToString() + " " + gold21Sum1.ToString() + " " + gold18Sum1.ToString() + " " + gold14Sum1.ToString());
                realNewCashTxtbox.Text = RealCash.ToString();
                realNew21Txtbox.Text = Real21.ToString();
                realNew18Txtbox.Text = Real18.ToString();
                realNew14Txtbox.Text = Real14.ToString();
                newCashTxtbox.Text = cashSum1.ToString();
                new21Txtbox.Text = gold21Sum1.ToString();
                new18Txtbox.Text = gold18Sum1.ToString();
                new14Txtbox.Text = gold14Sum1.ToString();
                //MessageBox.Show(payCash.Text + " " + payGold21.Text + " " + payGold18.Text + " " + payGold14.Text);
            }
        }

        public void data2()
        {
            decimal cashSum1 = 0;
            decimal gold14Sum1 = 0;
            decimal gold18Sum1 = 0;
            decimal gold21Sum1 = 0;
            decimal RealCash = 0;
            decimal Real21 = 0;
            decimal Real18 = 0;
            decimal Real14 = 0;
            if (dataGridView2.RowCount > 1)
            {
                for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                {
                    if (dataGridView2.Rows[i].Cells[0].Value.ToString().Trim() != "" && dataGridView2.Rows[i].Cells[1].Value.ToString().Trim() != "")
                    {
                        if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "اجور دفعة")
                        {
                            
                            dataGridView2.Rows[i].Cells[2].Value = Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            RealCash += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            cashSum1 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString());
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب21 دفعة")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            Real21 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold21Sum1 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString());
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب18 دفعة")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            Real18 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold18Sum1 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString());
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب14 دفعة")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            Real14 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold14Sum1 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString());
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "اجور دفعة إلى ذهب21")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) / Convert.ToDecimal(goldPrice.Text)), 2);
                                RealCash += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                                gold21Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "اجور دفعة إلى ذهب18")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.166667)) / Convert.ToDecimal(goldPrice.Text)), 2);
                                RealCash += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                                gold18Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "اجور دفعة إلى ذهب14")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.5)) / Convert.ToDecimal(goldPrice.Text)), 2);

                                RealCash += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                                gold14Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب21 دفعة إلى اجور")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView2.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text)), 2)));
                                Real21 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                                cashSum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب21 دفعة إلى ذهب18")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.166667)), 2);
                            Real21 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold18Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب21 دفعة إلى ذهب14")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.5)), 2);
                            Real21 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold14Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب18 دفعة إلى اجور")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView2.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text) * Convert.ToDecimal(0.85714)), 2)));
                                Real18 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                                cashSum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب18 دفعة إلى ذهب21")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.85714)), 2);
                            Real18 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold21Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب18 دفعة إلى ذهب14")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(1.285714)), 2);
                            Real18 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold14Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب14 دفعة إلى اجور")
                        {
                            if (goldPrice.Text.Trim() != "")
                            {
                                dataGridView2.Rows[i].Cells[2].Value = Convert.ToDecimal(Convert.ToInt64(Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(goldPrice.Text) * Convert.ToDecimal(0.666667)), 2)));
                                Real14 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                                cashSum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                            }
                            else
                            {
                                MessageBox.Show("ادخل سعر غرام ذهب21");
                                goldPrice.Select();
                                goldPrice.Focus();
                            }
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب14 دفعة إلى ذهب21")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.66667)), 2);
                            Real14 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold21Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                        }
                        else if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "ذهب14 دفعة إلى ذهب18")
                        {
                            dataGridView2.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString()) * Convert.ToDecimal(0.77778)), 2);
                            Real14 += Convert.ToDecimal(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            gold18Sum1 += Math.Round(Convert.ToDecimal(dataGridView2.Rows[i].Cells[2].Value.ToString()), 2);
                        }

                        //MessageBox.Show(dataGridView3.Rows[i].Cells[2].Value.ToString());
                        //MessageBox.Show(cashSum1.ToString() + " " + gold21Sum1.ToString() + " " + gold18Sum1.ToString() + " " + gold14Sum1.ToString());
                    }
                }
                //MessageBox.Show(cashSum1.ToString() + " " + gold21Sum1.ToString() + " " + gold18Sum1.ToString() + " " + gold14Sum1.ToString());
                realNewCashTxtbox.Text = RealCash.ToString();
                realNew21Txtbox.Text = Real21.ToString();
                realNew18Txtbox.Text = Real18.ToString();
                realNew14Txtbox.Text = Real14.ToString();
                newCashTxtbox.Text = cashSum1.ToString();
                new21Txtbox.Text = gold21Sum1.ToString();
                new18Txtbox.Text = gold18Sum1.ToString();
                new14Txtbox.Text = gold14Sum1.ToString();
                //MessageBox.Show(payCash.Text + " " + payGold21.Text + " " + payGold18.Text + " " + payGold14.Text);
            }
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            string text = dataGridView2.Columns[1].HeaderText;
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

        public void reset()
        {
            this.Update();
            //idTxtbox.Text = "";
            no.Text = "000";
            dt1.Clear();
            dt2.Clear();
            pCash = 0;
            p21 = 0;
            p18 = 0;
            p14 = 0;
            rrcash = 0;
            rr21 = 0;
            rr18 = 0;
            rr14 = 0;
            GlobalVar.editPaymentId = "";
            detailsTxtbox.Text = "اكتب ملاحظات عن الدفعة";
            goldPrice.Text = "";
            dateTimePicker1.Value = DateTime.Now.Date;
            itemNameTxtBox.Text = "اسم المصاغ";
            customerNameTxtbox.Text = "اسم الصائغ";
            paymentCashTxtbox.Text = "0";
            paymentGold21Txtbox.Text = "0";
            paymentGold18Txtbox.Text = "0";
            paymentGold14Txtbox.Text = "0";
            new14Txtbox.Text = "0";
            new18Txtbox.Text = "0";
            new21Txtbox.Text = "0";
            newCashTxtbox.Text = "0";
            newCountTxtbox.Text = "0";
            realPaymentCashTxtbox.Text = "0";
            realPaymentGold21Txtbox.Text = "0";
            realPaymentGold18Txtbox.Text = "0";
            realPaymentGold14Txtbox.Text = "0";
            realNew14Txtbox.Text = "0";
            realNew18Txtbox.Text = "0";
            realNew21Txtbox.Text = "0";
            realNewCashTxtbox.Text = "0";
            newCountTxtbox.Text = "0";
            toCustomer.Checked = false;
            fromCustomer.Checked = false;
            type.Text = "";
            this.Update();
            paymentCashTxtbox.Update();
            paymentGold21Txtbox.Update();
            paymentGold18Txtbox.Update();
            paymentGold14Txtbox.Update();
            realPaymentCashTxtbox.Update();
            realPaymentGold21Txtbox.Update();
            realPaymentGold18Txtbox.Update();
            paymentGold14Txtbox.Update();
        }

        public void reset1()
        {
            this.Update();
            idTxtbox.Text = "";
            no.Text = "000";
            dt1.Clear();
            dt2.Clear();
            pCash = 0;
            p21 = 0;
            p18 = 0;
            p14 = 0;
            rrcash = 0;
            rr21 = 0;
            rr18 = 0;
            rr14 = 0;
            GlobalVar.editPaymentId = "";
            detailsTxtbox.Text = "اكتب ملاحظات عن الدفعة";
            goldPrice.Text = "";
            dateTimePicker1.Value = DateTime.Now.Date;
            itemNameTxtBox.Text = "اسم المصاغ";
            customerNameTxtbox.Text = "اسم الصائغ";
            paymentCashTxtbox.Text = "0";
            paymentGold21Txtbox.Text = "0";
            paymentGold18Txtbox.Text = "0";
            paymentGold14Txtbox.Text = "0";
            new14Txtbox.Text = "0";
            new18Txtbox.Text = "0";
            new21Txtbox.Text = "0";
            newCashTxtbox.Text = "0";
            newCountTxtbox.Text = "0";
            realPaymentCashTxtbox.Text = "0";
            realPaymentGold21Txtbox.Text = "0";
            realPaymentGold18Txtbox.Text = "0";
            realPaymentGold14Txtbox.Text = "0";
            realNew14Txtbox.Text = "0";
            realNew18Txtbox.Text = "0";
            realNew21Txtbox.Text = "0";
            realNewCashTxtbox.Text = "0";
            newCountTxtbox.Text = "0";
            toCustomer.Checked = false;
            fromCustomer.Checked = false;
            type.Text = "";
            this.Update();
            paymentCashTxtbox.Update();
            paymentGold21Txtbox.Update();
            paymentGold18Txtbox.Update();
            paymentGold14Txtbox.Update();
            realPaymentCashTxtbox.Update();
            realPaymentGold21Txtbox.Update();
            realPaymentGold18Txtbox.Update();
            paymentGold14Txtbox.Update();
        }

        private void paymentCashTxtbox_OnValueChanged(object sender, EventArgs e)
        {
            this.Update();
            getPaymentDetails();
            this.Update();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getPaymentDetails();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            getPaymentDetails();
        }

        private void idTxtbox_TextChanged(object sender, EventArgs e)
        {
            //getPaymentDetails();
        }

        private void deletePay_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex3 = dataGridView2.CurrentCell.RowIndex;
                dataGridView2.Rows.RemoveAt(rowIndex3);
                data2();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView2_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            data2();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete_Click(sender, e);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            edit_Click(sender, e);
        }
    }
}
