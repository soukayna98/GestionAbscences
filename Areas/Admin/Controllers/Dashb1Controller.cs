using GestionAbscences.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace GestionAbscences.Areas.Admin.Controllers
{
    public class Dashb1Controller : Controller
    {
        // GET: Admin/Dashb1
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();
        // GET: DashB
        public ActionResult Index()
        {

            string x = Session["matricule"].ToString();
            //global
            var count1 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x).Count();
            Session["global"] = count1.ToString();

            var test = count1.ToString();
            if (test.Equals("0"))
            {
                Session["accept1"] = 0;
                Session["refuse1"] = 0;
                Session["enCours1"] = 0;

            }
            else
            {
                //accepte
                var count2 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x && p.ValidationRH == "Accepte").Count();
                Session["accepte"] = count2.ToString();
                var num1 = count2 * 100;
                Session["accept1"] = num1 / count1;
                //refuse
                var count3 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x && (p.ValidationRH == "refuse" || p.ValidationN1 == "refuse" || p.ValidationN2 == "refuse")).Count();
                Session["refuse"] = count3.ToString();
                var num2 = count3 * 100;
                Session["refuse1"] = num2 / count1;
                //encours
                var count4 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x && (p.ValidationRH == "En cours" || p.ValidationN1 == "En cours" || p.ValidationN2 == "En cours")).Count();
                Session["enCours"] = count4.ToString();
                var num3 = count4 * 100;
                Session["enCours1"] = num3 / count1;

            }

            return View();
        }
        public ActionResult Global()
        {
            //employe e = new employe();
            Session["Message"] = null;

            string x = Session["matricule"].ToString();

            // int x1 = int.Parse(x);


            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x).OrderByDescending(news => news.DateDC).ToList();



            //  var t = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x).Count();
            //ViewBag.count = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x).Count().ToString();
            return View(demandeConge);

        }
        public ActionResult Accepte()
        {
            //employe e = new employe();
            Session["Message"] = null;

            string x = Session["matricule"].ToString();

            // int x1 = int.Parse(x);


            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x && p.ValidationRH == "accepte").OrderByDescending(news => news.DateDC).ToList();


            return View(demandeConge);

        }
        public ActionResult Refuse()
        {
            //employe e = new employe();
            Session["Message"] = null;

            string x = Session["matricule"].ToString();

            // int x1 = int.Parse(x);


            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x && (p.ValidationRH == "refuse" || p.ValidationN1 == "refuse" || p.ValidationN2 == "refuse")).OrderByDescending(news => news.DateDC).ToList();


            return View(demandeConge);

        }
        public ActionResult Encours()
        {
            //employe e = new employe();
            Session["Message"] = null;

            string x = Session["matricule"].ToString();

            // int x1 = int.Parse(x);


            var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x && (p.ValidationRH == "En cours" || p.ValidationN1 == "En cours" || p.ValidationN2 == "En cours")).OrderByDescending(news => news.DateDC).ToList();


            return View(demandeConge);

        }

        public ActionResult GetData()
        {
            return View();
        }
    }
}