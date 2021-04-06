using GestionAbscences.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GestionAbscences.Services;
using GestionAbscences.Areas.Admin.Models;


namespace GestionAbscences.Areas.Admin.Controllers
{
    public class HistoriqueController : Controller
    {

        private readonly DemandeService demandeService;

        public HistoriqueController()
        {
            demandeService = new DemandeService();
        }

        //private GestionAbscencesEntities1 db = new GestionAbscencesEntities1();

        // GET: Admin/Historique
        public ActionResult historique()
        {
            //les employes from DB

            var employes = demandeService.ReadAll();

            var employesList = new List<DemandeModel>();
            foreach (var item in employes)
            {
                employesList.Add(new DemandeModel
                {
                    DateDebut = (DateTime)item.DateDebut,
                    DateFin = (DateTime)item.DateFin,
                    DateDc = (DateTime)item.DateDC,
                    validationN1 = item.ValidationN1,
                    validationN2 = item.ValidationN2,
                    matricule = item.IdEmploye,
                    IdTypeConge = item.IdtypeConge,
                    IdDemandeConge = item.idDemandeConge,
                    NomComplet = item.employe.NomComplet

                });
            }
                return View(employesList);
            }


        
    }
}