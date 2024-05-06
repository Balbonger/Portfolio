using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace NibsInventoryManagementSystem
{
    internal class CategoriesData
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }

        public List<CategoriesData> AllCategoriesData()
        {
            List<CategoriesData> listData = new List<CategoriesData>();
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = "SELECT * FROM categories";

                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CategoriesData cData = new CategoriesData();
                        cData.ID = (int)reader["id"];
                        cData.Category = reader["category"].ToString();
                        cData.Date = reader["date"].ToString();

                        listData.Add(cData);
                    }
                }
            }

            return listData;
        }

        public List<CategoriesData> FilterCategoriesData()
        {
            List<CategoriesData> listData = new List<CategoriesData>();
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = "SELECT * FROM categories WHERE id LIKE '%' @location '%' OR" +
                    " category LIKE '%' @location '%' ";


                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {

                    cmd.Parameters.AddWithValue("@location", AdminAddCategories.categoryfilter.ToString());

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        CategoriesData cData = new CategoriesData();
                        cData.ID = (int)reader["id"];
                        cData.Category = reader["category"].ToString();
                        cData.Date = reader["date"].ToString();

                        listData.Add(cData);
                    }
                }
            }

            return listData;
        }
    }
}
