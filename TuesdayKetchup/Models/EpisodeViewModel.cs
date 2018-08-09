using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuesdayKetchup.Models
{
    public class EpisodeViewModel
    {
        public Episode episode;
        public List<Comment> comments;
        public int rating;
        public int? currentUserRating;
    }
}