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
    public class typecongesController : Controller
    {
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();

        // GET: RH/typeconges
        public ActionResult Index()
        {
            // typeconge t = new typeconge();
            var t = db.typeconge;
            foreach (var item in t)
            {
                if (item.idtypeConge == 1)
                {
                    ViewBag.t1 = item.dureeJ;
                }
                if (item.idtypeConge == 2)
                {
                    ViewBag.t2 = item.dureeJ;
                }
                if (item.idtypeConge == 3)
                {
                    ViewBag.t3 = item.dureeJ;
                }


                if (item.idtypeConge == 4)
                {
                    ViewBag.t4 = item.dureeJ;
                }
                if (item.idtypeConge == 5)
                {
                    ViewBag.t5 = item.dureeJ;
                }
                if (item.idtypeConge == 6)
                {
                    ViewBag.t6 = item.dureeJ;
                }


               
                if (item.idtypeConge == 9)
                {
                    ViewBag.t9 = item.dureeJ;
                }


                
                if (item.idtypeConge == 11)
                {
                    ViewBag.t11 = item.dureeJ;
                }
                if (item.idtypeConge == 12)
                {
                    ViewBag.t12 = item.dureeJ;
                }


                if (item.idtypeConge == 13)
                {
                    ViewBag.t13 = item.dureeJ;
                }
                if (item.idtypeConge == 14)
                {
                    ViewBag.t14 = item.dureeJ;
                }
                if (item.idtypeConge == 15)
                {
                    ViewBag.t15 = item.dureeJ;
                }

                /*
                 *  if (item.idtypeConge == 7)
                {
                    ViewBag.t7 = item.dureeJ;
                }
                if (item.idtypeConge == 8)
                {
                    ViewBag.t8 = item.dureeJ;
                }
                 * if (item.idtypeConge == 10)
                {
                    ViewBag.t10 = item.dureeJ;
                }
                if (item.idtypeConge == 16)
                {
                    ViewBag.t16 = item.dureeJ;
                }
                if (item.idtypeConge == 17)
                {
                    ViewBag.t17 = item.dureeJ;
                }
                if (item.idtypeConge == 18)
                {
                    ViewBag.t18 = item.dureeJ;
                }

                if (item.idtypeConge == 19)
                {
                    ViewBag.t19 = item.dureeJ;
                }
                 if (item.idtypeConge == 21)
                {
                    ViewBag.t21 = item.dureeJ;
                }

                */
                if (item.idtypeConge == 20)
                {
                    ViewBag.t20 = item.dureeJ;
                }
               

                if (item.idtypeConge == 22)
                {
                    ViewBag.t22 = item.dureeJ;
                }



            }

            return View(t.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Modifier()
        {
           

            typeconge a1= db.typeconge.Find(1);
            a1.dureeJ = Request["x1"];
            db.Entry(a1).State = EntityState.Modified;
            db.SaveChanges();
            //
            typeconge a2 = db.typeconge.Find(2);
            a2.dureeJ = Request["x2"];
            db.Entry(a2).State = EntityState.Modified;
            db.SaveChanges();
            // 
            typeconge a3= db.typeconge.Find(3);
            a3.dureeJ = Request["x3"];
            db.Entry(a3).State = EntityState.Modified;
            db.SaveChanges();
            // 
            typeconge a4= db.typeconge.Find(4);
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
           
            typeconge a9 = db.typeconge.Find(9);
            a9.dureeJ = Request["x9"];
            db.Entry(a9).State = EntityState.Modified;
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
            /*
            typeconge a16 = db.typeconge.Find(16);
            a16.dureeJ = Request["x16"];
            db.Entry(a16).State = EntityState.Modified;
            db.SaveChanges();
            //
            typeconge a17 = db.typeconge.Find(17);
            a17.dureeJ = Request["x17"];
            db.Entry(a17).State = EntityState.Modified;
            db.SaveChanges();
            //
            typeconge a18 = db.typeconge.Find(18);
            a18.dureeJ = Request["x18"];
            db.Entry(a18).State = EntityState.Modified;
            db.SaveChanges();
            //
            typeconge a19 = db.typeconge.Find(19);
            a19.dureeJ = Request["x19"];
            db.Entry(a19).State = EntityState.Modified;
            db.SaveChanges();
              typeconge a21 = db.typeconge.Find(21);
            a21.dureeJ = Request["x21"];
            db.Entry(a21).State = EntityState.Modified;
            db.SaveChanges();
            //
            */
            typeconge a20 = db.typeconge.Find(20);
            a20.dureeJ = Request["x20"];
            db.Entry(a20).State = EntityState.Modified;
            db.SaveChanges();
            //
          
            typeconge a22 = db.typeconge.Find(22);
            a22.dureeJ = Request["x22"];
            db.Entry(a22).State = EntityState.Modified;
            db.SaveChanges();
            //








            return RedirectToAction("Index");
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
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
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

        // POST: RH/typeconges/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
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
