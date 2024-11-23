using Serilog;
using lr13.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Налаштування Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // Мінімальний рівень логування
    .WriteTo.Console() // Логування в консоль
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Логування у файл з розбивкою за днями
    .CreateLogger();

builder.Host.UseSerilog(); // Інтеграція Serilog з ASP.NET Core

// Додаємо служби
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Конфігурація HTTP-запитів
if (!app.Environment.IsDevelopment())
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
    pattern: "{controller=Home}/{action=Index}/{id?}");

try
{
    Log.Information("Starting the application...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed!");
}
finally
{
    Log.CloseAndFlush();
}
