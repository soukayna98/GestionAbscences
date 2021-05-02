using GestionAbscences.Areas.Admin.Models;
using GestionAbscences.Data;
using GestionAbscences.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GestionAbscences.Areas.RH.Controllers
{
    public class HistoriqueController : Controller
    {
        private readonly DemandeService demandeService;
        private GestionAbscencesEntities5 db = new GestionAbscencesEntities5();


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
                if (item.ValidationN2 == "accepte" && item.ValidationN1 == "accepte" && item.ValidationRH == "En cours")
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

                    });
                }
            }
            return View(employesList);
        }

        public ActionResult Validation(int? id)
        {
            if (id == null)
            {
                //return RedirectToAction("Index", "Default");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var currentDemande = demandeService.ReadById(id.Value);
            if (currentDemande == null)
            {
                return HttpNotFound($"this demande ({id}) is not found");
            }

            demandeconge demandeconge = db.demandeconge.Find(id);
            Session["uid"] = currentDemande.idDemandeConge;

            if (demandeconge == null)
            {
                return HttpNotFound();
            }

            return View(demandeconge);
            /*
            var historiqueModel = new HistoriqueModel
            {
                IdDemande = currentDemande.idDemandeConge,
                IdType = currentDemande.IdtypeConge,
                //Nom = currentDemande.employe.NomComplet,
                //matricule = currentDemande.employe.matricule,
                DateD = (DateTime)currentDemande.DateDebut,
                DateF = (DateTime)currentDemande.DateFin,
                Datedc = (DateTime)currentDemande.DateDC,
                validation1 = currentDemande.ValidationN1,
                validation2 = currentDemande.ValidationN2,
                IdEmploye = currentDemande.IdEmploye



            };

            return View(historiqueModel);*/
        }



        [HttpPost]
        public ActionResult Validation()
        {
            int uid = int.Parse(Session["uid"].ToString());
            demandeconge e = db.demandeconge.Find(uid);
            string button = Request["button"];
            switch (button)
            {
                case "Accepté":
                    e.ValidationN1 = "Accepte";
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("historique");
                case "Refusé":
                    e.ValidationN1 = "refuse";
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("historique");
                case "Annulé":

                    return RedirectToAction("historique");
                default:
                    return View();

            }



        }

    }
}