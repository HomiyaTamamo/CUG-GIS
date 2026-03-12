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
    public partial class StudentInfoEditForm : Form
    {
        private string connectionString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";
        private DataTable studentsTable; // 存储学生信息表
        public StudentInfoEditForm()
        {
            InitializeComponent();
        }

        private void StudentInfoEditForm_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }
        private void LoadStudents()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Students";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    studentsTable = new DataTable();
                    adapter.Fill(studentsTable);

                    dgvStudents.DataSource = studentsTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }
        }

        

        private void UpdateStudentInfo(SqlConnection connection, DataTable modifiedTable)
        {
            
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
            {
                bulkCopy.DestinationTableName = "Students";
                bulkCopy.ColumnMappings.Add("StudentID", "StudentID");
                bulkCopy.ColumnMappings.Add("StudentNumber", "StudentNumber");
                bulkCopy.ColumnMappings.Add("Name", "Name");
                bulkCopy.ColumnMappings.Add("Gender", "Gender");
                bulkCopy.ColumnMappings.Add("Birthdate", "Birthdate");

                bulkCopy.WriteToServer(modifiedTable);
            }
        }

        private void dgvStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SIFsave_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // 获取修改后的学生信息表
                    DataTable modifiedTable = ((DataTable)dgvStudents.DataSource).GetChanges();

                    if (modifiedTable != null)
                    {
                        // 更新数据库中的学生信息
                        UpdateStudentInfo(connection, modifiedTable);

                        studentsTable.Merge(modifiedTable); // 合并修改后的数据到原始数据表
                        studentsTable.AcceptChanges(); // 接受所有更改
                    }

                    MessageBox.Show("学生信息保存成功！", "学生信息编辑", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }
        }
    }
}
