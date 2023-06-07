using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using KurbanTakip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KurbanTakip.Controllers
{
    [Authorize]
    public class ProfilController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfilController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var model = new ProfilViewModel
            {
                KullaniciAdi = user.UserName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ProfilViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                // Şifreyi değiştirme işlemi
                var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.EskiSifre, model.YeniSifre);

                if (changePasswordResult.Succeeded)
                {
                    // Şifre değiştirme başarılı
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Şifre değiştirme hatası
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // Şifre değiştirme başarısız veya model geçersiz, view'i tekrar göster
            return View(model);
        }


    }
}
