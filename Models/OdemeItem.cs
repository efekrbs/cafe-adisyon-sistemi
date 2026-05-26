using System.ComponentModel;

namespace CafeAdisyon.Models
{
    public class OdemeItem : INotifyPropertyChanged
    {
        private bool _secili;
        private int _secilenAdet;

        public Adisyon Adisyon { get; }

        public bool Secili
        {
            get => _secili;
            set
            {
                if (_secili != value)
                {
                    _secili = value;
                    OnPropertyChanged(nameof(Secili));
                    OnPropertyChanged(nameof(SecilenToplam));
                }
            }
        }

        public int SecilenAdet
        {
            get => _secilenAdet;
            set
            {
                int clamped = value < 1 ? 1 : (value > Adisyon.Adet ? Adisyon.Adet : value);
                if (_secilenAdet != clamped)
                {
                    _secilenAdet = clamped;
                    OnPropertyChanged(nameof(SecilenAdet));
                    OnPropertyChanged(nameof(SecilenToplam));
                }
            }
        }

        public decimal SecilenToplam => Secili ? Adisyon.Fiyat * SecilenAdet : 0;

        // Kolay erişim için geçişli property'ler
        public string UrunAdi => Adisyon.UrunAdi;
        public int MaxAdet => Adisyon.Adet;
        public decimal BirimFiyat => Adisyon.Fiyat;

        public OdemeItem(Adisyon adisyon)
        {
            Adisyon = adisyon;
            _secilenAdet = adisyon.Adet;
            _secili = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
