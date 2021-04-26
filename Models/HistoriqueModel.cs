using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GestionAbscences.Models
{
    public class HistoriqueModel
    {
        public int IdDemande { get; set; }
        public int IdType { get; set; }

        // [Required(ErrorMessage ="name is required")]
        public int IdEmploye { get; set; }
        public string matricule { get; set; }
        public String Nom { get; set; }
        // [Required(ErrorMessage = "classe is required")]

        public String validation1 { get; set; }
        public String validation2 { get; set; }
        // [Required(ErrorMessage = "date debut is required")]

        public DateTime DateD { get; set; }
        //[Required(ErrorMessage = "name is required")]

        public DateTime DateF { get; set; }
        public DateTime Datedc { get; set; }


    }
}