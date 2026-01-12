using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Passwordchance : Form
    {
        private string connectionString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";
        

        public Passwordchance()
        {
            InitializeComponent();
            
        }

          
        private void Passwordchance_Load(object sender, EventArgs e)
        {

        }

        private void btnPwtrue_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text.Trim();
            string currentPassword = txtpassword.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("请填写完整信息！", "修改密码", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ValidateStudentLogin(username, currentPassword))
            {
                if (UpdateStudentPassword(username, newPassword))
                {
                    MessageBox.Show("密码修改成功！", "修改密码", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("密码修改失败！", "修改密码", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (ValidateTeacherLogin(username, currentPassword))
            {
                if (UpdateTeacherPassword(username, newPassword))
                {
                    MessageBox.Show("密码修改成功！", "修改密码", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1 form1 = new Form1();
                    form1.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("密码修改失败！", "修改密码", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("用户名或当前密码错误！", "修改密码", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool UpdateStudentPassword(string username, string newPassword)
        {
            bool isSuccess = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Passwords SET Password = @NewPassword WHERE StudentID = @StudentID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NewPassword", newPassword);
                    command.Parameters.AddWithValue("@StudentID", username);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }

            return isSuccess;
        }
        private bool UpdateTeacherPassword(string username, string newPassword)
        {
            bool isSuccess = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE TeachersPassword SET Password = @NewPassword WHERE TeacherID = @TeacherID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@NewPassword", newPassword);
                    command.Parameters.AddWithValue("@TeacherID", username);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }

            return isSuccess;
        }

        private void btnPwfalse_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
    }
}
