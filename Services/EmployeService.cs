using GestionAbscences.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionAbscences.Services
{

    public interface IEmployeService
    {
        int Update(employe updatedEmploye);
        employe ReadById(int id);
        List<employe> ReadAll();

        int Create(employe newEmploye);
        bool Delete(int id);
    }

    public class EmployeService : IEmployeService
    {
        private readonly GestionAbscencesEntities11 db;

        public EmployeService()
        {
            db = new GestionAbscencesEntities11();
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

        public bool Delete(int id)
        {
            var employe = ReadById(id);
            if (employe != null)
            {
                db.employe.Remove(employe);
                return db.SaveChanges() > 0 ? true : false;
            }
            return false;
        }

        public List<employe> ReadAll()
        {

            return db.employe.ToList();
        }

        public employe ReadById(int id)
        {
            return db.employe.Find(id);
        }

        public int Update(employe updatedEmploye)
        {
            //prend les ancien data remplace par nv
            db.employe.Attach(updatedEmploye);
            db.Entry(updatedEmploye).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }
}