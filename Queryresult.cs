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
    public partial class Queryresult : Form
    {
        // 连接字符串
        string connectionString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";
        public string username { get; set; }
        public Queryresult(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        public Queryresult()
        {
            InitializeComponent();
            this.username = username;
        }

        private void Querycourse_Load(object sender, EventArgs e)
        {

        }

        //计算绩点
        static double CalculateGPA(int grade)
        {
            if (grade >= 90)
            {
                return 4.0;
            }
            else if (grade >= 85)
            {
                return 3.7;
            }
            else if (grade >= 82)
            {
                return 3.3;
            }
            else if (grade >= 78)
            {
                return 3.0;
            }
            else if (grade >= 75)
            {
                return 2.7;
            }
            else if (grade >= 71)
            {
                return 2.3;
            }
            else if (grade >= 66)
            {
                return 2.0;
            }
            else if (grade >= 62)
            {
                return 1.7;
            }
            else if (grade >= 60)
            {
                return 1.3;
            }
            else if (grade >= 0)
            {
                return 0.0;
            }
            else
            {
                return 0.0;
            }
        }
        // 获取课程权重系数
        static double GetCourseWeight(string courseType)
        {
            if (courseType == "基础必修")
            {
                return 1.2;
            }
            else if (courseType == "专业必修")
            {
                return 1.1;
            }
            else if (courseType == "选修")
            {
                return 1.0;
            }
            else
            {
                return 1.0;
            }
        }

        private void GPAquery_Click(object sender, EventArgs e)
        {
            // 验证学生身份
            if (ValidateStudent(username))
            {
                double averageGPA = AVGGPA();

                MessageBox.Show("平均绩点：" + averageGPA.ToString("0.00"), "绩点查询", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("学生身份验证失败！", "登录错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private double AVGGPA()
        {
            double totalGPA = 0.0;
            int totalCredits = 0;
            string studentID = username;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Courses.CourseName, StudentCourses.Grade, Courses.CourseType, Courses.Credits " +
                                   "FROM StudentCourses " +
                                   "INNER JOIN Courses ON StudentCourses.CourseID = Courses.CourseID " +
                                   "WHERE StudentCourses.StudentID = @StudentID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StudentID", username);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable gradesTable = new DataTable();
                    adapter.Fill(gradesTable);

                    foreach (DataRow row in gradesTable.Rows)
                    {
                        int grade = Convert.ToInt32(row["Grade"]);
                        string courseType = row["CourseType"].ToString();
                        int credits = Convert.ToInt32(row["Credits"]);

                        double gpa = CalculateGPA(grade);
                        double courseWeight = GetCourseWeight(courseType);

                        totalGPA += gpa * courseWeight * credits;
                        totalCredits += credits;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }

            if (totalCredits > 0)
            {
                return totalGPA / totalCredits;
            }
            else
            {
                return 0.0;
            }
        }

        private void GPArank_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT StudentID, AVG(Grade) AS AverageGrade " +
                                   "FROM StudentCourses " +
                                   "GROUP BY StudentID " +
                                   "ORDER BY AverageGrade DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable rankTable = new DataTable();
                    adapter.Fill(rankTable);

                    int rank = 1;
                    foreach (DataRow row in rankTable.Rows)
                    {
                        string studentID = row["StudentID"].ToString();
                        double averageGrade = Convert.ToDouble(row["AverageGrade"]);

                        if (studentID == username)
                        {
                            MessageBox.Show("您的平均绩点排名：" + rank, "绩点排名", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        }

                        rank++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }
        }

        private void Gradequery_Click(object sender, EventArgs e)
        {
            // 验证学生身份
            if (ValidateStudent(username))
            {
                // 查询学生成绩
                DataTable gradesTable = GetStudentGrades(username);

                // 显示学生成绩
                if (gradesTable != null && gradesTable.Rows.Count > 0)
                {
                    GradesForm gradesForm = new GradesForm();
                    gradesForm.SetGradesTable(gradesTable); // 将成绩表传递给成绩窗体
                    gradesForm.Show();
                }
                else
                {
                    MessageBox.Show("没有找到学生的成绩信息！", "成绩查询", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("学生身份验证失败！", "登录错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidateStudent(string username)
        {
            bool isValid = false;
            string studentID = username;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT COUNT(*) FROM Students WHERE StudentID = @StudentID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StudentID", username);

                    int count = (int)command.ExecuteScalar();
                    isValid = (count > 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }

            return isValid;
        }

        private DataTable GetStudentGrades(string username)
        {
            DataTable gradesTable = null;
            string studentID = username;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Courses.CourseName, StudentCourses.Grade " +
                                   "FROM StudentCourses " +
                                   "INNER JOIN Courses ON StudentCourses.CourseID = Courses.CourseID " +
                                   "WHERE StudentCourses.StudentID = @StudentID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@StudentID", username);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    gradesTable = new DataTable();
                    adapter.Fill(gradesTable);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }

            return gradesTable;
        }

        private void ReturnSF_Click(object sender, EventArgs e)
        {
            StudentForm studentForm = Application.OpenForms.OfType<StudentForm>().FirstOrDefault();
            if (studentForm != null)
            {
                studentForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("无法找到StudentForm窗体！", "返回错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
