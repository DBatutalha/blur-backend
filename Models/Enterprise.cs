using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlurApi.Models
{
  public class Enterprise
  {
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^90[0-9]{10}$", ErrorMessage = "Telefon numarası 90 ile başlamalı ve toplam 12 haneli olmalıdır")]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0, double.MaxValue, ErrorMessage = "Bakiye 0'dan büyük olmalıdır")]
    public decimal Balance { get; set; }

    [Required]
    public bool Verified { get; set; } = true;

    [Required]
    [StringLength(1000)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Vergi kimlik numarası 10 haneli olmalıdır")]
    public string TaxNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string TaxProvince { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string TaxDistrict { get; set; } = string.Empty;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required]
    public bool Disabled { get; set; } = false;
  }

  public class CreateEnterpriseDto
  {
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^90[0-9]{10}$", ErrorMessage = "Telefon numarası 90 ile başlamalı ve toplam 12 haneli olmalıdır")]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Bakiye 0'dan büyük olmalıdır")]
    public decimal Balance { get; set; }

    [Required]
    [StringLength(1000)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Vergi kimlik numarası 10 haneli olmalıdır")]
    public string TaxNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string TaxProvince { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string TaxDistrict { get; set; } = string.Empty;
  }

  public class EnterpriseResponseDto
  {
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public bool Verified { get; set; }
    public string Address { get; set; } = string.Empty;
    public string TaxNumber { get; set; } = string.Empty;
    public TaxAddressDto TaxAddress { get; set; } = new();
    public DateTime CreatedAt { get; set; }
    public bool Disabled { get; set; }
  }

  public class TaxAddressDto
  {
    public string Province { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
  }

  public class EnterpriseListDto
  {
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public bool Verified { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Disabled { get; set; }
  }
}
