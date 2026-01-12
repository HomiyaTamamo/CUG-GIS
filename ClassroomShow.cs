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
    public partial class ClassroomShow : Form
    {
        private string connectionString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";
        private DataTable classroomTable;
        public ClassroomShow()
        {
            InitializeComponent();
        }

        private void ClassroomShow_Load(object sender, EventArgs e)
        {
            LoadClassroomData();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                int classroomID = Convert.ToInt32(row.Cells["ClassroomID"].Value);
                int newCapacity = Convert.ToInt32(row.Cells["Capacity"].Value);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE Classrooms SET Capacity = @Capacity WHERE ClassroomID = @ClassroomID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Capacity", newCapacity);
                    command.Parameters.AddWithValue("@ClassroomID", classroomID);

                    connection.Open();
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("教室可容纳人数已成功修改！");
                LoadClassroomData();
            }
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                int classroomID = Convert.ToInt32(row.Cells["ClassroomID"].Value);
                int newCapacity = Convert.ToInt32(row.Cells["Capacity"].Value);

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE Classrooms SET Capacity = @Capacity WHERE ClassroomID = @ClassroomID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Capacity", newCapacity);
                        command.Parameters.AddWithValue("@ClassroomID", classroomID);

                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("教室可容纳人数已成功修改！");
                        }
                        else
                        {
                            MessageBox.Show("教室可容纳人数修改失败！");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("数据库连接错误：" + ex.Message);
                }

                LoadClassroomData();
            }
        }

        private void 修改教室信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
                int columnIndex = dataGridView1.Columns["Capacity"].Index;
                dataGridView1.CurrentCell = dataGridView1.Rows[rowIndex].Cells[columnIndex];
                dataGridView1.BeginEdit(true);

                MessageBox.Show("教室可容纳人数已成功修改！");
                LoadClassroomData();
            }
        }
        private void LoadClassroomData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Classrooms";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                classroomTable = new DataTable();
                adapter.Fill(classroomTable);

                dataGridView1.DataSource = classroomTable;
                dataGridView1.ReadOnly = false; // 允许编辑单元格
            }
        }
        private void LoadClassroomCourses(int classroomID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM ClassroomCourses " +
                               "INNER JOIN Courses ON ClassroomCourses.CourseID = Courses.CourseID " +
                               "WHERE ClassroomCourses.ClassroomID = @ClassroomID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClassroomID", classroomID);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable classroomCoursesTable = new DataTable();
                adapter.Fill(classroomCoursesTable);

                dataGridView1.DataSource = classroomCoursesTable;
            }
        }

        private void 修改须知ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("管理员只能修改教室的容积量Capacity，其他属性均不可修改。");
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
