using BlurApi.Data;
using BlurApi.Models;

namespace BlurApi.Services
{
  public class DatabaseSeederService
  {
    private readonly AppDbContext _dbContext;

    public DatabaseSeederService(AppDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
      if (!_dbContext.Enterprises.Any())
      {
        var sampleEnterprises = new List<Enterprise>
                {
                    new Enterprise
                    {
                        Title = "Şirket A",
                        Phone = "905551234567",
                        Email = "info@sirket-a.com",
                        Balance = 15000.50m,
                        Verified = true,
                        Address = "Örnek Mahallesi, Örnek Sokak No:1\nKadıköy, İstanbul",
                        TaxNumber = "1234567890",
                        TaxProvince = "İstanbul",
                        TaxDistrict = "Kadıköy",
                        CreatedAt = DateTime.UtcNow.AddDays(-30),
                        Disabled = false
                    },
                    new Enterprise
                    {
                        Title = "Şirket B",
                        Phone = "905559876543",
                        Email = "info@sirket-b.com",
                        Balance = 25000.75m,
                        Verified = true,
                        Address = "Örnek Mahallesi, Örnek Sokak No:2\nÇankaya, Ankara",
                        TaxNumber = "0987654321",
                        TaxProvince = "Ankara",
                        TaxDistrict = "Çankaya",
                        CreatedAt = DateTime.UtcNow.AddDays(-25),
                        Disabled = true
                    },
                    new Enterprise
                    {
                        Title = "Şirket C",
                        Phone = "905551112233",
                        Email = "info@sirket-c.com",
                        Balance = 50000.00m,
                        Verified = false,
                        Address = "Örnek Mahallesi, Örnek Sokak No:3\nBeşiktaş, İstanbul",
                        TaxNumber = "1122334455",
                        TaxProvince = "İstanbul",
                        TaxDistrict = "Beşiktaş",
                        CreatedAt = DateTime.UtcNow.AddDays(-20),
                        Disabled = false
                    },
                    new Enterprise
                    {
                        Title = "Şirket D",
                        Phone = "905559998877",
                        Email = "info@sirket-d.com",
                        Balance = 75000.25m,
                        Verified = true,
                        Address = "Örnek Mahallesi, Örnek Sokak No:4\nNilüfer, Bursa",
                        TaxNumber = "9988776655",
                        TaxProvince = "Bursa",
                        TaxDistrict = "Nilüfer",
                        CreatedAt = DateTime.UtcNow.AddDays(-15),
                        Disabled = false
                    },
                    new Enterprise
                    {
                        Title = "Şirket E",
                        Phone = "905551234567",
                        Email = "info@sirket-e.com",
                        Balance = 30000.00m,
                        Verified = true,
                        Address = "Örnek Mahallesi, Örnek Sokak No:5\nKonak, İzmir",
                        TaxNumber = "5544332211",
                        TaxProvince = "İzmir",
                        TaxDistrict = "Konak",
                        CreatedAt = DateTime.UtcNow.AddDays(-10),
                        Disabled = true
                    },
                    new Enterprise
                    {
                        Title = "Şirket F",
                        Phone = "905556667788",
                        Email = "info@sirket-f.com",
                        Balance = 120000.00m,
                        Verified = true,
                        Address = "Örnek Mahallesi, Örnek Sokak No:6\nÜsküdar, İstanbul",
                        TaxNumber = "6677889900",
                        TaxProvince = "İstanbul",
                        TaxDistrict = "Üsküdar",
                        CreatedAt = DateTime.UtcNow.AddDays(-8),
                        Disabled = false
                    },
                    new Enterprise
                    {
                        Title = "Şirket G",
                        Phone = "905557778899",
                        Email = "info@sirket-g.com",
                        Balance = 85000.50m,
                        Verified = false,
                        Address = "Örnek Mahallesi, Örnek Sokak No:7\nTepebaşı, Eskişehir",
                        TaxNumber = "7788990011",
                        TaxProvince = "Eskişehir",
                        TaxDistrict = "Tepebaşı",
                        CreatedAt = DateTime.UtcNow.AddDays(-6),
                        Disabled = false
                    },
                    new Enterprise
                    {
                        Title = "Şirket H",
                        Phone = "905558889900",
                        Email = "info@sirket-h.com",
                        Balance = 200000.75m,
                        Verified = true,
                        Address = "Örnek Mahallesi, Örnek Sokak No:8\nAtaşehir, İstanbul",
                        TaxNumber = "8899001122",
                        TaxProvince = "İstanbul",
                        TaxDistrict = "Ataşehir",
                        CreatedAt = DateTime.UtcNow.AddDays(-4),
                        Disabled = false
                    },
                    new Enterprise
                    {
                        Title = "Şirket I",
                        Phone = "905559900112",
                        Email = "info@sirket-i.com",
                        Balance = 95000.25m,
                        Verified = true,
                        Address = "Örnek Mahallesi, Örnek Sokak No:9\nPendik, İstanbul",
                        TaxNumber = "9900112233",
                        TaxProvince = "İstanbul",
                        TaxDistrict = "Pendik",
                        CreatedAt = DateTime.UtcNow.AddDays(-2),
                        Disabled = true
                    },
                    new Enterprise
                    {
                        Title = "Şirket J",
                        Phone = "905550011223",
                        Email = "info@sirket-j.com",
                        Balance = 175000.00m,
                        Verified = true,
                        Address = "Örnek Mahallesi, Örnek Sokak No:10\nSancaktepe, İstanbul",
                        TaxNumber = "0011223344",
                        TaxProvince = "İstanbul",
                        TaxDistrict = "Sancaktepe",
                        CreatedAt = DateTime.UtcNow.AddDays(-1),
                        Disabled = false
                    }
                };

        _dbContext.Enterprises.AddRange(sampleEnterprises);
        await _dbContext.SaveChangesAsync();


      }
    }
  }
}
