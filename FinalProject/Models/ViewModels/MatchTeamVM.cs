using FinalProject.Models.TicketyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Models.ViewModels
{
    public class MatchTeamVM
    {
        public List<Match> match { get; set; }
        public List<Team> team { get; set; }
    }
}