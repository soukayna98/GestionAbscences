using GestionAbscences.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionAbscences.Areas.AdminN2.Controllers
{
    public class DefaultController : BaseController
    {
        // GET: AdminN2/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}