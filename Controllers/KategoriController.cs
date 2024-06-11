using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCStokProject.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MVCStokProject.Controllers
{
    public class KategoriController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        
        // GET: Kategori
        public ActionResult Index(int sayfa=1)
        {
            // var degerler = db.TBLKATEGRORILER.ToList();
            var degerler = db.TBLKATEGRORILER.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }

        //herhangi bir post işlemi yapılmazsa sadece sayfayı görüntülemeli
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }

        //butona basana kadar kategori ekleme işini gerçekleştirmemeli
        [HttpPost]
        public ActionResult YeniKategori(TBLKATEGRORILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.TBLKATEGRORILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Sil(int id)
        {
            var kategori = db.TBLKATEGRORILER.Find(id);
            if(kategori != null)
            {
                db.TBLKATEGRORILER.Remove(kategori);
                db.SaveChanges();
            }
         
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var kategori = db.TBLKATEGRORILER.Find(id);
            return View("KategoriGetir",kategori);

        }

        public ActionResult Guncelle(TBLKATEGRORILER p1)
        {
            var kategori = db.TBLKATEGRORILER.Find(p1.kategoriid);
            kategori.kategoriAd = p1.kategoriAd;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}