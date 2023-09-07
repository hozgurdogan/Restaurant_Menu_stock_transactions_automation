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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace YazOdeviDeneme2
{

 


    public partial class MalzemeIslemleri : Form
    {
        Boolean satirSecildiMi = false;
        Boolean yeniVerilerGirildiMi =false;

        public MalzemeIslemleri()
        {
            InitializeComponent();
        }

        private void MalzemeIslemleri_Load(object sender, EventArgs e)
        {
            EkranAyarlari.CenterFormOnScreen(this);
            EkranAyarlari.ArkaPlanResmiSet(this);
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
        }





        private void RaporAll_Click(object sender, EventArgs e)
        {
            ListeyiGetirDepo();
        }

     

        private void KayitSil_Click(object sender, EventArgs e)
        {
            KayıtSilDepo.Visible = true;
            ListeyiGetirDepo();
        }

        private void KayitGuncelle_Click(object sender, EventArgs e)
        {
            button1.Visible = true;
            ListeyiGetirDepo();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            satirSecildiMi = true;

            button2.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            textBox6.Visible = true;

            kayitEkle.Visible = false;
            KayitSil.Visible = false;
            KayitGuncelle.Visible = false;
            RaporAll.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            yeniVerilerGirildiMi = true;


            string[] lines = File.ReadAllLines(@"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\Depo.txt");

            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Find the index of the selected row
                int rowIndex = dataGridView1.SelectedRows[0].Index;

                // Update the line in the text file with the values from the TextBoxes
                lines[rowIndex] = $"Ürün Adı: {textBox1.Text} Üretim Tarihi: {textBox2.Text} Son Kullanma Tarihi: {textBox4.Text} Kalori (gram): {textBox3.Text} Stok Adet: {textBox6.Text} Fiyat: {textBox5.Text}";

                // Write the updated data back to the text file
                File.WriteAllLines(@"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\Depo.txt", lines);
            }


            RaporAll.Visible = true;
            KayitSil.Visible = true;
            KayitGuncelle.Visible=true;
            kayitEkle.Visible=true;
            button1.Visible = false; button2.Visible = false;
            label1.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            label2.Visible= false;
            textBox3.Visible = false;
            label3.Visible= false;
            label4.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            label5.Visible = false;
            label6.Visible = false;

        }

        private void yenile_Click(object sender, EventArgs e)
        {
            ListeyiGetirDepo();
        }
        private void ListeyiGetirDepo()
        {

            // Read the data from the text file
            string[] lines = File.ReadAllLines(@"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\Depo.txt");

            // Create a DataTable to hold the data
            DataTable table = new DataTable();
            table.Columns.Add("Ürün Adı");
            table.Columns.Add("Üretim Tarihi");
            table.Columns.Add("Son Kullanma Tarihi");
            table.Columns.Add("Kalori (gram)");
            table.Columns.Add("Stok Adet");
            table.Columns.Add("Fiyat");

            // Parse the data and add it to the DataTable
            foreach (string line in lines)
            {
                string[] parts = line.Split(new string[] { "Ürün Adı: ", " Üretim Tarihi: ", " Son Kullanma Tarihi: ", " Kalori (gram): ", " Stok Adet: ", " Fiyat: " }, StringSplitOptions.RemoveEmptyEntries);
                table.Rows.Add(parts);
            }

            // Set the DataSource of the DataGridView to the DataTable
            dataGridView1.DataSource = table;
        }

        private void KayıtSilDepo_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Read the data from the text file
                List<string> lines = File.ReadAllLines(@"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\Depo.txt").ToList();

                // Find the index of the selected row
                int rowIndex = dataGridView1.SelectedRows[0].Index;

                // Remove the line from the text file
                lines.RemoveAt(rowIndex);

                // Write the updated data back to the text file
                File.WriteAllLines(@"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\Depo.txt", lines);

                // Remove the row from the DataGridView
                dataGridView1.Rows.RemoveAt(rowIndex);
            }
            KayıtSilDepo.Visible = false;
        }

        private void kayitEkle_Click(object sender, EventArgs e)
        {

            button2.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            textBox4.Visible = true;
            textBox5.Visible = true;
            textBox6.Visible = true;
            DepoyaUrunEkle.Visible = true;
            kayitEkle.Visible = false;
            KayitSil.Visible = false;
            KayitGuncelle.Visible = false;
            RaporAll.Visible = false;
            button2.Visible = false;
        }

        private void DepoyaUrunEkle_Click(object sender, EventArgs e)
        {
            // Read the data from the text file
            List<string> lines = File.ReadAllLines(@"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\Depo.txt").ToList();

            // Add a new line to the text file with the values from the TextBoxes
            lines.Add($"Ürün Adı: {textBox1.Text} Üretim Tarihi: {textBox2.Text} Son Kullanma Tarihi: {textBox4.Text} Kalori (gram): {textBox3.Text} Stok Adet: {textBox6.Text} Fiyat: {textBox5.Text}");

            // Write the updated data back to the text file
            File.WriteAllLines(@"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\Depo.txt", lines);

            // Add a new row to the DataGridView with the values from the TextBoxes
      //      dataGridView1.Rows.Add(textBox1.Text, textBox2.Text, textBox4.Text, textBox3.Text, textBox6.Text, textBox5.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            // Read the data from the text file
            string[] lines = File.ReadAllLines(@"C:\Users\hasan\source\repos\YazOdeviDeneme2\YazOdeviDeneme2\Veriler\Depo.txt");

            // Create a DataTable to hold the data
            DataTable table = new DataTable();
            table.Columns.Add("Ürün Adı");
            table.Columns.Add("Üretim Tarihi");
            table.Columns.Add("Son Kullanma Tarihi");
            table.Columns.Add("Kalori (gram)");
            table.Columns.Add("Stok Adet");
            table.Columns.Add("Fiyat");

            // Parse the data and add it to the DataTable
            foreach (string line in lines)
            {
                string[] parts = line.Split(new string[] { "Ürün Adı: ", " Üretim Tarihi: ", " Son Kullanma Tarihi: ", " Kalori (gram): ", " Stok Adet: ", " Fiyat: " }, StringSplitOptions.RemoveEmptyEntries);
                table.Rows.Add(parts);
            }

            // Set the DataSource of the DataGridView to the DataTable
            dataGridView1.DataSource = table;

            // Create a List<Siparis> to hold the Siparis objects
            List<EksikMalzemeSiparis> siparisListesi = new List<EksikMalzemeSiparis>();

            // Iterate over each row in the DataTable
            foreach (DataRow row in table.Rows)
            {
                // Get the Stok Adet value for this row
                int stokAdet = int.Parse(row["Stok Adet"].ToString());

                // Check if the Stok Adet value is negative
                if (stokAdet < 0)
                {
                    // Get the Ürün Adı value for this row
                    string urunAdi = row["Ürün Adı"].ToString();

                    // Calculate the quantity needed to bring the Stok Adet value back to 0
                    int quantityNeeded = -stokAdet;

                    // Create a new Siparis object with the Ürün Adı and quantityNeeded values
                    EksikMalzemeSiparis siparis = new EksikMalzemeSiparis(urunAdi, quantityNeeded);

                    // Add the Siparis object to the siparisListesi
                    siparisListesi.Add(siparis);
                }
            }

            // Display a MessageBox with the contents of the siparisListesi
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Sipariş Listesi:");
            int i = 1;
            foreach (EksikMalzemeSiparis siparis in siparisListesi)
            {
                sb.AppendLine($"{i}-Ürün adı ({siparis.UrunAdi}), ({siparis.Miktar}) adet sipariş verildi.");
                i++;
            }
            MessageBox.Show(sb.ToString());


        }
    }
}

