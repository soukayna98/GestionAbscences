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

        private readonly DemandeService demandeService;
        private GestionAbscencesEntities4 db = new GestionAbscencesEntities4();


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
                if (item.ValidationN2 == "En cours")
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
        //"Edit", "Edit", new { id = item.IdDemandeConge }
        //get infos
        public ActionResult Validation(int? id)
        {
            if (id == null )
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

            string validation1 = Request.Form["validation1"];
            string validation2 = Request.Form["validation2"];

            int uid = int.Parse(Session["uid"].ToString());

            demandeconge e = db.demandeconge.Find(uid);

            Session["index"] = uid;



            if (validation2.Equals("Accepte"))
            {
                e.ValidationN2 = "accepte";
            }
            if (validation2.Equals("Refuse"))
            {
                e.ValidationN2 = "refuse";
            }
            if (validation1.Equals("Accepte"))
            {
                e.ValidationN1 = "accepte";
            }
            if (validation1.Equals("Refuse"))
            {
                e.ValidationN1 = "refuse";
            }




            db.Entry(e).State = EntityState.Modified;
            db.SaveChanges();





            return RedirectToAction("historique", "Historique");
        }



        /* [HttpPost]
          public ActionResult Validation(HistoriqueModel data)
          {

             // if (ModelState.IsValid)
              //{
                  var updatedDemande = new demandeconge
                  {
                      idDemandeConge = data.IdDemande,
                    IdtypeConge = data.IdType,
                     // IdEmploye = data.IdEmploye,
                     // d.NomComplet = data.Nom,
                      // matricule = data.employe.matricule,
                      //DateDebut = (DateTime)data.DateD,
                     // DateFin = (DateTime)data.DateF,
                    //  DateDC = (DateTime)data.Datedc,
                      ValidationN1 = data.validation1,
                      ValidationN2 = data.validation2
                  };
                  var result = demandeService.Update(updatedDemande);

                  if (result > 0)
                  {
                      ViewBag.Success = true;
                      ViewBag.Message = $"demande ({data.IdDemande}) updated succefully";
                  }
                  else
                      ViewBag.Message = $"an error occurred while updation demande !";

              //}

              return View(data);
          }*/



    }
}