using GestionAbscences.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionAbscences.Services
{

    public interface IDemandeService
    {

        List<demandeconge> ReadAll();
        demandeconge ReadById(int id);
        int Update(demandeconge updatedDemande);

    }
    public class DemandeService : IDemandeService
    {


        private readonly GestionAbscencesEntities11 db;

        public DemandeService()
        {
            db = new GestionAbscencesEntities11();
        }

        public List<demandeconge> ReadAll()
        {
            return db.demandeconge.ToList();
        }

        public demandeconge ReadById(int id)
        {
            return db.demandeconge.Find(id);
        }

        public int Update(demandeconge updatedDemande)
        {
            //prend les ancien data remplace par nv
            db.demandeconge.Attach(updatedDemande);
            db.Entry(updatedDemande).State = System.Data.Entity.EntityState.Modified;
            return db.SaveChanges();
        }
    }



}