using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using KurbanTakip.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace KurbanTakip.Controllers
{
    public class CariController : Controller
    {
        CarikartManager cm = new CarikartManager(new EfCarikartRepository());
		StokManager sm = new StokManager(new EfStokRepository());
		HissecarikartManager hcm = new HissecarikartManager(new EfHissecarikartRepository());

		public IActionResult CariListe()
        {
            var values= cm.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CariEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CariEkle(Carikart p)
        {
            cm.TAdd(p);
            return RedirectToAction("CariListe","Cari");
        }
        [HttpGet]
        public IActionResult CariKurbanEkle(int id)
        {
			var values = cm.GetCariById(id);
			var stoks = sm.GetList();
			var viewModel = new CariStokModel
			{
				Carikarts = values,
				Stoks = stoks
			};

			return View(viewModel);
		}
        [HttpPost]
        public IActionResult CariKurbanEkle(Hissecarikart p)
        {
			int selectedStokId = p.StokId; // Seçilen StokId değerini alın
			p.StokId = selectedStokId;
			p.Tarih = DateTime.Now; // Geçerli tarihi atayın
			p.VekaletAlindiMi = Request.Form["VekaletAlindiMi"] == "on";
			hcm.TAdd(p);
            return RedirectToAction("CariListe","Cari");
		}
        [HttpGet]
        public IActionResult CariDetay(int id)
        {
            var values =hcm.TGetById(id);
            return View(values);
        }
        [HttpGet]
        public IActionResult CariIslem()
        {
            return View();
        }
    }
}
