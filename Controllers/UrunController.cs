using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MVCStokProject.Models.Entity;

namespace MVCStokProject.Controllers
{
    public class UrunController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        // GET: Urun
        public ActionResult Index()
        {
            var degerler = db.TBLURUNLER.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            //Kategorileri DB'den dropdownlist' çekme işlemi

            List<SelectListItem> degerler = (from kategori in db.TBLKATEGRORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = kategori.kategoriAd,
                                                 Value = kategori.kategoriid.ToString()

                                             }).ToList();


            //ViewBag dinamik bir nesne olup, controller'dan view'a veri taşımak için kullanılır.
            //ViewBag.dgr = degerler;: degerler listesini ViewBag.dgr olarak view'a taşır.
            //Bu, view içinde ViewBag.dgr olarak erişilebilir hale gelir ve
            //dropdown list'i oluşturmak için kullanılabilir
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult YeniUrun(TBLURUNLER p1)
        {
            var kategori = db.TBLKATEGRORILER.Where(m => m.kategoriid == p1.TBLKATEGRORILER.kategoriid).FirstOrDefault();
            p1.TBLKATEGRORILER = kategori;
            db.TBLURUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            if (urun != null)
            {
                db.TBLURUNLER.Remove(urun);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> degerler = (from kategori in db.TBLKATEGRORILER
                                             select new SelectListItem
                                             {
                                                 Text = kategori.kategoriAd,
                                                 Value = kategori.kategoriid.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View("UrunGetir", urun);
        }

        public ActionResult UrunDuzenle(TBLURUNLER p)
        {

            if (p == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var urun = db.TBLURUNLER.Find(p.urunid);
            if (urun == null)
            {
                return HttpNotFound();
            }

            // Diğer işlemler burada yapılır
            try
            {
                urun.urunAd = p.urunAd;
                urun.urunMarka = p.urunMarka;
                urun.urunFiyat = p.urunFiyat;
                urun.Stok = p.Stok;
                var kategori = db.TBLKATEGRORILER.Where(m => m.kategoriid == p.TBLKATEGRORILER.kategoriid).FirstOrDefault();
                urun.urunKategori = kategori.kategoriid;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Hata mesajını loglayabilir veya kullanıcıya uygun bir mesaj döndürebilirsiniz.
                Console.WriteLine(ex.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Bir hata oluştu.");
            }

            return RedirectToAction("Index");

        }
    }
}