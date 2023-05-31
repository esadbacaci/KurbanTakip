using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using KurbanTakip.Models;
using Microsoft.AspNetCore.Mvc;

namespace KurbanTakip.Controllers
{
    public class KurbanlikController : Controller
    {
        StokManager sm = new StokManager(new EfStokRepository());
        CarikartManager cm = new CarikartManager(new EfCarikartRepository());
        HissecarikartManager hcm = new HissecarikartManager(new EfHissecarikartRepository());
        CariIslemManager cim=new CariIslemManager(new EfCariIslemRepository());
        Context c=new Context();
        public IActionResult KurbanlikListe()
        {
			var stoks = sm.GetList();
			var caris = hcm.GetList();
			var values = cm.GetList();
            var viewModel = new CariStokModel
            {
                Carikarts = values,
                Stoks = stoks,
                Hissecarikarts = caris
			};
			//ViewBag.hisse = caris.Where(c=> c.StokId).Count();//bu nedir
			return View(viewModel);
		}
        [HttpGet]
		public IActionResult KurbanlikListeId(int stokId)
		{
            var stoks = sm.GetKurbanlikById(stokId);
            var caris = hcm.GetList();
            var values = cm.GetList();
            var viewModel = new CariStokModel
            {
                Carikarts = values,
                Stoks = stoks,
                Hissecarikarts = caris
            };        
			return PartialView("_KurbanlikDetaylar", viewModel);
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
        public IActionResult KurbanlikDetay(int id)
        {
            var stoks = sm.GetList();
            var caris = hcm.GetHisseCarikartByStokId(id);
            var values = cm.GetList();
            var cislem = cim.GetList();
            var viewModel = new CariStokModel
            {
                Carikarts = values,
                Stoks = stoks,
                Hissecarikarts = caris,
                Cariislems = cislem
            };
          
            return View(viewModel);
           
        }

    }
}
