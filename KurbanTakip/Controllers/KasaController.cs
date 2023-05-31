using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace KurbanTakip.Controllers
{
    public class KasaController : Controller
    {
		CarikartManager cm = new CarikartManager(new EfCarikartRepository());
		StokManager sm = new StokManager(new EfStokRepository());
		HissecarikartManager hcm = new HissecarikartManager(new EfHissecarikartRepository());
		CariIslemManager cim = new CariIslemManager(new EfCariIslemRepository());

		public IActionResult Index()
        {
            return View();
        }
    }
}
