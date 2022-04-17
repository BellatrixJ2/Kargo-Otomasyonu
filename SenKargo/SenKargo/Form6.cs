using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SenKargo
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }
        Form3 frm3 = new Form3();
        Form2 frm2 = new Form2();
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm3.Show();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Form1.barkod_no =textBox1.Text;
            Form2.doldur();
            this.Hide();
            frm2.Show();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
    }
}
