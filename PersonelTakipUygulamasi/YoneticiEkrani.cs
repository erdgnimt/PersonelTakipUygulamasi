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
using System.IO;
using System.Text.RegularExpressions;//(regex) güvenli parola oluşturmak üzere hazır kodlar mevcuttur. bir parolannın güvenli veya güvenli olmadığını belirtir.

namespace PersonelTakipUygulamasi
{
    public partial class YoneticiEkrani : Form
    {
        public YoneticiEkrani()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Personeller;Integrated Security=True;MultipleActiveResultSets=True");

        private void KullanicilariGoster()//veritabanındaki verileri datagridviewe yazdırıyoruz.
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter kullanicilarilistele = new SqlDataAdapter("select tcno as[TC Kimlik No], ad as [Ad], soyad as[Soyad], yetki as [Yetki], kullaniciadi as [Kullanıcı Adı], parola as [Parola] from Kullanicilar order by ad asc",baglanti);
                DataSet ds = new DataSet();
                kullanicilarilistele.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                baglanti.Close();
            }
            catch (Exception hatamesaji)
            {
                MessageBox.Show(hatamesaji.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);//ilk yazılan parametre hatamesajını yazar,ikincisi hata ekranın başlığı,üçüncü tamam butonu gösterir,son parametre hata ikonu gösterir.
                baglanti.Close();
            }
        }

        private void PersonelleriGoster()
        {
            try
            {
                baglanti.Open();
                SqlDataAdapter personellerilistele = new SqlDataAdapter("select tcno as[TC Kimlik No], ad as [Ad], soyad as[Soyad], cinsiyet as [Cinsiyet], mezuniyet as [Mezuniyet], dogumtarihi as [Doğum Tarihi], gorev as [Görev], gorevyeri as [Görev Yeri], maas as [Maaş] from Personel order by ad asc", baglanti);
                DataSet ds = new DataSet();
                personellerilistele.Fill(ds);
                dataGridView2.DataSource = ds.Tables[0];
                baglanti.Close();
            }
            catch (Exception hatamesaji)
            {
                MessageBox.Show(hatamesaji.Message, "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);//ilk yazılan parametre hatamesajını yazar,ikincisi hata ekranın başlığı,üçüncü tamam butonu gösterir,son parametre hata ikonu gösterir.
                baglanti.Close();
            }
        }

        private void TabControl1Temizle()
        {
            txtkTC.Clear();
            txtkAd.Clear();
            txtkSoyad.Clear();
            radiobttnYonetici.Checked = true;
            txtkKullaniciad.Clear();
            txtkParola.Clear();
            txtkParolaTekrar.Clear();
            lblParolaDurum.Text = "";
            lblSeviye.Text = "Seviye";

        }

        private void TabConrol2Temizle()
        {
            mtxtTC.Text = "";
            mtxtAd.Text = "";
            mtxtSoyad.Text = "";
            radiobttnbay.Checked = true;
            mtxtMaas.Text = "";           
        }

        private void YoneticiEkrani_Load(object sender, EventArgs e)//Yönetici ekranı ilk yüklendiğindeki ayarlar.
        {           
            pictureBox1.Height = 150;//boyu
            pictureBox1.Width = 150;//genişliği 
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;//yüklenen resmi picturebox'a göre boyutlandırır.
            try
            {
                pictureBox1.Image = Image.FromFile(Application.StartupPath+@"\Resimler\"+ GirisEkrani.tcno+".jpg");
            }
            catch (Exception)
            {

                pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\Resimler\nopicture.jpg");
            }
            //KULLANICI İŞLEMLERİ SEKMESİ ilişkin düzenlemeler...
            label12.Text = GirisEkrani.adi + " " + GirisEkrani.soyadi;//picturebox1'in altındaki label textine kullanıcının adı ve soyadı yazılacaktır.
            txtkTC.MaxLength = 11;
            txtkKullaniciad.MaxLength = 8;
            toolTip1.SetToolTip(this.txtkTC, "TC Kimlik 11 Haneden Oluşmalıdır.");//mouse ile kullanıcı işlemlerindeki tc kimlik no textboxuna geldiğimizde uyarı belirecektir.
            txtkAd.CharacterCasing=CharacterCasing.Upper;//textboxa ne yazılırsa yazılsın büyük harfe dönüştürecektir.
            txtkSoyad.CharacterCasing = CharacterCasing.Upper;
            txtkParola.MaxLength = 10;//parola en fazla 10 karakterden oluşacaktır.
            txtkParolaTekrar.MaxLength = 10;
            progressBar1.Maximum = 100;//max değeri 100 yaptık yani 100 eşit parçaya böldük
            progressBar1.Value = 0;//başlangıçta 0 ile başlayacaktır.
            KullanicilariGoster();//kullanıcıları listelemek için oluşturduğumuz metodu çağırdık.

            //PERSONEL İŞLEMLERİ SEKMESİ ilişkin düzenlemelen...
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Width = 100;
            pictureBox2.Height = 100;
            pictureBox2.BorderStyle = BorderStyle.Fixed3D;
            mtxtTC.Mask = "00000000000";// 0 zorunlu rakam girişi demek.Maskeleme yaptık ve TC kimlik numarasını 11 haneli rakam girmesi zorunlu oldu.
            mtxtAd.Mask = "LL????????????????????";// maskeleme yapıldı ve isim en az 3 karakterden oluşacak. kullanıcı 3 karakterden az isim giremeyecektir. toplam 22 karaktere kadar isim girişi yapabilecektir.
            mtxtSoyad.Mask = "LL????????????????????";// maskeleme yapıldı ve soyad en az 3 karakterden oluşacak. kullanıcı 3 karakterden az soyad giremeyecektir. toplam 22 karaktere kadar isim girişi yapabilecektir.
            mtxtMaas.Mask = "00000";//maaş 5 haneden uzun olamayacak.
            mtxtAd.Text.ToUpper();
            mtxtSoyad.Text.ToUpper();

            cmbMezuniyet.Items.Add("İlköğretim");
            cmbMezuniyet.Items.Add("Ortaöğretim");
            cmbMezuniyet.Items.Add("Lise");
            cmbMezuniyet.Items.Add("Önlisans");
            cmbMezuniyet.Items.Add("Üniversite");

            cmbGorevi.Items.Add("Genel Müdür");
            cmbGorevi.Items.Add("Müdür");
            cmbGorevi.Items.Add("Mühendis");
            cmbGorevi.Items.Add("İşçi");
            cmbGorevi.Items.Add("Şoför");

            cmbGoreviYeri.Items.Add("Arge");
            cmbGoreviYeri.Items.Add("Finans");
            cmbGoreviYeri.Items.Add("Üretim Planlama");
            cmbGoreviYeri.Items.Add("Kalite");
            cmbGoreviYeri.Items.Add("Üretim");

            //datetimepicker kullanımına ilişkin kodlar. 
            DateTime zaman = DateTime.Now;//yeni bir datetime nesnesi oluşturuyoruz. ve şimdiki zamanı ilgili nesneye atıyoruz.
            int yil = Convert.ToInt32(zaman.ToString("yyyy"));//ilgili nesnenin yılını alıyoruz.
            int ay = Convert.ToInt32(zaman.ToString("MM"));//nesnenin ayını alıyoruz.
            int gun = Convert.ToInt32(zaman.ToString("dd"));//nesnenin gününü alıyoruz.

            dateTimePicker1.MinDate = new DateTime(1960, 1, 1);
            dateTimePicker1.MaxDate = new DateTime(yil - 18, ay, gun);//yıldan 18'i çıkardık ve yıl,ay,gün olarak yazdık. 18 yaşından küçükler işe alınamayacak ve sisteme kaydedilemeyecektir. yani datetimpicker en fazla bu tarihe kadar gidecektir.
            dateTimePicker1.Format = DateTimePickerFormat.Short;

            radiobttnbay.Checked = true;
            radiobttnYonetici.Checked = true;            
            PersonelleriGoster();
        }
        //***************KULLANICI İŞLEMLERİ SEKMESİNE İLİŞKİN KODLARIN BAŞLANGICI***************
        private void txtkTC_TextChanged(object sender, EventArgs e)
        {
            if (txtkTC.Text.Length < 11)//tc kimlik numarası 11 haneden az girildiği sürece textbox yanında uyarı işareti çıkacaktır.            
                errorProvider1.SetError(txtkTC, "TC Kimlik numarası 11 haneden oluşmalıdır.");  
                
            else//11 hane girildiği takdirde uyarı ikonu temizlenecek.
                errorProvider1.Clear();
        }
        private void txtkTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57) || (int)e.KeyChar == 8)//klavyeden kulanıcının tc texboxuna harf yazmasını engelledik. sayılar ascii tablosunda 48 ile 57 arasındadir ve 8 numarası da backspace tuşuna karşılık gelmektedir. bu yüzden o sayılar if koşuluna yazıldı. kullanıcı sayıdan başka bir tuşa bastığında textboxa yazamayacaktır. sayılar ve backspace tuşu çalışacaktır.
                e.Handled = false;//if bloguna girerse ilgili tuşlara basılmasına izin verdik.
            else
                e.Handled = true;//eğer kullanıca metinsel ifade veya sembol karakter girmeyece çalışırsa bunu engelledik.
        }
        private void txtkAd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            //IsLetter = kullanıcının bastığı tuş harf mi ? 
            //IsControl = kullanıcının bastığı tuş backspace mi ?
            //IsSeparator = kullanıcının bastığı tuş boşluk mu ?            
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }
        private void txtkSoyad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsSeparator(e.KeyChar) == true)
            //IsLetter = kullanıcının bastığı tuş harf mi ? 
            //IsControl = kullanıcının bastığı tuş backspace mi ?
            //IsSeparator = kullanıcının bastığı tuş boşluk mu ?            
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }


        private void txtkKullaniciad_TextChanged(object sender, EventArgs e)
        {
            if (txtkKullaniciad.Text.Length != 8)
                errorProvider1.SetError(txtkKullaniciad,"Kullanıcı adı 8 karakterden oluşmalıdır.");
            else
                errorProvider1.Clear();
        }

        private void txtkKullaniciad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == true || char.IsControl(e.KeyChar) == true || char.IsDigit(e.KeyChar) == true)
                //IsLetter = kullanıcının bastığı tuş harf mi ? 
                //IsControl = kullanıcının bastığı tuş backspace mi ?
                //Isdigit = kullanıcının bastığı sayi tuşu mu ? 
                e.Handled = false;
            else
                e.Handled = true;
        }
        int toplamparolaskoru = 0;

        private void txtkParola_TextChanged(object sender, EventArgs e)
        {
            string parolaseviyesi = "";
            int kucukharfskoru = 0, buyukharfskoru = 0, rakamskoru = 0, sembolskoru = 0;
            string sifre = txtkParola.Text;
            string duzeltilmissifre = "";
            duzeltilmissifre = sifre;
            duzeltilmissifre = duzeltilmissifre.Replace('İ', 'I');
            duzeltilmissifre = duzeltilmissifre.Replace('ı', 'i');
            duzeltilmissifre = duzeltilmissifre.Replace('Ç', 'C');
            duzeltilmissifre = duzeltilmissifre.Replace('ç', 'c');
            duzeltilmissifre = duzeltilmissifre.Replace('Ş', 'S');
            duzeltilmissifre = duzeltilmissifre.Replace('ş', 's');
            duzeltilmissifre = duzeltilmissifre.Replace('Ğ', 'G');
            duzeltilmissifre = duzeltilmissifre.Replace('ğ', 'g');
            duzeltilmissifre = duzeltilmissifre.Replace('Ü', 'U');
            duzeltilmissifre = duzeltilmissifre.Replace('Ö', 'O');
            duzeltilmissifre = duzeltilmissifre.Replace('ü', 'u');
            duzeltilmissifre = duzeltilmissifre.Replace('ö', 'o');

            if (sifre != duzeltilmissifre)
            {
                sifre = duzeltilmissifre;
                txtkParola.Text = sifre;
                MessageBox.Show("Parolanızdaki Türkçe karakterler İngilizceye dönüştürülmüştür.");
            }

            int kucukharfkaraktersayisi = sifre.Length - Regex.Replace(sifre, "[a-z]", "").Length;//sifrenin uzunluğundan sifre içerisinde küçük harfleri , sonraki "" boş stringe atıyoruz kalan harfler küçük olmayan karakterlerin sayısı olacaktır o sayıyı sifrenin uzunluğundan çıkartıyoruz ve küçükharfsayisini buluyoruz. regex ile başlayan kısım bize sifredeki küçük harf harici karakterlerin sayisini döndürür.
            kucukharfskoru = Math.Min(2, kucukharfkaraktersayisi) * 10;//kullanıcı 1 küçük harf kullanırsa 10, 2 ve üzeri küçük harf kullanırsa 20 puan alacaktır. 2 den fazla kullansada alacağı puan 20 olacaktır. çünkü math metodunda min değeri 2 olarak belirledik.

            int buyukharfkaraktersayisi = sifre.Length - Regex.Replace(sifre, "[A-Z]", "").Length;//büyük harflerin sayısı,

            buyukharfskoru = Math.Min(2, buyukharfkaraktersayisi) * 10;//büyük harflerin puanlandırılması.

            int rakamsayisi = sifre.Length - Regex.Replace(sifre, "[0-9]", "").Length;//rakamların sayisi
            rakamskoru = Math.Min(2, rakamsayisi) * 10;//rakamların puanlandırılması.

            int sembolsayisi = sifre.Length - kucukharfkaraktersayisi - buyukharfkaraktersayisi;//sembol sayısının bulunması.
            sembolskoru = Math.Min(2, sembolsayisi) * 10;

            toplamparolaskoru = buyukharfskoru + kucukharfskoru + rakamskoru + sembolskoru;

            if (sifre.Length == 9)//sifre 9 karakterden oluşuyorsa toplamparolaskoruna 10 ekledik ve puanı 90 oldu.
                toplamparolaskoru += 10;
            else if (sifre.Length == 10)//sifre 10 karakterden oluşuyorsa toplamparolaskoruna 20 ekledik ve puanı 100 oldu.
                toplamparolaskoru += 20;

            if (kucukharfskoru == 0 || buyukharfskoru == 0 || sembolskoru == 0 ||rakamskoru == 0)//kullanıcı kucukharf,buyukharf,sembol,rakam kullandı mı?
            {
                lblParolaDurum.Text = "Büyük,küçük harf,rakam,sembol herbirinden en az bir tane kullanılmalıdır.";
            }               
               
            if (kucukharfskoru != 0 && buyukharfskoru != 0 && sembolskoru != 0 && rakamskoru != 0)
            {
                lblParolaDurum.Text = "";
            }

            if (toplamparolaskoru < 50)
            {                
                parolaseviyesi = "Zayıf";                  
            }                   
            
            else if (toplamparolaskoru >= 50 && toplamparolaskoru < 80)
            {               
                parolaseviyesi = "Orta";              
            } 

            else if (toplamparolaskoru >= 80 && toplamparolaskoru <= 100)
            {              
                parolaseviyesi = "Güçlü";               
            }     
                
            lblSeviye.Text = parolaseviyesi;
            progressBar1.Value = toplamparolaskoru;
        }

        private void txtkParolaTekrar_TextChanged(object sender, EventArgs e)
        {
            if (txtkParolaTekrar.Text != txtkParola.Text)            
                errorProvider1.SetError(txtkParolaTekrar, "Girilen şifre ile eşleşmiyor.");
            
            else            
                errorProvider1.Clear();            
        }

        private void bttnkKaydet_Click(object sender, EventArgs e)
        {
            string yetki = "";//yönetici mi kullanıcı mı onu belirlemek için tanımladık.
            bool kayitkontrol = false;
            baglanti.Open();
            SqlCommand verigetir = new SqlCommand("select*from Kullanicilar where tcno='" + txtkTC.Text + "'",baglanti);
            SqlDataReader verioku = verigetir.ExecuteReader();
            while (verioku.Read())
            {
                kayitkontrol = true;
                break;
            }
            baglanti.Close();
            
            if (kayitkontrol == false)
            {
                if (txtkTC.Text.Length<11 || txtkTC.Text == "")                                    
                    lblkTC.ForeColor = Color.Red;                                           
                else                
                    lblkTC.ForeColor = Color.Black;
                if (txtkAd.Text.Length < 2 || txtkAd.Text == "")
                    lblkAd.ForeColor = Color.Red;
                else
                    lblkAd.ForeColor = Color.Black;
                if (txtkSoyad.Text.Length < 2 || txtkSoyad.Text == "")
                    lblkSoyad.ForeColor = Color.Red;
                else
                    lblkSoyad.ForeColor = Color.Black;
                if (txtkKullaniciad.Text.Length != 8 || txtkKullaniciad.Text == "")
                    lblkKullanıcıAdi.ForeColor = Color.Red;
                else
                    lblkKullanıcıAdi.ForeColor = Color.Black;
                if (txtkParola.Text == "" || toplamparolaskoru < 50)
                    lblkParola.ForeColor = Color.Red;
                else
                    lblkParola.ForeColor = Color.Black;
                if (txtkParolaTekrar.Text == "" || txtkParolaTekrar.Text != txtkParola.Text)
                    lblkPTekrar.ForeColor = Color.Red;
                else
                    lblkPTekrar.ForeColor = Color.Black;
                if(txtkTC.Text.Length==11&&txtkTC.Text!=""&&txtkAd.Text.Length>1&& txtkAd.Text != "" && txtkSoyad.Text.Length > 1 && txtkSoyad.Text != "" && txtkKullaniciad.Text != "" && txtkParola.Text != "" && txtkParolaTekrar.Text != "" && txtkParola.Text == txtkParolaTekrar.Text && toplamparolaskoru > 50)
                {
                    if (radiobttnYonetici.Checked)
                        yetki = "Yönetici";
                    else if (radiobttnKullanici.Checked)
                        yetki = "Kullanıcı";
                    try
                    {
                        baglanti.Open();
                        SqlCommand veriekle = new SqlCommand("insert into Kullanicilar values ('" + txtkTC.Text + "','" + txtkAd.Text + "','" + txtkSoyad.Text + "','" + yetki + "','" + txtkKullaniciad.Text + "','" + txtkParola.Text + "')", baglanti);  
                        veriekle.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("Yeni kullanıcı kaydı yapıldı!", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        KullanicilariGoster();
                    }
                    catch (Exception hatamesaji)
                    {
                        MessageBox.Show(hatamesaji.Message);
                        baglanti.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Yazı rengi kırmızı olan alanların doğruluğunu kontrol ediniz!", "Personel Kayıt Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Girilen TC Kimlik Numarası sistemde zaten kayıtlı!", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//++

        private void bttnkAra_Click(object sender, EventArgs e)
        {
            bool kayitaramadurumu = false;
            if (txtkTC.Text.Length == 11)
            {
                baglanti.Open();
                SqlCommand verilerigetir = new SqlCommand($"select tcno as[TC Kimlik No], ad as [Ad], soyad as[Soyad], yetki as [Yetki], kullaniciadi as [Kullanıcı Adı], parola as [Parola] from Kullanicilar where tcno={txtkTC.Text}", baglanti);
                SqlDataAdapter veriyilisteyeekle = new SqlDataAdapter(verilerigetir);//eğer kayıt bulunduysa datagridviewde ilgili kaydın gösterilmesini sağlıyoruz.
                DataTable dt = new DataTable();
                veriyilisteyeekle.Fill(dt);
                dataGridView1.DataSource = dt;
                SqlDataReader verilerioku1 = verilerigetir.ExecuteReader();
                while (verilerioku1.Read())//TC NUMARASINA AİT BİR KAYIT VAR İSE.
                {                   
                    kayitaramadurumu = true;
                    txtkAd.Text = verilerioku1.GetValue(1).ToString();//tablodan gelen 1. sutündaki(ad degeri) değeri aldık ve txtad.text'e yazdık.
                    txtkSoyad.Text = verilerioku1.GetValue(2).ToString();//tablodan gelen 2. sutündaki(soyad degeri) değeri aldık ve txtsoyad.text'e yazdık.
                    if (verilerioku1.GetValue(3).ToString() == "Yönetici")//veritabanından gelen veri yönetici ise yönetici seçili olacak.                    
                        radiobttnYonetici.Checked = true;                    
                    else                    
                        radiobttnKullanici.Checked = true;//veritabanından gelen veri kullanıcı ise kullanıcı seçili olacak.
                    txtkKullaniciad.Text = verilerioku1.GetValue(4).ToString();
                    txtkParola.Text = verilerioku1.GetValue(5).ToString();
                    txtkParolaTekrar.Text = verilerioku1.GetValue(5).ToString();
                    baglanti.Close();
                    break;//kayıt bulunduysa ilgili yerlere bilgiler yazıldı ve döngüden çıkıldı.
                    
                }
                if (kayitaramadurumu == false)//eğer tc numarasına ait kayıt bulunamadıysa
                { MessageBox.Show("Kayıt Bulunamadı", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }               
            }
            else//girilen tc numarası 11 haneden oluşmuyorsa.            
                MessageBox.Show("TC kimlik numaranızı eksik lütfen kontrol ediniz!", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Error);
            baglanti.Close();
            
        }//++

        private void bttnkGuncelle_Click(object sender, EventArgs e)
        {
            string yetki = "";     
            
                if (txtkTC.Text.Length < 11 || txtkTC.Text == "")
                    lblkTC.ForeColor = Color.Red;
                else
                    lblkTC.ForeColor = Color.Black;
                if (txtkAd.Text.Length < 2 || txtkAd.Text == "")
                    lblkAd.ForeColor = Color.Red;
                else
                    lblkAd.ForeColor = Color.Black;
                if (txtkSoyad.Text.Length < 2 || txtkSoyad.Text == "")
                    lblkSoyad.ForeColor = Color.Red;
                else
                    lblkSoyad.ForeColor = Color.Black;
                if (txtkKullaniciad.Text.Length != 8 || txtkKullaniciad.Text == "")
                    lblkKullanıcıAdi.ForeColor = Color.Red;
                else
                    lblkKullanıcıAdi.ForeColor = Color.Black;
                if (txtkParola.Text == "" || toplamparolaskoru < 60)
                    lblkParola.ForeColor = Color.Red;
                else
                    lblkParola.ForeColor = Color.Black;
                if (txtkParolaTekrar.Text == "" || txtkParolaTekrar.Text != txtkParola.Text)
                    lblkPTekrar.ForeColor = Color.Red;
                else
                    lblkPTekrar.ForeColor = Color.Black;
                if (txtkTC.Text.Length == 11 && txtkTC.Text != "" && txtkAd.Text.Length > 1 && txtkAd.Text != "" && txtkSoyad.Text.Length > 1 && txtkSoyad.Text != "" && txtkKullaniciad.Text != "" && txtkParola.Text != "" && txtkParolaTekrar.Text != "" && txtkParola.Text == txtkParolaTekrar.Text && toplamparolaskoru > 70)
                {
                    if (radiobttnYonetici.Checked)
                        yetki = "Yönetici";
                    else if (radiobttnKullanici.Checked)
                        yetki = "Kullanıcı";
                    try
                    {
                    baglanti.Open();
                    SqlCommand verileriguncelle = new SqlCommand($"update Kullanicilar set ad=@a,soyad=@b,yetki=@c,kullaniciadi=@d,parola=@e where tcno={txtkTC.Text}", baglanti);
                    verileriguncelle.Parameters.AddWithValue("@a", txtkAd.Text);
                    verileriguncelle.Parameters.AddWithValue("@b", txtkSoyad.Text);
                    verileriguncelle.Parameters.AddWithValue("@d", txtkKullaniciad.Text);
                    verileriguncelle.Parameters.AddWithValue("@e", txtkParola.Text);
                    if (yetki == "Yönetici")
                        verileriguncelle.Parameters.AddWithValue("@c", radiobttnYonetici.Text); 
                    else                    
                        verileriguncelle.Parameters.AddWithValue("@c", radiobttnKullanici.Text);         
                    verileriguncelle.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kullanıcı bilgileri güncellendi!", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    TabControl1Temizle();
                    KullanicilariGoster();
                    }
                    catch (Exception hatamesaji)
                    {
                        MessageBox.Show(hatamesaji.Message,"Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();
                    }
                }
                else               
                    MessageBox.Show("Yazı rengi kırmızı olan alanların doğruluğunu kontrol ediniz!", "Personel Kayıt Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Error);                         
        }//++

        private void bttnKullanicilariListele_Click(object sender, EventArgs e)
        {
            KullanicilariGoster();
        }

        private void bttnkSil_Click(object sender, EventArgs e)
        {
            if (txtkTC.Text.Length == 11)
            {
                bool kayitaramadurumu = false;//default olarak kayıt yok kabul ettik.
                baglanti.Open();
                SqlCommand verilerigetir = new SqlCommand("select*from Kullanicilar where tcno='"+txtkTC.Text+"'", baglanti);//tc numarası eşit ise kayıtlar gelecek.
                SqlDataReader verilerioku2 = verilerigetir.ExecuteReader();
                while (verilerioku2.Read())
                {
                    kayitaramadurumu = true;//kayıt bulunduğuna göre durumu true yaptık.
                    SqlCommand veriyisil = new SqlCommand("delete from Kullanicilar where tcno='"+txtkTC.Text+"'",baglanti);
                    veriyisil.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı kaydı silindi!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglanti.Close();
                    KullanicilariGoster();
                    TabControl1Temizle();
                    break;
                }
                if (kayitaramadurumu == false)
                    MessageBox.Show("Girilin TC Kimlik numarasına ait kullanıcı bulunamadı!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                baglanti.Close();
            }
            else            
                MessageBox.Show("TC kimlik numarası eksik lütfen kontrol ediniz!", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Error);
            baglanti.Close();
        }//HATA VAR BAK!!!!!!!
        private void bttnkTemizle_Click(object sender, EventArgs e)
        {
            TabControl1Temizle();
        }
        //************************KULLANICI İŞLEMLERİ SEKMESİNE İLİŞKİN KODLARIN SONU.*****************************

        //************************PERSONEL İŞLEMLERİ SEKMESİNE İLİŞKİN KODLARIN BAŞLANGICI*************************
        
        private void bttnGozat_Click(object sender, EventArgs e)
        {
            OpenFileDialog resimsec = new OpenFileDialog();
            resimsec.Title = "Resimler";//gözat butonuna tıkladığımızda açılan pencerenin başlığı olacaktır.
            resimsec.Filter = "JPG Dosyalar (*.jpg)|*.jpg"; //sadece jpg dosyalarının görüntülenmesini sağladık. | işaretinden önceki kısım kullanıcı bilgilendirme, sonraki kısım görüntülecek dosya türüne ilişkindir.
            if (resimsec.ShowDialog()==DialogResult.OK)//resim başarı ile seçildiyse.
            {
                this.pictureBox2.Image = new Bitmap(resimsec.OpenFile());//seçilen resmi picturebox'a ekledik.
            }            
        }

        private void bttnKaydet_Click(object sender, EventArgs e)
        {
            string cinsiyet = ""; // radiobuttonda seçilenin bayan mı bay mı onu belirlemke için tanımlandı.
            bool kayitkontrol = false;

            baglanti.Open();
            SqlCommand verilerigetir = new SqlCommand($"select*from Personel where tcno='"+mtxtTC.Text+"'",baglanti);
            SqlDataReader verilerioku3 = verilerigetir.ExecuteReader();
            while (verilerioku3.Read())//BÖYLE BİR BAĞLANTI VAR İSE DÖNGÜYE GİRER.
            {
                kayitkontrol = true;            
                break;
            }
            baglanti.Close();
            if (kayitkontrol == false)//bu tc numarasına ait kayıt yok ise.
            {
                //doldurulması gereken kısımlar boş mu diye denetliyoruz?
                if (pictureBox2.Image == null)
                    bttnGozat.ForeColor = Color.Red;
                else
                    bttnGozat.ForeColor = Color.Black;
                if (mtxtTC.MaskCompleted == false)//masked textbox 11 haneli mi ?
                    lblTc.ForeColor = Color.Red;
                else
                    lblTc.ForeColor = Color.Black;
                if (mtxtAd.MaskCompleted == false)
                    lblAd.ForeColor = Color.Red;
                else
                    lblAd.ForeColor = Color.Black;
                if (mtxtSoyad.MaskCompleted == false)
                    lblSoyad.ForeColor = Color.Red;
                else
                    lblSoyad.ForeColor = Color.Black;
                if (cmbMezuniyet.Text == "")
                {
                    lblMezuniyet.ForeColor = Color.Red;
                }
                else
                    lblMezuniyet.ForeColor = Color.Black;
                if (cmbGorevi.Text == "")
                    lblGorevi.ForeColor = Color.Red;
                else
                    lblGorevi.ForeColor = Color.Black;
                if (cmbGoreviYeri.Text == "")
                    lblGorevYeri.ForeColor = Color.Red;
                else
                    lblGorevYeri.ForeColor = Color.Black;
                if (mtxtMaas.MaskCompleted == false)
                    lblMaas.ForeColor = Color.Red;
                else
                {
                    if (int.Parse(mtxtMaas.Text) < 10000)
                        lblMaas.ForeColor = Color.Red;
                    else
                        lblMaas.ForeColor = Color.Black;
                }
                //ilgili alanların dolu ise.
                if (pictureBox2.Image != null && mtxtTC.MaskCompleted == true && mtxtSoyad.MaskCompleted == true && radiobttnbay.Text != "" && cmbMezuniyet.Text != null && cmbGorevi != null && cmbGoreviYeri != null && mtxtMaas != null)
                {
                    if (radiobttnbay.Checked)
                        cinsiyet = "Bay";
                    else if (radiobttnbayan.Checked)
                        cinsiyet = "Bayan";
                    try
                    {
                        baglanti.Open();
                        SqlCommand verikaydet = new SqlCommand("insert into Personel values('" + mtxtTC.Text + "','" + mtxtAd.Text + "','" + mtxtSoyad.Text + "','" + cinsiyet + "','" + cmbMezuniyet.Text + "','" + dateTimePicker1.Text + "','" + cmbGorevi.Text + "','" + cmbGoreviYeri.Text + "','" + mtxtMaas.Text + "')", baglanti);//verileri ekliyoruz.
                        verikaydet.ExecuteNonQuery();
                        baglanti.Close();
                        if (!Directory.Exists(Application.StartupPath + @"\PersonelResimler"))//debugdaki bin klasörünün içinde personelresimler diye bir klasör var mı diye bakıyoruz.                        
                            Directory.CreateDirectory(Application.StartupPath + @"\PersonelResimler");//klasör yoksa oluşturacaktır.
                        pictureBox2.Image.Save(Application.StartupPath + @"\PersonelResimler\" + mtxtTC.Text + ".jpg");//klasör var ise resmi kaydedilen kişinin tc numarası adıyla kaydedecektir.    
                        MessageBox.Show("Yeni personel kaydı oluşturuldu.", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        PersonelleriGoster();
                    }
                    catch (Exception hatamesaji)
                    {
                        MessageBox.Show(hatamesaji.Message, "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        baglanti.Close();
                    }
                }
                else
                    MessageBox.Show("Kırmızı alanları gözden geçiriniz!", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Error);//bir alan boş bırakılırsa bu mesaj ekranda gözükecektir.
            }
            else
                MessageBox.Show("Kayıt gerçekleşmedi. Kayıt sistemde mevcut!", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void bttnAra_Click(object sender, EventArgs e)
        {
            bool kayitaramadurumu = false;
            if (mtxtTC.Text.Length == 11)
            {
                baglanti.Open();
                SqlCommand verilerigetir = new SqlCommand("select*from Personel where tcno='" + mtxtTC.Text + "'", baglanti);
                SqlDataAdapter veriyilisteyeekle = new SqlDataAdapter(verilerigetir);//eğer kayıt bulunduysa datagridviewde ilgili kaydın gösterilmesini sağlıyoruz.
                DataTable dt = new DataTable();
                veriyilisteyeekle.Fill(dt);
                dataGridView2.DataSource = dt;
                SqlDataReader verilerioku4 = verilerigetir.ExecuteReader();
                while (verilerioku4.Read())
                {
                    kayitaramadurumu = true;
                    try
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + @"\PersonelResimler\" + verilerioku4.GetValue(0).ToString() + ".jpg");//Aranan tc kimlik numarasına ait resmi ilgili klasörden çekiyoruz ve picturebox'ta görüntülenmesini sağlıyoruz.
                    }
                    catch
                    {
                        pictureBox2.Image = Image.FromFile(Application.StartupPath + @"\PersonelResimler\nopicture.jpg");
                        //eğer kullanıcının resmi yoksa nopicture image yüklenecek.
                    }
                    mtxtAd.Text = verilerioku4.GetValue(1).ToString();//veritabanından verileri çektik ilgili yerlere yazdırdık.
                    mtxtSoyad.Text = verilerioku4.GetValue(2).ToString();
                    if (verilerioku4.GetValue(3).ToString() == "Bay")
                        radiobttnbay.Checked = true;
                    else
                        radiobttnbayan.Checked = false;
                    cmbMezuniyet.Text = verilerioku4.GetValue(4).ToString();
                    dateTimePicker1.Text = verilerioku4.GetValue(5).ToString();
                    cmbGorevi.Text = verilerioku4.GetValue(6).ToString();
                    cmbGoreviYeri.Text = verilerioku4.GetValue(7).ToString();
                    mtxtMaas.Text = verilerioku4.GetValue(8).ToString();
                    baglanti.Close();////
                    break;//döngüden çıkıyoruz.
                }
                if (kayitaramadurumu == false)
                    MessageBox.Show("Aranan kayıt bulunamadı!", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                baglanti.Close();
            }
            else
                MessageBox.Show("Lütfen TC Kimlik numarasını kontrol ediniz!", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void bttnGuncelle_Click(object sender, EventArgs e)
        {
            string cinsiyet = ""; // radiobuttonda seçilenin bayan mı bay mı onu belirlemke için tanımlandı.          
           
                //doldurulması gereken kısımlar boş mu diye denetliyoruz?
                if (pictureBox2.Image == null)
                    bttnGozat.ForeColor = Color.Red;
                else
                    bttnGozat.ForeColor = Color.Black;
                if (mtxtTC.MaskCompleted == false)//masked textbox 11 haneli mi ?
                    lblTc.ForeColor = Color.Red;
                else
                    lblTc.ForeColor = Color.Black;
                if (mtxtAd.MaskCompleted == false)
                    lblAd.ForeColor = Color.Red;
                else
                    lblAd.ForeColor = Color.Black;
                if (mtxtSoyad.MaskCompleted == false)
                    lblSoyad.ForeColor = Color.Red;
                else
                    lblSoyad.ForeColor = Color.Black;
                if (cmbMezuniyet.Text == "")
                {
                    lblMezuniyet.ForeColor = Color.Red;
                }
                else
                    lblMezuniyet.ForeColor = Color.Black;
                if (cmbGorevi.Text == "")
                    lblGorevi.ForeColor = Color.Red;
                else
                    lblGorevi.ForeColor = Color.Black;
                if (cmbGoreviYeri.Text == "")
                    lblGorevYeri.ForeColor = Color.Red;
                else
                    lblGorevYeri.ForeColor = Color.Black;
                if (mtxtMaas.MaskCompleted == false)
                    lblMaas.ForeColor = Color.Red;
                else
                {
                    if (int.Parse(mtxtMaas.Text) < 10000)
                        lblMaas.ForeColor = Color.Red;
                    else
                        lblMaas.ForeColor = Color.Black;
                }
            //ilgili alanların dolu ise.
            if (pictureBox2.Image != null && mtxtTC.MaskCompleted != false && mtxtSoyad.MaskCompleted != false && cmbMezuniyet.Text != "" && cmbGorevi.Text != "" && cmbGoreviYeri.Text != "" && mtxtMaas.MaskCompleted != false) 
                {
                    if (radiobttnbay.Checked)
                        cinsiyet = "Bay";
                    else if (radiobttnbayan.Checked)
                        cinsiyet = "Bayan";
                     try
                     {
                        baglanti.Open();
                        SqlCommand verileriguncelle = new SqlCommand("update Personel set ad='" + mtxtAd.Text + "',soyad='" + mtxtSoyad.Text + "',cinsiyet='" + cinsiyet + "',mezuniyet='" + cmbMezuniyet.Text + "',dogumtarihi='" + dateTimePicker1.Text + "',gorev='" + cmbGorevi.Text + "',gorevyeri='" + cmbGoreviYeri.Text + "',maas='" + mtxtMaas.Text + "'where tcno='"+mtxtTC.Text+"'", baglanti);//verileri guncelliyoruz.
                        verileriguncelle.ExecuteNonQuery();
                        baglanti.Close();
                        //if (!Directory.Exists(Application.StartupPath + @"\PersonelResimler"))//debugdaki bin klasörünün içinde personelresimler diye bir klasör var mı diye bakıyoruz.                        
                        //    Directory.CreateDirectory(Application.StartupPath + @"\PersonelResimler");//klasör yoksa oluşturacaktır.                      
                        //pictureBox2.Image.Save(Application.StartupPath + @"\PersonelResimler\" + mtxtTC.Text + ".jpg");//klasör var ise resmi kaydedilen kişinin tc numarası adıyla kaydedecektir.
                        //*************RESİM GÜNCELLEME İŞLEMİNE BAKILACAK!!!!!!!
                        MessageBox.Show("Personel kaydı güncellendi.", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        PersonelleriGoster();
                     }
                     catch (Exception hatamesaji)
                     {
                    MessageBox.Show(hatamesaji.Message, "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    baglanti.Close();
                     }
                }
                    else
                    MessageBox.Show("Kırmızı alanları gözden geçiriniz!", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Error);//bir alan boş bırakılırsa bu mesaj ekranda gözükecektir. 
        }
        private void bttnSil_Click(object sender, EventArgs e)
        {
            if (mtxtTC.MaskCompleted == true)
            {
                bool kayitaramadurumu = false;
                baglanti.Open();
                SqlCommand verilerigetir = new SqlCommand("select*from Personel where tcno='"+mtxtTC.Text+"'",baglanti);
                SqlDataReader verilerioku5 = verilerigetir.ExecuteReader();
                while (verilerioku5.Read())
                {
                    kayitaramadurumu = true;
                    SqlCommand verilerisil = new SqlCommand("delete from Personel where tcno='" + mtxtTC.Text + "'", baglanti);
                    verilerisil.ExecuteNonQuery();
                    MessageBox.Show("Personel kaydı silindi!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    baglanti.Close();
                    PersonelleriGoster();
                    TabConrol2Temizle();
                    break;                  
                }
                if (kayitaramadurumu == false)
                {
                    MessageBox.Show("Girilin TC Kimlik numarasına ait personel bulunamadı!", "Personel Takip Programı", MessageBoxButtons.OK, MessageBoxIcon.Error);                   
                }
                baglanti.Close();  
            }
            else
            {
                MessageBox.Show("TC kimlik numarası eksik lütfen kontrol ediniz!", "Personel Takip Uygulaması", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }//HATA VAR!!!!!!BAK!!!

        private void bttnPersonelListele_Click(object sender, EventArgs e)
        {
            PersonelleriGoster();
        }
    }
}
