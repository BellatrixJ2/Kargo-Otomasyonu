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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public static Form1 frm1 = new Form1();
        public static Form4 frm4 = new Form4();
        public static Form7 frm7 = new Form7();
        public static Form8 frm8 = new Form8();
        public static Form9 frm9 = new Form9();
        public static Form6 frm6 = new Form6();

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm4.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm1.Show();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm7.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm8.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm9.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1.acilis = 1;
            this.Hide();
            frm6.Show();
        }
    }
}
