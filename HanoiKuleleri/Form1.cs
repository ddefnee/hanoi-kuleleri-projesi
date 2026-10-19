using System;                                      //
using System.Collections.Generic;                  //
using System.Drawing;                              // Gerekli kütüphaneler
using System.Linq;                                 //
using System.Windows.Forms;                        //

namespace HanoiKuleleri
{
    public partial class Form1 : Form
    {
        // Disk panelleri listesi
        private List<Panel> diskler = new List<Panel>();

        // Her kule için konum (X,Y) bilgisi
        private List<Point> kuleKonumlari = new List<Point>();

        // Her kuledeki diskleri tutan 3 ayrı liste (kuleler[0], kuleler[1], kuleler[2])
        private List<List<Panel>> kuleler = new List<List<Panel>>();

        // Kule çubuklarının grafiksel temsilleri
        private List<Rectangle> kuleCubuklari = new List<Rectangle>();

        // Oyun alanındaki taban çizgisi
        private Rectangle taban;

        // Toplam yapılan hamle sayısı
        private int hamleSayisi = 0;

        // Taşınmakta olan disk
        private Panel tasinanDisk = null;

        // Taşınan diskin bulunduğu kule indeksi
        private int tasinanDiskKuleIndex = -1;

        // Pastel renk dizisi: Her diske ayrı renk verilir
        private Color[] diskRenkleri = new Color[]
        {
            Color.FromArgb(255, 179, 206, 234),  // Pastel mavi
            Color.FromArgb(255, 255, 209, 220),  // Pastel pembe
            Color.FromArgb(255, 207, 236, 207),  // Pastel yeşil
            Color.FromArgb(255, 255, 255, 186),  // Pastel sarı
            Color.FromArgb(255, 221, 214, 255),  // Pastel mor
            Color.FromArgb(255, 255, 223, 211),  // Pastel turuncu
            Color.FromArgb(255, 204, 255, 229),  // Pastel nane
            Color.FromArgb(255, 255, 204, 204)   // Pastel kırmızı
        };

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true; // Grafiksel çizimi daha düzgün hale getirir
        }

        // Form yüklendiğinde çalışan kod
        private void Form1_Load(object sender, EventArgs e)
        {
            // Disk sayısı seçim kutusuna 3–8 arası sayılar ekleniyor
            for (int i = 3; i <= 8; i++)
            {
                cmbDiskSayisi.Items.Add(i);
            }
            cmbDiskSayisi.SelectedIndex = 0;

            // Kulelerin konumları hesaplanıyor
            int panelGenislik = pnlOyunAlani.Width;
            int panelYukseklik = pnlOyunAlani.Height;
            int kuleArasiMesafe = panelGenislik / 4;

            kuleKonumlari.Add(new Point(kuleArasiMesafe, panelYukseklik - 50));   // 1. kulenin konumu (sol tarafta), tabanın biraz üstüne yerleştiriliyor
            kuleKonumlari.Add(new Point(kuleArasiMesafe * 2, panelYukseklik - 50)); // 2. kulenin konumu (orta), aynı yatay mesafede
            kuleKonumlari.Add(new Point(kuleArasiMesafe * 3, panelYukseklik - 50)); // 3. kulenin konumu (sağda), eşit aralıklarla hizalanmış

            // Her kule için boş liste oluşturuluyor
            for (int i = 0; i < 3; i++)
            {
                kuleler.Add(new List<Panel>());
            }

            // Oyun alanı çizimi yapılacak olan olay tetikleniyor
            pnlOyunAlani.Paint += pnlOyunAlani_Paint;

            // Oyun başlatılıyor
            OyunuBaslat();
        }

