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
    public partial class GradesForm : Form
    {
        public GradesForm()
        {
            InitializeComponent();
        }

        private void dgvGrades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void SetGradesTable(DataTable gradesTable)
        {
            dgvGrades.DataSource = gradesTable; // 将成绩表绑定到DataGridView控件上
        }
    }
}
