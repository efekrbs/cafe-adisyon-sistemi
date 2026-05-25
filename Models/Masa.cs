namespace CafeAdisyon.Models
{
    public class Masa
    {
        public int MasaId { get; set; }
        public string MasaAdi { get; set; }
        public string Durum { get; set; } // BOŞ, DOLU, ÖDENDI
        public decimal ToplamTutar { get; set; }

        public Masa()
        {
            Durum = "BOŞ";
            ToplamTutar = 0;
        }

        public Masa(int masaId, string masaAdi) : this()
        {
            MasaId = masaId;
            MasaAdi = masaAdi;
        }
    }
}
