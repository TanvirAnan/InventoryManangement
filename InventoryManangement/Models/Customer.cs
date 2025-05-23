using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InventoryManangement.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Adress { get; set; }
        public string PhoneNo { get; set; }


        public List<Customer> CustomerList()
        {

            List<Customer> Customers = new List<Customer>();


            var ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            var ConnStringAppSetting = ConfigurationManager.AppSettings["ConnStringAppSetting"].ToString();
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Customer", sqlConnection);
            //WHERE Username = @Username AND Password = @Password


            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlCommand.CommandTimeout = 0;
            sqlCommand.Parameters.Clear();


            //list

            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Customer ct = new Customer();
                    ct.CustomerId = reader["CustomerId"] != DBNull.Value ? Convert.ToInt32(reader["CustomerId"]) : 0;
                    ct.CustomerName = reader["CustomerName"] != DBNull.Value ? reader["CustomerName"].ToString() : string.Empty;
                    ct.Adress = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : string.Empty;
                    ct.PhoneNo = reader["PhoneNo"] != DBNull.Value ? reader["PhoneNo"].ToString() : string.Empty;

                    Customers.Add(ct);
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            sqlCommand.Dispose();
            sqlConnection.Close();
            return Customers;
        }
    }
}