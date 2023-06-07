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

	public class KasaController : Controller
	{
		KasaManager km = new KasaManager(new EfKasaRepository());
		CariIslemManager cim = new CariIslemManager(new EfCariIslemRepository());
		CarikartManager cm = new CarikartManager(new EfCarikartRepository());
		Context c = new Context();
		public IActionResult Index()
		{
			var ckart = cm.GetList();
			var values = km.GetList();
			var cislem = cim.GetList();
			var viewModel = new CariStokModel
			{
				Kasas = values,
				Cariislems = cislem,
				Carikarts=ckart
			};
			return View(viewModel);
		}
		public IActionResult KasaSil(int id)
		{
			try
			{
				var kasa = km.TGetById(id);

				// İlgili Kasa nesnesinin CariislemId değerini alın
				var cariislemId = kasa.CariIslemId;

				if (cariislemId != null)
				{
					// Cariislem tablosundan ilgili Cariislem nesnesini bulun
					var cariislem = c.Cariislems.FirstOrDefault(c => c.Id == cariislemId);
					if (cariislem != null)
					{
						cim.TDelete(cariislem);
					}
				}

				// Kasa nesnesini sil
				km.TDelete(kasa);

				return RedirectToAction("Index","Kasa");
			}
			catch (DbUpdateConcurrencyException ex)
			{
				// Hata durumunda yapılacak işlemleri burada ele alın
				// Örneğin, hata mesajını göstermek veya yönlendirme yapmak gibi
				ModelState.AddModelError("", "Bir hata oluştu. Lütfen tekrar deneyin.");
				return RedirectToAction("Index","Kasa");
			}
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

				// İlgili işlemi bul ve güncelle
				var cariIslem = cim.TGetById(kasaToUpdate.CariIslemId);
				if (cariIslem != null)
				{
					if (k.Tutar.HasValue) // nullable kontrolü yapın
					{
						cariIslem.Alacak = k.Tutar.Value; // null değilse değeri alın
					}
					cim.TUpdate(cariIslem);
				}
			}
			return RedirectToAction("Index", "Kasa");
		}

	}
}
