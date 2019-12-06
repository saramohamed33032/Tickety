using FinalProject.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace FinalProject.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel

    {
        [Required(ErrorMessage ="*Required")]
        [Display(Name ="User Name")]
        public string Email { get; set; }
        [Required(ErrorMessage = "*Required")]
        //[Remote("UserExistsAsync", "Account", AdditionalFields = "Email", ErrorMessage = "invalid Email Or Password")]
        [Remote("UserExistsAsync", "Account" ,AdditionalFields = "Email,Password")]
       

        [DataType(DataType.Password)]
        [Display(Name = "Password")]

        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "*Required")]
        [EmailAddress]
        [Display(Name = "Email")]
        [Remote("UserAlreadyExistsAsync", "Account", ErrorMessage = "*already exists")]

        public string Email { get; set; }

        [Required(ErrorMessage = "*Required")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$",ErrorMessage = "use at least one(lower-upper-special character-number)")]
        [StringLength(100, ErrorMessage = "The{0}must be at least {2} characters long.", MinimumLength = 6)]

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        
        public string Password { get; set; }

        [DataType(DataType.Password)]
       
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "*not match")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "*Required")]
        public virtual string Frist_Name { get; set; }
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "*Required")]
        [Remote("checkUserName", "Account", ErrorMessage = "*already used")]
        public virtual string Username { get; set; }

        [Required(ErrorMessage ="*Required")]
        [Range(1, int.MaxValue, ErrorMessage = "*Required")]

        public Gender gender { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "*not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

 
}
