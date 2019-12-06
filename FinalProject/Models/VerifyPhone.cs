using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class VerifyPhone
    {
        [Required]
        [Phone(ErrorMessage = "Invalid phone")]
        [Display(Name = "phone")]
        public string Phone { get; set; }
    }
}