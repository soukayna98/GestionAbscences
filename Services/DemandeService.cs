﻿using GestionAbscences.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionAbscences.Services
{

    public interface IDemandeService
    {

        List<demandeconge> ReadAll();
    }
        public class DemandeService : IDemandeService
    {
       

            private readonly GestionAbscencesEntities3 db;

            public DemandeService()
            {
                db = new GestionAbscencesEntities3();
            }

        public List<demandeconge> ReadAll()
        {
            return db.demandeconge.ToList();
        }
    }


    
}