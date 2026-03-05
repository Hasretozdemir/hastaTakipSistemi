# ?? Hasta Takip Sistemi

Gazi Hastanesi için geliþtirilmiþ, Windows Forms tabanlý bir **Hasta Takip ve Yönetim Sistemi**dir. Uygulama; hasta kayýtlarýnýn oluþturulmasý, listelenmesi, güncellenmesi ve silinmesi gibi temel CRUD iþlemlerini destekler. Kullanýcý giriþ ve kayýt sistemi ile yetkilendirme saðlanýr.

## ?? Ekran Görüntüleri

### Giriþ Ekraný
Kullanýcý adý ve þifre ile sisteme giriþ yapýlýr. Kayýtlý olmayan kullanýcýlar "Kayýt Ol" butonu ile yeni hesap oluþturabilir.

![Giriþ Ekraný](screenshots/giris-ekrani.png)

### Ana Sayfa - Hasta Yönetim Paneli
Hasta bilgileri bu ekran üzerinden listelenir, yeni hasta kaydedilir, mevcut kayýtlar güncellenir veya silinir.

![Ana Sayfa](screenshots/ana-sayfa.png)

## ?? Özellikler

- **Kullanýcý Giriþ Sistemi** — Stored Procedure ile güvenli giriþ doðrulamasý
- **Kullanýcý Kayýt** — Yeni kullanýcý hesabý oluþturma
- **Hasta Kayýt** — Ad, soyad, TC, telefon, yaþ, cinsiyet, þikayet, tarih, durum, bölüm ve ex durumu bilgileri ile hasta kaydý oluþturma
- **Hasta Listeleme** — Tüm hasta kayýtlarýný DataGridView üzerinde görüntüleme
- **Hasta Güncelleme** — Seçilen hasta kaydýný düzenleme
- **Hasta Silme** — Seçilen hasta kaydýný veritabanýndan kaldýrma
- **Form Temizleme** — Formdaki tüm alanlarý tek týkla sýfýrlama
- **Durum & Bölüm Yönetimi** — Veritabanýndan dinamik olarak doldurulan ComboBox'lar

## ??? Kullanýlan Teknolojiler

| Teknoloji | Detay |
|-----------|-------|
| **Dil** | C# 11 |
| **Framework** | .NET 7 (Windows Forms) |
| **Veritabaný** | Microsoft SQL Server |
| **Veri Eriþimi** | ADO.NET (SqlConnection, SqlCommand, SqlDataAdapter) |
| **Mimari** | Stored Procedure tabanlý veritabaný iþlemleri |

## ?? Proje Yapýsý

```
hastaTakipSistemi/
??? Form1.cs                 # Giriþ ekraný (Kullanýcý adý & þifre)
??? frmKayit.cs              # Kullanýcý kayýt formu
??? frmAnaSayfa.cs           # Ana sayfa - Hasta yönetim paneli
??? frmSqlBaglanti.cs        # SQL Server baðlantý yönetimi
??? Program.cs               # Uygulama giriþ noktasý
??? Properties/
    ??? Resources.resx       # Uygulama kaynaklarý (logo vb.)
```

## ??? Veritabaný

Uygulama **SQL Server** üzerinde `db_HastaneYonetim` veritabanýný kullanmaktadýr. Aþaðýdaki Stored Procedure'ler kullanýlmaktadýr:

| Stored Procedure | Açýklama |
|------------------|----------|
| `girisYap` | Kullanýcý giriþ doðrulamasý |
| `kayitol` | Yeni kullanýcý kaydý |
| `listele` | Tüm hasta kayýtlarýný listeleme |
| `kaydet` | Yeni hasta kaydý oluþturma |
| `guncelle` | Mevcut hasta kaydýný güncelleme |
| `durumDoldur` | Hasta durum listesini getirme |
| `bolumDoldur` | Hastane bölüm listesini getirme |

## ?? Kurulum

1. **Projeyi klonlayýn:**
   ```bash
   git clone https://github.com/Hasretozdemir/hastaTakipSistemi.git
   ```

2. **SQL Server'da veritabanýný oluþturun:**
   - `db_HastaneYonetim` adýnda bir veritabaný oluþturun
   - Gerekli tablolarý ve Stored Procedure'leri oluþturun

3. **Baðlantý ayarýný güncelleyin:**
   - `frmSqlBaglanti.cs` dosyasýndaki `Data Source` deðerini kendi SQL Server instance adýnýza göre deðiþtirin

4. **Projeyi çalýþtýrýn:**
   - Visual Studio ile `hastaTakipSistemi.sln` dosyasýný açýn
   - Projeyi derleyip çalýþtýrýn

## ?? Gereksinimler

- .NET 7 SDK
- Microsoft SQL Server
- Visual Studio 2022+

## ?? Geliþtirici

**Hasret Özdemir**

---

> Bu proje eðitim amaçlý geliþtirilmiþtir.
