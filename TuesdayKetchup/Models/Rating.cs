using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TuesdayKetchup.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Episode")]
        public int EpisodeId { get; set; }
        public Episode Episode { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Range(1,5)]
        [Display(Name = "Star")]
        public int Score { get; set; }
    }
}