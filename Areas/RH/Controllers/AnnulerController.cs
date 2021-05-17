﻿using GestionAbscences.Data;
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
    public class AnnulerController : Controller
    {
        // GET: RH/Annuler
        private GestionAbscencesEntities5 db = new GestionAbscencesEntities5();
        private readonly DemandeService demandeService;

        public AnnulerController()
        {
            demandeService = new DemandeService();
        }

        // GET: RH/Annuler
        public ActionResult Index()
        {
            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationRH == "accepte" && p.ValidationN2 == "accepte");

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
                    e.ValidationRH = "refuse";
                    e.employe.nbjoursR = Convert.ToInt32(res);
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                case "Retour":

                    return RedirectToAction("Index");

                default:
                    return View();

            }

        }


        }
    }