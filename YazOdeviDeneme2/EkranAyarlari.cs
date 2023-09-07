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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YazOdeviDeneme2
{
    public static class EkranAyarlari
    {
        public static void CenterFormOnScreen(Form form)
        { // ekranı oratlamak için gerekli olan kod
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            int formWidth = form.Width;
            int formHeight = form.Height;

            int left = (screenWidth - formWidth) / 2;
            int top = (screenHeight - formHeight) / 2;

            form.StartPosition = FormStartPosition.Manual;
            form.Location = new Point(left, top);

        }


        public static void ArkaPlanResmiSet(Form form)
        { //Arka plana resim koymak için
            string imagePath = @"C:\Users\hasan\source\repos\YazProject\YazProject\img\Menu.jpg";
            if (System.IO.File.Exists(imagePath))
            {
                form.BackgroundImage = Image.FromFile(imagePath);
                form.BackgroundImageLayout = ImageLayout.Stretch; // Arka plan resmini pencereye sığdırmak için ImageLayout.Stretch kullanılır
            }
        }


    }
}