﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TuesdayKetchup.Models
{
    public class BlogViewModel
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public BlogPost CurrentPost { get; set; }
    }
}