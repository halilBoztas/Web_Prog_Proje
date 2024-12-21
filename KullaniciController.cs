using Microsoft.AspNetCore.Mvc;
using ProjeCore.Models;

namespace ProjeCore.Controllers
{
	public class KullaniciController : Controller
	{
		Context c = new Context();
		public IActionResult Index()
		{
			var degerler = c.Kullanicis.ToList();
			return View(degerler);
		}

		[HttpGet] // SUNUCUDAN VERİ İSTER OKUMAK İÇİN
		public IActionResult YeniKullanici()
		{
			return View();
		}
		[HttpPost] // SUNUCUYA VERİ GÖNDERİR EKLEME VE GÜNCELLEME İŞLEMİ İÇİN
		public IActionResult YeniKullanici(Kullanici k)
		{
			c.Kullanicis.Add(k);
			c.SaveChanges();
			return RedirectToAction("GirisYap","Login");
		}


	}
}
