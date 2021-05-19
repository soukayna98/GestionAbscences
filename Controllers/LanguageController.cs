using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionAbscences.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Français()
        {
            Session["Lang"] = "fr";
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult English()
        {
            Session["Lang"] = "en-US";
            return Redirect(Request.UrlReferrer.ToString());

        }
    }
}