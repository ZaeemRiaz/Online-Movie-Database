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
        public static List<Actor> SearchActorFunc(string email, string password)
        {
            List<Actor> alist = null;
            return alist;
        }
        public static int DelActorFunc(int actorId)
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

                command = new SqlCommand("delete_actor", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@aID", SqlDbType.VarChar, 100).Value = actorId;

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
        public static int AddActorFunc(string name, string bdate, string gender, string descript)
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

                command = new SqlCommand("add_actor", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@title", SqlDbType.VarChar, 100).Value = name;
                command.Parameters.Add("@descript", SqlDbType.VarChar, 10).Value = bdate;
                command.Parameters.Add("@genre", SqlDbType.Char).Value = gender;
                command.Parameters.Add("@releasedate", SqlDbType.VarChar, 300).Value = descript;

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
        public static Actor DisplayActorFunc(int actorId)
        {
            Actor a= null;
            return a;
        }
        public static List<Actor> MovieCastFunc(int movieId)
        {
            List<Actor> alist = null;
            return alist;
        }
    }
}