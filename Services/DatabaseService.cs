using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using CafeAdisyon.Models;

namespace CafeAdisyon
{
    public static class DatabaseService
    {
        private static string _connectionString;
        private static readonly string DbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "CafeAdisyon",
            "database.db"
        );

        public static void Initialize()
        {
            try
            {
                // Veritabanı dizinini oluştur
                string dbDir = Path.GetDirectoryName(DbPath);
                if (!Directory.Exists(dbDir))
                {
                    Directory.CreateDirectory(dbDir);
                }

                _connectionString = $"Data Source={DbPath};Version=3;";

                // Veritabanı dosyası yoksa oluştur
                if (!File.Exists(DbPath))
                {
                    CreateDatabase();
                }

                // Bağlantı test et
                using (var conn = GetConnection())
                {
                    conn.Open();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Veritabanı başlatılamadı: {ex.Message}");
            }
        }

        private static void CreateDatabase()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = File.ReadAllText("VeriTabanı.sql");
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        // MASALAR
        public static List<Masa> GetAllMasalar()
        {
            var masalar = new List<Masa>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM Masalar", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            masalar.Add(new Masa
                            {
                                MasaId = Convert.ToInt32(reader["masa_id"]),
                                MasaAdi = reader["masa_adi"].ToString(),
                                Durum = reader["durum"].ToString()
                            });
                        }
                    }
                }
                conn.Close();
            }
            return masalar;
        }

        public static void AddMasa(string masaAdi)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "INSERT INTO Masalar (masa_adi, durum) VALUES (@adi, 'BOŞ')", conn))
                {
                    cmd.Parameters.AddWithValue("@adi", masaAdi);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        // MENÜ
        public static List<Urun> GetAllUrunler()
        {
            var urunler = new List<Urun>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM Menu ORDER BY kategori", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            urunler.Add(new Urun
                            {
                                UrunId = Convert.ToInt32(reader["urun_id"]),
                                UrunAdi = reader["urun_adi"].ToString(),
                                Kategori = reader["kategori"].ToString(),
                                Fiyat = Convert.ToDecimal(reader["fiyat"])
                            });
                        }
                    }
                }
                conn.Close();
            }
            return urunler;
        }

        public static void AddUrun(string urunAdi, string kategori, decimal fiyat)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "INSERT INTO Menu (urun_adi, kategori, fiyat) VALUES (@adi, @kategori, @fiyat)", conn))
                {
                    cmd.Parameters.AddWithValue("@adi", urunAdi);
                    cmd.Parameters.AddWithValue("@kategori", kategori);
                    cmd.Parameters.AddWithValue("@fiyat", fiyat);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public static void UpdateUrun(int urunId, string urunAdi, string kategori, decimal fiyat)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    "UPDATE Menu SET urun_adi=@adi, kategori=@kategori, fiyat=@fiyat WHERE urun_id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", urunId);
                    cmd.Parameters.AddWithValue("@adi", urunAdi);
                    cmd.Parameters.AddWithValue("@kategori", kategori);
                    cmd.Parameters.AddWithValue("@fiyat", fiyat);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        public static void DeleteUrun(int urunId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("DELETE FROM Menu WHERE urun_id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", urunId);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        // ADİSYONLAR
        public static List<Adisyon> GetMasaAdisyonlari(int masaId, bool odenmemisler = true)
        {
            var adisyonlar = new List<Adisyon>();
            string query = @"
                SELECT a.adisyon_id, a.masa_id, a.urun_id, m.urun_adi, m.fiyat, a.adet, a.odendi_mi, a.eklenme_zamani
                FROM Adisyonlar a
                INNER JOIN Menu m ON a.urun_id = m.urun_id
                WHERE a.masa_id = @masaId";

            if (odenmemisler)
                query += " AND a.odendi_mi = 0";

            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@masaId", masaId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            adisyonlar.Add(new Adisyon
                            {
                                AdisyonId = Convert.ToInt32(reader["adisyon_id"]),
                                MasaId = Convert.ToInt32(reader["masa_id"]),
                                UrunId = Convert.ToInt32(reader["urun_id"]),
                                UrunAdi = reader["urun_adi"].ToString(),
                                Fiyat = Convert.ToDecimal(reader["fiyat"]),
                                Adet = Convert.ToInt32(reader["adet"]),
                                OdenmeMi = Convert.ToInt32(reader["odendi_mi"]),
                                EklenmeTarihi = Convert.ToDateTime(reader["eklenme_zamani"])
                            });
                        }
                    }
                }
                conn.Close();
            }
            return adisyonlar;
        }

        public static decimal GetMasaToplam(int masaId)
        {
            decimal toplam = 0;
            var adisyonlar = GetMasaAdisyonlari(masaId, true);
            foreach (var adisyon in adisyonlar)
            {
                toplam += adisyon.Toplam;
            }
            return toplam;
        }

        public static void AddAdisyon(int masaId, int urunId, int adet)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    @"INSERT INTO Adisyonlar (masa_id, urun_id, adet, odendi_mi, eklenme_zamani)
                      VALUES (@masaId, @urunId, @adet, 0, @zaman)", conn))
                {
                    cmd.Parameters.AddWithValue("@masaId", masaId);
                    cmd.Parameters.AddWithValue("@urunId", urunId);
                    cmd.Parameters.AddWithValue("@adet", adet);
                    cmd.Parameters.AddWithValue("@zaman", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }

                // Masa durumunu güncelle
                UpdateMasaDurum(masaId, "DOLU", conn);
                conn.Close();
            }
        }

        public static void DeleteAdisyon(int adisyonId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("DELETE FROM Adisyonlar WHERE adisyon_id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", adisyonId);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        // ÖDEME (Parçalı Ödeme)
        public static void PayAdisyonlar(int masaId, List<int> adisyonIds)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (int adisyonId in adisyonIds)
                        {
                            // Adisyon bilgilerini al
                            var adisyon = GetAdisyonById(adisyonId, conn);
                            if (adisyon != null)
                            {
                                // Satış geçmişine ekle
                                InsertSatisGecmisi(adisyon, conn);

                                // Adisyonu ödenmiş olarak işaretle
                                using (var cmd = new SQLiteCommand(
                                    "UPDATE Adisyonlar SET odendi_mi = 1 WHERE adisyon_id = @id", conn))
                                {
                                    cmd.Parameters.AddWithValue("@id", adisyonId);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        // Masa hala ödenmemiş item var mı kontrol et
                        int kalanItemler = 0;
                        using (var cmd = new SQLiteCommand(
                            "SELECT COUNT(*) FROM Adisyonlar WHERE masa_id = @masaId AND odendi_mi = 0", conn))
                        {
                            cmd.Parameters.AddWithValue("@masaId", masaId);
                            kalanItemler = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Tüm itemler ödendiyse masayı boş yap
                        if (kalanItemler == 0)
                        {
                            UpdateMasaDurum(masaId, "BOŞ", conn);
                        }

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
                conn.Close();
            }
        }

        private static Adisyon GetAdisyonById(int adisyonId, SQLiteConnection conn)
        {
            using (var cmd = new SQLiteCommand(
                @"SELECT a.*, m.urun_adi, m.fiyat
                  FROM Adisyonlar a
                  INNER JOIN Menu m ON a.urun_id = m.urun_id
                  WHERE a.adisyon_id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", adisyonId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Adisyon
                        {
                            AdisyonId = Convert.ToInt32(reader["adisyon_id"]),
                            MasaId = Convert.ToInt32(reader["masa_id"]),
                            UrunId = Convert.ToInt32(reader["urun_id"]),
                            UrunAdi = reader["urun_adi"].ToString(),
                            Fiyat = Convert.ToDecimal(reader["fiyat"]),
                            Adet = Convert.ToInt32(reader["adet"]),
                            OdenmeMi = Convert.ToInt32(reader["odendi_mi"])
                        };
                    }
                }
            }
            return null;
        }

        private static void InsertSatisGecmisi(Adisyon adisyon, SQLiteConnection conn)
        {
            using (var cmd = new SQLiteCommand(
                @"INSERT INTO Satis_Gecmisi (urun_id, adet, toplam_tutar, satis_zamani)
                  VALUES (@urunId, @adet, @tutar, @zaman)", conn))
            {
                cmd.Parameters.AddWithValue("@urunId", adisyon.UrunId);
                cmd.Parameters.AddWithValue("@adet", adisyon.Adet);
                cmd.Parameters.AddWithValue("@tutar", adisyon.Toplam);
                cmd.Parameters.AddWithValue("@zaman", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }

        private static void UpdateMasaDurum(int masaId, string durum, SQLiteConnection conn)
        {
            using (var cmd = new SQLiteCommand("UPDATE Masalar SET durum = @durum WHERE masa_id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@durum", durum);
                cmd.Parameters.AddWithValue("@id", masaId);
                cmd.ExecuteNonQuery();
            }
        }

        // İSTATİSTİKLER
        public static decimal GetGunlukCiro()
        {
            decimal toplam = 0;
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    @"SELECT SUM(toplam_tutar) FROM Satis_Gecmisi
                      WHERE DATE(satis_zamani) = DATE('now')", conn))
                {
                    var result = cmd.ExecuteScalar();
                    toplam = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
                conn.Close();
            }
            return toplam;
        }

        public static List<(string UrunAdi, int ToplamAdet)> GetUrunSatisRaporu()
        {
            var rapor = new List<(string, int)>();
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(
                    @"SELECT m.urun_adi, SUM(sg.adet) as toplam_adet
                      FROM Satis_Gecmisi sg
                      INNER JOIN Menu m ON sg.urun_id = m.urun_id
                      GROUP BY sg.urun_id
                      ORDER BY toplam_adet DESC", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            rapor.Add((
                                reader["urun_adi"].ToString(),
                                Convert.ToInt32(reader["toplam_adet"])
                            ));
                        }
                    }
                }
                conn.Close();
            }
            return rapor;
        }
    }
}
