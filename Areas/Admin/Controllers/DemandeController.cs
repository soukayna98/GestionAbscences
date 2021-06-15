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
using Rotativa;
using System.IO;
using System.Data;
using ClosedXML.Excel;

namespace GestionAbscences.Areas.Admin.Controllers
{
   

    public class DemandeController : Controller
    {
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();
        private readonly DemandeService demandeService;


        // GET: Admin/Demande
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
                    Session["tranche1"] = a;

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

                    Session["test1"] = b;
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
        public ActionResult historique()
        {
            Session["demande"] = null;


            string x = Session["matricule"].ToString();

            int x1 = int.Parse(x);
           
            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x).OrderByDescending(news => news.DateDC).Take(10).ToList();

            return View(demandeConge);

        }



        [HttpPost]
        public ActionResult Dashboard1()
        {
            Session["Message"] = null;

            //donnée from index (view)
            int uid = int.Parse(Session["idEmploye"].ToString());

            employe e = db.employe.Find(uid);

            demandeconge demande = new demandeconge();
            string dateDebut = Request["dateDebut"];
            string timeDebut = Request["timeDebut"];
            string dateFin = Request["dateFin"];
            string timeFin = Request["timeFin"];


            string operation = Request["operation"].ToString();
            string marriage = Request["marriage1"].ToString();
            string deces = Request["deces1"].ToString();
            string typeCongeIdTypeconge = Request.Form["typeCongeIdTypeconge"];
            string justification = Request["justification"];
            DateTime dc = DateTime.Now;




            // ---------- Recûperation 

            

            var recup = db.CumulRecup.Include(d => d.employe).Where(p => p.employe.idEmploye == uid).Select(u => new {
                hs = u.CumulHr,
                jf = u.CumulJrF,
                jr = u.CumulJrR

            }).Single();

            double hsA = Convert.ToDouble(recup.hs);
            Session["h"] = hsA;
            TimeSpan ths = TimeSpan.FromHours(hsA);

            double jf = Convert.ToDouble(recup.jf);
            Session["j"] = jf;
            TimeSpan tjf = TimeSpan.FromDays(jf);

            double jR = Convert.ToDouble(recup.jr);
            Session["h"] = hsA;
            TimeSpan tjR = TimeSpan.FromDays(jR);




            //condition de date

            double jours = Convert.ToDouble(Session["nbjours"].ToString()) - 1;
            double joursR = Convert.ToDouble(Session["nbjoursR"].ToString()) - 1;
            TimeSpan t = TimeSpan.FromDays(jours); //jurs: nbjours
            TimeSpan tR = TimeSpan.FromDays(joursR); //jurs: nbjoursR
            TimeSpan t0 = TimeSpan.FromDays(0);//0
            TimeSpan t10 = TimeSpan.FromDays(9);//10
            TimeSpan t7 = TimeSpan.FromDays(6);//7
            TimeSpan t1 = TimeSpan.FromDays(1);//1
            TimeSpan t2 = TimeSpan.FromDays(2);//2
            TimeSpan t12 = TimeSpan.FromHours(12);//pour la demi journée
            TimeSpan t112 = TimeSpan.FromHours(36);  //pour la jour et demi



            //duree
            var l = db.typeconge;

            if (Request["dateDebut"].Equals("") || typeCongeIdTypeconge.Equals(""))
            {

                Session["Message"] = "Remlpir tout les champs svp";
                // ViewBag.Message = "Remlpir tout les champs svp !";
                return RedirectToAction("Index", "Demande");

            }
            //date debut 
            DateTime debut = Convert.ToDateTime(Request["dateDebut"]);


            if (typeCongeIdTypeconge.Equals("Opération chirurgicale") || typeCongeIdTypeconge.Equals("Décès") || typeCongeIdTypeconge.Equals("Mariage"))
            {
                if (debut < dc)
                {
                    Session["Message"] = "Verifier vos choix svp!";
                    return RedirectToAction("Index", "Demande");
                }
                else if (typeCongeIdTypeconge.Equals("Mariage") && !(marriage.Equals("")))
                {

                    if (marriage.Equals("1"))
                    {
                        foreach (var item in l)
                        {
                            if (item.idtypeConge == 4)
                            {

                                int x4 = int.Parse(item.dureeJ) - 1;
                                var df = debut.AddDays(x4);
                                demande.DateFin = Convert.ToDateTime(df);
                                demande.DateDebut = Convert.ToDateTime(dateDebut);
                                demande.IdtypeConge = 4;
                            }
                        }


                    }
                    if (marriage.Equals("2"))
                    {
                        foreach (var item in l)
                        {
                            if (item.idtypeConge == 5)
                            {

                                int x5 = int.Parse(item.dureeJ) - 1;
                                var df = debut.AddDays(x5);
                                demande.DateFin = Convert.ToDateTime(df);
                                demande.DateDebut = Convert.ToDateTime(dateDebut);
                                demande.IdtypeConge = 5;
                            }
                        }
                    }
                }

                else if (typeCongeIdTypeconge.Equals("Décès") && !(deces.Equals("")))
                {
                    if (deces.Equals("1"))
                    {
                        foreach (var item in l)
                        {
                            if (item.idtypeConge == 6)
                            {

                                int x6 = int.Parse(item.dureeJ) - 1;
                                var df = debut.AddDays(x6);
                                demande.DateFin = Convert.ToDateTime(df);
                                demande.DateDebut = Convert.ToDateTime(dateDebut);
                                demande.IdtypeConge = 6;
                            }
                        }

                    }
                    if (deces.Equals("2"))
                    {
                        foreach (var item in l)
                        {
                            if (item.idtypeConge == 7)
                            {

                                int x7 = int.Parse(item.dureeJ) - 1;
                                var df = debut.AddDays(x7);
                                demande.DateFin = Convert.ToDateTime(df);
                                demande.DateDebut = Convert.ToDateTime(dateDebut);
                                demande.IdtypeConge = 7;
                            }
                        }

                    }
                    if (deces.Equals("3"))
                    {
                        foreach (var item in l)
                        {
                            if (item.idtypeConge == 8)
                            {

                                int x8 = int.Parse(item.dureeJ) - 1;
                                var df = debut.AddDays(x8);
                                demande.DateFin = Convert.ToDateTime(df);
                                demande.DateDebut = Convert.ToDateTime(dateDebut);
                                demande.IdtypeConge = 8;
                            }
                        }

                    }
                    if (deces.Equals("4"))
                    {
                        foreach (var item in l)
                        {
                            if (item.idtypeConge == 9)
                            {

                                int x9 = int.Parse(item.dureeJ) - 1;
                                var df = debut.AddDays(x9);
                                demande.DateFin = Convert.ToDateTime(df);
                                demande.DateDebut = Convert.ToDateTime(dateDebut);
                                demande.IdtypeConge = 9;
                            }
                        }

                    }
                    if (deces.Equals("5"))
                    {
                        foreach (var item in l)
                        {
                            if (item.idtypeConge == 10)
                            {

                                int x10 = int.Parse(item.dureeJ) - 1;
                                var df = debut.AddDays(x10);
                                demande.DateFin = Convert.ToDateTime(df);
                                demande.DateDebut = Convert.ToDateTime(dateDebut);
                                demande.IdtypeConge = 10;
                            }
                        }
                    }
                }
                else if (typeCongeIdTypeconge.Equals("Opération chirurgicale") && !(operation.Equals("")))
                {
                    if (operation.Equals("1"))
                    {
                        foreach (var item in l)
                        {
                            if (item.idtypeConge == 20)
                            {

                                int x20 = int.Parse(item.dureeJ) - 1;
                                var df = debut.AddDays(x20);
                                demande.DateFin = Convert.ToDateTime(df);
                                demande.DateDebut = Convert.ToDateTime(dateDebut);
                                demande.IdtypeConge = 20;
                            }
                        }
                    }
                    if (operation.Equals("2"))
                    {
                        foreach (var item in l)
                        {
                            if (item.idtypeConge == 21)
                            {

                                int x21 = int.Parse(item.dureeJ) - 1;
                                var df = debut.AddDays(x21);
                                demande.DateFin = Convert.ToDateTime(df);
                                demande.DateDebut = Convert.ToDateTime(dateDebut);
                                demande.IdtypeConge = 21;
                            }
                        }
                    }

                }




            }
            else if (typeCongeIdTypeconge.Equals("Naissance"))
            {
                foreach (var item in l)
                {
                    if (item.idtypeConge == 22)
                    {

                        int x22 = int.Parse(item.dureeJ) - 1;
                        var df = debut.AddDays(x22);
                        demande.DateFin = Convert.ToDateTime(df);
                        demande.DateDebut = Convert.ToDateTime(dateDebut);
                        demande.IdtypeConge = 22;
                    }
                }
            }
            else if (typeCongeIdTypeconge.Equals("Circoncision"))
            {
                foreach (var item in l)
                {
                    if (item.idtypeConge == 15)
                    {

                        int x15 = int.Parse(item.dureeJ) - 1;
                        var df = debut.AddDays(x15);
                        demande.DateFin = Convert.ToDateTime(df);
                        demande.DateDebut = Convert.ToDateTime(dateDebut);
                        demande.IdtypeConge = 15;
                    }
                }
            }
            else if (typeCongeIdTypeconge.Equals("1/2 journée") || typeCongeIdTypeconge.Equals("1.5 journée") || typeCongeIdTypeconge.Equals("H.S"))
            {

                if (Request["dateDebut"].Equals("") || Request["dateFin"].Equals("") || Request["timeDebut"].Equals("") || Request["timeFin"].Equals(""))
                {
                    Session["Message"] = "Remlpir tout les champs svp ";
                    return RedirectToAction("Index", "Demande");
                }

                // concatenation-------------------------------------------------------------------------------------------------

                DateTime dtd1 = DateTime.Parse(Request["dateDebut"] + " " + Request["timeDebut"]);
                DateTime dtf1 = DateTime.Parse(Request["dateFin"] + " " + Request["timeFin"]);
                TimeSpan timeSpan = dtf1 - dtd1;

                if (typeCongeIdTypeconge.Equals("1/2 journée") && timeSpan <= t12)
                {

                    // demande.DateFin = Convert.ToDateTime(dateFin);
                    demande.DateFin = Convert.ToDateTime(dtf1);
                    demande.DateDebut = Convert.ToDateTime(dtd1);
                    demande.IdtypeConge = 11;

                }
                else if (typeCongeIdTypeconge.Equals("1.5 journée") && timeSpan <= t112)
                {
                    //demande.DateFin = Convert.ToDateTime(dateFin);
                    demande.DateFin = Convert.ToDateTime(dtf1);
                    demande.DateDebut = Convert.ToDateTime(dtd1);
                    demande.IdtypeConge = 13;

                }
                else if (typeCongeIdTypeconge.Equals("H.S") && timeSpan <= ths)
                {

                    demande.IdtypeConge = 23;
                    demande.DateFin = Convert.ToDateTime(dtf1);
                    demande.DateDebut = Convert.ToDateTime(dtd1);



                }
            }
            else
            {
                if (Request["dateDebut"].Equals("") || Request["dateFin"].Equals(""))
                {
                    Session["Message"] = "Remlpir tout les champs svp ";
                    return RedirectToAction("Index", "Demande");
                }
                DateTime fin = Convert.ToDateTime(Request["dateFin"]);

                TimeSpan dateSpan = fin - debut;





                if (typeCongeIdTypeconge.Equals(""))
                {
                    Session["Message"] = "Selectionner un type de conge svp ";
                    return RedirectToAction("Index", "Demande");
                }
                else
               if ((debut < dc) || (fin < dc) || (fin < debut))


                {
                    Session["Message"] = "verifier les dates svp";
                    return RedirectToAction("Index", "Demande");
                }

                else
                {


                    if (typeCongeIdTypeconge.Equals("reliquat") && dateSpan > t0 && dateSpan <= tR)
                    {

                        demande.IdtypeConge = 3;
                        demande.DateFin = Convert.ToDateTime(dateFin);
                        demande.DateDebut = Convert.ToDateTime(dateDebut);

                    }
                    else if (typeCongeIdTypeconge.Equals("1 ere tranche") && dateSpan >= t10 && dateSpan <= t) //obj <JR ,obj >=10
                    {

                        demande.IdtypeConge = 1;
                        demande.DateFin = Convert.ToDateTime(dateFin);
                        demande.DateDebut = Convert.ToDateTime(dateDebut);

                        //****
                        var employe = db.employe.Find(uid);
                        if (employe.sexe.Equals("Homme"))
                        {
                            if (employe.Status.Equals("celibataire"))
                            {
                                if (employe.Classe.Equals("Agent de Maitrise"))
                                {
                                    demande.soldeConge = "700";
                                }
                                else if (employe.Classe.Equals("OE"))
                                {
                                    demande.soldeConge = "600";
                                }
                            }
                            else if (employe.Status.Equals("Marie") && employe.nbEnfants == 0)
                            {
                                if (employe.Classe.Equals("Agent de Maitrise"))
                                {
                                    demande.soldeConge = "950";
                                }
                                else if (employe.Classe.Equals("OE"))
                                {
                                    demande.soldeConge = "800";
                                }
                            }
                            else if (employe.Status.Equals("Marie") && employe.nbEnfants == 1)
                            {
                                if (employe.Classe.Equals("Agent de Maitrise"))
                                {
                                    demande.soldeConge = "1250";
                                }
                                else if (employe.Classe.Equals("OE"))
                                {
                                    demande.soldeConge = "1000";
                                }
                            }
                            else if (employe.Status.Equals("Marie") && employe.nbEnfants == 2)
                            {
                                if (employe.Classe.Equals("Agent de Maitrise"))
                                {
                                    demande.soldeConge = "1550";
                                }
                                else if (employe.Classe.Equals("OE"))
                                {
                                    demande.soldeConge = "1200";
                                }
                            }
                            else if (employe.Status.Equals("Marie") && employe.nbEnfants == 3)
                            {
                                if (employe.Classe.Equals("Agent de Maitrise"))
                                {
                                    demande.soldeConge = "1850";
                                }
                                else if (employe.Classe.Equals("OE"))
                                {
                                    demande.soldeConge = "1400";
                                }
                            }
                            else if (employe.Status.Equals("Marie") && employe.nbEnfants == 4)
                            {
                                if (employe.Classe.Equals("Agent de Maitrise"))
                                {
                                    demande.soldeConge = "2150";
                                }
                                else if (employe.Classe.Equals("OE"))
                                {
                                    demande.soldeConge = "1600";
                                }
                            }
                            else if (employe.Status.Equals("Marie") && employe.nbEnfants == 5)
                            {
                                if (employe.Classe.Equals("Agent de Maitrise"))
                                {
                                    demande.soldeConge = "2450";
                                }
                                else if (employe.Classe.Equals("OE"))
                                {
                                    demande.soldeConge = "1800";
                                }
                            }
                            else if (employe.Status.Equals("Marie") && employe.nbEnfants == 6)
                            {
                                if (employe.Classe.Equals("Agent de Maitrise"))
                                {
                                    demande.soldeConge = "2750";
                                }
                                else if (employe.Classe.Equals("OE"))
                                {
                                    demande.soldeConge = "2000";
                                }
                            }

                        }
                        else if (employe.sexe.Equals("Femme"))
                        {
                            if (employe.Status.Equals("celibataire"))
                            {
                                if (employe.Classe.Equals("Agent de Maitrise"))
                                {
                                    demande.soldeConge = "700";
                                }
                                else if (employe.Classe.Equals("OE"))
                                {
                                    demande.soldeConge = "600";
                                }
                            }
                            else if (employe.Status.Equals("Marie") && employe.nbEnfants == 0)
                            {
                                if (employe.Classe.Equals("Agent de Maitrise"))
                                {
                                    demande.soldeConge = "950";
                                }
                                else if (employe.Classe.Equals("OE"))
                                {
                                    demande.soldeConge = "800";
                                }
                            }
                            else if (employe.Status.Equals("Marie") && employe.nbEnfants == 1)
                            {
                                if (employe.Classe.Equals("Agent de Maitrise"))
                                {
                                    demande.soldeConge = "1250";
                                }
                                else if (employe.Classe.Equals("OE"))
                                {
                                    demande.soldeConge = "1000";
                                }
                            }
                            else if (employe.Status.Equals("Marie") && employe.nbEnfants == 2)
                            {
                                if (employe.Classe.Equals("Agent de Maitrise"))
                                {
                                    demande.soldeConge = "1550";
                                }
                                else if (employe.Classe.Equals("OE"))
                                {
                                    demande.soldeConge = "1200";
                                }
                            }
                            else if (employe.Status.Equals("Marie") && employe.nbEnfants == 3)
                            {
                                if (employe.Classe.Equals("Agent de Maitrise"))
                                {
                                    demande.soldeConge = "1850";
                                }
                                else if (employe.Classe.Equals("OE"))
                                {
                                    demande.soldeConge = "1400";
                                }
                            }
                            else if (employe.Status.Equals("Marie") && employe.nbEnfants == 4)
                            {
                                if (employe.Classe.Equals("Agent de Maitrise"))
                                {
                                    demande.soldeConge = "2150";
                                }
                                else if (employe.Classe.Equals("OE"))
                                {
                                    demande.soldeConge = "1600";
                                }
                            }
                            else if (employe.Status.Equals("Marie") && employe.nbEnfants == 5)
                            {
                                if (employe.Classe.Equals("Agent de Maitrise"))
                                {
                                    demande.soldeConge = "2450";
                                }
                                else if (employe.Classe.Equals("OE"))
                                {
                                    demande.soldeConge = "1800";
                                }
                            }
                            else if (employe.Status.Equals("Marie") && employe.nbEnfants == 6)
                            {
                                if (employe.Classe.Equals("Agent de Maitrise"))
                                {
                                    demande.soldeConge = "2750";
                                }
                                else if (employe.Classe.Equals("OE"))
                                {
                                    demande.soldeConge = "2000";
                                }
                            }
                            else
                            {
                                Session["Message"] = "Verifier vos donnees svp ";
                                return RedirectToAction("Index", "Demande");
                            }
                        }



                        //******solde



                    }
                    else if (typeCongeIdTypeconge.Equals("2 eme tranche") && dateSpan >= t7 && dateSpan <= tR)//obj <7(t7) , obj <=jR
                    {
                        demande.IdtypeConge = 2;
                        demande.DateFin = Convert.ToDateTime(dateFin);
                        demande.DateDebut = Convert.ToDateTime(dateDebut);
                    }
                    //èèèèèèèèèèèèèèèèèèèè

                    else if (typeCongeIdTypeconge.Equals("1 journée") && dateSpan == t1)
                    {
                        demande.DateFin = Convert.ToDateTime(dateFin);
                        demande.IdtypeConge = 12;
                        demande.DateDebut = Convert.ToDateTime(dateDebut);

                    }
                    //---------------------------------------

                    else if (typeCongeIdTypeconge.Equals("2 journée") && dateSpan == t2)
                    {
                        demande.DateFin = Convert.ToDateTime(dateFin);
                        demande.IdtypeConge = 14;
                        demande.DateDebut = Convert.ToDateTime(dateDebut);

                    }







                    else if (typeCongeIdTypeconge.Equals("J.R") && dateSpan <= tjR)
                    {

                        demande.IdtypeConge = 24;
                        demande.DateFin = Convert.ToDateTime(dateFin);
                        demande.DateDebut = Convert.ToDateTime(dateDebut);


                    }
                    else if (typeCongeIdTypeconge.Equals("J.F") && dateSpan <= tjf)
                    {
                        demande.IdtypeConge = 25;
                        demande.DateFin = Convert.ToDateTime(dateFin);
                        demande.DateDebut = Convert.ToDateTime(dateDebut);
                    }
                    else if (typeCongeIdTypeconge.Equals("heures"))
                    {
                        demande.IdtypeConge = 26;
                        demande.DateFin = Convert.ToDateTime(dateFin);
                        demande.DateDebut = Convert.ToDateTime(dateDebut);
                    }
                    else if (typeCongeIdTypeconge.Equals("jours"))
                    {
                        demande.IdtypeConge = 27;
                        demande.DateFin = Convert.ToDateTime(dateFin);
                        demande.DateDebut = Convert.ToDateTime(dateDebut);

                    }

                    // 6666

                    else
                    {
                        Session["Message"] = "Verifier vos donnees svp ";
                        // ViewBag.Message = "Vérifier vos données svp !";
                        return RedirectToAction("Index", "Demande");
                    }





                }








            }



            demande.ValidationN1 = "*******";
            demande.ValidationN2 = "En cours";
            demande.ValdationRH = "En cours";

            demande.IdEmploye = uid;



            demande.DateDC = dc;
            demande.justification = justification;
            demande.annulation = "non";





            db.demandeconge.Add(demande);

            db.SaveChanges();



            return RedirectToAction("historique", "Demande");

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
            Session["demande"] = null;
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