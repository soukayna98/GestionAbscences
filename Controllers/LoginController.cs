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

        public GestionAbscencesEntities4 db = new GestionAbscencesEntities4();


        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /*[HttpPost]
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
            var user = db.employe.Where(x => x.matricule == log.matricule && x.password == log.password).ToList().FirstOrDefault();
            if (user != null)
            {
                Session["userName"] = user.NomComplet;
                Session["matricule"] = user.matricule;
                Session["affectation"] = user.affectation;
                Session["nbjours"] = user.nbjours.ToString();
                Session["nbjoursR"] = user.nbjoursR.ToString();
                Session["nbjoursR"] = user.nbjoursR.ToString();
                Session["Classe"] = user.Classe;
                Session["DateFin"] = user.DateFin;
                Session["DateDebut"] = user.DateDebut;
                Session["idEmploye"] = user.idEmploye; 


                string role = user.role;
                if (role == "adminN1")
                {
                    return RedirectToAction("Index", "Default", new { area = "Admin" });
                }
                else if (role == "adminN2")
                {
                    return RedirectToAction("Index", "Default", new { area = "AdminN2" });
                }else if (role == "Rh")
                {
                    return RedirectToAction("Index", "Default", new { area = "RH" });
                }
                else
                {
                    return RedirectToAction("Index", "Default");

                }

            }
            else
            {
                return View();
            }

        
                  ////  return RedirectToAction("Index", "Default");
                
            
            
        }
    }
}
