using EntityLayer.Concrete;
using KurbanTakip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KurbanTakip.Controllers
{
    [AllowAnonymous]
    public class KayitController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public KayitController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel p)
        {

            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = p.UserName
                };
                var result = await _userManager.CreateAsync(user, p.Password);
                await _userManager.AddToRoleAsync(user, "Admin");
                if (result.Succeeded)
                {
                    // await _userManager.AddToRoleAsync(user, "Admin");
                    return RedirectToAction("Index", "Giris");

                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(p);
        }
    }
}