        // Oyunu başlatan ya da yeniden başlatan fonksiyon
        private void OyunuBaslat()
        {
            // Önceki çizimleri temizle
            pnlOyunAlani.Controls.Clear();
            diskler.Clear();
            kuleler.Clear();
            kuleCubuklari.Clear();

            // Kule çubuklarının grafiksel olarak oluşturulması
            int cubukGenislik = 10;
            int cubukYukseklik = pnlOyunAlani.Height - 70;

            for (int i = 0; i < 3; i++) // 3 kule için döngü başlatılır
            {
                kuleCubuklari.Add(new Rectangle( // Her kule için bir dikdörtgen (çubuk) oluşturulur ve listeye eklenir
                    kuleKonumlari[i].X - cubukGenislik / 2, // Çubuğun X konumu: kulenin merkezinden çubuğun yarı genişliği kadar sola kaydırılır
                    kuleKonumlari[i].Y - cubukYukseklik + 20, // Çubuğun Y konumu: tabandan yukarıya doğru olacak şekilde ayarlanır
                    cubukGenislik,                                    
                    cubukYukseklik                                    
                ));
            }

            // Alt taban çizimi
            taban = new Rectangle(
                10,                         // X konumu (panelin solundan 10 piksel içeriden başlar)
                kuleKonumlari[0].Y,         // Y konumu (kulelerin alt hizasıyla aynı dikey konuma yerleştirilir)
                pnlOyunAlani.Width - 20,    // Genişlik (sağdan ve soldan 10'ar piksel boşluk)
                20                          // Yükseklik (kalınlığı 20 piksel) 
            );

            // Seçilen disk sayısına göre diskler oluşturuluyor
            int diskSayisi = (int)cmbDiskSayisi.SelectedItem;
            for (int i = 0; i < diskSayisi; i++)
            {
                Panel disk = new Panel();
                disk.Width = 100 - i * 10; // Her disk bir öncekinden daha küçük
                disk.Height = 20;
                disk.BackColor = diskRenkleri[i];
                disk.BorderStyle = BorderStyle.FixedSingle;
                disk.Tag = diskSayisi - i - 1; // Her diske büyüklük bilgisi atanır

                // Konum ayarları
                disk.Left = kuleKonumlari[0].X - disk.Width / 2; // Diski 1. kulenin ortasına hizalar (diskin sol kenarını ayarlarken merkezden yarı genişliği kadar sola kaydırır)
                disk.Top = taban.Top - disk.Height * (i + 1); // Diskin yüksekliğini ayarlayarak üst üste dizilmelerini sağlar

                // Mouse olayları bağlanıyor
                disk.MouseDown += Disk_MouseDown;
                disk.MouseMove += Disk_MouseMove;
                disk.MouseUp += Disk_MouseUp;

                pnlOyunAlani.Controls.Add(disk); // Oluşturulan diski oyun alanı paneline ekler
                diskler.Add(disk); // Diski 'diskler' adlı listeye ekler

            }

            // Kuleleri yeniden oluştur ve tüm diskleri 1. kuleye yerleştir
            for (int i = 0; i < 3; i++) kuleler.Add(new List<Panel>());
            foreach (var disk in diskler) kuleler[0].Add(disk);

            // Yeniden çizim
            pnlOyunAlani.Invalidate();
        }

        // Oyun alanı çizimi (Paint olayı)
        private void pnlOyunAlani_Paint(object sender, PaintEventArgs e) // Oyun paneli her yeniden çizildiğinde çalışacak olay (Paint Event)
        {
            using (SolidBrush kuleBrush = new SolidBrush(Color.FromArgb(230, 220, 255))) // Kule çubuklarının iç rengini belirleyen fırça
            using (Pen kuleKenarlik = new Pen(Color.FromArgb(180, 180, 180)))           // Kule çubuklarının kenar çizgilerini belirleyen kalem (gri ton)
            {
                foreach (var cubuk in kuleCubuklari) // Her bir kule çubuğu için
                {
                    e.Graphics.FillRectangle(kuleBrush, cubuk);   // Kule çubuğu dikdörtgeni pastel lavanta rengiyle doldurulur
                    e.Graphics.DrawRectangle(kuleKenarlik, cubuk); // Kule çubuğunun etrafı gri kenarlıkla çizilir
                }
            }

            using (SolidBrush tabanBrush = new SolidBrush(Color.FromArgb(240, 235, 220))) // Alt tabanın iç rengini belirleyen fırça
            using (Pen tabanKenarlik = new Pen(Color.FromArgb(180, 180, 180)))            // Alt tabanın kenar çizgilerini belirleyen kalem (gri ton)
            {
                e.Graphics.FillRectangle(tabanBrush, taban);    // Alt taban pastel bej rengiyle doldurulur
                e.Graphics.DrawRectangle(tabanKenarlik, taban); // Alt tabanın kenarlığı gri ile çizilir
            }
        }

        // Disk tıklanınca rengi koyulaşır ve taşınmaya hazır hale gelir
        private void Disk_MouseDown(object sender, MouseEventArgs e)
        {
            Panel disk = (Panel)sender; // Tıklanan nesneyi Panel tipinde disk olarak al

            // Yalnızca en üstteki disk seçilebilir, bunun için kulelerde kontrol yap
            int kuleIndex = -1; // Başlangıçta disk herhangi bir kulede değil olarak ayarla
            for (int i = 0; i < 3; i++)
            {
                // Eğer kule[i] içindeki disk, tıklanan disk ise ve aynı zamanda o kulenin en üstündeki diskse
                if (kuleler[i].Contains(disk) && kuleler[i].Last() == disk)
                {
                    kuleIndex = i; // O kulenin indeksini kaydet
                    break;
                }
            }

            if (kuleIndex == -1) return; // Eğer disk kulelerin en üstünde değilse işlem yapma, fonksiyondan çık

            tasinanDisk = disk; // Taşınacak disk olarak tıklanan diski kaydet
            tasinanDiskKuleIndex = kuleIndex; // Diskin hangi kulenin üzerinde olduğunu kaydet

            // Disk rengini koyulaştırmak için mevcut rengin RGB değerlerinden 40 çıkar, en az 0 olmalı
            Color currentColor = disk.BackColor;
            disk.BackColor = Color.FromArgb(
                255, // (opaklık) tam, yani tamamen görünür
                Math.Max(currentColor.R - 40, 0), // Kırmızı değeri koyulaşacak (en az 0)
                Math.Max(currentColor.G - 40, 0), // Yeşil değeri koyulaşacak (en az 0)
                Math.Max(currentColor.B - 40, 0)  // Mavi değeri koyulaşacak (en az 0)
            );

            disk.BringToFront(); // Diskin diğer nesnelerin önünde gözükmesi için en öne getir
        }

