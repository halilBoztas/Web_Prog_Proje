using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProjeCore.Migrations;
using ProjeCore.Models;
using System.Security.Claims;

namespace ProjeCore.Controllers
{
    public class LoginController : Controller
    {
        Context c = new Context();
        [HttpGet]
        public IActionResult GirisYap()
        {
            return View();
        }

        public async Task<IActionResult> GirisYap(Admin p,Kullanici k )
        {
            var bilgiler = c.Admins.FirstOrDefault(x=>x.Kullanici == p.Kullanici &&
                x.Sifre == p.Sifre);

            var bilgi = c.Kullanicis.FirstOrDefault(x => x.KullaniciEmail == p.Kullanici &&
                x.KullaniciSifre == p.Sifre);

            if (bilgiler != null) {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,p.Kullanici)
                };
                var useridentity = new ClaimsIdentity(claims,"Login");

                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index","Personelim");

            }
            else if(bilgi != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,p.Kullanici)
                };
                var useridentity = new ClaimsIdentity(claims, "Kullanici");

                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);

                TempData["WelcomeMessage"] = "Hoşgeldin, " + bilgi.KullaniciAd + " " + bilgi.KullaniciSoyad;

                return RedirectToAction("Index", "Kullanici");

            }
            return View();
        }
    }
}
