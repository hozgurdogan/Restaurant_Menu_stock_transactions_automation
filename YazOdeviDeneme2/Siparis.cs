/****************************************************************************
**					SAKARYA ÜNİVERSİTESİ
**				BİLGİSAYAR VE BİLİŞİM BİLİMLERİ FAKÜLTESİ
**				    BİLGİSAYAR MÜHENDİSLİĞİ BÖLÜMÜ
**				   NESNEYE DAYALI PROGRAMLAMA DERSİ
**					2014-2015 BAHAR DÖNEMİ
**	
**				ÖDEV NUMARASI..........:Proje1
**				ÖĞRENCİ ADI............:Hasan Özgür Doğan
**				ÖĞRENCİ NUMARASI.......:G201210020
**              DERSİN ALINDIĞI GRUP...:1. Öğretim A

****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YazOdeviDeneme2
{
    public partial class Siparis : Form
    {
        private LinkedList yiyecekListesi;
        private int aktifMasaNo;
        private Form1 mainFormm; // Form1'e referansı tutmak için bir değişken ekleyin

        public Siparis(LinkedList yiyecekListesi)
        {
            InitializeComponent();
            this.yiyecekListesi = yiyecekListesi;
        }
        private void Siparis_Load(object sender, EventArgs e)
        {
            EkranAyarlari.CenterFormOnScreen(this);
            EkranAyarlari.ArkaPlanResmiSet(this);


            dataGridView2.Columns.Add("Adi", "Adı");
            dataGridView2.Columns.Add("Cinsi", "Cinsi");
            dataGridView2.Columns.Add("Fiyat", "Fiyatı");

            // DataGridView'in Seçim Modu
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // DataGridView'in başlık arka plan rengi
            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 192, 128);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);

            // DataGridView'in hücre arka plan rengi
            dataGridView2.DefaultCellStyle.BackColor = Color.White;
            dataGridView2.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView2.DefaultCellStyle.Font = new Font("Arial", 10);

            // DataGridView'in seçili hücre arka plan rengi
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 128);

            // DataGridView'in alternatif satır rengi
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // DataGridView'in hücre kenarlık stilinin ayarlanması
            dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;




            // DataGridView'in başlık arka plan rengi
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 192, 128);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);

            // DataGridView'in hücre arka plan rengi
            dataGridView1.DefaultCellStyle.BackColor = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10);

            // DataGridView'in seçili hücre arka plan rengi
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 128);

            // DataGridView'in alternatif satır rengi
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;

            // DataGridView'in hücre kenarlık stilinin ayarlanması
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

        }

        //fonksiyonlar
        private void SiparisiDosyayaEkle(string masaNo, string urunAdi, string cinsi, string fiyati)
        {
            try
            {
                string dosyaYolu = @"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\siparis.txt";
                using (StreamWriter writer = new StreamWriter(dosyaYolu, true))
                {
                    // Sipariş bilgilerini satır satır dosyaya ekliyoruz
                    writer.WriteLine($"{masaNo},{urunAdi},{cinsi},{fiyati}");
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya bilgi vermek için bir mesaj kutusu gösteriyoruz
                MessageBox.Show("Sipariş dosyasına yazılırken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SiparisleriListele()
        {
            try
            {
                string dosyaYolu = @"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\siparis.txt";

                if (File.Exists(dosyaYolu))
                {
                    dataGridView1.Columns.Clear();
                    dataGridView1.Rows.Clear();

                    // Başlık satırlarını ekliyoruz
                    dataGridView1.Columns.Add("MasaNo", "Masa No");
                    dataGridView1.Columns.Add("UrunAdi", "Urun Adi");
                    dataGridView1.Columns.Add("Cinsi", "Cinsi");
                    dataGridView1.Columns.Add("Fiyati", "Fiyati");

                    // Dosyadaki tüm satırları okuyup siparişleri listeye ekliyoruz
                    string[] siparisler = File.ReadAllLines(dosyaYolu);

                    foreach (string siparis in siparisler)
                    {
                        string[] siparisBilgileri = siparis.Split(',');
                        if (siparisBilgileri.Length == 4)
                        {
                            string masaNo = siparisBilgileri[0].Trim();
                            string urunAdi = siparisBilgileri[1].Trim();
                            string cinsi = siparisBilgileri[2].Trim();
                            string fiyati = siparisBilgileri[3].Trim();

                            // DataGridView'e bilgileri sütun sütun ekliyoruz
                            dataGridView1.Rows.Add(masaNo, urunAdi, cinsi, fiyati);
                        }
                    }
                }
                else
                {
                    // Dosya yoksa kullanıcıya uyarı veriyoruz
                    MessageBox.Show("Sipariş dosyası bulunamadı.", "Dosya Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya bilgi vermek için bir mesaj kutusu gösteriyoruz
                MessageBox.Show("Sipariş dosyası okunurken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SiparisiDosyadanSil(string masaNo, string urunAdi, string cinsi, string fiyat)
        {
            try
            {
                string dosyaYolu = @"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\siparis.txt";

                if (File.Exists(dosyaYolu))
                {
                    // Dosyadaki tüm siparişleri okuyup geçici bir liste oluşturuyoruz
                    List<string> siparisListesi = File.ReadAllLines(dosyaYolu).ToList();

                    // Silinecek siparişi bulmak için ilgili satırı arıyoruz
                    string siparisBilgisi = $"{masaNo},{urunAdi},{cinsi},{fiyat}";
                    int index = siparisListesi.FindIndex(x => x == siparisBilgisi);

                    if (index >= 0)
                    {
                        // Eğer sipariş bulunduysa, listeden sil
                        siparisListesi.RemoveAt(index);

                        // Dosyayı temizleyip güncellenmiş sipariş listesini yazıyoruz
                        File.WriteAllText(dosyaYolu, string.Join(Environment.NewLine, siparisListesi));
                    }
                }
                else
                {
                    // Dosya yoksa kullanıcıya uyarı veriyoruz
                    MessageBox.Show("Sipariş dosyası bulunamadı.", "Dosya Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya bilgi vermek için bir mesaj kutusu gösteriyoruz
                MessageBox.Show("Sipariş dosyasından silinirken bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            aktifMasaNo = 1;
            AktifMasaNoGuncelle(1);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            aktifMasaNo = 3;
            AktifMasaNoGuncelle(3);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            aktifMasaNo = 2;
            AktifMasaNoGuncelle(2);


        }

        private void button6_Click(object sender, EventArgs e)
        {
            aktifMasaNo = 4;
            AktifMasaNoGuncelle(4);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            aktifMasaNo = 5;
            AktifMasaNoGuncelle(5);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            aktifMasaNo = 6;
            AktifMasaNoGuncelle(6);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            aktifMasaNo = 7;
            AktifMasaNoGuncelle(7);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            aktifMasaNo = 8;
            AktifMasaNoGuncelle(8);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            aktifMasaNo = 9;
            AktifMasaNoGuncelle(9);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            aktifMasaNo = 10;
            AktifMasaNoGuncelle(10);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            aktifMasaNo = 11;
            AktifMasaNoGuncelle(11);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            aktifMasaNo = 12;

            AktifMasaNoGuncelle(12);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


        private void AktifMasaNoGuncelle(int masaNo)
        {
            aktifMasaNo = masaNo;
            label1.Text = "Aktif işlem yapılan masa no: " + masaNo;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            button19.Visible = true;
            dataGridView2.Visible = true;

            dataGridView2.Rows.Clear(); // DataGridView içeriğini temizle

            string tatliListesi = yiyecekListesi.GetTatliList();
            string[] tatliSatirlar = tatliListesi.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int currentRow = -1;

            foreach (string satir in tatliSatirlar)
            {
                string[] hucreler = satir.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (hucreler.Length == 2) // ':' karakterine göre ayırdığımızda her satırda 2 eleman olmalı
                {
                    string baslik = hucreler[0].Trim();
                    string deger = hucreler[1].Trim();

                    if (baslik == "Adı")
                    {
                        currentRow = dataGridView2.Rows.Add(); // Yeni bir satır ekle
                        dataGridView2.Rows[currentRow].Cells[0].Value = deger; // Tatlı adı ekle
                    }
                    else if (currentRow != -1)
                    {
                        // Son eklenen tatlı satırının diğer hücrelerini güncelle
                        if (baslik == "Cinsi")
                        {
                            dataGridView2.Rows[currentRow].Cells[1].Value = deger; // Cinsi ekle
                        }
                        else if (baslik == "Fiyatı")
                        {
                            dataGridView2.Rows[currentRow].Cells[2].Value = deger; // Fiyatı ekle
                        }
                        // KDV Oranı bilgisini burada görmezden gelerek eklemiyoruz

                        // Yukarıdaki kod, diğer özellikleri eklemiyor gibi görünebilir, ancak zaten yalnızca Cinsi ve Fiyatı ekliyoruz.
                        // Diğer özellikleri eklemek için else if (baslik == "ÖzellikAdi") bloklarını ekleyebilirsiniz.
                    }
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                if (aktifMasaNo == 0)
                {
                    MessageBox.Show("Lütfen bir masa seçin.", "Masa Seçimi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return; // Exit the event handler to prevent further processing
                }

                // Seçili ürün bilgilerini al
                string masaNo = aktifMasaNo.ToString();
                string urunAdi = dataGridView2.SelectedRows[0].Cells["Adi"].Value.ToString();
                string cinsi = dataGridView2.SelectedRows[0].Cells["Cinsi"].Value.ToString();
                string fiyat = dataGridView2.SelectedRows[0].Cells["Fiyat"].Value.ToString();

                // Sipariş bilgilerini siparis.txt dosyasına ekle
                SiparisiDosyayaEkle(masaNo, urunAdi, cinsi, fiyat);

                // Show a message box indicating that the ürün has been added to the selected masa
                string message = $"Masa No: {masaNo}\nÜrün Adı: {urunAdi}\nCinsi: {cinsi}\nFiyatı: {fiyat} TL";
                MessageBox.Show($"Sipariş başarıyla eklendi:\n\n{message}", "Ürün Eklendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Lütfen bir ürün seçin.", "Ürün Seçimi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }




        private void button17_Click(object sender, EventArgs e)
        {
            button19.Visible = true;

            dataGridView2.Visible = true;

            dataGridView2.Rows.Clear(); // DataGridView içeriğini temizle

            string salataListesi = yiyecekListesi.GetSalataList();
            string[] salataSatirlar = salataListesi.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int currentRow = -1;

            foreach (string satir in salataSatirlar)
            {
                string[] hucreler = satir.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (hucreler.Length == 2) // ':' karakterine göre ayırdığımızda her satırda 2 eleman olmalı
                {
                    string baslik = hucreler[0].Trim();
                    string deger = hucreler[1].Trim();

                    if (baslik == "Adı")
                    {
                        currentRow = dataGridView2.Rows.Add(); // Yeni bir satır ekle
                        dataGridView2.Rows[currentRow].Cells[0].Value = deger; // Salata adı ekle
                    }
                    else if (currentRow != -1)
                    {
                        // Son eklenen salata satırının diğer hücrelerini güncelle
                        if (baslik == "Cinsi")
                        {
                            dataGridView2.Rows[currentRow].Cells[1].Value = deger; // Cinsi ekle
                        }
                        else if (baslik == "Fiyatı")
                        {
                            dataGridView2.Rows[currentRow].Cells[2].Value = deger; // Fiyatı ekle
                        }
                        // KDV Oranı bilgisini burada görmezden gelerek eklemiyoruz

                        // Yukarıdaki kod, diğer özellikleri eklemiyor gibi görünebilir, ancak zaten yalnızca Cinsi ve Fiyatı ekliyoruz.
                        // Diğer özellikleri eklemek için else if (baslik == "ÖzellikAdi") bloklarını ekleyebilirsiniz.
                    }
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            button19.Visible = true;

            dataGridView2.Visible = true;

            dataGridView2.Rows.Clear(); // DataGridView içeriğini temizle

            string icecekListesi = yiyecekListesi.GetIcecekList();
            string[] icecekSatirlar = icecekListesi.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int currentRow = -1;

            foreach (string satir in icecekSatirlar)
            {
                string[] hucreler = satir.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (hucreler.Length == 2) // ':' karakterine göre ayırdığımızda her satırda 2 eleman olmalı
                {
                    string baslik = hucreler[0].Trim();
                    string deger = hucreler[1].Trim();

                    if (baslik == "Adı")
                    {
                        currentRow = dataGridView2.Rows.Add(); // Yeni bir satır ekle
                        dataGridView2.Rows[currentRow].Cells[0].Value = deger; // İçecek adı ekle
                    }
                    else if (currentRow != -1)
                    {
                        // Son eklenen içecek satırının diğer hücrelerini güncelle
                        if (baslik == "Cinsi")
                        {
                            dataGridView2.Rows[currentRow].Cells[1].Value = deger; // Cinsi ekle
                        }
                        else if (baslik == "Fiyatı")
                        {
                            dataGridView2.Rows[currentRow].Cells[2].Value = deger; // Fiyatı ekle
                        }
                        // KDV Oranı bilgisini burada görmezden gelerek eklemiyoruz

                        // Yukarıdaki kod, diğer özellikleri eklemiyor gibi görünebilir, ancak zaten yalnızca Cinsi ve Fiyatı ekliyoruz.
                        // Diğer özellikleri eklemek için else if (baslik == "ÖzellikAdi") bloklarını ekleyebilirsiniz.
                    }
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            button19.Visible = true;

            dataGridView2.Visible = true;

            dataGridView2.Rows.Clear(); // DataGridView içeriğini temizle

            string meyveListesi = yiyecekListesi.GetMeyveList();
            string[] meyveSatirlar = meyveListesi.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int currentRow = -1;

            foreach (string satir in meyveSatirlar)
            {
                string[] hucreler = satir.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (hucreler.Length == 2) // ':' karakterine göre ayırdığımızda her satırda 2 eleman olmalı
                {
                    string baslik = hucreler[0].Trim();
                    string deger = hucreler[1].Trim();

                    if (baslik == "Adı")
                    {
                        currentRow = dataGridView2.Rows.Add(); // Yeni bir satır ekle
                        dataGridView2.Rows[currentRow].Cells[0].Value = deger; // Meyve adı ekle
                    }
                    else if (currentRow != -1)
                    {
                        // Son eklenen meyve satırının diğer hücrelerini güncelle
                        if (baslik == "Cinsi")
                        {
                            dataGridView2.Rows[currentRow].Cells[1].Value = deger; // Cinsi ekle
                        }
                        else if (baslik == "Fiyatı")
                        {
                            dataGridView2.Rows[currentRow].Cells[2].Value = deger; // Fiyatı ekle
                        }
                        // KDV Oranı bilgisini burada görmezden gelerek eklemiyoruz

                        // Yukarıdaki kod, diğer özellikleri eklemiyor gibi görünebilir, ancak zaten yalnızca Cinsi ve Fiyatı ekliyoruz.
                        // Diğer özellikleri eklemek için else if (baslik == "ÖzellikAdi") bloklarını ekleyebilirsiniz.
                    }
                }
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            button19.Visible = true;

            dataGridView2.Visible = true;

            dataGridView2.Rows.Clear(); // DataGridView içeriğini temizle

            string yemekListesi = yiyecekListesi.GetYemekList();
            string[] yemekSatirlar = yemekListesi.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            int currentRow = -1;

            foreach (string satir in yemekSatirlar)
            {
                string[] hucreler = satir.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (hucreler.Length == 2) // ':' karakterine göre ayırdığımızda her satırda 2 eleman olmalı
                {
                    string baslik = hucreler[0].Trim();
                    string deger = hucreler[1].Trim();

                    if (baslik == "Adı")
                    {
                        currentRow = dataGridView2.Rows.Add(); // Yeni bir satır ekle
                        dataGridView2.Rows[currentRow].Cells[0].Value = deger; // Yemek adı ekle
                    }
                    else if (currentRow != -1)
                    {
                        // Son eklenen yemek satırının diğer hücrelerini güncelle
                        if (baslik == "Cinsi")
                        {
                            dataGridView2.Rows[currentRow].Cells[1].Value = deger; // Cinsi ekle
                        }
                        else if (baslik == "Fiyatı")
                        {
                            dataGridView2.Rows[currentRow].Cells[2].Value = deger; // Fiyatı ekle
                        }
                        // KDV Oranı bilgisini burada görmezden gelerek eklemiyoruz

                        // Yukarıdaki kod, diğer özellikleri eklemiyor gibi görünebilir, ancak zaten yalnızca Cinsi ve Fiyatı ekliyoruz.
                        // Diğer özellikleri eklemek için else if (baslik == "ÖzellikAdi") bloklarını ekleyebilirsiniz.
                    }
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
            button19.Visible = false;
            button21.Visible = true;

            label2.Visible = false;

            SiparisleriListele();

        }

        private void button21_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            button19.Visible = true;

            label2.Visible = true;
            button21.Visible = false;
            label2.Text = "";

        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            button21.Visible = false;

            label2.Text = "";
            label2.Visible = true;

        }

        private void button22_Click(object sender, EventArgs e)
        {
            this.Close(); // Menucs formunu kapatın
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçili sipariş bilgilerini al
                string masaNo = dataGridView1.SelectedRows[0].Cells["MasaNo"].Value.ToString();
                string urunAdi = dataGridView1.SelectedRows[0].Cells["UrunAdi"].Value.ToString();
                string cinsi = dataGridView1.SelectedRows[0].Cells["Cinsi"].Value.ToString();
                string fiyat = dataGridView1.SelectedRows[0].Cells["Fiyati"].Value.ToString();

                // Siparişi dosyadan sil
                SiparisiDosyadanSil(masaNo, urunAdi, cinsi, fiyat);

                // Siparişleri listeleme işlemini tekrar çağırarak DataGridView'i güncelle
                SiparisleriListele();
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz siparişi seçin.", "Sipariş Seçimi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
    
        }

    }



}
