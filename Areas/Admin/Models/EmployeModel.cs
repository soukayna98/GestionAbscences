using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionAbscences.Areas.Admin.Models
{
    public class EmployeModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "name is required")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "name is required")]

        public string Classe { get; set; }
        [Required(ErrorMessage = "name is required")]

        public DateTime DateD { get; set; }
        [Required(ErrorMessage = "name is required")]

        public DateTime DateF { get; set; }
        public int nbJ { get; set; }

    }
}