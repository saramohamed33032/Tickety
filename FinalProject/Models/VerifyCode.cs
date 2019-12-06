using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models
{
    public class VerifyCode
    {
        [Required]
        [StringLength(6, ErrorMessage = "Wrong Code", MinimumLength = 6)]
        
        [Display(Name = "verifyication code")]
        public string verifycode { get; set; }
    }
}