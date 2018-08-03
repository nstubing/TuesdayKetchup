using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace TuesdayKetchup.Models
{
    public class TextAlert
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Episode Link")]
        public string EpisodeLink { get; set; }
        [Required]
        [Display(Name = "Episode Text Message")]
        public string Message { get; set; }
        [ForeignKey("Show")]
        public int ShowId { get; set; }
        public Show Show { get; set; }
    }
}