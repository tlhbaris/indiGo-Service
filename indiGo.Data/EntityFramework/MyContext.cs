using indiGo.Core.Entities;
using indiGo.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace indiGo.Data.EntityFramework;
public sealed class MyContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public MyContext(DbContextOptions options) :base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(x => x.FirstName).HasMaxLength(50).IsRequired(true);
            entity.Property(x => x.LastName).HasMaxLength(50).IsRequired(true);
            entity.Property(x => x.RegisterDate).HasColumnType("datetime");
        });

        builder.Entity<ServiceDemand>(entity =>
        {
            entity.HasIndex(x => x.Id);
            entity.Property(x => x.FirstName).HasMaxLength(50).IsRequired(true);
            entity.Property(x => x.LastName).HasMaxLength(50).IsRequired(true);
            entity.Property(x => x.Address).HasMaxLength(300).IsRequired(true);
            entity.Property(x => x.PhoneNumber).HasMaxLength(13).IsRequired(true);
            entity.Property(x => x.TCKN).IsFixedLength().HasMaxLength(11).IsRequired(true);
            entity.Property(x => x.Problem).HasMaxLength(300).IsRequired(true);
        });

        builder.Entity<Entry>(entity =>
        {
            entity.HasIndex(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(80).IsRequired(true);
            entity.Property(x => x.Price).HasPrecision(2).IsRequired(true);
            entity.HasOne(x => x.Receipt).WithMany(x => x.ReceiptEntries).HasForeignKey(x => x.ReceiptId);
        });

        builder.Entity<Receipt>(entity =>
        {
            entity.HasIndex(x => x.Id);
            entity.Property(x => x.Operation).HasMaxLength(300).IsRequired(true);
            entity.HasMany(x => x.ReceiptEntries).WithOne(x => x.Receipt).HasForeignKey(x => x.ReceiptId);
        });
    }

    public DbSet<ServiceDemand> ServiceDemands { get; set; }
    public DbSet<Entry> Entries { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
}