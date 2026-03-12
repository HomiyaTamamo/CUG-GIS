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
    public partial class Viewpassword2 : Form
    {
        string connectingString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";
        public Viewpassword2()
        {
            InitializeComponent();
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

        private void Viewpassword2_Load(object sender, EventArgs e)
        {
            LoadPasswords();
        }
        private void LoadPasswords()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectingString))
                {
                    connection.Open();

                    
                    string query = "SELECT TeacherID, Password FROM TeachersPassword";

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
    }
}
