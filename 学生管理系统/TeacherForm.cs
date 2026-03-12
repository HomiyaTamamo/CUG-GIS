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
    public partial class TeacherForm : Form
    {
        public string username { get; set; }
        // 连接字符串
        string connectionString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";

        public TeacherForm()
        {
            InitializeComponent();
        }

        public TeacherForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }
        

        private void TeacherForm_Load(object sender, EventArgs e)
        {

        }

        private void btngrade_Click(object sender, EventArgs e)
        {
            // 进行学生成绩编辑
            GradesEditForm gradesEditForm = new GradesEditForm();
            gradesEditForm.TeacherID = GetTeacherID(username);
            gradesEditForm.Show();
            
        }

        private void btnshowme_Click(object sender, EventArgs e)
        {
            // 显示教师本人所教授的课程
            Teachershow teachershow = new Teachershow();
            teachershow.TeacherID = GetTeacherID(username); 
            teachershow.Show();
        }

        private void btnaddcourse_Click(object sender, EventArgs e)
        {
            // 添加新课程
            Addcourse addcourseForm = new Addcourse();
            addcourseForm.TeacherID=GetTeacherID(username);
            addcourseForm.Show();
            
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            // 返回到登录页面
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }
        private int GetTeacherID(string username)
        {
            int teacherID = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT TeacherID FROM Teachers WHERE TeacherID = @Username";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        teacherID = Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }

            return teacherID;
        }
    }
}
