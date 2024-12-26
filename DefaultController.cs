using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjeCore.Models;

namespace ProjeCore.Controllers
{
	[Authorize]
	public class DefaultController : Controller
    {
        Context c = new Context();
        public IActionResult Index()
        {
            var degerler = c.Birims.ToList();
            return View(degerler);
        }

        [HttpGet] // SUNUCUDAN VERİ İSTER OKUMAK İÇİN
        public IActionResult YeniBirim()
        {
            return View();
        }
        [HttpPost] // SUNUCUYA VERİ GÖNDERİR EKLEME VE GÜNCELLEME İŞLEMİ İÇİN
        public IActionResult YeniBirim(Birim b)
        {
            c.Birims.Add(b);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult BirimSil(int id)
        {
            var dep = c.Birims.Find(id);
            c.Birims.Remove(dep);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult BirimGetir(int id)
        {
            var depart = c.Birims.Find(id);
            return View("BirimGetir", depart);  // depGetir View e dep değerini render eder
        }

        public IActionResult BirimGuncelle(Birim d)
        {

            var dep = c.Birims.Find(d.BirimID);
            dep.BirimAd = d.BirimAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult BirimDetay(int id)
        {
            var degerler = c.Personels.Where(x=>x.BirimID == id).ToList();

            var brmad = c.Birims.Where(x=>x.BirimID==id).Select(y=>y.BirimAd).FirstOrDefault();
            ViewBag.brm=brmad;

            return View(degerler);
        }
    }
}
