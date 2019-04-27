using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DB_Project.Models
{
    public class CRUDuser
    {
        string connectionString = @"Data Source=localhost;Initial Catalog=muz;Intgrated Security=True;";
        string queryString = "SELECT * FROM Users";

    }
}