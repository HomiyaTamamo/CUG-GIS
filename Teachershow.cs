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
    public partial class Teachershow : Form
    {
        // 连接字符串
        string connectionString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";
        public Teachershow()
        {
            InitializeComponent();
        }
        public int TeacherID { get; set; }

        private void Teachershow_Load(object sender, EventArgs e)
        {
            LoadTeacherCourses();
        }
        private void LoadTeacherCourses()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Courses " +
                                   "INNER JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                                   "WHERE TeacherCourses.TeacherID = @TeacherID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TeacherID", TeacherID);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // 使用dataTable中的数据填充DataGridView显示教师所教授的课程信息
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
    }
}
