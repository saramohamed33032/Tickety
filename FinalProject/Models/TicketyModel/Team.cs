using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models.TicketyModel
{
    [Table("Team")]

    public partial class Team
    {

        [Key]
        public string teamID { get; set; }

        [Required]
        [StringLength(50)]
        public string teamName { get; set; }

        [Required]
        public string teamLogo { get; set; }

        [Required]
        public bool isValid { get; set; } = true;


        [InverseProperty("HomeTeam")]
        public virtual ICollection<Match> HomeMatches { get; set; }
        [InverseProperty("GuestTeam")]
        public virtual ICollection<Match> AwayMatches { get; set; }
    }
}