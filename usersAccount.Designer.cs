namespace iGOLD
{
    partial class usersAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usersAccount));
            this.IDLabel = new System.Windows.Forms.Label();
            this.IDTxtBox = new System.Windows.Forms.TextBox();
            this.bunifuCustomLabel3 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.userNameTxtbox = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.bunifuCustomLabel6 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.passWord2Txtbox = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.bunifuCustomLabel4 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.passWord1Txtbox = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.cancel = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.PictureBox();
            this.maximize = new System.Windows.Forms.PictureBox();
            this.minimize = new System.Windows.Forms.PictureBox();
            this.yellowPanel = new System.Windows.Forms.Panel();
            this.bar = new System.Windows.Forms.PictureBox();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.edit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maximize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar)).BeginInit();
            this.SuspendLayout();
            // 
            // IDLabel
            // 
            this.IDLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.IDLabel.AutoSize = true;
            this.IDLabel.ForeColor = System.Drawing.Color.Yellow;
            this.IDLabel.Location = new System.Drawing.Point(60, 59);
            this.IDLabel.Name = "IDLabel";
            this.IDLabel.Size = new System.Drawing.Size(18, 13);
            this.IDLabel.TabIndex = 108;
            this.IDLabel.Text = "ID";
            // 
            // IDTxtBox
            // 
            this.IDTxtBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.IDTxtBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(38)))), ((int)(((byte)(35)))));
            this.IDTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IDTxtBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.IDTxtBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDTxtBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.IDTxtBox.Location = new System.Drawing.Point(28, 75);
            this.IDTxtBox.Name = "IDTxtBox";
            this.IDTxtBox.ReadOnly = true;
            this.IDTxtBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.IDTxtBox.Size = new System.Drawing.Size(83, 21);
            this.IDTxtBox.TabIndex = 107;
            this.IDTxtBox.TabStop = false;
            this.IDTxtBox.Text = "001";
            this.IDTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bunifuCustomLabel3
            // 
            this.bunifuCustomLabel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.bunifuCustomLabel3.AutoSize = true;
            this.bunifuCustomLabel3.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.bunifuCustomLabel3.Location = new System.Drawing.Point(160, 55);
            this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
            this.bunifuCustomLabel3.Size = new System.Drawing.Size(280, 35);
            this.bunifuCustomLabel3.TabIndex = 105;
            this.bunifuCustomLabel3.Text = "حسابات المستخدمين";
            // 
            // userNameTxtbox
            // 
            this.userNameTxtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.userNameTxtbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.userNameTxtbox.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNameTxtbox.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.userNameTxtbox.HintForeColor = System.Drawing.Color.SpringGreen;
            this.userNameTxtbox.HintText = "";
            this.userNameTxtbox.isPassword = false;
            this.userNameTxtbox.LineFocusedColor = System.Drawing.Color.Beige;
            this.userNameTxtbox.LineIdleColor = System.Drawing.Color.MediumSpringGreen;
            this.userNameTxtbox.LineMouseHoverColor = System.Drawing.Color.Beige;
            this.userNameTxtbox.LineThickness = 2;
            this.userNameTxtbox.Location = new System.Drawing.Point(181, 109);
            this.userNameTxtbox.Margin = new System.Windows.Forms.Padding(5);
            this.userNameTxtbox.Name = "userNameTxtbox";
            this.userNameTxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.userNameTxtbox.Size = new System.Drawing.Size(217, 44);
            this.userNameTxtbox.TabIndex = 1;
            this.userNameTxtbox.Tag = "";
            this.userNameTxtbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.userNameTxtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userNameTxtbox_KeyDown);
            this.userNameTxtbox.Leave += new System.EventHandler(this.userNameTxtbox_Leave);
            this.userNameTxtbox.MouseEnter += new System.EventHandler(this.userNameTxtbox_MouseEnter);
            this.userNameTxtbox.MouseLeave += new System.EventHandler(this.userNameTxtbox_MouseLeave);
            // 
            // bunifuCustomLabel6
            // 
            this.bunifuCustomLabel6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.bunifuCustomLabel6.AutoSize = true;
            this.bunifuCustomLabel6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bunifuCustomLabel6.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel6.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.bunifuCustomLabel6.Location = new System.Drawing.Point(411, 136);
            this.bunifuCustomLabel6.Name = "bunifuCustomLabel6";
            this.bunifuCustomLabel6.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.bunifuCustomLabel6.Size = new System.Drawing.Size(163, 25);
            this.bunifuCustomLabel6.TabIndex = 131;
            this.bunifuCustomLabel6.Text = "اسم المستخدم :";
            // 
            // passWord2Txtbox
            // 
            this.passWord2Txtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.passWord2Txtbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.passWord2Txtbox.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passWord2Txtbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.passWord2Txtbox.HintForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.passWord2Txtbox.HintText = "";
            this.passWord2Txtbox.isPassword = true;
            this.passWord2Txtbox.LineFocusedColor = System.Drawing.Color.Beige;
            this.passWord2Txtbox.LineIdleColor = System.Drawing.Color.Yellow;
            this.passWord2Txtbox.LineMouseHoverColor = System.Drawing.Color.Beige;
            this.passWord2Txtbox.LineThickness = 2;
            this.passWord2Txtbox.Location = new System.Drawing.Point(181, 281);
            this.passWord2Txtbox.Margin = new System.Windows.Forms.Padding(5);
            this.passWord2Txtbox.Name = "passWord2Txtbox";
            this.passWord2Txtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.passWord2Txtbox.Size = new System.Drawing.Size(217, 44);
            this.passWord2Txtbox.TabIndex = 3;
            this.passWord2Txtbox.Tag = "";
            this.passWord2Txtbox.Text = "كلمة المرور";
            this.passWord2Txtbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.passWord2Txtbox.OnValueChanged += new System.EventHandler(this.passWord2Txtbox_OnValueChanged);
            this.passWord2Txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.passWord2Txtbox_KeyDown);
            this.passWord2Txtbox.MouseEnter += new System.EventHandler(this.passWord2Txtbox_MouseEnter);
            this.passWord2Txtbox.MouseLeave += new System.EventHandler(this.passWord2Txtbox_MouseLeave);
            // 
            // bunifuCustomLabel4
            // 
            this.bunifuCustomLabel4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.bunifuCustomLabel4.AutoSize = true;
            this.bunifuCustomLabel4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bunifuCustomLabel4.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel4.ForeColor = System.Drawing.Color.Yellow;
            this.bunifuCustomLabel4.Location = new System.Drawing.Point(411, 310);
            this.bunifuCustomLabel4.Name = "bunifuCustomLabel4";
            this.bunifuCustomLabel4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.bunifuCustomLabel4.Size = new System.Drawing.Size(189, 23);
            this.bunifuCustomLabel4.TabIndex = 129;
            this.bunifuCustomLabel4.Text = "أعد كتابة كلمة المرور :";
            // 
            // passWord1Txtbox
            // 
            this.passWord1Txtbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.passWord1Txtbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.passWord1Txtbox.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passWord1Txtbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.passWord1Txtbox.HintForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.passWord1Txtbox.HintText = "";
            this.passWord1Txtbox.isPassword = true;
            this.passWord1Txtbox.LineFocusedColor = System.Drawing.Color.Beige;
            this.passWord1Txtbox.LineIdleColor = System.Drawing.Color.Yellow;
            this.passWord1Txtbox.LineMouseHoverColor = System.Drawing.Color.Beige;
            this.passWord1Txtbox.LineThickness = 2;
            this.passWord1Txtbox.Location = new System.Drawing.Point(181, 196);
            this.passWord1Txtbox.Margin = new System.Windows.Forms.Padding(5);
            this.passWord1Txtbox.Name = "passWord1Txtbox";
            this.passWord1Txtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.passWord1Txtbox.Size = new System.Drawing.Size(217, 44);
            this.passWord1Txtbox.TabIndex = 2;
            this.passWord1Txtbox.Tag = "";
            this.passWord1Txtbox.Text = "كلمة المرور";
            this.passWord1Txtbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.passWord1Txtbox.OnValueChanged += new System.EventHandler(this.passWord1Txtbox_OnValueChanged);
            this.passWord1Txtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.passWord1Txtbox_KeyDown);
            this.passWord1Txtbox.MouseEnter += new System.EventHandler(this.passWord1Txtbox_MouseEnter);
            this.passWord1Txtbox.MouseLeave += new System.EventHandler(this.passWord1Txtbox_MouseLeave);
            // 
            // bunifuCustomLabel2
            // 
            this.bunifuCustomLabel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.bunifuCustomLabel2.AutoSize = true;
            this.bunifuCustomLabel2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuCustomLabel2.ForeColor = System.Drawing.Color.Yellow;
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(411, 223);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(125, 25);
            this.bunifuCustomLabel2.TabIndex = 127;
            this.bunifuCustomLabel2.Text = "كلمة المرور :";
            // 
            // cancel
            // 
            this.cancel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(37)))));
            this.cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancel.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.cancel.FlatAppearance.BorderSize = 0;
            this.cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancel.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.cancel.ForeColor = System.Drawing.Color.Black;
            this.cancel.Image = global::iGOLD.Properties.Resources.icons8_Cancel1_32;
            this.cancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cancel.Location = new System.Drawing.Point(458, 356);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(130, 54);
            this.cancel.TabIndex = 6;
            this.cancel.Text = "الغاء";
            this.cancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.cancel.UseVisualStyleBackColor = false;
            this.cancel.Click += new System.EventHandler(this.cancel_Click);
            // 
            // save
            // 
            this.save.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(37)))));
            this.save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.save.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.save.FlatAppearance.BorderSize = 0;
            this.save.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(225)))), ((int)(((byte)(0)))));
            this.save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.save.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.save.ForeColor = System.Drawing.Color.Black;
            this.save.Image = global::iGOLD.Properties.Resources.icons8_Download1_32;
            this.save.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.save.Location = new System.Drawing.Point(12, 356);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(130, 54);
            this.save.TabIndex = 4;
            this.save.Text = "حفظ";
            this.save.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.save.UseVisualStyleBackColor = false;
            this.save.Click += new System.EventHandler(this.save_Click);
            this.save.MouseLeave += new System.EventHandler(this.save_MouseLeave);
            this.save.MouseHover += new System.EventHandler(this.save_MouseHover);
            // 
            // close
            // 
            this.close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.close.Image = global::iGOLD.Properties.Resources.icons8_Delete_32;
            this.close.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.close.Location = new System.Drawing.Point(0, 0);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(32, 32);
            this.close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.close.TabIndex = 136;
            this.close.TabStop = false;
            this.close.Click += new System.EventHandler(this.close_Click);
            this.close.MouseEnter += new System.EventHandler(this.close_MouseEnter);
            this.close.MouseLeave += new System.EventHandler(this.close_MouseLeave);
            this.close.MouseHover += new System.EventHandler(this.close_MouseHover);
            // 
            // maximize
            // 
            this.maximize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maximize.Image = global::iGOLD.Properties.Resources.icons8_Max3imize_Window_32;
            this.maximize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.maximize.Location = new System.Drawing.Point(28, 0);
            this.maximize.Name = "maximize";
            this.maximize.Size = new System.Drawing.Size(32, 32);
            this.maximize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.maximize.TabIndex = 138;
            this.maximize.TabStop = false;
            this.maximize.Click += new System.EventHandler(this.maximize_Click);
            this.maximize.MouseLeave += new System.EventHandler(this.maximize_MouseLeave);
            this.maximize.MouseHover += new System.EventHandler(this.maximize_MouseHover);
            // 
            // minimize
            // 
            this.minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimize.Image = global::iGOLD.Properties.Resources.icons8_Minus_32;
            this.minimize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.minimize.Location = new System.Drawing.Point(63, 0);
            this.minimize.Name = "minimize";
            this.minimize.Size = new System.Drawing.Size(32, 32);
            this.minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.minimize.TabIndex = 137;
            this.minimize.TabStop = false;
            this.minimize.Click += new System.EventHandler(this.minimize_Click);
            this.minimize.MouseLeave += new System.EventHandler(this.minimize_MouseLeave);
            this.minimize.MouseHover += new System.EventHandler(this.minimize_MouseHover);
            // 
            // yellowPanel
            // 
            this.yellowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.yellowPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.yellowPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.yellowPanel.Location = new System.Drawing.Point(1, 33);
            this.yellowPanel.Name = "yellowPanel";
            this.yellowPanel.Size = new System.Drawing.Size(600, 3);
            this.yellowPanel.TabIndex = 135;
            // 
            // bar
            // 
            this.bar.Dock = System.Windows.Forms.DockStyle.Top;
            this.bar.Image = ((System.Drawing.Image)(resources.GetObject("bar.Image")));
            this.bar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.bar.Location = new System.Drawing.Point(0, 0);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(600, 32);
            this.bar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bar.TabIndex = 134;
            this.bar.TabStop = false;
            this.bar.WaitOnLoad = true;
            this.bar.Click += new System.EventHandler(this.bar_Click);
            this.bar.MouseLeave += new System.EventHandler(this.bar_MouseLeave);
            this.bar.MouseHover += new System.EventHandler(this.bar_MouseHover);
            // 
            // bunifuDragControl1
            // 
            this.bunifuDragControl1.Fixed = true;
            this.bunifuDragControl1.Horizontal = true;
            this.bunifuDragControl1.TargetControl = this.bar;
            this.bunifuDragControl1.Vertical = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(186, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 16);
            this.label1.TabIndex = 142;
            // 
            // edit
            // 
            this.edit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.edit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(37)))));
            this.edit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.edit.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.edit.FlatAppearance.BorderSize = 0;
            this.edit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.edit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.edit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.edit.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.edit.ForeColor = System.Drawing.Color.Black;
            this.edit.Image = global::iGOLD.Properties.Resources.icons8_Edit_File_32;
            this.edit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.edit.Location = new System.Drawing.Point(312, 356);
            this.edit.Name = "edit";
            this.edit.Size = new System.Drawing.Size(130, 54);
            this.edit.TabIndex = 5;
            this.edit.Text = "تعديل كلمة المرور";
            this.edit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.edit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.edit.UseVisualStyleBackColor = false;
            this.edit.Click += new System.EventHandler(this.edit_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(37)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Image = global::iGOLD.Properties.Resources.icons8_Edit_File_32;
            this.button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.button1.Location = new System.Drawing.Point(158, 356);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 54);
            this.button1.TabIndex = 143;
            this.button1.Text = "تعديل اسم المستخدم";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // usersAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(600, 430);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.edit);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.close);
            this.Controls.Add(this.maximize);
            this.Controls.Add(this.minimize);
            this.Controls.Add(this.yellowPanel);
            this.Controls.Add(this.bar);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.save);
            this.Controls.Add(this.userNameTxtbox);
            this.Controls.Add(this.bunifuCustomLabel6);
            this.Controls.Add(this.passWord2Txtbox);
            this.Controls.Add(this.bunifuCustomLabel4);
            this.Controls.Add(this.passWord1Txtbox);
            this.Controls.Add(this.bunifuCustomLabel2);
            this.Controls.Add(this.IDLabel);
            this.Controls.Add(this.IDTxtBox);
            this.Controls.Add(this.bunifuCustomLabel3);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(32)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(600, 430);
            this.Name = "usersAccount";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form8";
            this.Load += new System.EventHandler(this.usersAccount_Load);
            this.Resize += new System.EventHandler(this.usersAccount_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maximize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label IDLabel;
        private System.Windows.Forms.TextBox IDTxtBox;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel3;
        private Bunifu.Framework.UI.BunifuMaterialTextbox userNameTxtbox;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel6;
        private Bunifu.Framework.UI.BunifuMaterialTextbox passWord2Txtbox;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel4;
        private Bunifu.Framework.UI.BunifuMaterialTextbox passWord1Txtbox;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private System.Windows.Forms.Button cancel;
        private System.Windows.Forms.Button save;
        public System.Windows.Forms.PictureBox close;
        public System.Windows.Forms.PictureBox maximize;
        public System.Windows.Forms.PictureBox minimize;
        public System.Windows.Forms.Panel yellowPanel;
        public System.Windows.Forms.PictureBox bar;
        private Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button edit;
        public System.Windows.Forms.Button button1;
    }
}