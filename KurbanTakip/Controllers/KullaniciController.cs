using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using KurbanTakip.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace KurbanTakip.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KullaniciController : Controller
    {
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;
        AppUserManager apm = new AppUserManager(new EfAppUserRepository());
		public KullaniciController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}
		public async Task<IActionResult> Index()
		{
			var users = await _userManager.Users.ToListAsync();
			var roles = await _roleManager.Roles.ToListAsync();
			var clients = new List<AppUser>();

			foreach (var user in users)
			{
				var userRoles = await _userManager.GetRolesAsync(user);
				if (userRoles.Any())
				{
					clients.Add(user);
				}
			}

			return View(clients);
		}

		[HttpGet]
		public IActionResult KullaniciEkle()
		{
			return View();
		}
		[HttpPost]
        public async Task<IActionResult> KullaniciEkle(UserSignUpViewModel p)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = p.UserName,
                };

                var result = await _userManager.CreateAsync(user, p.Password);
                await _userManager.AddToRoleAsync(user, "User");
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Kullanici");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(p);
        }
		[HttpGet]
		public IActionResult AdminEkle()
		{
			return View();
		}
        [HttpPost]
        public async Task<IActionResult> AdminEkle(UserSignUpViewModel p)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    UserName = p.UserName,
                };
                var result = await _userManager.CreateAsync(user, p.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                    return RedirectToAction("Index", "Kullanici");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(p);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
		}
		public IActionResult KullaniciSil(int id)
		{
			var values = apm.TGetById(id);
			apm.TDelete(values);
			return RedirectToAction("Index", "Kullanici");

		}
	}
}
