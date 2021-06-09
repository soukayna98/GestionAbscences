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
using System.IO;
using System.Data;
using ClosedXML.Excel;

namespace GestionAbscences.Areas.AdminN2.Controllers
{
    public class HistoriqueController : Controller
    {
        private GestionAbscencesEntities10 db = new GestionAbscencesEntities10();
        private readonly DemandeService demandeService;

        public HistoriqueController()
        {
            demandeService = new DemandeService();
        }


        //private GestionAbscencesEntities1 db = new GestionAbscencesEntities1();

        // GET: Admin/Historique

        /*  public ActionResult historique()
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
                  });
              }
              return View(employesList);
          }
        [HttpPost]
        public FileResult Export()
        {
            GestionAbscencesEntities7 entities = new GestionAbscencesEntities7();

            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[8] { new  DataColumn("Date creation"),
                                            new DataColumn("Nom complet"),
                                           new  DataColumn("Matricule"),
                                            new DataColumn("Début"),
                                            new DataColumn("Fin"),
                                            new DataColumn("Validation N+1"),
                                            new DataColumn("Validation N+2"),
                                            new DataColumn("Validation RH") });

            var demande = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "En cours");

            foreach (var d in demande)
            {
                dt.Rows.Add(d.DateDC, d.employe.matricule, d.employe.NomComplet, d.DateDebut, d.DateFin, d.ValidationN1, d.ValidationN2, d.ValdationRH);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }*/
        public ActionResult historique()
        {
            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "En cours");

            return View(demandeConge.ToList());
        }
        public ActionResult validation(int? id)
        {
            if (id == null)
            {
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
        }/*
           [HttpPost]
           public ActionResult Validation(demandeconge data)
           {
               if (ModelState.IsValid)
               {
                   string validation1 = Request.Form["validation1"];
                   string validation2 = Request.Form["validation2"];
                   int uid = data.IdEmploye;
                   if (validation1.Equals("Accepté"))
                   {
                       Session["validation1"] = "Accepté";
                   }
                   else if (validation1.Equals("Refusé"))
                   {
                       Session["validation1"] = "Refusé";
                   }
                   if (validation2.Equals("Accepté"))
                   {
                       Session["validation2"] = "Accepté";
                   }
                   else if (validation2.Equals("Refusé"))
                   {
                       Session["validation2"] = "Refusé";
                   }
                   /*   {
                          idDemandeConge = data.idDemandeConge,
                          IdtypeConge = data.IdtypeConge,
                          IdEmploye = data.IdEmploye,
                          // d.NomComplet = data.Nom,
                          // matricule = data.employe.matricule,
                          DateDebut = (DateTime)data.DateDebut,
                          DateFin = (DateTime)data.DateFin,
                          DateDC = (DateTime)data.DateDC,
                          ValidationN1 = Session["validation1"].ToString(),
                          ValidationN2 = Session["validation1"].ToString(),
                      };
                   demandeconge updatedDemande = db.demandeconge.Find(uid);
                   updatedDemande.ValidationN1 = Session["validation1"].ToString();
                   updatedDemande.ValidationN2 = Session["validation2"].ToString();
                   db.Entry(updatedDemande).State = EntityState.Modified;
                   db.SaveChanges();
               }
               return View(data);
           }
        ///////////////////////////////
        ///
        //"Edit", "Edit", new { id = item.IdDemandeConge }
        public ActionResult Validation(int? id)
        {
            if (id == null || id == 0)
            {
                return RedirectToAction("Index", "Default");
            }
            var currentDemande = demandeService.ReadById(id.Value);
            if (currentDemande == null)
            {
                return HttpNotFound($"this demande ({id}) is not found");
            }
            Session["uid"] = currentDemande.idDemandeConge;
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
            return View(historiqueModel);
        }*/

        [HttpPost]
        public ActionResult Validation()
        {
            int uid = int.Parse(Session["uid"].ToString());
            demandeconge e = db.demandeconge.Find(uid);
            string button = Request["button"];

            //nbr jours qui reste
          /*  DateTime dateDebut = e.DateDebut.Value ;
            DateTime dateFin = e.DateFin.Value;

            TimeSpan diff = dateFin - dateDebut;
            DateTime diff1 = Convert.ToDateTime(diff);*/

         //   TimeSpan nbR = Convert.ToDateTime(e.employe.nbjoursR);
           

          //  TimeSpan R = nbR - dateFin - dateDebut;

        //    string nbjoursR = Convert.ToString(R);
                
            switch (button)
            {
                case "Accepté":
                    e.ValidationN2 = "accepte";
                    e.DateValidationN2= DateTime.Now;
                   
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("historique");
                case "Refusé":
                    e.ValidationN2 = "refuse";
                    e.ValdationRH = "*******";
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("historique");
                case "Annulé":

                    return RedirectToAction("historique");
                default:
                    return View();

            }



        }

        /*  [HttpPost]
         * 
        public ActionResult Validation()
        {
             string validation1 = Request.Form["validation1"];
               string validation2 = Request.Form["validation2"];
               int uid = int.Parse(Session["uid"].ToString());
               demandeconge e = db.demandeconge.Find(uid);
               Session["index"] = uid;
               if (validation2.Equals("Accepte"))
               {
                        e.ValidationN2 = "Accepté";
               }
                else if (validation2.Equals("Refuse"))
               {
                        e.ValidationN2 = "refusé";
               }
           else 
           {
               e.ValidationN2 = "En cours";
           }
           if (validation1.Equals("Accepte"))
               {
               e.ValidationN1 = "Accepté";
               }
           else if (validation1.Equals("Refuse"))
           {
               e.ValidationN1 = "refusé";
           }
           else
           {
               e.ValidationN1 = "En cours";
           }
           db.Entry(e).State = EntityState.Modified;
               db.SaveChanges();
           return RedirectToAction("Index" ,"Default");
       }*/




    }
}










