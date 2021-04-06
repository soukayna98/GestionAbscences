using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionAbscences.Models
{
    public class ChangePasswordModel
    {
        [Display (Name = "actuel ")]
        [Required( ErrorMessage = "l'ancien mot de passe est requis")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Display(Name = "Nouveau ")]
        [Required(ErrorMessage = "Nouveau mot de passe est requis")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirmer")]
        [Required(ErrorMessage = "confirmer le nouvau mot de passe est requis")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmNewPassword { get; set; }
    }
}