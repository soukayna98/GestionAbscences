using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionAbscences.Areas.Admin.Models
{
    public class HistoriqueModel
    {
        public int IdDemande { get; set; }
        public int IdType { get; set; }

        // [Required(ErrorMessage ="name is required")]
        public int IdEmploye { get; set; }
        public string matricule { get; set; }
        public string Nom { get; set; }
        // [Required(ErrorMessage = "classe is required")]

        public string validation1 { get; set; }
        public string validation2 { get; set; }
        // [Required(ErrorMessage = "date debut is required")]

        public DateTime DateD { get; set; }
        //[Required(ErrorMessage = "name is required")]

        public DateTime DateF { get; set; }
        public DateTime Datedc { get; set; }


    }
}