using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace GestionAbscences.Areas.Admin.Models
{
    public class ChangePasswordModel
    {
        //ancien pass
        [Display(Name = "actuel ")]
        [Required(ErrorMessage = "l'ancien mot de passe est requis")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        //nv pass
        [Display(Name = "Nouveau ")]
        [Required(ErrorMessage = "Nouveau mot de passe est requis")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        //confirm pass
        [Display(Name = "Confirmer")]
        [Required(ErrorMessage = "confirmer le nouvau mot de passe est requis")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmNewPassword { get; set; }
        public string Message { get; set; }
    }
}