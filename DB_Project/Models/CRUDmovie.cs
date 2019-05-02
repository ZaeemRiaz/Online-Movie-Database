using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DB_Project.Models
{
    public class CRUDmovie
    {
        public static int SearchMovieFunc(string email, string password)
        {

            return 0;
        }
        public static int DelMovieFunc(int movieId)
        {

            return 0;
        }
        public static int AddMovieFunc(string title, string descript, string genre, string releasedate)
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

                command = new SqlCommand("add_movie", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@title", SqlDbType.VarChar, 100).Value = title;
                command.Parameters.Add("@descript", SqlDbType.VarChar, 300).Value = descript;
                command.Parameters.Add("@genre", SqlDbType.VarChar, 100).Value = genre;
                command.Parameters.Add("@releasedate", SqlDbType.VarChar, 10).Value = releasedate;

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
        public static int MovieDetailFunc(string email, string password)
        {

            return 0;
        }
        public static SqlDataReader AllMovieFunc(string email, string password)
        {
            //open connection to db
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;

            //try execution
            try
            {
                connection.Open();

                command = new SqlCommand("all_movies", connection);
                SqlDataReader reader = command.ExecuteReader();
                return reader;
            }
            catch (SqlException ex)//print error message
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
            finally//close connection
            {
                connection.Close();
            }
        }
        public static SqlDataReader TopMovieFunc(string email, string password)
        {
            //open connection to db
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;

            //try execution
            try
            {
                connection.Open();

                command = new SqlCommand("top_movies", connection);
                SqlDataReader reader = command.ExecuteReader();
                return reader;
            }
            catch (SqlException ex)//print error message
            {
                Console.WriteLine("SQL Error" + ex.Message.ToString());
                return null;
            }
            finally//close connection
            {
                connection.Close();
            }
        }
    }
}