//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class employehasentite
    {
        public int IdEmploye { get; set; }
        public int IdEntite { get; set; }
        public Nullable<System.DateTime> date { get; set; }
    
        public virtual employe employe { get; set; }
        public virtual entite entite { get; set; }
    }
}
