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
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using ClosedXML.Excel;





namespace GestionAbscences.Areas.RH.Controllers
{
    public class HistoriqueController : Controller
    {
        private GestionAbscencesEntities5 db = new GestionAbscencesEntities5();
        private readonly DemandeService demandeService;

        public HistoriqueController()
        {
            demandeService = new DemandeService();
        }
        [HttpPost]
        public FileResult Export()
        {
            GestionAbscencesEntities5 entities = new GestionAbscencesEntities5();
          
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[8] { new  DataColumn("Date creation"),
                                            new DataColumn("Nom complet"),
                                           new  DataColumn("Matricule"),
                                            new DataColumn("Début"),
                                            new DataColumn("Fin"),
                                            new DataColumn("Validation N+1"),
                                            new DataColumn("Validation N+2"),
                                            new DataColumn("Validation RH") });

            var demande = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where((p => p.ValidationN2 == "accepte" && p.ValidationN1 != "refuse"));

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
        }








        public ActionResult historique()
        {
            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where((p => p.ValidationN2 == "accepte" && p.ValidationN1 != "refuse"));

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

            DateTime dateDebut = e.DateDebut.Value;
            DateTime dateFin = e.DateFin.Value;
            var dure = (dateFin - dateDebut).Days;
            var dureM = (dateFin - dateDebut).TotalMinutes;

            double du = Convert.ToDouble(dure) + 1;
            double duM = Convert.ToDouble(dureM) + 1440;
            double nb = Convert.ToDouble(e.employe.nbjoursR);
            double nbM = Convert.ToDouble(e.employe.nbjoursR) * 24 * 60;
            double res = nb - du;
            double resM = nbM - duM;
            double resJ = resM / 1440;

            //  Session["dur1"] = duM;
            switch (button)
            {
                case "Accepté":
                    e.ValdationRH = "accepte";


                    e.employe.nbjoursR = res.ToString();
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("historique");
                case "Refusé":
                    e.ValdationRH = "refuse";
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