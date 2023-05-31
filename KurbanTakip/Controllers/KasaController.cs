using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using KurbanTakip.Models;
using Microsoft.AspNetCore.Mvc;

namespace KurbanTakip.Controllers
{
    public class KasaController : Controller
    {
		CarikartManager cm = new CarikartManager(new EfCarikartRepository());
		StokManager sm = new StokManager(new EfStokRepository());
		HissecarikartManager hcm = new HissecarikartManager(new EfHissecarikartRepository());
		CariIslemManager cim = new CariIslemManager(new EfCariIslemRepository());
        KasaManager km = new KasaManager(new EfKasaRepository());

        public IActionResult Index()
        {
            var values=km.GetList();
            return View(values);
        }
    }
}
