using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB_Project.Models
{
    public class Movie
    {
        String movieID { get; set; }
        String title { get; set; }
        String rating { get; set; }
        String descript { get; set; }
        String genre { get; set; }
        String releasedate { get; set; }
        String picture { get; set; }
    }
}