using GestionAbscences.Data;
using GestionAbscences.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace GestionAbscences.Controllers
{
    public class employeController : Controller
    {
         private GestionAbscencesEntities2 db = new GestionAbscencesEntities2();

        // GET: employe
        public ActionResult Index()
        {
 
            return View();
        }


        [HttpGet]
        public ActionResult changePassword()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult changePassword(ChangePasswordModel obj)
        {
            if (ModelState.IsValid)
            {
                int uid = int.Parse(Session["matricule"].ToString());

                employe e = db.employe.Find(uid);

                if (e.password == obj.OldPassword)
                {
                    e.password = obj.NewPassword;
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                     obj.Message = "Your Password is updated successfully";
                    //ViewBag.Success = true;
                  // ViewBag.Message = $"password updated succefully";
                  // return RedirectToAction("Index", "Default");

                }
                else
                {
                   // ViewBag.Message = $"an error occurred while updation password !";
                  obj.Message = "Invalid currrent  Password";
                  // return RedirectToAction("Index", "Default");
                }

            }

            return View(obj);


        }


        [HttpPost]
            public ActionResult Dashboard1()
            {
               demandeconge demande = new demandeconge();


                string employeIdEmploye1 = Request["matricule"];
                string typeCongeIdTypeconge = Request.Form["typeCongeIdTypeconge"];
                string dateDebut = Request["dateDebut"] + " " + Request["timeDebut"];
                string dateFin = Request["dateFin"] + " " + Request["timeFin"];

                DateTime dc = DateTime.Now;


                if (typeCongeIdTypeconge.Equals("reliquat"))
                {
                demande.IdtypeConge = 1;
                }
                else if (typeCongeIdTypeconge.Equals("1 ere tranche"))
                {
                    demande.IdtypeConge = 2;
                }
                else if (typeCongeIdTypeconge.Equals("2 eme tranche"))
                {
                    demande.IdtypeConge = 3;
                }

                demande.ValidationN1 = "En cours";
                demande.ValidationN2 = "En cours";
                demande.IdEmploye = int.Parse(employeIdEmploye1);

                demande.DateDebut = Convert.ToDateTime(dateDebut);
                demande.DateFin = Convert.ToDateTime(dateFin);
                demande.DateDC = dc;

                db.demandeconge.Add(demande);


                db.SaveChanges();

            //ViewBag.Message = "la demande est enregistré !";

            //return "Demande : " + dc;
            return RedirectToAction("historique", "employe");


        }

        public ActionResult historique()
        {
            employe e = new employe();

            string x = Session["matricule"].ToString();

            int x1 = int.Parse(x);


            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.IdEmploye == x1);

            return View(demandeConge.ToList());


        }

        public ActionResult Donnee()
        {

            return View();
        }
    }
    }


