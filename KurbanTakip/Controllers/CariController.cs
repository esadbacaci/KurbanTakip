using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using KurbanTakip.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace KurbanTakip.Controllers
{
    public class CariController : Controller
    {
        CarikartManager cm = new CarikartManager(new EfCarikartRepository());
        StokManager sm = new StokManager(new EfStokRepository());
        HissecarikartManager hcm = new HissecarikartManager(new EfHissecarikartRepository());
        CariIslemManager cim = new CariIslemManager(new EfCariIslemRepository());
        KasaManager km=new KasaManager(new EfKasaRepository()); 

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
                cariToUpdate.Ulke = p.Ulke;
                cariToUpdate.Il = p.Il;
                cariToUpdate.Ulke = p.Ilce;
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
            t.Id= 0;

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
        public IActionResult CariIslem(int id,Hissecarikart h,Cariislem c)
		{
			var stoks = sm.GetList();
            var caris = hcm.GetList();
			var values = cm.GetList();
			var cislem = cim.GetHisseCariIslemById(id);
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
		public IActionResult CariIslemTahsilat(Cariislem c,Kasa k)
		{
			c.Id = 0;
			cim.TAdd(c);

            k.GirisMi = true;
            k.CariIslemId = c.Id;
            k.Tutar = c.Alacak;
            km.TAdd(k);
			return RedirectToAction("CariListe", "Cari");
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
            return RedirectToAction("CariListe", "Cari");
        }
    }
}
