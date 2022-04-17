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
    public partial class Form7 : Form
    {
        Form3 frm3 = new Form3();
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=vt.accdb");//veritabanı bağlantısı
        OleDbCommand cmd;
        OleDbDataAdapter da;
        DataSet ds;
        OleDbDataReader dr;
        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Open();// Bağlantıyı Açıyorum
            }

            //OleDbDataAdapter daa = new OleDbDataAdapter("Select * from kargolar where barkod_no Like '" + Form1.alicisube+ "%'", con);
            //DataSet dsaa = new DataSet();
            //daa.Fill(dsaa, "kargolar");
            //dataGridView1.DataSource = dsaa.Tables["kargolar"];

            OleDbDataAdapter daa = new OleDbDataAdapter("Select * from merkez where bulundugu_yer=  '" + Form1.alicisube + "' AND gidecegi_yer <> '" + Form1.alicisube + "'", con);

            DataSet dsaa = new DataSet();
            daa.Fill(dsaa, "merkez");
            dataGridView1.DataSource = dsaa.Tables["merkez"];
            con.Close();

            for (int i = 0; i < 5; i++)
                dataGridView2.Columns.Add("a", "a");
            if (con.State == ConnectionState.Open) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Close();// Bağlantıyı Açıyorum
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Open();// Bağlantıyı Açıyorum
            }
            OleDbDataAdapter daa = new OleDbDataAdapter("Select * from merkez where bulundugu_yer=  '" + Form1.alicisube + "' AND gidecegi_yer <> '"+Form1.alicisube +"'", con);
            DataSet dsaa = new DataSet();
            daa.Fill(dsaa, "merkez");
            dataGridView1.DataSource = dsaa.Tables["merkez"];
            con.Close();

            if (con.State == ConnectionState.Open) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Close();// Bağlantıyı Açıyorum
            }
            this.Hide();
            frm3.Show();
            dataGridView2.Rows.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < dataGridView2.Rows.Count-1; i++)
            {
                dataGridView2.Rows[i].Cells[4].Value = "Merkez";
            }
        }
        int d1satir;
        string fff;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            d1satir = dataGridView1.CurrentCell.RowIndex;
            if (dataGridView1.Rows[d1satir].Cells[0].Value != null) {
                dataGridView2.Rows.Add(dataGridView1.Rows[d1satir].Cells[0].Value, dataGridView1.Rows[d1satir].Cells[1].Value, dataGridView1.Rows[d1satir].Cells[2].Value, dataGridView1.Rows[d1satir].Cells[3].Value, dataGridView1.Rows[d1satir].Cells[4].Value);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.RemoveAt(dataGridView2.CurrentCell.RowIndex);
        }
        string islem, islem_no;
        private void button1_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Open();// Bağlantıyı Açıyorum
            }
            string merkez = "Merkez";
            cmd = new OleDbCommand();
            
            cmd.Connection = con;
            for (int i = 0; i < dataGridView2.Rows.Count-1; i++)
            {
                cmd.CommandText = "update merkez set bulundugu_yer= '" + merkez + "' where barkod_no= '" + dataGridView2.Rows[i].Cells[1].Value.ToString() + "'";
                cmd.ExecuteNonQuery();

            string dd = "insert into knerede(barkod_no,islem,islem_no) values(@barkod_no,@islem,@islem_no)";
            OleDbCommand xx = new OleDbCommand(dd, con);

            islem = "Merkeze Gönderildi";
            islem_no = "2";

            xx.Parameters.AddWithValue("@barkod_no", dataGridView2.Rows[i].Cells[1].Value);
            xx.Parameters.AddWithValue("@islem", islem);
            xx.Parameters.AddWithValue("@islem_no", islem_no);
            xx.ExecuteNonQuery();

            }
            
            

            con.Close();
            if (con.State == ConnectionState.Open) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Close();// Bağlantıyı Açıyorum
            }





        }
    }
}
