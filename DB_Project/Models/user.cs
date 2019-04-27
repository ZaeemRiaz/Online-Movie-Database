using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB_Project.Models
{
    public class user
    {
        public String email { get; set; }
        public String name { get; set; }
        public String usertype { get; set; }
        public String dateOfBirth { get; set; }
        public String password { get; set; }
    }
}