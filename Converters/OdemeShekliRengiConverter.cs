using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CafeAdisyon
{
    public class OdemeShekliRengiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string odemeShekli)
            {
                if (odemeShekli == "Kart")
                    return Color.FromArgb(255, 33, 150, 243); // Mavi - #2196F3
                else
                    return Color.FromArgb(255, 76, 175, 80); // Yeşil - #4CAF50
            }
            return Color.FromArgb(255, 158, 158, 158); // Gri - varsayılan
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
