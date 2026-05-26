using System;

namespace CafeAdisyon.Models
{
    public class OdemeKaydi
    {
        public string MasaAdi { get; set; }
        public string Tarih { get; set; }
        public string Saat { get; set; }
        public decimal ToplamTutar { get; set; }
        public decimal OdenenTutar { get; set; }
        public string OdemeShekli { get; set; }
    }
}
