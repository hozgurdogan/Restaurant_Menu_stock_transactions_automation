
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using System.Text.RegularExpressions;

namespace YazOdeviDeneme2
{


        class Yiyecek
        {
            public string Adi { get; set; }
            public string Cinsi { get; } // Cinsi, sınıfın adını temsil edecek şekilde değiştirildi
            public decimal Fiyat { get; set; }
            public double KDVOrani { get; } = 8; // KDV oranı %8 olarak tanımlanıyor

        int UretimMiktari;


        public Yiyecek(string adi, string cinsi, decimal fiyat)
            {
                Adi = adi;
                Cinsi = cinsi;
                Fiyat = fiyat;
            }
        }

        class Salata : Yiyecek
        {
            public Salata(string adi, decimal fiyat) : base(adi, "Salata", fiyat) // Cinsiyet otomatik olarak "Salata" olarak ayarlanıyor
            {
            }
        }

        class Tatli : Yiyecek
        {

            public Tatli(string adi, decimal fiyat) : base(adi, "Tatlı", fiyat) // Cinsiyet otomatik olarak "Tatlı" olarak ayarlanıyor
            {
            }
        }

        class Icecek : Yiyecek
        {
            public Icecek(string adi, decimal fiyat) : base(adi, "İçecek", fiyat) // Cinsiyet otomatik olarak "İçecek" olarak ayarlanıyor
            {
            }
        }

        class Yemek : Yiyecek
        {
        public List<Urun> UrunListesi { get; } = new List<Urun>();
        public int UretimMiktari { get; set; } // Üretim miktarı özelliği eklendi
        public decimal genelTutar; // Üretim miktarı özelliği eklendi

        public decimal MaliyetBirimUrun; // Üretim miktarı özelliği eklendi


        public decimal HesaplaUretimMaliyetiBirUrun()
        {
            decimal malzemeToplami = 0;

            foreach (Urun urun in UrunListesi)
            {
                string depoDosyaYolu = @"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\Depo.txt";
                string[] depoSatirlar = File.ReadAllLines(depoDosyaYolu);

                foreach (string satir in depoSatirlar)
                {
                    if (satir.Contains($"Ürün Adı: {urun.UrunAdi}"))
                    {
                        string[] tokens = satir.Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries);
                        int fiyatIndex = Array.IndexOf(tokens, "Fiyat");
                        decimal malzemeFiyati = decimal.Parse(tokens[fiyatIndex + 1]);

                        Console.WriteLine($"Ürün Adı: {urun.UrunAdi}, Malzeme Fiyatı: {malzemeFiyati}");

                        malzemeToplami += malzemeFiyati;
                        break; // Malzeme bulundu, döngüyü sonlandır
                    }
                }
            }

            return malzemeToplami;
        }






