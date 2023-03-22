
namespace PersonelTakipUygulamasi
{
    partial class GirisEkrani
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GirisEkrani));
            this.lblkullanici = new System.Windows.Forms.Label();
            this.lblparola = new System.Windows.Forms.Label();
            this.lblyetki = new System.Windows.Forms.Label();
            this.txtKullaniciAdi = new System.Windows.Forms.TextBox();
            this.txtParola = new System.Windows.Forms.TextBox();
            this.radiobttnyonetici = new System.Windows.Forms.RadioButton();
            this.lblgirishak = new System.Windows.Forms.Label();
            this.btngiris = new System.Windows.Forms.Button();
            this.btncikis = new System.Windows.Forms.Button();
            this.lblkalanhaksayisi = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lblkullanici
            // 
            this.lblkullanici.AutoSize = true;
            this.lblkullanici.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblkullanici.Location = new System.Drawing.Point(12, 33);
            this.lblkullanici.Name = "lblkullanici";
            this.lblkullanici.Size = new System.Drawing.Size(102, 17);
            this.lblkullanici.TabIndex = 0;
            this.lblkullanici.Text = "Kullanıcı Adı:";
            // 
            // lblparola
            // 
            this.lblparola.AutoSize = true;
            this.lblparola.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblparola.Location = new System.Drawing.Point(54, 66);
            this.lblparola.Name = "lblparola";
            this.lblparola.Size = new System.Drawing.Size(60, 17);
            this.lblparola.TabIndex = 1;
            this.lblparola.Text = "Parola:";
            // 
            // lblyetki
            // 
            this.lblyetki.AutoSize = true;
            this.lblyetki.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblyetki.Location = new System.Drawing.Point(65, 99);
            this.lblyetki.Name = "lblyetki";
            this.lblyetki.Size = new System.Drawing.Size(49, 17);
            this.lblyetki.TabIndex = 2;
            this.lblyetki.Text = "Yetki:";
            // 
            // txtKullaniciAdi
            // 
            this.txtKullaniciAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtKullaniciAdi.Location = new System.Drawing.Point(120, 30);
            this.txtKullaniciAdi.Name = "txtKullaniciAdi";
            this.txtKullaniciAdi.Size = new System.Drawing.Size(163, 21);
            this.txtKullaniciAdi.TabIndex = 1;
            // 
            // txtParola
            // 
            this.txtParola.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtParola.Location = new System.Drawing.Point(120, 62);
            this.txtParola.Name = "txtParola";
            this.txtParola.Size = new System.Drawing.Size(163, 21);
            this.txtParola.TabIndex = 2;
            this.txtParola.UseSystemPasswordChar = true;
            // 
            // radiobttnyonetici
            // 
            this.radiobttnyonetici.AutoSize = true;
            this.radiobttnyonetici.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.radiobttnyonetici.Location = new System.Drawing.Point(120, 99);
            this.radiobttnyonetici.Name = "radiobttnyonetici";
            this.radiobttnyonetici.Size = new System.Drawing.Size(76, 19);
            this.radiobttnyonetici.TabIndex = 3;
            this.radiobttnyonetici.TabStop = true;
            this.radiobttnyonetici.Text = "Yönetici";
            this.radiobttnyonetici.UseVisualStyleBackColor = true;
            // 
            // lblgirishak
            // 
            this.lblgirishak.AutoSize = true;
            this.lblgirishak.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblgirishak.Location = new System.Drawing.Point(12, 182);
            this.lblgirishak.Name = "lblgirishak";
            this.lblgirishak.Size = new System.Drawing.Size(138, 17);
            this.lblgirishak.TabIndex = 7;
            this.lblgirishak.Text = "Kalan Giriş Hakkı:";
            // 
            // btngiris
            // 
            this.btngiris.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btngiris.Location = new System.Drawing.Point(103, 134);
            this.btngiris.Name = "btngiris";
            this.btngiris.Size = new System.Drawing.Size(75, 35);
            this.btngiris.TabIndex = 5;
            this.btngiris.Text = "Giriş";
            this.btngiris.UseVisualStyleBackColor = true;
            this.btngiris.Click += new System.EventHandler(this.btngiris_Click);
            // 
            // btncikis
            // 
            this.btncikis.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btncikis.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btncikis.Location = new System.Drawing.Point(208, 134);
            this.btncikis.Name = "btncikis";
            this.btncikis.Size = new System.Drawing.Size(75, 35);
            this.btncikis.TabIndex = 6;
            this.btncikis.Text = "Çıkış";
            this.btncikis.UseVisualStyleBackColor = true;
            // 
            // lblkalanhaksayisi
            // 
            this.lblkalanhaksayisi.AutoSize = true;
            this.lblkalanhaksayisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblkalanhaksayisi.Location = new System.Drawing.Point(173, 182);
            this.lblkalanhaksayisi.Name = "lblkalanhaksayisi";
            this.lblkalanhaksayisi.Size = new System.Drawing.Size(44, 17);
            this.lblkalanhaksayisi.TabIndex = 10;
            this.lblkalanhaksayisi.Text = "____";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.radioButton1.Location = new System.Drawing.Point(198, 99);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(81, 19);
            this.radioButton1.TabIndex = 11;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Kullanıcı";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // GirisEkrani
            // 
            this.AcceptButton = this.btngiris;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.CancelButton = this.btncikis;
            this.ClientSize = new System.Drawing.Size(316, 211);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.lblkalanhaksayisi);
            this.Controls.Add(this.btncikis);
            this.Controls.Add(this.btngiris);
            this.Controls.Add(this.lblgirishak);
            this.Controls.Add(this.radiobttnyonetici);
            this.Controls.Add(this.txtParola);
            this.Controls.Add(this.txtKullaniciAdi);
            this.Controls.Add(this.lblyetki);
            this.Controls.Add(this.lblparola);
            this.Controls.Add(this.lblkullanici);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GirisEkrani";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giriş Ekranı";
            this.Load += new System.EventHandler(this.GirisEkrani_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblkullanici;
        private System.Windows.Forms.Label lblparola;
        private System.Windows.Forms.Label lblyetki;
        private System.Windows.Forms.TextBox txtKullaniciAdi;
        private System.Windows.Forms.TextBox txtParola;
        private System.Windows.Forms.RadioButton radiobttnyonetici;
        private System.Windows.Forms.Label lblgirishak;
        private System.Windows.Forms.Button btngiris;
        private System.Windows.Forms.Button btncikis;
        private System.Windows.Forms.Label lblkalanhaksayisi;
        private System.Windows.Forms.RadioButton radioButton1;
    }
}

