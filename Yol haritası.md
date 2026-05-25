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

---

## 5. İMPLEMENTASYON NOTLARI (26.05.2026)

### ✅ Tamamlanan İşler

#### Teknoloji Stack
- **Platform:** C# WPF (Windows Presentation Foundation)
- **Veritabanı:** SQLite 
- **Mimarisi:** MVVM (Model-View-ViewModel)
- **.NET Version:** .NET 8.0 Windows

#### Proje Yapısı
```
CafeAdisyon/
├── Models/
│   ├── Masa.cs          (Masa bilgisi ve durumu)
│   ├── Urun.cs          (Ürün bilgileri)
│   ├── Adisyon.cs       (Masa adisyonları)
│   └── SatisGecmisi.cs  (Satış geçmiş ve istatistikler)
├── Services/
│   └── DatabaseService.cs (SQLite DAO Patternı, Tüm DB işlemleri)
├── ViewModels/
│   ├── RelayCommand.cs      (ICommand implementasyonu)
│   ├── MainViewModel.cs     (Ana ekran mantığı)
│   └── AdisyonViewModel.cs  (Adisyon ekranı mantığı)
├── Views/
│   ├── MainWindow.xaml(.cs)     (Masa kartları grid'i)
│   ├── AdisyonWindow.xaml(.cs)  (Adisyon/sipariş ekranı)
│   ├── OdemeWindow.xaml(.cs)    (Parçalı ödeme ekranı)
│   └── AyarlarWindow.xaml(.cs)  (Menü yönetimi + İstatistikler)
├── App.xaml(.cs)           (Tema ve uygulamayönü)
├── Program.cs              (Entry Point)
└── CafeAdisyon.csproj      (Proje konfigürasyonu)
```

