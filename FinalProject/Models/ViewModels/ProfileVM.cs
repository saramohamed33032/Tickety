using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models.ViewModels
{
    public class ProfileVM
    {
        
        public ChangePasswordViewModel  ChangePasswordViewModel { get; set; }

        public EditEmail EditEmail { get; set; }
        public EditPhone editPhone { get; set; }
        public VerifyPhone  VerifyPhone { get; set; }
        public VerifyCode verifyCode { get; set; }
        public ApplicationUser applicationUser { get; set; }
        


        
    }
}