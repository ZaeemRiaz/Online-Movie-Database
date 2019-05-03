using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DB_Project.Models
{
    public class CRUDcomplaint
    {
        public static int AddComplaintFunc(string userID, string text)
        {
            //open connection to db
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = null;
            int result = 0;

            //try execution
            try
            {
                connection.Open();

                command = new SqlCommand("add_complaint", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                command.Parameters.Add("@text", SqlDbType.VarChar, 300).Value = text;

                command.Parameters.Add("@flag", SqlDbType.Int).Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();
                result = Convert.ToInt32(command.Parameters["@flag"].Value);
            }
            catch (SqlException ex)//print error message
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally//close connection
            {
                // close connection
                if (connection != null)
                {
                    connection.Close();
                }
            }
            return result;
        }
        public static int ChangeComplaintFunc(string complaintID)
        {
            //open connection to db
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = null;
            int result = 0;

            //try execution
            try
            {
                connection.Open();

                command = new SqlCommand("complaint_reviewed", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@complaintID", SqlDbType.Int).Value = complaintID;

                command.Parameters.Add("@flag", SqlDbType.Int).Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();
                result = Convert.ToInt32(command.Parameters["@flag"].Value);
            }
            catch (SqlException ex)//print error message
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                result = -1; //-1 will be interpreted as "error while connecting with the database."
            }
            finally//close connection
            {
                // close connection
                if (connection != null)
                {
                    connection.Close();
                }
            }
            return result;
        }
    }
}