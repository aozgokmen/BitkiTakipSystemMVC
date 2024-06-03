using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitkiTakipSystemMVC.Models;
namespace BitkiTakipSystemMVC.Controllers
{
    public class YoneticiController : Controller
    {
        // GET: Yonetici
        PlantTakipDbEntities entity = new PlantTakipDbEntities();

        public ActionResult Index()
        {
            int yetkiturId = Convert.ToInt32(Session["PersonelAuthorizationId"]);
            if (yetkiturId == 1)
            {
                int locationId = Convert.ToInt32(Session["PersonelLocationId"]);
                var location = (from l in entity.Locations where l.locationId == locationId select l).FirstOrDefault();

                ViewBag.LocationAd = location.locationAd;

                return View();
            }
            else
            {

            }
            {
                return RedirectToAction("Index", "Login");
            }

        }
        public ActionResult Assign()
        {
            int yetkiturId = Convert.ToInt32(Session["PersonelAuthorizationId"]);
            if (yetkiturId == 1)
            {
                int locationId = Convert.ToInt32(Session["PersonelLocationId"]);

                var personeller = (from p in entity.Personels where p.PersonelLocationId == locationId && p.PersonelAuthorizationId == 2 select p).ToList();

                ViewBag.personel = personeller;

                var location = (from l in entity.Locations where l.locationId == locationId select l).FirstOrDefault();

                ViewBag.LocationAd = location.locationAd;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public ActionResult Assign(FormCollection formCollection)
        {
            string isBaslik = formCollection["isBaslik"];
            string isAciklama = formCollection["isAciklama"];
            int secilenPersonelId = Convert.ToInt32(formCollection["selectPer"]);

            Works yeniIs = new Works();

            yeniIs.workName = isBaslik;
            yeniIs.workAciklama = isAciklama;
            yeniIs.workfarmerId = secilenPersonelId;
            yeniIs.workprogressId = 1;
            yeniIs.iletilenTarih = DateTime.Now;

            using (var transaction = entity.Database.BeginTransaction())
            {
                try
                {
                    // worksId için son değeri al ve 1 artır
                    int lastWorksId = entity.Works.Max(w => (int?)w.worksId) ?? 0;
                    yeniIs.worksId = lastWorksId + 1;

                    entity.Works.Add(yeniIs);
                    entity.SaveChanges();

                    transaction.Commit(); // İşlemleri onayla
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Hata olursa işlemleri geri al
                                            // Hata yönetimi: Loglama yapabilir veya kullanıcıya bilgi verebilirsiniz
                    return RedirectToAction("Error", "Yonetici");
                }
            }

            return RedirectToAction("List", "Yonetici");
        }
        public ActionResult List()
        {
            int yetkiturId = Convert.ToInt32(Session["PersonelAuthorizationId"]);
            if (yetkiturId == 1)
            {
                int locationId = Convert.ToInt32(Session["PersonelLocationId"]);

                var personeller = (from p in entity.Personels where p.PersonelLocationId == locationId && p.PersonelAuthorizationId == 2 select p).ToList();

                ViewBag.personel = personeller;

                var location = (from l in entity.Locations where l.locationId == locationId select l).FirstOrDefault();

                ViewBag.LocationAd = location.locationAd;

            }
            return View();

        }
        [HttpPost]
        public ActionResult List(int selectPer)
        {
            var secilenpersonel = (from p in entity.Personels where p.PersonelId == selectPer select p).FirstOrDefault();

            if (secilenpersonel != null)
            {
                ViewBag.secilenPersonel = secilenpersonel;

                TempData["secilenPersonel"] = secilenpersonel;

                return RedirectToAction("Preview", "Yonetici");


            }
            return View();
        }

        public ActionResult Preview()
        {
            int yetkiturId = Convert.ToInt32(Session["PersonelAuthorizationId"]);
            if (yetkiturId == 1)
            {
                Personels secilen_personels = TempData["secilenPersonel"] as Personels;

                try
                {
                    var works = (from w in entity.Works where w.workfarmerId == secilen_personels.PersonelId select w).ToList();
                    ViewBag.works = works;
                    ViewBag.personel = secilen_personels;

                    return View();
                }
                catch (Exception ex)
                {
                    return RedirectToAction("List", "Yonetici");
                }
            }
            return View();

        }
        public ActionResult Delete(int worksId)
        {
            var work = entity.Works.Find(worksId);
            if (work != null)
            {
                entity.Works.Remove(work);
                entity.SaveChanges();
                TempData["SuccessMessage"] = "İş başarıyla silindi.";
            }
            else
            {
                TempData["ErrorMessage"] = "İş bulunamadı.";
            }
            return RedirectToAction("Preview", "Yonetici");
        }


    }
}