using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CafeAdisyon.Models;

namespace CafeAdisyon.ViewModels
{
    public class AdisyonViewModel
    {
        private Masa _secilenMasa;
        public Masa SecilenMasa
        {
            get => _secilenMasa;
            set => _secilenMasa = value;
        }

        public ObservableCollection<Urun> Menu { get; }
        public ObservableCollection<Adisyon> MasaAdisyonlari { get; }
        public ICommand UrunEkleCommand { get; }
        public ICommand UrunCikarCommand { get; }
        public ICommand OdeCommand { get; }
        public ICommand KapaCommand { get; }

        public event EventHandler<(Masa, List<int>)> OdemeYapildi;
        public event EventHandler KapalıViewClosed;

        private decimal _toplamTutar;
        public decimal ToplamTutar
        {
            get => _toplamTutar;
            set => _toplamTutar = value;
        }

        public AdisyonViewModel(Masa masa)
        {
            _secilenMasa = masa;
            Menu = new ObservableCollection<Urun>();
            MasaAdisyonlari = new ObservableCollection<Adisyon>();

            UrunEkleCommand = new RelayCommand(UrunEkle);
            UrunCikarCommand = new RelayCommand(UrunCikar);
            OdeCommand = new RelayCommand(_ => OdemeEkrani());
            KapaCommand = new RelayCommand(_ => KapalıViewClosed?.Invoke(this, EventArgs.Empty));

            YukleMasaVerileri();
        }

        private void YukleMasaVerileri()
        {
            // Menüyü yükle
            Menu.Clear();
            var menu = DatabaseService.GetAllUrunler();
            foreach (var urun in menu)
            {
                Menu.Add(urun);
            }

            // Adisyonları yükle
            YukleMasaAdisyonlari();
        }

        private void YukleMasaAdisyonlari()
        {
            MasaAdisyonlari.Clear();
            var adisyonlar = DatabaseService.GetMasaAdisyonlari(_secilenMasa.MasaId, true);
            foreach (var adisyon in adisyonlar)
            {
                MasaAdisyonlari.Add(adisyon);
            }
            HesaplaToplamTutar();
        }

        private void UrunEkle(object parameter)
        {
            if (parameter is Urun urun)
            {
                // Aynı ürün zaten varsa adetini arttır
                var varolan = MasaAdisyonlari.FirstOrDefault(a => a.UrunId == urun.UrunId);
                if (varolan != null)
                {
                    varolan.Adet++;
                }
                else
                {
                    DatabaseService.AddAdisyon(_secilenMasa.MasaId, urun.UrunId, 1);
                    YukleMasaAdisyonlari();
                }
                HesaplaToplamTutar();
            }
        }

        private void UrunCikar(object parameter)
        {
            if (parameter is Adisyon adisyon)
            {
                DatabaseService.DeleteAdisyon(adisyon.AdisyonId);
                YukleMasaAdisyonlari();
                HesaplaToplamTutar();
            }
        }

        private void OdemeEkrani()
        {
            // Ödeme penceresini aç - MainWindow tarafından handle edilecek
        }

        private void HesaplaToplamTutar()
        {
            ToplamTutar = MasaAdisyonlari.Sum(a => a.Toplam);
        }

        public void OdemeyiTamamla(List<int> secilenAdisyonIds)
        {
            DatabaseService.PayAdisyonlar(_secilenMasa.MasaId, secilenAdisyonIds);
            YukleMasaAdisyonlari();
            OdemeYapildi?.Invoke(this, (_secilenMasa, secilenAdisyonIds));
        }
    }
}
