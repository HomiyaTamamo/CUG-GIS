namespace WindowsFormsApp2
{
    partial class Passwordchance
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
            this.txtusername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.btnPwtrue = new System.Windows.Forms.Button();
            this.btnPwfalse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtusername
            // 
            this.txtusername.Location = new System.Drawing.Point(223, 78);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(476, 25);
            this.txtusername.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "账号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 156);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "旧密码";
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(223, 153);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Size = new System.Drawing.Size(476, 25);
            this.txtpassword.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "新密码";
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(223, 237);
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.Size = new System.Drawing.Size(476, 25);
            this.txtNewPassword.TabIndex = 4;
            // 
            // btnPwtrue
            // 
            this.btnPwtrue.Location = new System.Drawing.Point(194, 347);
            this.btnPwtrue.Name = "btnPwtrue";
            this.btnPwtrue.Size = new System.Drawing.Size(138, 48);
            this.btnPwtrue.TabIndex = 6;
            this.btnPwtrue.Text = "确认";
            this.btnPwtrue.UseVisualStyleBackColor = true;
            this.btnPwtrue.Click += new System.EventHandler(this.btnPwtrue_Click);
            // 
            // btnPwfalse
            // 
            this.btnPwfalse.Location = new System.Drawing.Point(474, 347);
            this.btnPwfalse.Name = "btnPwfalse";
            this.btnPwfalse.Size = new System.Drawing.Size(138, 48);
            this.btnPwfalse.TabIndex = 7;
            this.btnPwfalse.Text = "取消";
            this.btnPwfalse.UseVisualStyleBackColor = true;
            this.btnPwfalse.Click += new System.EventHandler(this.btnPwfalse_Click);
            // 
            // Passwordchance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnPwfalse);
            this.Controls.Add(this.btnPwtrue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNewPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtusername);
            this.Name = "Passwordchance";
            this.Text = "更改密码";
            this.Load += new System.EventHandler(this.Passwordchance_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Button btnPwtrue;
        private System.Windows.Forms.Button btnPwfalse;
    }
}