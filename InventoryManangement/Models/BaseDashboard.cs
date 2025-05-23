using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManangement.Models
{
    public class BaseEquipment
    {
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ReceiveDate { get; set; }

        public List<BaseEquipment>  EquipementList()
        {

            List<BaseEquipment> equipements = new List<BaseEquipment>();

        
            var ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            var ConnStringAppSetting = ConfigurationManager.AppSettings["ConnStringAppSetting"].ToString();
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Equipments", sqlConnection);
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
                    BaseEquipment baseDashboard = new BaseEquipment();
                    baseDashboard.EquipmentId = reader["EquipmentId"] != DBNull.Value ? Convert.ToInt32(reader["EquipmentId"]) : 0;
                    baseDashboard.EquipmentName = reader["EquipmentName"] != DBNull.Value ? reader["EquipmentName"].ToString() : string.Empty;
                    baseDashboard.Quantity = reader["Quantity"] != DBNull.Value ? Convert.ToInt32(reader["Quantity"]) : 0;
                    baseDashboard.Stock = reader["Stock"] != DBNull.Value ? Convert.ToInt32(reader["Stock"]) : 0;
                    baseDashboard.EntryDate = reader["EntryDate"] != DBNull.Value ? Convert.ToDateTime(reader["EntryDate"]) : (DateTime?)null;
                    baseDashboard.ReceiveDate = reader["ReceiveDate"] != DBNull.Value ? Convert.ToDateTime(reader["ReceiveDate"]) : (DateTime?)null;
                    equipements.Add(baseDashboard);
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            sqlCommand.Dispose();
            sqlConnection.Close();
            return equipements;
        }

        public int SaveEquipment()
        {
            string Connstring = ConfigurationManager.ConnectionStrings["Connstring"].ToString();
            SqlConnection sqlConnection = new SqlConnection(Connstring);
            sqlConnection.Open();

            string CommandText = "spOST_InsEquipment";
            SqlCommand cmd = new SqlCommand(CommandText, sqlConnection);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@Id", this.EquipmentId));
            cmd.Parameters.Add(new SqlParameter("@Name", this.EquipmentName));
            cmd.Parameters.Add(new SqlParameter("@EcCount", this.Quantity));
            cmd.Parameters.Add(new SqlParameter("@EntryDate", this.EntryDate));
            cmd.Parameters.Add(new SqlParameter("@ReceiveDate", this.ReceiveDate));
            //cmd.Parameters.Add()
            int returnvalue = cmd.ExecuteNonQuery();
            cmd.Dispose();
            sqlConnection.Close();
            return returnvalue;
        }



        public void DeleteRow(int EquipeId)
        {
            string Connstring = ConfigurationManager.ConnectionStrings["Connstring"].ToString();
            SqlConnection sqlConnection = new SqlConnection(Connstring);
            sqlConnection.Open();

            string CommandText = "spOST_InsEquipment";
            SqlCommand cmd = new SqlCommand("DELETE FROM Equipments WHERE EquipmentId = @EquipeId", sqlConnection);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EquipeId", EquipeId);



            //cmd.Parameters.Add()
            int returnvalue = cmd.ExecuteNonQuery();
            cmd.Dispose();
            sqlConnection.Close();

            
        }
        public DataTable ListAssignedEquipment()
        {
            DataTable dataTable = new DataTable();

            string Connstring = ConfigurationManager.ConnectionStrings["Connstring"].ToString();
            SqlConnection sqlConnection = new SqlConnection(Connstring);
            sqlConnection.Open();

            string CommandText = "sp_GetAllAssignedEquipments";
            SqlCommand cmd = new SqlCommand(CommandText, sqlConnection);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            // cmd.ExecuteNonQuery();
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            dataAdapter.Fill(dataTable);
            cmd.Dispose();
            sqlConnection.Close();
            return dataTable;
        }






        public static int SaveEquipmentAssignment(FormCollection frmCol)
        {
            string Connstring = ConfigurationManager.ConnectionStrings["Connstring"].ToString();
            SqlConnection sqlConnection = new SqlConnection(Connstring);
            sqlConnection.Open();

            string CommandText = "spOST_InsEquiAssignment";
            SqlCommand cmd = new SqlCommand(CommandText, sqlConnection);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@CustomerID", Convert.ToInt32(frmCol["txtCustomerID"].ToString())));
            cmd.Parameters.Add(new SqlParameter("@EquipmentID", Convert.ToInt32(frmCol["txtEquipmentID"].ToString())));
            cmd.Parameters.Add(new SqlParameter("@EquiCount", Convert.ToInt32(frmCol["txtQuantity"].ToString())));
            //cmd.Parameters.Add()
            int returnvalue = cmd.ExecuteNonQuery();
            cmd.Dispose();
            sqlConnection.Close();
            return returnvalue;
        }

        public static int DeleteEquipmentAssignment(int AssignId)
        {
            string Connstring = ConfigurationManager.ConnectionStrings["Connstring"].ToString();
            SqlConnection sqlConnection = new SqlConnection(Connstring);
            sqlConnection.Open();

            string CommandText = "spOST_DeleteEquiAssignment";
            SqlCommand cmd = new SqlCommand(CommandText, sqlConnection);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@AssignId",AssignId ));
        
            //cmd.Parameters.Add()
            int returnvalue = cmd.ExecuteNonQuery();
            cmd.Dispose();
            sqlConnection.Close();
            return returnvalue;
        }

    }
}