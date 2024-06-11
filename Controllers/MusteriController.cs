using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStokProject.Models.Entity;


namespace MVCStokProject.Controllers
{
    public class MusteriController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        // GET: Musteri
        public ActionResult Index(string p)
        {
            var degerler = from d in db.TBLMUSTERILER select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.musteriAd.Contains(p));
                
            }

            return View(degerler.ToList());
        }


        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }


        [HttpPost]
        public ActionResult YeniMusteri(TBLMUSTERILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TBLMUSTERILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Sil(int id)
        {
            var musteri = db.TBLMUSTERILER.Find(id);
            if(musteri != null)
            {
                db.TBLMUSTERILER.Remove(musteri);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var musteri = db.TBLMUSTERILER.Find(id);
            return View("MusteriGetir", musteri);
        }

        public ActionResult MusteriDuzenle(TBLMUSTERILER paramMusteri)
        {
            var musteri = db.TBLMUSTERILER.Find(paramMusteri.musteriid);
            musteri.musteriAd = paramMusteri.musteriAd;
            musteri.musteriSoyad = paramMusteri.musteriSoyad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}