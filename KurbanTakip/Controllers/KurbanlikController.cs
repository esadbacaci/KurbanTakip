using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using KurbanTakip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
			//int maxSiraNo = c.Stoks.Max(stok => stok.SiraNo);
			//p.SiraNo = maxSiraNo + 1;
      
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
		//[HttpPost]
		//public IActionResult KurbanlikGuncelle(Stok p, int Id)
		//{
		//	var stokToUpdate = sm.TGetById(Id);

		//	if (stokToUpdate != null)
		//	{
		//		stokToUpdate.Ad = p.Ad;
		//		stokToUpdate.Kod = p.Kod;
		//		stokToUpdate.HisseAdet = p.HisseAdet;
		//		stokToUpdate.HisseFiyat = p.HisseFiyat;
		//		stokToUpdate.Kilo = p.Kilo;
		//		stokToUpdate.Yas = p.Yas;
		//		stokToUpdate.KupeNo = p.KupeNo;
		//		stokToUpdate.Cins=p.Cins;

		//		sm.TUpdate(stokToUpdate);
		//	}
		//	return RedirectToAction("KurbanlikListe", "Kurbanlik");
		//      }   
		[HttpPost]
		public IActionResult KurbanlikGuncelle(Stok p, int Id)
		{
				var stokToUpdate = c.Stoks.FirstOrDefault(s => s.Id == Id);
				var hisseCarikart = c.Hissecarikarts.FirstOrDefault(h => h.StokId == Id);

				if (stokToUpdate != null)
				{
					stokToUpdate.Ad = p.Ad;
					stokToUpdate.Kod = p.Kod;
					stokToUpdate.HisseAdet = p.HisseAdet;
					stokToUpdate.HisseFiyat = p.HisseFiyat;
					stokToUpdate.Kilo = p.Kilo;
					stokToUpdate.Yas = p.Yas;
					stokToUpdate.KupeNo = p.KupeNo;
					stokToUpdate.Cins = p.Cins;

					if (hisseCarikart != null)
					{
						hisseCarikart.HisseTutar = p.HisseFiyat; // HisseTutar alanını güncelle
					}

					c.SaveChanges(); // Güncelleme işlemlerini kaydet
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
        public IActionResult HissedarSil(int id)
        {
			var values = hcm.TGetById(id);
			hcm.TDelete(values);
			return RedirectToAction("KurbanlikDetay", "Kurbanlik", new { id = values.StokId });

		}
		public IActionResult EtTeslimEt(int id, bool toggle = false)
		{
			var hissecarikart = c.Hissecarikarts.FirstOrDefault(hc => hc.Id == id);

			if (hissecarikart == null)
			{
				// Hissecarikart bulunamadı
				return NotFound();
			}

			// Toggle değerine göre "EtTeslimEdildiMi" değerini güncelleme
			hissecarikart.EtTeslimEdildiMi = toggle ? !hissecarikart.EtTeslimEdildiMi : true;

			try
			{
				// Değişikliği veritabanına kaydetme
				c.SaveChanges();

				return Json(new { success = true, message = toggle ? "Teslim Başarılı !" : "Et Teslim Durumu Geri Alındı!" });
			}
			catch (Exception ex)
			{
				// Hata durumunda
				return Json(new { success = false, message = "Bir hata oluştu: " + ex.Message });
			}
		}
		[HttpPost]
		public IActionResult VekaletDegistir(int id, bool toggle = false)
		{
			var hissecarikart = c.Hissecarikarts.FirstOrDefault(hc => hc.Id == id);

			if (hissecarikart == null)
			{
				// Hissecarikart bulunamadı
				return NotFound();
			}

			// Toggle değerine göre "VekaletAlindiMi" değerini güncelleme
			hissecarikart.VekaletAlindiMi = toggle ? !hissecarikart.VekaletAlindiMi : true;

			try
			{
				// Değişikliği veritabanına kaydetme
				c.SaveChanges();

				return Json(new { success = true, message = toggle ? "Vekalet Değiştirildi!" : "Vekalet Alındı!" });
			}
			catch (Exception ex)
			{
				// Hata durumunda
				return Json(new { success = false, message = "Bir hata oluştu: " + ex.Message });
			}
		}

	}
}
