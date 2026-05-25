# Proje: Kafe Adisyon ve Sipariş Yönetim Sistemi (Masaüstü Uygulaması)

Bir kafe için masaüstü adisyon ve sipariş yönetim uygulaması geliştirmek istiyorum. Lütfen kod mimarisini, veritabanı şemasını ve arayüz (GUI) tasarımını aşağıdaki gereksinimlere göre oluştur.

## 1. Genel Kısıtlamalar ve Teknoloji Yığını
* **Platform:** Masaüstü uygulaması (Örn: Java Swing, JavaFX veya C# WPF).
* **Veritabanı:** SQLite. Veritabanı bağlantıları OOP prensiplerine (örneğin DAO tasarım deseni) uygun yazılmalı.
* **Kullanıcı Girişi (Auth):** Sistemde KESİNLİKLE giriş/çıkış (login) ekranı veya hesap oluşturma altyapısı olmayacaktır. Uygulama çalıştırıldığında doğrudan Ana Ekran açılacaktır.
* **Donanım:** Fiziksel POS cihazı entegrasyonu olmayacaktır, tüm işlemler yazılımsal olarak tutulacaktır.

## 2. Kullanıcı Arayüzü (UI) İşlevleri

### A. Ana Ekran (Masa Görünümü)
* Ekranın tamamı, kafedeki masaları temsil eden **kare şeklindeki kartlardan (buton/panel)** oluşmalıdır.
* Her kare kart, temsil ettiği masanın numarasını ve anlık durumunu (boş/dolu, mevcut hesap tutarı) göstermelidir.
* Bir masaya tıklandığında adisyon ekranı açılmalı; bu ekrandan o masaya belirli içecekler/yiyecekler eklenebilmeli ve çıkarılabilmelidir.

### B. Ayarlar ve Yönetim Paneli
Ana ekranda her zaman erişilebilir bir "Ayarlar" butonu bulunmalıdır. Bu buton aşağıdaki iki alt sekmeyi içermelidir:

#### 1. Menü Yönetimi
* Mevcut ürünlerin fiyatlarını güncelleme.
* Menüye yeni ürün (isim, kategori, fiyat) ekleme veya silme.

#### 2. İstatistikler ve Kasa Takibi
* Günlük, haftalık ve aylık toplam satış tutarlarının (ciro) listelenmesi.
* Ürün bazlı stok/satış analizi: Hangi üründen toplam kaç adet satıldığının takip edilmesi.

## 3. Ödeme ve Tahsilat Mantığı (ÖNEMLİ)
Sistemde "Alman usulü" ödemeyi destekleyen esnek bir ödeme altyapısı kurulmalıdır:
* **Parçalı Ödeme:** Masadaki tüm hesabı tek seferde kapatmak yerine, sadece belirli ürünler (örn: 2 çay, 1 kahve) seçilip ödenebilmelidir.
* Seçilip ödemesi alınan ürünler o masanın adisyonundan düşmeli, kalan ürünler için masa "açık" durumunu korumalıdır.

## 4. Geliştiriciden Beklentiler
Bu isterler doğrultusunda bana öncelikle:
1. SQLite veritabanı tablo yapılarını (`Masalar`, `Menu`, `Adisyonlar`, `Satis_Gecmisi` vb.) ve aralarındaki ilişkileri,
2. Projenin genel klasör ve sınıf (Class) yapısını,
3. Ana ekran ve parçalı ödeme sisteminin mantığını kuracak örnek temel kod parçalarını yazar mısın?