using GestionAbscences.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionAbscences.Services
{
    public interface IDemandeService
    {
        int Create(demandeconge demande);
    }

    public class DemandeService : IDemandeService
    {
        
        private readonly GestionAbscencesEntities db;

        public DemandeService()
        {
            db = new GestionAbscencesEntities();
        }
        public int Create(demandeconge newdemande)
        {
            db.demandeconge.Add(newdemande);
            return db.SaveChanges();
        }
    }
}