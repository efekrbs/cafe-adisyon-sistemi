namespace CafeAdisyon.Models
{
    public class Urun
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public string Kategori { get; set; }
        public decimal Fiyat { get; set; }

        public Urun() { }

        public Urun(int urunId, string urunAdi, string kategori, decimal fiyat)
        {
            UrunId = urunId;
            UrunAdi = urunAdi;
            Kategori = kategori;
            Fiyat = fiyat;
        }
    }
}
