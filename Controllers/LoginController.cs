
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
        private GestionAbscencesEntities7 db = new GestionAbscencesEntities7();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /*  [HttpPost]
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
        private readonly DemandeService demandeService;



        [HttpPost]
        public ActionResult Index(employe log)
        {
            var user = db.employe.Where(x => x.matricule == log.matricule && x.password == log.password).ToList().FirstOrDefault();
            if (user != null)
            {
                Session["userName"] = user.NomComplet;
                Session["matricule"] = user.matricule;
                Session["idEmploye"] = user.idEmploye;
                Session["affectation"] = user.affectation;
                Session["nbjours"] = user.nbjours.ToString();
               Session["nbjoursR"] = user.nbjoursR.ToString();
                Session["Classe"] = user.Classe;
                Session["DateFin"] = user.DateFin;
                Session["DateDebut"] = user.DateDebut;

               

                string role = user.role;
                if (role == "adminN1")
                {
                    return RedirectToAction("historique", "Historique", new { area = "Admin" });
                }
                else if (role == "adminN2")
                {
                    return RedirectToAction("historique", "Historique", new { area = "AdminN2" });
                }
                else if (role == "Rh")
                {
                    return RedirectToAction("historique", "Historique", new { area = "RH" });
                }
                else if (user.password=="supprime")
                {
                    Session["message"] = "Compte introuvable";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    return RedirectToAction("historique", "employe");

                }

            }
            else
            {
                return View();
            }



        }


    }
}
