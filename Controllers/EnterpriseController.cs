using BlurApi.Data;
using BlurApi.Models;
using BlurApi.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlurApi.Controllers
{
  [ApiController]
  [Route("api/enterprises")]
  public class EnterpriseController : ControllerBase
  {
    private readonly AppDbContext _db;

    public EnterpriseController(AppDbContext db)
    {
      _db = db;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateEnterprise([FromBody] CreateEnterpriseDto dto)
    {
      try
      {
        if (await _db.Enterprises.AnyAsync(e => e.Email == dto.Email))
        {
          return HttpException.With(400, "Bu e-posta adresi zaten kullanılıyor");
        }

        if (await _db.Enterprises.AnyAsync(e => e.TaxNumber == dto.TaxNumber))
        {
          return HttpException.With(400, "Bu vergi kimlik numarası zaten kullanılıyor");
        }

        if (await _db.Enterprises.AnyAsync(e => e.Phone == dto.Phone))
        {
          return HttpException.With(400, "Bu telefon numarası zaten kullanılıyor");
        }

        var enterprise = new Enterprise
        {
          Title = dto.Title,
          Phone = dto.Phone,
          Email = dto.Email,
          Balance = dto.Balance,
          Address = dto.Address,
          TaxNumber = dto.TaxNumber,
          TaxProvince = dto.TaxProvince,
          TaxDistrict = dto.TaxDistrict,
          Verified = true,
          CreatedAt = DateTime.UtcNow,
          Disabled = false
        };

        _db.Enterprises.Add(enterprise);
        await _db.SaveChangesAsync();

        var response = MapToResponseDto(enterprise);
        return GenericResponse.JustData(response);
      }
      catch (Exception ex)
      {
        return HttpException.With(500, $"Şirket oluşturulurken bir hata oluştu: {ex.Message}");
      }
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetEnterprises(
        [FromQuery] string? search = null,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 5)
    {
      try
      {
        var query = _db.Enterprises.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
          search = NormalizeTurkishText(search);

          query = query.Where(e =>
              NormalizeTurkishText(e.Title).Contains(search) ||
              e.Phone.Contains(search) ||
              NormalizeTurkishText(e.Email).Contains(search) ||
              e.TaxNumber.Contains(search)
          );
        }

        var totalCount = await query.CountAsync();

        var enterprises = await query
            .OrderByDescending(e => e.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var response = new
        {
          enterprises = enterprises.Select(MapToListDto),
          pagination = new
          {
            currentPage = page,
            pageSize = pageSize,
            totalCount = totalCount,
            totalPages = (int)Math.Ceiling((double)totalCount / pageSize)
          }
        };

        return GenericResponse.JustData(response);
      }
      catch (Exception ex)
      {
        return HttpException.With(500, $"Şirketler listelenirken bir hata oluştu: {ex.Message}");
      }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEnterpriseById(Guid id)
    {
      try
      {
        var enterprise = await _db.Enterprises.FindAsync(id);
        if (enterprise == null)
        {
          return HttpException.With(404, "Şirket bulunamadı");
        }

        var response = MapToResponseDto(enterprise);
        return GenericResponse.JustData(response);
      }
      catch (Exception ex)
      {
        return HttpException.With(500, $"Şirket getirilirken bir hata oluştu: {ex.Message}");
      }
    }

    [HttpPatch("{id}/toggle-status")]
    public async Task<IActionResult> ToggleEnterpriseStatus(Guid id, [FromForm] bool disabled)
    {
      try
      {
        var enterprise = await _db.Enterprises.FindAsync(id);
        if (enterprise == null)
        {
          return HttpException.With(404, "Şirket bulunamadı");
        }

        enterprise.Disabled = disabled;
        await _db.SaveChangesAsync();

        var response = MapToResponseDto(enterprise);
        return GenericResponse.JustData(response);
      }
      catch (Exception ex)
      {
        return HttpException.With(500, $"Şirket durumu değiştirilirken bir hata oluştu: {ex.Message}");
      }
    }

    [HttpDelete("{id}/delete")]
    public async Task<IActionResult> DeleteEnterprise(Guid id)
    {
      try
      {
        var enterprise = await _db.Enterprises.FindAsync(id);
        if (enterprise == null)
        {
          return HttpException.With(404, "Şirket bulunamadı");
        }

        _db.Enterprises.Remove(enterprise);
        await _db.SaveChangesAsync();

        return GenericResponse.JustData(new { message = "Şirket başarıyla silindi" });
      }
      catch (Exception ex)
      {
        return HttpException.With(500, $"Şirket silinirken bir hata oluştu: {ex.Message}");
      }
    }

    private static EnterpriseResponseDto MapToResponseDto(Enterprise enterprise)
    {
      return new EnterpriseResponseDto
      {
        Id = enterprise.Id,
        Title = enterprise.Title,
        Phone = enterprise.Phone,
        Email = enterprise.Email,
        Balance = enterprise.Balance,
        Verified = enterprise.Verified,
        Address = enterprise.Address,
        TaxNumber = enterprise.TaxNumber,
        TaxAddress = new TaxAddressDto
        {
          Province = enterprise.TaxProvince,
          District = enterprise.TaxDistrict
        },
        CreatedAt = enterprise.CreatedAt,
        Disabled = enterprise.Disabled
      };
    }

    private static EnterpriseListDto MapToListDto(Enterprise enterprise)
    {
      return new EnterpriseListDto
      {
        Id = enterprise.Id,
        Title = enterprise.Title,
        Email = enterprise.Email,
        Balance = enterprise.Balance,
        Verified = enterprise.Verified,
        CreatedAt = enterprise.CreatedAt,
        Disabled = enterprise.Disabled
      };
    }

    private static string NormalizeTurkishText(string text)
    {
      if (string.IsNullOrEmpty(text)) return string.Empty;

      var turkishCharMap = new Dictionary<char, char>
            {
                { 'ı', 'i' }, { 'ğ', 'g' }, { 'ü', 'u' }, { 'ş', 's' }, { 'ö', 'o' }, { 'ç', 'c' },
                { 'İ', 'I' }, { 'Ğ', 'G' }, { 'Ü', 'U' }, { 'Ş', 'S' }, { 'Ö', 'O' }, { 'Ç', 'C' },
                { 'â', 'a' }, { 'î', 'i' }, { 'û', 'u' }, { 'ô', 'o' },
                { 'Â', 'A' }, { 'Î', 'I' }, { 'Û', 'U' }, { 'Ô', 'O' },
                { 'æ', 'a' }, { 'œ', 'o' }, { 'ß', 's' },
                { 'Æ', 'A' }, { 'Œ', 'O' }
            };

      var normalized = text.ToLowerInvariant();
      foreach (var mapping in turkishCharMap)
      {
        normalized = normalized.Replace(mapping.Key, mapping.Value);
      }

      return normalized;
    }
  }
}
