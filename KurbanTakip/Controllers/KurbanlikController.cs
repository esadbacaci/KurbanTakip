using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using KurbanTakip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KurbanTakip.Controllers
{
    [Authorize]
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
		public IActionResult KurbanlikSil(int id)
		{
			var values = sm.TGetById(id);
			sm.TDelete(values);
			return RedirectToAction("KurbanlikListe", "Kurbanlik");
		}
		[HttpGet]
		public IActionResult KurbanlikGuncelle(int id)
		{
			var carivalue = sm.TGetById(id);
			return View(carivalue);
		}
		[HttpPost]
		public IActionResult KurbanlikGuncelle(Stok p, int Id)
		{
			var stokToUpdate = sm.TGetById(Id);

			if (stokToUpdate != null)
			{
				stokToUpdate.Ad = p.Ad;
				stokToUpdate.Kod = p.Kod;
				stokToUpdate.HisseAdet = p.HisseAdet;
				stokToUpdate.HisseFiyat = p.HisseFiyat;
				stokToUpdate.Kilo = p.Kilo;
				stokToUpdate.Yas = p.Yas;
				stokToUpdate.KupeNo = p.KupeNo;
				stokToUpdate.Cins=p.Cins;

				sm.TUpdate(stokToUpdate);
			}
			return RedirectToAction("KurbanlikListe", "Kurbanlik");
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
