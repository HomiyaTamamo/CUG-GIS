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
    public partial class Addcourse : Form
    {
        private int teacherID;
        public int TeacherID
        {
            get { return teacherID; }
            set { teacherID = value; }
        }
        // 连接字符串
        string connectionString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";
        public Addcourse()
        {
            InitializeComponent();
        }

        private void Addcourse_Load(object sender, EventArgs e)
        {
            LoadCourses();
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
                                     INNER JOIN TeacherCourses TC ON C.CourseID = TC.CourseID
                                     WHERE TC.TeacherID = @TeacherID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TeacherID", teacherID);

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 添加课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedCourseID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CourseID"].Value);

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string insertQuery = "INSERT INTO TeacherCourses (TeacherID, CourseID) VALUES (@TeacherID, @CourseID)";

                        SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
                        insertCommand.Parameters.AddWithValue("@TeacherID", teacherID);
                        insertCommand.Parameters.AddWithValue("@CourseID", selectedCourseID);

                        int rowsAffected = insertCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("课程添加成功！");
                            LoadCourses(); // 重新加载课程列表
                        }
                        else
                        {
                            MessageBox.Show("课程添加失败！");
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
                MessageBox.Show("请选择要添加的课程！");
            }
        }

        private void 删除课程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int selectedCourseID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["CourseID"].Value);

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string deleteQuery = "DELETE FROM TeacherCourses WHERE TeacherID = @TeacherID AND CourseID = @CourseID";

                        SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                        deleteCommand.Parameters.AddWithValue("@TeacherID", teacherID);
                        deleteCommand.Parameters.AddWithValue("@CourseID", selectedCourseID);

                        int rowsAffected = deleteCommand.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("课程删除成功！");
                        }
                        else
                        {
                            MessageBox.Show("课程删除失败！");
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
                MessageBox.Show("请选择要删除的课程！");
            }
        }

        private void 查看教室信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Floorchoose2 floorchoose2 = new Floorchoose2();
            floorchoose2.Show();
        }
    }
}
