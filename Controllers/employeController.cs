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
using System.Data;
using System.IO;


namespace GestionAbscences.Controllers
{
    public class employeController : BaseController
    {
        private GestionAbscencesEntities6 db = new GestionAbscencesEntities6();

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

                if (item.IdtypeConge == 1 && (item.ValidationRH.Equals("En cours") || item.ValidationRH.Equals("accepte")) && (item.ValidationN1.Equals("En cours") || item.ValidationN1.Equals("accepte")) && (item.ValidationN2.Equals("En cours") || item.ValidationN2.Equals("accepte")))
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
                if (item.IdtypeConge == 1 && (item.ValidationRH.Equals("accepte")))
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
                if (item.IdtypeConge == 2 && (item.ValidationRH.Equals("En cours") || item.ValidationRH.Equals("accepte")) && (item.ValidationN1.Equals("En cours") || item.ValidationN1.Equals("accepte")) && (item.ValidationN2.Equals("En cours") || item.ValidationN2.Equals("accepte")))
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
                if (item.IdtypeConge == 2 && item.ValidationRH.Equals("accepte"))
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

                if (item.IdtypeConge == 3 && (item.ValidationRH.Equals("En cours") || item.ValidationRH.Equals("accepte")) && (item.ValidationN1.Equals("En cours") || item.ValidationN1.Equals("accepte")) && (item.ValidationN2.Equals("En cours") || item.ValidationN2.Equals("accepte")))
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
                if (item.IdtypeConge == 3 && item.ValidationRH.Equals("accepte"))
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
            //donnée from index (view)
            int uid = int.Parse(Session["idEmploye"].ToString());
            employe e = db.employe.Find(uid);
            demandeconge demande = new demandeconge();
            string dateDebut = Request["dateDebut"] + " " + Request["timeDebut"];
            string dateFin = Request["dateFin"] + " " + Request["timeFin"];
            string operation = Request["operation"].ToString();
            string marriage = Request["marriage1"].ToString();
            string deces = Request["deces1"].ToString();
            string typeCongeIdTypeconge = Request.Form["typeCongeIdTypeconge"];
            string justification = Request["justification"];
            DateTime dc = DateTime.Now;

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
                Session["demande"] = "Remlpir tout les champs svp !";
                return RedirectToAction("Index", "employe");
            }
            //date debut 
            DateTime debut = Convert.ToDateTime(Request["dateDebut"]);

