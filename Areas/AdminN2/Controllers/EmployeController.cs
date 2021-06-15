using GestionAbscences.Areas.AdminN2.Models;
using GestionAbscences.Controllers;
using GestionAbscences.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionAbscences.Areas.AdminN2.Controllers
{
    public class EmployeController : BaseController
    {
        private readonly EmployeService employeService;

        public EmployeController()
        {
            employeService = new EmployeService();
        }
        // GET: AdminN2/Employe
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
                    //matricule = item.matricule,
                    Classe = item.Classe,
                    DateD = (DateTime)item.DateDebut,
                    DateF = (DateTime)item.DateFin,
                    Nom = item.NomComplet


                });
            }
            return View(employesList);
        }

        public ActionResult Donnee()
        {

            return View();
        }
    }
}