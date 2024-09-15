namespace iGOLD
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.userNameTxtbox = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.passWordTxtbox = new Bunifu.Framework.UI.BunifuMaterialTextbox();
            this.exit = new System.Windows.Forms.PictureBox();
            this.login_btn = new Bunifu.Framework.UI.BunifuThinButton2();
            this.bar = new System.Windows.Forms.PictureBox();
            this.bunifuDragControl1 = new Bunifu.Framework.UI.BunifuDragControl(this.components);
            this.passVisible = new System.Windows.Forms.CheckBox();
            this.minimize = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.exit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).BeginInit();
            this.SuspendLayout();
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 550;
            this.bunifuElipse1.TargetControl = this;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(50, 383);
            this.panel1.TabIndex = 0;
            // 
            // userNameTxtbox
            // 
            this.userNameTxtbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.userNameTxtbox.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userNameTxtbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.userNameTxtbox.HintForeColor = System.Drawing.Color.Empty;
            this.userNameTxtbox.HintText = "";
            this.userNameTxtbox.isPassword = false;
            this.userNameTxtbox.LineFocusedColor = System.Drawing.Color.Yellow;
            this.userNameTxtbox.LineIdleColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.userNameTxtbox.LineMouseHoverColor = System.Drawing.Color.Yellow;
            this.userNameTxtbox.LineThickness = 2;
            this.userNameTxtbox.Location = new System.Drawing.Point(76, 128);
            this.userNameTxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.userNameTxtbox.Name = "userNameTxtbox";
            this.userNameTxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.userNameTxtbox.Size = new System.Drawing.Size(278, 33);
            this.userNameTxtbox.TabIndex = 0;
            this.userNameTxtbox.Text = "اسم المستخدم";
            this.userNameTxtbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.userNameTxtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.userNameTxtbox_KeyDown);
            this.userNameTxtbox.MouseEnter += new System.EventHandler(this.userNameTxtbox_MouseEnter);
            this.userNameTxtbox.MouseLeave += new System.EventHandler(this.userNameTxtbox_MouseLeave);
            // 
            // passWordTxtbox
            // 
            this.passWordTxtbox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.passWordTxtbox.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passWordTxtbox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.passWordTxtbox.HintForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.passWordTxtbox.HintText = "";
            this.passWordTxtbox.isPassword = true;
            this.passWordTxtbox.LineFocusedColor = System.Drawing.Color.Yellow;
            this.passWordTxtbox.LineIdleColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.passWordTxtbox.LineMouseHoverColor = System.Drawing.Color.Yellow;
            this.passWordTxtbox.LineThickness = 2;
            this.passWordTxtbox.Location = new System.Drawing.Point(76, 201);
            this.passWordTxtbox.Margin = new System.Windows.Forms.Padding(4);
            this.passWordTxtbox.Name = "passWordTxtbox";
            this.passWordTxtbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.passWordTxtbox.Size = new System.Drawing.Size(278, 33);
            this.passWordTxtbox.TabIndex = 1;
            this.passWordTxtbox.Text = "كلمة المرور";
            this.passWordTxtbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.passWordTxtbox.OnValueChanged += new System.EventHandler(this.passWordTxtbox_OnValueChanged);
            this.passWordTxtbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.passWordTxtbox_KeyDown);
            this.passWordTxtbox.MouseEnter += new System.EventHandler(this.passWordTxtbox_MouseEnter);
            this.passWordTxtbox.MouseLeave += new System.EventHandler(this.passWordTxtbox_MouseLeave);
            // 
            // exit
            // 
            this.exit.Image = global::iGOLD.Properties.Resources.icons8_Shutdown_32;
            this.exit.Location = new System.Drawing.Point(326, 53);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(28, 22);
            this.exit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.exit.TabIndex = 5;
            this.exit.TabStop = false;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            this.exit.MouseLeave += new System.EventHandler(this.exit_MouseLeave);
            this.exit.MouseHover += new System.EventHandler(this.exit_MouseHover);
            // 
            // login_btn
            // 
            this.login_btn.ActiveBorderThickness = 1;
            this.login_btn.ActiveCornerRadius = 20;
            this.login_btn.ActiveFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.login_btn.ActiveForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(32)))));
            this.login_btn.ActiveLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.login_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(32)))));
            this.login_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("login_btn.BackgroundImage")));
            this.login_btn.ButtonText = "تسجيل الدخول";
            this.login_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.login_btn.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_btn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.login_btn.IdleBorderThickness = 1;
            this.login_btn.IdleCornerRadius = 20;
            this.login_btn.IdleFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(32)))));
            this.login_btn.IdleForecolor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.login_btn.IdleLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(199)))), ((int)(((byte)(38)))));
            this.login_btn.Location = new System.Drawing.Point(88, 278);
            this.login_btn.Margin = new System.Windows.Forms.Padding(5);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(230, 41);
            this.login_btn.TabIndex = 4;
            this.login_btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            // 
            // bar
            // 
            this.bar.Image = global::iGOLD.Properties.Resources.igold331;
            this.bar.Location = new System.Drawing.Point(65, 25);
            this.bar.Name = "bar";
            this.bar.Size = new System.Drawing.Size(278, 43);
            this.bar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.bar.TabIndex = 1;
            this.bar.TabStop = false;
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
            // passVisible
            // 
            this.passVisible.AutoSize = true;
            this.passVisible.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passVisible.ForeColor = System.Drawing.Color.Khaki;
            this.passVisible.Location = new System.Drawing.Point(89, 242);
            this.passVisible.Name = "passVisible";
            this.passVisible.Size = new System.Drawing.Size(89, 20);
            this.passVisible.TabIndex = 3;
            this.passVisible.Text = "رؤية كلمة السر";
            this.passVisible.UseVisualStyleBackColor = true;
            this.passVisible.CheckedChanged += new System.EventHandler(this.passVisible_CheckedChanged);
            // 
            // minimize
            // 
            this.minimize.Image = global::iGOLD.Properties.Resources.icons8_Minus_32;
            this.minimize.Location = new System.Drawing.Point(301, 53);
            this.minimize.Name = "minimize";
            this.minimize.Size = new System.Drawing.Size(28, 22);
            this.minimize.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.minimize.TabIndex = 6;
            this.minimize.TabStop = false;
            this.minimize.Click += new System.EventHandler(this.minimize_Click);
            this.minimize.MouseLeave += new System.EventHandler(this.minimize_MouseLeave);
            this.minimize.MouseHover += new System.EventHandler(this.minimize_MouseHover);
            // 
            // Login
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(38)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(413, 383);
            this.Controls.Add(this.minimize);
            this.Controls.Add(this.passVisible);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.passWordTxtbox);
            this.Controls.Add(this.userNameTxtbox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.bar);
            this.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.exit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.PictureBox bar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox exit;
        public Bunifu.Framework.UI.BunifuDragControl bunifuDragControl1;
        private System.Windows.Forms.CheckBox passVisible;
        private System.Windows.Forms.PictureBox minimize;
        public Bunifu.Framework.UI.BunifuThinButton2 login_btn;
        public Bunifu.Framework.UI.BunifuMaterialTextbox passWordTxtbox;
        public Bunifu.Framework.UI.BunifuMaterialTextbox userNameTxtbox;
    }
}