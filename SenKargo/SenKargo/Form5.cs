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
    public partial class Form5 : Form
    {
        
        
        public Form5()
        {
            InitializeComponent();
        }
Form4 frm4 ;
        private void Form5_Load(object sender, EventArgs e)
        {
            frm4 = new Form4();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                 Form4.fiyat= "5,75";
                this.Hide();
                frm4.label10.Text = "5,75";
                
            }
            else if (radioButton2.Checked)
                
            {
                Form4.fiyat = "9,99";
                this.Hide();
                frm4.label10.Text = "9,99"; 

            }
            else if (radioButton3.Checked)
            {
                Form4.fiyat = "19,99";
                this.Hide();
                frm4.label10.Text = "19,99"; 

            }
            else if (radioButton4.Checked)
            {
                Form4.fiyat = "29,99";
                this.Hide();
                frm4.label10.Text= "29,99";

            }
            else
            {
                MessageBox.Show("Kutu Seçmediniz");
            }
        }
    }
}
