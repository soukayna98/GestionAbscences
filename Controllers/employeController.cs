﻿using GestionAbscences.Data;
using GestionAbscences.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GestionAbscences.Services;
using System.Net;
using Rotativa;
using System.IO;
using System.Data;
using ClosedXML.Excel;

namespace GestionAbscences.Controllers
{
    public class employeController : Controller
    {
         private GestionAbscencesEntities5 db = new GestionAbscencesEntities5();

        // GET: employe
        public ActionResult Index()
        {
            string w = Session["matricule"].ToString();
            //  Session["tranche1"] = ""; Session["test1"] = ""; Session["tranche2"] = ""; Session["test2"] = ""; Session["rel"] = ""; Session["test3"] = "";

            int a = 0;
            int b = 0;
            int c = 0;
            int j = 0;
            int e = 0;
            int f = 0;

            //var employes = demandeService.ReadAll();
            var employes = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == w);

            // 1tranche
            foreach (var item in employes)
            {

                if (item.IdtypeConge == 1 && (item.ValdationRH.Equals("En cours") || item.ValdationRH.Equals("accepte")) && (item.ValidationN1.Equals("En cours") || item.ValidationN1.Equals("accepte")) && (item.ValidationN2.Equals("En cours") || item.ValidationN2.Equals("accepte")))
                {
                    a++;
                    Session["tranche1"] = a;

                }
              
                else
                {
                    Session["tranche1"] =a;
                   
                }


                
            }
            foreach (var item in employes)
            {
                if (item.IdtypeConge == 1 && (item.ValdationRH.Equals("accepte")))
                {

                    b++;
                    Session["test1"] = b;
                }
                else
                {
                    
                    Session["test1"] = a;
                }

            }

            //2 tranche

            foreach (var item in employes)
            {
                if (item.IdtypeConge == 2 && (item.ValdationRH.Equals("En cours") || item.ValdationRH.Equals("accepte")) && (item.ValidationN1.Equals("En cours") || item.ValidationN1.Equals("accepte")) && (item.ValidationN2.Equals("En cours") || item.ValidationN2.Equals("accepte")))
                {
                    c++;
                    Session["tranche2"] = c;

                }
                
                else
                {
                    Session["tranche2"] = c;
                   
                }
               

            }

            foreach (var item in employes)
            {
                if (item.IdtypeConge == 2 && item.ValdationRH.Equals("accepte"))
                {
                    j++;
                    Session["test2"] = j;

                }
                else
                {
                    Session["test2"] = j;
                }

            }

            //reli

            foreach (var item in employes)
            {
                 
                if (item.IdtypeConge == 3 && (item.ValdationRH.Equals("En cours") || item.ValdationRH.Equals("accepte")) && (item.ValidationN1.Equals("En cours") || item.ValidationN1.Equals("accepte")) && (item.ValidationN2.Equals("En cours") || item.ValidationN2.Equals("accepte")))
                {
                    e++;
                    Session["rel"] = e;

                }
                else
                {
                    Session["rel"] = e;
                }

                

           
            }
            foreach (var item in employes)
            {
                if (item.IdtypeConge == 3 && item.ValdationRH.Equals("accepte"))
                {
                    f++;
                    Session["test3"] = f;
                }
                else
                {
                    
                    Session["test3"] = f;
                }
            }


                return View();
        }
       /* [HttpPost]
        public FileResult Export()
        {
            GestionAbscencesEntities5 entities = new GestionAbscencesEntities5();
            string x = Session["matricule"].ToString();
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[8] { new  DataColumn("Date creation"),
                                            new DataColumn("Nom complet"),
                                           new  DataColumn("Matricule"),
                                            new DataColumn("Début"),
                                            new DataColumn("Fin"),
                                            new DataColumn("Validation N+1"),
                                            new DataColumn("Validation N+2"),
                                            new DataColumn("Validation RH") });

         // var demande = from demandeconge in entities.demandeconge.Take(100)
               //         select demandeconge where 

            var demande =  db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x);

            foreach (var d in demande)
            {
                dt.Rows.Add(d.DateDC, d.employe.matricule, d.employe.NomComplet, d.DateDebut, d.DateFin, d.ValidationN1, d.ValidationN2, d.ValdationRH);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }*/


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
            

            [HttpGet]
        public ActionResult changePassword()
        {
            
            return View();
        }

        public ActionResult historique()
        {
            //employe e = new employe();

            string x = Session["matricule"].ToString();

            int x1 = int.Parse(x);
            

            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x);