            if (typeCongeIdTypeconge.Equals("Opération chirurgicale") || typeCongeIdTypeconge.Equals("Décès") || typeCongeIdTypeconge.Equals("Mariage"))
            {
                if (debut < dc)
                {
                    Session["demande"] = "vérifier les dates svp!";
                    return RedirectToAction("Index", "employe");
                }
                else if (typeCongeIdTypeconge.Equals("Mariage") && !(marriage.Equals("")))
                {

                    if (marriage.Equals("1"))
                    {
                        foreach (var item in l)
                        {
                            if (item.idtypeConge == 4)
                            {

                                int x4 = int.Parse(item.dureeJ);
                                var df = debut.AddDays(x4);
                                demande.DateFin = Convert.ToDateTime(df);
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

                                int x5 = int.Parse(item.dureeJ);
                                var df = debut.AddDays(x5);
                                demande.DateFin = Convert.ToDateTime(df);
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

                                int x6 = int.Parse(item.dureeJ);
                                var df = debut.AddDays(x6);
                                demande.DateFin = Convert.ToDateTime(df);
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

                                int x7 = int.Parse(item.dureeJ);
                                var df = debut.AddDays(x7);
                                demande.DateFin = Convert.ToDateTime(df);
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

                                int x8 = int.Parse(item.dureeJ);
                                var df = debut.AddDays(x8);
                                demande.DateFin = Convert.ToDateTime(df);
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

                                int x9 = int.Parse(item.dureeJ);
                                var df = debut.AddDays(x9);
                                demande.DateFin = Convert.ToDateTime(df);
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

                                int x10 = int.Parse(item.dureeJ);
                                var df = debut.AddDays(x10);
                                demande.DateFin = Convert.ToDateTime(df);
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

                                int x20 = int.Parse(item.dureeJ);
                                var df = debut.AddDays(x20);
                                demande.DateFin = Convert.ToDateTime(df);
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

                                int x21 = int.Parse(item.dureeJ);
                                var df = debut.AddDays(x21);
                                demande.DateFin = Convert.ToDateTime(df);
                                demande.IdtypeConge = 21;
                            }
                        }
                    }

                }



            }
            else
            {
                if (Request["dateDebut"].Equals("") || Request["dateFin"].Equals("") || typeCongeIdTypeconge.Equals(""))
                {
                    Session["demande"] = "Remlpir tout les champs svp !";
                    return RedirectToAction("Index", "employe");
                }
                DateTime fin = Convert.ToDateTime(Request["dateFin"]);
                TimeSpan dateSpan = fin - debut;

                if ((debut < dc) || (fin < dc) || (fin < debut))
                {
                    Session["demande"] = "vérifier les dates svp!";
                    return RedirectToAction("Index", "employe");
                }

                else
                {


                    if (typeCongeIdTypeconge.Equals("reliquat") && dateSpan > t0 && dateSpan <= tR)
                    {

                        demande.IdtypeConge = 3;
                        demande.DateFin = Convert.ToDateTime(dateFin);

                    }
                    else if (typeCongeIdTypeconge.Equals("1 ere tranche") && dateSpan >= t10 && dateSpan <= t) //obj <JR ,obj >=10
                    {
                        demande.IdtypeConge = 1;
                        demande.DateFin = Convert.ToDateTime(dateFin);

                    }
                    else if (typeCongeIdTypeconge.Equals("2 eme tranche") && dateSpan >= t7 && dateSpan <= tR)//obj <7(t7) , obj <=jR
                    {
                        demande.IdtypeConge = 2;
                        demande.DateFin = Convert.ToDateTime(dateFin);

                    }
                    else if (typeCongeIdTypeconge.Equals("1/2 journée") && dateSpan <= t12)
                    {

                        demande.DateFin = Convert.ToDateTime(dateFin);
                        demande.IdtypeConge = 11;

                    }
                    else if (typeCongeIdTypeconge.Equals("1 journée") && dateSpan == t1)
                    {
                        demande.DateFin = Convert.ToDateTime(dateFin);
                        demande.IdtypeConge = 12;

                    }
                    else if (typeCongeIdTypeconge.Equals("1.5 journée") && dateSpan <= t112)
                    {
                        demande.DateFin = Convert.ToDateTime(dateFin);
                        demande.IdtypeConge = 13;

                    }
                    else if (typeCongeIdTypeconge.Equals("2 journée") && dateSpan == t2)
                    {
                        demande.DateFin = Convert.ToDateTime(dateFin);
                        demande.IdtypeConge = 14;

                    }
                    else if (typeCongeIdTypeconge.Equals("Naissance") && !(justification.Equals("")))
                    {
                        foreach (var item in l)
                        {
                            if (item.idtypeConge == 22)
                            {

                                int x22 = int.Parse(item.dureeJ);
                                var df = debut.AddDays(x22);
                                demande.DateFin = Convert.ToDateTime(df);
                                demande.IdtypeConge = 22;
                            }
                        }
                    }




                    else if (typeCongeIdTypeconge.Equals("H.S"))
                    {
                        demande.IdtypeConge = 23;
                        demande.DateFin = Convert.ToDateTime(dateFin);
                    }
                    else if (typeCongeIdTypeconge.Equals("J.R"))
                    {
                        demande.IdtypeConge = 24;
                        demande.DateFin = Convert.ToDateTime(dateFin);
                    }
                    else if (typeCongeIdTypeconge.Equals("J.F"))
                    {
                        demande.IdtypeConge = 25;
                        demande.DateFin = Convert.ToDateTime(dateFin);
                    }
                    else if (typeCongeIdTypeconge.Equals("heures"))
                    {
                        demande.IdtypeConge = 26;
                        demande.DateFin = Convert.ToDateTime(dateFin);
                    }
                    else if (typeCongeIdTypeconge.Equals("jours"))
                    {
                        demande.IdtypeConge = 27;
                        demande.DateFin = Convert.ToDateTime(dateFin);

                    }
                    else
                    {
                        Session["demande"] = "Vérifier vos données svp !";
                        return RedirectToAction("Index", "employe");
                    }





                }








            }



            demande.ValidationN1 = "En cours";
            demande.ValidationN2 = "En cours";
            demande.ValidationRH = "En cours";

            demande.IdEmploye = uid;

            demande.DateDebut = Convert.ToDateTime(dateDebut);

            demande.DateDC = dc;
            demande.justification = justification;
            demande.Annulation = "non";





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

            //condition pas encore
            if (id == null )
            {
                //return RedirectToAction("Index", "Default");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var currentDemande = demandeService.ReadById(id.Value);
            if (currentDemande == null )
            {
                return HttpNotFound($"this demande ({id}) is not found");
            }

            demandeconge demandeconge = db.demandeconge.Find(id);
            Session["uid"] = currentDemande.idDemandeConge;

            if (demandeconge == null )
            {
                return HttpNotFound();
            }

            return View(demandeconge);
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

        /* public FileResult Export()
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

             var demande = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x);

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

      
    }
}


