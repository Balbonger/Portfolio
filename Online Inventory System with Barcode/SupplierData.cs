using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibsInventoryManagementSystem
{
    internal class SupplierData
    {
        public int ID { get; set; }
        public string Supplier { get; set; }
        public string Contact { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }

        public List<SupplierData> AllSuppliersData()
        {
            List<SupplierData> listData = new List<SupplierData>();
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = "SELECT * FROM supplier";

                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SupplierData sData = new SupplierData();
                        sData.ID = (int)reader["id"];
                        sData.Supplier = reader["supplier_name"].ToString();
                        sData.Contact = reader["contact_info"].ToString();
                        sData.ContactPerson = reader["contact_person"].ToString();
                        sData.Address = reader["address"].ToString();
                        sData.Date = reader["date"].ToString();

                        listData.Add(sData);
                    }
                }
            }

            return listData;
        }

        public List<SupplierData> FilterSupplierData()
        {
            List<SupplierData> listData = new List<SupplierData>();
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = "SELECT * FROM supplier WHERE id LIKE '%' @location '%' OR" +
                    " supplier_name LIKE '%' @location '%' " +
                    "OR contact_info LIKE '%' @location '%' " +
                    "OR contact_person LIKE '%' @location '%' " +
                    "OR address LIKE '%' @location '%' ";


                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {

                    cmd.Parameters.AddWithValue("@location", AddClient.clientfilter.ToString());

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SupplierData sData = new SupplierData();
                        sData.ID = (int)reader["id"];
                        sData.Supplier = reader["supplier_name"].ToString();
                        sData.Contact = reader["contact_info"].ToString();
                        sData.ContactPerson = reader["contact_person"].ToString();
                        sData.Address = reader["address"].ToString();
                        sData.Date = reader["date"].ToString();

                        listData.Add(sData);
                    }
                }
            }

            return listData;
        }
    }
}
