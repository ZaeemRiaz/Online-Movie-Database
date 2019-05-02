using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB_Project.Models
{
    public class Comment
    {
        public String movieID { get; set; }
        public String userID { get; set; }
        public String dtime { get; set; }
        public String comment { get; set; }
    }
}