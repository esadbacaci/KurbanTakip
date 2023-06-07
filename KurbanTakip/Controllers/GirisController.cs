using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using KurbanTakip.Models;

namespace KurbanTakip.Controllers
{
	[Authorize]
	public class GirisController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;

		public GirisController(SignInManager<AppUser> signInManager)
		{
			_signInManager = signInManager;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Index(UserSignInViewModel p)
		{
			if (ModelState.IsValid)
			{
				//cookie hatırla ve 5 kez yanlışta ban
				var result = await _signInManager.PasswordSignInAsync(p.username, p.password, true, false);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre."); // Hata mesajını ekle
					return View(p); // Hatalı girişle birlikte giriş sayfasını tekrar yükle
				}
			}
			return View();

		}
		[HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Giris");
        }

    }
}