        // Disk taşınırken konumunu güncelle
        private void Disk_MouseMove(object sender, MouseEventArgs e)
        {
            // Eğer bir disk taşınıyorsa ve fare sol tuşu basılıysa
            if (tasinanDisk != null && e.Button == MouseButtons.Left)
            {
                // Diskin yatay konumunu güncelle:
                // Fare imlecinin X koordinatından disk genişliğinin yarısını çıkararak disk ortalanır
                tasinanDisk.Left += e.X - (tasinanDisk.Width / 2);

                // Diskin dikey konumunu güncelle:
                // Fare imlecinin Y koordinatından disk yüksekliğinin yarısını çıkararak disk ortalanır
                tasinanDisk.Top += e.Y - (tasinanDisk.Height / 2);
            }
        }

        // Disk bırakıldığında konum ve kural kontrolü
        private void Disk_MouseUp(object sender, MouseEventArgs e)
        {
            if (tasinanDisk == null) return;

            tasinanDisk.BackColor = diskRenkleri[(int)cmbDiskSayisi.SelectedItem - (int)tasinanDisk.Tag - 1]; // Disk rengini orijinal haline geri döndür
           
            int hedefKuleIndex = -1; // Diskin bırakıldığı hedef kuleyi tespit etmek için indeks değişkeni

            // Tüm kulelerin X konumlarına bakarak disk hangi kuleye bırakıldı kontrol et
            for (int i = 0; i < 3; i++)
            {
                // Diskin yatay merkez noktası ile kule konumu arasındaki mesafe 50 pikselden azsa
                if (Math.Abs(tasinanDisk.Left + tasinanDisk.Width / 2 - kuleKonumlari[i].X) < 50)
                {
                    hedefKuleIndex = i;  // Hedef kule bulundu
                    break;
                }
            }

            // Eğer geçersiz bir kuleye bırakıldıysa veya bırakılan kule taşındığı kuleyle aynıysa
            if (hedefKuleIndex == -1 || hedefKuleIndex == tasinanDiskKuleIndex)
            {
                DiskleriYerlestir(); // Diskleri eski konumlarına geri yerleştir
            }
            else
            {
                // Kural kontrolü: Hedef kule boşsa veya hedef kulenin en üstündeki diskin boyutu taşınan diskten büyükse
                if (kuleler[hedefKuleIndex].Count == 0 ||
                    (int)kuleler[hedefKuleIndex].Last().Tag > (int)tasinanDisk.Tag)
                {
                    // Disk eski kuleden kaldırılır
                    kuleler[tasinanDiskKuleIndex].Remove(tasinanDisk);

                    // Disk hedef kuleye eklenir
                    kuleler[hedefKuleIndex].Add(tasinanDisk);

                    // Hamle sayısı artırılır
                    hamleSayisi++;

                    // Hamle sayısı etiketine güncel değer yazılır
                    lblHamleSayisi.Text = "Hamle Sayısı: " + hamleSayisi;

                    // Oyun bitiş kontrolü: Tüm diskler 3. kuleye taşındıysa
                    if (kuleler[2].Count == (int)cmbDiskSayisi.SelectedItem)
                    {
                        // Tebrik mesajı gösterilir
                        MessageBox.Show("Tebrikler! Oyunu " + hamleSayisi + " hamlede tamamladınız!", "Başarılı");
                    }
                }
                else
                {
                    MessageBox.Show("Geçersiz hamle! Büyük disk küçük diskin üzerine gelemez.",
                                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                // Disklerin konumu tekrar güncellenir
                DiskleriYerlestir();
            }

            // Taşınan disk işlemi sona erer, değişkenler sıfırlanır
            tasinanDisk = null;
            tasinanDiskKuleIndex = -1;
        }
        // Diskleri kulelere göre hizalama (görsel düzenleme)
        private void DiskleriYerlestir()
        {
            for (int i = 0; i < 3; i++)
            {
                // Her kuledeki disk sayısı kadar döngü
                for (int j = 0; j < kuleler[i].Count; j++)
                {
                    // İlgili diski panel olarak al
                    Panel disk = kuleler[i][j];

                    // Diski yatayda hizala:
                    // Kule konumunun X koordinatından diskin genişliğinin yarısını çıkararak ortala
                    disk.Left = kuleKonumlari[i].X - disk.Width / 2;

                    // Diski dikeyde hizala:
                    // Tabanın üstünden başlayarak, disk yüksekliği * pozisyon (alt diskler daha altta)
                    disk.Top = taban.Top - disk.Height * (j + 1);
                }
            }
            // Oyun alanını yeniden çiz
            pnlOyunAlani.Invalidate();
        }

        // Yeniden başlat butonuna basıldığında sıfırla
        private void btnYenidenBaslat_Click_1(object sender, EventArgs e)
        {
            hamleSayisi = 0;
            lblHamleSayisi.Text = "Hamle Sayısı: 0";
            OyunuBaslat();
        }
    }
}
