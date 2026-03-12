namespace WindowsFormsApp2
{
    partial class AdminForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.学生信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看学生用户密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重置学生用户密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增学生ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.教师信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看教师课程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看教师密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重置教师密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.教室信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看教室信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出登录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.学生信息ToolStripMenuItem,
            this.教师信息ToolStripMenuItem,
            this.教室信息ToolStripMenuItem,
            this.退出登录ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 学生信息ToolStripMenuItem
            // 
            this.学生信息ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看学生用户密码ToolStripMenuItem,
            this.重置学生用户密码ToolStripMenuItem,
            this.新增学生ToolStripMenuItem});
            this.学生信息ToolStripMenuItem.Name = "学生信息ToolStripMenuItem";
            this.学生信息ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.学生信息ToolStripMenuItem.Text = "学生信息";
            this.学生信息ToolStripMenuItem.Click += new System.EventHandler(this.学生信息编辑ToolStripMenuItem_Click);
            // 
            // 查看学生用户密码ToolStripMenuItem
            // 
            this.查看学生用户密码ToolStripMenuItem.Name = "查看学生用户密码ToolStripMenuItem";
            this.查看学生用户密码ToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.查看学生用户密码ToolStripMenuItem.Text = "查看学生用户密码";
            this.查看学生用户密码ToolStripMenuItem.Click += new System.EventHandler(this.查看学生用户密码ToolStripMenuItem_Click);
            // 
            // 重置学生用户密码ToolStripMenuItem
            // 
            this.重置学生用户密码ToolStripMenuItem.Name = "重置学生用户密码ToolStripMenuItem";
            this.重置学生用户密码ToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.重置学生用户密码ToolStripMenuItem.Text = "重置学生用户密码";
            this.重置学生用户密码ToolStripMenuItem.Click += new System.EventHandler(this.重置学生用户密码ToolStripMenuItem_Click);
            // 
            // 新增学生ToolStripMenuItem
            // 
            this.新增学生ToolStripMenuItem.Name = "新增学生ToolStripMenuItem";
            this.新增学生ToolStripMenuItem.Size = new System.Drawing.Size(212, 26);
            this.新增学生ToolStripMenuItem.Text = "学生信息编辑";
            this.新增学生ToolStripMenuItem.Click += new System.EventHandler(this.新增学生ToolStripMenuItem_Click);
            // 
            // 教师信息ToolStripMenuItem
            // 
            this.教师信息ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看教师课程ToolStripMenuItem,
            this.查看教师密码ToolStripMenuItem,
            this.重置教师密码ToolStripMenuItem});
            this.教师信息ToolStripMenuItem.Name = "教师信息ToolStripMenuItem";
            this.教师信息ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.教师信息ToolStripMenuItem.Text = "教师信息";
            // 
            // 查看教师课程ToolStripMenuItem
            // 
            this.查看教师课程ToolStripMenuItem.Name = "查看教师课程ToolStripMenuItem";
            this.查看教师课程ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.查看教师课程ToolStripMenuItem.Text = "查看教师课程";
            this.查看教师课程ToolStripMenuItem.Click += new System.EventHandler(this.查看教师课程ToolStripMenuItem_Click);
            // 
            // 查看教师密码ToolStripMenuItem
            // 
            this.查看教师密码ToolStripMenuItem.Name = "查看教师密码ToolStripMenuItem";
            this.查看教师密码ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.查看教师密码ToolStripMenuItem.Text = "查看教师密码";
            this.查看教师密码ToolStripMenuItem.Click += new System.EventHandler(this.查看教师密码ToolStripMenuItem_Click);
            // 
            // 重置教师密码ToolStripMenuItem
            // 
            this.重置教师密码ToolStripMenuItem.Name = "重置教师密码ToolStripMenuItem";
            this.重置教师密码ToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.重置教师密码ToolStripMenuItem.Text = "重置教师密码";
            this.重置教师密码ToolStripMenuItem.Click += new System.EventHandler(this.重置教师密码ToolStripMenuItem_Click);
            // 
            // 教室信息ToolStripMenuItem
            // 
            this.教室信息ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看教室信息ToolStripMenuItem});
            this.教室信息ToolStripMenuItem.Name = "教室信息ToolStripMenuItem";
            this.教室信息ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.教室信息ToolStripMenuItem.Text = "教室信息";
            this.教室信息ToolStripMenuItem.Click += new System.EventHandler(this.教室信息ToolStripMenuItem_Click);
            // 
            // 查看教室信息ToolStripMenuItem
            // 
            this.查看教室信息ToolStripMenuItem.Name = "查看教室信息ToolStripMenuItem";
            this.查看教室信息ToolStripMenuItem.Size = new System.Drawing.Size(182, 26);
            this.查看教室信息ToolStripMenuItem.Text = "编辑教室信息";
            this.查看教室信息ToolStripMenuItem.Click += new System.EventHandler(this.查看教室信息ToolStripMenuItem_Click);
            // 
            // 退出登录ToolStripMenuItem
            // 
            this.退出登录ToolStripMenuItem.Name = "退出登录ToolStripMenuItem";
            this.退出登录ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.退出登录ToolStripMenuItem.Text = "退出登录";
            this.退出登录ToolStripMenuItem.Click += new System.EventHandler(this.退出登录ToolStripMenuItem_Click);
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AdminForm";
            this.Text = "管理员模式";
            this.Load += new System.EventHandler(this.adminform_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 学生信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看学生用户密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重置学生用户密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新增学生ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出登录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 教师信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看教师课程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看教师密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 重置教师密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 教室信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看教室信息ToolStripMenuItem;
    }
}