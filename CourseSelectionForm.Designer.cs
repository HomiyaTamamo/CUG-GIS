namespace WindowsFormsApp2
{
    partial class CourseSelectionForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.选择课程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退选课程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看教室信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.返回ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.已选课程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 28);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(800, 422);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择课程ToolStripMenuItem,
            this.退选课程ToolStripMenuItem,
            this.已选课程ToolStripMenuItem,
            this.查看教室信息ToolStripMenuItem,
            this.返回ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 选择课程ToolStripMenuItem
            // 
            this.选择课程ToolStripMenuItem.Name = "选择课程ToolStripMenuItem";
            this.选择课程ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.选择课程ToolStripMenuItem.Text = "选择课程";
            this.选择课程ToolStripMenuItem.Click += new System.EventHandler(this.选择课程ToolStripMenuItem_Click);
            // 
            // 退选课程ToolStripMenuItem
            // 
            this.退选课程ToolStripMenuItem.Name = "退选课程ToolStripMenuItem";
            this.退选课程ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.退选课程ToolStripMenuItem.Text = "退选课程";
            this.退选课程ToolStripMenuItem.Click += new System.EventHandler(this.退选课程ToolStripMenuItem_Click);
            // 
            // 查看教室信息ToolStripMenuItem
            // 
            this.查看教室信息ToolStripMenuItem.Name = "查看教室信息ToolStripMenuItem";
            this.查看教室信息ToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
            this.查看教室信息ToolStripMenuItem.Text = "查看教室信息";
            this.查看教室信息ToolStripMenuItem.Click += new System.EventHandler(this.查看教室信息ToolStripMenuItem_Click);
            // 
            // 返回ToolStripMenuItem
            // 
            this.返回ToolStripMenuItem.Name = "返回ToolStripMenuItem";
            this.返回ToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.返回ToolStripMenuItem.Text = "返回";
            this.返回ToolStripMenuItem.Click += new System.EventHandler(this.返回ToolStripMenuItem_Click);
            // 
            // 已选课程ToolStripMenuItem
            // 
            this.已选课程ToolStripMenuItem.Name = "已选课程ToolStripMenuItem";
            this.已选课程ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.已选课程ToolStripMenuItem.Text = "已选课程";
            this.已选课程ToolStripMenuItem.Click += new System.EventHandler(this.已选课程ToolStripMenuItem_Click);
            // 
            // CourseSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CourseSelectionForm";
            this.Text = "学生选课系统";
            this.Load += new System.EventHandler(this.CourseSelectionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 选择课程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退选课程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看教室信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 返回ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 已选课程ToolStripMenuItem;
    }
}