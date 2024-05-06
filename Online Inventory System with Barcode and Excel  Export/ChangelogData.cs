using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NibsInventoryManagementSystem
{
    internal class ChangelogData
    {
        //public int ID { set; get; } //0

        public string LogID { set; get; } //1

        public string Activity { set; get; } //2

        public string Destination { set; get; } //3

        public string Personnel { set; get; } //4

        public string Client { set; get; } //5

        public string User { set; get; } //6

        public string Date { set; get; } //7




        public List<ChangelogData> AllChangelogData()
        {
            List<ChangelogData> listData = new List<ChangelogData>();
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = "SELECT * FROM changelog ORDER BY id DESC";

                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ChangelogData apData = new ChangelogData();
                        //apData.ID = (int)reader["id"];
                        apData.LogID = reader["log_id"].ToString();
                        apData.Activity = reader["prod_name"].ToString();
                        apData.Destination = reader["destination"].ToString();
                        apData.Personnel = reader["person_incharge"].ToString();
                        apData.Client = reader["client"].ToString();
                        apData.User = reader["log_user"].ToString();
                        apData.Date = reader["log_date"].ToString();
                        

                        listData.Add(apData);
                    }
                }
            }

            return listData;
        }
    }
}
