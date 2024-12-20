using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjeCore.Models;

namespace ProjeCore.Controllers
{
    public class PersonelimController : Controller
    {
        Context c = new Context();
        public IActionResult Index()
        {
            var deger = c.Personels.Include(x => x.Birim).ToList();
            return View(deger);
        }
        [HttpGet]
        public IActionResult YeniPersonel()
        {
            List<SelectListItem> degerler = (from x in c.Birims.ToList()
                                             select new SelectListItem
                                             {
                                                 Text=x.BirimAd,
                                                 Value=x.BirimID.ToString()
                                             }).ToList();
            ViewBag.dgr=degerler;
            return View();
        }
        [HttpPost]
        public IActionResult YeniPersonel(Personel p)
        {
            var per = c.Birims.Where(x => x.BirimID == p.Birim.BirimID).FirstOrDefault();
            p.Birim = per;
            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult PersonelSil(int id)
        {
            var per = c.Personels.Find(id);
            c.Personels.Remove(per);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult PersonelGetir(int id)
        {
            var personel = c.Personels.Find(id);

            List<SelectListItem> degerler = (from x in c.Birims.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.BirimAd,
                                                 Value = x.BirimID.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;

            return View("PersonelGetir", personel);  // depGetir View e dep değerini render eder
        }

        public IActionResult PersonelGuncelle(Personel p)
        {
            var per = c.Personels.Include(x => x.Birim).FirstOrDefault(x => x.PersonelID == p.PersonelID);

            if (per == null)
            {
                return NotFound(); // Personel bulunamazsa hata döndür
            }

            per.Ad = p.Ad;
            per.Soyad = p.Soyad;
            per.Sehir = p.Sehir;

            // Eğer BirimID null değilse güncelle
            if (p.BirimID != 0)
            {
                per.BirimID = p.BirimID;
            }

            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
