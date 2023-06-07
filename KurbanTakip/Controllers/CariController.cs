using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using KurbanTakip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace KurbanTakip.Controllers
{
	[Authorize]

	public class CariController : Controller
	{
		CarikartManager cm = new CarikartManager(new EfCarikartRepository());
		StokManager sm = new StokManager(new EfStokRepository());
		HissecarikartManager hcm = new HissecarikartManager(new EfHissecarikartRepository());
		CariIslemManager cim = new CariIslemManager(new EfCariIslemRepository());
		KasaManager km = new KasaManager(new EfKasaRepository());
		Context c = new Context();

		public IActionResult CariListe()
		{
			var values = cm.GetList();
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
			return RedirectToAction("CariListe", "Cari");
		}
		[HttpGet]
		public IActionResult CariAyar()
		{
			var values = cm.GetList();
			return View(values);
		}
		public IActionResult CariSil(int id)
		{
			var values = cm.TGetById(id);
			cm.TDelete(values);
			return RedirectToAction("CariAyar", "Cari");
		}
		[HttpGet]
		public IActionResult CariGuncelle(int id)
		{
			var carivalue = cm.TGetById(id);
			return View(carivalue);
		}
		[HttpPost]
		public IActionResult CariGuncelle(Carikart p, int Id)
		{
			var cariToUpdate = cm.TGetById(Id);

			if (cariToUpdate != null)
			{
				cariToUpdate.AdSoyad = p.AdSoyad;
				cariToUpdate.Telefon = p.Telefon;
				cariToUpdate.Referans = p.Referans;
				cariToUpdate.Aciklama = p.Aciklama;
				cariToUpdate.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());

				cm.TUpdate(cariToUpdate);
			}
			return RedirectToAction("CariAyar", "Cari");
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
		public IActionResult CariKurbanEkle(Hissecarikart p, Cariislem t)
		{
			int selectedStokId = p.StokId; // Seçilen StokId değerini alın
			p.StokId = selectedStokId;
			p.Tarih = DateTime.Now;
			p.VekaletAlindiMi = Request.Form["VekaletAlindiMi"] == "on";
			p.Id = 0;

			t.Tarih = DateTime.Now;
			t.Alacak = 0;
			t.Aciklama = "Başarılı";
			t.Id = 0;

			hcm.TAdd(p);
			t.HisseCariKartId = p.Id;
			cim.TAdd(t);
			return RedirectToAction("CariListe", "Cari");
		}

		[HttpGet]
		public IActionResult CariDetay(int id)
		{
			var values = hcm.GetHisseCarikartById(id);
			return View(values);
		}
		[HttpGet]
		public IActionResult CariIslem(int id, Hissecarikart h, Cariislem c)
		{
			var stoks = sm.GetList();
			var caris = hcm.GetList();
			var values = cm.GetList();
			var cislem = cim.GetHisseCariIslemById(id);

			if (cislem.Count == 0)
			{
				// Eğer cislem listesi boş ise, hata yerine uygun bir durum sayfasına yönlendirme yapabilirsiniz.
				return RedirectToAction("CariIslemYok");
			}

			Cariislem tekCarti = cislem[0];
			var viewModel = new CariStokModel
			{
				Carikarts = values,
				Stoks = stoks,
				Hissecarikarts = caris,
				Cariislems = cislem,
				Cariislem = tekCarti
			};

			return View(viewModel);
		}

        [HttpPost]
        public IActionResult CariIslemTahsilat(Cariislem c, Kasa k)
        {
            c.Id = 0;
            cim.TAdd(c);

            k.GirisMi = true;
            k.CariIslemId = c.Id;
            k.Tutar = c.Alacak;
            km.TAdd(k);

            return RedirectToAction("CariIslem", "Cari", new { id = c.CariKartId });
        }

        [HttpPost]
		public IActionResult CariIslemOdeme(Cariislem c, Kasa k)
		{
			c.Id = 0;
			cim.TAdd(c);

			k.GirisMi = false;
			k.CariIslemId = c.Id;
			k.Tutar = c.Borc;
			km.TAdd(k);
            return RedirectToAction("CariIslem", "Cari", new { id = c.CariKartId });
        }

        [HttpPost]
		public IActionResult CariIslemBorclandir(Cariislem c)
		{
			c.Aciklama = "Borçlandırıldı";
			c.Id = 0;
			cim.TAdd(c);
            return RedirectToAction("CariIslem", "Cari", new { id = c.CariKartId });
        }
        [HttpPost]
		public IActionResult CariIslemAlacaklandir(Cariislem c)
		{
			c.Aciklama = "Alacaklarndırıldı";
			c.Id = 0;
			cim.TAdd(c);
            return RedirectToAction("CariIslem", "Cari", new { id = c.CariKartId });
		}
		public IActionResult CariDetaySil(int id)
		{
			var values = hcm.TGetById(id);
			hcm.TDelete(values);
			return RedirectToAction("CariListe", "Cari");
		}
		[HttpGet]
		public IActionResult CariIslemGuncelle(int id)
		{
			var carivalue = cim.TGetById(id);
			return View(carivalue);
		}
		//[HttpPost]
		//public IActionResult CariIslemGuncelle(Cariislem p, int Id)
		//{
		//	var cariIslemToUpdate = cim.TGetById(Id);
		//	if (cariIslemToUpdate != null)
		//	{
		//		cariIslemToUpdate.Aciklama = p.Aciklama;
		//		cariIslemToUpdate.Borc = p.Borc;
		//		cariIslemToUpdate.Alacak = p.Alacak;
		//		cariIslemToUpdate.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());

		//		cim.TUpdate(cariIslemToUpdate);
		//	}
		//	return RedirectToAction("CariListe", "Cari");
		//}
		[HttpPost]
		public IActionResult CariIslemGuncelle(Cariislem p, int Id)
		{
			var cariislem = c.Kasas.FirstOrDefault(h => h.CariIslemId == Id);
			var cariIslemToUpdate = cim.TGetById(Id);
			if (cariIslemToUpdate != null)
			{
				cariIslemToUpdate.Aciklama = p.Aciklama;
				cariIslemToUpdate.Borc = p.Borc;
				cariIslemToUpdate.Alacak = p.Alacak;
				cariIslemToUpdate.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
				if (cariislem != null)
				{
					cariislem.Tutar = p.Alacak; // HisseTutar alanını güncelle
				}

				c.SaveChanges(); // Güncelleme işlemlerini kaydet
				cim.TUpdate(cariIslemToUpdate);
			}
			return RedirectToAction("CariListe", "Cari");
		}
		public IActionResult CariIslemSil(int id)
        {
            var cariIslem = cim.TGetById(id);
            cim.TDelete(cariIslem);

            var remainingRecords = c.Cariislems.Any(c => c.CariKartId == cariIslem.CariKartId);
            if (remainingRecords)
            {
                return RedirectToAction("CariIslem", "Cari", new { id = cariIslem.CariKartId });
            }
            else
            {
                // No remaining records for the CariKartId, delete the corresponding Hissecarikart record and redirect to CariDetay
                var hisseCariKart = hcm.GetList().FirstOrDefault(h => h.Id == cariIslem.HisseCariKartId);
                if (hisseCariKart != null)
                {
                    hcm.TDelete(hisseCariKart);
                }
                return RedirectToAction("CariListe", "Cari");
            }
        }
        /*    kurbanlıklar tablosundan da silmemek için
         *    
         *   public IActionResult CariIslemSil(int id)
		{
			var values = cim.TGetById(id);
			cim.TDelete(values);

			var remainingRecords = c.Cariislems.Any(c => c.CariKartId == values.CariKartId);
			if (remainingRecords)
			{
				return RedirectToAction("CariIslem", "Cari", new { id = values.CariKartId });
			}
			else
			{
				// Handle the scenario when there are no remaining records for the CariKartId
				return RedirectToAction("CariListe", "Cari"); // Redirect to a different action or display a message
			}
		}*/
        public IActionResult CariIslemYok()
		{
			return View();
		}

	}
}
