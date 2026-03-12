using System;

namespace WindowsFormsApp2
{
    partial class StudentForm
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
            this.btnqueryresults = new System.Windows.Forms.Button();
            this.btnquerycourses = new System.Windows.Forms.Button();
            this.CourseChoice = new System.Windows.Forms.Button();
            this.btnexit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("华文中宋", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(84, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(647, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "欢迎进入学生个人信息查询管理系统！";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnqueryresults
            // 
            this.btnqueryresults.Location = new System.Drawing.Point(68, 140);
            this.btnqueryresults.Name = "btnqueryresults";
            this.btnqueryresults.Size = new System.Drawing.Size(248, 75);
            this.btnqueryresults.TabIndex = 1;
            this.btnqueryresults.Text = "成绩查询";
            this.btnqueryresults.UseVisualStyleBackColor = true;
            this.btnqueryresults.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnquerycourses
            // 
            this.btnquerycourses.Location = new System.Drawing.Point(483, 140);
            this.btnquerycourses.Name = "btnquerycourses";
            this.btnquerycourses.Size = new System.Drawing.Size(248, 75);
            this.btnquerycourses.TabIndex = 2;
            this.btnquerycourses.Text = "课程查询";
            this.btnquerycourses.UseVisualStyleBackColor = true;
            this.btnquerycourses.Click += new System.EventHandler(this.btnquerycourses_Click);
            // 
            // CourseChoice
            // 
            this.CourseChoice.Location = new System.Drawing.Point(68, 247);
            this.CourseChoice.Name = "CourseChoice";
            this.CourseChoice.Size = new System.Drawing.Size(248, 75);
            this.CourseChoice.TabIndex = 3;
            this.CourseChoice.Text = "选课系统";
            this.CourseChoice.UseVisualStyleBackColor = true;
            this.CourseChoice.Click += new System.EventHandler(this.CourseChoice_Click);
            // 
            // btnexit
            // 
            this.btnexit.Location = new System.Drawing.Point(483, 247);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(248, 75);
            this.btnexit.TabIndex = 4;
            this.btnexit.Text = "退出";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // StudentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnexit);
            this.Controls.Add(this.CourseChoice);
            this.Controls.Add(this.btnquerycourses);
            this.Controls.Add(this.btnqueryresults);
            this.Controls.Add(this.label1);
            this.Name = "StudentForm";
            this.Text = "学生个人信息查询";
            this.Load += new System.EventHandler(this.StudentForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void StudentForm_Load(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnqueryresults;
        private System.Windows.Forms.Button btnquerycourses;
        private System.Windows.Forms.Button CourseChoice;
        private System.Windows.Forms.Button btnexit;
    }
}