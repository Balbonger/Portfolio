using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibsInventoryManagementSystem
{
    internal class ClientsData
    {
        public int ID { get; set; }
        public string Client { get; set; }
        public string Contact { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }

        public List<ClientsData> AllClientsData()
        {
            List<ClientsData> listData = new List<ClientsData>();
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = "SELECT * FROM client";

                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ClientsData sData = new ClientsData();
                        sData.ID = (int)reader["id"];
                        sData.Client = reader["client_name"].ToString();
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

        public List<ClientsData> FilterClietntsData()
        {
            List<ClientsData> listData = new List<ClientsData>();
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = "SELECT * FROM client WHERE id LIKE '%' @location '%' OR" +
                    " client_name LIKE '%' @location '%' " +
                    "OR contact_info LIKE '%' @location '%' " +
                    "OR contact_person LIKE '%' @location '%' " +
                    "OR address LIKE '%' @location '%' ";


                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {

                    cmd.Parameters.AddWithValue("@location", AddClient.clientfilter.ToString());

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ClientsData sData = new ClientsData();
                        sData.ID = (int)reader["id"];
                        sData.Client = reader["client_name"].ToString();
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
