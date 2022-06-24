using indiGo.Business.MappingProfiles;
using indiGo.Business.Repositories;
using indiGo.Business.Repositories.Abstract;
using indiGo.Business.Services.Email;
using indiGo.Core.Entities;
using indiGo.Core.Services;
using indiGo.Data.EntityFramework;
using indiGo.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var con = builder.Configuration.GetConnectionString("con");
builder.Services.AddDbContext<MyContext>(options => options.UseSqlServer(con));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        // Password settings.
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = false;

        // User settings.
        options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
        options.User.RequireUniqueEmail = true;
    }).AddEntityFrameworkStores<MyContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.SlidingExpiration = true;
});


// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IEmailService, SmtpEmailService>();


builder.Services.AddScoped<IRepository<Address, int>, AddressRepository>();
builder.Services.AddScoped<IRepository<ServiceDemand, int>, ServiceDemandRepository>();
builder.Services.AddScoped<IRepository<Product, int>, ProductRepository>();
builder.Services.AddScoped<IRepository<Receipt, int>, ReceiptRepository>();
builder.Services.AddScoped<IRepository<ReceiptDetail, int>, ReceiptDetailRepository>();



builder.Services.AddAutoMapper(options =>
{
    options.AddProfile<ViewModelMappingProfile>();
});

builder.Services.AddSession();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
