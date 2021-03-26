using GestionAbscences.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionAbscences.Controllers
{
    public class employeController : Controller
    {
        // GET: employe
        public ActionResult Index()
        {

            var empl = new List<account>
            {
                new account
                {
                    Login="souka",
                    pass="souka"
                }

            };
            return View(empl);
        }
    }
}