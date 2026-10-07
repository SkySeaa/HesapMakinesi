namespace WinFormsApp5
{
    public partial class Form1 : Form
    {
        // Hesap makinesi bileşenleri ve değişkenleri
        private TextBox txtEkran;
        private double _ilkSayi = 0;
        private string _islem = "";
        private bool _yeniGiris = true;

        public Form1()
        {
            InitializeComponent();
            FormArayuzunuOlustur();
        }

        // Tasarımı kodla otomatik oluşturan kısım
        private void FormArayuzunuOlustur()
        {
            // Form Ayarları
            this.Text = "Pratik Hesap Makinesi";
            this.Width = 310;
            this.Height = 420;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Ekran (TextBox)
            txtEkran = new TextBox();
            txtEkran.Left = 15;
            txtEkran.Top = 15;
            txtEkran.Width = 265;
            txtEkran.Height = 40;
            txtEkran.Font = new Font("Arial", 18, FontStyle.Bold);
            txtEkran.TextAlign = HorizontalAlignment.Right;
            txtEkran.ReadOnly = true;
            txtEkran.Text = "0";
            this.Controls.Add(txtEkran);

            // Buton Bilgileri (Text, X konumu, Y konumu, Genişlik, Yükseklik, Renk)
            string[,] butonlar = {
                { "C", "15", "75", "60", "50" }, { "/", "85", "75", "60", "50" }, { "*", "155", "75", "60", "50" }, { "-", "220", "75", "60", "50" },
                { "7", "15", "135", "60", "50" }, { "8", "85", "135", "60", "50" }, { "9", "155", "135", "60", "50" }, { "+", "220", "135", "60", "110" },
                { "4", "15", "195", "60", "50" }, { "5", "85", "195", "60", "50" }, { "6", "155", "195", "60", "50" },
                { "1", "15", "255", "60", "50" }, { "2", "85", "255", "60", "50" }, { "3", "155", "255", "60", "50" }, { "=", "220", "255", "60", "110" },
                { "0", "15", "315", "130", "50" }, { ",", "155", "315", "60", "50" }
            };

            // Butonları dinamik olarak oluşturup ekleme
            for (int i = 0; i < butonlar.GetLength(0); i++)
            {
                Button btn = new Button();
                btn.Text = butonlar[i, 0];
                btn.Left = int.Parse(butonlar[i, 1]);
                btn.Top = int.Parse(butonlar[i, 2]);
                btn.Width = int.Parse(butonlar[i, 3]);
                btn.Height = int.Parse(butonlar[i, 4]);
                btn.Font = new Font("Arial", 12, FontStyle.Bold);
                btn.BackColor = Color.WhiteSmoke;
                btn.Click += Btn_Click;
                this.Controls.Add(btn);
            }
        }

        // Tüm butonların ortak tıklama olay yönetimi
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string deger = btn.Text;

            // Rakamlar (0-9) ve Virgül
            if (char.IsDigit(deger[0]) || deger == ",")
            {
                if (deger == ",")
                {
                    if (!txtEkran.Text.Contains(","))
                        txtEkran.Text += ",";
                    return;
                }

                if (txtEkran.Text == "0" || _yeniGiris)
                {
                    txtEkran.Text = deger;
                    _yeniGiris = false;
                }
                else
                {
                    txtEkran.Text += deger;
                }
            }
            // Temizleme Tuşu (C)
            else if (deger == "C")
            {
                txtEkran.Text = "0";
                _ilkSayi = 0;
                _islem = "";
                _yeniGiris = true;
            }
            // İşlem Tuşları (+, -, *, /)
            else if (deger == "+" || deger == "-" || deger == "*" || deger == "/")
            {
                try
                {
                    _ilkSayi = Convert.ToDouble(txtEkran.Text);
                    _islem = deger;
                    _yeniGiris = true;
                }
                catch
                {
                    txtEkran.Text = "Hata";
                }
            }
            // Eşittir Tuşu (=)
            else if (deger == "=")
            {
                try
                {
                    double ikinciSayi = Convert.ToDouble(txtEkran.Text);
                    double sonuc = 0;

                    switch (_islem)
                    {
                        case "+": sonuc = _ilkSayi + ikinciSayi; break;
                        case "-": sonuc = _ilkSayi - ikinciSayi; break;
                        case "*": sonuc = _ilkSayi * ikinciSayi; break;
                        case "/":
                            if (ikinciSayi != 0)
                                sonuc = _ilkSayi / ikinciSayi;
                            else
                            {
                                MessageBox.Show("Bir sayı sıfıra bölünemez!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtEkran.Text = "0";
                                _yeniGiris = true;
                                return;
                            }
                            break;
                    }

                    txtEkran.Text = sonuc.ToString();
                    _ilkSayi = sonuc;
                    _yeniGiris = true;
                }
                catch
                {
                    txtEkran.Text = "Hata";
                }
            }
        }
    }
}
