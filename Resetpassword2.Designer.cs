namespace WindowsFormsApp2
{
    partial class Resetpassword2
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
            this.NObtnResetPasswords = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnResetPasswords = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NObtnResetPasswords
            // 
            this.NObtnResetPasswords.Location = new System.Drawing.Point(172, 91);
            this.NObtnResetPasswords.Name = "NObtnResetPasswords";
            this.NObtnResetPasswords.Size = new System.Drawing.Size(95, 41);
            this.NObtnResetPasswords.TabIndex = 5;
            this.NObtnResetPasswords.Text = "取消";
            this.NObtnResetPasswords.UseVisualStyleBackColor = true;
            this.NObtnResetPasswords.Click += new System.EventHandler(this.NObtnResetPasswords_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(62, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "请问是否重置所有密码？";
            // 
            // btnResetPasswords
            // 
            this.btnResetPasswords.Location = new System.Drawing.Point(30, 91);
            this.btnResetPasswords.Name = "btnResetPasswords";
            this.btnResetPasswords.Size = new System.Drawing.Size(95, 41);
            this.btnResetPasswords.TabIndex = 3;
            this.btnResetPasswords.Text = "确认";
            this.btnResetPasswords.UseVisualStyleBackColor = true;
            this.btnResetPasswords.Click += new System.EventHandler(this.btnResetPasswords_Click);
            // 
            // Resetpassword2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 169);
            this.Controls.Add(this.NObtnResetPasswords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnResetPasswords);
            this.Name = "Resetpassword2";
            this.Text = "教师密码重置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button NObtnResetPasswords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnResetPasswords;
    }
}