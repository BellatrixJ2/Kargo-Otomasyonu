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
    public partial class Form9 : Form
    {
        public Form9()
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

        public void dolduur()
        {
            if (con.State == ConnectionState.Closed) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Open();// Bağlantıyı Açıyorum
            }

            OleDbDataAdapter daa = new OleDbDataAdapter("Select * from merkez where gidecegi_yer=  '" + Form1.alicisube + "' AND bulundugu_yer= '"+Form1.alicisube+"'", con);
            DataSet dsaa = new DataSet();
            daa.Fill(dsaa, "merkez");
            dataGridView1.DataSource = dsaa.Tables["merkez"];

            OleDbDataAdapter dxda = new OleDbDataAdapter("Select * from dagitimdakiler where sube_no='"+Form1.alicisube+"'", con);
            DataSet dagit = new DataSet();
            dxda.Fill(dagit, "dagitimdakiler");
            dataGridView2.DataSource = dagit.Tables["dagitimdakiler"];




            con.Close();
            if (con.State == ConnectionState.Open) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Close();// Bağlantıyı Açıyorum
            }
        }
        private void Form9_Load(object sender, EventArgs e)
        {
            dolduur();
        }
        string barkoddno,islem,islem_no;
        string araca="Dagitimda";

        private void button4_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Open();// Bağlantıyı Açıyorum
            }

            barkoddno = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            cmd = new OleDbCommand();

            cmd.Connection = con;
            cmd.CommandText = "update merkez set bulundugu_yer= '" + araca + "' where barkod_no= '" + barkoddno.ToString() + "'";
            cmd.ExecuteNonQuery();


            //OleDbCommand xdda = new OleDbCommand("Select * from kargolar where barkod_no = '"+barkoddno+"'", con);
            //OleDbDataReader adad = xdda.ExecuteReader();
            OleDbDataAdapter da = new OleDbDataAdapter("Select * from kargolar where barkod_no = '" + barkoddno + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "kargolar");
            string adres = ds.Tables["kargolar"].Rows[0][8].ToString();
            string adres1 = ds.Tables["kargolar"].Rows[0][9].ToString();


            string dekle = "insert into dagitimdakiler(barkod_no,sube_no,adres,adres1) values(@barkod_no,@sube_no,@adres,@adres1)";
            OleDbCommand xdd = new OleDbCommand(dekle, con);
            xdd.Parameters.AddWithValue("@barkod_no", barkoddno);
            xdd.Parameters.AddWithValue("@sube_no", Form1.alicisube);
            xdd.Parameters.AddWithValue("@adres", adres);
            xdd.Parameters.AddWithValue("@adres1", adres1);
            xdd.ExecuteNonQuery();

            string dd = "insert into knerede(barkod_no,islem,islem_no) values(@barkod_no,@islem,@islem_no)";
            OleDbCommand xx = new OleDbCommand(dd, con);

            islem = "Dağıtıma Çıktı";
            islem_no = "4";

            xx.Parameters.AddWithValue("@barkod_no", barkoddno);
            xx.Parameters.AddWithValue("@islem", islem);
            xx.Parameters.AddWithValue("@islem_no", islem_no);
            xx.ExecuteNonQuery();

            con.Close();
            if (con.State == ConnectionState.Open) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Close();// Bağlantıyı Açıyorum
            }
            dolduur();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Open();// Bağlantıyı Açıyorum
            }


            string dd = "insert into knerede(barkod_no,islem,islem_no) values(@barkod_no,@islem,@islem_no)";
            OleDbCommand xx = new OleDbCommand(dd, con);

            islem = "Teslim Edildi";
            islem_no = "5";

            xx.Parameters.AddWithValue("@barkod_no", dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[1].Value);
            xx.Parameters.AddWithValue("@islem", islem);
            xx.Parameters.AddWithValue("@islem_no", islem_no);
            xx.ExecuteNonQuery();


            string nooo = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[1].Value.ToString();
            OleDbCommand silici = new OleDbCommand("DELETE FROM dagitimdakiler WHERE barkod_no=@barkod_no and sube_no=@sube_no and adres=@adres and adres1=@adres1", con);
            silici.Parameters.AddWithValue("@barkod_no", dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[1].Value.ToString());
            silici.Parameters.AddWithValue("@sube_no", dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[2].Value.ToString());
            silici.Parameters.AddWithValue("Alan3", dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[3].Value.ToString());
            silici.Parameters.AddWithValue("Alan4", dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[4].Value.ToString());
        


            silici.ExecuteNonQuery();
            dolduur();



            con.Close();
            if (con.State == ConnectionState.Open) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Close();// Bağlantıyı Açıyorum
            }
        }

        OleDbCommand cmd;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            

        }
    }
}
