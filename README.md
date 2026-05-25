# cafe-adisyon-sistemi

Cafe adisyon süreçlerini yönetmek için geliştirilen bir Windows Forms masaüstü uygulaması. Bu depo, .NET üzerinde çalışan örnek bir kafe otomasyon projesinin temel iskeletini içerir.

## Özellikler

- Windows Forms tabanlı masaüstü arayüz
- .NET Windows hedefi (WinExe)
- Başlangıç formu ile uygulama iskeleti

> Not: Proje şu anda temel bir başlangıç şablonu içerir. İşlevler genişletildikçe bu bölüm güncellenmelidir.

## Teknolojiler

- .NET (Windows Forms)
- C#

## Gereksinimler

- Windows işletim sistemi
- .NET SDK (net10.0-windows hedefini destekleyen sürüm)
- Visual Studio (Windows Forms geliştirme araçlarıyla)

## Kurulum

1. Depoyu klonlayın:
   ```bash
   git clone <repo-url>
   ```
2. Çözümü açın:
   - `cafe-adisyon-sistemi/KeyfiDem/KeyfiDem.slnx`
3. NuGet bağımlılıklarını geri yükleyin (Visual Studio otomatik yapar).

## Çalıştırma

Visual Studio ile:

1. Solution’ı açın.
2. Startup project olarak **KeyfiDem**’i seçin.
3. `F5` ile çalıştırın.

Komut satırı ile:

```bash
dotnet run --project cafe-adisyon-sistemi/KeyfiDem/KeyfiDem/KeyfiDem.csproj
```

## Derleme

```bash
dotnet build cafe-adisyon-sistemi/KeyfiDem/KeyfiDem/KeyfiDem.csproj
```

## Proje Yapısı

```
cafe-adisyon-sistemi/
├─ README.md
└─ KeyfiDem/
   ├─ KeyfiDem.slnx
   └─ KeyfiDem/
	  ├─ KeyfiDem.csproj
	  ├─ Program.cs
	  ├─ Form1.cs
	  └─ Form1.Designer.cs
```

## Notlar

- Uygulama başlangıçta `Form1` formunu açar.
- İşlevsellik eklemek için yeni formlar, servisler ve veri erişim katmanı eklenebilir.

## Katkı

Katkı yapmak için fork alıp değişikliklerinizi pull request olarak gönderebilirsiniz.
