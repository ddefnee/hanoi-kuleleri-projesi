namespace HanoiKuleleri
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cmbDiskSayisi = new System.Windows.Forms.ComboBox();
            this.btnYenidenBaslat = new System.Windows.Forms.Button();
            this.lblHamleSayisi = new System.Windows.Forms.Label();
            this.pnlOyunAlani = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // cmbDiskSayisi
            // 
            this.cmbDiskSayisi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbDiskSayisi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDiskSayisi.FormattingEnabled = true;
            this.cmbDiskSayisi.Location = new System.Drawing.Point(142, 434);
            this.cmbDiskSayisi.Name = "cmbDiskSayisi";
            this.cmbDiskSayisi.Size = new System.Drawing.Size(121, 26);
            this.cmbDiskSayisi.TabIndex = 0;
            // 
            // btnYenidenBaslat
            // 
            this.btnYenidenBaslat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnYenidenBaslat.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnYenidenBaslat.Location = new System.Drawing.Point(337, 433);
            this.btnYenidenBaslat.Name = "btnYenidenBaslat";
            this.btnYenidenBaslat.Size = new System.Drawing.Size(130, 26);
            this.btnYenidenBaslat.TabIndex = 1;
            this.btnYenidenBaslat.Text = "RESET";
            this.btnYenidenBaslat.UseVisualStyleBackColor = true;
            this.btnYenidenBaslat.Click += new System.EventHandler(this.btnYenidenBaslat_Click_1);
            // 
            // lblHamleSayisi
            // 
            this.lblHamleSayisi.AutoSize = true;
            this.lblHamleSayisi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblHamleSayisi.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblHamleSayisi.Location = new System.Drawing.Point(535, 437);
            this.lblHamleSayisi.Name = "lblHamleSayisi";
            this.lblHamleSayisi.Size = new System.Drawing.Size(113, 18);
            this.lblHamleSayisi.TabIndex = 2;
            this.lblHamleSayisi.Text = "Hamle Sayısı: 0";
            // 
            // pnlOyunAlani
            // 
            this.pnlOyunAlani.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlOyunAlani.BackgroundImage")));
            this.pnlOyunAlani.Location = new System.Drawing.Point(99, 12);
            this.pnlOyunAlani.Name = "pnlOyunAlani";
            this.pnlOyunAlani.Size = new System.Drawing.Size(600, 400);
            this.pnlOyunAlani.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 505);
            this.Controls.Add(this.pnlOyunAlani);
            this.Controls.Add(this.lblHamleSayisi);
            this.Controls.Add(this.btnYenidenBaslat);
            this.Controls.Add(this.cmbDiskSayisi);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbDiskSayisi;
        private System.Windows.Forms.Button btnYenidenBaslat;
        private System.Windows.Forms.Label lblHamleSayisi;
        private System.Windows.Forms.Panel pnlOyunAlani;
    }
}

