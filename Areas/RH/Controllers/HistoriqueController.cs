using GestionAbscences.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GestionAbscences.Services;
using GestionAbscences.Areas.Admin.Models;
using System.Net;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using ClosedXML.Excel;





namespace GestionAbscences.Areas.RH.Controllers
{
    public class HistoriqueController : Controller
    {
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();
        private readonly DemandeService demandeService;

        public HistoriqueController()
        {
            demandeService = new DemandeService();
        }
       






      
        public ActionResult historique()
        {

            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where((p => p.ValidationN2 == "accepte" && p.ValidationN1 != "refuse")).OrderByDescending(news => news.DateDC).Take(10).ToList();
            return View(demandeConge);


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
                ViewBag.message="Selectionner les dates et la catégorie svp !";
                return RedirectToAction("historique");
            }
            else
            {
                DateTime debut = Convert.ToDateTime(Request["debut"]);

                DateTime fin = Convert.ToDateTime(Request["fin"]);

                if (val.Equals("1"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("2"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("3"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "accepte" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("4"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "refuse" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("5"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "En cours" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("6"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "accepte" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("7"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN2 == "refuse" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("8"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValdationRH == "En cours" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("9"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValdationRH == "accepte" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }
                else if (val.Equals("10"))
                {
                    var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValdationRH == "refuse" && p.DateDebut >= debut && p.DateDebut <= fin);
                    return View(demandeConge.ToList());
                }


            }


            return RedirectToAction("historique");

        }
        public ActionResult validation(int? id)
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

            /*var dure = (dateFin - dateDebut).Days;
            var dureM = (dateFin - dateDebut).TotalMinutes;

            double du = Convert.ToDouble(dure) + 1;
            double duM = Convert.ToDouble(dureM) + 1440;
            double nb = Convert.ToDouble(e.employe.nbHeureR);
            double nbM = Convert.ToDouble(e.employe.nbHeureR) * 24 * 60;
            double res = nb - du;
            double resM = nbM - duM;
            double resJ = resM / 1440;
            */

            var recup = db.CumulRecup.Include(d => d.employe).Where(p => p.employe.idEmploye == e.IdEmploye).Select(u => new {
                hs = u.CumulHr,
                jf = u.CumulJrF,
                jr = u.CumulJrR ,
                id = u.IDCumulRecup

            }).Single();

            CumulRecup cr = db.CumulRecup.Find(recup.id);

            double hsA = Convert.ToDouble(cr.CumulHr);

            //en heure 
            double jf = Convert.ToDouble(recup.jf) ;


            double jR = Convert.ToDouble(recup.jr) ;
           


            var dureM = (dateFin - dateDebut).TotalHours;
            var dureD = (dateFin - dateDebut).Days;
            double duM = Convert.ToDouble(dureM);
            double nbM = Convert.ToDouble(e.employe.nbHeureR) ;
            double resM = nbM - duM;

            

            
            switch (button)
            {
                case "Accepté":
                   if(e.IdtypeConge== 23 )
                    {
                        double dhs = hsA - dureM;
                        cr.CumulHr = Convert.ToString(dhs);

                    }else
                    if (e.IdtypeConge == 25)
                    {
                        double dhs = jf - dureD;
                        string dh = Convert.ToString(dhs);
                        cr.CumulJrF = float.Parse(dh);

                    }else
                    if (e.IdtypeConge == 24)
                    {
                        double dhs = jR - dureD;
                        string dh = Convert.ToString(dhs);
                        cr.CumulJrR =  float.Parse(dh);

                    }
                    else
                    {
                        e.employe.nbHeureR = resM.ToString();
                    }
                    e.ValdationRH = "accepte";
                    
                    e.DateValidationRH = DateTime.Now;

                    //DCTEMP
                    dc.matricule = e.employe.matricule;
                    dc.DateDebut = e.DateDebut;
                    dc.DateFin = e.DateFin;
                    dc.typeDeConge = e.typeconge.designation;
                    dc.Status = "En att";


                    db.Entry(e).State = EntityState.Modified;

                    db.DCTEMP.Add(dc);
                    db.SaveChanges();
                    return RedirectToAction("historique");
                case "Refusé":
                    e.ValdationRH = "refuse";
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("historique");
                case "Annulé":

                    return RedirectToAction("historique");
                default:
                    return View();

            }



        }

    }











}