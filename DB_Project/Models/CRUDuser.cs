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
        public static int LoginFunc(string email, string password, string utype)
        {
            //@email varchar(50),
            //@passcode varchar(50),
            //@uIDOUT     int OUTPUT,
            //@uTypeOUT   char(1) OUTPUT
            //open connection to db
            string connectionString = @"Data Source=localhost;Initial Catalog=muz;Integrated Security=True;";

            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command;
            int result = 0;

            //try execution
            try
            {
                connection.Open();

                command = new SqlCommand("login_user", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = email;
                command.Parameters.Add("@passcode", SqlDbType.VarChar, 50).Value = password;

                command.Parameters.Add("@uIDOUT", SqlDbType.Int).Direction = ParameterDirection.Output;
                command.Parameters.Add("@uTypeOUT", SqlDbType.Char, 1).Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();
                result = Convert.ToInt32(command.Parameters["@uIDOUT"].Value);
                utype = Convert.ToString(command.Parameters["@uTypeOUT"].Value);
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
        public static int SignupFunc(string email, string name, string usertype, string dateOfBirth, string password)
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

                command = new SqlCommand("add_user", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("@name", SqlDbType.VarChar, 100).Value = name;
                command.Parameters.Add("@type", SqlDbType.VarChar, 1).Value = usertype;
                command.Parameters.Add("@email", SqlDbType.VarChar, 50).Value = email;
                command.Parameters.Add("@pass", SqlDbType.VarChar, 50).Value = password;
                command.Parameters.Add("@bdate", SqlDbType.VarChar, 10).Value = dateOfBirth;

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
        public static int DelUserFunc(int userID)
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
    }
}