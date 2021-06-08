using GestionAbscences.Controllers;
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
        private GestionAbscencesEntities7 db = new GestionAbscencesEntities7();

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
            data.NomComplet = Request["NomComplet"];
            data.matricule = Request["matricule"];
            data.nbjours = Convert.ToInt32(Request["nbjours"]);
           
            data.affectation = Request["affectation"];
            data.role = Request["role"];
            data.password = Request["password"];
            data.soldeConge = Convert.ToInt32(Request["soldeConge"]);
            data.Classe = Request["Classe"];
            data.DateDebut = Convert.ToDateTime(Request["DateDebut"]);
            //  data.DateFin = Convert.ToDateTime(Request["DateFin"]);

            db.employe.Add(data);
            db.SaveChanges();
            return RedirectToAction("Index", "Employe");
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