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
        public static List<Complaint> ShowComplaintFunc()
        {
            List<Complaint> cList = new List<Complaint>();
            //open connection to db
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = null;
            SqlDataReader reader = null;

            //try execution
            try
            {
                connection.Open();

                command = new SqlCommand("all_complaints", connection);
                reader = command.ExecuteReader();
                Complaint c;
                while (reader.Read())
                {
                    c = new Complaint();
                    c.complaintID = reader[0].ToString();
                    c.userID = reader[1].ToString();
                    c.dtime = reader[2].ToString();
                    c.text = reader[3].ToString();
                    c.status = reader[4].ToString();
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