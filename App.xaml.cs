using System.Windows;

namespace CafeAdisyon
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Veritabanı başlatma
            DatabaseService.Initialize();
        }
    }
}
