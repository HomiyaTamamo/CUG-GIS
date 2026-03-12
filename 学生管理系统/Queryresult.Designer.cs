namespace WindowsFormsApp2
{
    partial class Queryresult
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
            this.GPAquery = new System.Windows.Forms.Button();
            this.GPArank = new System.Windows.Forms.Button();
            this.Gradequery = new System.Windows.Forms.Button();
            this.ReturnSF = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GPAquery
            // 
            this.GPAquery.Location = new System.Drawing.Point(82, 89);
            this.GPAquery.Name = "GPAquery";
            this.GPAquery.Size = new System.Drawing.Size(220, 83);
            this.GPAquery.TabIndex = 0;
            this.GPAquery.Text = "平均绩点查询";
            this.GPAquery.UseVisualStyleBackColor = true;
            this.GPAquery.Click += new System.EventHandler(this.GPAquery_Click);
            // 
            // GPArank
            // 
            this.GPArank.Location = new System.Drawing.Point(426, 89);
            this.GPArank.Name = "GPArank";
            this.GPArank.Size = new System.Drawing.Size(220, 83);
            this.GPArank.TabIndex = 1;
            this.GPArank.Text = "绩点排名查询";
            this.GPArank.UseVisualStyleBackColor = true;
            this.GPArank.Click += new System.EventHandler(this.GPArank_Click);
            // 
            // Gradequery
            // 
            this.Gradequery.Location = new System.Drawing.Point(82, 234);
            this.Gradequery.Name = "Gradequery";
            this.Gradequery.Size = new System.Drawing.Size(220, 83);
            this.Gradequery.TabIndex = 2;
            this.Gradequery.Text = "各科成绩查询";
            this.Gradequery.UseVisualStyleBackColor = true;
            this.Gradequery.Click += new System.EventHandler(this.Gradequery_Click);
            // 
            // ReturnSF
            // 
            this.ReturnSF.Location = new System.Drawing.Point(426, 234);
            this.ReturnSF.Name = "ReturnSF";
            this.ReturnSF.Size = new System.Drawing.Size(220, 83);
            this.ReturnSF.TabIndex = 3;
            this.ReturnSF.Text = "返回上个界面";
            this.ReturnSF.UseVisualStyleBackColor = true;
            this.ReturnSF.Click += new System.EventHandler(this.ReturnSF_Click);
            // 
            // Queryresult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ReturnSF);
            this.Controls.Add(this.Gradequery);
            this.Controls.Add(this.GPArank);
            this.Controls.Add(this.GPAquery);
            this.Name = "Queryresult";
            this.Text = "成绩查询";
            this.Load += new System.EventHandler(this.Querycourse_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GPAquery;
        private System.Windows.Forms.Button GPArank;
        private System.Windows.Forms.Button Gradequery;
        private System.Windows.Forms.Button ReturnSF;
    }
}