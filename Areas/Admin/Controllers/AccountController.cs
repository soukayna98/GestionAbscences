using GestionAbscences.Areas.Admin.Models;
using GestionAbscences.Data;
using GestionAbscences.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GestionAbscences.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        public GestionAbscencesEntities1 db = new GestionAbscencesEntities1();


        // GET: Admin/Account
        public ActionResult Login()
        {
            return View();
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

        [HttpPost]
        public ActionResult Login(employe log)
        {
            var user = db.employe.Where(x => x.idEmploye == log.idEmploye && x.password == log.password).ToList().FirstOrDefault();
            if (user != null)
            {
                Session["userName"] = user.NomComplet;
                Session["matricule"] = user.idEmploye;
                Session["nbjours"] = user.nbjours.ToString();
                return RedirectToAction("Index", "Default");
            }
            else
            {
                return View();
            }
        }
    }
}