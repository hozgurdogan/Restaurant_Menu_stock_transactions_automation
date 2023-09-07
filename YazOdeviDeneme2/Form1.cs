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
**                         DERSİN ALINDIĞI GRUP...:
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
    public partial class Form1 : Form
    {

        private LinkedList yiyecekListesi;
        BagliListe urunlerListesi = new BagliListe();

        public Form1()
        {
            InitializeComponent();
            yiyecekListesi = new LinkedList();
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Depo.txt dosyasını oluştur ve içeriği temizle
            File.WriteAllText(@"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\Depo.txt", "");

            // siparis.txt dosyasını oluştur ve içeriği temizle
            File.WriteAllText(@"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\siparis.txt", "");

            // yemekcesit.txt dosyasını oluştur ve içeriği temizle
            File.WriteAllText(@"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\yemekcesit.txt", "");

            // malzeme.txt dosyasını oluştur ve içeriği temizle
            File.WriteAllText(@"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\malzeme.txt", "");
            EkranAyarlari.CenterFormOnScreen(this);
            EkranAyarlari.ArkaPlanResmiSet(this);

            yiyecekListesi.AddTatli("Kazandibi", 80);
            yiyecekListesi.AddSalata("Sezar Salata", 100);
         //   yiyecekListesi.AddYemek("Kazandibi", 80);
            yiyecekListesi.AddMeyve("Kazandibi", 80);
            yiyecekListesi.AddIcecek("Kola", 20);

            // 5 adet tatlı ekleyelim
            yiyecekListesi.AddTatli("Sütlaç", 70);
            yiyecekListesi.AddTatli("Kadayıf", 90);
            yiyecekListesi.AddTatli("İrmik Helvası", 60);
            yiyecekListesi.AddTatli("Muhallebi", 75);
            yiyecekListesi.AddTatli("Revani", 85);

            // 5 adet salata ekleyelim
            yiyecekListesi.AddSalata("Coban Salata", 90);
            yiyecekListesi.AddSalata("Roka Salata", 85);
            yiyecekListesi.AddSalata("Mevsim Salatası", 95);
            yiyecekListesi.AddSalata("Patates Salatası", 80);
            yiyecekListesi.AddSalata("Akdeniz Salatası", 100);


            // Urun nesnelerini oluşturup urunListesi içerisine ekleyin


            void EkleYemek(string yemekAdi, decimal yemekFiyati, List<Urun> yemekUrunleri, int uretimMiktari)
            {
                string depoDosyaYoluu = @"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\Depo.txt";

                foreach (Urun urun in yemekUrunleri)
                {
                    string depoSatir = $"Ürün Adı: {urun.UrunAdi} Üretim Tarihi: {urun.UretimTarihi.ToString("dd.MM.yyyy")} Son Kullanma Tarihi: {urun.SonKullanmaTarihi.ToString("dd.MM.yyyy")} Kalori (gram): {urun.KaloriGram} Stok Adet: {0} Fiyat: {urun.Fiyat} TL";

                    // Kontrol ederek aynı ürün adına sahip ürünlerin varlığını kontrol edin
                    if (!DepoIceriyor(urun.UrunAdi, depoDosyaYoluu))
                    {
                        // Depo.txt dosyasına satırı ekleyin
                        using (StreamWriter writer = File.AppendText(depoDosyaYoluu))
                        {
                            writer.WriteLine(depoSatir);
                        }
                    }
                }

                // Kontrol ederek aynı yemek adına sahip yemeklerin varlığını kontrol edin
                if (!YemekCesidiIceriyor(yemekAdi))
                {
                    yiyecekListesi.AddYemek(yemekAdi, yemekFiyati, yemekUrunleri, uretimMiktari);
                }
            }

            bool YemekCesidiIceriyor(string yemekAdi)
            {
                string yemekCesitDosyaYolu = @"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\yemekcesit.txt";

                if (File.Exists(yemekCesitDosyaYolu))
                {
                    string[] yemekCesitSatirlar = File.ReadAllLines(yemekCesitDosyaYolu);
                    foreach (string satir in yemekCesitSatirlar)
                    {
                        if (satir.Contains($"Yemek Adı: {yemekAdi}"))
                        {
                            return true;
                        }
                    }
                }

                return false;
            }


            //20 adet ürün ekleniyor

            List<Urun> yeniYemekUrunleri1 = new List<Urun>
            {
                new Urun("Domates", DateTime.Now, DateTime.Now.AddMonths(1), 20, 50, 3),
                new Urun("Biber", DateTime.Now, DateTime.Now.AddMonths(1), 15, 40, 2),
                new Urun("Soğan", DateTime.Now, DateTime.Now.AddMonths(2), 10, 60, 1),
            };

            EkleYemek("Sebzeli Pilav", 40, yeniYemekUrunleri1, 15);


            List<Urun> yeniYemekUrunleri2 = new List<Urun>
            {
                new Urun("Pirinç", DateTime.Now, DateTime.Now.AddYears(2), 100, 200, 5),
                new Urun("Tavuk Göğsü", DateTime.Now, DateTime.Now.AddMonths(3), 150, 30, 12),
                new Urun("Sarımsak", DateTime.Now, DateTime.Now.AddMonths(4), 5, 100, 1),
            };

            EkleYemek("Tavuklu Pilav", 50, yeniYemekUrunleri2, 20);

            List<Urun> yeniYemekUrunleri3 = new List<Urun>
            {
                new Urun("Kıyma", DateTime.Now, DateTime.Now.AddMonths(6), 1000, 30, 25),
                new Urun("Yoğurt", DateTime.Now, DateTime.Now.AddMonths(2), 50, 20, 3),
                new Urun("Nane", DateTime.Now, DateTime.Now.AddMonths(1), 5, 50, 1),
            };

            EkleYemek("Yoğurtlu Kıymalı Makarna", 35, yeniYemekUrunleri3, 10);

            List<Urun> yeniYemekUrunleri4 = new List<Urun>
            {
                new Urun("Mercimek", DateTime.Now, DateTime.Now.AddYears(1), 200, 150, 4),
                new Urun("Un", DateTime.Now, DateTime.Now.AddYears(3), 500, 100, 2),
                new Urun("Zeytinyağı", DateTime.Now, DateTime.Now.AddMonths(6), 10, 80, 8),
            };

            EkleYemek("Mercimek Çorbası", 30, yeniYemekUrunleri4, 25);

            List<Urun> yeniYemekUrunleri5 = new List<Urun>
            {
                new Urun("Tavuk Göğsü", DateTime.Now, DateTime.Now.AddMonths(3), 150, 30, 12),
                new Urun("Bulgur", DateTime.Now, DateTime.Now.AddYears(2), 300, 50, 4),
                new Urun("Maydanoz", DateTime.Now, DateTime.Now.AddMonths(1), 5, 100, 1),
            };

            EkleYemek("Tavuklu Bulgur Pilavı", 45, yeniYemekUrunleri5, 18);



            List<Urun> yeniYemekUrunleri6 = new List<Urun>
            {
                new Urun("Patates", DateTime.Now, DateTime.Now.AddMonths(2), 30, 40, 2),
                new Urun("Havuç", DateTime.Now, DateTime.Now.AddMonths(2), 20, 30, 1),
                new Urun("Bezelye", DateTime.Now, DateTime.Now.AddMonths(1), 10, 35, 3),
            };

            EkleYemek("Sebzeli Nohut Pilavı", 35, yeniYemekUrunleri6, 12);

            List<Urun> yeniYemekUrunleri7 = new List<Urun>
            {
                new Urun("Balık", DateTime.Now, DateTime.Now.AddMonths(3), 120, 40, 15),
                new Urun("Ispanak", DateTime.Now, DateTime.Now.AddMonths(1), 25, 20, 3),
                new Urun("Limon", DateTime.Now, DateTime.Now.AddMonths(1), 5, 70, 2),
            };

            EkleYemek("Izgara Balık", 55, yeniYemekUrunleri7, 15);

            List<Urun> yeniYemekUrunleri8 = new List<Urun>
            {
                new Urun("Kuzu Et", DateTime.Now, DateTime.Now.AddMonths(3), 300, 80, 18),
                new Urun("Patlıcan", DateTime.Now, DateTime.Now.AddMonths(2), 15, 30, 2),
                new Urun("Yoğurt", DateTime.Now, DateTime.Now.AddMonths(1), 50, 20, 3),
            };

            EkleYemek("Kuzu Tandır", 70, yeniYemekUrunleri8, 10);

            List<Urun> yeniYemekUrunleri9 = new List<Urun>
            {
                new Urun("Makarna", DateTime.Now, DateTime.Now.AddYears(2), 200, 40, 5),
                new Urun("Kıyma", DateTime.Now, DateTime.Now.AddMonths(3), 150, 30, 12),
                new Urun("Domates", DateTime.Now, DateTime.Now.AddMonths(2), 20, 50, 3),
            };

            EkleYemek("Makarna Bolognese", 40, yeniYemekUrunleri9, 18);

            List<Urun> yeniYemekUrunleri10 = new List<Urun>
            {
                new Urun("Tavuk Göğsü", DateTime.Now, DateTime.Now.AddMonths(3), 150, 30, 12),
                new Urun("Sebzeler", DateTime.Now, DateTime.Now.AddMonths(1), 50, 20, 3),
                new Urun("Sos", DateTime.Now, DateTime.Now.AddMonths(1), 5, 70, 2),
            };

            EkleYemek("Tavuklu Sebzeli Karışık", 60, yeniYemekUrunleri10, 15);
            // Yemek 11
            List<Urun> yeniYemekUrunleri11 = new List<Urun>
            {
                new Urun("Kırmızı Et", DateTime.Now, DateTime.Now.AddMonths(3), 400, 70, 20),
                new Urun("Patates", DateTime.Now, DateTime.Now.AddMonths(2), 30, 40, 2),
                new Urun("Soğan", DateTime.Now, DateTime.Now.AddMonths(2), 10, 60, 1),
            };

            EkleYemek("Etli Patates", 55, yeniYemekUrunleri11, 18);

            // Yemek 12
            List<Urun> yeniYemekUrunleri12 = new List<Urun>
            {
                new Urun("Dana Kavurma", DateTime.Now, DateTime.Now.AddMonths(3), 350, 90, 22),
                new Urun("Pirinç", DateTime.Now, DateTime.Now.AddYears(2), 100, 200, 5),
                new Urun("Sarımsak", DateTime.Now, DateTime.Now.AddMonths(4), 5, 100, 1),
            };

            EkleYemek("Dana Kavurmalı Pilav", 70, yeniYemekUrunleri12, 20);

            // Yemek 13
            List<Urun> yeniYemekUrunleri13 = new List<Urun>
            {
                new Urun("Mantar", DateTime.Now, DateTime.Now.AddMonths(2), 25, 35, 4),
                new Urun("Tavuk Göğsü", DateTime.Now, DateTime.Now.AddMonths(3), 150, 30, 12),
                new Urun("Nane", DateTime.Now, DateTime.Now.AddMonths(1), 5, 50, 1),
            };

            EkleYemek("Tavuklu Mantar Sote", 40, yeniYemekUrunleri13, 12);

            // Yemek 14
            List<Urun> yeniYemekUrunleri14 = new List<Urun>
            {
                new Urun("Köfte", DateTime.Now, DateTime.Now.AddMonths(4), 250, 50, 18),
                new Urun("Patates", DateTime.Now, DateTime.Now.AddMonths(2), 30, 40, 2),
                new Urun("Yoğurt", DateTime.Now, DateTime.Now.AddMonths(1), 50, 20, 3),
            };

            EkleYemek("Patatesli Köfte", 60, yeniYemekUrunleri14, 15);

            // Yemek 15
            List<Urun> yeniYemekUrunleri15 = new List<Urun>
            {
                new Urun("Tavuk Göğsü", DateTime.Now, DateTime.Now.AddMonths(3), 150, 30, 12),
                new Urun("Sebzeler", DateTime.Now, DateTime.Now.AddMonths(1), 50, 20, 3),
                new Urun("Sos", DateTime.Now, DateTime.Now.AddMonths(1), 5, 70, 2),
            };

            EkleYemek("Tavuklu Sebzeli Karışık", 60, yeniYemekUrunleri15, 15);

            // Devam eden yemekleri burada sıralayarak ekleyebilirsiniz...

            // Yemek 16
            List<Urun> yeniYemekUrunleri16 = new List<Urun>
            {
                new Urun("Tavuk Kanat", DateTime.Now, DateTime.Now.AddMonths(3), 180, 45, 10),
                new Urun("Patates", DateTime.Now, DateTime.Now.AddMonths(2), 30, 40, 2),
                new Urun("Ketçap", DateTime.Now, DateTime.Now.AddMonths(1), 10, 60, 5),
            };

            EkleYemek("Tavuk Kanatlı Patates", 55, yeniYemekUrunleri16, 15);

            // Yemek 17
            List<Urun> yeniYemekUrunleri17 = new List<Urun>
            {
                new Urun("Balık", DateTime.Now, DateTime.Now.AddMonths(3), 120, 40, 15),
                new Urun("Ispanak", DateTime.Now, DateTime.Now.AddMonths(1), 25, 20, 3),
                new Urun("Limon", DateTime.Now, DateTime.Now.AddMonths(1), 5, 70, 2),
            };

            EkleYemek("Izgara Balık", 55, yeniYemekUrunleri17, 15);

            // Yemek 18
            List<Urun> yeniYemekUrunleri18 = new List<Urun>
            {
                new Urun("Kuzu Et", DateTime.Now, DateTime.Now.AddMonths(3), 300, 80, 18),
                new Urun("Patlıcan", DateTime.Now, DateTime.Now.AddMonths(2), 15, 30, 2),
                new Urun("Yoğurt", DateTime.Now, DateTime.Now.AddMonths(1), 50, 20, 3),
            };

            EkleYemek("Kuzu Tandır", 70, yeniYemekUrunleri18, 10);

            // Yemek 19
            List<Urun> yeniYemekUrunleri19 = new List<Urun>
            {
                new Urun("Makarna", DateTime.Now, DateTime.Now.AddYears(2), 200, 40, 5),
                new Urun("Kıyma", DateTime.Now, DateTime.Now.AddMonths(3), 150, 30, 12),
                new Urun("Domates", DateTime.Now, DateTime.Now.AddMonths(2), 20, 50, 3),
            };

            EkleYemek("Makarna Bolognese", 40, yeniYemekUrunleri19, 18);

            // Yemek 20
            List<Urun> yeniYemekUrunleri20 = new List<Urun>
            {
                new Urun("Tavuk Göğsü", DateTime.Now, DateTime.Now.AddMonths(3), 150, 30, 12),
                new Urun("Sebzeler", DateTime.Now, DateTime.Now.AddMonths(1), 50, 20, 3),
                new Urun("Sos", DateTime.Now, DateTime.Now.AddMonths(1), 5, 70, 2),
            };

            EkleYemek("Tavuklu Sebzeli Karışık", 60, yeniYemekUrunleri20, 15);




            // ...

            // Depo.txt dosyasının belirli bir ürünü içerip içermediğini kontrol eden metot
            bool DepoIceriyor(string urunAdi, string dosyaYolu)
            {
                string[] depoSatirlar = File.ReadAllLines(dosyaYolu);
                foreach (string satir in depoSatirlar)
                {
                    if (satir.Contains($"Ürün Adı: {urunAdi}"))
                    {
                        return true;
                    }
                }
                return false;
            }



            // 5 adet yemek ekleyelim
            /*         yiyecekListesi.AddYemek("Izgara Köfte", 120);
                 yiyecekListesi.AddYemek("Tavuk Şiş", 110);
                 yiyecekListesi.AddYemek("Mantı", 130);
                 yiyecekListesi.AddYemek("Mercimek Çorbası", 70);
                 yiyecekListesi.AddYemek("Etli Pide", 140);
            */
            // 5 adet meyve ekleyelim
            yiyecekListesi.AddMeyve("Elma", 40);
            yiyecekListesi.AddMeyve("Muz", 50);
            yiyecekListesi.AddMeyve("Üzüm", 45);
            yiyecekListesi.AddMeyve("Kiraz", 60);
            yiyecekListesi.AddMeyve("Kavun", 70);

            // 5 adet içecek ekleyelim
            yiyecekListesi.AddIcecek("Ayran", 10);
            yiyecekListesi.AddIcecek("Limonata", 25);
            yiyecekListesi.AddIcecek("Meyve Suyu", 30);
            yiyecekListesi.AddIcecek("Çay", 5);
            yiyecekListesi.AddIcecek("Su", 2);
            UpdateYiyecekListesi(yiyecekListesi); // yiyecekListesi'ni mainForm'da güncelleyin

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Menucs menuForm = new Menucs(yiyecekListesi, this); // yiyecekListesi ve mevcut Form1 örneğini Menucs formuna geçirin
            menuForm.ShowDialog();

        }

        public void UpdateYiyecekListesi(LinkedList newListesi)
        {
            yiyecekListesi = newListesi;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteFiles();
            this.Close();
        }
        private void DeleteFiles()
        {
            try
            {
                // Depo.txt dosyasını sil
                File.Delete(@"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\Depo.txt");

                // siparis.txt dosyasını sil
                File.Delete(@"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\siparis.txt");

                // yemekcesit.txt dosyasını sil
                File.Delete(@"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\yemekcesit.txt");

                // malzeme.txt dosyasını sil
                File.Delete(@"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\malzeme.txt");

               // MessageBox.Show("Dosyalar başarıyla silindi.", "Dosya Silme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dosya silme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Siparis siparisForm = new Siparis(yiyecekListesi); // yiyecekListesi parametresini Siparis formuna gönderin
            siparisForm.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MalzemeIslemleri malzemeForm = new MalzemeIslemleri(); // yiyecekListesi ve mevcut Form1 örneğini Menucs formuna geçirin
            malzemeForm.ShowDialog();
        }
    }
}
