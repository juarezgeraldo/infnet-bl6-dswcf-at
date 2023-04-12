using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using PaisMVC.Models;
using System.Diagnostics;

namespace PaisMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly string url = "https://paises.azurewebsites.net/api/";

        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ActionResult> Index()
        {
            var estatistica = await $"{url}estatistica".GetJsonAsync<Estatistica>();
            return View(estatistica);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}