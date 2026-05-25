using System;

namespace CafeAdisyon.Models
{
    public class Adisyon
    {
        public int AdisyonId { get; set; }
        public int MasaId { get; set; }
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public decimal Fiyat { get; set; }
        public int Adet { get; set; }
        public decimal Toplam { get => Fiyat * Adet; }
        public int OdenmeMi { get; set; } // 0 = Ödenmedi, 1 = Ödendi
        public DateTime EklenmeTarihi { get; set; }

        public Adisyon()
        {
            OdenmeMi = 0;
            EklenmeTarihi = DateTime.Now;
        }

        public Adisyon(int masaId, int urunId, string urunAdi, decimal fiyat, int adet) : this()
        {
            MasaId = masaId;
            UrunId = urunId;
            UrunAdi = urunAdi;
            Fiyat = fiyat;
            Adet = adet;
        }
    }
}
