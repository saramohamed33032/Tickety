using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Web;
using FinalProject.Enums;

namespace FinalProject.Models.TicketyModel
{
    [Table("Degree")]

    public partial class Degree
    {

        
        [Required]
        [MaxLength(1)]
        [Key]
        [Column(Order =0)]
        public string ticketClass { get; set; }
        [Required]
        public double degreePrice { get; set; }

        [Column("NO.Seat")]
        [Display(Name ="Avilable Seats")]
        public int NO_Seat { get; set; }
        [ForeignKey("Match")]
        [Key]
        [Column(Order =1)]
        public string matchID { get; set; }

        public virtual Match Match { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}