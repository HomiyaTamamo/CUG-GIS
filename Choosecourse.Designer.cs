namespace WindowsFormsApp2
{
    partial class Choosecourse
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
            this.btnexit = new System.Windows.Forms.Button();
            this.btncug = new System.Windows.Forms.Button();
            this.btnCRinf = new System.Windows.Forms.Button();
            this.btnchco = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnexit
            // 
            this.btnexit.Location = new System.Drawing.Point(484, 289);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(248, 75);
            this.btnexit.TabIndex = 9;
            this.btnexit.Text = "返回";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // btncug
            // 
            this.btncug.Location = new System.Drawing.Point(69, 289);
            this.btncug.Name = "btncug";
            this.btncug.Size = new System.Drawing.Size(248, 75);
            this.btncug.TabIndex = 8;
            this.btncug.Text = "学校平面图";
            this.btncug.UseVisualStyleBackColor = true;
            this.btncug.Click += new System.EventHandler(this.btncug_Click);
            // 
            // btnCRinf
            // 
            this.btnCRinf.Location = new System.Drawing.Point(484, 182);
            this.btnCRinf.Name = "btnCRinf";
            this.btnCRinf.Size = new System.Drawing.Size(248, 75);
            this.btnCRinf.TabIndex = 7;
            this.btnCRinf.Text = "查看教室信息";
            this.btnCRinf.UseVisualStyleBackColor = true;
            this.btnCRinf.Click += new System.EventHandler(this.btnCRinf_Click);
            // 
            // btnchco
            // 
            this.btnchco.Location = new System.Drawing.Point(69, 182);
            this.btnchco.Name = "btnchco";
            this.btnchco.Size = new System.Drawing.Size(248, 75);
            this.btnchco.TabIndex = 6;
            this.btnchco.Text = "自主选课";
            this.btnchco.UseVisualStyleBackColor = true;
            this.btnchco.Click += new System.EventHandler(this.btnchco_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("华文中宋", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(198, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(425, 42);
            this.label1.TabIndex = 5;
            this.label1.Text = "欢迎进入学生选课系统！";
            // 
            // Choosecourse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnexit);
            this.Controls.Add(this.btncug);
            this.Controls.Add(this.btnCRinf);
            this.Controls.Add(this.btnchco);
            this.Controls.Add(this.label1);
            this.Name = "Choosecourse";
            this.Text = "Choosecourse";
            this.Load += new System.EventHandler(this.Choosecourse_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Button btncug;
        private System.Windows.Forms.Button btnCRinf;
        private System.Windows.Forms.Button btnchco;
        private System.Windows.Forms.Label label1;
    }
}