using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionAbscences.Areas.AdminN2.Models
{
    public class DemandeModel
    {
        public int IdTypeConge { get; set; }
        public int matricule { get; set; }
        public int IdDemandeConge { get; set; }

        public String NomComplet { get; set; }

        public String Classe { get; set; }
        public string validationN1 { get; set; }
        public string validationN2 { get; set; }
        public string validationRh { get; set; }

        public DateTime DateDebut { get; set; }

        public DateTime DateFin { get; set; }
        public DateTime DateDc { get; set; }

        public int nbJ { get; set; }
    }
}