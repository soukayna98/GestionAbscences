using GestionAbscences.Areas.Admin.Models;
using GestionAbscences.Data;
using GestionAbscences.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GestionAbscences.Areas.Admin.Controllers
{

    public class AccountController : Controller
    {

        private GestionAbscencesEntities3  db = new GestionAbscencesEntities3();

        // GET: Admin/Account
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(employe log)
        {
            var user = db.employe.Where(x => x.idEmploye == log.idEmploye && x.password == log.password).ToList().FirstOrDefault();
            if (user != null)
            {
                Session["userName"] = user.NomComplet;
                Session["matricule"] = user.idEmploye;
                Session["nbjours"] = user.nbjours.ToString();
                Session["Classe"] = user.Classe;
                @Session["DateFin"] = user.DateFin;
                @Session["DateDebut"] = user.DateDebut;

                return RedirectToAction("Index", "Default");
            }
            else
            {
                return View();
            }

        }

        /* [HttpPost]
         public ActionResult Login(LoginModel loginInfo)
         {
            var adminService = new AdminService();
            var isLoggedIn =  adminService.Login(loginInfo.Id, loginInfo.Password);
             if (isLoggedIn)
             {
                 return RedirectToAction("Index", "Default");
             }
             else
             {
                 loginInfo.Message = "email or pass incorrect";
                 return View(loginInfo);
             }

         }*/
        [HttpGet]
        public ActionResult changePassword()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult changePassword(ChangePasswordModel obj)
        {
            if (ModelState.IsValid)
            {
                int uid = int.Parse(Session["matricule"].ToString());

                employe e = db.employe.Find(uid);

                if (e.password == obj.OldPassword)
                {
                    e.password = obj.NewPassword;
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                   obj.Message = "Your Password is updated successfully";
                   // return RedirectToAction("Index", "Default");

                }
                else
                {
                    obj.Message = "Invalid currrent  Password";
                 //  return RedirectToAction("Index", "Default");
                }

            }

            return View(obj);


        }
        public ActionResult Donnee()
        {
            return View();
        }

    }
}