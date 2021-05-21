using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;
namespace GestionAbscences.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Frensh()
        {
            Session["Lang"] = "fr";
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult English()
        {
            Session["Lang"] = "en-US";
            return Redirect(Request.UrlReferrer.ToString());

        }

        public ActionResult Arabic()
        {
            Session["Lang"] = "ar-EG";
            return Redirect(Request.UrlReferrer.ToString());

        }

       /* public ActionResult Index(string language)
        {
            if (!string.IsNullOrEmpty(language))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentCulture = new CultureInfo(language);

            }
            HttpCookie cookie = new HttpCookie("Languages");
            cookie.Value = language;
            Response.Cookies.Add(cookie);
            return Redirect(Request.UrlReferrer.ToString());
        }*/
    }
}