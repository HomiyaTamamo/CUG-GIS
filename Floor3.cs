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
    public partial class Floor3 : Form
    {
        private string connectionString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";
        public Floor3()
        {
            InitializeComponent();
        }

        private void CR301_Click(object sender, EventArgs e)
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

        private void CR302_Click(object sender, EventArgs e)
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

        private void CR303_Click(object sender, EventArgs e)
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

        private void CR304_Click(object sender, EventArgs e)
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

        private void CR305_Click(object sender, EventArgs e)
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

        private void CR306_Click(object sender, EventArgs e)
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

        private void CR307_Click(object sender, EventArgs e)
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

        private void CR308_Click(object sender, EventArgs e)
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

        private void CR311_Click(object sender, EventArgs e)
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

        private void CR312_Click(object sender, EventArgs e)
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

        private void CR313_Click(object sender, EventArgs e)
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

        private void CR314_Click(object sender, EventArgs e)
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

        private void CR315_Click(object sender, EventArgs e)
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

        private void CR316_Click(object sender, EventArgs e)
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

        private void CR317_Click(object sender, EventArgs e)
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

        private void CR318_Click(object sender, EventArgs e)
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

        private void Floor3_Load(object sender, EventArgs e)
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
