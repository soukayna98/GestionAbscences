using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;



namespace GestionAbscences.Controllers
{
    public class DefaultController : BaseController
    {
        // GET: Default
        public ActionResult Index()
        {
            /*if (Session["idEmploye"] != null)
            {
                return RedirectToAction("Index", "Default");
            }
            else
            {
                return RedirectToAction("Index", "Login");

            }*/
            return View();
        }
    }
}