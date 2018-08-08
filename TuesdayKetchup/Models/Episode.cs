using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TuesdayKetchup.Models
{
    public class Episode
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        [Display(Name = "Soundcloud link")]
        public string SoundCloudLink { get; set; }
        [ForeignKey("Show")]
        public int ShowId { get; set; }
<<<<<<< HEAD
        public virtual Show Show { get; set; }
        [NotMapped]
        public double OverallRating
        {
            get
            {
                if (ratings.Count > 0)
                {
                    return (ratings.Average(x => x.Star));
                }
                return (4);
            }
        }
        [Display(Name = "Ratings")]
        [InverseProperty("Episode")]
        public ICollection<Rating> ratings { get; set; } = new HashSet<Rating>();
=======
        public Show Show { get; set; }
>>>>>>> e48d9c2ccd82d32a4c84382ff2488636aa466464
    }


}