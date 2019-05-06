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
        public static List<Actor> SearchActorFunc(string stext)
        {
            List<Actor> aList = new List<Actor>();
            //open connection to db
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = null;
            SqlDataReader reader = null;

            //try execution
            try
            {
                connection.Open();

                command = new SqlCommand("search_actor", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = stext;
                reader = command.ExecuteReader();
                Actor a;
                while (reader.Read())
                {
                    a = new Actor();
                    a.actorID = reader[0].ToString();
                    a.name = reader[1].ToString();
                    aList.Add(a);
                }
                return aList;
            }
            catch (SqlException ex)//print error message
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
            finally//close connection
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        public static int DelActorFunc(string actorID)
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

                command = new SqlCommand("delete_actor", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@aID", SqlDbType.VarChar, 100).Value = actorID;

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
        public static int AddActorFunc(string name, string bdate, string gender, string descript)
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
                // close connection
                if (connection != null)
                {
                    connection.Close();
                }
            }
            return result;
        }
        public static Actor DisplayActorFunc(string actorId)
        {
            Actor a = null;
            //open connection to db
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = null;
            SqlDataReader reader = null;

            //try execution
            try
            {
                connection.Open();

                command = new SqlCommand("actor_details", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@input", SqlDbType.Int).Value = actorId;
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    a = new Actor();
                    a.actorID = reader[0].ToString();
                    a.name = reader[1].ToString();
                    a.bdate = reader[2].ToString();
                    a.gender = reader[3].ToString();
                    a.description = reader[4].ToString();
                }
                return a;
            }
            catch (SqlException ex)//print error message
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
            finally//close connection
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        public static List<Actor> MovieCastFunc(string movieID)
        {
            List<Actor> aList = new List<Actor>();
            //open connection to db
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = null;
            SqlDataReader reader = null;

            //try execution
            try
            {
                connection.Open();

                command = new SqlCommand("cast_movie", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@mID", SqlDbType.Int).Value = movieID;
                reader = command.ExecuteReader();
                Actor a;
                while (reader.Read())
                {
                    a = new Actor();
                    a.actorID = reader[0].ToString();
                    a.name = reader[1].ToString();
                    a.bdate = reader[2].ToString();
                    a.gender = reader[3].ToString();
                    a.description = reader[4].ToString();
                    aList.Add(a);
                }
                return aList;
            }
            catch (SqlException ex)//print error message
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
            finally//close connection
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
        public static List<Actor> RemainingActorsFunc(string movieID)
        {
            List<Actor> aList = new List<Actor>();
            //open connection to db
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = null;
            SqlDataReader reader = null;

            //try execution
            try
            {
                connection.Open();

                command = new SqlCommand("remaining_actors", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@mID", SqlDbType.Int).Value = movieID;
                reader = command.ExecuteReader();
                Actor a;
                while (reader.Read())
                {
                    a = new Actor();
                    a.actorID = reader[0].ToString();
                    a.name = reader[1].ToString();
                    aList.Add(a);
                }
                return aList;
            }
            catch (SqlException ex)//print error message
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
            finally//close connection
            {
                // close reader
                if (reader != null)
                {
                    reader.Close();
                }

                // close connection
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }
    }
}