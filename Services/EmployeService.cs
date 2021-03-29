using GestionAbscences.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionAbscences.Services
{

    public interface IEmployeService
    {

        List<employe> ReadAll();
        int Create(employe newEmploye);
    }

    public class EmployeService : IEmployeService
    {
        private readonly GestionAbscencesEntities db;

        public EmployeService()
        {
            db = new GestionAbscencesEntities();
        }

        public int Create(employe newEmploye)
        {
            var employeName = newEmploye.NomComplet.ToLower();
            var employeNameExists = db.employe.Where(c => c.NomComplet.ToLower() == employeName).Any();
            if (employeNameExists)
            {
                return -2;
            }
           
            
            db.employe.Add(newEmploye);
             return db.SaveChanges();
           
        }

        public List<employe> ReadAll()
        {

            return db.employe.ToList();
        }
    }
}