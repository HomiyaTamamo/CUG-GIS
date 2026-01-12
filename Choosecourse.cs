using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Choosecourse : Form
    {
        string connectionString = "Data Source=DESKTOP-DACGBC5;Initial Catalog=master;User ID=sa;Password=1234";
        public string username { get; set; }
        public Choosecourse()
        {
            InitializeComponent();
            this.username = username;
        }
        public Choosecourse(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void btnchco_Click(object sender, EventArgs e)
        {
            CourseSelectionForm courseSelectionForm = new CourseSelectionForm(username);
            courseSelectionForm.Show();

        }

        private void btnCRinf_Click(object sender, EventArgs e)
        {
            Floorchoose floorchoose = new Floorchoose();
            floorchoose.Show();
            this.Hide();
        }

        private void btncug_Click(object sender, EventArgs e)
        {
            CUG cUG = new CUG();
            cUG.Show();
            this.Hide();
        }

        private void btnexit_Click(object sender, EventArgs e)
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

        private void Choosecourse_Load(object sender, EventArgs e)
        {

        }
    }
}
