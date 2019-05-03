using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB_Project.Models
{
    public class userLoginStruct
    {
        public int ret { get; set; }
        public String id { get; set; }
        public String type { get; set; }
    }
    public class movieDetailStruct
    {
        public Movie movieDetail { get; set; }
        public List<Actor> cast { get; set; }
        public List<Comment> commentList { get; set; }
        public int rating { get; set; }
    }
    
}