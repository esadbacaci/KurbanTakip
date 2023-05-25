using DataAccessLayer.Concrete;
using KurbanTakip.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KurbanTakip.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
			Context c = new Context();
			ViewBag.cari = c.Carikarts.Count().ToString();
			ViewBag.stok = c.Stoks.Count().ToString();

			return View();
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