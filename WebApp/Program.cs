using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebApp;
using DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // For MVC
builder.Services.AddControllers(); // For Web API

// Register the DbContext with the connection string from appsettings.json
builder.Services.AddDbContext<CompanyInfoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CompanyInfoConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"); // For MVC
app.MapControllerRoute(
    name: "Users",
    pattern: "{controller=User}/{action=Index}/{id?}"); // For MVC
app.MapControllerRoute(
    name: "Products",
    pattern: "{controller=Product}/{action=Index}/{id?}"); // For MVC
app.MapControllerRoute(
    name: "Categories",
    pattern: "{controller=Category}/{action=Index}/{id?}"); // For MVC

app.MapControllers(); // For Web API

app.Run();
