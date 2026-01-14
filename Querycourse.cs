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
    public partial class Querycourse : Form
    {
        string connectionString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";
        public Querycourse()
        {
            InitializeComponent();
        }




        private void Querycourse_Load(object sender, EventArgs e)
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

                    string query = "SELECT Courses.CourseID, Courses.CourseType,Courses.Capacity ,Courses.Credits,Courses.ClassTime,Courses.CourseName, Teachers.Name AS TeacherName " +
                                   "FROM Courses " +
                                   "INNER JOIN TeacherCourses ON Courses.CourseID = TeacherCourses.CourseID " +
                                   "INNER JOIN Teachers ON TeacherCourses.TeacherID = Teachers.TeacherID";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dgvcourse.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }
        }

        private void dgvcourse_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            AdminForm adminForm = new AdminForm();
            adminForm.Show();
            this.Hide();
        }
    }
}


