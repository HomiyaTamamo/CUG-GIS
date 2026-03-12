using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsFormsApp2
{
    public partial class StudentForm : Form
    {
        // 连接字符串
        string connectionString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";
        public StudentForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        public string username { get; set; }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Queryresult queryResultForm = new Queryresult(username);
            queryResultForm.Show();
            this.Hide();
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();  
            form1.Show();
            this.Hide();
        }

        private void CourseChoice_Click(object sender, EventArgs e)
        {
            Choosecourse choosecourse = new Choosecourse(username);
            choosecourse.Show();
            this.Hide();
        }

        private void btnquerycourses_Click(object sender, EventArgs e)
        {
            Querycourse querycourse = new   Querycourse();
            querycourse.Show();
            
        }

        
    }
}
