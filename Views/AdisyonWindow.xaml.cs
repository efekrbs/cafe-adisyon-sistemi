using System.Windows;
using System.Windows.Controls;
using CafeAdisyon.Models;
using CafeAdisyon.ViewModels;

namespace CafeAdisyon.Views
{
    public partial class AdisyonWindow : Window
    {
        private AdisyonViewModel _viewModel;

        public AdisyonWindow(Masa masa)
        {
            InitializeComponent();
            _viewModel = new AdisyonViewModel(masa);
            DataContext = _viewModel;

            _viewModel.KapalıViewClosed += (s, e) => Close();
            _viewModel.OdemeYapildi += (s, data) => AcOdemeWindow(data.Item1, data.Item2);
        }

        private void AdetArtir_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int adisyonId)
            {
                _viewModel.AdetArtir(adisyonId);
            }
        }

        private void AdetAzalt_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int adisyonId)
            {
                _viewModel.AdetAzalt(adisyonId);
            }
        }

        private void AcOdemeWindow(Masa masa, List<int> adisyonIds)
        {
            var odemeWindow = new OdemeWindow(masa, _viewModel.MasaAdisyonlari.ToList(), adisyonIds);
            if (odemeWindow.ShowDialog() == true)
            {
                var secilenAdisyonAdetleri = odemeWindow.SecilenAdisyonAdetleri;
                var odemeShekli = odemeWindow.OdemeShekli;
                _viewModel.OdemeyiTamamla(secilenAdisyonAdetleri, odemeShekli);
                Close();
            }
        }
    }
}
