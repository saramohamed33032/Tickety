using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models.TicketyModel
{
    [Table("Ticket")]

    public partial class Ticket
    {
        [Key]

        public string ticketID { get; set; }
        [MaxLength(1)]
        [Column(Order = 0)]
        public string ticketClass { get; set; }
        [MaxLength(128)]
        [Column(Order = 1)]
        public string matchID { get; set; }
        [ForeignKey("ticketClass , matchID")]
        public virtual Degree Degree { get; set; }
        [ForeignKey("User")]
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        public int Seat_Number { get; set; }

        //[ForeignKey("Match")]
        //public string matchID { get; set; }

        //public virtual Match Match { get; set; }
    }
}