using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionAbscences.Models;
using GestionAbscences.Data;
using System.Data.Entity;
using System.Web.Mvc;

namespace GestionAbscences.Areas.AdminN2.Controllers
{
    public class DefaultController : Controller
    {
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();
        // GET: Admin/Default
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult changePassword()
        {

            return View();
        }
        public ActionResult employe()
        {

            var employe = db.employe;

            return View(employe.ToList());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult changePassword(ChangePasswordModel obj)
        {

            if (ModelState.IsValid)
            {
                int uid = int.Parse(Session["idEmploye"].ToString());

                employe e = db.employe.Find(uid);


                if (e.password == obj.OldPassword)
                {
                    e.password = obj.NewPassword;
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    Session["Messag"] = "Your Password is updated successfully";
                    return RedirectToAction("Index", "Default");

                }
                else
                {
                    Session["Messag"] = "Invalid currrent  Password";
                    return RedirectToAction("Index", "Default");
                }

            }

            return View(obj);


        }


        [HttpGet]
        public ActionResult Donnée()
        {
           
            return View();
        }

    }
}