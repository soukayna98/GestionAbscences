using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GestionAbscences.Models;
using GestionAbscences.Data;
using System.Data.Entity;
namespace GestionAbscences.Areas.Admin.Controllers
{
    public class DefaultController : Controller
    {
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();
        // GET: Admin/Default
        public ActionResult Index()
        {
            Session["demande"] = null;
            return View();
        }
        [HttpGet]
        public ActionResult changePassword()
        {
            Session["demande"] = null;
            return View();
        }
        public ActionResult employe()
        {
            Session["demande"] = null;
            string aff = Session["affectation"].ToString();
            var employe = db.employe.Where(p => p.affectation.Equals(aff));

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
            Session["demande"] = null;
            return View();
        }

    }
}