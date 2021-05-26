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
        private GestionAbscencesEntities9 db = new GestionAbscencesEntities9();
        private readonly DemandeService demandeService;

        public HistoriqueController()
        {
            demandeService = new DemandeService();
        }
        /*
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
        }*/







      
        public ActionResult historique()
        {

            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where((p => p.ValidationN2 == "accepte" && p.ValidationN1 != "refuse"));
            return View(demandeConge.ToList());


        }
        
       
        [HttpPost]
        public ActionResult historique1()
        {
           DateTime debut =Convert.ToDateTime(Request["debut"]);

            DateTime fin = Convert.ToDateTime(Request["fin"]);
            if (!(Request["debut"].Equals(""))  && !(Request["fin"].Equals("")))
            {
                var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= debut && p.DateFin <= fin);

                return View(demandeConge.ToList());
            }
            else
             if (!(Request["debut"].Equals("")) && (Request["fin"].Equals("")))
              
            {
                var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => Convert.ToDateTime(p.DateDebut) == Convert.ToDateTime(Request["debut"]));

                return View(demandeConge.ToList());
            }
            else
             if ((Request["debut"].Equals("")) && !(Request["fin"].Equals("")))
                
            {
                var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateFin == fin);

                return View(demandeConge.ToList());
            }
            else
            {
                var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where((p => p.ValidationN2 == "accepte" && p.ValidationN1 != "refuse"));

                return View(demandeConge.ToList());
            }




            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult choixVal()
        {
            string val = Request["validation"].ToString();
            
            if(val.Equals("1"))
                {
                var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge);
                return View(demandeConge.ToList());
               }
            else if (val.Equals("2"))
            {
                var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1== "En cours");
                return View(demandeConge.ToList());
            }
            else if (val.Equals("3"))
            {
                var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte");
                return View(demandeConge.ToList());
            }
            else if (val.Equals("4"))
            {
                var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse");
                return View(demandeConge.ToList());
            }
            else if (val.Equals("5"))
            {
                var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "En cours");
                return View(demandeConge.ToList());
            }
            else if (val.Equals("6"))
            {
                var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "accepte");
                return View(demandeConge.ToList());
            }
            else if (val.Equals("7"))
            {
                var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "refuse");
                return View(demandeConge.ToList());
            }
            else if (val.Equals("8"))
            {
                var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValdationRH == "En cours");
                return View(demandeConge.ToList());
            }
            else if (val.Equals("9"))
            {
                var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValdationRH == "accepte");
                return View(demandeConge.ToList());
            }
            else if (val.Equals("10"))
            {
                var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValdationRH == "refuse");
                return View(demandeConge.ToList());
            }


            return RedirectToAction("historique");
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
        }

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