using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BitkiTakipSystemMVC.Models;
namespace BitkiTakipSystemMVC.Controllers 
{
    public class LoginController : Controller
    {
        // GET: Login
        PlantTakipDbEntities entity = new PlantTakipDbEntities();
        
        public ActionResult Index()
        {
            ViewBag.Mesaj = null;
            return View();
        }

        [HttpPost]
        public ActionResult Index(string kullaniciAd, string parola)
        {
            var personel = (from p in entity.Personels 
                            where p.PersonelKullaniciAd == kullaniciAd && p.PersonelParola == parola
                            select p).FirstOrDefault();
            if(personel != null)
            {
                Session["PersonelID"] = personel.PersonelId;
                Session["PersonelAdSoyad"] = personel.PersonelAdSoyad;
                Session["PersonelAuthorizationId"] = personel.PersonelAuthorizationId;
                Session["AuthorizationTypesId"] = personel.AuthorizationTypes;
                Session["PersonelLocationId"] = personel.PersonelLocationId;

                switch (personel.PersonelAuthorizationId)
                {
                    case 1:
                        return RedirectToAction("Index", "Yonetici");
                    case 2:
                        return RedirectToAction("Index", "Calisan");
                    default:
                        return View();
                }

            }
            else
            {
                ViewBag.Mesaj = "Wrong username or Password";
                return View();
            }    
        }
    }
}