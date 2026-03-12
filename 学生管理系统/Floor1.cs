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
    public partial class Floor1 : Form
    {
        private string connectionString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";
        public Floor1()
        {
            InitializeComponent();
        }
        

        private void CR101_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CR102_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR103_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR104_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR105_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR106_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR107_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR108_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR109_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR110_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR112_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR113_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR114_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR115_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR116_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR117_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR118_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR119_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CR120_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomNumber = button.Name.Substring(2);

            // 查询教室信息和课程信息
            string query = "SELECT Classrooms.RoomNumber, Classrooms.Capacity, Courses.CourseName, Teachers.Name " +
                           "FROM Classrooms " +
                           "LEFT JOIN ClassroomCourses ON Classrooms.ClassroomID = ClassroomCourses.ClassroomID " +
                           "LEFT JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                           "LEFT JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                           "LEFT JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID " +
                           "WHERE Classrooms.RoomNumber = @RoomNumber";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@RoomNumber", roomNumber);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string roomNumberResult = reader["RoomNumber"].ToString();
                                int capacity = Convert.ToInt32(reader["Capacity"]);
                                string courseName = reader["CourseName"].ToString();
                                string teacherName = reader["Name"].ToString();

                                string message = $"教室编号：{roomNumberResult}\n容量：{capacity}\n课程：{courseName}\n授课教师：{teacherName}";

                                MessageBox.Show(message, "教室信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("未找到教室信息！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Floor1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            Floorchoose floorchoose = new Floorchoose();
            floorchoose.Show();
            this.Close();
        }
    }
}
