using GestionAbscences.Data;
using GestionAbscences.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionAbscences.Controllers
{
    public class employeController : Controller
    {
        private GestionAbscencesEntities db = new GestionAbscencesEntities();

        // GET: employe
        public ActionResult Index()
        {
 
            return View();
        }

        [HttpPost]
        public ActionResult Index()
        {

            return View();
        }



        
           /* public ActionResult Login(employe log)
            {
                var user = db.employes.Where(x => x.idEmploye == log.idEmploye && x.password == log.password).ToList().FirstOrDefault();
                if (user != null)
                {
                    Session["userName"] = user.NomComplet;
                    Session["matricule"] = user.idEmploye;
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    return View();
                }

            }
            [HttpGet]

            public ActionResult Dashboard()
            {

                return View();
            }
            public ActionResult index1()
            {

                return View();
            }

            [HttpPost]
            public string Dashboard1()
            {
                DemandeConge demande = new DemandeConge();


                string employeIdEmploye1 = Request["matricule"];
                string typeCongeIdTypeconge = Request.Form["typeCongeIdTypeconge"];
                string dateDebut = Request["dateDebut"] + " " + Request["timeDebut"];
                string dateFin = Request["dateFin"] + " " + Request["timeFin"];

                DateTime dc = DateTime.Now;


                if (typeCongeIdTypeconge.Equals("reliquat"))
                {
                    demande.typeCongeIdTypeconge = 1;
                }
                else if (typeCongeIdTypeconge.Equals("1 ere tranche"))
                {
                    demande.typeCongeIdTypeconge = 2;
                }
                else if (typeCongeIdTypeconge.Equals("2 eme tranche"))
                {
                    demande.typeCongeIdTypeconge = 3;
                }

                demande.validation1 = "En cours";
                demande.validation2 = "En cours";
                demande.employeIdEmploye1 = int.Parse(employeIdEmploye1);

                demande.dateDebut = Convert.ToDateTime(dateDebut);
                demande.dateFin = Convert.ToDateTime(dateFin);
                demande.dateDc = dc;

                db.DemandeConges.Add(demande);


                db.SaveChanges();

                ViewBag.Message = "la demande est enregistré !";

                return "Demande : " + dc;

            }
        }*/
    }

}
}