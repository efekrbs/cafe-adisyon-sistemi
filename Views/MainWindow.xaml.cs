using System.Windows;
using CafeAdisyon.Models;
using CafeAdisyon.ViewModels;

namespace CafeAdisyon.Views
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;

            _viewModel.MasaTiklandi += (s, masa) => AcAdisyonWindow(masa);
            _viewModel.AyarlarAcildı += (s, e) => AcAyarlarWindow();
        }

        private void AcAdisyonWindow(Masa masa)
        {
            var adisyonWindow = new AdisyonWindow(masa);
            adisyonWindow.ShowDialog();
            _viewModel.YenilemeCommand.Execute(null);
        }

        private void AcAyarlarWindow()
        {
            var ayarlarWindow = new AyarlarWindow();
            ayarlarWindow.ShowDialog();
        }
    }
}
