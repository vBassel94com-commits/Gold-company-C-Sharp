using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace iGOLD
{
    public partial class itemEntry1 : Form
    {
        public itemEntry1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private const int cGrip = 16;
        private const int cCaption = 32;
        Itemdb_Class itm = new Itemdb_Class();
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
            DialogResult result = MessageBox.Show("   هل أنت متأكد بالخروج من ادخال بيانات مصاغ  ", "الخروج", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
            }
            else
            {
                if (GlobalVar.status_value)
                {
                    MessageBox.Show("سيتم اغلاق البرنامج");
                    GlobalVar.status_value = false;
                    main.setBackUp();

                    Application.Exit();
                }
            }
        }

        private void close_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox1.BackColor =GlobalVar.closeHoverColor;
        }

        private void close_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox1.BackColor = GlobalVar.leaveColor;
        }

        private void close_MouseEnter(object sender, EventArgs e)
        {
            this.pictureBox1.BackColor =GlobalVar.closeHoverColor;
        }

        private void minimize_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox3.BackColor = GlobalVar.minMaxHoverColor;
        }

        private void minimize_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox3.BackColor = GlobalVar.leaveColor;
        }

        private void minimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void maximize_MouseHover(object sender, EventArgs e)
        {
            this.pictureBox2.BackColor = GlobalVar.minMaxHoverColor;
        }

        private void maximize_MouseLeave(object sender, EventArgs e)
        {
            this.pictureBox2.BackColor = GlobalVar.leaveColor;
        }

        private void maximize_Click(object sender, EventArgs e)
        {
            Maximize_Click1();
        }

        public void Maximize_Click1()
        {
            GlobalVar.item = itemNameTxtbox.Text;
            int W = this.Width;
            int H = this.Height;
            int screenW = Screen.PrimaryScreen.Bounds.Width;
            int screenH = Screen.PrimaryScreen.Bounds.Height;
            if (W == 646 && H == 500)
            {
                this.SetDesktopLocation(0, 0);
                this.Size = new Size(screenW, screenH-40);
                pictureBox1.Location = new Point(2, 0);
                pictureBox2.Location = new Point(36, 0);
                pictureBox3.Location = new Point(70, 0);
                bar.Size = new Size(screenW - 95, 37);
                bar.Location = new Point(0, 0);
                yellowPanel.Size = new Size(screenW, 3);
                yellowPanel.Location = new Point(0, 32);
                GlobalVar.itemEntryisMainMax = true;
                itemNameTxtbox.Text = GlobalVar.item;
                panel2.Width = this.Width - 41;
                panel2.Height = this.Height - 37;
                panelSlide.Height = this.Height - 37;
                main_Gold1.Height = panelSlide.Height / 11;
                customerEntry1.Height = panelSlide.Height / 11;
                itemEntry11.Height = panelSlide.Height / 11;
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
                itemEntry1 form = new itemEntry1();
                form.Show();
                GlobalVar.itemEntryisMainMax=false;
                form.itemNameTxtbox.Text = GlobalVar.item;
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

        private void save_Click(object sender, EventArgs e)
        {
            save_Click1();
            itemNameTxtbox.Select();
            itemNameTxtbox.Focus();
        }

        public void save_Click1()
        {
            if (Equals(itemNameTxtbox.Text, "أدخل اسم المصاغ") && Equals(weightTxtbox.Text, "0") && Equals(feesTxtbox.Text, "0") && Equals(itemCountTxtbox.Text, "0"))
            {

                MessageBox.Show("!!.. يرجى ادخال بيانات المصاغ");

            }

            else if (Equals(itemNameTxtbox.Text, "أدخل اسم المصاغ") || Equals(itemNameTxtbox.Text, ""))
            {

                MessageBox.Show("!!.. يرجى ادخال اسم المصاغ");

            }


            else if (!carat21.Checked && !carat18.Checked && !carat14.Checked)
            {
                MessageBox.Show("!!.. يرجى اختيار عيار المصاغ");
            }

            else
            {
                if (itemCountTxtbox.Text == "") { itemCountTxtbox.Text = "0"; }
                if (feesTxtbox.Text == "") { feesTxtbox.Text = "0"; }
                if (weightTxtbox.Text == "") { weightTxtbox.Text = "0"; }

                string itemName = itemNameTxtbox.Text.TrimEnd();

                decimal weight = Convert.ToDecimal(weightTxtbox.Text);

                decimal fees = Convert.ToDecimal(feesTxtbox.Text);

                int carat = 0;

                int count = Convert.ToInt32(itemCountTxtbox.Text);

                int id = Convert.ToInt32(IDTxtBox.Text);

                itm.paymentCash1 = 0;
                itm.openWeight = weight;
                itm.openCount = count;
                itm.openFees = fees;
                itm.itemWeight1 = weight;
                itm.itemFees1 = fees;
                itm.itemCount1 = count;
                itm.paymentTypeInt1 = -1;
                itm.paymentTypeString1 = "راس مال";
                itm.userId1 = GlobalVar.id;
                itm.customerName1 = "راس مال";
                itm.customerId1 = itm.GetCustomerid();
                itm.paymentDateTime1 = DateTime.Now.Date;

                if (carat21.Checked)
                {
                    carat = 21;
                    itm.paymentNotice1 = "زيادة راس مال ذهب21 (" + itemName + ")";
                    if (itemName.Contains("عيار"))
                    {
                        MessageBox.Show("لا داعي لكتابة عيار المصاغ , قم بتحديده من الخانات بالاسفل");
                        int index = itemName.IndexOf("عيار");
                        if (index >= 0)
                        {
                            itemNameTxtbox.Text = itemName.Remove(index - 1);
                        }
                    }
                    else
                    {
                        itm.itemName1 = itemName.Trim() + " عيار " + Convert.ToString(carat);
                    }
                    itm.payment141 = 0;
                    itm.payment181 = 0;
                    itm.payment211 = weight;
                    itm.itemCarat1 = carat;

                    if (itm.checkCustomerExist() == "true")
                    {
                        bool d1 = itm.checkItemCaratExist();
                        if (d1)
                        {
                            itm.item14Id = 0;
                            itm.item18Id = 0;
                            itm.item21Id = Convert.ToInt32(itm.getItemId1()) + 1;
                            itm.insertItem21();
                            //MessageBox.Show(itm.insertItem21());
                            IDTxtBox.Text = idLabel(itm.getItemsCount());
                        }
                        else
                        {
                            string aa = itemName.Trim() + " عيار " + Convert.ToString(carat);
                            //MessageBox.Show(aa + " موجود مسبقا ");
                            System.Threading.Thread.Sleep(10);
                            this.Close();
                            editItem form = new editItem();
                            //form.Show();
                            itm.itemName1 = aa;
                            itm.itemId1 = itm.getItemId();
                            form.itemWeightTxtbox.Text = itm.getItemWeight().ToString();
                            form.newWeightTextbox.Text = (weight + itm.getItemWeight()).ToString();
                            form.itemNameTxtbox.Text = aa;
                            form.newFeesTextbox.Text = fees.ToString();
                            itm.itemName1 = aa;
                            form.idTxtbox.Text = idLabel(Convert.ToString(itm.getItemId()));
                            itm.itemName1 = aa;
                            itm.itemId1 = itm.getItemId();
                            form.itemCountTxtbox.Text = itm.getItemCount().ToString();
                            form.newCountTextbox.Text = (count + itm.getItemCount()).ToString();
                            form.carat21.Checked = true;
                            form.edit1();
                        }
                    }
                    else
                    {
                        MessageBox.Show(" قم باضافة صائغ باسم (راس مال) في نافذة اضافة صائغ");
                        this.Close();
                        System.Threading.Thread.Sleep(10);
                        customerEntry1 form = new customerEntry1();
                        form.nameTxtbox.Text = "راس مال";
                        form.mobileTxtbox.Text = "0900000001";
                        form.save_Click1();

                        bool d1 = itm.checkItemCaratExist();
                        if (d1)
                        {
                            itm.item14Id = 0;
                            itm.item18Id = 0;
                            itm.item21Id = Convert.ToInt32(itm.getItemId1()) + 1;
                            itm.insertItem21();
                            //MessageBox.Show(itm.insertItem21());
                            IDTxtBox.Text = idLabel(itm.getItemsCount());
                        }
                        else
                        {
                            string aa = itemName.Trim() + " عيار " + Convert.ToString(carat);
                            //MessageBox.Show(aa + " موجود مسبقا ");
                            System.Threading.Thread.Sleep(10);
                            this.Close();
                            editItem form1 = new editItem();
                            //form.Show();
                            itm.itemName1 = aa;
                            itm.itemId1 = itm.getItemId();
                            form1.itemWeightTxtbox.Text = itm.getItemWeight().ToString();
                            form1.newWeightTextbox.Text = (weight + itm.getItemWeight()).ToString();
                            form1.itemNameTxtbox.Text = aa;
                            form1.newFeesTextbox.Text = fees.ToString();
                            itm.itemName1 = aa;
                            form1.idTxtbox.Text = idLabel(Convert.ToString(itm.getItemId()));
                            itm.itemName1 = aa;
                            itm.itemId1 = itm.getItemId();
                            form1.itemCountTxtbox.Text = itm.getItemCount().ToString();
                            form1.newCountTextbox.Text = (count + itm.getItemCount()).ToString();
                            form1.carat21.Checked = true;
                            form1.edit1();
                        }
                    }
                }
                else if (carat18.Checked)
                {
                    carat = 18;
                    itm.paymentNotice1 = "زيادة راس مال ذهب18 (" + itemName + ")";
                    if (itemName.Contains("عيار"))
                    {
                        MessageBox.Show("لا داعي لكتابة عيار المصاغ , قم بتحديده من الخانات بالاسفل");
                        int index = itemName.IndexOf("عيار");
                        if (index >= 0)
                        {
                            itemNameTxtbox.Text = itemName.Remove(index - 1);
                        }
                    }
                    else
                    {
                        itm.itemName1 = itemName.Trim() + " عيار " + Convert.ToString(carat);
                    }
                    itm.payment141 = 0;
                    itm.payment181 = weight;
                    itm.payment211 = 0;
                    itm.itemCarat1 = carat;

                    if (itm.checkCustomerExist() == "true")
                    {
                        bool d1 = itm.checkItemCaratExist();
                        if (d1)
                        {
                            itm.item14Id = 0;
                            itm.item18Id = Convert.ToInt32(itm.getItemId1()) + 1;
                            itm.item21Id = 0;
                            itm.insertItem18();
                            //MessageBox.Show(itm.insertItem18());
                            IDTxtBox.Text = idLabel(itm.getItemsCount());
                        }
                        else
                        {
                            string aa = itemName.Trim() + " عيار " + Convert.ToString(carat);
                            //MessageBox.Show(aa + " موجود مسبقا ");
                            System.Threading.Thread.Sleep(10);
                            this.Close();
                            editItem form = new editItem();
                            //form.Show();
                            itm.itemName1 = aa;
                            itm.itemId1 = itm.getItemId();
                            form.itemWeightTxtbox.Text = itm.getItemWeight().ToString();
                            form.newWeightTextbox.Text = (weight + itm.getItemWeight()).ToString();
                            form.itemNameTxtbox.Text = aa;
                            form.newFeesTextbox.Text = fees.ToString();
                            itm.itemName1 = aa;
                            form.idTxtbox.Text = idLabel(Convert.ToString(itm.getItemId()));
                            itm.itemName1 = aa;
                            itm.itemId1 = itm.getItemId();
                            form.itemCountTxtbox.Text = itm.getItemCount().ToString();
                            form.newCountTextbox.Text = (count + itm.getItemCount()).ToString();
                            form.carat18.Checked = true;
                            form.edit1();
                        }
                    }
                    else
                    {
                        MessageBox.Show(" قم باضافة صائغ باسم (راس مال) في نافذة اضافة صائغ");
                        this.Close();
                        System.Threading.Thread.Sleep(1000);
                        customerEntry1 form = new customerEntry1();
                        form.Show();
                        form.nameTxtbox.Text = "راس مال";
                        form.mobileTxtbox.Text = "0900000001";
                    }
                }
                else if (carat14.Checked)
                {
                    carat = 14;
                    itm.paymentNotice1 = "زيادة راس مال ذهب14 (" + itemName + ")";
                    if (itemName.Contains("عيار"))
                    {
                        MessageBox.Show("لا داعي لكتابة عيار المصاغ , قم بتحديده من الخانات بالاسفل");
                        int index = itemName.IndexOf("عيار");
                        if (index >= 0)
                        {
                            itemNameTxtbox.Text = itemName.Remove(index - 1);
                        }
                    }
                    else
                    {
                        itm.itemName1 = itemName.Trim() + " عيار " + Convert.ToString(carat);
                    }
                    itm.payment141 = weight;
                    itm.payment181 = 0;
                    itm.payment211 = 0;
                    itm.itemCarat1 = carat;

                    if (itm.checkCustomerExist() == "true")
                    {
                        bool d1 = itm.checkItemCaratExist();
                        if (d1)
                        {
                            itm.item14Id = Convert.ToInt32(itm.getItemId1()) + 1;
                            itm.item18Id = 0;
                            itm.item21Id = 0;
                            itm.insertItem14();
                            //MessageBox.Show(itm.insertItem14());
                            IDTxtBox.Text = idLabel(itm.getItemsCount());
                        }
                        else
                        {
                            string aa = itemName.Trim() + " عيار " + Convert.ToString(carat);
                            //MessageBox.Show(aa + " موجود مسبقا ");
                            System.Threading.Thread.Sleep(1000);
                             this.Close();
                            editItem form = new editItem();
                            //form.Show();
                            itm.itemName1 = aa;
                            itm.itemId1 = itm.getItemId();
                            form.itemWeightTxtbox.Text = itm.getItemWeight().ToString();
                            form.newWeightTextbox.Text = (weight + itm.getItemWeight()).ToString();
                            form.itemNameTxtbox.Text = aa;
                            form.newFeesTextbox.Text = fees.ToString();
                            itm.itemName1 = aa;
                            form.idTxtbox.Text = idLabel(Convert.ToString(itm.getItemId()));
                            itm.itemName1 = aa;
                            itm.itemId1 = itm.getItemId();
                            form.itemCountTxtbox.Text = itm.getItemCount().ToString();
                            form.newCountTextbox.Text = (count + itm.getItemCount()).ToString();
                            form.carat14.Checked = true;
                            form.edit1();
                        }
                    }
                    else
                    {
                        MessageBox.Show(" قم باضافة صائغ باسم (راس مال) في نافذة اضافة صائغ");
                        this.Close();
                        System.Threading.Thread.Sleep(1000);
                        customerEntry1 form = new customerEntry1();
                        form.Show();
                        form.nameTxtbox.Text = "راس مال";
                        form.mobileTxtbox.Text = "0900000001";
                    }
                }

                itemNameTxtbox.Text = "أدخل اسم المصاغ";
                weightTxtbox.Text = "0";
                itemCountTxtbox.Text = "0";
                feesTxtbox.Text = "0";
                carat14.Checked = false;
                carat18.Checked = false;
                carat21.Checked = false;
            }

        }

        private void cancel_Click(object sender, EventArgs e)
        {
            itemNameTxtbox.Text = "أدخل اسم المصاغ";
            weightTxtbox.Text = "0";
            feesTxtbox.Text = "0";
            itemCountTxtbox.Text = "0";
            carat21.Checked = false;
            carat18.Checked = false;
            carat14.Checked = false;
        }

        private void itemNameTxtbox_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(itemNameTxtbox.Text))
            {
                itemNameTxtbox.Text = "أدخل اسم المصاغ";
            }

        }

        private void itemNameTxtbox_MouseEnter(object sender, EventArgs e)
        {

            if (Equals(itemNameTxtbox.Text, "أدخل اسم المصاغ"))
            {
                itemNameTxtbox.Text = "";
            }
        }

        private void weightTxtbox_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(weightTxtbox.Text))
            {
                weightTxtbox.Text = "0";
            }
            weightTxtbox.LineIdleColor = GlobalVar.menuHoverColor;
        }

        private void weightTxtbox_MouseEnter(object sender, EventArgs e)
        {

            if (Equals(weightTxtbox.Text, "0"))
            {
                weightTxtbox.Text = "";
            }
        }

        private void weightTxtbox_OnValueChanged(object sender, EventArgs e)
        {
            if (weightTxtbox.Text.Trim() != "")

            {

                try

                {

                    decimal i = Convert.ToDecimal(weightTxtbox.Text.Trim());

                }

                catch
                {

                    MessageBox.Show("يرجى ادخال أرقام فقط");

                    weightTxtbox.Text = "0";

                }

            }
        }
 
        private void feesTxtbox_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(feesTxtbox.Text))
            {
                feesTxtbox.Text = "0";
            }
            feesTxtbox.LineIdleColor = GlobalVar.menuHoverColor;
        }

        private void feesTxtbox_MouseEnter(object sender, EventArgs e)
        {

            if (Equals(feesTxtbox.Text, "0"))
            {
                feesTxtbox.Text = "";
            }
        }

        private void feesTxtbox_OnValueChanged(object sender, EventArgs e)
        {
            if (feesTxtbox.Text.Trim() != "")

            {

                try

                {

                    int i = Convert.ToInt32(feesTxtbox.Text.Trim());

                }

                catch
                {

                    MessageBox.Show("يرجى ادخال أرقام فقط");

                    feesTxtbox.Text = "0";

                }
            }
        }

        private void itemCountTxtbox_MouseLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(itemCountTxtbox.Text))
            {
                itemCountTxtbox.Text = "0";
            }
            itemCountTxtbox.LineIdleColor = GlobalVar.menuHoverColor;
        }

        private void itemCountTxtbox_MouseEnter(object sender, EventArgs e)
        {

            if (Equals(itemCountTxtbox.Text, "0"))
            {
                itemCountTxtbox.Text = "";
            }
        }

        private void itemCountTxtbox_OnValueChanged(object sender, EventArgs e)
        {
            if (itemCountTxtbox.Text.Trim() != "")

            {

                try

                {

                    int i = Convert.ToInt32(itemCountTxtbox.Text.Trim());
                    
                }

                catch
                {

                    MessageBox.Show("يرجى ادخال أرقام فقط");

                    itemCountTxtbox.Text = "0";
                    
                }
                
            }
            if (itemCountTxtbox.Text == "0" || itemCountTxtbox.Text == "" )
            {
                type.Text = "ادخال مصاغ بالغرام";
                L4.Text = "أجور صياغة الغرام:";
            }
            else
            {
                type.Text = "ادخال مصاغ بالقطعة";
                L4.Text = "أجور صياغة القطعة:";
            }
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "";
            }
            }

        private void itemEntry_Resize(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(2, 0);
            pictureBox2.Location = new Point(36, 0);
            pictureBox3.Location = new Point(70, 0);
            bar.Size = new Size(this.Width - 95, 37);
            bar.Location = new Point(0, 0);
            yellowPanel.Size = new Size(this.Width, 3);
            yellowPanel.Location = new Point(0, 32);
        }

        private void edit_Click(object sender, EventArgs e)
        {
  this.Close();
            editItem form = new editItem();
            form.Show();
        }

        private void itemEntry_Load(object sender, EventArgs e)
        {
            if (main.IsUsbDeviceConnected(GlobalVar.pid))
            {
                try
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
                        if (GlobalVar.itemEntryisMainMax)
                        {
                            maximize_Click(sender, e);
                        }
                        Itemdb_Class cus = new Itemdb_Class();
                        IDTxtBox.Text = idLabel(cus.getItemsCount());
                        try
                        {
                            SqlConnection con = new SqlConnection();
                            con.ConnectionString = GlobalVar.dataBaseLocation;
                            SqlDataAdapter adapter = new SqlDataAdapter();
                            con.Open();
                            SqlDataReader DataRdr;
                            SqlCommand cmd = new SqlCommand("select itemName from items where userId = "+ GlobalVar.id.ToString(), con);
                            cmd.ExecuteNonQuery();
                            DataRdr = cmd.ExecuteReader();
                            AutoCompleteStringCollection itemNames = new AutoCompleteStringCollection();
                            while (DataRdr.Read())
                            {
                                itemNames.Add(DataRdr.GetString(0));
                            }
                            itemNameTxtbox.AutoCompleteCustomSource = itemNames;
                            con.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    itemNameTxtbox.Select();
                    itemNameTxtbox.Focus();
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

        private bool check()
        {
            return (Equals(itemNameTxtbox.Text, "أدخل اسم المصاغ") && Equals(weightTxtbox.Text, "0") && Equals(feesTxtbox.Text, "0") && Equals(itemCountTxtbox.Text, "0"));
        }

        private void itemNameTxtbox_Leave(object sender, EventArgs e)
        {
            if (itemNameTxtbox.Text.Contains("عيار"))
            {
                MessageBox.Show("لا داعي لكتابة عيار المصاغ , قم بتحديده من الخانات بالاسفل");
                int index = itemNameTxtbox.Text.IndexOf("عيار");
                if (index >= 0)
                {
                    itemNameTxtbox.Text = itemNameTxtbox.Text.Remove(index-1);
                }
            }
        }

        private void menu_Click(object sender, EventArgs e)
        {
            if( panel2.Width == (this.Width - 41))
            {
                panel2.Width = this.Width - 141;
                panel2.Height = this.Height - 37;
            }
            else if ( panel2.Width == (this.Width - 141))
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
                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);
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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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
                customerPayment form = new customerPayment();
                GlobalVar.paymentType = true;
                form.Show();
      this.Close();

            }
            else
            {

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

                DialogResult result = MessageBox.Show("هل تريد حفظ التغييرات الحاصلة على بيانات المصاغ", "الخروج", MessageBoxButtons.YesNoCancel, icon: MessageBoxIcon.Question);

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

        private void itemCountTxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void feesTxtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        public void save_Click11()
        {
            if (Equals(itemNameTxtbox.Text, "أدخل اسم المصاغ") && Equals(weightTxtbox.Text, "0") && Equals(feesTxtbox.Text, "0") && Equals(itemCountTxtbox.Text, "0"))
            {

                MessageBox.Show("!!.. يرجى ادخال بيانات المصاغ");

            }

            else if (Equals(itemNameTxtbox.Text, "أدخل اسم المصاغ") || Equals(itemNameTxtbox.Text, ""))
            {

                MessageBox.Show("!!.. يرجى ادخال اسم المصاغ");

            }

            else if (Equals(weightTxtbox.Text, "0") || Equals(weightTxtbox.Text, ""))
            {

                MessageBox.Show("!!.. يرجى ادخال وزن المصاغ");

            }

            else if (!carat21.Checked && !carat18.Checked && !carat14.Checked)
            {
                MessageBox.Show("!!.. يرجى اختيار عيار المصاغ");
            }

            else if ((Equals(feesTxtbox.Text, "0") || Equals(feesTxtbox.Text, "")) && !(Equals(itemNameTxtbox.Text.TrimEnd(), "خشر")))
            {

                MessageBox.Show("!!.. يرجى ادخال اجور صياغة الغرام");
            }

            else if (Equals(itemCountTxtbox.Text, "") && !(Equals(itemNameTxtbox.Text.TrimEnd(), "خشر")))
            {
                MessageBox.Show("!!.. يرجى ادخال اجور صياغة الغرام");
            }

            else
            {
                if (itemCountTxtbox.Text == "") { itemCountTxtbox.Text = "0"; }
                if (feesTxtbox.Text == "") { feesTxtbox.Text = "0"; }

                string itemName = itemNameTxtbox.Text.TrimEnd();

                decimal weight = Convert.ToDecimal(weightTxtbox.Text);

                decimal fees = Convert.ToDecimal(feesTxtbox.Text);

                int carat = 0;

                int count = Convert.ToInt32(itemCountTxtbox.Text);

                int id = Convert.ToInt32(IDTxtBox.Text);

                itm.paymentCash1 = 0;
                itm.openWeight = weight;
                itm.openCount = count;
                itm.openFees = fees;
                itm.itemWeight1 = weight;
                itm.itemFees1 = fees;
                itm.itemCount1 = count;
                itm.paymentTypeInt1 = -1;
                itm.paymentTypeString1 = "راس مال";
                itm.userId1 = GlobalVar.id;
                itm.customerName1 = "راس مال";
                itm.customerId1 = itm.GetCustomerid();
                itm.paymentDateTime1 = DateTime.Now.Date;

                if (carat21.Checked)
                {
                    carat = 21;
                    itm.paymentNotice1 = "زيادة راس مال ذهب21 (" + itemName + ")";
                    if (itemName.Contains("عيار"))
                    {
                        int index = itemName.IndexOf("عيار");
                        if (index >= 0)
                        {
                            itemNameTxtbox.Text = itemName.Remove(index - 1);
                        }
                    }
                    else
                    {
                        itm.itemName1 = itemName.Trim() + " عيار " + Convert.ToString(carat);

                    }
                    itm.payment141 = 0;
                    itm.payment181 = 0;
                    itm.payment211 = weight;
                    itm.itemCarat1 = carat;

                    if (itm.checkCustomerExist() == "true")
                    {
                        bool d1 = itm.checkItemCaratExist();
                        if (d1)
                        {
                            itm.item14Id = 0;
                            itm.item18Id = 0;
                            itm.item21Id = Convert.ToInt32(itm.getItemId1()) + 1;
                            itm.insertItem21();
                            IDTxtBox.Text = idLabel(itm.getItemsCount());
                        }
                        else
                        {
                            string aa = itemName.Trim() + " عيار " + Convert.ToString(carat);
                            System.Threading.Thread.Sleep(1000);
                            this.Close();
                            editItem form = new editItem();
                            form.Show();
                            itm.itemName1 = aa;
                            itm.itemId1 = itm.getItemId();
                            form.itemWeightTxtbox.Text = Microsoft.VisualBasic.Interaction.InputBox(aa + " موجود مسبقا " + Environment.NewLine + "هل تريد اضافة الكميات الجديدة إلى الكميات الموجودة سابقاً" + Environment.NewLine, "مصاغ بالقطعة", (weight + itm.getItemWeight()).ToString());
                            form.itemNameTxtbox.Text = aa;
                            form.itemFeesTxtbox.Text = fees.ToString();
                            itm.itemName1 = aa;
                            form.idTxtbox.Text = idLabel(Convert.ToString(itm.getItemId()));
                            itm.itemName1 = aa;
                            itm.itemId1 = itm.getItemId();
                            form.itemCountTxtbox.Text = (count + itm.getItemCount()).ToString();
                            form.carat21.Checked = true;
                        }
                    }
                    else
                    {
                        this.Close();
                        System.Threading.Thread.Sleep(1000);
                        customerEntry1 form = new customerEntry1();
                        form.Show();
                        form.nameTxtbox.Text = "راس مال";
                        form.mobileTxtbox.Text = "0900000001";
                    }
                }
                else if (carat18.Checked)
                {
                    carat = 18;
                    itm.paymentNotice1 = "زيادة راس مال ذهب18 (" + itemName + ")";
                    if (itemName.Contains("عيار"))
                    {
                        int index = itemName.IndexOf("عيار");
                        if (index >= 0)
                        {
                            itemNameTxtbox.Text = itemName.Remove(index - 1);
                        }
                    }
                    else
                    {
                        itm.itemName1 = itemName.Trim() + " عيار " + Convert.ToString(carat);
                    }
                    itm.payment141 = 0;
                    itm.payment181 = weight;
                    itm.payment211 = 0;
                    itm.itemCarat1 = carat;

                    if (itm.checkCustomerExist() == "true")
                    {
                        bool d1 = itm.checkItemCaratExist();
                        if (d1)
                        {
                            itm.item14Id = 0;
                            itm.item18Id = Convert.ToInt32(itm.getItemId1()) + 1;
                            itm.item21Id = 0;
                            itm.insertItem18();
                            IDTxtBox.Text = idLabel(itm.getItemsCount());
                        }
                        else
                        {
                            string aa = itemName.Trim() + " عيار " + Convert.ToString(carat);
                            System.Threading.Thread.Sleep(1000);
                            this.Close();
                            editItem form = new editItem();
                            form.Show();
                            itm.itemName1 = aa;
                            itm.itemId1 = itm.getItemId();
                            form.itemWeightTxtbox.Text = Microsoft.VisualBasic.Interaction.InputBox(aa + " موجود مسبقا " + Environment.NewLine + "هل تريد اضافة الكميات الجديدة إلى الكميات الموجودة سابقاً" + Environment.NewLine, "مصاغ بالقطعة", (weight + itm.getItemWeight()).ToString());
                            form.itemNameTxtbox.Text = aa;
                            form.itemFeesTxtbox.Text = fees.ToString();
                            itm.itemName1 = aa;
                            form.idTxtbox.Text = idLabel(Convert.ToString(itm.getItemId()));
                            itm.itemName1 = aa;
                            itm.itemId1 = itm.getItemId();
                            form.itemCountTxtbox.Text = (count + itm.getItemCount()).ToString();
                            form.carat18.Checked = true;
                        }
                    }
                    else
                    {
              this.Close();
                        System.Threading.Thread.Sleep(1000);
                        customerEntry1 form = new customerEntry1();
                        form.Show();
                        form.nameTxtbox.Text = "راس مال";
                        form.mobileTxtbox.Text = "0900000001";
                    }
                }
                else if (carat14.Checked)
                {
                    carat = 14;
                    itm.paymentNotice1 = "زيادة راس مال ذهب14 (" + itemName + ")";
                    if (itemName.Contains("عيار"))
                    {
                        int index = itemName.IndexOf("عيار");
                        if (index >= 0)
                        {
                            itemNameTxtbox.Text = itemName.Remove(index - 1);
                        }
                    }
                    else
                    {
                        itm.itemName1 = itemName.Trim() + " عيار " + Convert.ToString(carat);
                    }
                    itm.payment141 = weight;
                    itm.payment181 = 0;
                    itm.payment211 = 0;
                    itm.itemCarat1 = carat;

                    if (itm.checkCustomerExist() == "true")
                    {
                        bool d1 = itm.checkItemCaratExist();
                        if (d1)
                        {
                            itm.item14Id = Convert.ToInt32(itm.getItemId1()) + 1;
                            itm.item18Id = 0;
                            itm.item21Id = 0;
                            itm.insertItem14();
                            IDTxtBox.Text = idLabel(itm.getItemsCount());
                        }
                        else
                        {
                            string aa = itemName.Trim() + " عيار " + Convert.ToString(carat);
                        //    MessageBox.Show(aa + " موجود مسبقا ");
                            System.Threading.Thread.Sleep(1000);
                  this.Close();
                            editItem form = new editItem();
                            form.Show();
                            itm.itemName1 = aa;
                            itm.itemId1 = itm.getItemId();
                            form.itemWeightTxtbox.Text = Microsoft.VisualBasic.Interaction.InputBox(aa + " موجود مسبقا " + Environment.NewLine + "هل تريد اضافة الكميات الجديدة إلى الكميات الموجودة سابقاً" + Environment.NewLine, "مصاغ بالقطعة", (weight + itm.getItemWeight()).ToString());
                            form.itemNameTxtbox.Text = aa;
                            form.itemFeesTxtbox.Text = fees.ToString();
                            itm.itemName1 = aa;
                            form.idTxtbox.Text = idLabel(Convert.ToString(itm.getItemId()));
                            itm.itemName1 = aa;
                            itm.itemId1 = itm.getItemId();
                            form.itemCountTxtbox.Text = (count + itm.getItemCount()).ToString();
                            form.carat14.Checked = true;
                        }
                    }
                    else
                    {
                     //   MessageBox.Show(" قم باضافة صائغ باسم (راس مال) في نافذة اضافة صائغ");
              this.Close();
                        System.Threading.Thread.Sleep(1000);
                        customerEntry1 form = new customerEntry1();
                        form.Show();
                        form.nameTxtbox.Text = "راس مال";
                        form.mobileTxtbox.Text = "0900000001";
                    }
                }

                itemNameTxtbox.Text = "أدخل اسم المصاغ";
                weightTxtbox.Text = "0";
                itemCountTxtbox.Text = "0";
                feesTxtbox.Text = "0";
                carat14.Checked = false;
                carat18.Checked = false;
                carat21.Checked = false;
            }

        }

        private void itemNameTxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                carat21.Checked=true;
                itemCountTxtbox.Focus();
                itemCountTxtbox.Select();
            }
        }

        private void itemCountTxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                weightTxtbox.Select();
                weightTxtbox.Focus();
            }
        }

        private void weightTxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                feesTxtbox.Select();
                feesTxtbox.Focus();
            }
        }

        private void feesTxtbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            save_Click(sender, e);
        }
        //

    }
}
