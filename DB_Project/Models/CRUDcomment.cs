using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DB_Project.Models
{
    public class CRUDcomment
    {
        public static int AddCommentFunc(string movieID, string userID, string comment)
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

                command = new SqlCommand("add_comment", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@movieID", SqlDbType.Int).Value = movieID;
                command.Parameters.Add("@userID", SqlDbType.Int).Value = userID;
                command.Parameters.Add("@comment", SqlDbType.VarChar, 300).Value = comment;

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
        public static List<Comment> MovieCommentFunc(int movieID)
        {
            List<Comment> cList = new List<Comment>();
            //open connection to db
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = null;
            SqlDataReader reader = null;

            //try execution
            try
            {
                connection.Open();

                command = new SqlCommand("comments_movie", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@mID", SqlDbType.Int).Value = movieID;
                reader = command.ExecuteReader();
                Comment c;
                while (reader.Read())
                {
                    c = new Comment();
                    c.userID= reader[0].ToString();
                    c.userName = reader[1].ToString();
                    c.comment = reader[2].ToString();
                    c.dtime = reader[3].ToString();
                    cList.Add(c);
                }
                return cList;
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