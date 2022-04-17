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
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
        }
        public static OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=vt.accdb");//veritabanı bağlantısı
        public static OleDbCommand cmd;
        public static OleDbDataAdapter da;
        public static DataSet ds;
        public static OleDbDataReader dr;
        public static Form1 frm1 = new Form1();
        Form3 frm3 = new Form3();
        public static void doldur()
        {

            da = new OleDbDataAdapter("Select * from knerede where barkod_no='" + Form1.barkod_no + "'", con);
            ds = new DataSet();
            da.Fill(ds, "knerede");
            dgv1.DataSource = ds.Tables["knerede"];

            con.Close();
            if (con.State == ConnectionState.Open) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Close();// Bağlantıyı Açıyorum
            }
        }
        public static OleDbDataAdapter daa1;
        public static DataSet dss1;
        public static string annn="3";

        public static void eksikdoldur()
        {
            if (con.State == ConnectionState.Closed) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Open();// Bağlantıyı Açıyorum
            }
            //annn += Form10.barcodee;
            daa1 = new OleDbDataAdapter("select * from knerede where barkod_no like '%"+ Form10.barcodee + "'", con);
            dss1 = new DataSet();
            daa1.Fill(dss1, "knerede");
            dgv1.DataSource = dss1.Tables["knerede"];

            con.Close();
            if (con.State == ConnectionState.Open) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Close();// Bağlantıyı Açıyorum
            }
        }
        public static DataGridView dgv1 = new DataGridView();
        
        private void Form2_Load(object sender, EventArgs e)
        {
            dgv1.SetBounds(10,10,443,157);
            this.Controls.Add(dgv1);
            

            da = new OleDbDataAdapter("select * from merkez where barkod_no '" + Form1.barkod_no + "'", con);
            ds = new DataSet();
            da.Fill(ds, "merkez");

            
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if(Form1.acilis==1)
            {
                this.Hide();
                frm3.Show();
                
                Form1.barkod_no = "";
            }
            else
            {
                this.Hide();
                frm1.Show();
                
                Form1.barkod_no = "";
            }
        }
    }
}
