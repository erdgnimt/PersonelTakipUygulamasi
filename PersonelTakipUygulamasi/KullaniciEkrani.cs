using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace PersonelTakipUygulamasi
{
    public partial class KullaniciEkrani : Form
    {
        public KullaniciEkrani()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source =.\SQLEXPRESS; Initial Catalog = Personeller; Integrated Security = True");

        private void PersonelleriGoster()
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter personellerilistele = new SqlDataAdapter("select tcno as[TC Kimlik No], ad as [Ad], soyad as[Soyad], cinsiyet as [Cinsiyet], mezuniyet as [Mezuniyet], dogumtarihi as [Doğum Tarihi], gorev as [Görev], gorevyeri as [Görev Yeri], maas as [Maaş] from Personel order by ad asc", baglanti);
                DataSet ds = new DataSet();
                personellerilistele.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                baglanti.Close();

            }
            catch (Exception hatamesaji)
            {

                MessageBox.Show(hatamesaji.Message, "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void KullaniciEkrani_Load(object sender, EventArgs e)
        {
            PersonelleriGoster();
            label11.Text = GirisEkrani.adi + " " + GirisEkrani.soyadi;
            pictureBox1.Height = 150;
            pictureBox1.Width = 150;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox2.Height = 150;
            pictureBox2.Width = 150;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            try
            {
                pictureBox2.Image = Image.FromFile(Application.StartupPath + @"\Resimler\" + GirisEkrani.tcno + ".jpg");
            }
            catch 
            {

                pictureBox2.Image = Image.FromFile(Application.StartupPath + @"\Resimler\nopicture.jpg");
            }
            maskedTextBox1.Mask = "00000000000";
        }

        private void bttnAra_Click(object sender, EventArgs e)
        {
            bool kayitaramadurumu = false;
            if (maskedTextBox1.Text.Length == 11)
            {
                baglanti.Open();
                SqlCommand verilerigetir = new SqlCommand("select*from Personel where tcno='" + maskedTextBox1.Text + "'", baglanti);
                SqlDataReader verilerioku = verilerigetir.ExecuteReader();
                while (verilerioku.Read())
                {
                    kayitaramadurumu = true;
                    try
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\PersonelResimler\" + verilerioku.GetValue(0).ToString() + ".jpg");
                    }
                    catch 
                    {
                        pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\PersonelResimler\nopicture.jpg");
                    }
                    label1.Text = verilerioku.GetValue(1).ToString();
                    label3.Text = verilerioku.GetValue(2).ToString();
                    if (verilerioku.GetValue(3).ToString() == "Bay")
                        label2.Text = "Bay";
                    else
                        label2.Text = "Bayan";
                    label4.Text = verilerioku.GetValue(4).ToString();
                    label8.Text = verilerioku.GetValue(5).ToString();
                    label6.Text = verilerioku.GetValue(6).ToString();
                    label7.Text = verilerioku.GetValue(7).ToString();
                    label5.Text = verilerioku.GetValue(8).ToString();
                    break;
                }
                if (kayitaramadurumu == false)
                    MessageBox.Show("Kayıt bulunamadı!", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                baglanti.Close();
            }
            else
                MessageBox.Show("Hatalı TC Kimlik numarası!", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
