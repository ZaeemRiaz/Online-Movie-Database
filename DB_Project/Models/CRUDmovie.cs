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
        public static List<Movie> SearchMovieFunc(string stext)
        {
            List<Movie> mList = new List<Movie>();
            //open connection to db
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = null;
            SqlDataReader reader = null;

            //try execution
            try
            {
                connection.Open();

                command = new SqlCommand("search_movie", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = stext;
                reader = command.ExecuteReader();
                Movie m;
                while (reader.Read())
                {
                    m = new Movie();
                    m.movieID = reader[0].ToString();
                    m.title = reader[1].ToString();
                    m.rating = reader[2].ToString();
                    m.descript = reader[3].ToString();
                    m.genre = reader[4].ToString();
                    m.releasedate = reader[5].ToString();
                    m.picture = reader[6].ToString();
                    mList.Add(m);
                }
                return mList;
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
        public static int DelMovieFunc(string movieId)
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

                command = new SqlCommand("delete_movie", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@mID", SqlDbType.Int).Value = movieId;

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
        public static int AddMovieFunc(string title, string descript, string genre, string releasedate, string picture)
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

                command = new SqlCommand("add_movie", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@title", SqlDbType.VarChar, 100).Value = title;
                command.Parameters.Add("@descript", SqlDbType.VarChar, 300).Value = descript;
                command.Parameters.Add("@genre", SqlDbType.VarChar, 100).Value = genre;
                command.Parameters.Add("@releasedate", SqlDbType.VarChar, 10).Value = releasedate;
                command.Parameters.Add("@pic", SqlDbType.VarChar, 10).Value = picture;

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
        public static Movie MovieDetailFunc(string movieId)
        {
            //open connection to db
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = null;
            SqlDataReader reader = null;

            //try execution
            try
            {
                connection.Open();

                command = new SqlCommand("movie_details", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@input", SqlDbType.Int).Value = movieId;
                reader = command.ExecuteReader();
                Movie m = null;
                if (reader.Read())
                {
                    m = new Movie();
                    m.movieID = reader[0].ToString();
                    m.title = reader[1].ToString();
                    m.rating = reader[2].ToString();
                    m.descript = reader[3].ToString();
                    m.genre = reader[4].ToString();
                    m.releasedate = reader[5].ToString();
                    m.picture = reader[6].ToString();
                }
                
                return m;
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
        public static List<Movie> AllMovieFunc()
        {
            List<Movie> mList = new List<Movie>();
            //open connection to db
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = null;
            SqlDataReader reader = null;

            //try execution
            try
            {
                connection.Open();

                command = new SqlCommand("all_movies", connection);
                reader = command.ExecuteReader();
                Movie m;
                while (reader.Read())
                {
                    m = new Movie();
                    m.movieID = reader[0].ToString();
                    m.title = reader[1].ToString();
                    m.releasedate = reader[2].ToString();
                    m.rating = reader[3].ToString();
                    mList.Add(m);
                }
                return mList;
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
        public static List<Movie> TopMovieFunc()
        {
            List<Movie> mList = new List<Movie>();
            //open connection to db
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = null;
            SqlDataReader reader = null;

            //try execution
            try
            {
                connection.Open();

                command = new SqlCommand("top_movies", connection);
                reader = command.ExecuteReader();
                Movie m;
                while (reader.Read())
                {
                    m = new Movie();
                    m.movieID = reader[0].ToString();
                    m.title = reader[1].ToString();
                    m.releasedate = reader[2].ToString();
                    m.rating = reader[3].ToString();
                    //TODO: add picture 
                    mList.Add(m);
                }
                return mList;
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
        public static int EditMovieTitleFunc(string movieId, string title)
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

                command = new SqlCommand("update_movie_title", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@mID", SqlDbType.Int).Value = movieId;
                command.Parameters.Add("@title", SqlDbType.VarChar, 100).Value = title;

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
        public static int EditMovieGenreFunc(string movieId, string genre)
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

                command = new SqlCommand("update_movie_genre", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@mID", SqlDbType.Int).Value = movieId;
                command.Parameters.Add("@genre", SqlDbType.VarChar, 100).Value = genre;

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
        public static int EditMovieDescriptionFunc(string movieId, string description)
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

                command = new SqlCommand("update_movie_descript", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@mID", SqlDbType.Int).Value = movieId;
                command.Parameters.Add("@descript", SqlDbType.VarChar, 300).Value = description;

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
        public static int EditMovieDateofReleaseFunc(string movieId, string releaseDate)
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

                command = new SqlCommand("update_movie_releasedate", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@mID", SqlDbType.Int).Value = movieId;
                command.Parameters.Add("@releasedate", SqlDbType.VarChar, 10).Value = releaseDate;

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