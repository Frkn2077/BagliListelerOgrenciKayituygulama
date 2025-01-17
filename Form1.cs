using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Bağlı_liste_uygulama
{
    public partial class Form1 : Form
    {
       private BagliListe bl = new BagliListe();
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String m1 = textBox1.Text;
            String m2 = textBox2.Text;
            
            int k = Convert.ToInt32(textBox3.Text);
            Dugum d1 = new Dugum(k,m1,m2);
            bl.ArayaEkle(d1.Veri,d1.Ad,d1.Soyad);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            String m1 = textBox1.Text;
            String m2 = textBox2.Text;
            int s1=Convert.ToInt32(textBox3.Text);
            Dugum d1 = new Dugum(s1,m1,m2);
            bl.dugumuSil(d1.Veri);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            String m1 = textBox1.Text;
            String m2 = textBox2.Text;
            int k = Convert.ToInt32(textBox3.Text);
            Dugum d1 = new Dugum(k,m1,m2);
            bl.sonaEkle(d1.Veri,d1.Ad,d1.Soyad);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //başa eleman ekleme
                String m1 = textBox1.Text;
                String m2 = textBox2.Text;
                int k = Convert.ToInt32(textBox3.Text);
                Dugum d1 = new Dugum(k,m1,m2);
                bl.BasaEkle(d1.Veri, d1.Ad, d1.Soyad);

           
        }
        // Düğüm sınıfı (Node)
        public class Dugum
        {
            public string Ad { get; set; }
            public string Soyad { get; set; }
            public int Veri{ get; set; } // Düğümde saklanacak veri
            public Dugum Sonraki{ get; set; } // Bir sonraki düğüme referans

            // Yapıcı fonksiyon (Constructor)
            public Dugum(int veri,string ad,string soyad)
            {
                this.Ad = ad;
                this.Soyad = soyad;
                this.Veri = veri;
                this.Sonraki = null; // Başlangıçta bir sonraki düğüm yok
            }
        }

        // Tek yönlü bağlı liste sınıfı
        public class BagliListe
        {
            private Dugum baslangic; // Listenin başını (head) tutar
            
            public BagliListe()
            {
                baslangic = null; // İlk başta liste boş
            }
         
            public void Yazdir(ListBox listBox)
            {
                listBox.Items.Clear(); // Mevcut öğeleri temizle
                Dugum temp = baslangic; // Listeyi baştan dolaşmaya başla
                String m1 = "";
                while (temp != null)
                {
                    m1 += $"No: {temp.Veri}, Ad: {temp.Ad}, Soyad: {temp.Soyad}\n"; // Ad, Soyad ve No'yu yazdır ;

                    temp = temp.Sonraki; // Sonraki düğüme geç
                }
                listBox.Items.Add(m1);// Veriyi ListBox'a ekle
            }

            // Listenin başına düğüm ekleme fonksiyonu
            public void BasaEkle(int veri,string ad,string soyad)
            {
                Dugum yeniDugum = new Dugum(veri,ad,soyad); // Yeni düğüm oluştur
                yeniDugum.Sonraki = baslangic; // Yeni düğümün 'Sonraki' kısmı mevcut başa işaret eder
                baslangic = yeniDugum; // Yeni düğümü baş olarak ayarla
                
            }
            public void ArayaEkle(int data,string ad,string soyad)
            {
                Dugum newNode = new Dugum(data, ad, soyad);

                // Eğer liste boşsa
                if (baslangic == null)
                {
                    baslangic= newNode;
                }
                else
                {
                    Dugum current = baslangic;
                    while (current.Sonraki != null && newNode.Veri > current.Sonraki.Veri)
                    {
                        current = current.Sonraki;
                    }

                    
                        newNode.Sonraki = current.Sonraki;
                        current.Sonraki = newNode;
                    
                }
            }


            // Araya düğüm ekleme fonksiyonu (belirli bir değerden sonra)

            public void sonaEkle(int veri,string ad, string soyad)
            {
                Dugum yeniDugum = new Dugum(veri,ad, soyad); // Yeni düğüm oluştur

                // Eğer liste boşsa, yeni düğüm doğrudan başa eklenir
                if (baslangic == null)
                {
                    baslangic = yeniDugum;
                }
                else
                {
                    // Listenin sonuna gitmek için geçici bir düğüm kullanılır
                    Dugum gecici = baslangic;
                    while (gecici.Sonraki != null)
                    {
                        gecici = gecici.Sonraki;
                    }
                    // Son düğüm bulunduğunda, onun 'sonraki' kısmı yeni düğüme işaret eder
                    gecici.Sonraki = yeniDugum;
                }
                


            }
           


            public void dugumuSil(int silinecekDeger)
            {
                // Liste boşsa hiçbir şey yapma
                if (baslangic == null)
                {
                    return;
                }

                // Silinecek düğüm baştaki düğümse
                if (baslangic.Veri == silinecekDeger)
                {
                    baslangic = baslangic.Sonraki;
                    return;
                }

                // Listede dolanmak için geçici düğümler
                Dugum gecici = baslangic;
                Dugum onceki = null;

                // Silinecek düğüm bulunana kadar döngü
                while (gecici != null && gecici.Veri != silinecekDeger)
                {
                    onceki = gecici;
                    gecici = gecici.Sonraki;
                }

                // Eğer düğüm bulunduysa sil
                if (gecici != null)
                {
                    onceki.Sonraki = gecici.Sonraki;
                }
                else
                {
                    MessageBox.Show("Silinecek değer listede bulunamadı.");
                }
            }

        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            String m1=textBox1.Text;
            String m2=textBox2.Text;
            int s1=Convert.ToInt32(textBox3.Text);
            Dugum d1 = new Dugum(s1,m1,m2);
           
            bl.sonaEkle(d1.Veri,d1.Ad, d1.Soyad);

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
       

        private void button6_Click(object sender, EventArgs e)
        {
            bl.Yazdir(listBox1);

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }
    }
}