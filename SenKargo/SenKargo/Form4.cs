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
using MessagingToolkit.QRCode;
using MessagingToolkit.QRCode.Codec;

namespace SenKargo
{
    public partial class Form4 : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=vt.accdb");//veritabanı bağlantısı
        OleDbCommand cmd;
        OleDbDataAdapter da;
        DataSet ds;
        OleDbDataReader dr;
        static public string fiyat;
        public Form4()
        {
            InitializeComponent();
        }
        Form5 frm5 = new Form5();
        Form3 frm3 = new Form3();
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            
            frm5.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            frm5.ShowDialog();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            

            label10.Text = fiyat;
            for (int i = 1; i <= 81; i++)
            {
                OleDbCommand cmd = new OleDbCommand("select * from pk_il where il_no ='" + i + "'", con);
                try
                {
                    con.Open();
                    OleDbDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        comboBox1.Items.Add(dr["il_ad"].ToString());

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                }
            }
            if (con.State == ConnectionState.Open) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Close();// Bağlantıyı Açıyorum
            }
        }
        string islem, islem_no;
        Image qrcode;
        QRCodeEncoder codee = new QRCodeEncoder();
        string adress;
        string odeme;
        private void button1_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Open();// Bağlantıyı Açıyorum
            }
            OleDbDataAdapter dgg = new OleDbDataAdapter("select * from kargolar",con);
            #region barkod numarası hazırlanış
            DataSet dx = new DataSet();
            dgg.Fill(dx, "kargolar");
            int sayi = dx.Tables["kargolar"].Rows.Count;
            string barkodbas;
            string barkodson;
            string barkodno;

            barkodbas = Form1.alicisube;
            if(textBox8.Text.Length==3)
            {
                textBox8.Text = "00" + textBox8.Text;
                barkodson = textBox8.Text;
            }
            else if(textBox8.Text.Length == 4)
            {
                textBox8.Text = "0" + textBox8.Text;
                barkodson = textBox8.Text;
            }
            else
            { barkodson = textBox8.Text; }

            barkodno = barkodbas + barkodson.ToString() + (sayi + 1).ToString();
            
            #endregion
            #region adres hazırlama
            string adres1 = comboBox1.SelectedItem.ToString();
            string adres2 = comboBox2.SelectedItem.ToString();
            string adres3 = comboBox3.SelectedItem.ToString();
            string adres4 = comboBox4.SelectedItem.ToString();
            adress = adres1 + "-" + adres2 + "-" + adres3 + "-" + adres4;
            #endregion
            #region etiket hazırlama
            string etiket="";
            if(checkBox1.Checked)
            { etiket = etiket + "1"; }
            if(checkBox2.Checked)
            { etiket = etiket + "2"; }
            if (checkBox3.Checked)
            { etiket = etiket + "3"; }
            if (checkBox4.Checked)
            { etiket = etiket + "4"; }
            #endregion
            #region ödeyici
            
            if(radioButton1.Checked)
            {odeme = "Gönderici Ödemeli";}
            else
            { odeme = "Alıcı Ödemeli"; }
            #endregion


            string ekle = "insert into kargolar(barkod_no,gonderen_ad,gonderen_soyad,gonderen_tel,alici_ad,alici_soyad,alici_tel,alici_adres,alici_adres1,kargo_etiketi,kargo_odeyici,kargo_ucreti) values (@barkod_no,@gonderen_ad,@gonderen_soyad,@gonderen_tel,@alici_ad,@alici_soyad,@alici_tel,@alici_adres,@alici_adres1,@kargo_etiketi,@kargo_odeyici,@kargo_ucreti)";
            OleDbCommand komut = new OleDbCommand(ekle, con);
            komut.Parameters.AddWithValue("@barkod_no", barkodno);
            komut.Parameters.AddWithValue("@gonderen_ad", textBox1.Text);
            komut.Parameters.AddWithValue("@gonderen_soyad", textBox2.Text);
            komut.Parameters.AddWithValue("@gonderen_tel", textBox3.Text);
            komut.Parameters.AddWithValue("@alici_ad", textBox5.Text);
            komut.Parameters.AddWithValue("@alici_soyad", textBox6.Text);
            komut.Parameters.AddWithValue("@alici_tel", textBox7.Text);
            komut.Parameters.AddWithValue("@alici_adres", adress);
            komut.Parameters.AddWithValue("@alici_adres1", textBox4.Text);
            komut.Parameters.AddWithValue("@kargo_etiketi", etiket);
            komut.Parameters.AddWithValue("@kargo_odeyici", odeme);
            komut.Parameters.AddWithValue("@kargo_ucreti", fiyat);
            komut.ExecuteNonQuery();
            string dd = "insert into knerede(barkod_no,islem,islem_no) values(@barkod_no,@islem,@islem_no)";
            OleDbCommand xx = new OleDbCommand(dd, con);

            islem = "Teslim Alındı";
            islem_no = "1";

            xx.Parameters.AddWithValue("@barkod_no", barkodno);
            xx.Parameters.AddWithValue("@islem", islem);
            xx.Parameters.AddWithValue("@islem_no", islem_no);
            xx.ExecuteNonQuery();

            string asd = "insert into merkez(barkod_no,alinan_yer,gidecegi_yer,bulundugu_yer) values(@barkod_no,@alinan_yer,@gidecegi_yer,@bulundugu_yer)";
            OleDbCommand ddf = new OleDbCommand(asd, con);
            ddf.Parameters.AddWithValue("@barkod_no", barkodno);
            ddf.Parameters.AddWithValue("@alinan_yer", Form1.alicisube);
            ddf.Parameters.AddWithValue("@gidecegi_yer", barkodson);
            ddf.Parameters.AddWithValue("@bulundugu_yer", Form1.alicisube);
            ddf.ExecuteNonQuery();

            frm3.Show();
            this.Hide();

            con.Close();
            if (con.State == ConnectionState.Open) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Close();// Bağlantıyı Açıyorum
            }

            qrcode = codee.Encode(barkodno);
            printPreviewDialog1.ShowDialog();
        }
        string[,] dizi;
        string[,] dizi2;
        string[,] dizi3;
        
        string ilceno,bolgeno,postano;
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Open();// Bağlantıyı Açıyorum
            }
            comboBox2.Items.Clear();
            int deger = comboBox1.SelectedIndex + 1;
            OleDbDataAdapter da = new OleDbDataAdapter("select * from pk_ilce where il_no = '" + deger.ToString() + "'", con); // Program Tablosundan Tüm Verileri Alan SQ
            
                                                                                                                           // cmd = new OleDbCommand("select * from pk_ilce where il_no='" + deger.ToString() + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "pk_ilce");
            dizi = new string[ds.Tables["pk_ilce"].Rows.Count, 3];
            for (int i = 0; i < ds.Tables["pk_ilce"].Rows.Count; i++)
            {
                comboBox2.Items.Add(Convert.ToString(ds.Tables["pk_ilce"].Rows[i][2]));
                dizi[i,0]= Convert.ToString(ds.Tables["pk_ilce"].Rows[i][0]);
                dizi[i, 1] = Convert.ToString(ds.Tables["pk_ilce"].Rows[i][1]);
                dizi[i, 2] = Convert.ToString(ds.Tables["pk_ilce"].Rows[i][2]);

            }

            con.Close();
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            
            for (int i = 0; i < comboBox2.Items.Count; i++)
            {
                
                if (comboBox2.SelectedItem == dizi[i, 2])
                {
                    ilceno = dizi[i, 0];
                    
                }
            }


            if (con.State == ConnectionState.Closed) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Open();// Bağlantıyı Açıyorum
            }
            comboBox3.Items.Clear();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from pk_bolge where ilce_no = '" + ilceno.ToString() + "'", con); // Program Tablosundan Tüm Verileri Alan SQ

            // cmd = new OleDbCommand("select * from pk_ilce where il_no='" + deger.ToString() + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "pk_bolge");
            dizi2 = new string[ds.Tables["pk_bolge"].Rows.Count, 3];
            for (int i = 0; i < ds.Tables["pk_bolge"].Rows.Count; i++)
            {
                comboBox3.Items.Add(Convert.ToString(ds.Tables["pk_bolge"].Rows[i][2]));
                dizi2[i, 0] = Convert.ToString(ds.Tables["pk_bolge"].Rows[i][0]);
                dizi2[i, 1] = Convert.ToString(ds.Tables["pk_bolge"].Rows[i][1]);
                dizi2[i, 2] = Convert.ToString(ds.Tables["pk_bolge"].Rows[i][2]);

            }
            con.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm3.Show();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Yazı fontumu ve çizgi çizmek için fırçamı ve kalem nesnemi oluşturdum
            Font myFont = new Font("Calibri", 20);
            SolidBrush sbrush = new SolidBrush(Color.Black);
            Pen myPen = new Pen(Color.Black);

            //Bu kısımda sipariş formu yazısını ve çizgileri yazdırıyorum
            e.Graphics.DrawLine(myPen, 5, 5, 815, 5);
            e.Graphics.DrawLine(myPen, 5, 380, 815, 380);
            e.Graphics.DrawLine(myPen, 815, 5, 815, 380);
            e.Graphics.DrawLine(myPen, 5, 5, 5, 380);
            e.Graphics.DrawImage(qrcode, 655, 10,150,150);
            e.Graphics.DrawLine(myPen, 245, 5, 245, 380);
            e.Graphics.DrawLine(myPen, 5, 55, 640, 55);
            e.Graphics.DrawLine(myPen, 5, 95, 640, 95);
            e.Graphics.DrawLine(myPen, 5, 135, 640, 135);
            e.Graphics.DrawLine(myPen, 5, 175, 815, 175);
            e.Graphics.DrawLine(myPen, 5, 215, 815, 215);
            e.Graphics.DrawLine(myPen, 5, 255, 815, 255);
            e.Graphics.DrawLine(myPen, 5, 295, 815, 295);
            e.Graphics.DrawLine(myPen, 5, 335, 815, 335);

            e.Graphics.DrawString("Gönderen Adı:\t "+textBox1.Text, myFont, sbrush, 20, 20);
            e.Graphics.DrawString("Gönderen Soyadı:\t "+textBox2.Text, myFont, sbrush, 20, 60);
            e.Graphics.DrawString("Alıcı Adı:\t\t " + textBox5.Text, myFont, sbrush, 20, 100);
            e.Graphics.DrawString("Alıcı Soyadı:\t "+textBox6.Text, myFont, sbrush, 20, 140);
            e.Graphics.DrawString("Alıcı Tel: \t\t " + textBox7.Text, myFont, sbrush, 20, 180);
            e.Graphics.DrawString("Alıcı Adresi: \t ", myFont, sbrush, 20, 220);
            myFont = new Font("Calibri", 16);
            e.Graphics.DrawString(""+adress, myFont, sbrush, 250, 220);

            myFont = new Font("Calibri", 20);
            e.Graphics.DrawString("Alıcı Adresi2: \t " + textBox4.Text,myFont,sbrush,20,260);
            e.Graphics.DrawString("Ödeme Şekli: \t "+odeme, myFont, sbrush, 20, 300);
            e.Graphics.DrawString("Fiyat: \t\t "+fiyat, myFont, sbrush, 20, 340);

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private void comboBox4_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < comboBox4.Items.Count; i++)
            {

                if (comboBox4.SelectedItem == dizi3[i, 2])
                {
                    postano = dizi3[i, 3];
                    textBox8.Text =postano;
                }
            }
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < comboBox3.Items.Count; i++)
            {

                if (comboBox3.SelectedItem == dizi2[i, 2])
                {
                    bolgeno = dizi2[i, 0];

                }
            }

            
            if (con.State == ConnectionState.Closed) //Bağlantının Durumunu Sorguluyorum Kapalımı Diye
            { // Eğer Kapalıysa Bağlantıyı Açıyorum Çalışmalar Sırasında Bağlantı Açık Kalabiliyor Çünkü Hata Vermesini Engellemek İçin
                con.Open();// Bağlantıyı Açıyorum
            }
            comboBox4.Items.Clear();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from pk_mahalle where bolge_no = '" + bolgeno.ToString() + "'", con); // Program Tablosundan Tüm Verileri Alan SQ

            // cmd = new OleDbCommand("select * from pk_ilce where il_no='" + deger.ToString() + "'", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "pk_mahalle");
            dizi3 = new string[ds.Tables["pk_mahalle"].Rows.Count, 4];
            for (int i = 0; i < ds.Tables["pk_mahalle"].Rows.Count; i++)
            {
                comboBox4.Items.Add(Convert.ToString(ds.Tables["pk_mahalle"].Rows[i][2]));
                dizi3[i, 0] = Convert.ToString(ds.Tables["pk_mahalle"].Rows[i][0]);
                dizi3[i, 1] = Convert.ToString(ds.Tables["pk_mahalle"].Rows[i][1]);
                dizi3[i, 2] = Convert.ToString(ds.Tables["pk_mahalle"].Rows[i][2]);
                dizi3[i, 3] = Convert.ToString(ds.Tables["pk_mahalle"].Rows[i][3]);

            }
            con.Close();
        }
    }
}

