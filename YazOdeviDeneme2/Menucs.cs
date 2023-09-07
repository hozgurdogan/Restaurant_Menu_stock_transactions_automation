using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace YazOdeviDeneme2
{
    public partial class Menucs : Form
    {
        public LinkedList yiyecekListesi;
        private Form1 mainForm; // Form1'e referansı tutmak için bir değişken ekleyin
        BagliListe urunlerListesi = new BagliListe();
        Urun tempUrun = new Urun("", DateTime.Now, DateTime.Now, 0, 0, 0);
        Boolean KontrolMazlemeEkleme = false;
        public List<Urun> UrunListesii { get; } = new List<Urun>();


        private bool urunEklenmedi = true;


        private Urun urun; // Urun sınıfından nesne tanımlayalım
        private const string dosyaAdi = "Depo.txt"; // Depo yerine Urun verilerini bu dosyada saklayalım
        public Menucs(LinkedList yiyecekListesi, Form1 mainForm)
        {
            InitializeComponent();
            this.yiyecekListesi = yiyecekListesi;
            this.mainForm = mainForm; // mainForm parametresini sınıf değişkenine atayın
                                      //   urun = new Urun(); // Urun nesnesini oluşturalım

            radioButton1.CheckedChanged += radioButton_CheckedChanged;
            radioButton2.CheckedChanged += radioButton_CheckedChanged;
            radioButton3.CheckedChanged += radioButton_CheckedChanged;
            radioButton4.CheckedChanged += radioButton_CheckedChanged;
            radioButton5.CheckedChanged += radioButton_CheckedChanged;


        }



        private void Menucs_Load(object sender, EventArgs e)
        {
            EkranAyarlari.CenterFormOnScreen(this);
            EkranAyarlari.ArkaPlanResmiSet(this);

            // DataGridView sütunlarını ayarlayın
            dataGridView1.Columns.Add("Adi", "Adı");
            dataGridView1.Columns.Add("Cinsi", "Cinsi");
            dataGridView1.Columns.Add("Fiyat", "Fiyatı");


            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 192, 128); // Başlık arka plan rengi
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White; // Başlık yazı rengi
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold); // Başlık yazı fontu

            dataGridView1.DefaultCellStyle.BackColor = Color.White; // Hücre arka plan rengi
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black; // Hücre yazı rengi
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 10); // Hücre yazı fontu

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 128, 128); // Seçim rengi

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray; // Alternatif satır rengi

            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // Hücre kenarlık stilini ayarla

            mainForm.UpdateYiyecekListesi(yiyecekListesi); // yiyecekListesi'ni mainForm'da güncelleyin


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            dataGridView1.Visible = true;

            dataGridView1.Rows.Clear(); // DataGridView içeriğini temizle

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
                        currentRow = dataGridView1.Rows.Add(); // Yeni bir satır ekle
                        dataGridView1.Rows[currentRow].Cells[0].Value = deger; // Tatlı adı ekle
                    }
                    else if (currentRow != -1)
                    {
                        // Son eklenen tatlı satırının diğer hücrelerini güncelle
                        if (baslik == "Cinsi")
                        {
                            dataGridView1.Rows[currentRow].Cells[1].Value = deger; // Cinsi ekle
                        }
                        else if (baslik == "Fiyatı")
                        {
                            dataGridView1.Rows[currentRow].Cells[2].Value = deger; // Fiyatı ekle
                        }
                        // KDV Oranı bilgisini burada görmezden gelerek eklemiyoruz

                        // Yukarıdaki kod, diğer özellikleri eklemiyor gibi görünebilir, ancak zaten yalnızca Cinsi ve Fiyatı ekliyoruz.
                        // Diğer özellikleri eklemek için else if (baslik == "ÖzellikAdi") bloklarını ekleyebilirsiniz.
                    }
                }
            }
        }





        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label1.Text = "Salata ekleme menüsü";
            label3.Visible = true;
            label4.Visible = true;
            textBox4.Visible = true;
            textBox3.Visible = true;
            button11.Visible = true;

            string adi = textBox4.Text;
            decimal fiyat = decimal.Parse(textBox3.Text);
            yiyecekListesi.AddSalata(adi, fiyat);
            MessageBox.Show("Tatlı başarıyla eklendi.");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;

            dataGridView1.Visible = true;

            dataGridView1.Visible = true;

            dataGridView1.Rows.Clear(); // DataGridView içeriğini temizle

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
                        currentRow = dataGridView1.Rows.Add(); // Yeni bir satır ekle
                        dataGridView1.Rows[currentRow].Cells[0].Value = deger; // Salata adı ekle
                    }
                    else if (currentRow != -1)
                    {
                        // Son eklenen salata satırının diğer hücrelerini güncelle
                        if (baslik == "Cinsi")
                        {
                            dataGridView1.Rows[currentRow].Cells[1].Value = deger; // Cinsi ekle
                        }
                        else if (baslik == "Fiyatı")
                        {
                            dataGridView1.Rows[currentRow].Cells[2].Value = deger; // Fiyatı ekle
                        }
                        // KDV Oranı bilgisini burada görmezden gelerek eklemiyoruz

                        // Yukarıdaki kod, diğer özellikleri eklemiyor gibi görünebilir, ancak zaten yalnızca Cinsi ve Fiyatı ekliyoruz.
                        // Diğer özellikleri eklemek için else if (baslik == "ÖzellikAdi") bloklarını ekleyebilirsiniz.
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;

            dataGridView1.Rows.Clear(); // DataGridView içeriğini temizle

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
                        currentRow = dataGridView1.Rows.Add(); // Yeni bir satır ekle
                        dataGridView1.Rows[currentRow].Cells[0].Value = deger; // İçecek adı ekle
                    }
                    else if (currentRow != -1)
                    {
                        // Son eklenen içecek satırının diğer hücrelerini güncelle
                        if (baslik == "Cinsi")
                        {
                            dataGridView1.Rows[currentRow].Cells[1].Value = deger; // Cinsi ekle
                        }
                        else if (baslik == "Fiyatı")
                        {
                            dataGridView1.Rows[currentRow].Cells[2].Value = deger; // Fiyatı ekle
                        }
                        // KDV Oranı bilgisini burada görmezden gelerek eklemiyoruz

                        // Yukarıdaki kod, diğer özellikleri eklemiyor gibi görünebilir, ancak zaten yalnızca Cinsi ve Fiyatı ekliyoruz.
                        // Diğer özellikleri eklemek için else if (baslik == "ÖzellikAdi") bloklarını ekleyebilirsiniz.
                    }
                }
            }
        }



        private void button10_Click(object sender, EventArgs e)
        {

        }


        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (urunEklenmedi == true && label1.Text != "Yemek Ekle")
            {
                DialogResult result = MessageBox.Show("Ürün eklenmeden yiyecek eklemeye çalışıyorsunuz. Devam etmek istiyor musunuz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            else if (urunEklenmedi == true && label1.Text == "Yemek Ekle")
            {
                DialogResult result = MessageBox.Show("Ürün eklenmeden yiyecek eklemeye çalışıyorsunuz,Lütfen öncesinde en az 1 adet malzeme ekleyin!!", "Uyarı");
                return;
            }

            else
            {
                if (yiyecekListesi.YiyecekOncedenVarmi(textBox4.Text))
                {
                    MessageBox.Show("Ürün başarıyla eklenemedi...\n" +
                        "Aynı isimde Ürün mevcut");


                }
                else
                {

                    if (label1.Text == "Tatlı Ekle")
                    {

                        if (textBox4.Text == "" || textBox3.Text == "")
                        {

                            MessageBox.Show("Boş bırakmayın");

                        }

                        else
                        {
                            string adi = textBox4.Text;
                            decimal fiyat = decimal.Parse(textBox3.Text);
                            yiyecekListesi.AddTatli(adi, fiyat);
                            MessageBox.Show("Tatlı başarıyla eklendi.");

                        }

                    }
                    else if (label1.Text == "Salata Ekle")
                    {
                        if (textBox4.Text == "" || textBox3.Text == "")
                        {

                            MessageBox.Show("Boş bırakmayın");

                        }
                        else
                        {
                            string adi = textBox4.Text;
                            decimal fiyat = decimal.Parse(textBox3.Text);
                            yiyecekListesi.AddSalata(adi, fiyat);
                            MessageBox.Show("Tatlı başarıyla eklendi.");
                        }

                    }
                    else if (label1.Text == "Icecek Ekle")
                    {
                        if (textBox4.Text == "" || textBox3.Text == "")
                        {

                            MessageBox.Show("Boş bırakmayın");

                        }
                        else
                        {

                            string adi = textBox4.Text;
                            decimal fiyat = decimal.Parse(textBox3.Text);
                            yiyecekListesi.AddIcecek(adi, fiyat);
                            MessageBox.Show("İcecek başarıyla eklendi.");
                        }
                    }
                    else if (label1.Text == "Meyve Ekle")
                    {
                        if (textBox4.Text == "" || textBox3.Text == "")
                        {

                            MessageBox.Show("Boş bırakmayın");

                        }
                        else
                        {

                            string adi = textBox4.Text;
                            decimal fiyat = decimal.Parse(textBox3.Text);
                            yiyecekListesi.AddMeyve(adi, fiyat);
                            MessageBox.Show("Meyve başarıyla eklendi.");
                        }


                    }
                    else if (label1.Text == "Yemek Ekle")
                    {
                        if (textBox4.Text == "" || textBox3.Text == "" || textBox1.Text == "")
                        {
                            MessageBox.Show("Boş bırakmayın");
                        }
                        else
                        {
                            string adi = textBox4.Text;
                            decimal fiyat = decimal.Parse(textBox3.Text);

                            if (int.TryParse(textBox1.Text, out int uretimMiktari))
                            {
                                yiyecekListesi.AddYemek(adi, fiyat, UrunListesii, uretimMiktari);
                               MessageBox.Show("Yemek başarıyla eklendi."+uretimMiktari+"kadar");
                            }
                            else
                            {
                                MessageBox.Show("Üretim miktarı için geçerli bir sayı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }



                }





                label1.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                textBox4.Visible = false;
                textBox3.Visible = false;
                button11.Visible = false;
                textBox3.Text = "";
                textBox4.Text = "";




                button1.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button2.Visible = true;


                radioButton1.Visible = false;
                radioButton2.Visible = false;
                radioButton3.Visible = false;
                radioButton4.Visible = false;
                radioButton5.Visible = false;

            }




        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            dataGridView1.Visible = false;
            radioButton1.Visible = true;
            radioButton2.Visible = true;

            radioButton3.Visible = true;
            radioButton4.Visible = true;

            radioButton5.Visible = true;


            button1.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button2.Visible = false;

            label1.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label15.Visible = true;
            textBox1.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            button11.Visible = true;
            label1.Text = "" + radioButton1.Text;

            if (radioButton2.Checked)
            {
                label1.Text = "" + radioButton2.Text;


            }
        }
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton.Checked)
            {
                label1.Text = radioButton.Text;
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;

            dataGridView1.Visible = true;

            dataGridView1.Rows.Clear(); // DataGridView içeriğini temizle

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
                        currentRow = dataGridView1.Rows.Add(); // Yeni bir satır ekle
                        dataGridView1.Rows[currentRow].Cells[0].Value = deger; // Meyve adı ekle
                    }
                    else if (currentRow != -1)
                    {
                        // Son eklenen meyve satırının diğer hücrelerini güncelle
                        if (baslik == "Cinsi")
                        {
                            dataGridView1.Rows[currentRow].Cells[1].Value = deger; // Cinsi ekle
                        }
                        else if (baslik == "Fiyatı")
                        {
                            dataGridView1.Rows[currentRow].Cells[2].Value = deger; // Fiyatı ekle
                        }
                        // KDV Oranı bilgisini burada görmezden gelerek eklemiyoruz

                        // Yukarıdaki kod, diğer özellikleri eklemiyor gibi görünebilir, ancak zaten yalnızca Cinsi ve Fiyatı ekliyoruz.
                        // Diğer özellikleri eklemek için else if (baslik == "ÖzellikAdi") bloklarını ekleyebilirsiniz.
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            dataGridView1.Visible = true;

            dataGridView1.Visible = true;

            dataGridView1.Rows.Clear(); // DataGridView içeriğini temizle

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
                        currentRow = dataGridView1.Rows.Add(); // Yeni bir satır ekle
                        dataGridView1.Rows[currentRow].Cells[0].Value = deger; // Yemek adı ekle
                    }
                    else if (currentRow != -1)
                    {
                        // Son eklenen yemek satırının diğer hücrelerini güncelle
                        if (baslik == "Cinsi")
                        {
                            dataGridView1.Rows[currentRow].Cells[1].Value = deger; // Cinsi ekle
                        }
                        else if (baslik == "Fiyatı")
                        {
                            dataGridView1.Rows[currentRow].Cells[2].Value = deger; // Fiyatı ekle
                        }
                        // KDV Oranı bilgisini burada görmezden gelerek eklemiyoruz

                        // Yukarıdaki kod, diğer özellikleri eklemiyor gibi görünebilir, ancak zaten yalnızca Cinsi ve Fiyatı ekliyoruz.
                        // Diğer özellikleri eklemek için else if (baslik == "ÖzellikAdi") bloklarını ekleyebilirsiniz.
                    }
                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                string adi = selectedRow.Cells["Adi"].Value.ToString();
                string cinsi = selectedRow.Cells["Cinsi"].Value.ToString();
                decimal fiyat = Convert.ToDecimal(selectedRow.Cells["Fiyat"].Value);
                label2.Text = adi + " Cinsi... " + cinsi + " fiyatı: " + fiyat;
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            mainForm.UpdateYiyecekListesi(yiyecekListesi); // yiyecekListesi'ni mainForm'da güncelleyin
            this.Close(); // Menucs formunu kapatın
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            string urunAdi = textBox2.Text;
            int kaloriGram, stokAdet, fiyat;

            int.TryParse(textBox6.Text, out kaloriGram);
            int.TryParse(textBox7.Text, out stokAdet);
            int.TryParse(textBox8.Text, out fiyat);
            string UretimTarihi = dateTimePicker1.Value.ToString("dd.MM.yyyy"); // Tarih formatını isteğinize göre ayarlayabilirsiniz
            string SonKullanmaTarihi  = dateTimePicker2.Value.ToString("dd.MM.yyyy"); // Tarih formatını isteğinize göre ayarlayabilirsiniz
            DateTime uretimTarihi = DateTime.ParseExact(UretimTarihi, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            DateTime sonKullanmaTarihi = DateTime.ParseExact(SonKullanmaTarihi, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            Urun tempUrun2 = new Urun(urunAdi, DateTime.Now, DateTime.Now, 0, 0, 0);

            tempUrun2.UrunAdi= urunAdi;
            tempUrun2.UretimTarihi = uretimTarihi;
            tempUrun2.SonKullanmaTarihi= sonKullanmaTarihi;
            tempUrun2.KaloriGram= kaloriGram;
            tempUrun2.StokAdet= stokAdet;
            tempUrun2.Fiyat= fiyat;
            UrunListesii.Add(tempUrun2);

            // Tüm dönüşümler başarılı bir şekilde gerçekleşti
            // urunlerListesi'ne ürünü ekleyebiliriz
            urunlerListesi.Ekle(urunAdi, uretimTarihi, sonKullanmaTarihi, kaloriGram, stokAdet, fiyat);

            // GroupBox ve içindeki elemanları görünür yap
            groupBox1.Visible = true;

            // Başarılı bir şekilde eklendi mesajını göster
            string message = $"Ürün Adı: {urunAdi}\nÜretim Tarihi: {uretimTarihi:dd.MM.yyyy}\nSon Kullanma Tarihi: {sonKullanmaTarihi:dd.MM.yyyy}\nKalori (gram): {kaloriGram}\nStok Adet: {stokAdet}\nFiyat: {fiyat} TL";

            MessageBox.Show(message);
            urunEklenmedi = false;
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();

        }

        private void button9_Click_2(object sender, EventArgs e)
        {
            UrunListesii.Clear();
            urunEklenmedi=true;
        }

     

        private void YemekSilme_Click(object sender, EventArgs e)
        {
            YemekAramaTexBox.Visible = true;
            YemekAraButton.Visible = true;
            Sil.Visible = true;

        }

        private void Sil_Click(object sender, EventArgs e)
        {
            string arananYemekAdi = YemekAramaTexBox.Text;

            if (!string.IsNullOrWhiteSpace(arananYemekAdi))
            {
                string klasorAdi = @"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler";
                string dosyaYolu = Path.Combine(klasorAdi, "yemekcesit.txt");
                string malzemeDosyaYolu = Path.Combine(klasorAdi, "malzeme.txt");

                List<string> yemekLines = File.ReadAllLines(dosyaYolu).ToList();
                List<string> malzemeLines = File.ReadAllLines(malzemeDosyaYolu).ToList();

                int indexToRemove = -1;
                for (int i = 0; i < yemekLines.Count; i++)
                {
                    if (yemekLines[i].Contains($"Yemek Adı: {arananYemekAdi}"))
                    {
                        indexToRemove = i;
                        break;
                    }
                }

                if (indexToRemove != -1)
                {
                    int yemekSayisi = yiyecekListesi.GetYemekSayisi();
                
                        yemekLines.RemoveAt(indexToRemove);
                        File.WriteAllLines(dosyaYolu, yemekLines);

                        int malzemeIndexToRemove = malzemeLines.IndexOf($"Yemek Adı: {arananYemekAdi}");
                        if (malzemeIndexToRemove != -1)
                        {
                            int malzemeBaslangicIndex = malzemeIndexToRemove;
                            int malzemeBitisIndex = malzemeLines.IndexOf("", malzemeBaslangicIndex);

                            if (malzemeBitisIndex != -1)
                            {
                                malzemeLines.RemoveRange(malzemeBaslangicIndex, malzemeBitisIndex - malzemeBaslangicIndex + 1);
                            }
                            else
                            {
                                malzemeLines.RemoveRange(malzemeBaslangicIndex, malzemeLines.Count - malzemeBaslangicIndex);
                            }

                            File.WriteAllLines(malzemeDosyaYolu, malzemeLines);
                        }
                        yiyecekListesi.SilYemek(arananYemekAdi);
                        MessageBox.Show($"{arananYemekAdi} yemeği ve malzemeleri başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else
                {
                    MessageBox.Show($"{arananYemekAdi} yemeği bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz yemeğin adını girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }





        private void YemekAraButton_Click(object sender, EventArgs e)
        {
            string arananYemekAdi = YemekAramaTexBox.Text; // Göstermek istediğiniz yemek adı

            yiyecekListesi.GosterYemekUrunleri(arananYemekAdi);
        }
    }
}