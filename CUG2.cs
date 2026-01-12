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
    public partial class CUG2 : Form
    {
        public CUG2()
        {
            InitializeComponent();
        }

        private void 返回ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CUG cUG = new CUG();
            cUG.Show();
            this.Close();
        }
    }
}
