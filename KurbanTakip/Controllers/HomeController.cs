using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using KurbanTakip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KurbanTakip.Controllers
{
    [Authorize]

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
            int toplamHisseAdet = c.Stoks.Sum(s => s.HisseAdet);

            ViewBag.toplamhisseadet = toplamHisseAdet;

            var query = from item in c.Hissecarikarts
                        group item by item.StokId into g
                        select new
                        {
                            Count = g.Count(),
                            StokId = g.Key
                        };

            int toplamDolanHisse = query.Sum(item => item.Count);
            ViewBag.toplamDolanHisse = toplamDolanHisse;
            ViewBag.toplamBosHisse = toplamHisseAdet - toplamDolanHisse;
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }
        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}