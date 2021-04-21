using GestionAbscences.Data;
using GestionAbscences.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GestionAbscences.Services;
using System.Net;

namespace GestionAbscences.Controllers
{
    public class employeController : Controller
    {
        private GestionAbscencesEntities3 db = new GestionAbscencesEntities3();

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
                //int uid = int.Parse(Session["matricule"].ToString());
                int uid = int.Parse(Session["idEmploye"].ToString());

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


            int uid = int.Parse(Session["idEmploye"].ToString());

            string typeCongeIdTypeconge = Request.Form["typeCongeIdTypeconge"];
            string dateDebut = Request["dateDebut"] + " " + Request["timeDebut"];
            string dateFin = Request["dateFin"] + " " + Request["timeFin"];
            string justification = Request["justification"];


            DateTime debut = Convert.ToDateTime(Request["dateDebut"]);
            DateTime fin = Convert.ToDateTime(Request["dateFin"]);
            //le nb de jour choisi dans le form
            TimeSpan objTimeSpan = fin - debut;
            Session["objTimeSpan"] = objTimeSpan.ToString();

            double jours = Convert.ToDouble(Session["nbjours"].ToString());
            double j = 24;

            double j0 = 0;
            double j10 = 14;
            double j7 = 7;
            double j1 = 1;
            double j2 = 2;

            double j15 = 1680;
            double j05 = 720;

            TimeSpan tm = TimeSpan.FromMinutes(j05); //j05:720
            TimeSpan tm1 = TimeSpan.FromMinutes(j15);//j15:1680


            TimeSpan tt = TimeSpan.FromDays(jours); //jurs: nbjours
            TimeSpan t = TimeSpan.FromDays(j);//24
            TimeSpan t0 = TimeSpan.FromDays(j0);//0
            TimeSpan t10 = TimeSpan.FromDays(j10);//14
            TimeSpan t7 = TimeSpan.FromDays(j7);//7
            //pour 1j
            TimeSpan t1 = TimeSpan.FromDays(j1);//1
            //pour 2j
            TimeSpan t2 = TimeSpan.FromDays(j2);//2
            //pour la demi journée
            TimeSpan t12 = TimeSpan.FromHours(12);
            //pour la jour et demi
            TimeSpan t112 = TimeSpan.FromHours(36);

            if (objTimeSpan > t0 && objTimeSpan < t)
            {
                Session["total"] = "hi;";
            }
            else
            {

                Session["total"] = "bye";
            }


            DateTime dc = DateTime.Now;
            demande.ValidationN1 = "En cours";
            demande.ValidationN2 = "En cours";
            // Session["validation"] = "En cours";
            Session["validation"] = demande.ValidationN2;
            demande.IdEmploye = uid;

            demande.DateDebut = Convert.ToDateTime(dateDebut);
            demande.DateFin = Convert.ToDateTime(dateFin);
            demande.DateDC = dc;
            demande.justification = justification;

            if (typeCongeIdTypeconge.Equals("reliquat") && objTimeSpan > t0 && objTimeSpan <= t7)
            {

                demande.IdtypeConge = 1;
            }
            else if (typeCongeIdTypeconge.Equals("1 ere tranche") && objTimeSpan > t10 && objTimeSpan <= t) //obj <JR ,obj >=10
            {
                demande.IdtypeConge = 2;
            }
            else if (typeCongeIdTypeconge.Equals("2 eme tranche") && objTimeSpan > t7 && objTimeSpan <= t10)//obj <7(t7) , obj <=jR
            {
                demande.IdtypeConge = 3;
            }
            else if (typeCongeIdTypeconge.Equals("1/2 journée") && objTimeSpan <= t12)
            {
                demande.IdtypeConge = 7;
            }
            else if (typeCongeIdTypeconge.Equals("1 journée") && objTimeSpan == t1)
            {
                demande.IdtypeConge = 8;
            }
            else if (typeCongeIdTypeconge.Equals("1.5 journée") && objTimeSpan <= t112)
            {
                demande.IdtypeConge = 9;
            }
            else if (typeCongeIdTypeconge.Equals("2 journée") && objTimeSpan == t2)
            {
                demande.IdtypeConge = 10;
            }
            else if (typeCongeIdTypeconge.Equals("Mariage") && justification != null)
            {
                demande.IdtypeConge = 4;
            }
            else if (typeCongeIdTypeconge.Equals("Naissance") && justification != null)
            {
                demande.IdtypeConge = 5;
            }
            else if (typeCongeIdTypeconge.Equals("Décès") && justification != null)
            {
                demande.IdtypeConge = 6;
            }
            else if (typeCongeIdTypeconge.Equals("H.S"))
            {
                demande.IdtypeConge = 11;
            }
            else if (typeCongeIdTypeconge.Equals("J.R"))
            {
                demande.IdtypeConge = 14;
            }
            else if (typeCongeIdTypeconge.Equals("J.F"))
            {
                demande.IdtypeConge = 13;
            }
            else if (typeCongeIdTypeconge.Equals("heures"))
            {
                demande.IdtypeConge = 12;
            }
            else if (typeCongeIdTypeconge.Equals("jours"))
            {
                demande.IdtypeConge = 15;
            }
            else
            {
                Session["demande"] = "Vérifier vos information svp  !";
                return RedirectToAction("Index", "Default");
            }

            db.demandeconge.Add(demande);

            db.SaveChanges();

            return RedirectToAction("historique", "employe");

        }

        public ActionResult historique()
        {
            //employe e = new employe();

            string x = Session["matricule"].ToString();

            int x1 = int.Parse(x);


            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x);

            return View(demandeConge.ToList());


        }

        public ActionResult Donnee()
        {

            return View();
        }

        private readonly DemandeService demandeService;

        public employeController()
        {
            demandeService = new DemandeService();
        }
        public ActionResult Imprimer(int? id)
        {

            if (id == null)
            {
                //return RedirectToAction("Index", "Default");
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

    }
    }


