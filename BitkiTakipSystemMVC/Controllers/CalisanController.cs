using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using BitkiTakipSystemMVC.Models;

namespace BitkiTakipSystemMVC.Controllers
{

    public class jobstatus
    {
        public string workName { get; set; }
        public string workAciklama { get; set; }

        public DateTime? iletilenTarih { get; set; }

        public DateTime? yapilanTarih { get; set; }

        public string workprogressAd { get; set; }


    }


    public class CalisanController : Controller
    {
        // GET: Personel
        PlantTakipDbEntities entity = new PlantTakipDbEntities();

        public ActionResult Index()
        {
            int yetkiturId = Convert.ToInt32(Session["PersonelAuthorizationId"]);
            if (yetkiturId == 2)
            {
                int locationId = Convert.ToInt32(Session["PersonelLocationId"]);
                var location = (from l in entity.Locations where l.locationId == locationId select l).FirstOrDefault();

                ViewBag.LocationAd = location.locationAd;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        public ActionResult Done()
        {
            int yetkiturId = Convert.ToInt32(Session["PersonelAuthorizationId"]);
           
            if (yetkiturId == 2)
            {
                int personelId = Convert.ToInt32(Session["PersonelID"]);
                var works = (from w in entity.Works where w.workfarmerId == personelId && w.workprogressId == 1 select w ).ToList().
                    OrderByDescending(w=>w.iletilenTarih);
                ViewBag.works = works;

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Done(int ? worksId)
        {

            var tekis = (from w in entity.Works where w.worksId == worksId select w).FirstOrDefault();

            tekis.workprogressId = 2;
            tekis.yapilanTarih = DateTime.Now;

            entity.SaveChanges();
            return RedirectToAction("Index", "Calisan");



        }


        public ActionResult Liste()
        {
            int yetkiturId = Convert.ToInt32(Session["PersonelAuthorizationId"]);

            if (yetkiturId == 2)
            {
                int personelId = Convert.ToInt32(Session["PersonelID"]);
                var works = (from w in entity.Works join j in entity.Progress on w.workprogressId equals j.progressId
                             where w.workfarmerId == personelId select w).ToList().OrderByDescending(w => w.iletilenTarih);
               
                jobStatusModel model = new jobStatusModel();

                List<jobstatus> jobstatusList = new List<jobstatus>();

                foreach (var item in works)
                {
                    jobstatus jobstatus = new jobstatus();
                    jobstatus.workName = item.workName;
                    jobstatus.workAciklama = item.workAciklama;
                    jobstatus.iletilenTarih = item.iletilenTarih;
                    jobstatus.yapilanTarih = item.yapilanTarih;
                    jobstatus.workprogressAd = item.Progress.progressAd;

                    jobstatusList.Add(jobstatus);


                }

                model.jobstatusList = jobstatusList;

                ViewBag.works = works;

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        





    }
}