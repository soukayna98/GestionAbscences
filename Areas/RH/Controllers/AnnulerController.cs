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
namespace GestionAbscences.Areas.RH.Controllers
{
    public class AnnulerController : Controller
    {
        private GestionAbscencesEntities5 db = new GestionAbscencesEntities5();
        private readonly DemandeService demandeService;

        public AnnulerController()
        {
            demandeService = new DemandeService();
        }

        // GET: RH/Annuler
        public ActionResult Index()
        {
            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValdationRH == "accepte" && p.ValidationN2 == "accepte");

            return View(demandeConge.ToList());

        }




        public ActionResult Annulation(int? id)
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
        public ActionResult Annulation()
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
            double res = nb + du;
            double resM = nbM - duM;
            double resJ = resM / 1440;

            //  Session["dur1"] = duM;
            switch (button)
            {
                case "Annulé":
                    e.ValdationRH = "refuse";
                    e.employe.nbjoursR = res.ToString();
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                case "Retour":

                    return RedirectToAction("Index");

                default:
                    return View();

            }





        




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

            var demande  = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValdationRH == "accepte" && p.ValidationN2 == "accepte");

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

    }
}