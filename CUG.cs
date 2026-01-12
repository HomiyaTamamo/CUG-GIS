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
    public partial class CUG : Form
    {
        public CUG()
        {
            InitializeComponent();
        }

        private void 返回ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Choosecourse choosecourse=new Choosecourse();
            choosecourse.Show();
            this.Close();
        }

        private void 查看最短路径ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CUG2 cUG2 = new CUG2();
            cUG2.Show();
            this.Close();
        }
    }
}
