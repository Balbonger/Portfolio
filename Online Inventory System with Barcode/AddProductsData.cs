using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace NibsInventoryManagementSystem
{
    internal class AddProductsData
    {
        public int ID { set; get; } //0

        public string ProdID { set; get; } //1

        public string ProdName { set; get; } //2

        public string Category { set; get; } //3

        public string Price { set; get; } //4

        public string Stock { set; get; } //5

        public string Unit { set; get; } //6

        public string Status { set; get; } //7

        public string Date { set; get; } //8

        public string Location { set; get; } //9

        public string PersonInCharge { set; get; } // 10

        public string Supplier { set; get; } //11




        public List<AddProductsData> AllProductsData()
        {
            List<AddProductsData> listData = new List<AddProductsData>();
            using (MySqlConnection connect = new MySqlConnection(Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = "SELECT * FROM products";

                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        AddProductsData apData = new AddProductsData();
                        apData.ID = (int)reader["id"];
                        apData.ProdID = reader["prod_id"].ToString();
                        apData.ProdName = reader["prod_name"].ToString();
                        apData.Category = reader["category"].ToString();
                        apData.Price = reader["price"].ToString();
                        apData.Stock = reader["stock"].ToString();
                        apData.Unit = reader["unit"].ToString();
                        apData.Status = reader["status"].ToString();
                        apData.Date = reader["date_insert"].ToString();

                        apData.Location = reader["prod_location"].ToString();
                        apData.PersonInCharge = reader["prod_personnel"].ToString();

                        apData.Supplier = reader["prod_supplier"].ToString();
                        listData.Add(apData);
                    }
                }
            }

            return listData;
        }

        public List<AddProductsData> allAvailableProducts()
        {
            List<AddProductsData> listData = new List<AddProductsData>();
            using (MySqlConnection connect = new MySqlConnection (Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = "SELECT * FROM products WHERE status = @status AND stock >0";

                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {
                    cmd.Parameters.AddWithValue("@status", "Available");
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        AddProductsData apData = new AddProductsData();
                        apData.ID = (int)reader["id"];
                        apData.ProdID = reader["prod_id"].ToString();
                        apData.ProdName = reader["prod_name"].ToString();
                        apData.Category = reader["category"].ToString();
                        apData.Price = reader["price"].ToString();
                        apData.Stock = reader["stock"].ToString();
                        apData.Unit = reader["unit"].ToString();
                        apData.Status = reader["status"].ToString();
                        apData.Date = reader["date_insert"].ToString();

                        apData.Location = reader["prod_location"].ToString();
                        apData.PersonInCharge = reader["prod_personnel"].ToString();

                        apData.Supplier = reader["prod_supplier"].ToString();

                        listData.Add(apData);
                    }
                }
            }

            return listData;
        }

        public List<AddProductsData> FilterProductsData()
        {
            List<AddProductsData> listData = new List<AddProductsData>();
            using (MySqlConnection connect = new MySqlConnection (Form1.gateway))
            {
                connect.Open();

                MySqlCommand direct = new MySqlCommand(Form1.database, connect);
                direct.ExecuteNonQuery();


                string selectData = "SELECT * FROM products WHERE prod_location LIKE '%' @location '%' OR" +
                    " prod_id LIKE '%' @location '%' " +
                    " OR prod_name LIKE '%' @location '%' " +
                    "OR prod_personnel LIKE '%' @location '%' " +
                    "OR category LIKE '%' @location '%' " +
                    "OR status LIKE '%' @location '%' " +
                    "OR prod_supplier LIKE '%' @location '%' ";
                    
                using (MySqlCommand cmd = new MySqlCommand(selectData, connect))
                {

                    cmd.Parameters.AddWithValue("@location", AdminAddProducts.filter.ToString());

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        AddProductsData apData = new AddProductsData();
                        apData.ID = (int)reader["id"];
                        apData.ProdID = reader["prod_id"].ToString();
                        apData.ProdName = reader["prod_name"].ToString();
                        apData.Category = reader["category"].ToString();
                        apData.Price = reader["price"].ToString();
                        apData.Stock = reader["stock"].ToString();
                        apData.Unit = reader["unit"].ToString();
                        apData.Status = reader["status"].ToString();
                        apData.Date = reader["date_insert"].ToString();

                        apData.Location = reader["prod_location"].ToString();
                        apData.PersonInCharge = reader["prod_personnel"].ToString();

                        apData.Supplier = reader["prod_supplier"].ToString();
                        listData.Add(apData);
                    }
                }
            }

            return listData;
        }
    }


}
