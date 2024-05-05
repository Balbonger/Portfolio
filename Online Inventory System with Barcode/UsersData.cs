using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace NibsInventoryManagementSystem
{
    internal class UsersData
    {
        public int ID { set; get; }

        public string Username { set; get; }

        public string Password { set; get; }

        public string Role { set; get; }

        public string Status { set; get; }

        public string Date { set; get; }


        public List<UsersData> AllUsersData()
        {
            List<UsersData> listData = new List<UsersData>();
            using (MySqlConnection connect
                = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = "SELECT * FROM users";

                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        UsersData uData = new UsersData();
                        uData.ID = (int)reader["id"];
                        uData.Username = reader["username"].ToString();
                        uData.Password = reader["password"].ToString();
                        uData.Role = reader["role"].ToString();
                        uData.Status = reader["status"].ToString();
                        uData.Date = reader["date"].ToString();

                        listData.Add(uData);
                    }
                }
            }

            return listData;
        }

        public List<UsersData> FilterUsersData()
        {
            List<UsersData> listData = new List<UsersData>();
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = "SELECT * FROM users WHERE id LIKE '%' @location '%' OR" +
                    " username LIKE '%' @location '%' " +
                    " OR role LIKE '%' @location '%' " +
                    "OR status LIKE '%' @location '%' " +
                    "OR date LIKE '%' @location '%' ";


                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {

                    cmd.Parameters.AddWithValue("@location", AdminAddUsers.userfilter.ToString());

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        UsersData uData = new UsersData();
                        uData.ID = (int)reader["id"];
                        uData.Username = reader["username"].ToString();
                        uData.Password = reader["password"].ToString();
                        uData.Role = reader["role"].ToString();
                        uData.Status = reader["status"].ToString();
                        uData.Date = reader["date"].ToString();

                        listData.Add(uData);
                    }
                }
            }

            return listData;
        }
    }
}
