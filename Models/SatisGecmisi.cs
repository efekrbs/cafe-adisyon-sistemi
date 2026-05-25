using System;

namespace CafeAdisyon.Models
{
    public class SatisGecmisi
    {
        public int SatisId { get; set; }
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int Adet { get; set; }
        public decimal ToplamTutar { get; set; }
        public DateTime SatisTarihi { get; set; }

        public SatisGecmisi()
        {
            SatisTarihi = DateTime.Now;
        }

        public SatisGecmisi(int urunId, string urunAdi, int adet, decimal toplamTutar) : this()
        {
            UrunId = urunId;
            UrunAdi = urunAdi;
            Adet = adet;
            ToplamTutar = toplamTutar;
        }
    }
}
