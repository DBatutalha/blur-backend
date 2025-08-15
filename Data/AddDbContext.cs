using BlurApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlurApi.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Enterprise> Enterprises { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Configure Enterprise entity
      modelBuilder.Entity<Enterprise>(entity =>
      {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedOnAdd();

        entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
        entity.Property(e => e.Phone).IsRequired().HasMaxLength(12);
        entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
        entity.Property(e => e.Balance).HasColumnType("decimal(18,2)");
        entity.Property(e => e.Address).IsRequired().HasMaxLength(1000);
        entity.Property(e => e.TaxNumber).IsRequired().HasMaxLength(10);
        entity.Property(e => e.TaxProvince).IsRequired().HasMaxLength(100);
        entity.Property(e => e.TaxDistrict).IsRequired().HasMaxLength(100);
        entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        // Indexes for better performance
        entity.HasIndex(e => e.Email).IsUnique();
        entity.HasIndex(e => e.TaxNumber).IsUnique();
        entity.HasIndex(e => e.Phone);
        entity.HasIndex(e => e.CreatedAt);
        entity.HasIndex(e => e.Disabled);
      });
    }
  }
}