            return View(demandeConge.ToList());

        }

        [HttpGet]
        public ActionResult Donnée()
        {
            return View();
        }

        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult changePassword(ChangePasswordModel obj)
        {
              if(ModelState.IsValid)
               {
                int uid = int.Parse(Session["idEmploye"].ToString());

                employe e = db.employe.Find(uid);

                if (e.password == obj.OldPassword)
                {
                    e.password = obj.NewPassword;
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    Session["Message"] = "Your Password is updated successfully";
                    return RedirectToAction("Index", "Default");

                }
                else
                {
                    Session["Message"] = "Invalid currrent  Password";
                    return RedirectToAction("Index", "Default");
                }

            }

            return View(obj);


        }
      



        [HttpPost]
        public ActionResult Dashboard1()
        {
            int uid = int.Parse(Session["idEmploye"].ToString());
            demandeconge demande = new demandeconge();
            string dateDebut = Request["dateDebut"] + " " + Request["timeDebut"];
            string dateFin = Request["dateFin"] + " " + Request["timeFin"];

            string typeCongeIdTypeconge = Request.Form["typeCongeIdTypeconge"];
           

            string justification = Request["justification"];


          
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


           
            DateTime dc = DateTime.Now;
            

            if (Request["dateDebut"].Equals("") || Request["dateFin"].Equals("") || typeCongeIdTypeconge.Equals(""))
                {
                    Session["demande"] = "Remlpir tout les champs svp !";
                    return RedirectToAction("Index", "employe");
                }
            DateTime debut = Convert.ToDateTime(Request["dateDebut"]);
            DateTime fin = Convert.ToDateTime(Request["dateFin"]);
            TimeSpan dateSpan = fin - debut;
                if ((typeCongeIdTypeconge.Equals("Mariage") || typeCongeIdTypeconge.Equals("Naissance") || typeCongeIdTypeconge.Equals("Décès")) && justification.Equals(""))
                {
                    Session["demande"] = "Remlir votre observation svp !";
                    return RedirectToAction("Index", "employe");
                }
           

                else if((debut < dc) || (fin < dc) || (fin < debut) )
            {
                Session["demande"] = "vérifier les dates svp!";
                return RedirectToAction("Index", "employe");
            }
                else
                {
                   
                    //  int reste = Convert.ToInt32(dateSpan);

                    if (typeCongeIdTypeconge.Equals("reliquat") && dateSpan > t0 && dateSpan <= tR)
                    {

                        demande.IdtypeConge = 3;
                      
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
                        return RedirectToAction("Index", "employe");
                    }


                


            }


           

          //  DateTime timeD = Convert.ToDateTime(Request["timeDebut"]);
          //  DateTime timeF = Convert.ToDateTime(Request["timeFin"]);

            //le nb de jour choisi dans le form
           
          //  TimeSpan timeSpan = timeF - timeD;

            
            demande.ValidationN1 = "En cours";
            demande.ValidationN2 = "En cours";
            demande.ValdationRH = "En cours";

            demande.IdEmploye = uid;

            demande.DateDebut = Convert.ToDateTime(dateDebut);
            demande.DateFin = Convert.ToDateTime(dateFin);
            demande.DateDC = dc;
            demande.justification = justification;

           
           
           

            db.demandeconge.Add(demande);

            db.SaveChanges();

            return RedirectToAction("historique", "employe");

        }


        [HttpPost]
        public ActionResult demande()
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

            //condition pas encore
            if (id == null || db.demandeconge.Find(id).ValidationN1 == "refuse" || db.demandeconge.Find(id).ValidationN2 == "refuse")
            {
                //return RedirectToAction("Index", "Default");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var currentDemande = demandeService.ReadById(id.Value);
            if (currentDemande == null || db.demandeconge.Find(id).ValidationN1 == "refuse" || db.demandeconge.Find(id).ValidationN2 == "refuse")
            {
                return HttpNotFound($"this demande ({id}) is not found");
            }

            demandeconge demandeconge = db.demandeconge.Find(id);
            Session["uid"] = currentDemande.idDemandeConge;

            if (demandeconge == null || db.demandeconge.Find(id).ValidationN1 == "refuse" || db.demandeconge.Find(id).ValidationN2 == "refuse")
            {
                return HttpNotFound();
            }

            return View(demandeconge);
        }

        public ActionResult PrintAll()
        {
            int uid =int.Parse(Session["uid"].ToString());

            var q = new ActionAsPdf("historique");
            return q;

        }


    }
}


