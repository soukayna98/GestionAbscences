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
using GestionAbscences.Controllers;

namespace GestionAbscences.Areas.Admin.Controllers
{
    public class HistoriqueController : BaseController
    {

        private readonly DemandeService demandeService;
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();


        public HistoriqueController()
        {
            demandeService = new DemandeService();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult choixVal()
        {
            Session["demande"] = null;
            string d1 = Request["debut"].ToString();
            string f1 = Request["fin"].ToString();
            ViewBag.d2 = Request["debut"].ToString();
            ViewBag.f2 = Request["fin"].ToString();


            string val = Request["validation"].ToString();



            if (d1.Equals("") || f1.Equals("") || val.Equals(""))
            {
                ViewBag.message = "Selectionner les dates et la catégorie svp !";
                return RedirectToAction("historique");
            }
            else
            {
                DateTime debut = Convert.ToDateTime(Request["debut"]);

                DateTime fin = Convert.ToDateTime(Request["fin"]);

                if (val.Equals("1"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("2"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("3"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("4"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("5"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "En cours" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("6"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "accepte" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("7"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "refuse" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("8"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationRH == "En cours" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("9"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationRH == "accepte" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("10"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationRH == "refuse" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }


            }


            return RedirectToAction("historique");

        }

        //private GestionAbscencesEntities1 db = new GestionAbscencesEntities1();

        // GET: Admin/Historique
        /* public ActionResult historique()
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
         }*/
        public ActionResult historique()
        {
            Session["demande"] = null;
            string aff = Session["affectation"].ToString();
            ViewBag.val1 = new SelectList(db.demandeconge, "idDemandeConge", "ValidationN1");
            ViewBag.val2 = new SelectList(db.demandeconge, "idDemandeConge", "ValidationN2");
            ViewBag.valR = new SelectList(db.demandeconge, "idDemandeConge", "ValdationRH");
            ViewBag.dateD = new SelectList(db.demandeconge, "idDemandeConge", "DateDebut");

            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.employe.affectation.Equals(aff)).OrderByDescending(news => news.DateDC).ToList();
            return View(demandeConge);
        }
      /*  public ActionResult historiques()
        {
            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge);

            return View(demandeConge.ToList());
        }*/
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
                    e.ValidationN2 = "****";
                    e.ValidationRH = "****";
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("historique");
                case "Annulé":

                    return RedirectToAction("historique");
                default:
                    return View();

            }



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