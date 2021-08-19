using GestionAbscences.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace GestionAbscences.Areas.RH.Controllers
{
    public class DashRHController : Controller
    {
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();

        // GET: RH/DashRH
        public ActionResult Index()
        {
            // var demandeConge = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge)
            // .Where(p => p.ValidationN1 != "refuse" && p.ValidationN2 == "accepte" && p.ValidationRH == "En cours");

            var query = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge)
                .GroupBy(p => p.DateDebut)
                .Select(g =>new { count = g.Count(k => k.ValidationRH == "Accepte") ,date = g.Key });
            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}