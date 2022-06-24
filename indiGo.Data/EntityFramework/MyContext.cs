using indiGo.Core.Entities;
using indiGo.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace indiGo.Data.EntityFramework;
public sealed class MyContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public MyContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var passwordHasher = new PasswordHasher<ApplicationUser>();
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(x => x.FirstName).HasMaxLength(50).IsRequired(true);
            entity.Property(x => x.LastName).HasMaxLength(50).IsRequired(true);
            entity.Property(x => x.RegisterDate).HasColumnType("datetime");
        });


        //SEED DATA
        builder.Entity<IdentityRole>(entity =>
        {
            entity.HasData(new IdentityRole()
            {
                Id = "1",
                Name = Core.Identity.Roles.Admin,
                NormalizedName = Core.Identity.Roles.Admin.ToUpper(),
            });
            entity.HasData(new IdentityRole()
            {
                Id = "2",
                Name = Core.Identity.Roles.Customer,
                NormalizedName = Core.Identity.Roles.Customer.ToUpper(),
            });
            entity.HasData(new IdentityRole()
            {
                Id = "3",
                Name = Core.Identity.Roles.Operator,
                NormalizedName = Core.Identity.Roles.Operator.ToUpper(),
            });
            entity.HasData(new IdentityRole()
            {
                Id = "4",
                Name = Core.Identity.Roles.Passive,
                NormalizedName = Core.Identity.Roles.Passive.ToUpper(),
            });
            entity.HasData(new IdentityRole()
            {
                Id = "5",
                Name = Core.Identity.Roles.ElectricalService,
                NormalizedName = Core.Identity.Roles.ElectricalService.ToUpper(),
            });
            entity.HasData(new IdentityRole()
            {
                Id = "6",
                Name = Core.Identity.Roles.GasService,
                NormalizedName = Core.Identity.Roles.GasService.ToUpper(),
            });
            entity.HasData(new IdentityRole()
            {
                Id = "7",
                Name = Core.Identity.Roles.PlumbingService,
                NormalizedName = Core.Identity.Roles.PlumbingService.ToUpper(),
            });
        });
        builder.Entity<ApplicationUser>(entity =>
        {
            entity.HasData(new ApplicationUser()
            {
                Id = "8e552862-a24d-4548-a6c6-9443d048cdb9",
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@gmail.com",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                PasswordHash = passwordHasher.HashPassword(null, "123456")
            });
            entity.HasData( new ApplicationUser()
            {
                Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", 
                FirstName = "Operator",
                LastName = "Operator",
                Email = "operator@gmail.com",
                UserName = "operator",
                NormalizedUserName = "OPERATOR",
                PasswordHash = passwordHasher.HashPassword(null,"123456")
            });
            entity.HasData(new ApplicationUser()
            {
                Id = "8e443125-a24d-4543-a6c6-9443d048cdb9",
                FirstName = "Mandosi",
                LastName = "Paki",
                Email = "mandosi@gmail.com",
                UserName = "mandosi",
                NormalizedUserName = "MANDOSI",
                PasswordHash = passwordHasher.HashPassword(null, "123456")
            });
            entity.HasData(new ApplicationUser()
            {
                Id = "8e443125-a24d-4543-a6c6-8223d048cdb9",
                FirstName = "Bewar",
                LastName = "Dılbixhin",
                Email = "bewar@gmail.com",
                UserName = "bewar",
                NormalizedUserName = "BEWAR",
                PasswordHash = passwordHasher.HashPassword(null, "123456")
            });
            entity.HasData(new ApplicationUser()
            {
                Id = "8e443125-a24d-4543-a5g5-8223d048cdb9",
                FirstName = "Cumali",
                LastName = "Cemalikızık",
                Email = "cumali@gmail.com",
                UserName = "cumali",
                NormalizedUserName = "CUMALI",
                PasswordHash = passwordHasher.HashPassword(null, "123456")
            });
        });
        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            
            entity.HasData(new IdentityUserRole<string>
            {
                RoleId = "1",
                UserId = "8e552862-a24d-4548-a6c6-9443d048cdb9"
            });
            entity.HasData(new IdentityUserRole<string>
            {
                RoleId = "3",
                UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
            });
            entity.HasData(new IdentityUserRole<string>
            {
                RoleId = "5",
                UserId = "8e443125-a24d-4543-a6c6-9443d048cdb9"
            });
            entity.HasData(new IdentityUserRole<string>
            {
                RoleId = "6",
                UserId = "8e443125-a24d-4543-a6c6-8223d048cdb9"
            });
            entity.HasData(new IdentityUserRole<string>
            {
                RoleId = "7",
                UserId = "8e443125-a24d-4543-a5g5-8223d048cdb9"
            });
        });



        builder.Entity<Address>(entity =>
        {
            entity.HasOne<ApplicationUser>().WithMany().HasForeignKey(x => x.UserId);
        });

        builder.Entity<ServiceDemand>(entity =>
        {
            entity.HasIndex(x => x.Id);
            entity.Property(x => x.FirstName).HasMaxLength(50).IsRequired(true);
            entity.Property(x => x.LastName).HasMaxLength(50).IsRequired(true);
            entity.Property(x => x.PhoneNumber).HasMaxLength(13).IsRequired(true);
            entity.Property(x => x.TCKN).IsFixedLength().HasMaxLength(11).IsRequired(true);
            entity.Property(x => x.Problem).HasMaxLength(300).IsRequired(true);
            entity.HasOne<Address>().WithMany().HasForeignKey(x => x.AddressId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne<ApplicationUser>().WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Product>(entity =>
        {
            entity.HasIndex(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(80).IsRequired(true);
            entity.Property(x => x.Price).HasPrecision(8,2).IsRequired(true);
          

        });

        builder.Entity<Receipt>(entity =>
        {
            entity.HasIndex(x => x.Id);
        });

        builder.Entity<ReceiptDetail>(entity =>
        {
            entity.HasIndex(x => x.Id);
            entity.HasOne<Product>().WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne<Receipt>().WithMany().HasForeignKey(x => x.ReceiptId).OnDelete(DeleteBehavior.Restrict);
        });
    }

    public DbSet<ServiceDemand> ServiceDemands { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Receipt> Receipts { get; set; }
    public DbSet<Address> Addresses { get; set; }
}