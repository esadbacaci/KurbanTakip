using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KurbanTakip.Controllers
{
	[Authorize]
	public class ProfilController : Controller
	{
		AdminManager am = new AdminManager(new EfAdminRepository());

		[HttpGet]
		public IActionResult Index(int id)
		{
			var adminvalue = am.TGetById(id);
			return View(adminvalue);
		}

		[HttpPost]
		public IActionResult Index(Admin p, int Id)
		{
			var profilToUpdate = am.TGetById(Id);

			if (profilToUpdate != null)
			{
				profilToUpdate.AdminName = p.AdminName;
				profilToUpdate.AdminPassword = p.AdminPassword;

				am.TUpdate(profilToUpdate);
			}
			return RedirectToAction("Index", "Home");
		}

	}
}
