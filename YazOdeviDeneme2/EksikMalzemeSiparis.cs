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
**               DERSİN ALINDIĞI GRUP...:1. Öğretim A
****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YazOdeviDeneme2
{
    public class EksikMalzemeSiparis
    {
        //- değerlede olan verileri tespit etmek için için
        public string UrunAdi { get; set; }
        public int Miktar { get; set; }

        public EksikMalzemeSiparis(string urunAdi, int miktar)
        {
            UrunAdi = urunAdi;
            Miktar = miktar;
        }
    }
}
