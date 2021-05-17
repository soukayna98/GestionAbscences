using GestionAbscences.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionAbscences.Areas.Admin.Controllers
{
    public class DemandeCongeController : Controller
    {
        // GET: Admin/DemandeConge
        private GestionAbscencesEntities5 db = new GestionAbscencesEntities5();
        [HttpPost]
        public ActionResult Dashboard1()
        {
            demandeconge demande = new demandeconge();
            string dateDebut = Request["dateDebut"] + " " + Request["timeDebut"];
            string dateFin = Request["dateFin"] + " " + Request["timeFin"];

            string typeCongeIdTypeconge = Request.Form["typeCongeIdTypeconge"];
            DateTime dc = DateTime.Now;

            string justification = Request["justification"];


            int uid = int.Parse(Session["idEmploye"].ToString());
            employe e = db.employe.Find(uid);
            Session["affectatio"] = e.affectation;
            Session["nbjour"] = e.nbjours.ToString();
            Session["nbjourR"] = e.nbjoursR.ToString();
            Session["userNam"] = e.NomComplet;
            Session["matricul"] = e.matricule;



            var x = db.demandeconge.Where(p => p.IdEmploye == uid && p.IdtypeConge == 1);



            double jours = Convert.ToDouble(Session["nbjours"].ToString()) - 1;
            double joursR = Convert.ToDouble(Session["nbjoursR"].ToString()) - 1;

            TimeSpan t = TimeSpan.FromDays(jours); //jurs: nbjours
            TimeSpan tR = TimeSpan.FromDays(joursR); //jurs: nbjoursR
            TimeSpan t0 = TimeSpan.FromDays(0);//0
            TimeSpan t10 = TimeSpan.FromDays(9);//10
            TimeSpan t7 = TimeSpan.FromDays(6);//7
                                               //pour 1j
            TimeSpan t1 = TimeSpan.FromDays(1);//1
                                               //pour 2j
            TimeSpan t2 = TimeSpan.FromDays(2);//2
                                               //pour la demi journée
            TimeSpan t12 = TimeSpan.FromHours(12);
            //pour la jour et demi
            TimeSpan t112 = TimeSpan.FromHours(36);




            if (Request["dateDebut"].Equals("") || Request["dateFin"].Equals("") || typeCongeIdTypeconge.Equals(""))
            {
                Session["demande"] = "Remlpir tout les champs svp !";
                return RedirectToAction("Index", "Default");
            }
            else if ((typeCongeIdTypeconge.Equals("Mariage") || typeCongeIdTypeconge.Equals("Naissance") || typeCongeIdTypeconge.Equals("Décès")) && justification.Equals(""))
            {
                Session["demande"] = "Remlir la  justification svp !";
                return RedirectToAction("Index", "Default");
            }
            else
            {
                DateTime debut = Convert.ToDateTime(Request["dateDebut"]);
                DateTime fin = Convert.ToDateTime(Request["dateFin"]);
                TimeSpan dateSpan = fin - debut;
                //  int reste = Convert.ToInt32(dateSpan);

                if (typeCongeIdTypeconge.Equals("reliquat") && dateSpan > t0 && dateSpan < t7)
                {

                    demande.IdtypeConge = 3;
                    /* e.nbjoursR = e.nbjoursR - reste;
                     db.Entry(e).State = EntityState.Modified;
                     db.SaveChanges();*/
                }
                else if (typeCongeIdTypeconge.Equals("1 ere tranche") && dateSpan >= t10 && dateSpan <= t) //obj <JR ,obj >=10
                {
                    demande.IdtypeConge = 1;

                }
                else if (typeCongeIdTypeconge.Equals("2 eme tranche") && dateSpan >= t7 && dateSpan <= tR)//obj <7(t7) , obj <=jR
                {
                    demande.IdtypeConge = 2;

                }
                else if (typeCongeIdTypeconge.Equals("1/2 journée") && dateSpan <= t12)
                {
                    demande.IdtypeConge = 7;

                }
                else if (typeCongeIdTypeconge.Equals("1 journée") && dateSpan == t1)
                {
                    demande.IdtypeConge = 8;

                }
                else if (typeCongeIdTypeconge.Equals("1.5 journée") && dateSpan <= t112)
                {
                    demande.IdtypeConge = 9;

                }
                else if (typeCongeIdTypeconge.Equals("2 journée") && dateSpan == t2)
                {
                    demande.IdtypeConge = 10;
                    //  e.nbjoursR = e.nbjoursR - 2;
                    //  db.Entry(e).State = EntityState.Modified;
                    //  db.SaveChanges();
                }
                else if (typeCongeIdTypeconge.Equals("Mariage") && !(justification.Equals("")))
                {

                    demande.IdtypeConge = 4;
                }
                else if (typeCongeIdTypeconge.Equals("Naissance") && !(justification.Equals("")))
                {
                    demande.IdtypeConge = 5;
                }
                else if (typeCongeIdTypeconge.Equals("Décès") && !(justification.Equals("")))
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
                    Session["demande"] = "Vérifier vos données svp !";
                    return RedirectToAction("Index", "Default");
                }





            }




            //  DateTime timeD = Convert.ToDateTime(Request["timeDebut"]);
            //  DateTime timeF = Convert.ToDateTime(Request["timeFin"]);

            //le nb de jour choisi dans le form

            //  TimeSpan timeSpan = timeF - timeD;


            demande.ValidationN1 = "Accepte";
            demande.ValidationN2 = "En cours";
            demande.ValidationRH = "En cours";

            demande.IdEmploye = uid;

            demande.DateDebut = Convert.ToDateTime(dateDebut);
            demande.DateFin = Convert.ToDateTime(dateFin);
            demande.DateDC = dc;
            demande.justification = justification;





            db.demandeconge.Add(demande);

            db.SaveChanges();

            return RedirectToAction("historique", "employe");

        }

        public ActionResult modifier(int? id)
        {
            demandeconge demandeconge = db.demandeconge.Find(id);
            Session["modifierID"] = id;

            return View();
        }
        [HttpPost]
        public ActionResult modifier()
        {
            int uid = int.Parse(Session["modifierID"].ToString());
            demandeconge e = db.demandeconge.Find(uid);
            string button = Request["modifier"];
            string dateDebut = Request["dateDebut"] + " " + Request["timeDebut"];
            string dateFin = Request["dateFin"] + " " + Request["timeFin"];
            DateTime dc = DateTime.Now;
            switch (button)
            {
                case "valide":
                    e.DateDebut = Convert.ToDateTime(dateDebut);
                    e.DateFin = Convert.ToDateTime(dateFin);
                    e.DateDC = dc;
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("historique");

                case "annule":

                    return RedirectToAction("historique");
                default:
                    return View();

            }

        }

    
}
}