using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TuesdayKetchup.Models
{
    public class HomeInfo
    {
        [Key]
        public int Id { get; set; }
        public string Announcement { get; set; }
        public string SliderPic1 { get; set; }
        public string SliderPic2 { get; set; }
        public string SliderPic3 { get; set; }
    }
}