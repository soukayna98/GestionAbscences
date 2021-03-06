using GestionAbscences.Areas.Admin.Models;
using GestionAbscences.Controllers;
using GestionAbscences.Data;
using GestionAbscences.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace GestionAbscences.Areas.Admin.Controllers
{
    public class EmployeController : BaseController
    {
        private GestionAbscencesEntities11 db = new GestionAbscencesEntities11();
        private readonly EmployeService employeService;

        public EmployeController()
        {
            employeService = new EmployeService();
        }

        // GET: Admin/Employe
       /* public ActionResult Index()
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
        }*/
        public ActionResult Index()
        {
            var employe = db.employe.ToList();
            return View(employe);
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

        public ActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return RedirectToAction("Index", "Default");
            }

            var currentEmploye = employeService.ReadById(id.Value);
            if (currentEmploye == null)
            {
                return HttpNotFound($"this employe ({id}) is not found");
            }
            var EmployeModel = new EmployeModel
            {
                Id = currentEmploye.idEmploye,
                Nom = currentEmploye.NomComplet,
                Classe = currentEmploye.Classe,
                DateD = (DateTime)currentEmploye.DateDebut,
                DateF = (DateTime)currentEmploye.DateFin

            };

            return View(EmployeModel);
        }
        [HttpPost]
        public ActionResult Edit(EmployeModel data)
        {
            if (ModelState.IsValid)
            {
                var updatedEmploye = new employe
                {
                    idEmploye = data.Id,
                    NomComplet = data.Nom,
                    Classe = data.Classe,
                    DateDebut = (DateTime)data.DateD,
                    DateFin = (DateTime)data.DateF
                };
                var result = employeService.Update(updatedEmploye);

                if (result > 0)
                {
                    ViewBag.Success = true;
                    ViewBag.Message = $"employe ({data.Id}) updated succefully";
                }
                else
                    ViewBag.Message = $"an error occurred while updation employe !";

            }

            return View(data);
        }

        public ActionResult Donnee()
        {

            return View();
        }


        /*  public ActionResult Delete(int? Id)
          {
              if(Id != null)
              {
                  var employe = employeService.ReadById(Id.Value);
                  var employeInfo = new EmployeModel
                  {
                      Id = employe.idEmploye,
                      Classe = employe.Classe,
                      DateD = (DateTime)employe.DateDebut,
                      DateF = (DateTime)employe.DateFin,
                      Nom = employe.NomComplet
                  };
                  return View(employeInfo);
              }
              return RedirectToAction("Index");
          }
        */

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var currentEmploye = employeService.ReadById(id.Value);
            if (currentEmploye == null)
            {
                return HttpNotFound($"this demande ({id}) is not found");
            }


            employe e = db.employe.Find(id);
            Session["idE"] = currentEmploye.idEmploye;

            if (e == null)
            {
                return HttpNotFound();
            }

            return View(e);
        }
        [HttpPost]
        public ActionResult Delete()
        {

            int uid = int.Parse(Session["idE"].ToString());
            employe e = db.employe.Find(uid);
            DateTime dc = DateTime.Now;


            string button = Request["button"];
            switch (button)
            {
                case "Supprimer":
                    e.DateFin = dc;
                    e.password = "supprime";
                    db.Entry(e).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

                case "Annuler":

                    return RedirectToAction("Index");

                default:
                    return View();

            }
        }

           /* [HttpPost]
        public ActionResult DeleteConfirmed(int? Id)
        {
            if(Id != null)
            {
                var deleted =employeService.Delete(Id.Value);
                if (deleted)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Delete", new {Id =Id });

            }
            return HttpNotFound();
        }*/
    }
}