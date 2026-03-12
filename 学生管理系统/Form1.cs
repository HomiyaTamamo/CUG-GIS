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
    public partial class Form1 : Form
    {
        private Dictionary<string, string> adminAccounts;
        private string connectionString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234"; 

        public Form1()
        {
            InitializeComponent();
            InitializeAccounts();
        }
        public string username { get; set; }
        private void InitializeAccounts()
        {
            // 初始化管理员账户
            adminAccounts = new Dictionary<string, string>
            {
                { "admin", "1234" },
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void userlogin_Click_1(object sender, EventArgs e)
        {
            // 获取用户登录信息
            string username = txtusername.Text.Trim();
            string password = txtpassword.Text.Trim();

            // 判断是否为空
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("账号或密码不能为空！", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtusername.Focus();
                txtpassword.Focus();
                return;
            }

            // 学生登录验证
            if (ValidateStudentLogin(username, password))
            {
                MessageBox.Show("学生登录成功！", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StudentForm studentForm = new StudentForm(username);
                studentForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("学生账号或密码错误！", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateStudentLogin(string username, string password)
        {
            bool isValid = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Password FROM Passwords WHERE StudentID = @StudentID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StudentID", username);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string storedPassword = reader["Password"].ToString();
                        if (password == storedPassword)
                        {
                            isValid = true;
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }

            return isValid;
        }

        private void adminlogin_Click(object sender, EventArgs e)
        {
            // 获取用户登录信息
            username = txtusername.Text.Trim();
            string password = txtpassword.Text.Trim();

            // 判断是否为空
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("账号或密码不能为空！", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtusername.Focus();
                txtpassword.Focus();
                return;
            }

            // 管理员登录验证
            if (adminAccounts.ContainsKey(username) && adminAccounts[username] == password)
            {
                MessageBox.Show("管理员登录成功！", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AdminForm adminForm = new AdminForm();
                adminForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("管理员账号或密码错误！", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttomexit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void Findpassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string username = txtusername.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("请输入用户名！", "找回密码", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (adminAccounts.ContainsKey(username))
            {
                // 管理员用户重置密码为1234
                adminAccounts[username] = "1234";
                MessageBox.Show("管理员密码已重置为默认密码！", "找回密码", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (ValidateStudentUsername(username))
            {
                // 学生用户重置密码为0000
                ResetStudentPassword(username);
                MessageBox.Show("学生用户密码已重置为默认密码0000。", "找回密码", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (ValidateTeacherUsername(username))
            {
                // 教师用户重置密码为0000
                ResetTeacherPassword(username);
                MessageBox.Show("教师用户密码已重置。", "找回密码", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("用户名不存在！", "找回密码", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateStudentUsername(string username)
        {
            bool isValid = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Students WHERE StudentID = @StudentID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StudentID", username);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        isValid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }

            return isValid;
        }

        private void ResetStudentPassword(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Passwords SET Password = '0000' WHERE StudentID = @StudentID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StudentID", username);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Passwordchance  passwordchance=new Passwordchance();
            passwordchance.Show();
            this.Hide();
        }

        private void teacherlogin_Click(object sender, EventArgs e)
        {
            // 获取用户登录信息
            string username = txtusername.Text.Trim();
            string password = txtpassword.Text.Trim();

            // 判断是否为空
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("账号或密码不能为空！", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtusername.Focus();
                txtpassword.Focus();
                return;
            }

            // 教师登录验证
            if (ValidateTeacherLogin(username, password))
            {
                MessageBox.Show("教师登录成功！", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TeacherForm teacherForm = new TeacherForm();
                teacherForm.username = username; // 将输入的用户名赋值给TeacherForm的username属性
                teacherForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("教师账号或密码错误！", "登录提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateTeacherLogin(string username, string password)
        {
            bool isValid = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Password FROM TeachersPassword WHERE TeacherID = @TeacherID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TeacherID", username);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string storedPassword = reader["Password"].ToString();
                        if (password == storedPassword)
                        {
                            isValid = true;
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }

            return isValid;
        }
        private bool ValidateTeacherUsername(string username)
        {
            bool isValid = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM TeachersPassword WHERE TeacherID = @TeacherID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TeacherID", username);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        isValid = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }

            return isValid;
        }
        private void ResetTeacherPassword(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE TeachersPassword SET Password = '0000' WHERE TeacherID = @TeacherID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TeacherID", username);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }
        }
    }
}



