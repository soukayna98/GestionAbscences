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
    public class employeController : Controller
    {

        //private readonly DemandeService demandeService;
        public GestionAbscencesEntities db = new GestionAbscencesEntities();

       
       

        // GET: employe
        public ActionResult Index()
        {
 
            return View();
        }

 
        [HttpPost]
        public string Dashboard1()
        {
            demandeconge demande = new demandeconge();

            

            string employeIdEmploye1 = Request["matricule"];
            string typeCongeIdTypeconge = Request.Form["typeCongeIdTypeconge"];
            string dateDebut = Request["dateDebut"] + " " + Request["timeDebut"];
            string dateFin = Request["dateFin"] + " " + Request["timeFin"];

            DateTime dc = DateTime.Now;


            if (typeCongeIdTypeconge.Equals("reliquat"))
            {
                demande.IdtypeConge = 1;
            }
            else if (typeCongeIdTypeconge.Equals("1 ere tranche"))
            {
                demande.IdtypeConge = 2;
            }
            else if (typeCongeIdTypeconge.Equals("2 eme tranche"))
            {
                demande.IdtypeConge = 3;
            }

            

            demande.IdEmploye = int.Parse(employeIdEmploye1);

            demande.DateDebut = Convert.ToDateTime(dateDebut);
            demande.DateFin = Convert.ToDateTime(dateFin);
            demande.DateDC = dc;

            demande.ValidationN1 = "En cours";
            demande.ValidationN2 = "En cours";

            db.demandeconge.Add(demande);
            db.SaveChanges();

            //ViewBag.Message = "la demande est enregistré !";



            return "Demande : " + dc + dateDebut ;

        }

    }
}