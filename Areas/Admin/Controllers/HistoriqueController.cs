using GestionAbscences.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GestionAbscences.Services;
using GestionAbscences.Areas.Admin.Models;
using System.Net;

namespace GestionAbscences.Areas.Admin.Controllers
{
    public class HistoriqueController : Controller
    {
        private GestionAbscencesEntities4 db = new GestionAbscencesEntities4();
        private readonly DemandeService demandeService;

        public HistoriqueController()
        {
            demandeService = new DemandeService();
        }

       
        //private GestionAbscencesEntities1 db = new GestionAbscencesEntities1();

        // GET: Admin/Historique
        public ActionResult historique()
        {
            //les employes from DB

            var employes = demandeService.ReadAll();

            var employesList = new List<DemandeModel>();
            foreach (var item in employes)
            {
                employesList.Add(new DemandeModel
                {
                    DateDebut = (DateTime)item.DateDebut,
                    DateFin = (DateTime)item.DateFin,
                    DateDc = (DateTime)item.DateDC,
                    validationN1 = item.ValidationN1,
                    validationN2 = item.ValidationN2,
                    matricule = item.IdEmploye,
                    IdTypeConge = item.IdtypeConge,
                    IdDemandeConge = item.idDemandeConge,
                    NomComplet = item.employe.NomComplet

                }) ;
            }
            return View(employesList);
        }

        
        public ActionResult validation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            demandeconge demandeconge = db.demandeconge.Find(id);
            if (demandeconge == null)
            {
                return HttpNotFound();
            }
           
            return View(demandeconge);
        }
        [HttpPost]
        public ActionResult Validation(demandeconge demandeconge)
        {
            string validation1 = Request.Form["validation1"];
            string validation2 = Request.Form["validation2"];
            if (validation1.Equals("Accepté"))
            {
                demandeconge.ValidationN1 = "Accepté";
            }
            else
                  if (validation1.Equals("Refusé"))
            {
                demandeconge.ValidationN1 = "Refusé";
            }
            else if (validation2.Equals("Accepté"))
            {
                demandeconge.ValidationN2 = "Accepté";
            }
            else
                  if (validation2.Equals("Refusé"))
            {
                demandeconge.ValidationN2 = "Refusé";
            }

            db.Entry(demandeconge).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Default");

        }
               
    }
}
            
                
        

