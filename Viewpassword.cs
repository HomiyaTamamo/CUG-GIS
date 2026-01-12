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
    
    public partial class Viewpassword : Form
    {
        string connectingString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";
        public Viewpassword()
        {
            InitializeComponent();
        }

        private void Passwordchance_Load(object sender, EventArgs e)
        {
            LoadStudentPasswords();
        }
        private void LoadStudentPasswords()
        {
            try
            {
                using (SqlConnection connection=new SqlConnection(connectingString))
                {
                    connection.Open();

                    string query = "SELECT StudentID, Password FROM Passwords";
                    

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dgvallpw.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误：" + ex.Message);
            }
        }

        private void Querycourse_Load(object sender, EventArgs e)
        {
            LoadStudentPasswords();
        }

        private void dgvallpw_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Vpwexit_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();
            adminForm.Show();
            this.Hide();
        }
    }
}
