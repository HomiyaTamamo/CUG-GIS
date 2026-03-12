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
    public partial class Floorchoose : Form
    {
        public Floorchoose()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Floor1 floor1 = new Floor1();
            floor1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Floor2 floor2 = new Floor2();
            floor2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Floor3 floor3 = new Floor3();
            floor3.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Floor4 floor4 = new Floor4();
            floor4.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Floor5 floor5 = new Floor5();
            floor5.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Choosecourse choosecourse = new Choosecourse();
            choosecourse.Show();
            this.Hide();
        }

        private void Floorchoose_Load(object sender, EventArgs e)
        {

        }

        private void btn1f_Click(object sender, EventArgs e)
        {
            Floor1 floor1 = new Floor1();
            floor1.Show();
            this.Hide();
        }
    }
}
