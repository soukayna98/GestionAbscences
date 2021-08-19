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
    public class AnnulerController : BaseController
    {
        // GET: RH/Annuler
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();
        private readonly DemandeService demandeService;

        public AnnulerController()
        {
            demandeService = new DemandeService();
        }

        // GET: RH/Annuler
        public ActionResult Index()
        {
            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationRH == "accepte" && p.ValidationN2 == "accepte").OrderByDescending(news => news.DateDC).ToList();

            return View(demandeConge);

        }

        public ActionResult Annulation(int? id)
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
        public ActionResult Annulation()
        {
            int uid = int.Parse(Session["uid"].ToString());
            demandeconge e = db.demandeconge.Find(uid);
            string button = Request["button"];

            DateTime dateDebut = e.DateDebut.Value;
            DateTime dateFin = e.DateFin.Value;


            /*   var dure = (dateFin - dateDebut).Days;
               double du = Convert.ToDouble(dure) + 1;
               double d = du * 24;
               double nb = Convert.ToDouble(e.employe.nbHeureR);
               double res = nb + d;
            */

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
            double resM = nbM + duM;

            switch (button)
            {
                case "Retour":

                    return RedirectToAction("Index");
                case "annule":
                    if (e.IdtypeConge == 23)
                    {
                        double dhs = hsA + dureM;
                        string dh = Convert.ToString(dhs);
                        cr.CumulHr = float.Parse(dh);
                        e.ValidationRH = "En cours";

                    }
                    else
                   if (e.IdtypeConge == 25)
                    {
                        double dhs = jf + dureD;
                        string dh = Convert.ToString(dhs);
                        cr.CumulJrF = float.Parse(dh);
                        e.ValidationRH = "En cours";
                    }
                    else
                   if (e.IdtypeConge == 24)
                    {
                        double dhs = jR + dureD;
                        string dh = Convert.ToString(dhs);
                        cr.CumulJrR = float.Parse(dh);
                        e.ValidationRH = "En cours";

                    }
                    else
                    if (e.IdtypeConge == 4 || e.IdtypeConge == 5 || e.IdtypeConge == 22 || e.IdtypeConge == 6 || e.IdtypeConge == 7 || e.IdtypeConge == 8 || e.IdtypeConge == 9 || e.IdtypeConge == 10 || e.IdtypeConge == 15 || e.IdtypeConge == 20 || e.IdtypeConge == 21)
                    {
                        e.ValidationRH = "En cours";
                    }
                    else
                    {
                        e.employe.nbHeureR = resM.ToString();
                        e.ValidationRH = "En cours";
                    }






                    e.ValidationN1 = "En cours";
                    e.ValidationN2 = "En cours";
                    e.Annulation = "oui";
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                default:
                    return View();

            }


        
    }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult choixVal1()
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
