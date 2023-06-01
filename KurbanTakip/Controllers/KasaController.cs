using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using KurbanTakip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KurbanTakip.Controllers
{
    [Authorize]

    public class KasaController : Controller
    {
        KasaManager km = new KasaManager(new EfKasaRepository());
        public IActionResult Index()
        {
            var values=km.GetList();
            return View(values);
        }
		public IActionResult KasaSil(int id)
		{
			var values = km.TGetById(id);
			km.TDelete(values);
			return RedirectToAction("Index", "Kasa");
		}
		[HttpGet]
		public IActionResult KasaGuncelle(int id)
		{
			var kasavalue = km.TGetById(id);
			return View(kasavalue);
		}
		[HttpPost]
		public IActionResult KasaGuncelle(Kasa k, int Id)
		{
			var kasaToUpdate = km.TGetById(Id);

			if (kasaToUpdate != null)
			{
				kasaToUpdate.Tutar = k.Tutar;
				kasaToUpdate.Aciklama = k.Aciklama;
				kasaToUpdate.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());

				km.TUpdate(kasaToUpdate);
			}
			return RedirectToAction("Index", "Kasa");
		}
	}
}
