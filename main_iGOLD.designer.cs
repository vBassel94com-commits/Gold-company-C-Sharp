namespace iGOLD
{
    partial class main_iGOLD
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main_iGOLD));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.yellowPanel = new System.Windows.Forms.Panel();
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.bar = new System.Windows.Forms.PictureBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.connectionS = new System.Windows.Forms.Label();
            this.vLabel = new System.Windows.Forms.Label();
            this.financeAccount = new System.Windows.Forms.Button();
            this.status = new Bunifu.Framework.UI.BunifuiOSSwitch();
            this.close = new System.Windows.Forms.PictureBox();
            this.minimize = new System.Windows.Forms.PictureBox();
            this.companyTotalAccount = new System.Windows.Forms.Button();
            this.update = new System.Windows.Forms.Button();
            this.customerPayment = new System.Windows.Forms.Button();
            this.sellBill = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView11 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView11)).BeginInit();
            this.SuspendLayout();
            // 
            // yellowPanel
            // 
            this.yellowPanel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.yellowPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(37)))));
            this.yellowPanel.Location = new System.Drawing.Point(0, 32);
            this.yellowPanel.Name = "yellowPanel";
            this.yellowPanel.Size = new System.Drawing.Size(790, 3);
            this.yellowPanel.TabIndex = 15;
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // TopToolStripPanel
            // 
            this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.TopToolStripPanel.Name = "TopToolStripPanel";
            this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(149, 124);
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.bar;
            this.bunifuDragControl1.Vertical = true;
            // 
            // bar
            // 
            this.bar.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar.Image = global::iGOLD.Properties.Resources.igold331;
            this.bar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bar.Location = new System.Drawing.Point(0, 0);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(792, 37);
            this.bar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bar.TabIndex = 17;
            this.bar.TabStop = false;
            this.bar.Click += new System.EventHandler(this.bar_Click);
            this.bar.MouseLeave += new System.EventHandler(this.bar_MouseLeave);
            this.bar.MouseHover += new System.EventHandler(this.bar_MouseHover);
            // 
            // statusLabel
            // 
            this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLabel.AutoSize = true;
            this.statusLabel.ForeColor = System.Drawing.Color.Red;
            this.statusLabel.Location = new System.Drawing.Point(692, 5);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(57, 21);
            this.statusLabel.TabIndex = 22;
            this.statusLabel.Text = "مغلق      ";
            this.statusLabel.Click += new System.EventHandler(this.statusLabel_Click);
            // 
            // connectionS
            // 
            this.connectionS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionS.AutoSize = true;
            this.connectionS.ForeColor = System.Drawing.Color.Red;
            this.connectionS.Location = new System.Drawing.Point(595, 5);
            this.connectionS.Name = "connectionS";
            this.connectionS.Size = new System.Drawing.Size(81, 21);
            this.connectionS.TabIndex = 13;
            this.connectionS.Text = "غير متصل    ";
            this.connectionS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.connectionS.Click += new System.EventHandler(this.connectionS_Click);
            // 
            // vLabel
            // 
            this.vLabel.AutoSize = true;
            this.vLabel.BackColor = System.Drawing.Color.Gold;
            this.vLabel.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vLabel.ForeColor = System.Drawing.Color.OrangeRed;
            this.vLabel.Location = new System.Drawing.Point(189, 4);
            this.vLabel.Name = "vLabel";
            this.vLabel.Size = new System.Drawing.Size(36, 25);
            this.vLabel.TabIndex = 23;
            this.vLabel.Text = "10";
            this.vLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.vLabel.Visible = false;
            // 
            // financeAccount
            // 
            this.financeAccount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.financeAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(37)))));
            this.financeAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.financeAccount.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.financeAccount.FlatAppearance.BorderSize = 0;
            this.financeAccount.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.financeAccount.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.financeAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.financeAccount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.financeAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.financeAccount.Image = global::iGOLD.Properties.Resources.icons8_Purchase_Order_32;
            this.financeAccount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.financeAccount.Location = new System.Drawing.Point(175, 74);
            this.financeAccount.Name = "financeAccount";
            this.financeAccount.Size = new System.Drawing.Size(130, 100);
            this.financeAccount.TabIndex = 10;
            this.financeAccount.Text = "القيود والاستعلامات";
            this.financeAccount.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.financeAccount.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.financeAccount.UseVisualStyleBackColor = false;
            this.financeAccount.Click += new System.EventHandler(this.financeAccount_Click);
            // 
            // status
            // 
            this.status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.status.BackColor = System.Drawing.Color.Transparent;
            this.status.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("status.BackgroundImage")));
            this.status.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.status.Cursor = System.Windows.Forms.Cursors.Hand;
            this.status.Location = new System.Drawing.Point(4789, 42);
            this.status.Margin = new System.Windows.Forms.Padding(37, 34, 37, 34);
            this.status.Name = "status";
            this.status.OffColor = System.Drawing.Color.Red;
            this.status.OnColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(32)))));
            this.status.Size = new System.Drawing.Size(35, 20);
            this.status.TabIndex = 12;
            this.status.Value = false;
            // 
            // close
            // 
            this.close.Image = global::iGOLD.Properties.Resources.icons8_Delete_32;
            this.close.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.close.Location = new System.Drawing.Point(4, 0);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(32, 32);
            this.close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.close.TabIndex = 11;
            this.close.TabStop = false;
            this.close.Click += new System.EventHandler(this.close_Click);
            this.close.MouseEnter += new System.EventHandler(this.close_MouseEnter);
            this.close.MouseLeave += new System.EventHandler(this.close_MouseLeave);
            this.close.MouseHover += new System.EventHandler(this.close_MouseHover);
            // 
            // minimize
            // 
            this.minimize.Image = global::iGOLD.Properties.Resources.icons8_Minus_32;
            this.minimize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.minimize.Location = new System.Drawing.Point(42, 0);
            this.minimize.Name = "minimize";
            this.minimize.Size = new System.Drawing.Size(32, 32);
            this.minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.minimize.TabIndex = 12;
            this.minimize.TabStop = false;
            this.minimize.Click += new System.EventHandler(this.minimize_Click);
            this.minimize.MouseLeave += new System.EventHandler(this.minimize_MouseLeave);
            this.minimize.MouseHover += new System.EventHandler(this.minimize_MouseHover);
            // 
            // companyTotalAccount
            // 
            this.companyTotalAccount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.companyTotalAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(37)))));
            this.companyTotalAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.companyTotalAccount.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.companyTotalAccount.FlatAppearance.BorderSize = 0;
            this.companyTotalAccount.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.companyTotalAccount.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.companyTotalAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.companyTotalAccount.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.companyTotalAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.companyTotalAccount.Image = global::iGOLD.Properties.Resources.icons8_Combo_Chart_32;
            this.companyTotalAccount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.companyTotalAccount.Location = new System.Drawing.Point(19, 74);
            this.companyTotalAccount.MinimumSize = new System.Drawing.Size(130, 100);
            this.companyTotalAccount.Name = "companyTotalAccount";
            this.companyTotalAccount.Size = new System.Drawing.Size(130, 100);
            this.companyTotalAccount.TabIndex = 11;
            this.companyTotalAccount.Text = "اعدادات";
            this.companyTotalAccount.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.companyTotalAccount.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.companyTotalAccount.UseVisualStyleBackColor = false;
            this.companyTotalAccount.Click += new System.EventHandler(this.companyTotalAccount_Click);
            // 
            // update
            // 
            this.update.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.update.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(37)))));
            this.update.Cursor = System.Windows.Forms.Cursors.Hand;
            this.update.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.update.FlatAppearance.BorderSize = 0;
            this.update.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.update.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.update.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.update.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.update.Image = global::iGOLD.Properties.Resources.icons8_Download1_32;
            this.update.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.update.Location = new System.Drawing.Point(643, 74);
            this.update.MinimumSize = new System.Drawing.Size(130, 100);
            this.update.Name = "update";
            this.update.Size = new System.Drawing.Size(130, 100);
            this.update.TabIndex = 8;
            this.update.Text = "تحديث";
            this.update.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.update.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.update.UseVisualStyleBackColor = false;
            this.update.Click += new System.EventHandler(this.update_Click);
            // 
            // customerPayment
            // 
            this.customerPayment.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.customerPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(37)))));
            this.customerPayment.Cursor = System.Windows.Forms.Cursors.Hand;
            this.customerPayment.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.customerPayment.FlatAppearance.BorderSize = 0;
            this.customerPayment.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.customerPayment.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.customerPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customerPayment.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customerPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.customerPayment.Image = global::iGOLD.Properties.Resources.icons8_Money_Bag_32;
            this.customerPayment.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.customerPayment.Location = new System.Drawing.Point(487, 74);
            this.customerPayment.MinimumSize = new System.Drawing.Size(130, 100);
            this.customerPayment.Name = "customerPayment";
            this.customerPayment.Size = new System.Drawing.Size(130, 100);
            this.customerPayment.TabIndex = 7;
            this.customerPayment.Text = "الفواتير";
            this.customerPayment.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.customerPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.customerPayment.UseVisualStyleBackColor = false;
            this.customerPayment.Click += new System.EventHandler(this.customerPayment_Click);
            // 
            // sellBill
            // 
            this.sellBill.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sellBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(37)))));
            this.sellBill.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sellBill.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.sellBill.FlatAppearance.BorderSize = 0;
            this.sellBill.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.sellBill.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.sellBill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sellBill.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sellBill.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sellBill.Image = global::iGOLD.Properties.Resources.icons8_Receive_Cash_32;
            this.sellBill.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.sellBill.Location = new System.Drawing.Point(331, 74);
            this.sellBill.MinimumSize = new System.Drawing.Size(130, 100);
            this.sellBill.Name = "sellBill";
            this.sellBill.Size = new System.Drawing.Size(130, 100);
            this.sellBill.TabIndex = 4;
            this.sellBill.Text = "اليومية";
            this.sellBill.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.sellBill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.sellBill.UseVisualStyleBackColor = false;
            this.sellBill.Click += new System.EventHandler(this.sellBill_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(32)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(32)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Orange;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(32)))));
            this.dataGridView1.Location = new System.Drawing.Point(617, -82);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(132, 10);
            this.dataGridView1.TabIndex = 156;
            this.dataGridView1.Visible = false;
            // 
            // label1
            // 
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(257, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 21);
            this.label1.TabIndex = 157;
            this.label1.Text = ":اهلا وسهلا بك";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // username
            // 
            this.username.ForeColor = System.Drawing.Color.Turquoise;
            this.username.Location = new System.Drawing.Point(129, 40);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(130, 21);
            this.username.TabIndex = 158;
            this.username.Text = "addas";
            this.username.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.ForeColor = System.Drawing.Color.Salmon;
            this.label3.Location = new System.Drawing.Point(38, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 21);
            this.label3.TabIndex = 159;
            this.label3.Text = "تسجيل الخروج";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // dataGridView11
            // 
            this.dataGridView11.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView11.Location = new System.Drawing.Point(739, 74);
            this.dataGridView11.Name = "dataGridView11";
            this.dataGridView11.Size = new System.Drawing.Size(10, 10);
            this.dataGridView11.TabIndex = 160;
            this.dataGridView11.Visible = false;
            // 
            // main_iGOLD
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(38)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(792, 200);
            this.Controls.Add(this.update);
            this.Controls.Add(this.dataGridView11);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.sellBill);
            this.Controls.Add(this.vLabel);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.connectionS);
            this.Controls.Add(this.financeAccount);
            this.Controls.Add(this.status);
            this.Controls.Add(this.close);
            this.Controls.Add(this.yellowPanel);
            this.Controls.Add(this.minimize);
            this.Controls.Add(this.companyTotalAccount);
            this.Controls.Add(this.customerPayment);
            this.Controls.Add(this.bar);
            this.Controls.Add(this.username);
            this.Controls.Add(this.label3);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(792, 200);
            this.Name = "main_iGOLD";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "iGOLD";
            this.Load += new System.EventHandler(this.main_iGOLD_Load);
            this.Shown += new System.EventHandler(this.main_iGOLD_Shown);
            this.Resize += new System.EventHandler(this.main_iGOLD_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView11)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox close;
        private System.Windows.Forms.PictureBox minimize;
        private System.Windows.Forms.Panel yellowPanel;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel TopToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.PictureBox bar;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        public System.Windows.Forms.Button sellBill;
        public System.Windows.Forms.Button companyTotalAccount;
        private System.Windows.Forms.Label statusLabel;
        public Bunifu.Framework.UI.BunifuiOSSwitch status;
        public System.Windows.Forms.Button financeAccount;
        private System.Windows.Forms.Label connectionS;
		private System.Windows.Forms.Label vLabel;
		public System.Windows.Forms.Button customerPayment;
		public System.Windows.Forms.Button update;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label username;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView11;
    }
}

