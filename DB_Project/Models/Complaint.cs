using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DB_Project.Models
{
    public class Complaint
    {
        public String complaintID { get; set; }
        public String userID { get; set; }
        public String dtime { get; set; }
        public String text { get; set; }
        public String status { get; set; }
    }
}