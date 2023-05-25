using Microsoft.AspNetCore.Mvc;

namespace KurbanTakip.Controllers
{
    public class KasaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
