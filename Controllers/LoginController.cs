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
        public GestionAbscencesEntities db { get; set; }

        public LoginController()
        {

            db = new GestionAbscencesEntities();
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login loginInfo)
        {
            var adminService = new AdminService();
            var isLoggedIn = adminService.Login(loginInfo.Id, loginInfo.Password);
            if (isLoggedIn)
            {
                Session["userName"] = db.employe.Select(x => x.NomComplet);
                Session["matricule"] = db.employe.Select(x => x.idEmploye);
                return RedirectToAction("Index", "employe");
            }
            else
            {
                loginInfo.Message = "email or pass incorrect";
                return View(loginInfo);
            }

        }
    }
}