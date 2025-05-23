using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventoryManangement.Models
{
    public class BaseMember
    {
        public  int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public DataTable validateasTable(string username, string password)
        {

            DataTable dt = new DataTable();
            var ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            var ConnStringAppSetting = ConfigurationManager.AppSettings["ConnStringAppSetting"].ToString();
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Users", sqlConnection);
            //WHERE Username = @Username AND Password = @Password


            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlCommand.CommandTimeout = 0;
            sqlCommand.Parameters.Clear();


            //table 

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            da.Fill(dt);
            sqlCommand.Dispose();
            sqlConnection.Close();
            return dt;
        }

        //Store Procedure

        public DataTable validateasTableBySp(string username, string password)
        {

            DataTable dt = new DataTable();
            var ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            var ConnStringAppSetting = ConfigurationManager.AppSettings["ConnStringAppSetting"].ToString();
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = "GetUsers";
            sqlCommand.Connection = sqlConnection;
            //WHERE Username = @Username AND Password = @Password


            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            sqlCommand.CommandTimeout = 0;
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@Username", username);
            sqlCommand.Parameters.AddWithValue("@PasswordHash", password);


            //table 

            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            da.Fill(dt);
            sqlCommand.Dispose();
            sqlConnection.Close();
            return dt;
        }




        public List<BaseMember> validateasList(string username, string password)
        {

            List<BaseMember> baseMembers = new List<BaseMember>();

            DataTable dt = new DataTable();
            var ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            var ConnStringAppSetting = ConfigurationManager.AppSettings["ConnStringAppSetting"].ToString();
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Users", sqlConnection);
            //WHERE Username = @Username AND Password = @Password


            sqlCommand.CommandType= System.Data.CommandType.Text;
            sqlCommand.CommandTimeout = 0;
            sqlCommand.Parameters.Clear();


            //list

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if(reader.HasRows)
            {
               while(reader.Read())
                {
                    BaseMember baseMember = new BaseMember();
                    baseMember.Id = Convert.ToInt32(reader["Id"]);
                    baseMember.Username = reader["Username"].ToString();
                    baseMember.Password = reader["PasswordHash"].ToString();
                    baseMembers.Add(baseMember);
                }
            }
            else
            {
                Console.WriteLine("No rows found."); 
            }
            sqlCommand.Dispose();
            sqlConnection.Close();
            return baseMembers;
        }

        
    }
}