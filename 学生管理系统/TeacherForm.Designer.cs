namespace WindowsFormsApp2
{
    partial class TeacherForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.btngrade = new System.Windows.Forms.Button();
            this.btnshowme = new System.Windows.Forms.Button();
            this.btnaddcourse = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(100, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(625, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "欢迎使用学生成绩管理系统教师端！";
            // 
            // btngrade
            // 
            this.btngrade.Location = new System.Drawing.Point(121, 148);
            this.btngrade.Name = "btngrade";
            this.btngrade.Size = new System.Drawing.Size(241, 75);
            this.btngrade.TabIndex = 1;
            this.btngrade.Text = "学生成绩编辑";
            this.btngrade.UseVisualStyleBackColor = true;
            this.btngrade.Click += new System.EventHandler(this.btngrade_Click);
            // 
            // btnshowme
            // 
            this.btnshowme.Location = new System.Drawing.Point(440, 148);
            this.btnshowme.Name = "btnshowme";
            this.btnshowme.Size = new System.Drawing.Size(241, 75);
            this.btnshowme.TabIndex = 2;
            this.btnshowme.Text = "个人授课情况";
            this.btnshowme.UseVisualStyleBackColor = true;
            this.btnshowme.Click += new System.EventHandler(this.btnshowme_Click);
            // 
            // btnaddcourse
            // 
            this.btnaddcourse.Location = new System.Drawing.Point(121, 276);
            this.btnaddcourse.Name = "btnaddcourse";
            this.btnaddcourse.Size = new System.Drawing.Size(241, 75);
            this.btnaddcourse.TabIndex = 3;
            this.btnaddcourse.Text = "添加新课程";
            this.btnaddcourse.UseVisualStyleBackColor = true;
            this.btnaddcourse.Click += new System.EventHandler(this.btnaddcourse_Click);
            // 
            // btnexit
            // 
            this.btnexit.Location = new System.Drawing.Point(440, 276);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(241, 75);
            this.btnexit.TabIndex = 4;
            this.btnexit.Text = "退出登录";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // TeacherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnexit);
            this.Controls.Add(this.btnaddcourse);
            this.Controls.Add(this.btnshowme);
            this.Controls.Add(this.btngrade);
            this.Controls.Add(this.label1);
            this.Name = "TeacherForm";
            this.Text = "TeacherForm";
            this.Load += new System.EventHandler(this.TeacherForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btngrade;
        private System.Windows.Forms.Button btnshowme;
        private System.Windows.Forms.Button btnaddcourse;
        private System.Windows.Forms.Button btnexit;
    }
}