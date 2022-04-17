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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        Form3 frm3 = new Form3();
        Form1 frm1 = new Form1();
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm3.Show();
        }
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=vt.accdb");//veritabanı bağlantısı
        void dolduuur()
        {
            if (con.State == ConnectionState.Closed) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Open();// Bağlantıyı Açıyorum
            }



            OleDbDataAdapter daa = new OleDbDataAdapter("Select * from merkez where gidecegi_yer=  '" + Form1.alicisube + "' AND bulundugu_yer= 'Merkez'", con);
            DataSet dsaa = new DataSet();
            daa.Fill(dsaa, "merkez");
            dataGridView1.DataSource = dsaa.Tables["merkez"];
            con.Close();
            if (con.State == ConnectionState.Open) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Close();// Bağlantıyı Açıyorum
            }
        }
        private void Form8_Load(object sender, EventArgs e)
        {
            dolduuur();
        }
        OleDbCommand cmd;
        string islem, islem_no;
        private void button1_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Open();// Bağlantıyı Açıyorum
            }
            string sube = "Şubeye Ulaştı";
            cmd = new OleDbCommand();

            cmd.Connection = con;
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                cmd.CommandText = "update merkez set bulundugu_yer= '" +Form1.alicisube  + "' where barkod_no= '" + dataGridView1.Rows[i].Cells[1].Value.ToString() + "'";
                cmd.ExecuteNonQuery();

                string dd = "insert into knerede(barkod_no,islem,islem_no) values(@barkod_no,@islem,@islem_no)";
                OleDbCommand xx = new OleDbCommand(dd, con);

                islem = "Şubeye Ulaştı";
                islem_no = "3";

                xx.Parameters.AddWithValue("@barkod_no", dataGridView1.Rows[i].Cells[1].Value);
                xx.Parameters.AddWithValue("@islem", islem);
                xx.Parameters.AddWithValue("@islem_no", islem_no);
                xx.ExecuteNonQuery();
            }

            con.Close();
            if (con.State == ConnectionState.Open) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Close();// Bağlantıyı Açıyorum
            }
            dolduuur();
        }
    }
}
