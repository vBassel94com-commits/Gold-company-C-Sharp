using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Management;

namespace iGOLD
{
    public partial class prop : Form
    {
        public prop()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        Customerdb_Class cus = new Customerdb_Class();
        userdb_Class usr = new userdb_Class();
        SqlConnection con = new SqlConnection(GlobalVar.dataBaseLocation);
        private const int cGrip = 16;
        private const int cCaption = 32;
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
            DialogResult result = MessageBox.Show("هل أنت متأكد بالخروج من الاعدادات", "الخروج", MessageBoxButtons.YesNo, icon: MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
            }
            else
            {
                this.Close();
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

        private void prop_Load(object sender, EventArgs e)
        {
            comboBox1.Text=GlobalVar.defaultPrinter;
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                comboBox1.Items.Add(printer);
            }
			////logoTextbox.Text = cus.getLogo();
			foreach (DriveInfo drive in DriveInfo.GetDrives())
			{
				if (drive.DriveType == DriveType.Removable)
				{
					comboBox2.Items.Add(string.Format("{0}", drive.Name.Replace("\\","")));
				}
			}
            comboBox3.Text = GlobalVar.Items;
            comboBox4.Text = GlobalVar.Daily;
            comboBox5.Text = GlobalVar.Payment;
            textBox2.Text = GlobalVar.Logo;
            textBox1.Text = cus.getDays();
            textBox3.Text = usr.getNo();
		}

		private void choose_Click(object sender, EventArgs e)
        {
			if (comboBox1.Text != "default")
			{
				GlobalVar.defaultPrinter = comboBox1.Text;
				cus.name1 = GlobalVar.defaultPrinter;
				MessageBox.Show(cus.updatePrinter());
			}
			else
			{
				MessageBox.Show("يرجى اختيار طابعة اخرى ");
			}
        }

		private void comboBox1_TextChanged(object sender, EventArgs e)
		{
			if (comboBox1.Text== "default" && GlobalVar.userName=="addas")
			{
				panel1.Visible = true;

			}
			else
			{
				panel1.Visible = false;
			}
		}

		private void flash_Click(object sender, EventArgs e)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("letter", typeof(string));
			dt.Columns.Add("pid", typeof(string));
			foreach (ManagementObject drive in new ManagementObjectSearcher("select * from Win32_DiskDrive where InterfaceType='USB'").Get())
			{

				foreach (ManagementObject partition in new ManagementObjectSearcher("ASSOCIATORS OF {Win32_DiskDrive.DeviceID='"
					+ drive["DeviceID"] + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())
				{
					foreach (ManagementObject disk in new ManagementObjectSearcher("ASSOCIATORS OF {Win32_DiskPartition.DeviceID='"
						  + partition["DeviceID"] + "'} WHERE AssocClass = Win32_LogicalDiskToPartition").Get())
					{
						dt.Rows.Add(disk["Name"], new ManagementObject("Win32_PhysicalMedia.Tag='"
							 + drive["DeviceID"] + "'")["SerialNumber"]);
					}

				}
			}
			string a = "";
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				if (comboBox2.Text == dt.Rows[i][0].ToString())
				{
					a = dt.Rows[i][1].ToString().ToUpper();
				}
			}
			cus.name1 = a;
            GlobalVar.pid = a;
			MessageBox.Show(cus.updateFlash());

		}

		private void valid_Click(object sender, EventArgs e)
		{
			if (Convert.ToDecimal(textBox1.Text)>1)
			{
				cus.days1 = Convert.ToInt32(textBox1.Text);
				cus.startDay1 = DateTime.Now.Date;
				MessageBox.Show(cus.updateLicences());
			}
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
		}

        private void button1_Click(object sender, EventArgs e)
        {
            string nam = "";
            if (textBox2.Text.Trim()=="")
            { }
            else
            {
                nam = textBox2.Text;
                cus.name = nam;
                GlobalVar.Logo = nam;
                MessageBox.Show(cus.updateLogo());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nam = "";
            if (comboBox3.Text.Trim() == "")
            { }
            else
            {
                nam = comboBox3.Text.Trim();
                cus.name = nam;
                GlobalVar.Items = nam;
                MessageBox.Show(cus.updateItems());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string nam = "";
            if (comboBox4.Text.Trim() == "")
            { }
            else
            {
                nam = comboBox4.Text.Trim();
                cus.name = nam;
                GlobalVar.Daily = nam;
                MessageBox.Show(cus.updateDaily());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string nam = "";
            if (comboBox5.Text.Trim() == "")
            { }
            else
            {
                nam = comboBox5.Text.Trim();
                cus.name = nam;
                GlobalVar.Payment = nam;
                MessageBox.Show(cus.updatePayment1());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string nam = "";
            if (textBox2.Text.Trim() == "")
            { }
            else
            {
                nam = textBox3.Text;
                usr.name = nam;
                MessageBox.Show(usr.updateN());
            }
        }
    }
}
