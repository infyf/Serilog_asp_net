using Serilog;
using lr13.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// ������������ Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug() // ̳�������� ����� ���������
    .WriteTo.Console() // ��������� � �������
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // ��������� � ���� � ��������� �� �����
    .CreateLogger();

builder.Host.UseSerilog(); // ���������� Serilog � ASP.NET Core

// ������ ������
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ������������ HTTP-������
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
