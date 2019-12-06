using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class EditPhone
    {
        [Required]
        [Phone(ErrorMessage = "Invalid phone")]
        [Display(Name = "New phone")]
        public string Phone { get; set; }

        [Phone(ErrorMessage = "Invalid phone")]
        [Display(Name = "oldphone")]
        public string oldPhone { get; set; }
    }
}