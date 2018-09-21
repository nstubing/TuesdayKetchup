using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TuesdayKetchup.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string Picture { get; set; }
        [ForeignKey("Blog")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}