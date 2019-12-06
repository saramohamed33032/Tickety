using FinalProject.Models.TicketyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models.ViewModels
{
    public class MatchDegreeVM
    {
        public List<Degree> Degree { get; set; }
        public Match Match { get; set; }
    }
}