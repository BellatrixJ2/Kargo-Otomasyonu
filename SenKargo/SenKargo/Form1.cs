using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace SenKargo
{
    public partial class Form1 : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=vt.accdb");//veritabanı bağlantısı
        OleDbCommand cmd;
        OleDbDataAdapter da;
        DataSet ds;
        OleDbDataReader dr;
        
        public static Form2 frm2 = new Form2();
        public static Form3 frm3 = new Form3();
        public Form1()
        {
            InitializeComponent();
        }
        public static int frmacilis = 0;
        public static string barkod_no;

        public static int acilis = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            acilis = 0;
            barkod_no=textBox3.Text;
            frmacilis = 1;
            Form2.doldur();
            this.Hide();
            frm2.Show();
        }
        public static string alicisube;
        private void button2_Click(object sender, EventArgs e)
        {
            
            string kadi = textBox1.Text;
            string sifre = textBox2.Text;
            alicisube = kadi.Substring(0,5);
            
            cmd = new OleDbCommand();
           
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM kadi_sifre where kadi='" + textBox1.Text + "' AND sifre='" + textBox2.Text + "'";//veritabanı sql sorgusu
            dr=cmd.ExecuteReader();
            if (dr.Read())
            {
                this.Hide();
                frm3.Show();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı ya da şifre yanlış");
            }

            con.Close();
            if (con.State == ConnectionState.Open) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Close();// Bağlantıyı Açıyorum
            }
        } // Kullanıcı giriş butonu

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "3482001";
            textBox2.Text = "erdem123";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        Form10 frm10 = new Form10();
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm10.Show();
        }
    }
}
