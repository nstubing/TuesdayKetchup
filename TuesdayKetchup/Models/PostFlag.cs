using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
namespace TuesdayKetchup.Models
{
    public class PostFlag
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Post")]
        public int PostID { get; set; }
        public Post Post { get; set; }
        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int Counter { get; set; }
        public bool IsRemoved { get; set; }

    }
}