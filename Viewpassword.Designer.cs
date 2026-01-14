namespace WindowsFormsApp2
{
    partial class Viewpassword
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
            this.dgvallpw = new System.Windows.Forms.DataGridView();
            this.Vpwexit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvallpw)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvallpw
            // 
            this.dgvallpw.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvallpw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvallpw.Location = new System.Drawing.Point(0, 0);
            this.dgvallpw.Name = "dgvallpw";
            this.dgvallpw.RowHeadersWidth = 51;
            this.dgvallpw.RowTemplate.Height = 27;
            this.dgvallpw.Size = new System.Drawing.Size(800, 450);
            this.dgvallpw.TabIndex = 0;
            this.dgvallpw.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvallpw_CellContentClick);
            // 
            // Vpwexit
            // 
            this.Vpwexit.Location = new System.Drawing.Point(699, 402);
            this.Vpwexit.Name = "Vpwexit";
            this.Vpwexit.Size = new System.Drawing.Size(100, 47);
            this.Vpwexit.TabIndex = 1;
            this.Vpwexit.Text = "exit";
            this.Vpwexit.UseVisualStyleBackColor = true;
            this.Vpwexit.Click += new System.EventHandler(this.Vpwexit_Click);
            // 
            // Viewpassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Vpwexit);
            this.Controls.Add(this.dgvallpw);
            this.Name = "Viewpassword";
            this.Text = "查看学生密码";
            this.Load += new System.EventHandler(this.Passwordchance_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvallpw)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvallpw;
        private System.Windows.Forms.Button Vpwexit;
    }
}