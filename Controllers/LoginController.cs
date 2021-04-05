using GestionAbscences.Data;
using GestionAbscences.Models;
using GestionAbscences.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionAbscences.Controllers
{
    public class LoginController : Controller
    {

        public GestionAbscencesEntities1 db = new GestionAbscencesEntities1();
        

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

     /*   [HttpPost]
        public ActionResult Index(LoginModl loginInfo)
        {
            var adminService = new AdminService();
            var isLoggedIn = adminService.Login(loginInfo.Id, loginInfo.Password);
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
        public ActionResult Index(employe log)
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