#### Tasarım Özellikleri (Beyaz Arka Plan)
- ✅ Beyaz ana arka plan (#FFFFFF)
- ✅ Açık gri ikincil renkler (#F5F5F5)
- ✅ Yeşil aksan renkleri (#4CAF50) - "Ödeme" ve önemli butonları
- ✅ Yuvarlatılmış köşeler ve modern flat design
- ✅ Segoe UI font için profesyonel görünüm
- ✅ Tüm metinler koyu gri (#333333)

#### Ekranlar ve İşlevsellik

**1. Ana Ekran (MainWindow)**
- ✅ Masaları temsil eden kare kartlar (140x140 px)
- ✅ Her masa: Adı, güncel toplam tutar (₺), durum badge (BOŞ/DOLU)
- ✅ WrapPanel layout ile responsive tasarım
- ✅ Hover efektleri ve interaktif butonlar
- ✅ "Yenile" ve "Ayarlar" butonları üst bar'da

**2. Adisyon/Sipariş Ekranı (AdisyonWindow)**
- ✅ Sol taraf: Menü ürünleri grid'i (120x100 px butonlar)
- ✅ Sağ taraf: Seçilen masa adisyonları listesi
- ✅ Ürün tıkla → anında adisyon listesine ekle
- ✅ X butonuyla ürün çıkart
- ✅ Real-time toplam tutarı göster
- ✅ "ÖDEME YAP" ve "KAPAT" butonları

**3. Parçalı Ödeme Ekranı (OdemeWindow)**
- ✅ Adisyon listesi CheckBox'lar ile
- ✅ Seçilen itemlerin toplamı real-time hesapla
- ✅ Almanca (Alman) usulü ödeme: Belirli ürünler seç → ödemeyi tamamla
- ✅ Kalan ürünler masada kalsın, masa açık kalır

**4. Ayarlar Paneli (AyarlarWindow)**
- ✅ Sekme sistemi (Menü Yönetimi / İstatistikler / Masalar)
  
  **4a. Menü Yönetimi**
  - ✅ Mevcut ürünleri listele (Ürün Adı, Kategori, Fiyat)
  - ✅ Ürün sil butonu
  - ✅ Yeni ürün ekle formu (Adı, Kategori dropdown, Fiyat)
  
  **4b. İstatistikler**
  - ✅ Günlük ciro (bugün satılan toplam)
  - ✅ Tahmini aylık ciro
  - ✅ Tahmini yıllık ciro
  - ✅ Ürün bazlı satış raporu (En çok satılan üründen sırası ile)
  
  **4c. Masa Yönetimi**
  - ✅ Mevcut masaları listele (İsim + Durum + Toplam Tutar)
  - ✅ Yeni masa ekle formu

#### Veritabanı & Business Logic
- ✅ DatabaseService.cs: DAO Pattern ile tüm CRUD işlemleri
- ✅ Masalar: İsim, Durum (BOŞ/DOLU/ÖDENDI) yönetimi
- ✅ Menü: Ürün CRUD + kategori yönetimi
- ✅ Adisyonlar: Masa başına sepet yönetimi + ödenmiş/ödenmemiş bayrakı
- ✅ Parçalı Ödeme: Seçilen adisyonları Satis_Gecmisi'ne ekle + durum güncelle
- ✅ Satış Geçmişi: Her ödeme işlemi kayıt edilir

#### Avantajlar & Mimari Kararlar
1. **MVVM Pattern:** UI ve Business Logic tamamen ayrı (Test edilebilir, Bakım kolay)
2. **DatabaseService:** Centralized DB access, connection pooling olmaksızın basit ve etkili
3. **RelayCommand:** Data Binding'e uygun komut sistemi
4. **Beyaz Tema:** Gözler rahat, profesyonel görünüm, okunabilir
5. **Responsive Grid:** Kaç masa olursa olsun uyum sağlar
6. **Parçalı Ödeme:** Gerçek kafe kullanım senaryolarına uygun (grup hesap, kısmi ödeme vb.)

---

## 6. ÖN EKİP ARAŞTIRMALARI

### SQLite ile WPF İntegrasyonu
- **Yapılan:** System.Data.SQLite NuGet paketi ile entegrasyon
- **Karar:** In-process SQLite, ayrı server gerekmez (masaüstü uygulaması için ideal)
- **Veritabanı Konumu:** `AppData\Roaming\CafeAdisyon\database.db`

### MVVM Kütüphane Seçimi
- **Yapılan:** Lightweight RelayCommand + Manuel ViewModel
- **Alternatif:** CommunityToolkit.Mvvm (eklenmiş, ihtiyaç durumunda kullanılabilir)
- **Karar:** Basit proje olduğu için minimal dependency

### Parçalı Ödeme Tasarımı
- **Seçilen Yöntem:** CheckBox ile adisyon seçimi
- **Veritabanı:** `odendi_mi` bayrakı (0=ödenmedi, 1=ödendi)
- **Mantık:** Seçilen adisyonları UPDATE ile odendi_mi=1 yap, kalan itemler dönsün

### Tema Sistemi
- **Beyaz Arka Plan:** App.xaml'de StaticResource olarak tanımlanmış
- **Renk Paleti:** Material Design prensipleri (Yeşil aksan, gri nötr)
- **Esneklik:** Tema renkleri değiştirmek için App.xaml'de tek nokta değişim

---

## 7. YAPILACAKLAR (TODO)

### Faz 1: Temel İşlevsellik (Öncelikli)
- [ ] Ödeme penceresinde CheckBox event handling iyileştirmesi
- [ ] Ürün adetini artır/azalt özelliği (şu an +1 her tıkla)
- [ ] Boş masa sayısını göster istatistiklerde

### Faz 2: UX İyileştirmeleri
- [ ] Ana ekranda arama/filtreleme (masa ismine göre)
- [ ] Ürün kategorilerine göre sekmeleme menüde
- [ ] Ödeme öncesi özet confirmation dialog
- [ ] Tuş takımı kısayolları (POS cihazı emülasyonu)

### Faz 3: İleri Özellikler
- [ ] Kasiyerler/Kullanıcı yönetimi (isteğe bağlı)
- [ ] Belirli tarih aralığı istatistikleri
- [ ] PDF fatura yazdırma
- [ ] Ürün resim galerisi
- [ ] Masaya not/yorum ekleme (VIP müşteri, özel istek vb.)

### Faz 4: Optimizasyon
- [ ] Veritabanı backup/restore
- [ ] Uygulama skinning/dark mode seçeneği
- [ ] Performans profiling (çok masalı kafe için)
- [ ] Lokalizasyon (İngilizce, Arapça vb.)

---

## 8. SİSTEM GEREKSINIMLERI

**Minimum:**
- Windows 7 SP1 veya üzeri
- .NET 8.0 Runtime
- 100 MB disk alanı
- 512 MB RAM

**Önerilen:**
- Windows 10/11
- Dokunmatik ekran (tablet/POS cihazı)
- 1024x768 minimum çözünürlük

---

## 9. DERLEME & ÇALIŞTIRMA

```bash
# NuGet paketlerini geri yükle
dotnet restore

# Derle
dotnet build -c Release

# Çalıştır
dotnet run
```

İlk çalıştırmada veritabanı otomatik oluşturulur (`VeriTabanı.sql` script'i ile).

---

**Sonraki Adımlar:**
1. Test masaları ve ürünleri başlangıç verileri olarak ekle
2. UI responsiveness kontrolü
3. Veritabanı verilerinin tutarlılığını test et
4. Parçalı ödeme workflow'unu kafe senaryolarıyla test et