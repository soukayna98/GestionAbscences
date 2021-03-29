using GestionAbscences.Areas.Admin.Models;
using GestionAbscences.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionAbscences.Areas.Admin.Controllers
{
    public class EmployeController : Controller
    {
        private readonly EmployeService employeService;

        public EmployeController()
        {
            employeService = new EmployeService();
        }

        // GET: Admin/Employe
        public ActionResult Index()
        {
            //les employes from DB
            var employes = employeService.ReadAll();
            var employesList = new List<EmployeModel>();
            foreach (var item in employes)
            {
                employesList.Add(new EmployeModel
                {
                    Id = item.idEmploye,
                    Classe = item.Classe,
                    DateD = (DateTime)item.DateDebut,
                    DateF = (DateTime)item.DateFin,
                    Nom = item.NomComplet

                });
            }
            return View(employesList);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeModel data)
        {
            if (ModelState.IsValid)
            {
                int creationResult = employeService.Create(new Data.employe
                {
                    NomComplet = data.Nom,
                    Classe = data.Classe,
                    DateDebut = data.DateD,
                    DateFin = data.DateF
                });
                if(creationResult == -2)
                {
                    ViewBag.Message = "nom exist";
                    return View(data);
                }
                
                    return RedirectToAction("Index");

            }

            return View();
        }
    }
}