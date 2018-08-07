using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuesdayKetchup.Models
{
    public class ShowViewModel
    {
        public EpisodeViewModel episodeVM;
        public List<Episode> episodes;

        public ShowViewModel()
        {
            episodeVM = new EpisodeViewModel();
        }

    }
}