        public decimal HesaplaUretimMaliyeti()
        {
            return Fiyat * UretimMiktari;
        }
        public Yemek(string adi, decimal fiyat) : base(adi, "Yemek", fiyat) // Cinsiyet otomatik olarak "Yemek" olarak ayarlanıyor
        {
        }
        public void UrunEkle(Urun urun)
        {
            UrunListesi.Add(urun);
        }
    }

    class Meyve : Yiyecek
        {
            public Meyve(string adi, decimal fiyat) : base(adi, "Meyve", fiyat) // Cinsiyet otomatik olarak "Meyve" olarak ayarlanıyor
            {
            }
        }

    public class LinkedList
    {

        private SalataNode salataHead;
            private TatliNode tatliHead;
            private IcecekNode icecekHead;
            private YemekNode yemekHead;
            private MeyveNode meyveHead;
            private YiyecekNode yiyeceklerHead;

        public float ToplamDegerYemek;


        public void AddSalata(string adi, decimal fiyat)
            {
                if (!YiyecekOncedenVarmi(adi))
                {
                    SalataNode newSalataNode = new SalataNode(new Salata(adi, fiyat));

                    if (salataHead == null)
                    {
                        salataHead = newSalataNode;
                    }
                    else
                    {
                        SalataNode current = salataHead;
                        while (current.Next != null)
                        {
                            current = current.Next;
                        }
                        current.Next = newSalataNode;
                    }

                    AddYiyecek(newSalataNode.salata);
            }
        }

        public void AddTatli(string adi, decimal fiyat)
        {
            if (!YiyecekOncedenVarmi(adi))
            {
                TatliNode newTatliNode = new TatliNode(new Tatli(adi, fiyat));

                if (tatliHead == null)
                {
                    tatliHead = newTatliNode;
                }
                else
                {
                    TatliNode current = tatliHead;
                    while (current.Next != null)
                    {
                        current = current.Next;
                    }
                    current.Next = newTatliNode;
                }

                AddYiyecek(newTatliNode.tatli);
            }
        }


        public void AddIcecek(string adi, decimal fiyat)
            {
                if (!YiyecekOncedenVarmi(adi))
                {
                    IcecekNode newIcecekNode = new IcecekNode(new Icecek(adi, fiyat));

                    if (icecekHead == null)
                    {
                        icecekHead = newIcecekNode;
                    }
                    else
                    {
                        IcecekNode current = icecekHead;
                        while (current.Next != null)
                        {
                            current = current.Next;
                        }
                        current.Next = newIcecekNode;
                    }

                    AddYiyecek(newIcecekNode.icecek);
            }
        }
        public int GetYemekSayisi()
        {
            int count = 0;
            YiyecekNode current = yiyeceklerHead;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }

        public void AddYemek(string adi, decimal fiyat, List<Urun> urunListesi, int uretimMiktari)
        {
             
            if (!YiyecekOncedenVarmi(adi))
            {
                string depoDosyaYolu = @"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\Depo.txt";
                string[] depoSatirlar = File.ReadAllLines(depoDosyaYolu);

                for (int i = 0; i < depoSatirlar.Length; i++)
                {
                    string satir = depoSatirlar[i];
                    foreach (Urun urun in urunListesi)
                    {
                        if (satir.Contains($"Ürün Adı: {urun.UrunAdi}"))
                        {
                            string[] tokens = Regex.Split(satir, @"[ :]+");
                            int stokIndex = Array.IndexOf(tokens, "Stok");
                            int stokBilgisi = int.Parse(tokens[stokIndex + 2]);
                            
                            stokBilgisi -= uretimMiktari;
                            tokens[stokIndex + 2] = stokBilgisi.ToString();
                            depoSatirlar[i] = string.Join(" ", tokens).Replace(" :", ":").Replace("Ürün Adı", "Ürün Adı:").Replace("Üretim Tarihi", "Üretim Tarihi:").Replace("Son Kullanma Tarihi", "Son Kullanma Tarihi:").Replace("Kalori (gram)", "Kalori (gram):").Replace("Stok Adet", "Stok Adet:").Replace("Fiyat", "Fiyat:");
                        }
                    }
                }

                File.WriteAllLines(depoDosyaYolu, depoSatirlar);

                YemekNode newYemekNode = new YemekNode(new Yemek(adi, fiyat)
                {
                    UretimMiktari = uretimMiktari // Üretim miktarı atanıyor
                    
                });
                newYemekNode.yemek.genelTutar = newYemekNode.yemek.HesaplaUretimMaliyeti();
        //        MessageBox.Show($"Yemek Üretim Maliyeti: {newYemekNode.yemek.genelTutar}", "Üretim Maliyeti", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // ...
                newYemekNode.yemek.MaliyetBirimUrun = newYemekNode.yemek.HesaplaUretimMaliyetiBirUrun();
         //       MessageBox.Show($"Yemek Üretim Maliyeti: {newYemekNode.yemek.MaliyetBirimUrun}", "Üretim Maliyeti", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // ...


                if (yemekHead == null)
                {

                    yemekHead = newYemekNode;

                }
                else
                {
                    YemekNode current = yemekHead;
                    while (current.Next != null)
                    {
                        current = current.Next;
                    }
                    current.Next = newYemekNode;
                }

                AddYiyecek(newYemekNode.yemek);
                newYemekNode.yemek.UrunListesi.AddRange(urunListesi);

                // Yemek bilgilerini dosyaya ekle
                try
                {
                    string klasorAdi = @"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler";
                    string dosyaYolu = Path.Combine(klasorAdi, "yemekcesit.txt");

                    string urunler = string.Join(" | ", urunListesi.Select(urun => $"{urun.UrunAdi}: {urun.Fiyat}"));
                    string yemekBilgileri = $"Yemek Adı: {adi} | Fiyatı: {fiyat}";

                    using (StreamWriter writer = new StreamWriter(dosyaYolu, true))
                    {
                        writer.WriteLine(yemekBilgileri);
                        writer.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Dosya yazma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                try
                {
                    string klasorAdi = @"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler";
                    string malzemeDosyaYolu = Path.Combine(klasorAdi, "malzeme.txt");

                    using (StreamWriter writer = new StreamWriter(malzemeDosyaYolu, true))
                    {
                        writer.WriteLine($"Yemek Adı: {adi}");
                        writer.WriteLine("Malzemeler:");
                        foreach (Urun urun in urunListesi)
                        {
                            writer.WriteLine($"- {urun.UrunAdi}");
                        }
                        writer.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"malzeme.txt dosyasına yazma hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        public void GosterYemekUrunleri(string yemekAdi)
        {
            YemekNode current = yemekHead;

            while (current != null)
            {
                if (current.yemek.Adi == yemekAdi)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine($"{current.yemek.Adi} Yemeği İçin Ürünler:");

                    foreach (Urun urun in current.yemek.UrunListesi)
                    {
                        sb.AppendLine($"Ürün Adı: {urun.UrunAdi}");
                        sb.AppendLine($"Ürün Fiyatı: {urun.Fiyat}");
                        sb.AppendLine($"KDV Oranı: {urun.UretimTarihi}%");
                        sb.AppendLine();
                        ToplamDegerYemek += urun.Fiyat; // Ürün fiyatını toplamFiyat'a ekleyin

                    }

                    MessageBox.Show(sb.ToString(), $"{current.yemek.Adi} Yemeği Ürünleri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // İstenen yemeği bulduk, fonksiyondan çık
                }
                current = current.Next;
            }

            MessageBox.Show($"'{yemekAdi}' isimli yemek bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void AddMeyve(string adi, decimal fiyat)
            {
                if (!YiyecekOncedenVarmi(adi))
                {
                    MeyveNode newMeyveNode = new MeyveNode(new Meyve(adi, fiyat));

                    if (meyveHead == null)
                    {
                        meyveHead = newMeyveNode;
                    }
                    else
                    {
                        MeyveNode current = meyveHead;
                        while (current.Next != null)
                        {
                            current = current.Next;
                        }
                        current.Next = newMeyveNode;
                    }

                    AddYiyecek(newMeyveNode.meyve);
                }
            }

            private void AddYiyecek(Yiyecek yiyecek)
            {
                YiyecekNode newYiyecekNode = new YiyecekNode(yiyecek);

                if (yiyeceklerHead == null)
                {
                    yiyeceklerHead = newYiyecekNode;
                }
                else
                {
                    YiyecekNode current = yiyeceklerHead;
                    while (current.Next != null)
                    {
                        current = current.Next;
                    }
                    current.Next = newYiyecekNode;
                }
            }


        public void GosterTatliBilgileri(string tatliIsmi)
        {
            
            TatliNode current = tatliHead;

            
            while (current != null)
            {
                if (current.tatli.Adi == tatliIsmi)
                {
                    string mesaj = $"Tatlı Adı: {current.tatli.Adi}\n" +
                                   $"Tatlı Cinsi: {current.tatli.Cinsi}\n" +
                                   $"Tatlı Fiyatı: {current.tatli.Fiyat}\n" +
                                   $"KDV Oranı: {current.tatli.KDVOrani}%";

                    MessageBox.Show(mesaj, "Tatlı Bilgileri", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return; // İstenen tatlıyı bulduk, fonksiyondan çık
                }
                current = current.Next;
            }

            MessageBox.Show($"'{tatliIsmi}' isimli tatlı bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        public string GetSalataList()
            {
                StringBuilder sb = new StringBuilder();

                if (salataHead == null)
                {
                    sb.AppendLine("Salata listesi boş.");
                }
                else
                {
                    sb.AppendLine("Salata Listesi:");
                    SalataNode current = salataHead;
                    while (current != null)
                    {
                        sb.AppendLine("Adı: " + current.salata.Adi);
                        sb.AppendLine("Cinsi: " + current.salata.Cinsi);
                        sb.AppendLine("Fiyatı: " + current.salata.Fiyat);
                        sb.AppendLine("KDV Oranı: " + current.salata.KDVOrani + "%");
                        sb.AppendLine();
                        current = current.Next;
                    }
                }

                return sb.ToString();
            }



            public string GetTatliList()
            {
                StringBuilder sb = new StringBuilder();

                if (tatliHead == null)
                {
                    sb.AppendLine("Tatlı listesi boş.");
                }
                else
                {
                    sb.AppendLine("Tatlı Listesi:");
                    TatliNode current = tatliHead;
                    while (current != null)
                    {
                        sb.AppendLine("Adı: " + current.tatli.Adi);
                        sb.AppendLine("Cinsi: " + current.tatli.Cinsi);
                        sb.AppendLine("Fiyatı: " + current.tatli.Fiyat);
                        sb.AppendLine("KDV Oranı: " + current.tatli.KDVOrani + "%");
                        sb.AppendLine();
                        current = current.Next;
                    }
                }

                return sb.ToString();
            }

    

            public string GetIcecekList()
            {
                StringBuilder sb = new StringBuilder();

                if (icecekHead == null)
                {
                    sb.AppendLine("İçecek listesi boş.");
                }
                else
                {
                    sb.AppendLine("İçecek Listesi:");
                    IcecekNode current = icecekHead;
                    while (current != null)
                    {
                        sb.AppendLine("Adı: " + current.icecek.Adi);
                        sb.AppendLine("Cinsi: " + current.icecek.Cinsi);
                        sb.AppendLine("Fiyatı: " + current.icecek.Fiyat);
                        sb.AppendLine("KDV Oranı: " + current.icecek.KDVOrani + "%");
                        sb.AppendLine();
                        current = current.Next;
                    }
                }

                return sb.ToString();
        }

        public string GetYemekList()
        {
            StringBuilder sb = new StringBuilder();

            if (yemekHead == null)
            {
                sb.AppendLine("Yemek listesi boş.");
            }
            else
            {
                sb.AppendLine("Yemek Listesi:");
                YemekNode current = yemekHead;
                while (current != null)
                {
                    sb.AppendLine("Adı: " + current.yemek.Adi);
                    sb.AppendLine("Cinsi: " + current.yemek.Cinsi);
                    sb.AppendLine("Fiyatı: " + current.yemek.Fiyat);
                    sb.AppendLine("KDV Oranı: " + current.yemek.KDVOrani + "%");
                    sb.AppendLine();
                    current = current.Next;
                }
            }

            return sb.ToString();
        }
        public void SilYemek(string arananYemekAdi)
        {
            YemekNode current = yemekHead;
            YemekNode prev = null;

            while (current != null)
            {
                if (current.yemek.Adi == arananYemekAdi)
                {
                    // Yemek düğümünü listeden çıkart
                    if (prev == null)
                    {
                        yemekHead = current.Next;
                    }
                    else
                    {
                        prev.Next = current.Next;
                    }

                    // Malzeme düğümlerini sil

                    break;
                }

                prev = current;
                current = current.Next;
            }
        }

        public string GetMeyveList()
            {
                StringBuilder sb = new StringBuilder();

                if (meyveHead == null)
                {
                    sb.AppendLine("Meyve listesi boş.");
                }
                else
                {
                    sb.AppendLine("Meyve Listesi:");
                    MeyveNode current = meyveHead;
                    while (current != null)
                    {
                        sb.AppendLine("Adı: " + current.meyve.Adi);
                        sb.AppendLine("Cinsi: " + current.meyve.Cinsi);
                        sb.AppendLine("Fiyatı: " + current.meyve.Fiyat);
                        sb.AppendLine("KDV Oranı: " + current.meyve.KDVOrani + "%");
                        sb.AppendLine();
                        current = current.Next;
                    }
                }

                return sb.ToString();
            }

            public string GetYiyecekList()
            {
                StringBuilder sb = new StringBuilder();

                if (yiyeceklerHead == null)
                {
                    sb.AppendLine("Yiyecek listesi boş.");
                }
                else
                {
                    sb.AppendLine("Yiyecek Listesi:");
                    YiyecekNode current = yiyeceklerHead;
                    while (current != null)
                    {
                        sb.AppendLine("Adı: " + current.yiyecek.Adi);
                        sb.AppendLine("Cinsi: " + current.yiyecek.Cinsi);
                        sb.AppendLine("Fiyatı: " + current.yiyecek.Fiyat);
                        sb.AppendLine("KDV Oranı: " + current.yiyecek.KDVOrani + "%");
                        sb.AppendLine();
                        current = current.Next;
                    }
                }

                return sb.ToString();
            }



            public bool YiyecekOncedenVarmi(string adi)
            {
                YiyecekNode current = yiyeceklerHead;
                while (current != null)
                {
                    if (current.yiyecek.Adi == adi)
                    {
                        return true; // Yiyecek zaten listede var
                    }
                    current = current.Next;
                }
                return false; // Yiyecek listede bulunamadı
            }






        }

        class SalataNode
        {
            public Salata salata;
            public SalataNode Next;

            public SalataNode(Salata salata)
            {
                this.salata = salata;
                Next = null;
            }
        }

        class TatliNode
        {
            public Tatli tatli;
            public TatliNode Next;

            public TatliNode(Tatli tatli)
            {
                this.tatli = tatli;
                Next = null;
            }
        }

        class IcecekNode
        {
            public Icecek icecek;
            public IcecekNode Next;

            public IcecekNode(Icecek icecek)
            {
                this.icecek = icecek;
                Next = null;
            }
        }

        class YemekNode
        {
            public Yemek yemek;
            public YemekNode Next;

            public YemekNode(Yemek yemek)
            {
                this.yemek = yemek;
                Next = null;
            }
        }

        class MeyveNode
        {
            public Meyve meyve;
            public MeyveNode Next;

            public MeyveNode(Meyve meyve)
            {
                this.meyve = meyve;
                Next = null;
            }
        }

        class YiyecekNode
        {
            public Yiyecek yiyecek;
            public YiyecekNode Next;

            public YiyecekNode(Yiyecek yiyecek)
            {
                this.yiyecek = yiyecek;
                Next = null;
            }
        }



    
        }
