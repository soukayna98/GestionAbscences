using GestionAbscences.Areas.Admin.Models;
using GestionAbscences.Controllers;
using GestionAbscences.Data;
using GestionAbscences.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GestionAbscences.Areas.RH.Controllers
{
    public class HistoriqueController : BaseController
    {
        private readonly DemandeService demandeService;
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();
        private readonly EmployeService employeService;


       

        public HistoriqueController()
        {
            demandeService = new DemandeService();
            employeService = new EmployeService();
        }

        //private GestionAbscencesEntities1 db = new GestionAbscencesEntities1();

        // GET: Admin/Historique
        /* public ActionResult historique()
         {
             //les employes from DB

             var employes = demandeService.ReadAll();

             var employesList = new List<DemandeModel>();

             foreach (var item in employes)
             {
                 if (item.ValidationN2 == "accepte"  && item.ValidationRH == "En cours")
                 {
                     employesList.Add(new DemandeModel
                     {
                         DateDebut = (DateTime)item.DateDebut,
                         DateFin = (DateTime)item.DateFin,
                         DateDc = (DateTime)item.DateDC,
                         validationN1 = item.ValidationN1,
                         validationN2 = item.ValidationN2,
                         matricule = item.IdEmploye,
                         IdTypeConge = item.IdtypeConge,
                         IdDemandeConge = item.idDemandeConge,
                         NomComplet = item.employe.NomComplet

                     });
                 }
             }
             return View(employesList);
         }*/

        public ActionResult historique()
        {
            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 != "refuse" && p.ValidationN2 == "accepte" && p.ValidationRH == "En cours");

            return View(demandeConge.ToList());
        }
        public ActionResult historiques()
        {
            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge);

            return View(demandeConge.ToList());
        }

        /*   public ActionResult historique()
           {
               var employes = db.demandeconge;
               var demandeConge = new List<demandeconge>();


               foreach (var item in employes)
               {
                   if (item.ValidationN2 == "accepte" && item.ValidationN1 == "accepte" && item.ValidationRH == "En cours")

                       demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).ToList();


               }
               return View(demandeConge.ToList());

           }*/
        public ActionResult Validation(int? id)
        {
            if (id == null)
            {
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

        [HttpPost]
        public ActionResult Validation()
        {
            int uid = int.Parse(Session["uid"].ToString());
            demandeconge e = db.demandeconge.Find(uid);
            string button = Request["button"];
            DCTEMP dc = new DCTEMP();

            DateTime dateDebut = e.DateDebut.Value;
            DateTime dateFin = e.DateFin.Value;


            int annee = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
            int mois = Convert.ToInt32(DateTime.Now.ToString("MM"));

            var recup = db.CumulRecup.Include(d => d.employe).Where(p => p.employe.idEmploye == e.IdEmploye && p.Annee == annee && p.Mois == mois).Select(u => new {
                hs = u.CumulHr,
                jf = u.CumulJrF,
                jr = u.CumulJrR,
                id = u.IDCumulRecup

            }).Single();

            CumulRecup cr = db.CumulRecup.Find(recup.id);

            double hsA = Convert.ToDouble(cr.CumulHr);

            //en heure 
            double jf = Convert.ToDouble(recup.jf);


            double jR = Convert.ToDouble(recup.jr);



            var dureM = (dateFin - dateDebut).TotalHours;
            var dureD = (dateFin - dateDebut).Days + 1;
            double duM = Convert.ToDouble(dureM);
            double nbM = Convert.ToDouble(e.employe.nbHeureR);
            double resM = nbM - duM;




            switch (button)
            {
                case "Accepté":
                    if (e.IdtypeConge == 23)
                    {
                        double dhs = hsA - dureM;
                        cr.CumulHr = float.Parse (Convert.ToString(dhs));
                        e.ValidationRH = "accepte";

                    }
                    else
                     if (e.IdtypeConge == 25)
                    {
                        double dhs = jf - dureD;
                        string dh = Convert.ToString(dhs);
                        cr.CumulJrF = float.Parse(dh);
                        e.ValidationRH = "accepte";

                    }
                    else
                     if (e.IdtypeConge == 24)
                    {
                        double dhs = jR - dureD;
                        string dh = Convert.ToString(dhs);
                        cr.CumulJrR = float.Parse(dh);
                        e.ValidationRH = "accepte";

                    }
                    else
                     if (e.IdtypeConge == 4 || e.IdtypeConge == 5 || e.IdtypeConge == 22 || e.IdtypeConge == 6 || e.IdtypeConge == 7 || e.IdtypeConge == 8 || e.IdtypeConge == 9 || e.IdtypeConge == 10 || e.IdtypeConge == 15 || e.IdtypeConge == 20 || e.IdtypeConge == 21)
                    {
                        e.ValidationRH = "accepte";
                    }
                    else
                    {
                        e.employe.nbHeureR = resM.ToString();
                        e.ValidationRH = "accepte";
                    }

                    if (e.ValidationN1.Equals("En cours"))
                    {
                        e.ValidationN1 = "*******";
                    }

                    e.DateValidationRH = DateTime.Now;

                    //DCTEMP
                    dc.matricule = int.Parse(e.employe.matricule);
                    dc.DateDebut = e.DateDebut;
                    dc.DateFin = e.DateFin;
                    dc.typeDeConge = e.typeconge.designation;
                    dc.Status = "En att";


                    db.Entry(e).State = EntityState.Modified;

                    db.DCTEMP.Add(dc);
                    db.SaveChanges();
                    return RedirectToAction("historique");
                case "Refusé":
                    e.ValidationRH = "refuse";
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("historique");
                case "Annulé":

                    return RedirectToAction("historique");
                default:
                    return View();

            }



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult choixVal()
        {
            string d1 = Request["debut"].ToString();
            string f1 = Request["fin"].ToString();
            ViewBag.d2 = Request["debut"].ToString();
            ViewBag.f2 = Request["fin"].ToString();


            string val = Request["validation"].ToString();



            if (d1.Equals("") || f1.Equals("") || val.Equals(""))
            {
                ViewBag.message = "Selectionner les dates et la catégorie svp !";
                return RedirectToAction("historique");
            }
            else
            {
                DateTime debut = Convert.ToDateTime(Request["debut"]);

                DateTime fin = Convert.ToDateTime(Request["fin"]);

                if (val.Equals("1"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("2"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("3"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("4"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("5"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "En cours" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("6"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "accepte" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("7"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "refuse" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("8"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationRH == "En cours" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("9"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationRH == "accepte" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("10"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationRH == "refuse" && p.DateDebut >= debut && p.DateDebut <= fin).OrderByDescending(news => news.DateDC).ToList();
                    return View(demandeConge.ToList());
                }


            }


            return RedirectToAction("historique");

        
        }
    }
}