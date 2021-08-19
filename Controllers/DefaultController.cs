using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;
using GestionAbscences.Data;
using System.Data.Entity;
using GestionAbscences.Services;
using System.Net;
using System.Data;
using System.IO;

namespace GestionAbscences.Controllers
{
    public class DefaultController : BaseController
    {
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Getdata()
        {

            string x = Session["matricule"].ToString();

            int x1 = int.Parse(x);

            int RHA = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x && p.ValidationRH == "Accepte").Count();
            int RHE = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x && p.ValidationRH == "En cours").Count();
            int RHR = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.employe.matricule == x && p.ValidationRH == "refuse").Count();

         
            Ratio obj = new Ratio();
            obj.RHA = RHA;
            obj.RHE = RHE;
            obj.RHR = RHR;

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public class Ratio
        {
            public int RHA { get; set; }
            public int RHE { get; set; }
            public int RHR { get; set; }
        }
    }
}