using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionAbscences.Data;

namespace GestionAbscences.Areas.RH.Controllers
{
    public class employehasentitesController : Controller
    {
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();

        // GET: Admin/employehasentites
        public ActionResult Index()
        {
            var employehasentite = db.employehasentite.Include(e => e.employe).Include(e => e.entite);
            return View(employehasentite.ToList());
        }

        // GET: Admin/employehasentites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employehasentite employehasentite = db.employehasentite.Find(id);
            if (employehasentite == null)
            {
                return HttpNotFound();
            }
            return View(employehasentite);
        }

        // GET: Admin/employehasentites/Create
        public ActionResult Create()
        {
            ViewBag.IdEmploye = new SelectList(db.employe, "idEmploye", "matricule");
            ViewBag.IdEntite = new SelectList(db.entite, "idEntite", "Designation");
            return View();
        }

        // POST: Admin/employehasentites/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEmploye,IdEntite,date")] employehasentite employehasentite)
        {
            
          

            if (ModelState.IsValid)
             {
                int id = employehasentite.IdEmploye;
                var id1 = employehasentite.IdEntite;
                entite e1 = db.entite.Find(id1);
                employe e = db.employe.Find(id);
              // Session["des"] = e1.Designation;
                e.affectation = e1.Designation;
                db.employehasentite.Add(employehasentite);
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }

             ViewBag.IdEmploye = new SelectList(db.employe, "idEmploye", "matricule", employehasentite.IdEmploye);
             ViewBag.IdEntite = new SelectList(db.entite, "idEntite", "Designation", employehasentite.IdEntite);
             return View(employehasentite);
            /*
           employehasentite.date = Convert.ToDateTime(Request["date"]);
           employehasentite.IdEmploye = 1;
           employehasentite.IdEntite = 3;
           db.employehasentite.Add(employehasentite);
           db.SaveChanges();
           return RedirectToAction("Index");
           */
        }

        // GET: Admin/employehasentites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employehasentite employehasentite = db.employehasentite.Find(id);
            if (employehasentite == null)
            {
                return HttpNotFound();
            }
            return View(employehasentite);
        }

        // POST: Admin/employehasentites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            employehasentite employehasentite = db.employehasentite.Find(id);
            db.employehasentite.Remove(employehasentite);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }

}