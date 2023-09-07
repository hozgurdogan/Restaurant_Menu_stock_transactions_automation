using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

public class Urun
{
    public string UrunAdi { get; set; }
    public DateTime UretimTarihi { get; set; }
    public DateTime SonKullanmaTarihi { get; set; }
    public float KaloriGram { get; set; }
    public float StokAdet { get; set; }
    public float Fiyat { get; set; }

    public Urun(string urunAdi, DateTime uretimTarihi, DateTime sonKullanmaTarihi, float kaloriGram, float stokAdet, float fiyat)
    {
        UrunAdi = urunAdi;
        UretimTarihi = uretimTarihi;
        SonKullanmaTarihi = sonKullanmaTarihi;
        KaloriGram = kaloriGram;
        StokAdet = stokAdet;
        Fiyat = fiyat;
    }

    public override string ToString()
    {
        return $"Ürün Adı: {UrunAdi}\nÜretim Tarihi: {UretimTarihi}\nSon Kullanma Tarihi: {SonKullanmaTarihi}\nKalori (gram): {KaloriGram}\nStok Adet: {StokAdet}\nFiyat: {Fiyat} TL";
    }
}

class UrunNode
{
    public Urun urun;
    public UrunNode Next;

    public UrunNode(Urun urun)
    {
        this.urun = urun;
        Next = null;
    }
}

class BagliListe
{
    private UrunNode head;


    private bool MalzemeListedeVarMi(string malzemeAdi)   //eğer malzeme listede var ise false var ise true dödüürür
    {
        // Depo.txt dosyasındaki malzeme listesini okuyarak, malzemenin listede olup olmadığını kontrol ediyoruz.
        string dosyaYolu = @"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\Depo.txt";
        if (!File.Exists(dosyaYolu))
        {
            // Dosya bulunamadıysa, malzeme listede yoktur.
            return false;
        }

        string[] satirlar = File.ReadAllLines(dosyaYolu);
        foreach (string satir in satirlar)
        {
            if (satir.Contains("Adı:") && satir.Contains(malzemeAdi))
            {
                // Malzeme listede bulundu.
                return true;
            }
        }

        // Malzeme listede bulunamadı.
        return false;
    }

    public void Ekle(string urunAdi, DateTime uretimTarihi, DateTime sonKullanmaTarihi, int kaloriGram, int stokAdet, int fiyat)
    {

        
        Urun yeniUrun = new Urun(urunAdi, uretimTarihi, sonKullanmaTarihi, kaloriGram, stokAdet, fiyat);
        bool varMi=    MalzemeListedeVarMi(urunAdi);

        if(varMi==false)
        {
            UrunNode yeniUrunNode = new UrunNode(yeniUrun);

            if (head == null)
            {
                head = yeniUrunNode;
            }
            else
            {
                UrunNode current = head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = yeniUrunNode;
            }
            string dosyaYolu = @"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\Depo.txt";

            // Dosyayı oluştur veya varsa içine ekle ve ürün bilgilerini yaz
            using (StreamWriter writer = new StreamWriter(dosyaYolu, true, Encoding.UTF8))
            {
                writer.Write($"Ürün Adı: {urunAdi} ");
                writer.Write($"Üretim Tarihi: {uretimTarihi:dd.MM.yyyy} ");
                writer.Write($"Son Kullanma Tarihi: {sonKullanmaTarihi:dd.MM.yyyy} ");
                writer.Write($"Kalori (gram): {kaloriGram} ");
                writer.Write($"Stok Adet: {stokAdet} ");
                writer.Write($"Fiyat: {fiyat} TL");
                writer.WriteLine();
            }

            string message = $"Ürün Adı: {urunAdi}\nÜretim Tarihi: {uretimTarihi}\nSon Kullanma Tarihi: {sonKullanmaTarihi}\nKalori (gram): {kaloriGram}\nStok Adet: {stokAdet}\nFiyat: {fiyat} TL";
            MessageBox.Show(message, "Ürün Eklendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
            MessageBox.Show("Eklemek istediğiniz ürünün ismi depoda mevcut dilerseniz stok artırabilir veya düşürüebilrisiniz");
        }
    }

    public override string ToString()
    {
        if (head == null)
        {
            return "Liste boş.";
        }
        else
        {
            UrunNode current = head;
            string liste = "";

            while (current != null)
            {
                liste += current.urun.ToString() + "\n\n";
                current = current.Next;
            }

            return liste;
        }
    }
}
