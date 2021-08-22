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

            /* var query = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge)
                 .GroupBy(p => p.DateDebut)
                 .Select(g =>new { count = g.Count(k => k.ValidationRH == "Accepte") ,date = g.Key });
             return Json(query, JsonRequestBehavior.AllowGet);*/


            DateTime jD = Convert.ToDateTime(DateTime.Now.ToString("01/01/yyyy"));
            DateTime jF = Convert.ToDateTime(DateTime.Now.ToString("31/01/yyyy"));

            DateTime FD = Convert.ToDateTime(DateTime.Now.ToString("01/02/yyyy"));
            DateTime FF = Convert.ToDateTime(DateTime.Now.ToString("28/02/yyyy"));

            DateTime MarsD = Convert.ToDateTime(DateTime.Now.ToString("01/03/yyyy"));
            DateTime MarsF = Convert.ToDateTime(DateTime.Now.ToString("31/03/yyyy"));

            DateTime AD = Convert.ToDateTime(DateTime.Now.ToString("01/04/yyyy"));
            DateTime AF = Convert.ToDateTime(DateTime.Now.ToString("30/04/yyyy"));

            DateTime MD = Convert.ToDateTime(DateTime.Now.ToString("01/05/yyyy"));
            DateTime MF = Convert.ToDateTime(DateTime.Now.ToString("31/05/yyyy"));

            DateTime juD = Convert.ToDateTime(DateTime.Now.ToString("01/06/yyyy"));
            DateTime juF = Convert.ToDateTime(DateTime.Now.ToString("30/06/yyyy"));

            DateTime julD = Convert.ToDateTime(DateTime.Now.ToString("01/07/yyyy"));
            DateTime julF = Convert.ToDateTime(DateTime.Now.ToString("31/07/yyyy"));


            DateTime AoutD = Convert.ToDateTime(DateTime.Now.ToString("01/08/yyyy"));
            DateTime AoutF = Convert.ToDateTime(DateTime.Now.ToString("31/08/yyyy"));


            DateTime sepD = Convert.ToDateTime(DateTime.Now.ToString("01/09/yyyy"));
            DateTime sepF = Convert.ToDateTime(DateTime.Now.ToString("30/09/yyyy"));

            DateTime OcD = Convert.ToDateTime(DateTime.Now.ToString("01/10/yyyy"));
            DateTime OcF = Convert.ToDateTime(DateTime.Now.ToString("31/10/yyyy"));

            DateTime novD = Convert.ToDateTime(DateTime.Now.ToString("01/11/yyyy"));
            DateTime novF = Convert.ToDateTime(DateTime.Now.ToString("30/11/yyyy"));

            DateTime DEcD = Convert.ToDateTime(DateTime.Now.ToString("01/12/yyyy"));
            DateTime DecF = Convert.ToDateTime(DateTime.Now.ToString("31/12/yyyy"));


            string[] xv = { "janvier", "Février", "Mars", "Avril", "May", "juin", "juillet", "Aout", "September", "October", "November", "December" };


            //Les demandes non traiter

            var NT1 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= jD && p.DateFin <= jF).Count();
            var NT2 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= FD && p.DateFin <= FF).Count();
            var NT3 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= MarsD && p.DateFin <= MarsF).Count();
            var NT4 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= AD && p.DateFin <= AF).Count();
            var NT5 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= MD && p.DateFin <= MF).Count();
            var NT6 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= juD && p.DateFin <= juF).Count();
            var NT7 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= julD && p.DateFin <= julF).Count();

            var NT8 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= AoutD && p.DateFin <= AoutF).Count();
            var NT9 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= sepD && p.DateFin <= sepF).Count();
            var NT10 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= OcD && p.DateFin <= OcF).Count();
            var NT11 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= novD && p.DateFin <= novF).Count();
            var NT12 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= DEcD && p.DateFin <= DecF).Count();

            //tous les demandes

            var T1 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= jD && p.DateFin <= jF).Count();
            var T2 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= FD && p.DateFin <= FF).Count();
            var T3 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= MarsD && p.DateFin <= MarsF).Count();
            var T4 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= AD && p.DateFin <= AF).Count();
            var T5 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= MD && p.DateFin <= MF).Count();
            var T6 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= juD && p.DateFin <= juF).Count();
            var T7 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= julD && p.DateFin <= julF).Count();

            var T8 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= AoutD && p.DateFin <= AoutF).Count();
            var T9 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= sepD && p.DateFin <= sepF).Count();
            var T10 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= OcD && p.DateFin <= OcF).Count();
            var T11 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= novD && p.DateFin <= novF).Count();
            var T12 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= DEcD && p.DateFin <= DecF).Count();


            int[] yv = { NT1, NT2, NT3, NT4, NT5, NT6, NT7, NT8, NT9, NT10, NT11, NT12 };
            int[] yv1 = { T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12 };
            // int[] yv = { 2,8 ,9 ,2 , 4 , 9 ,4};
            // string[] xv = { "janvier", "Février", "Mars", "Avril", "May", "juin", "juillet" };

            new System.Web.Helpers.Chart(width: 1200, height: 400)

                .AddLegend("Legend", "yyyyy")
                .AddSeries
                (chartType: "Column",

                name: "DC",
                xValue: xv,
                yValues: yv1)

                 .AddSeries
                (chartType: "Column",

                name: "Bre",


                xValue: xv,
                yValues: yv


                ).
                Write("png");



            return null;
        }
        public ActionResult chart()
        {
            DateTime jD = Convert.ToDateTime(DateTime.Now.ToString("01/01/yyyy"));
            DateTime jF = Convert.ToDateTime(DateTime.Now.ToString("31/01/yyyy"));

            DateTime FD = Convert.ToDateTime(DateTime.Now.ToString("01/02/yyyy"));
            DateTime FF = Convert.ToDateTime(DateTime.Now.ToString("28/02/yyyy"));

            DateTime MarsD = Convert.ToDateTime(DateTime.Now.ToString("01/03/yyyy"));
            DateTime MarsF = Convert.ToDateTime(DateTime.Now.ToString("31/03/yyyy"));

            DateTime AD = Convert.ToDateTime(DateTime.Now.ToString("01/04/yyyy"));
            DateTime AF = Convert.ToDateTime(DateTime.Now.ToString("30/04/yyyy"));

            DateTime MD = Convert.ToDateTime(DateTime.Now.ToString("01/05/yyyy"));
            DateTime MF = Convert.ToDateTime(DateTime.Now.ToString("31/05/yyyy"));

            DateTime juD = Convert.ToDateTime(DateTime.Now.ToString("01/06/yyyy"));
            DateTime juF = Convert.ToDateTime(DateTime.Now.ToString("30/06/yyyy"));

            DateTime julD = Convert.ToDateTime(DateTime.Now.ToString("01/07/yyyy"));
            DateTime julF = Convert.ToDateTime(DateTime.Now.ToString("31/07/yyyy"));


            DateTime AoutD = Convert.ToDateTime(DateTime.Now.ToString("01/08/yyyy"));
            DateTime AoutF = Convert.ToDateTime(DateTime.Now.ToString("31/08/yyyy"));


            DateTime sepD = Convert.ToDateTime(DateTime.Now.ToString("01/09/yyyy"));
            DateTime sepF = Convert.ToDateTime(DateTime.Now.ToString("30/09/yyyy"));

            DateTime OcD = Convert.ToDateTime(DateTime.Now.ToString("01/10/yyyy"));
            DateTime OcF = Convert.ToDateTime(DateTime.Now.ToString("31/10/yyyy"));

            DateTime novD = Convert.ToDateTime(DateTime.Now.ToString("01/11/yyyy"));
            DateTime novF = Convert.ToDateTime(DateTime.Now.ToString("30/11/yyyy"));

            DateTime DEcD = Convert.ToDateTime(DateTime.Now.ToString("01/12/yyyy"));
            DateTime DecF = Convert.ToDateTime(DateTime.Now.ToString("31/12/yyyy"));


            string[] xv = { "janvier", "Février", "Mars", "Avril", "May", "juin", "juillet", "Aout", "September", "October", "November", "December" };


            //Les demandes non traiter

            Session["NT1"] = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= jD && p.DateFin <= jF).Count();
            Session["NT2"] = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= FD && p.DateFin <= FF).Count();
            Session["NT3"] = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= MarsD && p.DateFin <= MarsF).Count();
            Session["NT4"] = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= AD && p.DateFin <= AF).Count();
            Session["NT5"] = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= MD && p.DateFin <= MF).Count();
            Session["NT6"] = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= juD && p.DateFin <= juF).Count();
            Session["NT7"] = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= julD && p.DateFin <= julF).Count();

            Session["NT8"] = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= AoutD && p.DateFin <= AoutF).Count();
            Session["NT9"] = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= sepD && p.DateFin <= sepF).Count();
            Session["NT10"] = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= OcD && p.DateFin <= OcF).Count();
            Session["NT11"] = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= novD && p.DateFin <= novF).Count();
            Session["NT12"] = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.ValidationN1 == "En cours" && p.DateDebut >= DEcD && p.DateFin <= DecF).Count();

            //tous les demandes

            var T1 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= jD && p.DateFin <= jF).Count();
            var T2 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= FD && p.DateFin <= FF).Count();
            var T3 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= MarsD && p.DateFin <= MarsF).Count();
            var T4 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= AD && p.DateFin <= AF).Count();
            var T5 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= MD && p.DateFin <= MF).Count();
            var T6 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= juD && p.DateFin <= juF).Count();
            var T7 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= julD && p.DateFin <= julF).Count();

            var T8 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= AoutD && p.DateFin <= AoutF).Count();
            var T9 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= sepD && p.DateFin <= sepF).Count();
            var T10 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= OcD && p.DateFin <= OcF).Count();
            var T11 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= novD && p.DateFin <= novF).Count();
            var T12 = db.demandeconge.Include(d => d.employe).Include(d => d.typeconge).Where(p => p.DateDebut >= DEcD && p.DateFin <= DecF).Count();


            /*   int[] yv = { NT1, NT2, NT3, NT4, NT5, NT6, NT7, NT8, NT9, NT10, NT11, NT12 };
               int[] yv1 = { T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12 };*/




            return null;
        }
    }
}