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
        public static SqlDataReader GetUsers()
        {
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            string queryString = "SELECT * FROM Users";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(queryString, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

    }
}