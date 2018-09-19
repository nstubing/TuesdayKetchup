using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuesdayKetchup.Models
{
    public class VideosViewModel
    {
        public List<Video> Videos { get; set; }
        public Video PinnedVideo { get; set; }
    }
}