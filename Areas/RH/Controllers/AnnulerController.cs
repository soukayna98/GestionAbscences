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
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();
        private readonly DemandeService demandeService;

        public AnnulerController()
        {
            demandeService = new DemandeService();
        }

        // GET: RH/Annuler
        public ActionResult Index()
        {
            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValdationRH == "accepte" && p.ValidationN2 == "accepte").OrderByDescending(news => news.DateDC).Take(10).ToList();

            return View(demandeConge);

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


                double du = Convert.ToDouble(dure) + 1;
                double d = du * 24;
                double nb = Convert.ToDouble(e.employe.nbHeureR);
               
                double res = nb + d;

           








            switch (button)
            {
                case "Retour":
                   
                    return RedirectToAction("Index");
                case "annule":
                    e.ValdationRH = "En cours";
                    e.ValidationN1 = "En cours";
                    e.ValidationN2 = "En cours";
                    e.annulation = "oui";
                    e.employe.nbHeureR = res.ToString();
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                default:
                    return View();

            }





        




    }

    }
}