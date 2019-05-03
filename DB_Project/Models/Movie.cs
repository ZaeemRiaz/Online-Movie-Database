using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB_Project.Models
{
    public class Movie
    {
        public String movieID { get; set; }
        public String title { get; set; }
        public String rating { get; set; }
        public String descript { get; set; }
        public String genre { get; set; }
        public String releasedate { get; set; }
        public String picture { get; set; }
        public byte[] picture2 { get; set; }
    }
}