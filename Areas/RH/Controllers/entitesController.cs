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
    public class entitesController : Controller
    {
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();

        // GET: RH/entites
        public ActionResult Index()
        {
            return View(db.entite.ToList());
        }

        // GET: RH/entites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            entite entite = db.entite.Find(id);
            if (entite == null)
            {
                return HttpNotFound();
            }
            return View(entite);
        }


       
        // GET: RH/entites/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: RH/entites/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(entite entite)
        {
            entite.Designation = Request["Designation"];

            db.entite.Add(entite);
            db.SaveChanges();
            return RedirectToAction("Index", "entites");
        }

        // GET: RH/entites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            entite entite = db.entite.Find(id);
            if (entite == null)
            {
                return HttpNotFound();
            }
            return View(entite);
        }

        // POST: RH/entites/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idEntite,Designation")] entite entite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(entite);
        }

        // GET: RH/entites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            entite entite = db.entite.Find(id);
            if (entite == null)
            {
                return HttpNotFound();
            }
            return View(entite);
        }

        // POST: RH/entites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            entite entite = db.entite.Find(id);
            db.entite.Remove(entite);
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
