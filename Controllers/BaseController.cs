using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.Globalization;

namespace GestionAbscences.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Lang"] != null)
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(Session["Lang"].ToString());

        }
    }
}

