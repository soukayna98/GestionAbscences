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
    
    public partial class employe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public employe()
        {
            this.demandeconge = new HashSet<demandeconge>();
        }
    
        public int idEmploye { get; set; }
        public string NomComplet { get; set; }
        public string Classe { get; set; }
        public Nullable<System.DateTime> DateDebut { get; set; }
        public Nullable<System.DateTime> DateFin { get; set; }
        public string password { get; set; }
        public Nullable<int> nbjours { get; set; }
        public string nbjoursR { get; set; }
        public string matricule { get; set; }
        public string affectation { get; set; }
        public string role { get; set; }
        public Nullable<double> soldeConge { get; set; }
        public string nbjoursA { get; set; }
        public string sexe { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<demandeconge> demandeconge { get; set; }
        public virtual employehasentite employehasentite { get; set; }
    }
}
