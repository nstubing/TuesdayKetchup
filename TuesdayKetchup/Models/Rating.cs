using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
=======
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
>>>>>>> fcae6d448707a7131d84963581d93aa1626fcb15

namespace TuesdayKetchup.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Episode")]
        public int EpisodeId { get; set; }
        public Episode Episode { get; set; }
<<<<<<< HEAD

      

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Range(0,4)]
        [Display(Name = "Star")]
        public double Star { get; set; }


=======
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Range(0, 4)]
        [Display(Name = "Star")]
        public double Star { get; set; }
>>>>>>> fcae6d448707a7131d84963581d93aa1626fcb15
    }
}