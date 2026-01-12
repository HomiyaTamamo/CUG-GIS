using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class GradesEditForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";
        private DataTable gradesTable;
        public int TeacherID { get; set; }
        public GradesEditForm()
        {
            InitializeComponent();
        }

        private void GradesEditForm_Load(object sender, EventArgs e)
        {
            LoadGradesFromDatabase();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void LoadGradesFromDatabase()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT StudentID, CourseID, Grade FROM StudentCourses " +
                                   "WHERE CourseID IN (SELECT CourseID FROM TeacherCourses WHERE TeacherID = @TeacherID)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TeacherID", TeacherID);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gradesTable = dataTable;
                    dataGridView1.DataSource = gradesTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }
        }
        //private void GEFsave()
        //{
            
        //}

        public void SetGradesTable(DataTable table)
        {
            gradesTable = table;
            dataGridView1.DataSource = gradesTable;
        }

        public DataTable GetGradesTable()
        {
            return gradesTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (DataRow row in gradesTable.Rows)
                    {
                        int studentID = Convert.ToInt32(row["StudentID"]);
                        int courseID = Convert.ToInt32(row["CourseID"]);
                        int grade = Convert.ToInt32(row["Grade"]);

                        string updateQuery = "UPDATE StudentCourses SET Grade = @Grade WHERE StudentID = @StudentID AND CourseID = @CourseID";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@Grade", grade);
                        updateCommand.Parameters.AddWithValue("@StudentID", studentID);
                        updateCommand.Parameters.AddWithValue("@CourseID", courseID);
                        updateCommand.ExecuteNonQuery();
                    }

                    MessageBox.Show("成绩保存成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存成绩时出现错误：" + ex.Message);
            }
        }
    }
}
