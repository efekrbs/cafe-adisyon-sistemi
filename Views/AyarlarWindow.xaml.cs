using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CafeAdisyon.Models;

namespace CafeAdisyon.Views
{
    public partial class AyarlarWindow : Window
    {
        public AyarlarWindow()
        {
            InitializeComponent();
            YukleBilgiler();
        }

        private void YukleBilgiler()
        {
            // Ürünleri yükle
            var urunler = DatabaseService.GetAllUrunler();
            UrunlerListesi.ItemsSource = urunler;

            // Masaları yükle
            var masalar = DatabaseService.GetAllMasalar();
            MasalarListesi.ItemsSource = masalar;

            // Varsayılan kategori seç
            KategoriCombo.SelectedIndex = 0;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            MenuTabi.Visibility = Visibility.Visible;
            IstatistikTabi.Visibility = Visibility.Hidden;
            MasalarTabi.Visibility = Visibility.Hidden;

            MenuButton.Background = (System.Windows.Media.Brush)FindResource("AccentBrush");
            IstatistikButton.Background = (System.Windows.Media.Brush)FindResource("TextBrush");
            MasalarButton.Background = (System.Windows.Media.Brush)FindResource("TextBrush");
        }

        private void IstatistikButton_Click(object sender, RoutedEventArgs e)
        {
            MenuTabi.Visibility = Visibility.Hidden;
            IstatistikTabi.Visibility = Visibility.Visible;
            MasalarTabi.Visibility = Visibility.Hidden;

            MenuButton.Background = (System.Windows.Media.Brush)FindResource("TextBrush");
            IstatistikButton.Background = (System.Windows.Media.Brush)FindResource("AccentBrush");
            MasalarButton.Background = (System.Windows.Media.Brush)FindResource("TextBrush");

            YukleiStatistikler();
        }

        private void MasalarButton_Click(object sender, RoutedEventArgs e)
        {
            MenuTabi.Visibility = Visibility.Hidden;
            IstatistikTabi.Visibility = Visibility.Hidden;
            MasalarTabi.Visibility = Visibility.Visible;

            MenuButton.Background = (System.Windows.Media.Brush)FindResource("TextBrush");
            IstatistikButton.Background = (System.Windows.Media.Brush)FindResource("TextBrush");
            MasalarButton.Background = (System.Windows.Media.Brush)FindResource("AccentBrush");

            YukleMasalar();
        }

        private void EkleUrun_Click(object sender, RoutedEventArgs e)
        {
            string urunAdi = UrunAdiTxt.Text.Trim();
            string kategori = (KategoriCombo.SelectedItem as ComboBoxItem)?.Content.ToString();
            string fiyatStr = FiyatTxt.Text.Trim();

            if (string.IsNullOrEmpty(urunAdi) || string.IsNullOrEmpty(kategori) || string.IsNullOrEmpty(fiyatStr))
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!decimal.TryParse(fiyatStr, out decimal fiyat) || fiyat <= 0)
            {
                MessageBox.Show("Lütfen geçerli bir fiyat giriniz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (_duzenleniyorUrunId == -1)
                {
                    // Yeni ürün ekle
                    DatabaseService.AddUrun(urunAdi, kategori, fiyat);
                    MessageBox.Show("Ürün başarıyla eklendi!", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Var olan ürünü güncelle
                    DatabaseService.UpdateUrun(_duzenleniyorUrunId, urunAdi, kategori, fiyat);
                    MessageBox.Show("Ürün başarıyla güncellendi!", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
                    _duzenleniyorUrunId = -1;

                    var btnEkle = FindName("EkleButton") as Button;
                    if (FindName("EkleButton") is Button ekleBtn)
                    {
                        ekleBtn.Content = "ÜRÜN EKLE";
                    }
                }

                UrunAdiTxt.Clear();
                FiyatTxt.Clear();
                KategoriCombo.SelectedIndex = 0;

                var urunler = DatabaseService.GetAllUrunler();
                UrunlerListesi.ItemsSource = urunler;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int _duzenleniyorUrunId = -1;

        private void DuzenleUrun_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag is int urunId)
            {
                var urunler = DatabaseService.GetAllUrunler();
                var urun = urunler.FirstOrDefault(u => u.UrunId == urunId);

                if (urun != null)
                {
                    _duzenleniyorUrunId = urunId;
                    UrunAdiTxt.Text = urun.UrunAdi;
                    KategoriCombo.SelectedItem = KategoriCombo.Items.Cast<ComboBoxItem>()
                        .FirstOrDefault(cb => cb.Content.ToString() == urun.Kategori);
                    FiyatTxt.Text = urun.Fiyat.ToString("F2");

                    // Formu ÜRÜN GÜNCELLE moduna geçir
                    var btnEkle = FindName("EkleButton") as Button;
                    if (FindName("EkleButton") is Button ekleBtn)
                    {
                        ekleBtn.Content = "ÜRÜN GÜNCELLE";
                    }
                }
            }
        }

        private void SilUrun_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag is int urunId)
            {
                var result = MessageBox.Show("Bu ürünü silmek istediğinize emin misiniz?",
                    "Onay", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        DatabaseService.DeleteUrun(urunId);
                        var urunler = DatabaseService.GetAllUrunler();
                        UrunlerListesi.ItemsSource = urunler;
                        MessageBox.Show("Ürün başarıyla silindi!", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void YukleiStatistikler()
        {
            decimal gunlukCiro = DatabaseService.GetGunlukCiro();
            GunlukCiroText.Text = $"₺{gunlukCiro:F2}";

            // Aylık ve toplam ciro (basitçe günlük çarpı 30 ve 365 olarak göster)
            AylikCiroText.Text = $"₺{gunlukCiro * 30:F2}";
            ToplamCiroText.Text = $"₺{gunlukCiro * 365:F2}";

            var rapor = DatabaseService.GetUrunSatisRaporu();
            RaporuListesi.ItemsSource = rapor;
        }

        private void YukleMasalar()
        {
            var masalar = DatabaseService.GetAllMasalar();
            foreach (var masa in masalar)
            {
                masa.ToplamTutar = DatabaseService.GetMasaToplam(masa.MasaId);
            }
            MasalarListesi.ItemsSource = masalar;
        }

        private void SifirlaIstatistikler_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Tüm satış geçmişini sıfırlamak istediğinize emin misiniz?\n\nBu işlem geri alınamaz!",
                "İstatistikleri Sıfırla", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    DatabaseService.SifirlaIstatistikler();
                    YukleiStatistikler();
                    MessageBox.Show("İstatistikler başarıyla sıfırlandı!", "Başarılı",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void MasaEkle_Click(object sender, RoutedEventArgs e)
        {
            string masaAdi = YeniMasaTxt.Text.Trim();

            if (string.IsNullOrEmpty(masaAdi))
            {
                MessageBox.Show("Lütfen masa adını giriniz!", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                DatabaseService.AddMasa(masaAdi);
                MessageBox.Show("Masa başarıyla eklendi!", "Başarılı", MessageBoxButton.OK, MessageBoxImage.Information);
                YeniMasaTxt.Clear();
                YukleMasalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
