using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionAbscences.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required]
        public int Id{ get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string password { get; set; }

        public string Message { get; set; }



    }
}