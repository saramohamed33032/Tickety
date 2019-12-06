using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalProject.Models.TicketyModel
{

    [Table("Staduim")]
    public partial class Staduim
    {

        [Key]
        public string staduimID { get; set; }

        [Required]
        [StringLength(50)]
        public string staduimName { get; set; }

        [Required]
        public string staduimLocation { get; set; }
        [Required]
        public string city { get; set; }


        [Required]
        public bool isValid { get; set; }

        public virtual ICollection<Match> Matches { get; set; }
    }
}