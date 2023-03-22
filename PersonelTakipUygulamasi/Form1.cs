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
    public partial class GirisEkrani : Form
    {
        public GirisEkrani()
        {
            InitializeComponent();
        }
        //SQL BAGLANTISI YAPILDI.
        SqlConnection baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Personeller;Integrated Security=True");

        //BÜTÜN FORMLARDA KULLANICILACAK DEĞİŞKENLER
        public static string tcno, adi, soyadi, yetkim;
        

        //SADECE BU FORMDA KULLANILICAK DEĞİŞKENLER
        int hak = 3;
        bool durum = false;

        private void GirisEkrani_Load(object sender, EventArgs e)
        {
            //this.AcceptButton = btngiris; enter tuşuna basıldığında giriş butonu çalışır. ben propertiesden yaptım.
            //this.CancelButton = btncikis; cancel tuşuna basıldığında çıkış butonu çalışır. ben propertiesden yaptım.
            lblkalanhaksayisi.Text = hak.ToString();
            //radiobttnyonetici.Checked = true;//radiobutton seçili gelecek.
        }

        private void btngiris_Click(object sender, EventArgs e)
        {
            if (hak != 0)
            {
                baglanti.Open();
                string verigetirsorgusu = "Select*from Kullanicilar";
                SqlCommand tablodakiverilerigetir = new SqlCommand(verigetirsorgusu, baglanti);
                SqlDataReader verileriokuhafizayaal = tablodakiverilerigetir.ExecuteReader();
                while (verileriokuhafizayaal.Read())
                {
                    if (radiobttnyonetici.Checked == true)//YÖNETİCİ RADİOBUTTON SECİLİ İSE.
                    {
                        if (verileriokuhafizayaal["kullaniciadi"].ToString() == txtKullaniciAdi.Text && verileriokuhafizayaal["parola"].ToString() == txtParola.Text && verileriokuhafizayaal["yetki"].ToString() == "Yönetici")//VERİTABANINDA KULLANICIADI TXT KULLANICIYA EŞİT İSE, VERİTABANINDA PAROLA TXTPAROLAYA EŞİT İSE VE VERİTABANINDA YETKİ YÖNETİCİ İSE BU BLOK ÇALIŞACAK...
                        {
                            durum = true;
                            tcno = verileriokuhafizayaal.GetValue(0).ToString();//DİĞER FORMLARDA KULLANACAĞIMIZ DEĞİŞKENLERE SQLDEN ÇEKTİĞİMİZ DEĞERLERİ ATADIK. VE BU ŞEKİLDE DİĞER FORMDA KULLANACAĞIZ.
                            adi = verileriokuhafizayaal.GetValue(1).ToString();
                            soyadi = verileriokuhafizayaal.GetValue(2).ToString();
                            yetkim = verileriokuhafizayaal.GetValue(3).ToString();
                            this.Hide();
                            YoneticiEkrani yoneticiekran = new YoneticiEkrani();
                            yoneticiekran.Show();
                            break;//İSTENİLEN KULLANICI ADI VE ŞİFREYİ BULDUKTAN SONRA DÖNDÜGÜYÜ KIRAR VE ÇIKAR.
                        }                    
                    }
                    if (radioButton1.Checked == true)
                    {
                        if (verileriokuhafizayaal["kullaniciadi"].ToString() == txtKullaniciAdi.Text && verileriokuhafizayaal["yetki"].ToString()=="Kullanıcı" && verileriokuhafizayaal["parola"].ToString() == txtParola.Text )
                        {
                            durum = true;
                            tcno = verileriokuhafizayaal.GetValue(0).ToString();//DİĞER FORMLARDA KULLANACAĞIMIZ DEĞİŞKENLERE SQLDEN ÇEKTİĞİMİZ DEĞERLERİ ATADIK. VE BU ŞEKİLDE DİĞER FORMDA KULLANACAĞIZ.
                            adi = verileriokuhafizayaal.GetValue(1).ToString();
                            soyadi = verileriokuhafizayaal.GetValue(2).ToString();
                            yetkim = verileriokuhafizayaal.GetValue(3).ToString();
                            this.Hide();
                            KullaniciEkrani kullaniciekran = new KullaniciEkrani();
                            kullaniciekran.Show();
                            break;//İSTENİLEN KULLANICI ADI VE ŞİFREYİ BULDUKTAN SONRA DÖNDÜGÜYÜ KIRAR VE ÇIKAR.
                        }                       
                    }
                }
                if(durum==false)
                {
                    hak--;
                    lblkalanhaksayisi.Text = hak.ToString();
                    baglanti.Close();                   
                }
            }
            if(hak==0)
            {
                baglanti.Close();
                btngiris.Enabled = false;
                MessageBox.Show("Giriş hakkınız bitti!", "Personel Takip Programı", MessageBoxButtons.OK);
                this.Close();
            }
        }
    }
}
