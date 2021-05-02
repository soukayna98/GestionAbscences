using GestionAbscences.Areas.Admin.Models;
using GestionAbscences.Data;
using GestionAbscences.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionAbscences.Areas.AdminN2.Controllers
{
    public class HistoriqueController : Controller
    {
        // GET: AdminN2/Historique
        private readonly DemandeService demandeService;
        private GestionAbscencesEntities4 db = new GestionAbscencesEntities4();


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
                if (item.ValidationN2 == "En cours")
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
            }
            return View(employesList);
        }
        //"Edit", "Edit", new { id = item.IdDemandeConge }
        //get infos
       


    }
}