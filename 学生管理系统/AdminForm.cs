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

namespace WindowsFormsApp2
{
    public partial class AdminForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";
        public AdminForm()
        {
            InitializeComponent();
        }

        private void adminform_Load(object sender, EventArgs e)
        {

        }

        private void 查看学生用户密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Viewpassword viewpassword = new Viewpassword();
            viewpassword.Show();
            this.Hide();
        }

        private void 重置学生用户密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Resetpassword chancepassword = new Resetpassword();
            chancepassword.Show();
            this.Close();
        }

        

        
      

        private void 学生信息编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void 退出登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1= new Form1();
            form1.Show();
            this.Hide();
           
        }

        private void 新增学生ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            StudentInfoEditForm studentInfoEditForm = new StudentInfoEditForm();
            studentInfoEditForm.ShowDialog();
        }

        

        private void 查看教师密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Viewpassword2 viewpassword2 = new Viewpassword2();  
            viewpassword2.Show();
            this.Hide();
        }

        private void 重置教师密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Resetpassword2 chancepassword2 = new Resetpassword2();
            chancepassword2.Show();
            this.Close();
        }

        

        private void 查看教师课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Querycourse querycourse = new Querycourse();
            querycourse.Show();
            
        }

        private void 查看教室信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClassroomShow classroomShow= new ClassroomShow();
            classroomShow.Show();
        }

        

        private void 教室信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
