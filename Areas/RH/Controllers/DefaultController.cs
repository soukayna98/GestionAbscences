﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionAbscences.Areas.RH.Controllers
{
    public class DefaultController : Controller
    {
        // GET: RH/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}