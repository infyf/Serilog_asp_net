using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Diagnostics;
using lr13.Models;

namespace lr13.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            Log.Information("User with IP {IP} accessed the Index page.", ipAddress);


            ViewData["UserIp"] = ipAddress;
            return View();
        }

        public IActionResult Privacy()
        {
            Log.Information("User with IP {IP} accessed the Privacy page.", HttpContext.Connection.RemoteIpAddress);
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            Log.Error("An error occurred on the Error page with Request ID: {RequestId} from IP {IP}", requestId, HttpContext.Connection.RemoteIpAddress);
            return View(new ErrorViewModel { RequestId = requestId });
        }

    }
}
