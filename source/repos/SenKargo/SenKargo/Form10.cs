using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using BarcodeLib.BarcodeReader;
using System.Data.OleDb;
using System.IO.Ports;

namespace SenKargo
{
    public partial class Form10 : Form
    {
        
        public Form10()
        {
            InitializeComponent();
        }
        FilterInfoCollection ball;
        VideoCaptureDevice fuenteVideo;
        AForge.Controls.VideoSourcePlayer asdccc = new AForge.Controls.VideoSourcePlayer();

        Form2 frm2 = new Form2();
        private void Form10_Load(object sender, EventArgs e)
        {
            asdccc.SetBounds(1,1,208,202);
            this.Controls.Add(asdccc);
            ball = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo x in ball)
            {
                comboBox1.Items.Add(x.Name);
            }
            comboBox1.SelectedIndex = 4;
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
            fuenteVideo = new VideoCaptureDevice(ball[comboBox1.SelectedIndex].MonikerString);
            asdccc.VideoSource = fuenteVideo;
            asdccc.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fuenteVideo.IsRunning)
            {

                fuenteVideo.Stop();
            }
        }
        public static string barcodee;
        int uzub ;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (asdccc.GetCurrentVideoFrame() != null)
            {

                Bitmap img = new Bitmap(asdccc.GetCurrentVideoFrame());
                string[] resultados = BarcodeReader.read(img, BarcodeReader.QRCODE);
                img.Dispose();
                if (resultados != null)
                {

                    textBox1.Text = resultados[0];
                    timer1.Stop();
                    uzub = textBox1.Text.Length;
                    barcodee = textBox1.Text.Substring(1,uzub-1);
                    Form1.barkod_no = barcodee;
                    Form2.eksikdoldur();
                    this.Hide();
                    frm2.Show();
                }
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2.frm1.Show();
        }
    }
}
