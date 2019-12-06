using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalProject.Models.TicketyModel
{
    [Table("Match")]
    public partial class Match
    {


        [Key]
        public string matchID { get; set; }


        [Required]
        public DateTime matchDate { get; set; }
        [Required]
        public TimeSpan matchTime { get; set; }

        [Required]
        public bool isValid { get; set; } = true;



        [ForeignKey("Staduim")]
        public string staduimID { get; set; }

        public virtual Staduim Staduim { get; set; }

        [ForeignKey("HomeTeam")]
        [Display(Name = "Home Team")]

        //[Compare("GuestTeamId",ErrorMessage ="Please Make Sure Home&Guest Teams Can Not Be Same")]
        public string HomeTeamId { get; set; }
        [ForeignKey("GuestTeam")]
        [Display(Name = "Away Team")]

        public string GuestTeamId { get; set; }
        public virtual Team HomeTeam { get; set; }

        public virtual Team GuestTeam { get; set; }
        public virtual ICollection<Degree> Degrees { get; set; }
        //public virtual ICollection<Ticket> Tickets { get; set; }
    }
}