namespace WindowsFormsApp2
{
    partial class Viewpassword2
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
            this.Vpwexit = new System.Windows.Forms.Button();
            this.dgvallpw = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvallpw)).BeginInit();
            this.SuspendLayout();
            // 
            // Vpwexit
            // 
            this.Vpwexit.Location = new System.Drawing.Point(700, 401);
            this.Vpwexit.Name = "Vpwexit";
            this.Vpwexit.Size = new System.Drawing.Size(100, 48);
            this.Vpwexit.TabIndex = 3;
            this.Vpwexit.Text = "exit";
            this.Vpwexit.UseVisualStyleBackColor = true;
            this.Vpwexit.Click += new System.EventHandler(this.Vpwexit_Click);
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
            this.dgvallpw.TabIndex = 2;
            this.dgvallpw.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvallpw_CellContentClick);
            // 
            // Viewpassword2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Vpwexit);
            this.Controls.Add(this.dgvallpw);
            this.Name = "Viewpassword2";
            this.Text = "查看教师密码";
            this.Load += new System.EventHandler(this.Viewpassword2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvallpw)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Vpwexit;
        private System.Windows.Forms.DataGridView dgvallpw;
    }
}