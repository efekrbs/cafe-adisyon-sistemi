-- SQLite'ta Foreign Key kısıtlamalarını aktif etmek için (isteğe bağlı ama önerilir)
PRAGMA foreign_keys = ON;

-- 1. Masalar Tablosu
CREATE TABLE IF NOT EXISTS Masalar (
    masa_id INTEGER PRIMARY KEY AUTOINCREMENT,
    masa_adi TEXT NOT NULL, 
    durum TEXT DEFAULT 'BOŞ' 
);

-- 2. Menü (Ürünler) Tablosu
CREATE TABLE IF NOT EXISTS Menu (
    urun_id INTEGER PRIMARY KEY AUTOINCREMENT,
    urun_adi TEXT NOT NULL,
    kategori TEXT, 
    fiyat REAL NOT NULL 
);

-- 3. Adisyon Tablosu
CREATE TABLE IF NOT EXISTS Adisyonlar (
    adisyon_id INTEGER PRIMARY KEY AUTOINCREMENT,
    masa_id INTEGER NOT NULL,
    urun_id INTEGER NOT NULL,
    adet INTEGER DEFAULT 1,
    odendi_mi INTEGER DEFAULT 0, 
    eklenme_zamani DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (masa_id) REFERENCES Masalar(masa_id),
    FOREIGN KEY (urun_id) REFERENCES Menu(urun_id)
);

-- 4. Satış Geçmişi ve İstatistikler Tablosu
CREATE TABLE IF NOT EXISTS Satis_Gecmisi (
    satis_id INTEGER PRIMARY KEY AUTOINCREMENT,
    urun_id INTEGER NOT NULL,
    adet INTEGER NOT NULL,
    toplam_tutar REAL NOT NULL,
    satis_zamani DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (urun_id) REFERENCES Menu(urun_id)
);