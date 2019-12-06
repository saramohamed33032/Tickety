using FinalProject.Models.TicketyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models.ViewModels
{
    public class IndexVM
    {
        public RegisterViewModel RegisterViewModel { get; set; }
        public LoginViewModel LoginViewModel { get; set; }
        public List<Match> match { get; set; }
        public List<Team> team { get; set; }
    }
}