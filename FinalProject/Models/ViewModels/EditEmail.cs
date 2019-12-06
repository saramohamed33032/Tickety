
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace FinalProject.Models
{
    public class EditEmail
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Remote("ChangeEmailCheck", "Account", ErrorMessage = "*already exists")]
        [Display(Name = "New Email")]
        public string Email { get; set; }
        
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "oldEmail")]
        public string oldEmail { get; set; }
    }
}