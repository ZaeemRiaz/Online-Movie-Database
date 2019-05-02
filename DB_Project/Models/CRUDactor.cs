using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DB_Project.Models
{
    public class CRUDactor
    {
        public static int SearchActorFunc(string email, string password)
        {

            return 0;
        }
        public static int DelActorFunc(string email, string password)
        {
            //open connection to db
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            int result = 0;

            //try execution
            try
            {
                connection.Open();

                command = new SqlCommand("delete_user", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;

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
                connection.Close();
            }
            return result;
        }
        public static int AddActorFunc(string email, string password)
        {

            return 0;
        }
        public static int DisplayActorFunc(string email, string password)
        {

            return 0;
        }
        public static int MovieCastFunc(string email, string password)
        {

            return 0;
        }
    }
}