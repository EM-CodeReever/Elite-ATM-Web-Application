using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EliteATMWebApp.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<EliteATMDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EliteATMDBContext") ?? throw new InvalidOperationException("Connection string 'EliteATMDBContext' not found.")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();