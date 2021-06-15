using GestionAbscences.Controllers;
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
    public class typecongesController : BaseController
    {
        // GET: RH/typeconges
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();

        // GET: RH/typeconges
        public ActionResult Index()
        {
            var typeConge = db.typeconge;

            return View(typeConge.ToList());
        }
        [HttpPost]
        public ActionResult Index(List<typeconge> list)
        {
            if (ModelState.IsValid)
            {
               

                    foreach (var i in list)
                    {
                        var c = db.typeconge.Where(a => a.idtypeConge.Equals(i.idtypeConge)).FirstOrDefault();
                        if (c != null)
                        {
                            c.designation = i.designation;
                            c.dureeJ = i.dureeJ;
                        }
                    }
                    db.SaveChanges();
                
                ViewBag.Message = "succes";
                return View(list);

            }
            else
            {
                ViewBag.Message = "faaaiiiils";
                return View(list);
            }
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

      
            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modifier()
        {
            var t = new List<typeconge>();
            foreach (var item in t)
            {
                int id = item.idtypeConge;
                for(int i = 1; i < 19; i++)
                {
                    if(id == i && !item.dureeJ.Equals(Request["type"]))
                    {
                        item.dureeJ = Request["type"];
                        db.SaveChanges();
                    }
                }
               
            }


                /*typeconge a1 = db.typeconge.Find(1);
                a1.dureeJ = Request["x1"];
                db.Entry(a1).State = EntityState.Modified;
                db.SaveChanges();
                //
                typeconge a2 = db.typeconge.Find(2);
                a2.dureeJ = Request["x2"];
                db.Entry(a2).State = EntityState.Modified;
                db.SaveChanges();
                // 
                typeconge a3 = db.typeconge.Find(3);
                a3.dureeJ = Request["x3"];
                db.Entry(a3).State = EntityState.Modified;
                db.SaveChanges();
                // 
                typeconge a4 = db.typeconge.Find(4);
                a4.dureeJ = Request["x4"];
                db.Entry(a4).State = EntityState.Modified;
                db.SaveChanges();
                //
                typeconge a5 = db.typeconge.Find(5);
                a5.dureeJ = Request["x5"];
                db.Entry(a5).State = EntityState.Modified;
                db.SaveChanges();
                //
                typeconge a6 = db.typeconge.Find(6);
                a6.dureeJ = Request["x6"];
                db.Entry(a6).State = EntityState.Modified;
                db.SaveChanges();
                //
                typeconge a7 = db.typeconge.Find(7);
                a7.dureeJ = Request["x7"];
                db.Entry(a7).State = EntityState.Modified;
                db.SaveChanges();
                //
                typeconge a8 = db.typeconge.Find(8);
                a8.dureeJ = Request["x8"];
                db.Entry(a8).State = EntityState.Modified;
                db.SaveChanges();
                //
                typeconge a9 = db.typeconge.Find(9);
                a9.dureeJ = Request["x9"];
                db.Entry(a9).State = EntityState.Modified;
                db.SaveChanges();
                //
                typeconge a10 = db.typeconge.Find(10);
                a10.dureeJ = Request["x10"];
                db.Entry(a10).State = EntityState.Modified;
                db.SaveChanges();
                //
                typeconge a11 = db.typeconge.Find(11);
                a11.dureeJ = Request["x11"];
                db.Entry(a11).State = EntityState.Modified;
                db.SaveChanges();
                //
                typeconge a12 = db.typeconge.Find(12);
                a12.dureeJ = Request["x12"];
                db.Entry(a12).State = EntityState.Modified;
                db.SaveChanges();
                //
                typeconge a13 = db.typeconge.Find(13);
                a13.dureeJ = Request["x13"];
                db.Entry(a13).State = EntityState.Modified;
                db.SaveChanges();
                //
                typeconge a14 = db.typeconge.Find(14);
                a14.dureeJ = Request["x14"];
                db.Entry(a14).State = EntityState.Modified;
                db.SaveChanges();
                //
                typeconge a15 = db.typeconge.Find(15);
                a15.dureeJ = Request["x15"];
                db.Entry(a15).State = EntityState.Modified;
                db.SaveChanges();

                typeconge a20 = db.typeconge.Find(20);
                a20.dureeJ = Request["x20"];
                db.Entry(a20).State = EntityState.Modified;
                db.SaveChanges();
                //
                typeconge a21 = db.typeconge.Find(21);
                a21.dureeJ = Request["x21"];
                db.Entry(a21).State = EntityState.Modified;
                db.SaveChanges();
                //
                typeconge a22 = db.typeconge.Find(22);
                a22.dureeJ = Request["x22"];
                db.Entry(a22).State = EntityState.Modified;
                db.SaveChanges();*/
                //
                return RedirectToAction("Index");
        }
        // GET: RH/typeconges/Create

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