﻿using GestionAbscences.Controllers;
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
    public class EmployeController : BaseController
    {
        // GET: RH/Employe
        private GestionAbscencesEntities5 db = new GestionAbscencesEntities5();

        private readonly EmployeService employeService;
        // GET: RH/Employe
        public ActionResult Index()
        {

            var employe = db.employe;

            return View(employe.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(employe data)
        {
            if (ModelState.IsValid)
            {
                int creationResult = employeService.Create(new Data.employe
                {
                    NomComplet = data.NomComplet,
                    matricule = data.matricule,
                    nbjours = data.nbjours,
                    nbjoursA = data.nbjoursA,
                    nbjoursR = data.nbjoursR,
                    affectation = data.affectation,
                    role = data.role,
                    password = data.password,
                    soldeConge = data.soldeConge,
                    Classe = data.Classe,
                    DateDebut = data.DateDebut,
                    DateFin = data.DateFin
                });
                if (creationResult == -2)
                {
                    ViewBag.Message = "nom exist";
                    return View(data);
                }

                return RedirectToAction("Index");

            }

            return View();
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var currentEmploye = employeService.ReadById(id.Value);
            if (currentEmploye == null)
            {
                return HttpNotFound($"this demande ({id}) is not found");
            }


            employe e = db.employe.Find(id);
             Session["idE"] = currentEmploye.idEmploye;
           // Session["idE"] = e.idEmploye;
            if (e == null)
            {
                return HttpNotFound();
            }

            return View(e);
        }

        [HttpPost]
        public ActionResult Delete()
        {

            int uid = int.Parse(Session["idE"].ToString());
            employe e = db.employe.Find(uid);
            DateTime dc = DateTime.Now;


            string button = Request["button"];
            switch (button)
            {
                case "Supprimer":
                    e.DateFin = dc;
                    e.password = "supprime";
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                case "Annuler":

                    return RedirectToAction("Index");

                default:
                    return View();

            }
        }
        }
}