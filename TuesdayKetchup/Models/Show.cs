using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TuesdayKetchup.Models
{
    public class Show
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public string Image { get; set; }
        public string ItunesName { get; set; }
        public string PatreonId { get; set; }
        public string TwitterAccount { get; set; }
        public string NavImage { get; set; }
    }
}