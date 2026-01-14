namespace WindowsFormsApp2
{
    partial class Querycourse
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
            this.components = new System.ComponentModel.Container();
            this.masterDataSet3 = new WindowsFormsApp2.masterDataSet3();
            this.masterDataSet3BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dgvcourse = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.masterDataSet3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.masterDataSet3BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvcourse)).BeginInit();
            this.SuspendLayout();
            // 
            // masterDataSet3
            // 
            this.masterDataSet3.DataSetName = "masterDataSet3";
            this.masterDataSet3.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // masterDataSet3BindingSource
            // 
            this.masterDataSet3BindingSource.DataSource = this.masterDataSet3;
            this.masterDataSet3BindingSource.Position = 0;
            // 
            // dgvcourse
            // 
            this.dgvcourse.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvcourse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvcourse.Location = new System.Drawing.Point(0, 0);
            this.dgvcourse.Name = "dgvcourse";
            this.dgvcourse.RowHeadersWidth = 51;
            this.dgvcourse.RowTemplate.Height = 27;
            this.dgvcourse.Size = new System.Drawing.Size(800, 450);
            this.dgvcourse.TabIndex = 0;
            this.dgvcourse.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvcourse_CellContentClick);
            // 
            // Querycourse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvcourse);
            this.Name = "Querycourse";
            this.Text = "课程信息";
            this.Load += new System.EventHandler(this.Querycourse_Load);
            ((System.ComponentModel.ISupportInitialize)(this.masterDataSet3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.masterDataSet3BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvcourse)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private masterDataSet3 masterDataSet3;
        private System.Windows.Forms.BindingSource masterDataSet3BindingSource;
        private System.Windows.Forms.DataGridView dgvcourse;
    }
}