using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;


namespace TuesdayKetchup.Models
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        public string YoutubeLink { get; set; }
        public string Title { get; set; }
        public bool Pinned { get; set; }
    }
}