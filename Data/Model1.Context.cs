﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionAbscences.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GestionAbscencesEntities7 : DbContext
    {
        public GestionAbscencesEntities7()
            : base("name=GestionAbscencesEntities7")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<demandeconge> demandeconge { get; set; }
        public virtual DbSet<employe> employe { get; set; }
        public virtual DbSet<employehasentite> employehasentite { get; set; }
        public virtual DbSet<entite> entite { get; set; }
        public virtual DbSet<entitesuper> entitesuper { get; set; }
        public virtual DbSet<supervisionher> supervisionher { get; set; }
        public virtual DbSet<typeconge> typeconge { get; set; }
    }
}
