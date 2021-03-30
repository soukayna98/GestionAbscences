﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GestionAbscences.Models
{
    public class Login
    {
        [Required]
        [Display(Name = "Identifiant")]
        public int Id { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        public string Message { get; set; }



    }
}