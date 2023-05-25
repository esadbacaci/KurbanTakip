using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace KurbanTakip.Controllers
{
    public class KurbanlikController : Controller
    {
        StokManager sm = new StokManager(new EfStokRepository());
        public IActionResult KurbanlikListe()
        {
			var values = sm.GetList();
            return View(values);
		}
        [HttpGet]
		public IActionResult KurbanlikListeId(int stokId)
		{
			var values = sm.GetKurbanlikById(stokId);
			return PartialView("_KurbanlikDetaylar", values);
		}
		[HttpGet]
        public IActionResult KurbanlikEkle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult KurbanlikEkle(Stok p)
        {
            sm.TAdd(p);
            return RedirectToAction("KurbanlikListe","Kurbanlik");
        }
        [HttpGet]
        public IActionResult KurbanlikDetay()
        {
            return View();
        }

    }
}
