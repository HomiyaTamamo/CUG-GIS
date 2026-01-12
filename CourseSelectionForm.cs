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
    public partial class CourseSelectionForm : Form
    {
        string connectionString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";
        public string username { get; set; }
        public int studentID { get; set; }
        public CourseSelectionForm()
        {
            InitializeComponent();
            this.username = username;
            this .studentID = studentID;
        }


        public CourseSelectionForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void LoadCourses()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT C.CourseID, C.CourseNumber, C.CourseName, C.CourseType, C.Credits, C.ClassTime, C.Capacity, C.CourseLocation 
                                     FROM Courses C
                                     LEFT JOIN StudentCourses SC ON C.CourseID = SC.CourseID AND SC.StudentID = (SELECT StudentID FROM Students WHERE StudentID = @Username)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }
        }
        private void LoadSelectedCourses()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT C.CourseID, C.CourseNumber, C.CourseName, C.CourseType, C.Credits, C.ClassTime, C.Capacity, C.CourseLocation 
                                     FROM Courses C
                                     INNER JOIN StudentCourses SC ON C.CourseID = SC.CourseID
                                     WHERE SC.StudentID = (SELECT StudentID FROM Students WHERE StudentID = @Username)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }
        }

        private void CourseSelectionForm_Load(object sender, EventArgs e)
        {
            LoadCourses();
        }

        private void 选择课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedCourseID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CourseID"].Value);

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // 检查课程ID是否存在
                        string courseCheckQuery = "SELECT COUNT(*) FROM Courses WHERE CourseID = @CourseID";
                        SqlCommand courseCheckCommand = new SqlCommand(courseCheckQuery, connection);
                        courseCheckCommand.Parameters.AddWithValue("@CourseID", selectedCourseID);
                        int courseCount = Convert.ToInt32(courseCheckCommand.ExecuteScalar());

                        if (courseCount == 0)
                        {
                            MessageBox.Show("选课失败，课程ID不存在！");
                            return;
                        }

                        // 检查学生ID是否存在
                        string studentCheckQuery = "SELECT COUNT(*) FROM Students WHERE StudentID = @Username";
                        SqlCommand studentCheckCommand = new SqlCommand(studentCheckQuery, connection);
                        studentCheckCommand.Parameters.AddWithValue("@Username", username);
                        int studentCount = Convert.ToInt32(studentCheckCommand.ExecuteScalar());

                        if (studentCount == 0)
                        {
                            MessageBox.Show("选课失败，学生ID不存在！");
                            return;
                        }

                        // 检查是否存在时间冲突的课程
                        string conflictQuery = @"SELECT COUNT(*) 
                                        FROM StudentCourses SC
                                        INNER JOIN Courses C ON SC.CourseID = C.CourseID
                                        WHERE SC.StudentID = (SELECT StudentID FROM Students WHERE StudentID = @Username) AND C.ClassTime = (SELECT ClassTime FROM Courses WHERE CourseID = @CourseID)";

                        SqlCommand conflictCommand = new SqlCommand(conflictQuery, connection);
                        conflictCommand.Parameters.AddWithValue("@Username", username);
                        conflictCommand.Parameters.AddWithValue("@CourseID", selectedCourseID);

                        int conflictCount = Convert.ToInt32(conflictCommand.ExecuteScalar());

                        if (conflictCount > 0)
                        {
                            MessageBox.Show("选课失败，存在时间冲突的课程！");
                            return;
                        }

                        // 添加选课记录
                        string insertQuery = "INSERT INTO StudentCourses (StudentID, CourseID) VALUES ((SELECT StudentID FROM Students WHERE StudentID = @Username), @CourseID)";

                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@Username", username);
                        insertCommand.Parameters.AddWithValue("@CourseID", selectedCourseID);

                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("选课成功！");
                            LoadCourses(); // 重新加载课程列表
                        }
                        else
                        {
                            MessageBox.Show("选课失败！");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("数据库连接错误：" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("请选择要选修的课程！");
            }
        }

        private void 退选课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedCourseID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CourseID"].Value);

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // 检查学生ID是否存在
                        string studentCheckQuery = "SELECT COUNT(*) FROM Students WHERE StudentID = @Username";
                        SqlCommand studentCheckCommand = new SqlCommand(studentCheckQuery, connection);
                        studentCheckCommand.Parameters.AddWithValue("@Username", username);
                        int studentCount = Convert.ToInt32(studentCheckCommand.ExecuteScalar());

                        if (studentCount == 0)
                        {
                            MessageBox.Show("退课失败，学生ID不存在！");
                            return;
                        }

                        // 删除选课记录
                        string deleteQuery = "DELETE FROM StudentCourses WHERE StudentID = (SELECT StudentID FROM Students WHERE StudentID = @Username) AND CourseID = @CourseID";

                        SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                        deleteCommand.Parameters.AddWithValue("@Username", username);
                        deleteCommand.Parameters.AddWithValue("@CourseID", selectedCourseID);

                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("退课成功！");
                            LoadCourses(); // 重新加载课程列表
                        }
                        else
                        {
                            MessageBox.Show("退课失败！");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("数据库连接错误：" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("请选择要退选的课程！");
            }
        }

        private void 查看教室信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Floorchoose floorchoose = new Floorchoose();
            floorchoose.Show();
        }

        private void 返回ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 已选课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadSelectedCourses();
        }
    }
}
