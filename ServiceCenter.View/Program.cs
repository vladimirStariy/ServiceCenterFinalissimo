using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceCenter.Data;
using ServiceCenter.Domain.Entity;
using ServiceCenter.Domain.Enum;
using ServiceCenter.View;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .UseLazyLoadingProxies()
);
builder.Services.AddScoped<DbContext, ApplicationDbContext>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.LogoutPath = new Microsoft.AspNetCore.Http.PathString("/Account/Logout");
    });
builder.Services.AddAuthorization();

builder.Services.InitializeRepositories();
builder.Services.InitializeServices();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.UseRequestLocalization();
CultureInfo customCulture = new CultureInfo("en-US");
customCulture.NumberFormat.NumberDecimalSeparator = ".";

CultureInfo.DefaultThreadCurrentCulture = customCulture;
//CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;



app.MapAreaControllerRoute(
    name: "Operator",
    areaName: "Operator",
    pattern: "operator/{controller=Abonent}/{action=Abonents}/{id?}");

app.MapAreaControllerRoute(
    name: "Employee",
    areaName: "Employer",
    pattern: "employee/{controller=Abonent}/{action=Abonents}/{id?}");

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",
    pattern: "admin/{controller=Tariff}/{action=Tariffs}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Guest}/{action=Tariffs}/{id?}");


app.Run();
