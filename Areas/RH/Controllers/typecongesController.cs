using GestionAbscences.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GestionAbscences.Areas.RH.Controllers
{
    public class typecongesController : Controller
    {
        // GET: RH/typeconges
        private GestionAbscencesEntities5 db = new GestionAbscencesEntities5();

        // GET: RH/typeconges
        public ActionResult Index()
        {
            return View(db.typeconge.ToList());
        }

        // GET: RH/typeconges/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            typeconge typeconge = db.typeconge.Find(id);
            if (typeconge == null)
            {
                return HttpNotFound();
            }
            return View(typeconge);
        }

        // GET: RH/typeconges/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RH/typeconges/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idtypeConge,designation,dureeJ")] typeconge typeconge)
        {
            if (ModelState.IsValid)
            {
                db.typeconge.Add(typeconge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(typeconge);
        }

        // GET: RH/typeconges/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            typeconge typeconge = db.typeconge.Find(id);
            if (typeconge == null)
            {
                return HttpNotFound();
            }
            return View(typeconge);
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idtypeConge,designation,dureeJ")] typeconge typeconge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(typeconge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(typeconge);
        }

        // GET: RH/typeconges/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            typeconge typeconge = db.typeconge.Find(id);
            if (typeconge == null)
            {
                return HttpNotFound();
            }
            return View(typeconge);
        }

        // POST: RH/typeconges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            typeconge typeconge = db.typeconge.Find(id);
            db.typeconge.Remove(typeconge);
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