namespace WindowsFormsApp2
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.userlogin = new System.Windows.Forms.Button();
            this.adminlogin = new System.Windows.Forms.Button();
            this.buttomexit = new System.Windows.Forms.Button();
            this.Findpassword = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.teacherlogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "账号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(214, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码：";
            // 
            // txtusername
            // 
            this.txtusername.Location = new System.Drawing.Point(292, 85);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(284, 25);
            this.txtusername.TabIndex = 2;
            this.txtusername.TextChanged += new System.EventHandler(this.txtusername_TextChanged);
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(292, 145);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.PasswordChar = '*';
            this.txtpassword.Size = new System.Drawing.Size(284, 25);
            this.txtpassword.TabIndex = 3;
            this.txtpassword.TextChanged += new System.EventHandler(this.txtpassword_TextChanged);
            // 
            // userlogin
            // 
            this.userlogin.Location = new System.Drawing.Point(171, 199);
            this.userlogin.Name = "userlogin";
            this.userlogin.Size = new System.Drawing.Size(193, 73);
            this.userlogin.TabIndex = 4;
            this.userlogin.Text = "学生登录";
            this.userlogin.UseVisualStyleBackColor = true;
            this.userlogin.Click += new System.EventHandler(this.userlogin_Click_1);
            // 
            // adminlogin
            // 
            this.adminlogin.Location = new System.Drawing.Point(171, 308);
            this.adminlogin.Name = "adminlogin";
            this.adminlogin.Size = new System.Drawing.Size(193, 73);
            this.adminlogin.TabIndex = 5;
            this.adminlogin.Text = "管理员登录";
            this.adminlogin.UseVisualStyleBackColor = true;
            this.adminlogin.Click += new System.EventHandler(this.adminlogin_Click);
            // 
            // buttomexit
            // 
            this.buttomexit.Location = new System.Drawing.Point(449, 308);
            this.buttomexit.Name = "buttomexit";
            this.buttomexit.Size = new System.Drawing.Size(200, 73);
            this.buttomexit.TabIndex = 6;
            this.buttomexit.Text = "退出";
            this.buttomexit.UseVisualStyleBackColor = true;
            this.buttomexit.Click += new System.EventHandler(this.buttomexit_Click);
            // 
            // Findpassword
            // 
            this.Findpassword.AutoSize = true;
            this.Findpassword.Location = new System.Drawing.Point(615, 152);
            this.Findpassword.Name = "Findpassword";
            this.Findpassword.Size = new System.Drawing.Size(67, 15);
            this.Findpassword.TabIndex = 7;
            this.Findpassword.TabStop = true;
            this.Findpassword.Text = "找回密码";
            this.Findpassword.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Findpassword_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(615, 95);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(97, 15);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "修改个人信息";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // teacherlogin
            // 
            this.teacherlogin.Location = new System.Drawing.Point(449, 199);
            this.teacherlogin.Name = "teacherlogin";
            this.teacherlogin.Size = new System.Drawing.Size(200, 73);
            this.teacherlogin.TabIndex = 9;
            this.teacherlogin.Text = "教师登录";
            this.teacherlogin.UseVisualStyleBackColor = true;
            this.teacherlogin.Click += new System.EventHandler(this.teacherlogin_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.teacherlogin);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.Findpassword);
            this.Controls.Add(this.buttomexit);
            this.Controls.Add(this.adminlogin);
            this.Controls.Add(this.userlogin);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.txtusername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "学生课程学习管理与成绩评价系统";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.Button userlogin;
        private System.Windows.Forms.Button adminlogin;
        private System.Windows.Forms.Button buttomexit;
        private System.Windows.Forms.LinkLabel Findpassword;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button teacherlogin;
    }
}

