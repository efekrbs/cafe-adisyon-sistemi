using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CafeAdisyon.Models;

namespace CafeAdisyon.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Masa> Masalar { get; }
        public ICommand MasaTiklaCommand { get; }
        public ICommand AyarlarAcCommand { get; }
        public ICommand YenilemeCommand { get; }

        public event EventHandler<Masa> MasaTiklandi;
        public event EventHandler AyarlarAcildı;

        public MainViewModel()
        {
            Masalar = new ObservableCollection<Masa>();
            MasaTiklaCommand = new RelayCommand(TabloyuAc);
            AyarlarAcCommand = new RelayCommand(_ => AyarlarAcildı?.Invoke(this, EventArgs.Empty));
            YenilemeCommand = new RelayCommand(_ => YukleMasalar());

            YukleMasalar();
        }

        private void YukleMasalar()
        {
            Masalar.Clear();
            var masalar = DatabaseService.GetAllMasalar();
            foreach (var masa in masalar)
            {
                masa.ToplamTutar = DatabaseService.GetMasaToplam(masa.MasaId);
                Masalar.Add(masa);
            }
        }

        private void TabloyuAc(object parameter)
        {
            if (parameter is Masa masa)
            {
                MasaTiklandi?.Invoke(this, masa);
            }
        }

        public void YeniMasaEkle(string masaAdi)
        {
            DatabaseService.AddMasa(masaAdi);
            YukleMasalar();
        }
    }
}
