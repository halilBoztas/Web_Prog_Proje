using Microsoft.AspNetCore.Mvc;
using ProjeCore.Models;

namespace ProjeCore.Controllers
{
    public class RandevController : Controller
    {
        Context c = new Context();
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Al()
        {
            // Birimler listesini çekiyoruz
            ViewBag.Birimler = c.Birims.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult GetPersonels(int birimID)
        {
            var personeller = c.Personels.Where(x => x.BirimID == birimID).ToList();
            return Json(personeller);
        }


        [HttpPost]
        public IActionResult Al(Randev randevu)
        {
            var randevuVar = c.Randevs.Any(x => x.PersonelID == randevu.PersonelID && x.Tarih.Date == randevu.Tarih.Date && x.Saat == randevu.Saat);

            if (randevuVar)
            {
                TempData["ErrorMessage"] = "Bu tarih ve saatte randevu alınmıştır.";
                return RedirectToAction("Al");
            }

            c.Randevs.Add(randevu);
            c.SaveChanges();

            TempData["SuccessMessage"] = "Randevunuz başarıyla alındı.";
            return RedirectToAction("Index", "Kullanici");
        }



    }
}
